using Clocks.App.Button;
using Clocks.App.Display;
using Clocks.App.Factories;
using Clocks.Classes;
using Iot.Device.CharacterLcd;
using Iot.Device.Pcx857x;
using System;
using System.Device.Gpio;
using System.Device.I2c;

namespace Clocks.App
{
    class Program
    {
        public static ClockRunner _clockRunner;

        static void Main(string[] args)
        {
            Console.WriteLine("Displaying current time. Press Ctrl+C to end.");

            bool runningOnPi = true;

            IControllerFactory controllerFactory;
            IDeviceFactory deviceFactory;

            if (runningOnPi)
            {
                controllerFactory = new PiControllerFactory();
                deviceFactory = new PiDeviceFactory(controllerFactory);
            }
            else
            {
                deviceFactory = new ConsoleDeviceFactory();
            }


            IClockDisplay display = deviceFactory.GetDisplay();

            var button1 = deviceFactory.GetButton();
            ConfigureButton(button1, 18);

            var button2 = deviceFactory.GetButton();
            ConfigureButton(button2, 25);


            _clockRunner = new ClockRunner(display);
            _clockRunner.Clock1 = new StandardTime();
            _clockRunner.Clock2 = new MetricTime();


            _clockRunner.RunClock();
            //var clock = Task.Factory.StartNew(() => _clockRunner.RunClockAsync());
            //clock.Wait(Timeout.Infinite);
        }

        public static IPhysicalButton ConfigureButton(IPhysicalButton button, int pin)
        {
            button.Initialize(pin);
            button.Click += ButtonClick;
            button.Held += ButtonHeld;
            return button;
        }

        public static void ButtonClick(object sender, ButtonHandlerEventArgs e)
        {
            switch (e.pin)
            {
                case 18:
                    _clockRunner.Clock1 = NextClock(_clockRunner.Clock1);
                    break;
                case 25:
                    _clockRunner.Clock2 = NextClock(_clockRunner.Clock2);
                    break;
                default:
                    Console.WriteLine($"Unrecongised pin {e.pin}");
                    break;
            }
            Console.WriteLine($"Button pressed on pin {e.pin}");
        }

        public static void ButtonHeld(object sender, ButtonHandlerEventArgs e)
        {
            Console.WriteLine($"Button held on pin {e.pin}");
        }

        public static ITime NextClock(ITime currentClock)
        {
            switch (currentClock)
            {
                case StandardTime t1: return new MetricTime();
                default: return new StandardTime();
            }
        }

    }
}