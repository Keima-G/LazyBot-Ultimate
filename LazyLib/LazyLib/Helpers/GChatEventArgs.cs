namespace LazyLib.Helpers
{
    using System;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    [Obfuscation(Feature="renaming", ApplyToMembers=true)]
    public class GChatEventArgs : EventArgs
    {
        public ChatMsg Msg { get; set; }
    }
}

