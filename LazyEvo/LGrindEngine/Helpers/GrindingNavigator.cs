namespace LazyEvo.LGrindEngine.Helpers
{
    using LazyEvo.Public;
    using LazyLib.Helpers;
    using LazyLib.Wow;
    using System;
    using System.Threading;

    internal class GrindingNavigator
    {
        private static readonly Ticker StuckTimer = new Ticker(300000.0);
        private readonly Ticker _stuckTimer = new Ticker(3000.0);
        private Location _destination = new Location(0f, 0f, 0f);
        private Thread _navigatorThread;
        private double _stopDistance = 1.0;

        private void NavigatorLoop()
        {
            int num = 0;
            while (true)
            {
                double num2 = this._destination.DistanceToSelf2D;
                if (Stuck.IsStuck && this._stuckTimer.IsReady)
                {
                    Unstuck.TryUnstuck();
                    this._stuckTimer.Reset();
                    num++;
                }
                if (StuckTimer.IsReady)
                {
                    num = 0;
                }
                if (num > 6)
                {
                    LazyEvo.Public.LazyHelpers.StopAll("Stuck more than 6 times in 5 min");
                }
                if (num2 <= this._stopDistance)
                {
                    MoveHelper.ReleaseKeys();
                }
                else
                {
                    if (!this._destination.IsFacing(0.2f))
                    {
                        this._destination.Face();
                    }
                    KeyHelper.PressKey("Up");
                }
                Thread.Sleep(10);
            }
        }

        internal void SetDestination(Location pos)
        {
            InterfaceHelper.CloseMainMenuFrame();
            this._destination = pos;
        }

        internal void SetDestination(float x, float y, float z)
        {
            this._destination = new Location(x, y, z);
        }

        internal void SetStopDistance(double dis)
        {
            this._stopDistance = dis;
        }

        internal void Start()
        {
            if (!this.IsRunning)
            {
                this._navigatorThread = new Thread(new ThreadStart(this.NavigatorLoop));
                this._navigatorThread.Name = "NavigatorThread";
                this._navigatorThread.Start();
                this._navigatorThread.IsBackground = true;
            }
        }

        internal void Stop()
        {
            if (this.IsRunning)
            {
                try
                {
                    this._navigatorThread.Abort();
                    this._navigatorThread = null;
                }
                catch
                {
                }
            }
            MoveHelper.ReleaseKeys();
        }

        internal bool IsRunning =>
            (this._navigatorThread != null) && this._navigatorThread.IsAlive;

        internal Location GetDestination =>
            this._destination;
    }
}

