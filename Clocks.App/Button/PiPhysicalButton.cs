using System;
using System.Device.Gpio;

namespace Clocks.App.Button
{
    public class PiPhysicalButton : PhysicalButtonBase
    {
        private readonly GpioController _gpc;

        public PiPhysicalButton(GpioController gpc)
        {
            _gpc = gpc;
        }

        public override void OnInitialize()
        {
            _gpc.OpenPin(_pin, PinMode.Input);
            _gpc.RegisterCallbackForPinValueChangedEvent(_pin, PinEventTypes.Rising, OnButtonPress);
            _gpc.RegisterCallbackForPinValueChangedEvent(_pin, PinEventTypes.Falling, OnButtonPress);
        }

        DateTime pressedDown = DateTime.MinValue;

        private void OnButtonPress(object sender, PinValueChangedEventArgs pinValueChangedEventArgs)
        {
            //todo: extend this to support double click, on hold, etc

            if (pinValueChangedEventArgs.ChangeType == PinEventTypes.Falling)
            {
                pressedDown = DateTime.Now;
            }
            else
            {
                var e = new ButtonHandlerEventArgs()
                {
                    pin = _pin
                };

                var _holdTime = (DateTime.Now - pressedDown);
                if (_holdTime.Seconds > 1)
                {
                    OnHeld(e);
                }
                else
                {
                    OnClick(e);
                }
            }
        }

        ~PiPhysicalButton()
        {
            if (_gpc.IsPinOpen(_pin))
                _gpc.ClosePin(_pin);
        }
    }
}
