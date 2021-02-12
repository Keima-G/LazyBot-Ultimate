namespace LazyEvo.LGatherEngine.Helpers
{
    using LazyEvo.Public;
    using LazyLib.Helpers;
    using LazyLib.Wow;
    using System;
    using System.Threading;

    internal class FlyingNavigator
    {
        private static Location _destination = new Location(0f, 0f, 0f);
        private static Thread _navigatorThread;
        private static double _stopDistance = 2.0;
        private static readonly Ticker StuckTimer = new Ticker(300000.0);

        private void NavigatorLoop()
        {
            int num = 0;
            while (true)
            {
                double num2 = (_destination.Z != 0f) ? _destination.DistanceToSelf : _destination.DistanceToSelf2D;
                if (num2 <= _stopDistance)
                {
                    KeyHelper.PressKey("Up");
                    MoveHelper.ReleaseKeys();
                    KeyHelper.ReleaseKey("Up");
                }
                else
                {
                    if (Stuck.IsStuck)
                    {
                        Unstuck.TryUnstuck(num < 2);
                        MoveHelper.ReleaseKeys();
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
                    if (_destination.DistanceToSelf2D > 60.0)
                    {
                        HelperFunctions.Move3D(_destination, 30);
                    }
                    else if (_destination.DistanceToSelf2D > 30.0)
                    {
                        HelperFunctions.Move3D(_destination, 20);
                    }
                    else if (_destination.DistanceToSelf2D > 20.0)
                    {
                        HelperFunctions.Move3D(_destination, 8);
                    }
                    else
                    {
                        HelperFunctions.Move3D(_destination, 6);
                    }
                    KeyHelper.PressKey("Up");
                }
                Thread.Sleep(100);
            }
        }

        internal void SetDestination(Location pos)
        {
            _destination = pos;
        }

        internal void SetDestination(float x, float y, float z)
        {
            _destination = new Location(x, y, z);
        }

        internal void SetStopDistance(double dis)
        {
            _stopDistance = dis;
        }

        internal void Start()
        {
            try
            {
                if (!this.IsRunning)
                {
                    _navigatorThread = new Thread(new ThreadStart(this.NavigatorLoop));
                    _navigatorThread.Name = "NavigatorThread";
                    _navigatorThread.Start();
                    _navigatorThread.IsBackground = true;
                }
            }
            catch (Exception)
            {
                _navigatorThread = null;
            }
        }

        public static bool StillMoving()
        {
            Ticker ticker = new Ticker(850.0);
            Location location = LazyLib.Wow.ObjectManager.MyPlayer.Location;
            Location pos = location;
            while ((location.DistanceFrom(pos) < 0.15) && !ticker.IsReady)
            {
                Thread.Sleep(10);
                pos = LazyLib.Wow.ObjectManager.MyPlayer.Location;
            }
            return (location.DistanceFrom(pos) > 0.15);
        }

        internal void Stop()
        {
            try
            {
                if (this.IsRunning)
                {
                    _navigatorThread.Abort();
                    _navigatorThread = null;
                }
            }
            catch (Exception)
            {
                _navigatorThread = null;
            }
            MoveHelper.ReleaseKeys();
        }

        internal bool IsRunning =>
            (_navigatorThread != null) && _navigatorThread.IsAlive;

        internal Location GetDestination =>
            _destination;
    }
}

