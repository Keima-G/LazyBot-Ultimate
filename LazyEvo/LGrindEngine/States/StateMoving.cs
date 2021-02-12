namespace LazyEvo.LGrindEngine.States
{
    using LazyEvo.LGrindEngine;
    using LazyEvo.LGrindEngine.Helpers;
    using LazyEvo.Public;
    using LazyLib.FSM;
    using LazyLib.Helpers;
    using LazyLib.Wow;
    using System;
    using System.Threading;

    internal class StateMoving : MainState
    {
        private static Ticker _jumpRandomly = new Ticker(4000.0);
        private readonly Random _random = new Random();

        public override void DoWork()
        {
            if (LazyLib.Wow.ObjectManager.MyPlayer.IsAlive)
            {
                CombatHandler.RunningAction();
            }
            if (GrindingSettings.Jump && _jumpRandomly.IsReady)
            {
                MoveHelper.Jump();
                _jumpRandomly = new Ticker((double) (this._random.Next(4, 8) * 0x3e8));
            }
            GrindingEngine.Navigation.Pulse();
            Thread.Sleep(10);
        }

        public override string Name() => 
            "Moving";

        public override int Priority =>
            Prio.Moving;

        public override bool NeedToRun =>
            true;
    }
}

