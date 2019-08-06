using System;

namespace Clocks.Classes
{
    public struct DecimalTime : ITime<int>
    {
        public int HoursPerDay => 10;
        public int MinutesPerHour => 100;
        public int SecondsPerMinute => 100;
        public int MillisecondsPerSecond => 1000;


        public int Hours { get; private set; }
        public int Minutes { get; private set; }
        public int Seconds { get; private set; }
        public int Milliseconds { get; private set; }


        public DecimalTime(int decHours,
                           int decMinutes,
                           int decSeconds,
                           int decMilliseconds)
        {
            Hours = decHours;
            Minutes = decMinutes;
            Seconds = decSeconds;
            Milliseconds = decMilliseconds;
        }

        public ITime<int> Empty() =>
            default;

        public bool Equals(ITime<int> dc1, ITime<int> dc2) =>
            dc1.Hours == dc2.Hours
            && dc1.Minutes == dc2.Minutes
            && dc1.Seconds == dc2.Seconds
            && dc1.Milliseconds == dc2.Milliseconds;

        public ITime<int> SetToNow() =>
            new DecimalTime().PopulateFromUtc(DateTime.Now.TimeOfDay);

        public ITime<int> PopulateFromUtc(int utcHours,
                                          int utcMinutes,
                                          int utcSeconds,
                                          int utcMilliseconds) =>
            PopulateFromUtc(new TimeSpan(0, utcHours, utcMinutes, utcSeconds, utcMilliseconds));

        public ITime<int> PopulateFromUtc(TimeSpan utcTime)
        {
            const double UtcMsToDecimalMs = 0.864;  // UTC MS in a day / Dec MS in a day
            const int MsPerHour = 10000000;
            const int MsPerMinute = 100000;
            const int MsPerSecond = 1000;

            var utcTotalMs = utcTime.TotalMilliseconds;
            var decTotalMs = utcTotalMs / UtcMsToDecimalMs;

            this.Hours = ExtractUnitByMs(ref decTotalMs, MsPerHour);
            this.Minutes = ExtractUnitByMs(ref decTotalMs, MsPerMinute);
            this.Seconds = ExtractUnitByMs(ref decTotalMs, MsPerSecond);
            this.Milliseconds = (int)decTotalMs;
            return this;
        }

        private static int ExtractUnitByMs(ref double totalMs, int msPerUnit)
        {
            var t = (int)(totalMs / msPerUnit);
            totalMs -= (t * msPerUnit);
            return t;
        }
    }
}
