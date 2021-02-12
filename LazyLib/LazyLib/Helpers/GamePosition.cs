namespace LazyLib.Helpers
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    internal class GamePosition
    {
        public static void Findpos(IntPtr hwnd)
        {
            RECT rect;
            GetWindowRect(Memory.WindowHandle, out rect);
            int top = rect.top;
            Width = (rect.left - rect.right) * -1;
            Height = (rect.top - rect.bottom) * -1;
            GetCenterX = rect.left + (Width / 2);
            GetCenterY = top + (Height / 2);
        }

        [DllImport("user32")]
        public static extern int GetWindowRect(IntPtr hwnd, out RECT lpRect);

        public static int GetCenterX { get; private set; }

        public static int GetCenterY { get; private set; }

        public static int Width { get; private set; }

        public static int Height { get; private set; }
    }
}

