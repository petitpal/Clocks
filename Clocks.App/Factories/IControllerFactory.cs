using Iot.Device.CharacterLcd;
using System;
using System.Device.Gpio;

namespace Clocks.App.Factories
{
    public interface IControllerFactory : IDisposable
    {
        GpioController GetButtonController();
        Lcd2004 GetLcd();
    }
}