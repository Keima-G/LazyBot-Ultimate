namespace LazyEvo.LGrindEngine.Helpers
{
    using LazyLib;
    using LazyLib.Helpers;
    using System;
    using System.Threading;

    internal class Unstuck
    {
        private static int _lastStuckTickcount;
        private static bool _lastStuckDirection = true;

        internal static void TryUnstuck()
        {
            Logging.Write(LogType.Warning, "Stuck", new object[0]);
            Thread.Sleep(0x7d0);
            MoveHelper.ReleaseKeys();
            if ((_lastStuckTickcount + 0x1388) < Environment.TickCount)
            {
                Logging.Debug("Jump", new object[0]);
                MoveHelper.Forwards(true);
                MoveHelper.Jump();
                MoveHelper.Forwards(false);
                Thread.Sleep(0x5dc);
            }
            else
            {
                Logging.Debug("Lets unstuck", new object[0]);
                MoveHelper.ReleaseKeys();
                if (_lastStuckDirection)
                {
                    MoveHelper.RotateLeft(true);
                }
                else
                {
                    MoveHelper.RotateRight(true);
                }
                _lastStuckDirection = !_lastStuckDirection;
                Thread.Sleep(500);
                MoveHelper.ReleaseKeys();
                MoveHelper.Forwards(true);
                Thread.Sleep(0x3e8);
                MoveHelper.Forwards(false);
            }
            _lastStuckTickcount = Environment.TickCount;
        }
    }
}

