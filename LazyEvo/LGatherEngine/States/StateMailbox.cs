namespace LazyEvo.LGatherEngine.States
{
    using LazyEvo.LGatherEngine;
    using LazyEvo.LGatherEngine.Activity;
    using LazyLib;
    using LazyLib.FSM;
    using LazyLib.Helpers;
    using LazyLib.Helpers.Mail;
    using LazyLib.Wow;
    using System;

    internal class StateMailbox : MainState
    {
        private const int SearchDistance = 30;
        private readonly Ticker _mailTimeout = new Ticker(300000.0);
        private readonly PGameObject _mailbox = new PGameObject(0);

        public StateMailbox()
        {
            this._mailTimeout.ForceReady();
        }

        public override void DoWork()
        {
            Logging.Write("Found a mailbox, lets do something", new object[0]);
            GatherEngine.Navigator.Stop();
            if (ApproachPosFlying.Approach(this._mailbox.Location, 12))
            {
                MoveHelper.MoveToLoc(this._mailbox.Location, 5.0);
                MailManager.DoMail();
            }
            ToTown.ToTownDoMail = true;
            this._mailTimeout.Reset();
        }

        public override string Name() => 
            "Mailbox";

        public override bool NeedToRun
        {
            get
            {
                if (LazyLib.Wow.ObjectManager.MyPlayer.IsDead || (GatherEngine.CurrentProfile.WaypointsToTown.Count == 0))
                {
                    return false;
                }
                if (!ToTown.ToTownEnabled)
                {
                    return false;
                }
                if (!LazySettings.ShouldMail || string.IsNullOrEmpty(LazySettings.MailTo))
                {
                    return false;
                }
                if (!ToTown.FollowingWaypoints)
                {
                    return false;
                }
                if (!this._mailTimeout.IsReady)
                {
                    return false;
                }
                this._mailbox.BaseAddress = 0;
                foreach (PGameObject obj2 in LazyLib.Wow.ObjectManager.GetGameObject)
                {
                    if ((obj2.GameObjectType == 0x13) && (obj2.Location.DistanceToSelf2D < 30.0))
                    {
                        this._mailbox.BaseAddress = obj2.BaseAddress;
                        break;
                    }
                }
                return (this._mailbox.BaseAddress != 0);
            }
        }

        public override int Priority =>
            Prio.MailBox;
    }
}

