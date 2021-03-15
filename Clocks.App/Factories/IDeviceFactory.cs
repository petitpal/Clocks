using Clocks.App.Button;
using Clocks.App.Display;

namespace Clocks.App.Factories
{
    public interface IDeviceFactory
    {
        IPhysicalButton GetButton();
        IClockDisplay GetDisplay();
    }

}