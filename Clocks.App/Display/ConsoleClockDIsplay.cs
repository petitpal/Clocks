using System;

namespace Clocks.App.Display
{
    public class ConsoleClockDisplay : IClockDisplay
    {
        public void Clear() =>
            Console.Clear();

        public void SetCursorPosition(int left, int top) =>
            Console.SetCursorPosition(left, top);

        public void Write(string message) =>
            Console.Write(message);
    }
}
