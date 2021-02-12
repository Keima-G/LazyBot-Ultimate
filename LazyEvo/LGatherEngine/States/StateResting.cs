namespace LazyEvo.LGatherEngine.States
{
    using LazyEvo.LGatherEngine;
    using LazyLib.FSM;
    using LazyLib.LActivity;
    using LazyLib.Wow;
    using System;

    internal class StateResting : MainState
    {
        public override void DoWork()
        {
            Resting.Rest();
        }

        public override string Name() => 
            "Resting";

        public override int Priority =>
            Prio.Resting;

        public override bool NeedToRun =>
            LazyLib.Wow.ObjectManager.MyPlayer.IsAlive && (!LazyLib.Wow.ObjectManager.MyPlayer.IsGhost && (!LazyLib.Wow.ObjectManager.MyPlayer.IsFlying && (!LazyLib.Wow.ObjectManager.MyPlayer.IsMounted && Resting.NeedResting)));
    }
}

