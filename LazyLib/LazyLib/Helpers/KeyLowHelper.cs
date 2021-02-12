namespace LazyLib.Helpers
{
    using System;
    using System.Runtime.InteropServices;

    public class KeyLowHelper
    {
        private const uint PressKeyCode = 0x100;
        private const uint ReleaseKeyCode = 0x101;
        public const uint WmKeydown = 0x100;
        public const uint WmKeyup = 0x101;
        public const uint WmChar = 0x102;

        [DllImport("user32.dll")]
        public static extern bool PostMessage(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);
        public static void PressKey(MicrosoftVirtualKeys key)
        {
            PostMessage(Memory.WindowHandle, 0x100, (IntPtr) ((long) key), IntPtr.Zero);
        }

        public static void ReleaseKey(MicrosoftVirtualKeys key)
        {
            PostMessage(Memory.WindowHandle, 0x101, (IntPtr) ((long) key), IntPtr.Zero);
        }

        public static void SendEnter()
        {
            PostMessage(Memory.WindowHandle, 0x100, (IntPtr) 13, IntPtr.Zero);
            PostMessage(Memory.WindowHandle, 0x101, (IntPtr) 13, IntPtr.Zero);
        }

        [DllImport("user32.dll", CharSet=CharSet.Unicode)]
        public static extern int SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);
        public static void SendV()
        {
            PostMessage(Memory.WindowHandle, 0x100, (IntPtr) 0x56, IntPtr.Zero);
            PostMessage(Memory.WindowHandle, 0x101, (IntPtr) 0x56, IntPtr.Zero);
        }
    }
}

