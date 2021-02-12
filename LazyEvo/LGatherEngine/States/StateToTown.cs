namespace LazyEvo.LGatherEngine.States
{
    using LazyEvo.LGatherEngine;
    using LazyEvo.LGatherEngine.Activity;
    using LazyLib.FSM;
    using LazyLib.Wow;
    using System;

    internal class StateToTown : MainState
    {
        public override void DoWork()
        {
        }

        public override string Name() => 
            "ToTown";

        public override bool NeedToRun
        {
            get
            {
                if (!LazyLib.Wow.ObjectManager.MyPlayer.IsDead && ((GatherEngine.CurrentProfile.WaypointsToTown.Count != 0) && ToTown.ToTownEnabled))
                {
                    ToTown.Pulse();
                }
                return false;
            }
        }

        public override int Priority =>
            Prio.ToTown;
    }
}

