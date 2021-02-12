namespace LazyEvo.LGatherEngine.Activity
{
    using LazyEvo.LGatherEngine;
    using LazyEvo.LGatherEngine.Helpers;
    using LazyLib;
    using LazyLib.Wow;
    using System;
    using System.Linq;

    internal class ToTown
    {
        internal static bool ToTownEnabled;
        internal static bool ToTownDoMail;
        internal static bool ToTownDoRepair;
        internal static bool ToTownDoVendor;
        private static int _toTownNearestWaypointIndex = -1;
        private static bool _reversed;
        internal static bool FollowingWaypoints;

        internal static void Pulse()
        {
            if (_toTownNearestWaypointIndex == -1)
            {
                _toTownNearestWaypointIndex = Location.GetClosestPositionInList(GatherEngine.CurrentProfile.WaypointsNormal, GatherEngine.CurrentProfile.WaypointsToTown[0]);
            }
            if ((_toTownNearestWaypointIndex != -1) && ((GatherEngine.Navigation.CurrentFlyingWaypointsType == FlyingWaypointsType.Normal) && (GatherEngine.Navigation.CurrentWaypointIndex == _toTownNearestWaypointIndex)))
            {
                GatherEngine.Navigator.Stop();
                GatherEngine.Navigation.Reset();
                GatherEngine.Navigation = new FlyingNavigation(GatherEngine.CurrentProfile.WaypointsToTown, false, FlyingWaypointsType.ToTown);
                Logging.Write("Following ToTown waypoints", new object[0]);
                FollowingWaypoints = true;
            }
            if ((GatherEngine.Navigation.CurrentFlyingWaypointsType == FlyingWaypointsType.ToTown) && (GatherEngine.Navigation.IsLastWaypoints && ((GatherEngine.Navigation.NextWaypointDistance <= 12.0) && !_reversed)))
            {
                _reversed = true;
                GatherEngine.Navigator.Stop();
                GatherEngine.Navigation.Reset();
                GatherEngine.Navigation = new FlyingNavigation((from p in GatherEngine.CurrentProfile.WaypointsToTown
                    orderby p.DistanceToSelf
                    select p).ToList<Location>(), false, FlyingWaypointsType.ToTown);
                Logging.Write("Following ToTown waypoints back", new object[0]);
            }
            if ((GatherEngine.Navigation.CurrentFlyingWaypointsType == FlyingWaypointsType.ToTown) && (GatherEngine.Navigation.IsLastWaypoints && ((GatherEngine.Navigation.NextWaypointDistance <= 12.0) && _reversed)))
            {
                FollowingWaypoints = false;
                Logging.Write("ToTown done, following normal waypoints", new object[0]);
                GatherEngine.Navigation.Reset();
                GatherEngine.Navigation = new FlyingNavigation(GatherEngine.CurrentProfile.WaypointsNormal, true, FlyingWaypointsType.Normal);
                GatherEngine.Navigation.UseNearestWaypoint(-1);
                SetToTown(false);
            }
        }

        internal static void SetToTown(bool enable)
        {
            if (!enable)
            {
                ToTownEnabled = false;
                _toTownNearestWaypointIndex = -1;
                _reversed = false;
            }
            else
            {
                ToTownEnabled = true;
                ToTownDoRepair = true;
                ToTownDoVendor = true;
                ToTownDoMail = LazySettings.ShouldMail;
                _toTownNearestWaypointIndex = -1;
                _reversed = false;
            }
        }
    }
}

