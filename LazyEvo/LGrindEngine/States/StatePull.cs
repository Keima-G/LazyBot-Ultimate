namespace LazyEvo.LGrindEngine.States
{
    using LazyEvo.LGrindEngine;
    using LazyEvo.LGrindEngine.Activity;
    using LazyEvo.LGrindEngine.Helpers;
    using LazyEvo.Public;
    using LazyLib;
    using LazyLib.FSM;
    using LazyLib.Wow;
    using System;

    internal class StatePull : MainState
    {
        private PUnit _unit;

        private static PUnit DefendAgainst()
        {
            PUnit getClosestAttacker = null;
            if (LazyLib.Wow.ObjectManager.ShouldDefend)
            {
                if (!PBlackList.IsBlacklisted(LazyLib.Wow.ObjectManager.GetClosestAttacker))
                {
                    getClosestAttacker = LazyLib.Wow.ObjectManager.GetClosestAttacker;
                }
                else
                {
                    foreach (PUnit unit2 in from un in LazyLib.Wow.ObjectManager.GetAttackers
                        where !PBlackList.IsBlacklisted(un)
                        select un)
                    {
                        getClosestAttacker = unit2;
                    }
                }
            }
            return getClosestAttacker;
        }

        public override void DoWork()
        {
            if (LazyLib.Wow.ObjectManager.ShouldDefend)
            {
                Logging.Write("Not pulling, we are in combat", new object[0]);
            }
            else
            {
                GrindingEngine.Navigator.Stop();
                if (LazyLib.Wow.ObjectManager.ShouldDefend)
                {
                    Logging.Write("Not pulling, we are in combat", new object[0]);
                }
                else
                {
                    CombatHandler.StartCombat(this._unit);
                    GrindingEngine.UpdateStats(0, 1, 0);
                    GrindingEngine.Navigation.UseNextNearestWaypoint();
                }
            }
        }

        public override string Name() => 
            "Pull";

        public override int Priority =>
            Prio.Targetting;

        public override bool NeedToRun
        {
            get
            {
                if (ToTown.ToTownEnabled)
                {
                    return false;
                }
                if (LazyLib.Wow.ObjectManager.ShouldDefend)
                {
                    return false;
                }
                this._unit = null;
                this._unit = DefendAgainst();
                if (this._unit != null)
                {
                    return true;
                }
                foreach (PUnit unit in PullController.ValidUnits)
                {
                    if (unit.DistanceToSelf < GrindingSettings.ApproachRange)
                    {
                        this._unit = unit;
                        break;
                    }
                }
                return !ReferenceEquals(this._unit, null);
            }
        }
    }
}

