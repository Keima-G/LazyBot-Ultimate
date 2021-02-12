namespace LazyLib.Helpers
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct KeyBinding
    {
        public string Command;
        public string Key;
    }
}

