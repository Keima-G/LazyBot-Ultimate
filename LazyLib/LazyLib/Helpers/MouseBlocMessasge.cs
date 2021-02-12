namespace LazyLib.Helpers
{
    using System;
    using System.Reflection;

    [Obfuscation(Feature="renaming", ApplyToMembers=true)]
    public class MouseBlocMessasge : EventArgs
    {
        public bool Block;
    }
}

