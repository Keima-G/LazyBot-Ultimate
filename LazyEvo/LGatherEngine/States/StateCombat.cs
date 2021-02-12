namespace LazyEvo.LGatherEngine.States
{
    using LazyEvo.LGatherEngine;
    using LazyEvo.LGatherEngine.Helpers;
    using LazyEvo.Public;
    using LazyLib.FSM;
    using LazyLib.Helpers;
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
            GatherEngine.Navigator.Stop();
            if (GatherSettings.SendKeyOnStartCombat)
            {
                KeyHelper.SendKey("CombatStart");
            }
            CombatHandler.StartCombat(this._unit);
            GatherEngine.UpdateStats(0, 1, 0);
            if (!LazyLib.Wow.ObjectManager.MyPlayer.IsDead)
            {
                CombatHandler.RunningAction();
            }
        }

        public override string Name() => 
            "Combat";

        public override int Priority =>
            Prio.Combat;

        public override bool NeedToRun
        {
            get
            {
                if (LazyLib.Wow.ObjectManager.MyPlayer.IsMounted)
                {
                    return false;
                }
                this._unit = null;
                this._unit = DefendAgainst();
                return !ReferenceEquals(this._unit, null);
            }
        }
    }
}

