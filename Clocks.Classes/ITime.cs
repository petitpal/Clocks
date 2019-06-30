using System;

namespace Clocks.Classes
{
    public interface ITime<timeUnitType>
    {
        timeUnitType HoursPerDay { get; }
        timeUnitType MinutesPerHour { get; }
        timeUnitType SecondsPerMinute { get; }
        timeUnitType MillisecondsPerSecond { get; }

        timeUnitType Hours { get; }
        timeUnitType Minutes { get; }
        timeUnitType Seconds { get; }
        timeUnitType Milliseconds { get; }

        ITime<timeUnitType> PopulateFromUtc(int utcHours, int utcMinutes, int utcSeconds, int utcMilliseconds);
        ITime<timeUnitType> PopulateFromUtc(TimeSpan utcTime);
        
        ITime<timeUnitType> SetToNow();
        bool Equals(ITime<timeUnitType> dc1, ITime<timeUnitType> dc2);
    }
}
