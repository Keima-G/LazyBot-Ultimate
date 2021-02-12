namespace LazyEvo.LGrindEngine.Helpers
{
    using LazyEvo.LGrindEngine;
    using LazyLib.Helpers;
    using LazyLib.Wow;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Threading;

    internal class GrindingNavigation
    {
        private readonly Ticker _standingStillToLong = new Ticker(3000.0);
        public GrindingWaypointType CurrentGrindingWaypointsType;
        private int _currentWaypointIndex;
        private List<Location> _path;

        public GrindingNavigation(LazyEvo.LGrindEngine.PathProfile pathProfile)
        {
            this.PathProfile = pathProfile;
            this.SpotToHit = pathProfile.GetSubProfile().GetNextHotSpot();
            this._currentWaypointIndex = 0;
            this._path = pathProfile.FindShortsPath(LazyLib.Wow.ObjectManager.MyPlayer.Location, this.SpotToHit);
            this.SetNextWaypoint();
        }

        public void FaceNextWaypoint()
        {
            this.NextPos.Face();
        }

        public void Pulse()
        {
            InterfaceHelper.CloseMainMenuFrame();
            if ((GrindingEngine.Navigator.GetDestination.DistanceToSelf2D < 1.1) && this._standingStillToLong.IsReady)
            {
                this.Reset();
            }
            if (LazyLib.Wow.ObjectManager.MyPlayer.IsMoving)
            {
                this._standingStillToLong.Reset();
            }
            if (this.IsLastWaypoints)
            {
                if (this.SpotToHit.DistanceToSelf2D < 10.0)
                {
                    this.SetNextWaypoint();
                }
                else if (!this.SpotToHit.Equals(GrindingEngine.Navigator.GetDestination))
                {
                    GrindingEngine.Navigator.SetDestination(this.SpotToHit);
                }
                GrindingEngine.Navigator.Start();
            }
            else
            {
                if (LazyLib.Wow.ObjectManager.MyPlayer.IsAlive)
                {
                    if (this.NextPos.NodeType == NodeType.GroundMount)
                    {
                        if (GrindingSettings.Mount && (!LazyLib.Wow.ObjectManager.MyPlayer.IsMounted && (LazyLib.Wow.ObjectManager.MyPlayer.Level > 0x13)))
                        {
                            Mount.MountUp();
                        }
                    }
                    else if ((this.NextPos.NodeType == NodeType.Normal) && LazyLib.Wow.ObjectManager.MyPlayer.IsMounted)
                    {
                        GrindingEngine.Navigator.Stop();
                        Mount.Dismount();
                        Thread.Sleep(500);
                    }
                }
                if (this.NextWaypointDistance < 3.0)
                {
                    this.SetNextWaypoint();
                }
                if (!this.NextPos.Equals(GrindingEngine.Navigator.GetDestination))
                {
                    GrindingEngine.Navigator.SetDestination(this.NextPos);
                }
                GrindingEngine.Navigator.Start();
            }
        }

        public void Reset()
        {
            this.SpotToHit = this.PathProfile.GetSubProfile().GetNextHotSpot();
            this._path = this.PathProfile.FindShortsPath(LazyLib.Wow.ObjectManager.MyPlayer.Location, this.SpotToHit);
            this._currentWaypointIndex = 0;
        }

        public void SetNewSpot(Location location)
        {
            this.SpotToHit = location;
            this._path = this.PathProfile.FindShortsPath(LazyLib.Wow.ObjectManager.MyPlayer.Location, location);
            this._currentWaypointIndex = 0;
            GrindingEngine.Navigator.SetDestination(location);
        }

        public void SetNextWaypoint()
        {
            if (!this.IsLastWaypoints)
            {
                this._currentWaypointIndex++;
            }
            else if (this.IsLastWaypoints)
            {
                this.Reset();
                GrindingEngine.Navigator.SetDestination(this.SpotToHit);
            }
        }

        public void UseNearestWaypoint()
        {
            this._currentWaypointIndex = this.GetNearestWaypointIndex;
        }

        public void UseNextNearestWaypoint()
        {
            this._path = this.PathProfile.FindShortsPath(LazyLib.Wow.ObjectManager.MyPlayer.Location, this.SpotToHit);
            this._currentWaypointIndex = 0;
        }

        public Location WayPoint(int index) => 
            this._path[index];

        private LazyEvo.LGrindEngine.PathProfile PathProfile { get; set; }

        public Location SpotToHit { get; private set; }

        public Location NextPos =>
            this._path[this._currentWaypointIndex];

        public Location GetNearestWaypoint =>
            this._path[this.GetNearestWaypointIndex];

        public bool IsLastWaypoints =>
            this._currentWaypointIndex >= (this._path.Count - 1);

        public double NextWaypointDistance =>
            this.NextPos.DistanceToSelf2D;

        public int GetNearestWaypointIndex =>
            Location.GetClosestPositionInList(this._path, LazyLib.Wow.ObjectManager.MyPlayer.Location);

        public int CurrentWaypointIndex =>
            this._currentWaypointIndex;
    }
}

