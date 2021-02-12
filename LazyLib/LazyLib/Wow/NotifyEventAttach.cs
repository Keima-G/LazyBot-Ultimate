namespace LazyLib.Wow
{
    using System;
    using System.Reflection;

    [Obfuscation(Feature="renaming", ApplyToMembers=true)]
    public class NotifyEventAttach : EventArgs
    {
        private readonly int _pid;

        public NotifyEventAttach(int pid)
        {
            this._pid = pid;
        }

        public int Pid =>
            this._pid;
    }
}

