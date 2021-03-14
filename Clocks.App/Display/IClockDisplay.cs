namespace Clocks.App.Display
{
    public interface IClockDisplay
    {
        void Clear();
        void SetCursorPosition(int left, int top);
        void Write(string message);
    }
}