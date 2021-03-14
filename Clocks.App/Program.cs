using Clocks.App.Button;
using Clocks.App.Display;
using Clocks.Classes;
using Iot.Device.CharacterLcd;
using Iot.Device.Pcx857x;
using System;
using System.Device.Gpio;
using System.Device.I2c;
using System.Threading;
using System.Threading.Tasks;

namespace Clocks.App
{
    class Program
    {
        public static ClockRunner _clockRunner;

        static void Main(string[] args)
        {
            Console.WriteLine("Displaying current time. Press Ctrl+C to end.");

            using I2cDevice i2c = I2cDevice.Create(new I2cConnectionSettings(1, 0x27));
            using var driver = new Pcf8574(i2c);
            using var gpc = new GpioController(PinNumberingScheme.Logical, driver);
            using var lcd = new Lcd2004(registerSelectPin: 0,
                               enablePin: 2,
                               dataPins: new int[] { 4, 5, 6, 7 },
                               backlightPin: 3,
                               backlightBrightness: 1.0f,
                               readWritePin: 1,
                               controller: gpc);

            using var buttonGpc = new GpioController(PinNumberingScheme.Logical);


            bool runningOnPi = true;
            IClockDisplay display = default;
            IButtonHandler button1 = default;
            IButtonHandler button2 = default;

            if (runningOnPi)
            {
                display = new LcdClockDisplay(lcd);
                button1 = new PiPhysicalButton(buttonGpc);
                button2 = new PiPhysicalButton(buttonGpc);
            }
            else
            {
                display = new ConsoleClockDIsplay();
                button1 = new ConsolePhysicalButton();
                button2 = new ConsolePhysicalButton();
            }

            button1.Initialize(18);
            button2.Initialize(25);
            
            button1.Click += ButtonClick;
            button2.Click += ButtonClick;

            button1.Held += ButtonHeld;
            button2.Held += ButtonHeld;


            _clockRunner = new ClockRunner(display);
            _clockRunner.Clock1 = new StandardTime();
            _clockRunner.Clock2 = new MetricTime();


            _clockRunner.RunClockAsync();
            //var clock = Task.Factory.StartNew(() => _clockRunner.RunClockAsync());
            //clock.Wait(Timeout.Infinite);

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