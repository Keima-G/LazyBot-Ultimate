namespace LazyLib.Helpers
{
    using LazyLib.Wow;
    using System;
    using System.Reflection;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential), Obfuscation(Feature="renaming", ApplyToMembers=true)]
    public struct ChatMsg
    {
        public string Channel;
        public string Msg;
        public string Player;
        public string chatGUID;
        public Constants.ChatType Type;
    }
}

