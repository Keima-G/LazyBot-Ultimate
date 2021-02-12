namespace LazyEvo.LGrindEngine.States
{
    using LazyEvo.LGrindEngine;
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
            !LazyLib.Wow.ObjectManager.MyPlayer.IsDead && (!LazyLib.Wow.ObjectManager.MyPlayer.IsGhost && (!LazyLib.Wow.ObjectManager.ShouldDefend ? Resting.NeedResting : false));
    }
}

