namespace LazyEvo.LGatherEngine.Activity
{
    using LazyEvo.Forms;
    using LazyEvo.LGatherEngine;
    using LazyEvo.LGatherEngine.Helpers;
    using LazyEvo.LGatherEngine.States;
    using LazyEvo.Public;
    using LazyLib;
    using LazyLib.Helpers;
    using LazyLib.Wow;
    using System;
    using System.Linq;
    using System.Threading;

    internal class Gather
    {
        private static readonly Ticker TimeOut = new Ticker(4500.0);
        private static readonly Ticker StopASec = new Ticker(6000.0);
        private static Ticker _reLure;
        private static readonly Ticker Face = new Ticker(150.0);
        private static readonly Ticker ToLongRun = new Ticker(60000.0);
        private static double _oldDis;
        private static double _distanceX;
        private static double _distanceY;

        private static bool ApprochNode(PGameObject harvest)
        {
            PGameObject obj2 = new PGameObject(harvest.BaseAddress);
            int num = Convert.ToInt32(GatherSettings.ApproachModifier);
            Location pos = new Location(obj2.X, obj2.Y, obj2.Z + num);
            if (LazySettings.GroundGather && (harvest.Location.DistanceToSelf > LazySettings.GroundGatherDistance))
            {
                return false;
            }
            if (!LazyLib.Wow.ObjectManager.MyPlayer.IsFlying)
            {
                pos = new Location(obj2.X, obj2.Y, obj2.Z);
            }
            Ticker ticker = new Ticker(8000.0);
            double distanceToSelf = pos.DistanceToSelf;
            bool flag = false;
            StopASec.Reset();
            while (pos.DistanceToSelf2D > 10.0)
            {
                GatherEngine.Navigator.SetDestination(pos);
                if (PlayerToClose(20, harvest))
                {
                    ToldAboutNode.TellAbout("Player to close", harvest);
                    return false;
                }
                if (ticker.IsReady)
                {
                    if (GatherSettings.AutoBlacklist)
                    {
                        Logging.Write(LogType.Warning, "Blacklisting node for-ever", new object[0]);
                        GatherBlackList.AddBadNode(harvest.Location);
                    }
                    GatherEngine.Navigator.Stop();
                    ToldAboutNode.TellAbout("node blacklisted", harvest);
                    return false;
                }
                if (StopASec.IsReady)
                {
                    Logging.Debug("Check spin", new object[0]);
                    GatherEngine.Navigator.Stop();
                    MoveHelper.ReleaseKeys();
                    harvest.Location.Face();
                    StopASec.Reset();
                    GatherEngine.Navigator.Start();
                }
                if (GatherBlackList.IsBlacklisted(harvest))
                {
                    ToldAboutNode.TellAbout("node blacklisted", harvest);
                    return false;
                }
                if (pos.DistanceToSelf < distanceToSelf)
                {
                    distanceToSelf = pos.DistanceToSelf;
                    ticker.Reset();
                }
                if (pos.DistanceToSelf > 2.0)
                {
                    GatherEngine.Navigator.Start();
                }
                else
                {
                    GatherEngine.Navigator.Stop();
                }
                if (Stuck.IsStuck)
                {
                    Unstuck.TryUnstuck(false);
                }
                if (!Mount.IsMounted())
                {
                    Logging.Write("We got dismounted, abort", new object[0]);
                    return false;
                }
                if (!LazySettings.GroundGather && (!flag && ((pos.DistanceToSelf2D > 20.0) && (!LazyLib.Wow.ObjectManager.MyPlayer.IsFlying && LazyLib.Wow.ObjectManager.MyPlayer.IsMounted))))
                {
                    Logging.Debug("Running on the ground, lets jump", new object[0]);
                    GatherEngine.Navigator.Stop();
                    MoveHelper.Jump(0x44c);
                    GatherEngine.Navigator.Start();
                    flag = true;
                }
                Thread.Sleep(150);
            }
            GatherEngine.Navigator.Stop();
            return (obj2.Location.DistanceToSelf2D < 10.0);
        }

        private static PGameObject Bobber() => 
            Enumerable.FirstOrDefault<PGameObject>(LazyLib.Wow.ObjectManager.GetGameObject, pGameObject => pGameObject.DisplayId == 0x29c);

        internal static bool CheckDruidFight(PGameObject gameObject)
        {
            if (!LazyLib.Wow.ObjectManager.ShouldDefend || !LazyLib.Wow.ObjectManager.MyPlayer.IsInFlightForm)
            {
                return false;
            }
            if (!GatherSettings.DruidAvoidCombat)
            {
                Mount.Dismount();
                return false;
            }
            Logging.Write("Druid - avoiding combat - blacklisting node", new object[0]);
            GatherBlackList.Blacklist(gameObject, 300, false);
            return true;
        }

        internal static bool CheckFight(PGameObject gameObject) => 
            !CheckDruidFight(gameObject) ? (LazyLib.Wow.ObjectManager.ShouldDefend && !LazyLib.Wow.ObjectManager.MyPlayer.IsInFlightForm) : true;

        private static bool CheckMobs(PGameObject harvest)
        {
            int num = (from unit in LazyLib.Wow.ObjectManager.GetUnits
                where (unit.Location.DistanceFrom(harvest.Location) < 25.0) && (unit.Reaction == LazyLib.Wow.Reaction.Hostile)
                select unit).Count<PUnit>();
            if ((num > 0) && HasRessSickness())
            {
                ToldAboutNode.TellAbout("there are mobs close to the node and we have ress sickness", harvest);
                GatherBlackList.Blacklist(harvest, 120, true);
                return false;
            }
            if (GatherSettings.AvoidElites && ((from unit in LazyLib.Wow.ObjectManager.GetUnits
                where (unit.Location.DistanceFrom(harvest.Location) < 30.0) && (unit.IsElite && (unit.Reaction == LazyLib.Wow.Reaction.Hostile))
                select unit).Count<PUnit>() != 0))
            {
                Logging.Write("Elite at node, not landing", new object[0]);
                GatherBlackList.Blacklist(harvest, 120, true);
                return false;
            }
            if (Convert.ToInt32(GatherSettings.MaxUnits) >= num)
            {
                return true;
            }
            Logging.Write("To many units at node.", new object[0]);
            GatherBlackList.Blacklist(harvest, 120, true);
            return false;
        }

        internal static void DescentToNode(PGameObject nodeToHarvest)
        {
            while (Main.chatcommand)
            {
                Thread.Sleep(100);
            }
            Ticker ticker = new Ticker(6000.0);
            if (LazyLib.Wow.ObjectManager.MyPlayer.InVashjir)
            {
                MoveHelper.Down(true);
                DescentToNodeVashir(nodeToHarvest);
                MoveHelper.Down(false);
            }
            else if (LazyLib.Wow.ObjectManager.MyPlayer.IsFlying)
            {
                MoveHelper.Down(true);
                while (LazyLib.Wow.ObjectManager.MyPlayer.IsFlying && !ticker.IsReady)
                {
                    Thread.Sleep(10);
                }
                MoveHelper.Down(false);
            }
        }

        internal static void DescentToNodeVashir(PGameObject nodeToHarvest)
        {
            Ticker ticker = new Ticker(20000.0);
            Ticker ticker2 = new Ticker(500.0);
            float num = 3f;
            Location location = LazyLib.Wow.ObjectManager.MyPlayer.Location;
            float num2 = MoveHelper.NegativeValue(nodeToHarvest.Location.Z - LazyLib.Wow.ObjectManager.MyPlayer.Location.Z);
            while ((num2 > 2f) && (!ticker.IsReady && (num > 0.3)))
            {
                if (GatherBlackList.IsBlacklisted(nodeToHarvest))
                {
                    return;
                }
                num2 = MoveHelper.NegativeValue(LazyLib.Wow.ObjectManager.MyPlayer.Location.Z - nodeToHarvest.Location.Z);
                if (ticker2.IsReady)
                {
                    num = MoveHelper.NegativeValue(location.Z - LazyLib.Wow.ObjectManager.MyPlayer.Location.Z);
                    ticker2.Reset();
                    location = LazyLib.Wow.ObjectManager.MyPlayer.Location;
                }
                Thread.Sleep(10);
            }
        }

        internal static void DescentToSchool(PGameObject nodeToHarvest)
        {
            Ticker ticker = new Ticker(20000.0);
            Ticker ticker2 = new Ticker(500.0);
            float num = 3f;
            Location location = LazyLib.Wow.ObjectManager.MyPlayer.Location;
            float num2 = MoveHelper.NegativeValue(nodeToHarvest.Location.Z - LazyLib.Wow.ObjectManager.MyPlayer.Location.Z);
            while (LazyLib.Wow.ObjectManager.MyPlayer.IsFlying && ((num2 > 2f) && (!ticker.IsReady && (num > 0.3))))
            {
                if (GatherBlackList.IsBlacklisted(nodeToHarvest))
                {
                    return;
                }
                num2 = MoveHelper.NegativeValue(LazyLib.Wow.ObjectManager.MyPlayer.Location.Z - nodeToHarvest.Location.Z);
                if (ticker2.IsReady)
                {
                    num = MoveHelper.NegativeValue(location.Z - LazyLib.Wow.ObjectManager.MyPlayer.Location.Z);
                    ticker2.Reset();
                    location = LazyLib.Wow.ObjectManager.MyPlayer.Location;
                }
                Thread.Sleep(10);
            }
        }

        internal static bool DismountAndHarvest(PGameObject harvest, Ticker timeOut)
        {
            while (Main.chatcommand)
            {
                Thread.Sleep(100);
            }
            if (!LazySettings.BackgroundMode && !harvest.Location.IsFacing())
            {
                harvest.Location.Face();
            }
            if (Mount.IsMounted() && !LazyLib.Wow.ObjectManager.MyPlayer.IsInFlightForm)
            {
                Mount.Dismount();
                timeOut.Reset();
                while (true)
                {
                    if (!LazyLib.Wow.ObjectManager.MyPlayer.IsMoving || timeOut.IsReady)
                    {
                        Thread.Sleep(500);
                        break;
                    }
                    Thread.Sleep(100);
                }
            }
            Logging.Debug("Going to do harvest now", new object[0]);
            harvest.Interact(true);
            Latency.Sleep((LazyLib.Wow.ObjectManager.MyPlayer.UnitRace != "Tauren") ? 750 : 500);
            if (!LazyLib.Wow.ObjectManager.MyPlayer.IsCasting && (LazyLib.Wow.ObjectManager.MyPlayer.UnitRace != "Tauren"))
            {
                harvest.Interact(true);
                Latency.Sleep(750);
            }
            if (CheckFight(harvest))
            {
                ToldAboutNode.TellAbout("we are in combat", harvest);
                return false;
            }
            timeOut.Reset();
            while (LazyLib.Wow.ObjectManager.MyPlayer.IsCasting && !timeOut.IsReady)
            {
                if (CheckFight(harvest))
                {
                    ToldAboutNode.TellAbout("we are in combat", harvest);
                    return false;
                }
                Thread.Sleep(100);
            }
            if (CheckFight(harvest))
            {
                ToldAboutNode.TellAbout("we are in combat", harvest);
                return false;
            }
            if (!Langs.SkillToLow(LazyLib.Wow.ObjectManager.MyPlayer.RedMessage))
            {
                return true;
            }
            Logging.Write("Skill to low", new object[0]);
            HelperFunctions.ResetRedMessage();
            if (FindNode.IsMine(harvest) || FindNode.IsHerb(harvest))
            {
                SkillToLow.Blacklist(harvest.Name, 240);
            }
            return false;
        }

        internal static bool GatherFishNode(PGameObject node)
        {
            Func<PObject, bool> func = null;
            if (_reLure == null)
            {
                _reLure = new Ticker(600000.0);
                _reLure.ForceReady();
            }
            GatherEngine.Navigator.Stop();
            StateCombat combat = new StateCombat();
            int closestPositionInList = Location.GetClosestPositionInList(GatherEngine.CurrentProfile.WaypointsNormal, node.Location);
            if (!ApproachPosFlying.Approach(GatherEngine.CurrentProfile.WaypointsNormal[closestPositionInList], 5))
            {
                return false;
            }
            node.Location.Face();
            if (!CheckMobs(node))
            {
                return false;
            }
            if (GatherBlackList.IsBlacklisted(node))
            {
                ToldAboutNode.TellAbout("is blacklisted", node);
                return false;
            }
            DescentToSchool(node);
            Mount.Dismount();
            Ticker ticker = new Ticker((GatherSettings.MaxTimeAtSchool * 60.0) * 1000.0);
            Ticker ticker2 = new Ticker(8000.0);
            while (true)
            {
                if (!node.IsValid)
                {
                    break;
                }
                while (true)
                {
                    if (combat.NeedToRun)
                    {
                        combat.DoWork();
                        ticker.Reset();
                        continue;
                    }
                    if (ticker2.IsReady)
                    {
                        if (func == null)
                        {
                            func = u => u.BaseAddress == node.BaseAddress;
                        }
                        if (Enumerable.FirstOrDefault<PObject>(LazyLib.Wow.ObjectManager.GetObjects, func) == null)
                        {
                            break;
                        }
                        else
                        {
                            ticker2.Reset();
                        }
                    }
                    if (GatherSettings.Lure && _reLure.IsReady)
                    {
                        KeyHelper.SendKey("Lure");
                        Thread.Sleep(0xdac);
                        _reLure.Reset();
                    }
                    if (ticker.IsReady)
                    {
                        return false;
                    }
                    if (LazyLib.Wow.ObjectManager.MyPlayer.IsSwimming)
                    {
                        MoveHelper.Jump(0x5dc);
                        Thread.Sleep(0x3e8);
                        KeyHelper.SendKey("Waterwalk");
                        Thread.Sleep(0x7d0);
                        MoveHelper.Jump(0x5dc);
                        Thread.Sleep(0x5dc);
                        if (LazyLib.Wow.ObjectManager.MyPlayer.IsSwimming)
                        {
                            return false;
                        }
                    }
                    node.Location.Face();
                    Ticker ticker3 = new Ticker(4000.0);
                    while (true)
                    {
                        if (ticker3.IsReady || (node.Location.DistanceToSelf2D >= 14.0))
                        {
                            MoveHelper.ReleaseKeys();
                            ticker3.Reset();
                            node.Location.Face();
                            while (true)
                            {
                                if (ticker3.IsReady || (node.Location.DistanceToSelf2D <= 16.0))
                                {
                                    MoveHelper.ReleaseKeys();
                                    KeyHelper.SendKey("Fishing");
                                    Thread.Sleep(600);
                                    Fishing.FindBobberAndClick(GatherSettings.WaitForLoot);
                                    Thread.Sleep(100);
                                    break;
                                }
                                MoveHelper.Forwards(true);
                                Thread.Sleep(20);
                            }
                            break;
                        }
                        MoveHelper.Backwards(true);
                        Thread.Sleep(20);
                    }
                    break;
                }
            }
            return true;
        }

        public static bool GatherNode(PGameObject harvest)
        {
            while (Main.chatcommand)
            {
                Thread.Sleep(100);
            }
            if (FindNode.IsSchool(harvest))
            {
                return GatherFishNode(harvest);
            }
            if (!ApprochNode(harvest))
            {
                ToldAboutNode.TellAbout("we never approached the node", harvest);
                return false;
            }
            Logging.Debug("We approached the node", new object[0]);
            HitTheNode(harvest);
            if (CheckMobs(harvest))
            {
                if (GatherBlackList.IsBlacklisted(harvest))
                {
                    ToldAboutNode.TellAbout("is blacklisted", harvest);
                    return false;
                }
                if (MoveHelper.NegativeValue(LazyLib.Wow.ObjectManager.MyPlayer.Location.Z - FindNode.GetLocation(harvest).Z) > 1f)
                {
                    if (!LazySettings.GroundGather)
                    {
                        Logging.Debug("Descending", new object[0]);
                    }
                    DescentToNode(harvest);
                }
                if (GatherBlackList.IsBlacklisted(harvest))
                {
                    ToldAboutNode.TellAbout("is blacklisted", harvest);
                    return false;
                }
                if (FindNode.GetLocation(harvest).DistanceToSelf2D > 5.0)
                {
                    ApproachPosFlying.Approach(harvest.Location, 4);
                }
                if (FindNode.GetLocation(harvest).DistanceToSelf <= 10.0)
                {
                    return DismountAndHarvest(harvest, TimeOut);
                }
                Logging.Write(LogType.Warning, "Could not get to the node", new object[0]);
            }
            return false;
        }

        internal static bool HasRessSickness() => 
            LazyLib.Wow.ObjectManager.MyPlayer.HasBuff(0x3a9f);

        internal static void HitTheNode(PGameObject nodeToHarvest)
        {
            while (Main.chatcommand)
            {
                Thread.Sleep(100);
            }
            Face.Reset();
            ToLongRun.Reset();
            _oldDis = FindNode.GetLocation(nodeToHarvest).DistanceToSelf;
            while (true)
            {
                if (!ToLongRun.IsReady)
                {
                    MoveHelper.Forwards(true);
                    _distanceX = Math.Round((double) Math.Abs((float) (FindNode.GetLocation(nodeToHarvest).Y - LazyLib.Wow.ObjectManager.MyPlayer.Location.Y)));
                    _distanceY = Math.Round((double) Math.Abs((float) (FindNode.GetLocation(nodeToHarvest).X - LazyLib.Wow.ObjectManager.MyPlayer.Location.X)));
                    if ((_distanceX > 3.0) || (_distanceY > 3.0))
                    {
                        if (Face.IsReady)
                        {
                            FindNode.GetLocation(nodeToHarvest).Face();
                            Face.Reset();
                        }
                        Thread.Sleep(2);
                        if (_oldDis >= FindNode.GetLocation(nodeToHarvest).DistanceToSelf)
                        {
                            continue;
                        }
                    }
                }
                if (nodeToHarvest.Location.DistanceToSelf2D > 2.9)
                {
                    if (nodeToHarvest.Location.DistanceToSelf2D > 2.0)
                    {
                        Thread.Sleep(150);
                    }
                    else if (nodeToHarvest.Location.DistanceToSelf2D > 1.0)
                    {
                        Thread.Sleep(100);
                    }
                }
                MoveHelper.ReleaseKeys();
                return;
            }
        }

        internal static bool PlayerToClose(int distance, PGameObject gameObject)
        {
            if (!GatherSettings.AvoidPlayers)
            {
                return false;
            }
            if (!Enumerable.Any<PPlayer>(from obj in LazyLib.Wow.ObjectManager.GetPlayers
                where !obj.Name.Equals(LazyLib.Wow.ObjectManager.MyPlayer.Name)
                select obj, obj => FindNode.GetLocation(gameObject).GetDistanceTo(obj.Location) < distance))
            {
                return false;
            }
            Logging.Write("Player to close to node", new object[0]);
            GatherBlackList.Blacklist(gameObject, 300, false);
            return true;
        }
    }
}

