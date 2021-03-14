using Clocks.App.Display;
using Clocks.Classes;
using System;
using System.Threading;

namespace Clocks.App
{
    public class ClockRunner
    {
        private readonly IClockDisplay _display;

        public ClockRunner(IClockDisplay display)
        {
            _display = display;
        }

        public ITime Clock1 { get; set; }
        public ITime Clock2 { get; set; }

        public void RunClockAsync()
        {
            _display.Clear();
            //_display.SetCursorPosition(3, 0);

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

                Thread.Sleep(500);
            }
        }

        private void DisplayClock(int line, ITime time)
        {
            if (time == null) return;
            _display.SetCursorPosition(0, line);
            _display.Write($"{time.ClockAbbreviation}: {time.ToShortString()}");
        }
    }
}