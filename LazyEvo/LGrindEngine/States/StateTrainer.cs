namespace LazyEvo.LGrindEngine.States
{
    using LazyEvo.LGrindEngine;
    using LazyEvo.LGrindEngine.Activity;
    using LazyEvo.LGrindEngine.Helpers;
    using LazyLib;
    using LazyLib.ActionBar;
    using LazyLib.FSM;
    using LazyLib.Helpers;
    using LazyLib.Wow;
    using System;
    using System.Threading;

    internal class StateTrainer : MainState
    {
        private const int SearchDistance = 12;
        private readonly PUnit _npc = new PUnit(0);

        private void DoTrain()
        {
            int num = GrindingShouldTrain.TrainCount();
            MoveHelper.MoveToUnit(this._npc, 3.0);
            this._npc.Location.Face();
            this._npc.Interact(false);
            Thread.Sleep(0x4b0);
            Logging.Write("Going to train: " + num + " new skill(s)", new object[0]);
            for (int i = 0; i <= num; i++)
            {
                try
                {
                    InterfaceHelper.GetFrameByName("ClassTrainerTrainButton");
                    Thread.Sleep(0x3e8);
                }
                catch (Exception exception)
                {
                    Logging.Debug("Error when clicking train button: " + exception, new object[0]);
                }
            }
        }

        public override void DoWork()
        {
            Logging.Write("[Train]Found the trainer", new object[0]);
            GrindingEngine.Navigator.Stop();
            MoveHelper.MoveToLoc(this._npc.Location, 5.0);
            this.DoTrain();
            Logging.Write("Re-mapping the bars", new object[0]);
            BarMapper.MapBars();
            Logging.Write("[Train]Train done", new object[0]);
            GrindingEngine.Navigator.Stop();
            GrindingEngine.Navigation = new GrindingNavigation(GrindingEngine.CurrentProfile);
            Train.SetTrain(false);
            GrindingEngine.ShouldTrain = false;
        }

        public override string Name() => 
            "Train";

        public override bool NeedToRun
        {
            get
            {
                if (LazyLib.Wow.ObjectManager.MyPlayer.IsDead)
                {
                    return false;
                }
                if (!GrindingSettings.ShouldTrain)
                {
                    return false;
                }
                if (!Train.TrainEnabled)
                {
                    return false;
                }
                if (Train.Trainer == null)
                {
                    return false;
                }
                this._npc.BaseAddress = 0;
                foreach (PUnit unit in LazyLib.Wow.ObjectManager.GetUnits)
                {
                    if (Train.Trainer.EntryId != -2147483648)
                    {
                        if ((unit.Entry != Train.Trainer.EntryId) || ((unit.Location.DistanceToSelf2D >= 12.0) || GrindingBlackList.IsBlacklisted(unit.Name)))
                        {
                            continue;
                        }
                        this._npc.BaseAddress = unit.BaseAddress;
                    }
                    else
                    {
                        if (!unit.Name.Equals(Train.Trainer.Name) || ((unit.Location.DistanceToSelf2D >= 12.0) || GrindingBlackList.IsBlacklisted(unit.Name)))
                        {
                            continue;
                        }
                        this._npc.BaseAddress = unit.BaseAddress;
                    }
                    break;
                }
                return (this._npc.BaseAddress != 0);
            }
        }

        public override int Priority =>
            Prio.Train;
    }
}

