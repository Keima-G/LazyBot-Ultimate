namespace LazyEvo.LGatherEngine.States
{
    using LazyEvo.LGatherEngine;
    using LazyEvo.LGatherEngine.Activity;
    using LazyEvo.LGatherEngine.Helpers;
    using LazyLib;
    using LazyLib.FSM;
    using LazyLib.Helpers;
    using LazyLib.Helpers.Vendor;
    using LazyLib.Wow;
    using System;

    internal class StateVendor : MainState
    {
        private const int SearchDistance = 12;
        private readonly PUnit _npc = new PUnit(0);

        public override void DoWork()
        {
            GatherEngine.Navigator.Stop();
            if (ApproachPosFlying.Approach(this._npc.Location, 12))
            {
                MoveHelper.MoveToLoc(this._npc.Location, 5.0);
                VendorManager.DoSell(this._npc);
            }
            GatherBlackList.Blacklist(this._npc, 200, true);
            Logging.Write("[Vendor]Vendor done", new object[0]);
        }

        public override string Name() => 
            "Vendor";

        public override bool NeedToRun
        {
            get
            {
                if (LazyLib.Wow.ObjectManager.MyPlayer.IsDead || (GatherEngine.CurrentProfile.WaypointsToTown.Count == 0))
                {
                    return false;
                }
                if (!ToTown.ToTownEnabled)
                {
                    return false;
                }
                if ((!LazySettings.ShouldVendor && !LazySettings.ShouldRepair) || string.IsNullOrEmpty(GatherEngine.CurrentProfile.VendorName))
                {
                    return false;
                }
                if (!ToTown.ToTownDoRepair && !ToTown.ToTownDoVendor)
                {
                    return false;
                }
                if (!ToTown.FollowingWaypoints)
                {
                    return false;
                }
                this._npc.BaseAddress = 0;
                foreach (PUnit unit in LazyLib.Wow.ObjectManager.GetUnits)
                {
                    if (unit.Name.Equals(GatherEngine.CurrentProfile.VendorName) && ((unit.Location.DistanceToSelf2D < 12.0) && !GatherBlackList.IsBlacklisted(unit.GUID)))
                    {
                        this._npc.BaseAddress = unit.BaseAddress;
                        break;
                    }
                }
                return (this._npc.BaseAddress != 0);
            }
        }

        public override int Priority =>
            Prio.Vendor;
    }
}

