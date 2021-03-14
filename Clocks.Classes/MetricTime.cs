using System;

namespace Clocks.Classes
{
    public struct MetricTime : ITime
    {
        private const int HoursPerDay = 10;
        private const int MinutesPerHour = 100;
        private const int SecondsPerMinute = 100;
        private const int MillisecondsPerSecond = 1000;

        public int Hours;
        public int Minutes;
        public int Seconds;
        public int Milliseconds;

        public MetricTime(int hours, int minutes, int seconds, int milliseconds)
        {
            Hours = hours;
            Minutes = minutes;
            Seconds = seconds;
            Milliseconds = milliseconds;
        }

        public ITime Empty() =>
            default;

        public string ToShortString() =>
            $"{Hours.ToString("00")}:{Minutes.ToString("00")}:{Seconds.ToString("00")}";

        public string ClockAbbreviation => "Dec";

        public ITime SetToNow() =>
            this.PopulateFromUtc(DateTime.Now.TimeOfDay);

        public ITime PopulateFromUtc(int utcHours,
                                     int utcMinutes,
                                     int utcSeconds,
                                     int utcMilliseconds) =>
            PopulateFromUtc(new TimeSpan(0, utcHours, utcMinutes, utcSeconds, utcMilliseconds));

        public ITime PopulateFromUtc(TimeSpan utcTime)
        {
            const double UtcMsToDecimalMs = 0.864;  // UTC MS in a day / Dec MS in a day
            const int MsPerHour = 10000000;
            const int MsPerMinute = 100000;
            const int MsPerSecond = 1000;

            var utcTotalMs = utcTime.TotalMilliseconds;
            var decTotalMs = utcTotalMs / UtcMsToDecimalMs;

            Hours = ExtractUnitByMs(ref decTotalMs, MsPerHour);
            Minutes = ExtractUnitByMs(ref decTotalMs, MsPerMinute);
            Seconds = ExtractUnitByMs(ref decTotalMs, MsPerSecond);
            Milliseconds = (int)decTotalMs;
            return this;
        }

        private static int ExtractUnitByMs(ref double totalMs, int msPerUnit)
        {
            var t = (int)(totalMs / msPerUnit);
            totalMs -= (t * msPerUnit);
            return t;
        }

        public bool AreEqual(ITime t1, ITime t2) =>
            (t1.ToShortString() == t2.ToShortString());
    }
}
