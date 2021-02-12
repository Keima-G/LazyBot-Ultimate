namespace LazyLib.FSM
{
    using LazyLib;
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    [Obfuscation(Feature="renaming", ApplyToMembers=true)]
    public abstract class MainState : IComparable<MainState>, IComparer<MainState>
    {
        protected MainState()
        {
        }

        public int Compare(MainState x, MainState y) => 
            -x.Priority.CompareTo(y.Priority);

        public int CompareTo(MainState other) => 
            -this.Priority.CompareTo(other.Priority);

        public static void Debug(string message)
        {
            Logging.Write(message, new object[0]);
        }

        public abstract void DoWork();
        public static void Log(string message)
        {
            Logging.Write(message, new object[0]);
        }

        public static void Log(string message, LogType type)
        {
            Logging.Write(type, message, new object[0]);
        }

        public abstract string Name();

        public abstract int Priority { get; }

        public abstract bool NeedToRun { get; }
    }
}

