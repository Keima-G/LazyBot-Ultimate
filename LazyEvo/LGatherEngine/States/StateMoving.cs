namespace LazyEvo.LGatherEngine.States
{
    using LazyEvo.LGatherEngine;
    using LazyEvo.LGatherEngine.Helpers;
    using LazyLib.FSM;
    using System;
    using System.Threading;

    internal class StateMoving : MainState
    {
        public override void DoWork()
        {
            GatherEngine.Navigation.Pulse();
            Thread.Sleep(400);
        }

        public override string Name() => 
            "Navigating";

        public override bool NeedToRun =>
            Mount.IsMounted();

        public override int Priority =>
            Prio.Moving;
    }
}

