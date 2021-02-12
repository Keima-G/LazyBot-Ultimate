namespace LazyLib.Helpers
{
    using System;
    using System.Reflection;

    [Obfuscation(Feature="renaming", ApplyToMembers=true)]
    public class MouseMoveMessasge : EventArgs
    {
        public int X;
        public int Y;
    }
}

