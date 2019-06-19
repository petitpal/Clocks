using System;

namespace Clocks.Classes
{
    public struct DecimalTime
    {
        public const int HoursPerDay = 10;
        public const int MinutesPerHour = 100;
        public const int SecondsPerHour = 10000;
        public const int SecondsPerMinute = 100;
        public const double ConvertUtcSecondsToDecimalSeconds = 0.864;

        public int Hours { get; set; }
        public int Minutes { get; set; }
        public int Seconds { get; set; }

        public DecimalTime Empty()
        {
            return default;
        }

        public bool Equals(DecimalTime dc1, DecimalTime dc2)
        {
            return (dc1.Hours == dc2.Hours && dc1.Minutes == dc2.Minutes && dc1.Seconds == dc2.Seconds);
        }

        public DecimalTime Now()
        {
            return FromUtc(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
        }

        public static DecimalTime FromUtc(int hours, int minutes, int seconds)
        {
            var totalUtcSeconds = (hours * 60 * 60) + (minutes * 60) + seconds;
            var totalDecimalSeconds = totalUtcSeconds / ConvertUtcSecondsToDecimalSeconds;

            var decimalHours = (int)totalDecimalSeconds / SecondsPerHour;
            totalDecimalSeconds -= (decimalHours * SecondsPerHour);

            var decimalMinutes = (int)totalDecimalSeconds / SecondsPerMinute;
            totalDecimalSeconds -= (decimalMinutes * SecondsPerMinute);

            var decimalSeconds = (int)totalDecimalSeconds;

            return new DecimalTime() { Hours = decimalHours, Minutes = decimalMinutes, Seconds = decimalSeconds };
        }

        //public static TimeSpan ToUtc(DecimalTime dc)
        //{

        //}

        //public TimeSpan ToUtc()
        //{
        //    return ToUtc(this);
        //}
    }
}
