using System;

namespace Clocks.Classes
{
    public class StandardTime : ITime
    {
        public string ClockAbbreviation => "s";

        private TimeSpan _time;

        public string ToShortString() =>
            $"{_time.Hours.ToString("00")}:{_time.Minutes.ToString("00")}:{_time.Seconds.ToString("00")}";

        public void PopulateFromUtc(int utcHours, int utcMinutes, int utcSeconds, int utcMilliseconds) =>
            _time = new TimeSpan(0, utcHours, utcMinutes, utcSeconds, utcMilliseconds);

        public void PopulateFromUtc(TimeSpan utcTime) =>
            _time = utcTime;

        public void SetToNow() =>
            PopulateFromUtc(DateTime.Now.TimeOfDay);

        public bool AreEqual(ITime t1, ITime t2) =>
            (t1.ToShortString() == t2.ToShortString());
    }
}