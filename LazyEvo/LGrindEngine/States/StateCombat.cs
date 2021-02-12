namespace LazyEvo.LGrindEngine.States
{
    using LazyEvo.LGrindEngine;
    using LazyEvo.Public;
    using LazyLib.FSM;
    using LazyLib.Wow;
    using System;

    internal class StateCombat : MainState
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
            GrindingEngine.Navigator.Stop();
            CombatHandler.StartCombat(this._unit);
            GrindingEngine.UpdateStats(0, 1, 0);
            GrindingEngine.Navigation.UseNextNearestWaypoint();
        }

        public override string Name() => 
            "Pull";

        public override int Priority =>
            Prio.Combat;

        public override bool NeedToRun
        {
            get
            {
                this._unit = null;
                this._unit = DefendAgainst();
                return !ReferenceEquals(this._unit, null);
            }
        }
    }
}

