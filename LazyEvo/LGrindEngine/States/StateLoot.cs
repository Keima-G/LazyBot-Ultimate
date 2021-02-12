namespace LazyEvo.LGrindEngine.States
{
    using LazyEvo.LGrindEngine;
    using LazyEvo.LGrindEngine.Activity;
    using LazyEvo.LGrindEngine.Helpers;
    using LazyEvo.Public;
    using LazyLib.FSM;
    using LazyLib.Helpers;
    using LazyLib.Wow;
    using System;

    internal class StateLoot : MainState
    {
        private PUnit _unit;

        public override void DoWork()
        {
            GrindingEngine.Navigator.Stop();
            LootAndSkin.DoWork(this._unit);
            GrindingEngine.Navigation.UseNextNearestWaypoint();
        }

        private static PUnit FindMobToLoot()
        {
            PUnit unit = null;
            foreach (PUnit unit2 in LazyLib.Wow.ObjectManager.GetUnits)
            {
                if (unit2.IsLootable && ((unit2.DistanceToSelf < 30.0) && !PBlackList.IsBlacklisted(unit2)))
                {
                    if (unit == null)
                    {
                        unit = unit2;
                        continue;
                    }
                    if (unit.DistanceToSelf > unit2.DistanceToSelf)
                    {
                        unit = unit2;
                    }
                }
            }
            return unit;
        }

        public override string Name() => 
            "Loot";

        public override int Priority =>
            Prio.LootAround;

        public override bool NeedToRun
        {
            get
            {
                if (GrindingSettings.StopLootOnFull && (Inventory.FreeBagSlots == 0))
                {
                    return false;
                }
                if (LazyLib.Wow.ObjectManager.ShouldDefend)
                {
                    return false;
                }
                if (!GrindingSettings.Loot)
                {
                    return false;
                }
                this._unit = FindMobToLoot();
                return !ReferenceEquals(this._unit, null);
            }
        }
    }
}

