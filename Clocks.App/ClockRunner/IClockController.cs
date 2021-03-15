using Clocks.Classes;

namespace Clocks.App.ClockRunner
{
    public interface IClockController
    {
        ITime Clock1 { get; set; }
        ITime Clock2 { get; set; }
        void RunClock();
    }
}
