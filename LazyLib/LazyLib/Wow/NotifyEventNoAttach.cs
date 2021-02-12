namespace LazyLib.Wow
{
    using System;
    using System.Reflection;

    [Obfuscation(Feature="renaming", ApplyToMembers=true)]
    public class NotifyEventNoAttach : EventArgs
    {
        private readonly string _message;

        public NotifyEventNoAttach(string message)
        {
            this._message = message;
        }

        public string Message =>
            this._message;
    }
}

