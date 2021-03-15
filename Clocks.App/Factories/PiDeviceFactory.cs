using Clocks.App.Button;
using Clocks.App.Display;

namespace Clocks.App.Factories
{
    public class PiDeviceFactory : IDeviceFactory
    {

        private readonly IControllerFactory _controllerFactory;

        public PiDeviceFactory(IControllerFactory controllerFactory)
        {
            _controllerFactory = controllerFactory;
        }

        public IPhysicalButton GetButton()
        {
            var gpc = _controllerFactory.GetButtonController();
            return new PiPhysicalButton(gpc);
        }

        public IClockDisplay GetDisplay()
        {
            var lcd = _controllerFactory.GetLcd();
            return new LcdClockDisplay(lcd);
        }
    }
}