using System;

namespace Clocks.Classes
{
    public class BinaryTime : ITime
    {
        private int _bits = default;
        private double _divisor = default;

        public BinaryTime() : this(12) { }

        public BinaryTime(int bits)
        {
            const int utcSecondsPerDay = 86400;

            _bits = bits;
            _divisor = utcSecondsPerDay / (int)Math.Pow(2, _bits);  // 2^bits = number of binary seconds per day
        }

        private string binaryTime = default;
        public string ClockAbbreviation => "b";

        public void PopulateFromUtc(int utcHours, int utcMinutes, int utcSeconds, int utcMilliseconds) =>
            PopulateFromUtc(new TimeSpan(0, utcHours, utcMinutes, utcSeconds, utcMilliseconds));

        public void PopulateFromUtc(TimeSpan utcTime)
        {
            var binarySeconds = (int)(utcTime.TotalSeconds / _divisor);
            binaryTime = Convert.ToString(binarySeconds, 2);
        }

        public void SetToNow() =>
            PopulateFromUtc(DateTime.Now.TimeOfDay);

        public string ToShortString() =>
            binaryTime;
        public bool AreEqual(ITime t1, ITime t2) =>
            (t1.ToShortString() == t2.ToShortString());

    }
}