namespace LazyEvo.LGatherEngine.Helpers
{
    using LazyEvo.LGatherEngine;
    using LazyEvo.Public;
    using LazyLib;
    using LazyLib.Helpers;
    using LazyLib.Wow;
    using System;
    using System.Threading;

    internal class Mount
    {
        public static Location LastHarvest;
        private static readonly Ticker DismountTimer = new Ticker(100.0);
        private static readonly Ticker MountTimeOut = new Ticker(18000.0);
        private static readonly Ticker MountTimer = new Ticker(2400.0);

        public static void Dismount()
        {
            if (IsMounted() && DismountTimer.IsReady)
            {
                if (!LazySettings.GroundGather)
                {
                    InterfaceHelper.CloseMainMenuFrame();
                    KeyHelper.SendKey("FMount");
                }
                if (LazySettings.GroundGather)
                {
                    InterfaceHelper.CloseMainMenuFrame();
                    KeyHelper.SendKey("GMount");
                }
                DismountTimer.Reset();
            }
        }

        public static bool IsMounted() => 
            LazyLib.Wow.ObjectManager.MyPlayer.IsMounted || LazyLib.Wow.ObjectManager.MyPlayer.HasBuff(0xd785);

        public static bool MountUp()
        {
            if (!IsMounted())
            {
                GatherEngine.Navigator.Stop();
                MoveHelper.ReleaseKeys();
                if (LazyLib.Wow.ObjectManager.ShouldDefend)
                {
                    return false;
                }
                if (LazyLib.Wow.ObjectManager.MyPlayer.IsSwimming)
                {
                    WeGotWet();
                }
                MoveHelper.ReleaseKeys();
                Latency.Sleep(LazyLib.Wow.ObjectManager.MyPlayer.IsInCombat ? 0x3e8 : 500);
                if (!LazySettings.GroundGather)
                {
                    try
                    {
                        InterfaceHelper.CloseMainMenuFrame();
                        KeyHelper.SendKey("FMount");
                    }
                    catch
                    {
                        Logging.Debug("mainMenuFrame not open", new object[0]);
                    }
                }
                if (LazySettings.GroundGather)
                {
                    InterfaceHelper.CloseMainMenuFrame();
                    KeyHelper.SendKey("GMount");
                }
                MountTimer.Reset();
                while (!MountTimer.IsReady && !IsMounted())
                {
                    if (LazyLib.Wow.ObjectManager.ShouldDefend)
                    {
                        return false;
                    }
                    Thread.Sleep(10);
                }
                Latency.Sleep(200);
                int tickCount = Environment.TickCount;
                if (LazyLib.Wow.ObjectManager.ShouldDefend)
                {
                    return false;
                }
                if (!IsMounted())
                {
                    if (LazyLib.Wow.ObjectManager.ShouldDefend || LazyLib.Wow.ObjectManager.MyPlayer.IsDead)
                    {
                        return false;
                    }
                    TryUnstuck(tickCount);
                }
                if (Langs.MountCantMount(LazyLib.Wow.ObjectManager.MyPlayer.RedMessage))
                {
                    LazyEvo.Public.LazyHelpers.StopAll("Cannot mount inside");
                    HelperFunctions.ResetRedMessage();
                }
                if (LazyLib.Wow.ObjectManager.ShouldDefend || LazyLib.Wow.ObjectManager.MyPlayer.IsDead)
                {
                    return false;
                }
                if (!IsMounted())
                {
                    Latency.Sleep(0x9c4);
                    if (!IsMounted())
                    {
                        if (!LazyLib.Wow.ObjectManager.ShouldDefend && !LazyLib.Wow.ObjectManager.MyPlayer.IsDead)
                        {
                            LazyEvo.Public.LazyHelpers.StopAll("Could not mount");
                        }
                        return false;
                    }
                }
            }
            return true;
        }

        private static void TryUnstuck(int tickCount)
        {
            MountTimeOut.Reset();
            while (!IsMounted() && !MountTimeOut.IsReady)
            {
                if ((Environment.TickCount - tickCount) > 0xbb8)
                {
                    if (LazyLib.Wow.ObjectManager.ShouldDefend)
                    {
                        return;
                    }
                    MoveHelper.RotateRight(true);
                    while (true)
                    {
                        if ((Environment.TickCount - tickCount) >= 0xdac)
                        {
                            Thread.Sleep(300);
                            MoveHelper.StopMove();
                            MoveHelper.Forwards(true);
                            if (LazyLib.Wow.ObjectManager.ShouldDefend)
                            {
                                MoveHelper.StopMove();
                                return;
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
                if (!IsMounted())
                {
                    Thread.Sleep(500);
                    if (LazyLib.Wow.ObjectManager.MyPlayer.IsSwimming)
                    {
                        WeGotWet();
                    }
                    if (!LazySettings.GroundGather)
                    {
                        InterfaceHelper.CloseMainMenuFrame();
                        KeyHelper.SendKey("FMount");
                    }
                    if (LazySettings.GroundGather)
                    {
                        InterfaceHelper.CloseMainMenuFrame();
                        KeyHelper.SendKey("GMount");
                    }
                    MountTimer.Reset();
                    while (true)
                    {
                        if (MountTimer.IsReady || IsMounted())
                        {
                            Latency.Sleep(0);
                            break;
                        }
                        if (LazyLib.Wow.ObjectManager.ShouldDefend || LazyLib.Wow.ObjectManager.MyPlayer.IsDead)
                        {
                            return;
                        }
                        Thread.Sleep(100);
                    }
                }
            }
        }

        public static void WeGotWet()
        {
            Logging.Debug(LogType.Info, "Looks like we got our weekly bath early, lets get out of this wet stuff!", new object[0]);
            int num = 0;
            int num2 = 0;
            Logging.Debug(LogType.Info, "While loop start", new object[0]);
            while (LazyLib.Wow.ObjectManager.MyPlayer.IsSwimming)
            {
                object[] args = new object[] { num };
                Logging.Debug(LogType.Info, "Loop Counter = {0}", args);
                if (num > 3)
                {
                    switch (num2)
                    {
                        case 0:
                            Logging.Debug(LogType.Info, "Unable to surface, moving left a bit", new object[0]);
                            MoveHelper.StrafeLeft(true);
                            Thread.Sleep(0x3e8);
                            MoveHelper.StrafeLeft(false);
                            num = 0;
                            num2 = 1;
                            break;

                        case 1:
                            Logging.Debug(LogType.Info, "Unable to surface, moving right a bit", new object[0]);
                            MoveHelper.StrafeRight(true);
                            Thread.Sleep(0x3e8);
                            MoveHelper.StrafeRight(false);
                            num = 0;
                            num2 = 2;
                            break;

                        case 2:
                            Logging.Debug(LogType.Info, "Unable to surface, moving forward a bit", new object[0]);
                            MoveHelper.Forwards(true);
                            Thread.Sleep(0x3e8);
                            MoveHelper.Forwards(false);
                            num = 0;
                            num2 = 3;
                            break;

                        case 3:
                            Logging.Debug(LogType.Info, "Unable to surface, moving backward a bit", new object[0]);
                            MoveHelper.Backwards(true);
                            Thread.Sleep(0x3e8);
                            MoveHelper.Backwards(false);
                            num = 0;
                            num2 = 0;
                            break;

                        default:
                            break;
                    }
                }
                if (LazyLib.Wow.ObjectManager.MyPlayer.Class == "Death Knight")
                {
                    Logging.Write("Casting Path of Frost", new object[0]);
                    if (!LazyLib.Wow.ObjectManager.MyPlayer.HasWellKnownBuff("Path of Frost"))
                    {
                        LazyLib.Wow.ObjectManager.MyPlayer.TargetSelf();
                        Thread.Sleep(250);
                        KeyHelper.SendKey("Path of Frost");
                    }
                    Thread.Sleep(0x3e8);
                    MoveHelper.Jump(0x1388);
                    Thread.Sleep(0x3e8);
                }
                if (LazyLib.Wow.ObjectManager.MyPlayer.Class == "Shaman")
                {
                    Logging.Write("Casting Water Walking", new object[0]);
                    if (!LazyLib.Wow.ObjectManager.MyPlayer.HasWellKnownBuff("Water Walking"))
                    {
                        LazyLib.Wow.ObjectManager.MyPlayer.TargetSelf();
                        Thread.Sleep(250);
                        KeyHelper.SendKey("Water Walking");
                    }
                    Thread.Sleep(0x3e8);
                    MoveHelper.Jump(0x1388);
                    Thread.Sleep(0x3e8);
                }
                if (LazyLib.Wow.ObjectManager.MyPlayer.Class == "Priest")
                {
                    Logging.Write("Casting Levitate", new object[0]);
                    if (!LazyLib.Wow.ObjectManager.MyPlayer.HasWellKnownBuff("Levitate"))
                    {
                        LazyLib.Wow.ObjectManager.MyPlayer.TargetSelf();
                        Thread.Sleep(250);
                        KeyHelper.SendKey("Levitate");
                    }
                    Thread.Sleep(0x3e8);
                    MoveHelper.Jump(0x1388);
                    Thread.Sleep(0x3e8);
                }
                if ((LazyLib.Wow.ObjectManager.MyPlayer.Class != "Shaman") && ((LazyLib.Wow.ObjectManager.MyPlayer.Class != "Death Knight") && (LazyLib.Wow.ObjectManager.MyPlayer.Class != "Priest")))
                {
                    Logging.Write("Class has no water walking abilities, using Elixir of Water Walking if available", new object[0]);
                    if (!LazyLib.Wow.ObjectManager.MyPlayer.HasWellKnownBuff("Elixir of Water Walking"))
                    {
                        LazyLib.Wow.ObjectManager.MyPlayer.TargetSelf();
                        Thread.Sleep(250);
                        KeyHelper.SendKey("Elixir of Water Walking");
                    }
                    Logging.Write("Elixir of Water Walking", new object[0]);
                    Thread.Sleep(0x3e8);
                    MoveHelper.Jump(0x1388);
                    Thread.Sleep(0x3e8);
                }
                num++;
            }
            Logging.Debug(LogType.Info, "While loop stop", new object[0]);
            num = 0;
            num2 = 0;
        }
    }
}

