namespace LazyEvo.Plugins.ExtraLazy
{
    using LazyLib.Helpers;
    using System;

    public class EventMessage
    {
        public static string readChat() => 
            Memory.ReadUtf8StringRelative(Convert.ToUInt32((uint) 0xa98068), 0x80);
    }
}

