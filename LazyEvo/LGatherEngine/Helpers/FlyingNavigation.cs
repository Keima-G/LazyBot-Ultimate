namespace LazyEvo.LGatherEngine.Helpers
{
    using LazyEvo.Forms;
    using LazyEvo.LGatherEngine;
    using LazyLib;
    using LazyLib.Helpers;
    using LazyLib.Wow;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Threading;

    internal class FlyingNavigation
    {
        private readonly List<Location> _waypoints = new List<Location>();
        private bool _loopWaypoints;

        public FlyingNavigation(IEnumerable<Location> waypoints, bool loopWaypoints, FlyingWaypointsType type)
        {
            this._waypoints = new List<Location>(waypoints);
            this._loopWaypoints = loopWaypoints;
            this.CurrentFlyingWaypointsType = type;
            this.UseNearestWaypoint(-1);
        }

        public void FaceNextWaypoint()
        {
            this.NextPos.Face();
        }

        public void Pulse()
        {
            while (Main.chatcommand)
            {
                Thread.Sleep(100);
            }
            InterfaceHelper.CloseMainMenuFrame();
            if (!LazySettings.GroundGather && (Mount.IsMounted() && !LazyLib.Wow.ObjectManager.MyPlayer.IsFlying))
            {
                KeyHelper.PressKey("Space");
                while (true)
                {
                    if (LazyLib.Wow.ObjectManager.MyPlayer.IsFlying || !Mount.IsMounted())
                    {
                        KeyHelper.ReleaseKey("Space");
                        break;
                    }
                    Thread.Sleep(5);
                }
            }
            if (this.NextWaypointDistance <= 18.0)
            {
                this.SetNextWaypoint();
            }
            if (!this.NextPos.Equals(GatherEngine.Navigator.GetDestination))
            {
                GatherEngine.Navigator.SetDestination(this.NextPos);
            }
            GatherEngine.Navigator.Start();
        }

        public void Reset()
        {
            this._waypoints.Clear();
            this._loopWaypoints = false;
            this.CurrentWaypointIndex = 0;
        }

        public void SetNextWaypoint()
        {
            if (!this.IsLastWaypoints)
            {
                this.CurrentWaypointIndex++;
            }
            else if (this.IsLastWaypoints && this._loopWaypoints)
            {
                this.CurrentWaypointIndex = 0;
            }
            else
            {
                MoveHelper.ReleaseKeys();
            }
        }

        public void UseNearestWaypoint(int radius = -1)
        {
            if (radius == -1)
            {
                this.CurrentWaypointIndex = this.GetNearestWaypointIndex;
                this.SetNextWaypoint();
            }
            else
            {
                double distanceToSelf = this._waypoints[this.CurrentWaypointIndex].DistanceToSelf;
                int currentWaypointIndex = this.CurrentWaypointIndex;
                for (int i = this.CurrentWaypointIndex - radius; i < (this.CurrentWaypointIndex + radius); i++)
                {
                    if ((i <= 0) || (i >= this._waypoints.Count))
                    {
                        this.CurrentWaypointIndex = this.GetNearestWaypointIndex;
                        return;
                    }
                    if (this._waypoints[i].DistanceToSelf < distanceToSelf)
                    {
                        distanceToSelf = this._waypoints[i].DistanceToSelf;
                        currentWaypointIndex = i;
                    }
                }
                this.CurrentWaypointIndex = currentWaypointIndex;
            }
        }

        public FlyingWaypointsType CurrentFlyingWaypointsType { get; private set; }

        public int CurrentWaypointIndex { get; private set; }

        public int GetNearestWaypointIndex =>
            Location.GetClosestPositionInList(this._waypoints, LazyLib.Wow.ObjectManager.MyPlayer.Location);

        public bool IsLastWaypoints =>
            this.CurrentWaypointIndex >= (this._waypoints.Count - 1);

        public Location NextPos =>
            this._waypoints[this.CurrentWaypointIndex];

        public double NextWaypointDistance =>
            this.NextPos.DistanceToSelf2D;
    }
}

