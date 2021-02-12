namespace LazyEvo.LGrindEngine.States
{
    using LazyEvo.LGrindEngine;
    using LazyEvo.LGrindEngine.Activity;
    using LazyLib.FSM;
    using LazyLib.Wow;
    using System;

    internal class StateResurrect : MainState
    {
        public override void DoWork()
        {
            Resurrect.Pulse();
        }

        public override string Name() => 
            "Resurrect";

        public override int Priority =>
            Prio.Resurrect;

        public override bool NeedToRun =>
            (LazyLib.Wow.ObjectManager.MyPlayer.IsDead || LazyLib.Wow.ObjectManager.MyPlayer.IsGhost) ? LazyLib.Wow.ObjectManager.MyPlayer.IsValid : false;
    }
}

