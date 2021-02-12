namespace LazyEvo.LGatherEngine.States
{
    using LazyEvo.LGatherEngine;
    using LazyEvo.LGatherEngine.Helpers;
    using LazyEvo.Public;
    using LazyLib.FSM;
    using LazyLib.Helpers;
    using LazyLib.Wow;
    using System;

    internal class StateFullBags : MainState
    {
        public override void DoWork()
        {
            HelperFunctions.ResetRedMessage();
            if (!Mount.IsMounted())
            {
                Mount.MountUp();
                MoveHelper.Jump(0xbb8);
            }
            LazyEvo.Public.LazyHelpers.StopAll("Bags are full, stopping");
        }

        public override string Name() => 
            "Full bags";

        public override int Priority =>
            Prio.BagsFull;

        public override bool NeedToRun =>
            GatherSettings.StopOnFullBags && (Langs.BagsFull(LazyLib.Wow.ObjectManager.MyPlayer.RedMessage) && !LazyLib.Wow.ObjectManager.ShouldDefend);
    }
}

