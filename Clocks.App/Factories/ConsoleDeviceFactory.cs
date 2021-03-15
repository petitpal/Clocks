using Clocks.App.Button;
using Clocks.App.Display;

namespace Clocks.App.Factories
{
    public class ConsoleDeviceFactory : IDeviceFactory
    {
        public IPhysicalButton GetButton() =>
            new ConsolePhysicalButton();

        public IClockDisplay GetDisplay() =>
                new ConsoleClockDisplay();
    }
}