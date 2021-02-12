namespace LazyEvo.LGrindEngine.Helpers
{
    using LazyEvo.LGrindEngine;
    using LazyEvo.Public;
    using LazyLib;
    using LazyLib.Helpers;
    using LazyLib.Wow;
    using System;
    using System.Threading;

    internal class Mount
    {
        private static readonly Ticker DismountTimer = new Ticker(100.0);

        public static void Dismount()
        {
            if (IsMounted() && DismountTimer.IsReady)
            {
                KeyHelper.SendKey("GMount");
                DismountTimer.Reset();
            }
        }

        public static bool IsMounted() => 
            LazyLib.Wow.ObjectManager.MyPlayer.IsMounted;

        public static bool MountUp()
        {
            if (LazyLib.Wow.ObjectManager.MyPlayer.Level < 20)
            {
                Logging.Debug(" We are below level 20, we do not have a mount", new object[0]);
                return true;
            }
            if (IsMounted())
            {
                return true;
            }
            if (LazyLib.Wow.ObjectManager.ShouldDefend)
            {
                return false;
            }
            GrindingEngine.Navigator.Stop();
            MoveHelper.ReleaseKeys();
            Thread.Sleep(0x3e8);
            KeyHelper.SendKey("GMount");
            int tickCount = Environment.TickCount;
            if (LazyLib.Wow.ObjectManager.ShouldDefend)
            {
                return false;
            }
            while (!IsMounted())
            {
                if ((Environment.TickCount - tickCount) > 0xbb8)
                {
                    if (!LazyLib.Wow.ObjectManager.ShouldDefend)
                    {
                        MoveHelper.RotateRight(true);
                        while (true)
                        {
                            if ((Environment.TickCount - tickCount) >= 0xdac)
                            {
                                MoveHelper.StopMove();
                                MoveHelper.Forwards(true);
                                if (LazyLib.Wow.ObjectManager.ShouldDefend)
                                {
                                    MoveHelper.StopMove();
                                    return false;
                                }
                                MoveHelper.Jump(0x3e8);
                                while (true)
                                {
                                    if ((Environment.TickCount - tickCount) >= 0x2328)
                                    {
                                        MoveHelper.StopMove();
                                        break;
                                    }
                                    Thread.Sleep(100);
                                }
                                break;
                            }
                            Thread.Sleep(100);
                        }
                    }
                    return false;
                }
                Thread.Sleep(100);
            }
            if (Langs.MountCantMount(LazyLib.Wow.ObjectManager.MyPlayer.RedMessage))
            {
                ResetRedMessage();
                LazyEvo.Public.LazyHelpers.StopAll("Cannot mount inside, fix the profile");
            }
            if (IsMounted())
            {
                return true;
            }
            Thread.Sleep(0x9c4);
            return (IsMounted() || (LazyLib.Wow.ObjectManager.MyPlayer.Level <= 0x13));
        }

        public static void ResetRedMessage()
        {
            Logging.Write("Resetting red message", new object[0]);
            KeyHelper.SendKey("F1");
            Thread.Sleep(100);
            KeyHelper.SendKey("Attack1");
        }
    }
}

