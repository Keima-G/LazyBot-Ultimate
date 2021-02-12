namespace LazyLib.Dialogs.UnhandledExceptionDlg
{
    using System;
    using System.Reflection;

    [Obfuscation(Feature="renaming", ApplyToMembers=true)]
    public class SendExceptionClickEventArgs : EventArgs
    {
        public bool RestartApp;
        public bool SendExceptionDetails;
        public Exception UnhandledException;

        public SendExceptionClickEventArgs(bool sendDetailsArg, Exception exceptionArg, bool restartAppArg)
        {
            this.SendExceptionDetails = sendDetailsArg;
            this.UnhandledException = exceptionArg;
            this.RestartApp = restartAppArg;
        }
    }
}

