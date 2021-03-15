using Clocks.App.Button;
using Clocks.App.Display;
using Clocks.App.Factories;
using Clocks.Classes;
using System;
using System.Threading;

namespace Clocks.App.ClockRunner
{
    public class ClockController : IClockController
    {
        private readonly IClockDisplay _display;
        private readonly IPhysicalButton _button1;
        private readonly IPhysicalButton _button2;

        public ClockController(IDeviceFactory deviceFactory)
        {
            _display = deviceFactory.GetDisplay();
            _button1 = deviceFactory.GetButton();
            _button2 = deviceFactory.GetButton();
        }

        public ITime Clock1 { get; set; }
        public ITime Clock2 { get; set; }

        public void RunClock()
        {
            _display.Clear();

            ConfigureButton(_button1, 18);
            ConfigureButton(_button2, 25);

            while (true)
            {
                var now = DateTime.Now.TimeOfDay;

                if (Clock1 != null)
                {
                    Clock1.PopulateFromUtc(now);
                    DisplayClock(0, Clock1);
                }

                if (Clock2 != null)
                {
                    Clock2.PopulateFromUtc(now);
                    DisplayClock(1, Clock2);
                }

                Thread.Sleep(100);
            }
        }

        private void DisplayClock(int line, ITime time)
        {
            if (time == null) return;
            _display.SetCursorPosition(0, line);
            _display.Write($"{time.ClockAbbreviation} {time.ToShortString()}");
        }

        public IPhysicalButton ConfigureButton(IPhysicalButton button, int pin)
        {
            button.Initialize(pin);
            button.Click += ButtonClick;
            button.Held += ButtonHeld;
            return button;
        }

        public void ButtonClick(object sender, ButtonHandlerEventArgs e)
        {
            switch (e.pin)
            {
                case 18:
                    Clock1 = NextClock(Clock1);
                    break;
                case 25:
                    Clock2 = NextClock(Clock2);
                    break;
                default:
                    Console.WriteLine($"Unrecongised pin {e.pin}");
                    break;
            }
            Console.WriteLine($"Button pressed on pin {e.pin}");
        }

        public void ButtonHeld(object sender, ButtonHandlerEventArgs e)
        {
            Console.WriteLine($"Button held on pin {e.pin}");
        }

        public ITime NextClock(ITime currentClock)
        {
            switch (currentClock)
            {
                case StandardTime t1: return new MetricTime();
                case MetricTime t2: return new BinaryTime(14);
                default: return new StandardTime();
            }
        }


    }
}