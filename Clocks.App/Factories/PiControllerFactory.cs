using Iot.Device.CharacterLcd;
using Iot.Device.Pcx857x;
using System;
using System.Device.Gpio;
using System.Device.I2c;

namespace Clocks.App.Factories
{
    public class PiControllerFactory : IControllerFactory
    {
        private I2cDevice _i2c = default;
        private Pcf8574 _lcdDriver = default;
        private GpioController _lcdGpc = default;
        private Lcd2004 _lcd = default;
        private GpioController _buttonGpc = default;
        private bool disposedValue = false;

        public GpioController GetButtonController()
        {
            if (_buttonGpc == default)
                _buttonGpc = new GpioController(PinNumberingScheme.Logical);
            return _buttonGpc;
        }

        public Lcd2004 GetLcd()
        {
            if (_lcd == default)
            {
                _i2c = I2cDevice.Create(new I2cConnectionSettings(1, 0x27));
                _lcdDriver = new Pcf8574(_i2c);
                _lcdGpc = new GpioController(PinNumberingScheme.Logical, _lcdDriver);
                _lcd = new Lcd2004(registerSelectPin: 0,
                                  enablePin: 2,
                                  dataPins: new int[] { 4, 5, 6, 7 },
                                      backlightPin: 3,
                                      backlightBrightness: 1.0f,
                                      readWritePin: 1,
                                      controller: _lcdGpc);
            }

            return _lcd;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _i2c.Dispose();
                    _lcdDriver.Dispose();
                    _lcdGpc.Dispose();
                    _lcd.Dispose();
                    _buttonGpc.Dispose();
                }
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
