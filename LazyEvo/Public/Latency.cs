namespace LazyEvo.Public
{
    using LazyLib;
    using System;
    using System.Threading;

    internal class Latency
    {
        public static void Sleep(int milliSeconds)
        {
            Thread.Sleep(milliSeconds);
            Thread.Sleep(LazySettings.Latency);
        }
    }
}

