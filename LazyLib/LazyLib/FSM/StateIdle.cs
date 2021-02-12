namespace LazyLib.FSM
{
    using System;

    internal class StateIdle : MainState
    {
        public override void DoWork()
        {
        }

        public override string Name() => 
            "StateIdle";

        public override int Priority =>
            -2147483648;

        public override bool NeedToRun =>
            true;
    }
}

