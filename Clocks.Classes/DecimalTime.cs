using System;

namespace Clocks.Classes
{
    public struct DecimalTime
    {
        public const int HoursPerDay = 10;
        public const int MinutesPerHour = 100;
        public const int SecondsPerMinute = 100;
        public const int MillisecondsPerSecond = 1000;

        // private const int DecMsPerDay = 100000000;
        // private const int UtcMsPerDay = 86400000;

        private const int MsPerHour = 10000000;
        private const int MsPerMinute = 100000;
        private const int MsPerSecond = 1000;

        public int Hours { get; set; }
        public int Minutes { get; set; }
        public int Seconds { get; set; }
        public int Milliseconds { get; set; }

        public DecimalTime(int hours, int minutes, int seconds, int milliseconds)
        {
            Hours = hours;
            Minutes = minutes;
            Seconds = seconds;
            Milliseconds = milliseconds;
        }

        public DecimalTime Empty() =>
            default;

        public bool Equals(DecimalTime dc1, DecimalTime dc2) =>
            dc1.Hours == dc2.Hours
            && dc1.Minutes == dc2.Minutes
            && dc1.Seconds == dc2.Seconds
            && dc1.Milliseconds == dc2.Milliseconds;

        public DecimalTime Now() =>
            FromUtc(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second, DateTime.Now.Millisecond);

        public static DecimalTime FromUtc(int hours, int minutes, int seconds, int milliseconds)
        {
            const double UtcMsToDecimalMs = 0.864;

            var utcTotalMs = new TimeSpan(0, hours, minutes, seconds, milliseconds).TotalMilliseconds;
            var decTotalMs = utcTotalMs / UtcMsToDecimalMs;

            return new DecimalTime()
            {
                Hours = ExtractUnitByMs(ref decTotalMs, MsPerHour),
                Minutes = ExtractUnitByMs(ref decTotalMs, MsPerMinute),
                Seconds = ExtractUnitByMs(ref decTotalMs, MsPerSecond),
                Milliseconds = (int)decTotalMs
            };
        }

        private static int ExtractUnitByMs(ref double totalMs, int msPerUnit)
        {
            var t = (int)(totalMs / msPerUnit);
            totalMs -= (t * msPerUnit);
            return t;
        }


        //var utcHour = (int)(decTotalMs / MsPerHour);
        //decTotalMs -= (utcHour * MsPerHour);

        //var utcMinute = (int)(decTotalMs / MsPerMinute);
        //decTotalMs -= (utcMinute * MsPerMinute);

        //var utcSecond = (int)(decTotalMs / MsPerSecond);
        //decTotalMs -= (utcSecond * MsPerSecond);

        //var utcMs = (int)decTotalMs;

        //public static TimeSpan ToUtc(utcTime dc)
        //{

        //}

        //public TimeSpan ToUtc()
        //{
        //    return ToUtc(this);
        //}
    }
}
