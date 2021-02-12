namespace LazyEvo.LGrindEngine.Activity
{
    using LazyEvo.LGrindEngine;
    using LazyEvo.LGrindEngine.Helpers;
    using LazyLib;
    using LazyLib.Helpers;
    using LazyLib.Wow;
    using System;
    using System.Threading;

    internal class Resurrect
    {
        internal static bool ReachedEndGhostWaypoint;
        internal static int NearestWaypointIndexFromCorpse = -1;
        internal static Location CorpsePosition = new Location(0f, 0f, 0f);

        internal static void Pulse()
        {
            if ((CorpsePosition.X == 0f) && ((CorpsePosition.Y == 0f) && (CorpsePosition.Z == 0f)))
            {
                GrindingEngine.Navigator.Stop();
                MoveHelper.ReleaseKeys();
                CorpsePosition = LazyLib.Wow.ObjectManager.MyPlayer.Location;
                GrindingEngine.UpdateStats(0, 0, 1);
            }
            while (!LazyLib.Wow.ObjectManager.MyPlayer.IsGhost)
            {
                Thread.Sleep(0x3e8);
                Frame frameByName = InterfaceHelper.GetFrameByName("StaticPopup1Button1");
                if ((frameByName != null) && frameByName.IsVisible)
                {
                    frameByName.LeftClick();
                    Thread.Sleep(0x7d0);
                }
                Thread.Sleep(100);
            }
            if (!ReferenceEquals(GrindingEngine.Navigation.SpotToHit, CorpsePosition))
            {
                GrindingEngine.Navigation.SetNewSpot(CorpsePosition);
            }
            GrindingEngine.Navigation.Pulse();
            if (!GrindingEngine.Navigation.IsLastWaypoints && (CorpsePosition.DistanceToSelf2D >= 20.0))
            {
                Thread.Sleep(10);
            }
            else
            {
                Logging.Write("Move to our corpse", new object[0]);
                GrindingEngine.Navigator.Stop();
                MoveHelper.ReleaseKeys();
                MoveHelper.MoveToLoc(CorpsePosition, 3.0);
                Logging.Write("Lets ress", new object[0]);
                Frame frameByName = InterfaceHelper.GetFrameByName("StaticPopup1Button1");
                Ticker ticker = new Ticker(5000.0);
                bool flag = false;
                while (LazyLib.Wow.ObjectManager.MyPlayer.IsGhost)
                {
                    if (((!flag || ticker.IsReady) && (frameByName != null)) && frameByName.IsVisible)
                    {
                        frameByName.LeftClick();
                        flag = true;
                        ticker.Reset();
                    }
                    Thread.Sleep(0x3e8);
                }
                Logging.Write("Ress worked", new object[0]);
                Reset();
                GrindingEngine.Navigator.Stop();
                GrindingEngine.Navigation = new GrindingNavigation(GrindingEngine.CurrentProfile);
            }
        }

        internal static void Reset()
        {
            ReachedEndGhostWaypoint = false;
            NearestWaypointIndexFromCorpse = -1;
            CorpsePosition = new Location(0f, 0f, 0f);
        }
    }
}

