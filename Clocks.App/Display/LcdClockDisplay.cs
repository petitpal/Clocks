using Iot.Device.CharacterLcd;

namespace Clocks.App.Display
{
    public class LcdClockDisplay : IClockDisplay
    {
        private readonly Lcd2004 _lcd;

        public LcdClockDisplay(Lcd2004 lcd)
        {
            _lcd = lcd;
        }

        public void Clear() =>
            _lcd.Clear();

        public void SetCursorPosition(int left, int top) =>
            _lcd.SetCursorPosition(left, top);

        public void Write(string message) =>
            _lcd.Write(message);
    }
}
