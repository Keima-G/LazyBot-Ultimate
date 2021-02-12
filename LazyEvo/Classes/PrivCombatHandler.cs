namespace LazyEvo.Classes
{
    using LazyEvo.Forms;
    using LazyEvo.Public;
    using LazyLib;
    using LazyLib.Combat;
    using LazyLib.Helpers;
    using LazyLib.Wow;
    using System;
    using System.Threading;

    internal class PrivCombatHandler
    {
        private static Thread _combatLoopThread = new Thread(new ThreadStart(PrivCombatHandler.ExitCombat));
        private static Thread _combatThread = new Thread(new ThreadStart(PrivCombatHandler.ExitCombat));
        private static readonly PUnit Unit = new PUnit(0);
        private static CombatResult _combatResult;

        internal static void CombatDone()
        {
            Main.CombatEngine.CombatDone();
        }

        private static void CombatThread()
        {
            try
            {
                Ticker ticker;
                while (true)
                {
                    if (Main.chatcommand)
                    {
                        Thread.Sleep(100);
                        continue;
                    }
                    Logging.Write("Started combat engine", new object[0]);
                    if (LazyLib.Wow.ObjectManager.MyPlayer.IsMounted && !LazyLib.Wow.ObjectManager.MyPlayer.TravelForm)
                    {
                        KeyHelper.SendKey("GMount");
                    }
                    MoveHelper.ReleaseKeys();
                    if (DefendAgainst() != null)
                    {
                        while (true)
                        {
                            if (!Main.chatcommand)
                            {
                                Logging.Write("Got into combat with: " + Unit.Name, new object[0]);
                                Unit.TargetHostile();
                                Unit.Face();
                                break;
                            }
                            Thread.Sleep(100);
                        }
                        goto TR_000F;
                    }
                    else
                    {
                        Logging.Write("New Target: " + Unit.Name, new object[0]);
                        MoveHelper.MoveToUnit(Unit, 30.0);
                        if (!Unit.TargetHostile() && (LazyLib.Wow.ObjectManager.GetAttackers.Count == 0))
                        {
                            PPullBlackList.Blacklist(Unit, 800, true);
                        }
                        Unit.Face();
                        MoveHelper.ReleaseKeys();
                        PullResult result = Pull();
                        Logging.Write("Pull result: " + result, new object[0]);
                        if (!result.Equals(PullResult.CouldNotPull))
                        {
                            if (!PPullBlackList.IsBlacklisted(Unit))
                            {
                                goto TR_000F;
                            }
                        }
                        else
                        {
                            PPullBlackList.Blacklist(Unit, 800, true);
                        }
                    }
                    break;
                }
                return;
            TR_000E:
                while (true)
                {
                    if (!Unit.IsDead)
                    {
                        while (true)
                        {
                            if (Main.chatcommand)
                            {
                                Thread.Sleep(100);
                                continue;
                            }
                            Thread thread = new Thread(new ThreadStart(PrivCombatHandler.DoCombat)) {
                                IsBackground = true
                            };
                            _combatLoopThread = thread;
                            _combatLoopThread.Name = "DoCombat";
                            _combatLoopThread.SetApartmentState(ApartmentState.STA);
                            _combatLoopThread.Start();
                            while (true)
                            {
                                if (_combatLoopThread.IsAlive)
                                {
                                    Thread.Sleep(50);
                                    if (Langs.TrainingDummy(Unit.Name) || (!ticker.IsReady || (Unit.Health <= 0x63)))
                                    {
                                        continue;
                                    }
                                    Logging.Write("Combat took to long, bugged - blacklisting", new object[0]);
                                    _combatResult = CombatResult.Bugged;
                                    if (!PBlackList.IsBlacklisted(Unit))
                                    {
                                        PBlackList.Blacklist(Unit, 0x4b0, false);
                                    }
                                }
                                else
                                {
                                    continue;
                                }
                                break;
                            }
                            break;
                        }
                    }
                    break;
                }
                return;
            TR_000F:
                ticker = (LazyLib.Wow.ObjectManager.MyPlayer.Level <= 10) ? new Ticker(40000.0) : new Ticker(30000.0);
                goto TR_000E;
            }
            catch
            {
            }
        }

        private static PUnit DefendAgainst()
        {
            PUnit getClosestAttacker = null;
            if (!LazyLib.Wow.ObjectManager.ShouldDefend)
            {
                return null;
            }
            if (!PBlackList.IsBlacklisted(LazyLib.Wow.ObjectManager.GetClosestAttacker))
            {
                getClosestAttacker = LazyLib.Wow.ObjectManager.GetClosestAttacker;
            }
            else
            {
                foreach (PUnit unit2 in from un in LazyLib.Wow.ObjectManager.GetAttackers
                    where !PBlackList.IsBlacklisted(un)
                    select un)
                {
                    getClosestAttacker = unit2;
                }
            }
            return getClosestAttacker;
        }

        private static void DoCombat()
        {
            Main.CombatEngine.Combat(Unit);
        }

        private static void ExitCombat()
        {
            try
            {
                _combatLoopThread.Abort();
            }
            catch
            {
            }
            try
            {
                _combatThread.Abort();
            }
            catch
            {
            }
            MoveHelper.ReleaseKeys();
            if (_combatResult == CombatResult.OtherPlayerTag)
            {
                PBlackList.Blacklist(Unit, 0x4b0, false);
            }
            if ((_combatResult == CombatResult.Bugged) && LazyLib.Wow.ObjectManager.MyPlayer.HasLivePet)
            {
                KeyHelper.SendKey("PetFollow");
            }
            if (Unit.IsDead)
            {
                _combatResult = CombatResult.Success;
            }
            if (!LazyLib.Wow.ObjectManager.MyPlayer.IsAlive)
            {
                _combatResult = CombatResult.Died;
            }
            Logging.Write("Combat done, result : " + _combatResult, new object[0]);
            InvokeCombatStatusChanged(new GCombatEventArgs(CombatType.CombatDone, Unit));
            CombatDone();
            try
            {
                Stop();
            }
            catch
            {
            }
        }

        internal static void InvokeCombatStatusChanged(GCombatEventArgs e)
        {
            CombatHandler.InvokeCombatStatusChanged(e);
        }

        internal static void OnRess()
        {
            Main.CombatEngine.OnRess();
        }

        private static PullResult Pull() => 
            Main.CombatEngine.Pull(Unit);

        internal static void Rest()
        {
            Main.CombatEngine.Rest();
        }

        internal static void RunningAction()
        {
            Main.CombatEngine.RunningAction();
        }

        internal static void StartCombat(PUnit u)
        {
            if (LazyLib.Wow.ObjectManager.MyPlayer.IsDead)
            {
                Stop();
                return;
            }
            Unit.BaseAddress = u.BaseAddress;
            InvokeCombatStatusChanged(new GCombatEventArgs(CombatType.CombatStarted));
            Thread thread = new Thread(new ThreadStart(PrivCombatHandler.CombatThread)) {
                IsBackground = true
            };
            _combatThread = thread;
            _combatThread.Name = "CombatThread";
            _combatThread.Start();
            _combatResult = CombatResult.Unknown;
            goto TR_0014;
        TR_0001:
            ExitCombat();
            return;
        TR_0014:
            while (true)
            {
                if (_combatThread.IsAlive)
                {
                    try
                    {
                        if (!Unit.IsDead)
                        {
                            if ((!Unit.IsValid || PBlackList.IsBlacklisted(Unit)) && !Langs.TrainingDummy(LazyLib.Wow.ObjectManager.MyPlayer.Target.Name))
                            {
                                _combatResult = CombatResult.Bugged;
                            }
                            else if (LazyLib.Wow.ObjectManager.MyPlayer.IsDead)
                            {
                                _combatResult = CombatResult.Died;
                            }
                            else if (Unit.IsPet || Unit.IsTotem)
                            {
                                Logging.Write("We are attacking a totem or a pet... doh", new object[0]);
                                _combatResult = CombatResult.Bugged;
                            }
                            else
                            {
                                if (Langs.TrainingDummy(Unit.Name))
                                {
                                    break;
                                }
                                if (!Unit.IsTagged)
                                {
                                    break;
                                }
                                if (Unit.IsTaggedByMe)
                                {
                                    break;
                                }
                                if (Unit.IsTargetingMe || ReferenceEquals(Unit, LazyLib.Wow.ObjectManager.MyPlayer))
                                {
                                    break;
                                }
                                Logging.Write("Other player tag", new object[0]);
                                _combatResult = CombatResult.OtherPlayerTag;
                            }
                            goto TR_0001;
                        }
                        else
                        {
                            _combatResult = CombatResult.Success;
                            goto TR_0001;
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
                else
                {
                    goto TR_0001;
                }
                break;
            }
            Thread.Sleep(160);
            goto TR_0014;
        }

        internal static void Stop()
        {
            if (_combatLoopThread.IsAlive)
            {
                _combatLoopThread.Abort();
            }
            if (_combatThread.IsAlive)
            {
                _combatThread.Abort();
            }
            try
            {
                _combatLoopThread.Abort();
            }
            catch
            {
            }
            try
            {
                _combatThread.Abort();
            }
            catch
            {
            }
        }

        public static class Globals
        {
            public const string TW = "Toxic Waste";
            public const int moveto = 0;
        }
    }
}

