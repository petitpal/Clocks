using System;
using System.Device.Gpio;

namespace Clocks.App.Button
{
    public interface IButtonHandler
    {
        event EventHandler<ButtonHandlerEventArgs> Click;
        event EventHandler<ButtonHandlerEventArgs> DoubleClick;
        event EventHandler<ButtonHandlerEventArgs> Held;
        void Initialize(int pin);
    }
}