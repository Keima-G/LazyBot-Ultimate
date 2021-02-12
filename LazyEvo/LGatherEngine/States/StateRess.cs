namespace LazyEvo.LGatherEngine.States
{
    using LazyEvo.LGatherEngine;
    using LazyEvo.LGatherEngine.Activity;
    using LazyEvo.LGatherEngine.Helpers;
    using LazyEvo.Public;
    using LazyLib;
    using LazyLib.FSM;
    using LazyLib.Helpers;
    using LazyLib.Wow;
    using System;
    using System.Collections.Generic;
    using System.Threading;

    internal class StateRess : MainState
    {
        private static void DescentToCorpse()
        {
            Ticker ticker = new Ticker(28000.0);
            Ticker ticker2 = new Ticker(500.0);
            float num = 3f;
            Location location = LazyLib.Wow.ObjectManager.MyPlayer.Location;
            MoveHelper.Down(true);
            while (!ticker.IsReady && (num > 0.3))
            {
                if (ticker2.IsReady)
                {
                    num = MoveHelper.NegativeValue(location.Z - LazyLib.Wow.ObjectManager.MyPlayer.Location.Z);
                    ticker2.Reset();
                    location = LazyLib.Wow.ObjectManager.MyPlayer.Location;
                }
                Thread.Sleep(10);
            }
            MoveHelper.Down(false);
        }

        private static void DoSpiritRess(PUnit vUnit)
        {
            Log("Going to accept ress sickness");
            Ress(vUnit);
            Thread.Sleep(0xfa0);
            if (LazyLib.Wow.ObjectManager.MyPlayer.IsDead || LazyLib.Wow.ObjectManager.MyPlayer.IsGhost)
            {
                Ress(vUnit);
            }
            if (LazyLib.Wow.ObjectManager.MyPlayer.IsDead || LazyLib.Wow.ObjectManager.MyPlayer.IsGhost)
            {
                Ress(vUnit);
            }
            if (LazyLib.Wow.ObjectManager.MyPlayer.IsDead || LazyLib.Wow.ObjectManager.MyPlayer.IsGhost)
            {
                LazyEvo.Public.LazyHelpers.StopAll("Could not ress.");
            }
        }

        public override void DoWork()
        {
            GatherEngine.Navigator.Stop();
            if (GatherSettings.StopOnDeath)
            {
                LazyEvo.Public.LazyHelpers.StopAll("We died");
            }
            Logging.Write("Going to ress", new object[0]);
            GatherEngine.UpdateStats(0, 0, 1);
            Location location = LazyLib.Wow.ObjectManager.MyPlayer.Location;
            Frame frameByName = InterfaceHelper.GetFrameByName("StaticPopup1Button1");
            try
            {
                frameByName.LeftClick();
                Thread.Sleep(0x7d0);
                frameByName.LeftClick();
                Thread.Sleep(0xbb8);
                Ticker ticker = new Ticker(8000.0);
                if (!LazyLib.Wow.ObjectManager.MyPlayer.IsGhost && !ticker.IsReady)
                {
                    frameByName.LeftClick();
                    Thread.Sleep(0x1388);
                }
            }
            catch
            {
            }
            if (!GatherSettings.FindCorpse || !Mount.IsMounted())
            {
                TrySpiritRess();
            }
            else
            {
                KeyHelper.PressKey("Space");
                Thread.Sleep(0x4e20);
                KeyHelper.ReleaseKey("Space");
                Ticker ticker2 = new Ticker(150000.0);
                GatherEngine.Navigator.SetDestination(new Location(location.X, location.Y, LazyLib.Wow.ObjectManager.MyPlayer.Location.Z));
                GatherEngine.Navigator.Start();
                while (LazyLib.Wow.ObjectManager.MyPlayer.IsGhost)
                {
                    if (location.DistanceToSelf2D < 20.0)
                    {
                        GatherEngine.Navigator.Stop();
                        Logging.Write("Looks like we found our corpse", new object[0]);
                        DescentToCorpse();
                        Thread.Sleep(0x7d0);
                        ApproachPosFlying.Approach(location, 3);
                        frameByName.LeftClick();
                        Ticker ticker3 = new Ticker(5000.0);
                        while (LazyLib.Wow.ObjectManager.MyPlayer.IsGhost && !ticker3.IsReady)
                        {
                            Thread.Sleep(10);
                        }
                    }
                    else if (!Mount.IsMounted() && LazyLib.Wow.ObjectManager.MyPlayer.IsGhost)
                    {
                        Logging.Write("We are not mounted, cannot find corpse", new object[0]);
                        ticker2.ForceReady();
                    }
                    else
                    {
                        if (!ticker2.IsReady || !LazyLib.Wow.ObjectManager.MyPlayer.IsGhost)
                        {
                            continue;
                        }
                        Logging.Write("We never found our corpse :(", new object[0]);
                        ticker2.ForceReady();
                    }
                    break;
                }
            }
            GatherEngine.Navigator.Stop();
            MoveHelper.ReleaseKeys();
            Thread.Sleep(0x5dc);
            if (!LazyLib.Wow.ObjectManager.MyPlayer.IsAlive)
            {
                if (!GatherSettings.FindCorpse)
                {
                    LazyEvo.Public.LazyHelpers.StopAll("Could not ress :(");
                }
                else
                {
                    Logging.Write("Could not find the corpse... trying to make it anyway", new object[0]);
                    InterfaceHelper.GetFrameByName("GhostFrame").LeftClick();
                    Thread.Sleep(0x1388);
                    TrySpiritRess();
                    if (!LazyLib.Wow.ObjectManager.MyPlayer.IsAlive)
                    {
                        LazyEvo.Public.LazyHelpers.StopAll("Could not ress :(");
                    }
                }
            }
            Logging.Write("Ress worked :)", new object[0]);
            GatherEngine.Navigator.Stop();
            if (!LazyLib.Wow.ObjectManager.MyPlayer.HasBuff(0x3a9f) || !GatherSettings.WaitForRessSick)
            {
                CombatHandler.RunningAction();
            }
            else
            {
                Logging.Write("Waiting for ress sickness", new object[0]);
                Mount.MountUp();
                MoveHelper.Jump(0x1770);
                if (!Mount.IsMounted())
                {
                    Mount.MountUp();
                    MoveHelper.Jump(0x1770);
                }
                while (LazyLib.Wow.ObjectManager.MyPlayer.HasBuff(0x3a9f))
                {
                    Thread.Sleep(0x1388);
                }
            }
        }

        private static PUnit FindSpiritHealer()
        {
            PUnit unit2;
            using (List<PUnit>.Enumerator enumerator = LazyLib.Wow.ObjectManager.GetUnits.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        PUnit current = enumerator.Current;
                        try
                        {
                            if (current.IsSpiritHealer)
                            {
                                unit2 = current;
                                break;
                            }
                        }
                        catch
                        {
                        }
                        continue;
                    }
                    return null;
                }
            }
            return unit2;
        }

        public override string Name() => 
            "Ress";

        private static void Ress(PUnit vUnit)
        {
            MoveHelper.MoveToUnit(vUnit, 2.0, false);
            Thread.Sleep(0x3e8);
            vUnit.InteractOrTarget(false);
            Thread.Sleep(0x3e8);
            if (!ReferenceEquals(LazyLib.Wow.ObjectManager.MyPlayer.Target, vUnit))
            {
                vUnit.InteractOrTarget(false);
            }
            Thread.Sleep(0x3e8);
            if (!ReferenceEquals(LazyLib.Wow.ObjectManager.MyPlayer.Target, vUnit))
            {
                vUnit.InteractOrTarget(false);
            }
            Thread.Sleep(0x3e8);
            if (!ReferenceEquals(LazyLib.Wow.ObjectManager.MyPlayer.Target, vUnit))
            {
                MoveHelper.ReleaseKeys();
                Thread.Sleep(100);
                KeyHelper.ChatboxSendText("/target " + vUnit.Name + " ;");
            }
            Thread.Sleep(0x3e8);
            vUnit.InteractWithTarget();
            Thread.Sleep(0x7d0);
            Frame frameByName = InterfaceHelper.GetFrameByName("GossipTitleButton1");
            if (frameByName.IsVisible)
            {
                Logging.Debug("GossipFrame at Spirit Healer? Seriously? Fire the Dev asap!", new object[0]);
                Thread.Sleep(0x3e8);
                Logging.Debug("Clicking GossipTitleButton1", new object[0]);
                frameByName.LeftClick();
                Thread.Sleep(0x5dc);
            }
            Thread.Sleep(500);
            Logging.Debug("GossipTitleButton1 Clicked", new object[0]);
            Thread.Sleep(0x7d0);
            Frame frame2 = InterfaceHelper.GetFrameByName("StaticPopup1Button1");
            Frame frame3 = InterfaceHelper.GetFrameByName("StaticPopup2Button1");
            frame2.LeftClick();
            Ticker ticker = new Ticker(5000.0);
            while (LazyLib.Wow.ObjectManager.MyPlayer.IsGhost && !ticker.IsReady)
            {
                if (frame2.IsVisible && frame2.IsVisible)
                {
                    Logging.Debug("Clicking StaticPopup1Button1", new object[0]);
                    Thread.Sleep(10);
                    frame2 = InterfaceHelper.GetFrameByName("StaticPopup1Button1");
                    frame2.LeftClick();
                    Thread.Sleep(0x5dc);
                }
                if (frame3.IsVisible && frame2.IsVisible)
                {
                    Logging.Debug("Hmm, did we die at the Spirit Healer?", new object[0]);
                    Thread.Sleep(10);
                    InterfaceHelper.GetFrameByName("StaticPopup2Button1").LeftClick();
                    Thread.Sleep(0x5dc);
                }
            }
        }

        private static void TrySpiritRess()
        {
            PUnit vUnit = FindSpiritHealer();
            if (vUnit == null)
            {
                Thread.Sleep(0x1f40);
                vUnit = FindSpiritHealer();
                if (vUnit == null)
                {
                    LazyEvo.Public.LazyHelpers.StopAll("Could not find spirit healer");
                }
            }
            ApproachPosFlying.Approach(vUnit.Location, 15);
            vUnit.Face();
            DoSpiritRess(vUnit);
        }

        public override int Priority =>
            Prio.Ress;

        public override bool NeedToRun =>
            LazyLib.Wow.ObjectManager.MyPlayer.IsDead;
    }
}

