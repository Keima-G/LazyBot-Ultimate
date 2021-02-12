namespace LazyEvo.LGatherEngine.States
{
    using LazyEvo.Forms;
    using LazyEvo.LGatherEngine;
    using LazyEvo.LGatherEngine.Activity;
    using LazyEvo.LGatherEngine.Helpers;
    using LazyEvo.Public;
    using LazyLib;
    using LazyLib.FSM;
    using LazyLib.Helpers;
    using LazyLib.Wow;
    using System;
    using System.Threading;

    internal class StateGather : MainState
    {
        private static readonly Ticker TimeOut = new Ticker(2000.0);
        private static Location _nodeLoc = new Location(0f, 0f, 0f);
        private static int _approachTimes;
        private readonly PGameObject _node = new PGameObject(0);

        public StateGather()
        {
            this._node = new PGameObject(0);
        }

        public override void DoWork()
        {
            while (Main.chatcommand)
            {
                Thread.Sleep(100);
            }
            Logging.Debug("Approaching: " + this._node.Location, new object[0]);
            LazyLib.Wow.Globals._schoolLocX = this._node.Location.X;
            LazyLib.Wow.Globals._schoolLocY = this._node.Location.Y;
            if (!ReferenceEquals(_nodeLoc, this._node.Location))
            {
                _nodeLoc = this._node.Location;
                _approachTimes = 0;
            }
            else
            {
                if (_approachTimes > 6)
                {
                    Logging.Write("We tried to approach the same node more than 6 times... does not make sense abort", new object[0]);
                    GatherBlackList.Blacklist(this._node, 120, true);
                    return;
                }
                _approachTimes++;
            }
            if (!Gather.GatherNode(this._node))
            {
                if (!LazySettings.GroundGather && (LazyLib.Wow.ObjectManager.ShouldDefend && Mount.IsMounted()))
                {
                    Logging.Write("ShouldDefend while mounted? Odd", new object[0]);
                    GatherBlackList.Blacklist(this._node, 120, true);
                }
            }
            else
            {
                GatherEngine.UpdateStats(1, 0, 0);
                if (GatherSettings.WaitForLoot)
                {
                    while (true)
                    {
                        if (!LazyLib.Wow.ObjectManager.MyPlayer.LootWinOpen || TimeOut.IsReady)
                        {
                            Latency.Sleep(0x514);
                            break;
                        }
                        Thread.Sleep(100);
                    }
                }
                if (LazyLib.Wow.ObjectManager.MyPlayer.IsInFlightForm)
                {
                    MoveHelper.Jump();
                }
            }
            if (LazyLib.Wow.ObjectManager.MyPlayer.IsSwimming)
            {
                Logging.Write("It appears we got wet, should propably blacklist this node?", new object[0]);
                KeyHelper.SendKey("Space");
                Thread.Sleep(0xbb8);
                KeyHelper.ReleaseKey("Space");
            }
            if (!LazyLib.Wow.ObjectManager.ShouldDefend && !LazyLib.Wow.ObjectManager.MyPlayer.IsInCombat)
            {
                LootedBlacklist.Looted(this._node);
                if (LazyLib.Wow.ObjectManager.MyPlayer.InVashjir)
                {
                    KeyHelper.SendKey("Space");
                    Thread.Sleep(0x3e8);
                    KeyHelper.ReleaseKey("Space");
                }
            }
            if (!Mount.IsMounted())
            {
                CombatHandler.RunningAction();
                CombatHandler.Rest();
            }
            Stuck.Reset();
            if (GatherEngine.CurrentProfile.NaturalRun)
            {
                GatherEngine.Navigation.UseNearestWaypoint(-1);
            }
            else
            {
                GatherEngine.Navigation.UseNearestWaypoint(10);
            }
        }

        public override string Name() => 
            "Gathering";

        public override bool NeedToRun
        {
            get
            {
                if (ToTown.ToTownEnabled)
                {
                    return false;
                }
                this._node.BaseAddress = 0;
                PGameObject target = FindNode.SearchForNode();
                if (target != null)
                {
                    if (GatherBlackList.IsBlacklisted(target) || SkillToLow.IsBlacklisted(target.Name))
                    {
                        return false;
                    }
                    if ((target.Location.DistanceToSelf2D > 10.0) && !Mount.IsMounted())
                    {
                        return false;
                    }
                    if (LazySettings.GroundGather && (target.Location.DistanceToSelf > LazySettings.GroundGatherDistance))
                    {
                        return false;
                    }
                    this._node.BaseAddress = target.BaseAddress;
                }
                return (this._node.BaseAddress != 0);
            }
        }

        public override int Priority =>
            Prio.Gathering;
    }
}

