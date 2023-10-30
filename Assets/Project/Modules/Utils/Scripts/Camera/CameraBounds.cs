namespace Utils
{
    internal struct CameraBounds
    {
        internal float Left;
        internal float Right;
        internal float Top;
        internal float Bottom;

        internal CameraBounds(float left, float right, float top, float bottom)
        {
            this.Left = left;
            this.Right = right;
            this.Top = top;
            this.Bottom = bottom;
        }
    }
}