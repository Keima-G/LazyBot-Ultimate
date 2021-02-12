namespace LazyLib.Helpers
{
    using System;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Threading;

    [Obfuscation(Feature="renaming", ApplyToMembers=true)]
    public class Ticker
    {
        private readonly double _countDowntime;
        private bool _varforceReady;

        public Ticker()
        {
            this._countDowntime = 0.0;
            this._varforceReady = false;
            this.Frequency = GetFrequency();
            this.Reset();
        }

        public Ticker(double countDowntime)
        {
            this._countDowntime = countDowntime * 10.0;
            this._varforceReady = false;
            this.Frequency = GetFrequency();
            this.Reset();
        }

        public void ForceReady()
        {
            this._varforceReady = true;
        }

        public static long GetFrequency()
        {
            long x = 0L;
            if (QueryPerformanceFrequency(ref x) == 0)
            {
                throw new NotSupportedException("Error while querying the performance counter frequency.");
            }
            return x;
        }

        public static long GetValue()
        {
            long x = 0L;
            if (QueryPerformanceCounter(ref x) == 0)
            {
                throw new NotSupportedException("Error while querying the high-resolution performance counter.");
            }
            return x;
        }

        public long Peek() => 
            (long) ((((double) (GetValue() - this.StartTime)) / ((double) this.Frequency)) * 10000.0);

        [DllImport("kernel32.dll")]
        private static extern int QueryPerformanceCounter(ref long x);
        [DllImport("kernel32.dll")]
        private static extern int QueryPerformanceFrequency(ref long x);
        public void Reset()
        {
            this.StartTime = GetValue();
            this._varforceReady = false;
        }

        public void Wait()
        {
            while (this.Peek() < this._countDowntime)
            {
                Thread.Sleep(5);
            }
        }

        public double TicksLeft =>
            (this.Peek() <= this._countDowntime) ? (this._countDowntime - this.Peek()) : (this._countDowntime - this.Peek());

        public bool IsReady
        {
            get
            {
                try
                {
                    bool flag;
                    if (!this._varforceReady)
                    {
                        if (this.Peek() <= this._countDowntime)
                        {
                            goto TR_0000;
                        }
                        else
                        {
                            flag = true;
                        }
                    }
                    else
                    {
                        flag = true;
                    }
                    return flag;
                }
                catch (Exception)
                {
                }
            TR_0000:
                return false;
            }
        }

        private long StartTime { get; set; }

        private long Frequency { get; set; }
    }
}

