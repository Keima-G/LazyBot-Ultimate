namespace LazyEvo.LGrindEngine.States
{
    using LazyEvo.LGrindEngine;
    using LazyEvo.LGrindEngine.Activity;
    using LazyEvo.LGrindEngine.Helpers;
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
            GrindingEngine.Navigator.Stop();
            MoveHelper.MoveToLoc(this._npc.Location, 5.0);
            VendorManager.DoSell(this._npc);
            Logging.Write("[Vendor]Vendor done", new object[0]);
            GrindingEngine.Navigator.Stop();
            GrindingEngine.Navigation = new GrindingNavigation(GrindingEngine.CurrentProfile);
            ToTown.SetToTown(false);
        }

        public override string Name() => 
            "Vendor";

        public override bool NeedToRun
        {
            get
            {
                if (LazyLib.Wow.ObjectManager.MyPlayer.IsDead)
                {
                    return false;
                }
                if (!ToTown.ToTownEnabled)
                {
                    return false;
                }
                if (!LazySettings.ShouldVendor && !LazySettings.ShouldRepair)
                {
                    return false;
                }
                if (!ToTown.ToTownDoRepair && !ToTown.ToTownDoVendor)
                {
                    return false;
                }
                if (ToTown.Vendor == null)
                {
                    return false;
                }
                this._npc.BaseAddress = 0;
                foreach (PUnit unit in LazyLib.Wow.ObjectManager.GetUnits)
                {
                    if (ToTown.Vendor.EntryId != -2147483648)
                    {
                        if ((unit.Entry != ToTown.Vendor.EntryId) || ((unit.Location.DistanceToSelf2D >= 12.0) || GrindingBlackList.IsBlacklisted(unit.Name)))
                        {
                            continue;
                        }
                        this._npc.BaseAddress = unit.BaseAddress;
                    }
                    else
                    {
                        if (!unit.Name.Equals(ToTown.Vendor.Name) || ((unit.Location.DistanceToSelf2D >= 12.0) || GrindingBlackList.IsBlacklisted(unit.Name)))
                        {
                            continue;
                        }
                        this._npc.BaseAddress = unit.BaseAddress;
                    }
                    break;
                }
                return (this._npc.BaseAddress != 0);
            }
        }

        public override int Priority =>
            Prio.Vendor;
    }
}

