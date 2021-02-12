namespace LazyEvo.LGatherEngine.Helpers
{
    using LazyLib;
    using LazyLib.Helpers;
    using System;
    using System.Threading;

    internal class Unstuck
    {
        private static readonly Random Ran = new Random();

        private static void MoveRandom()
        {
            int num = Ran.Next(4);
            if ((num == 0) || (num == 1))
            {
                MoveHelper.Forwards(true);
            }
            if (num == 1)
            {
                MoveHelper.StrafeRight(true);
            }
            if ((num == 2) || (num == 3))
            {
                MoveHelper.Backwards(true);
            }
            if (num == 3)
            {
                MoveHelper.StrafeLeft(true);
            }
        }

        internal static void TryUnstuck(bool smallUnstuck)
        {
            Logging.Write(LogType.Warning, "Stuck", new object[0]);
            MoveRandom();
            Thread.Sleep(0x7d0);
            MoveHelper.ReleaseKeys();
            if (!smallUnstuck)
            {
                if (new Random().Next(1, 3) != 2)
                {
                    MoveHelper.Jump(new Random().Next(0x7d0, 0xfa0));
                }
                else
                {
                    MoveHelper.Down(new Random().Next(0x3e8, 0x7d0));
                }
                MoveRandom();
                Thread.Sleep(0x5dc);
                MoveHelper.ReleaseKeys();
                Ticker ticker = new Ticker((double) Convert.ToInt32((double) (new Random().Next(50, 200) * 3.1415926535897931)));
                MoveHelper.RotateRight(true);
                while (true)
                {
                    if (ticker.IsReady)
                    {
                        MoveHelper.RotateRight(false);
                        MoveHelper.Forwards(true);
                        Thread.Sleep(new Random().Next(0x7d0, 0xfa0));
                        MoveHelper.Forwards(false);
                        break;
                    }
                    Thread.Sleep(10);
                }
            }
            MoveRandom();
            Thread.Sleep(0x7d0);
            MoveHelper.ReleaseKeys();
            Logging.Write(LogType.Warning, "Done", new object[0]);
        }
    }
}

