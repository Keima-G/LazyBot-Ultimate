namespace LazyEvo.LGrindEngine.Activity
{
    using LazyEvo.LGrindEngine;
    using LazyEvo.LGrindEngine.Helpers;
    using LazyEvo.Public;
    using LazyLib;
    using LazyLib.Helpers;
    using LazyLib.Wow;
    using System;
    using System.Threading;

    internal class LootAndSkin
    {
        private static readonly Ticker TimeOut = new Ticker(2000.0);

        public static void DoLootAfterCombat(PUnit unit)
        {
            if (GrindingSettings.Loot && !LazyLib.Wow.ObjectManager.ShouldDefend)
            {
                Thread.Sleep(500);
                if (!LazyLib.Wow.ObjectManager.MyPlayer.Target.IsValid && unit.IsLootable)
                {
                    KeyHelper.SendKey("TargetLastTarget");
                }
                Thread.Sleep(500);
                if (LazyLib.Wow.ObjectManager.MyPlayer.Target.IsValid)
                {
                    DoWork(LazyLib.Wow.ObjectManager.MyPlayer.Target);
                }
            }
        }

        public static void DoWork(PUnit unit)
        {
            MoveHelper.ReleaseKeys();
            if (unit.IsLootable)
            {
                Logging.Write("Looting: " + unit.Name, new object[0]);
                if (unit.DistanceToSelf > 5.0)
                {
                    MoveHelper.MoveToLoc(unit.Location, 4.0, false, true);
                }
                if (LazyLib.Wow.ObjectManager.ShouldDefend)
                {
                    Logging.Write("Skipping loot, we got into combat", new object[0]);
                }
                else
                {
                    Thread.Sleep(200);
                    if (LazyLib.Wow.ObjectManager.MyPlayer.HasLivePet)
                    {
                        Thread.Sleep(700);
                    }
                    if (!ReferenceEquals(LazyLib.Wow.ObjectManager.MyPlayer.Target, unit))
                    {
                        unit.Interact(false);
                    }
                    else
                    {
                        KeyHelper.SendKey("InteractTarget");
                    }
                    if (!LazyLib.Wow.ObjectManager.ShouldDefend)
                    {
                        TimeOut.Reset();
                        while (!LazyLib.Wow.ObjectManager.MyPlayer.LootWinOpen && !TimeOut.IsReady)
                        {
                            Thread.Sleep(100);
                        }
                        TimeOut.Reset();
                        if (!GrindingSettings.Skin && !GrindingSettings.WaitForLoot)
                        {
                            Thread.Sleep(200);
                        }
                        else
                        {
                            while (true)
                            {
                                if (!LazyLib.Wow.ObjectManager.MyPlayer.LootWinOpen || TimeOut.IsReady)
                                {
                                    Thread.Sleep(0x514);
                                    break;
                                }
                                Thread.Sleep(100);
                            }
                        }
                        GrindingEngine.UpdateStats(1, 0, 0);
                        PBlackList.Blacklist(unit, 300, false);
                        if (unit.IsSkinnable && GrindingSettings.Skin)
                        {
                            Logging.Write("Skinning", new object[0]);
                            KeyHelper.SendKey("TargetLastTarget");
                            Thread.Sleep(0x3e8);
                            if (!LazyLib.Wow.ObjectManager.MyPlayer.Target.IsValid)
                            {
                                unit.Interact(false);
                            }
                            else
                            {
                                KeyHelper.SendKey("InteractTarget");
                            }
                            TimeOut.Reset();
                            while (true)
                            {
                                if (LazyLib.Wow.ObjectManager.MyPlayer.LootWinOpen || TimeOut.IsReady)
                                {
                                    if (GrindingSettings.WaitForLoot)
                                    {
                                        Thread.Sleep(500);
                                    }
                                    GrindingEngine.UpdateStats(1, 0, 0);
                                    break;
                                }
                                Thread.Sleep(100);
                            }
                        }
                    }
                }
            }
        }
    }
}

