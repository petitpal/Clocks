using System;

namespace Clocks.Classes
{
    public class StandardTime : ITime
    {
        public string ClockAbbreviation => "STD";

        private TimeSpan _time;

        public string ToShortString() =>
            $"{_time.Hours.ToString("00")}:{_time.Minutes.ToString("00")}:{_time.Seconds.ToString("00")}";

        public ITime PopulateFromUtc(int utcHours, int utcMinutes, int utcSeconds, int utcMilliseconds)
        {
            _time = new TimeSpan(0, utcHours, utcMinutes, utcSeconds, utcMilliseconds);
            return this;
        }

        public ITime PopulateFromUtc(TimeSpan utcTime)
        {
            _time = utcTime;
            return this;
        }

        public ITime SetToNow() =>
            this.PopulateFromUtc(DateTime.Now.TimeOfDay);

        public bool AreEqual(ITime t1, ITime t2) =>
            (t1.ToShortString() == t2.ToShortString());
    }
}