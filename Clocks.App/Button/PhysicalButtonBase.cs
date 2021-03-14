using System;

namespace Clocks.App.Button
{
    public abstract class PhysicalButtonBase : IButtonHandler
    {
        public event EventHandler<ButtonHandlerEventArgs> Click;
        public event EventHandler<ButtonHandlerEventArgs> DoubleClick;
        public event EventHandler<ButtonHandlerEventArgs> Held;

        private bool _initialized = false;
        protected int _pin = default;

        public void Initialize(int pin)
        {
            _pin = pin;
            OnInitialize();
            _initialized = true;
        }

        public abstract void OnInitialize();

        protected virtual void OnClick(ButtonHandlerEventArgs e)
        {
            if (!_initialized) throw new InvalidOperationException($"Not initialized {_pin}");
            Click?.Invoke(this, e);
        }

        protected virtual void OnDoubleClick(ButtonHandlerEventArgs e)
        {
            if (!_initialized) throw new InvalidOperationException($"Not initialized {_pin}");
            DoubleClick?.Invoke(this, e);
        }

        protected virtual void OnHeld(ButtonHandlerEventArgs e)
        {
            if (!_initialized) throw new InvalidOperationException($"Not initialized {_pin}");
            Held?.Invoke(this, e);
        }
    }

    public class ButtonHandlerEventArgs
    {
        public int pin { get; set; }
    }
}
