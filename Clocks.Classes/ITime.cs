using System;

namespace Clocks.Classes
{
    public interface ITime
    {
        string ClockAbbreviation { get; }

        string ToShortString();

        ITime PopulateFromUtc(int utcHours, int utcMinutes, int utcSeconds, int utcMilliseconds);
        ITime PopulateFromUtc(TimeSpan utcTime);

        ITime SetToNow();

        bool AreEqual(ITime t1, ITime t2);
    }
}