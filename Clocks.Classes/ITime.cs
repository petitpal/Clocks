using System;

namespace Clocks.Classes
{
    public interface ITime
    {
        string ClockAbbreviation { get; }

        string ToShortString();

        void PopulateFromUtc(int utcHours, int utcMinutes, int utcSeconds, int utcMilliseconds);
        void PopulateFromUtc(TimeSpan utcTime);

        void SetToNow();

        bool AreEqual(ITime t1, ITime t2);
    }
}