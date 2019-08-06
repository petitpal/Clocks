using Clocks.Classes;
using System;

namespace Clocks
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            ITime<int> time = new DecimalTime();
            
            while (true)
            {
                var now = DateTime.Now.TimeOfDay;
                time.PopulateFromUtc(now);
                Console.SetCursorPosition(0, 0);
                Console.Write($"UTC: [{GetDisplayString(now.Hours, now.Minutes, now.Seconds)} ] Decimal: [{GetDisplayString(time.Hours, time.Minutes, time.Seconds)}]");
                System.Threading.Thread.Sleep(100);
            }
        }

        static string GetDisplayString(int hours, int minutes, int seconds) =>
            $"{hours.ToString("00")}:{minutes.ToString("00")}:{seconds.ToString("00")}";
    }
}
