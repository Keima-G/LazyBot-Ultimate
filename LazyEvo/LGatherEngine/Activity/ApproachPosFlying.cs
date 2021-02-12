namespace LazyEvo.LGatherEngine.Activity
{
    using LazyEvo.LGatherEngine;
    using LazyLib.Helpers;
    using LazyLib.Wow;
    using System;
    using System.Threading;

    internal class ApproachPosFlying
    {
        public static bool Approach(Location location, int distance)
        {
            Ticker ticker = new Ticker(6000.0);
            double distanceToSelf = location.DistanceToSelf;
            MoveHelper.StopMove();
            location.Face();
            GatherEngine.Navigator.Start();
            GatherEngine.Navigator.SetDestination(location);
            while (location.DistanceToSelf2D > distance)
            {
                if (ticker.IsReady)
                {
                    GatherEngine.Navigator.Stop();
                    return false;
                }
                if (location.DistanceToSelf < distanceToSelf)
                {
                    distanceToSelf = location.DistanceToSelf;
                    ticker.Reset();
                }
                if (location.DistanceToSelf < distance)
                {
                    GatherEngine.Navigator.Stop();
                }
                Thread.Sleep(10);
            }
            GatherEngine.Navigator.Stop();
            Descent();
            return true;
        }

        private static void Descent()
        {
            Ticker ticker = new Ticker(6000.0);
            MoveHelper.Down(true);
            if (!LazyLib.Wow.ObjectManager.MyPlayer.InVashjir)
            {
                while (LazyLib.Wow.ObjectManager.MyPlayer.IsFlying && !ticker.IsReady)
                {
                    Thread.Sleep(10);
                }
            }
            else
            {
                Ticker ticker2 = new Ticker(1000.0);
                float num = 3f;
                Location location = LazyLib.Wow.ObjectManager.MyPlayer.Location;
                while (!ticker.IsReady && (num > 0.3))
                {
                    if (ticker2.IsReady)
                    {
                        num = MoveHelper.NegativeValue(location.Z - LazyLib.Wow.ObjectManager.MyPlayer.Location.Z);
                        ticker2.Reset();
                        location = LazyLib.Wow.ObjectManager.MyPlayer.Location;
                    }
                    Thread.Sleep(10);
                }
            }
            MoveHelper.Down(false);
        }
    }
}

