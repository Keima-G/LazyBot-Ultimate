namespace LazyEvo.LGrindEngine.States
{
    using LazyEvo.LGrindEngine;
    using LazyEvo.LGrindEngine.Activity;
    using LazyEvo.LGrindEngine.Helpers;
    using LazyEvo.LGrindEngine.NpcClasses;
    using LazyLib;
    using LazyLib.FSM;
    using LazyLib.Helpers;
    using LazyLib.Wow;
    using System;
    using System.Linq;

    internal class StateToTown : MainState
    {
        public override void DoWork()
        {
        }

        public override string Name() => 
            "ToTown";

        public override int Priority =>
            Prio.ToTown;

        public override bool NeedToRun
        {
            get
            {
                if (GrindingEngine.CurrentProfile.NpcController != null)
                {
                    if ((Inventory.FreeBagSlots < Convert.ToInt32(LazySettings.FreeBackspace)) && (LazySettings.ShouldVendor && !ToTown.ToTownEnabled))
                    {
                        Logging.Write("Inventory full, we are now in to town mode", new object[0]);
                        ToTown.SetToTown(true);
                    }
                    if (LazyLib.Wow.ObjectManager.MyPlayer.ShouldRepair && (LazySettings.ShouldRepair && !ToTown.ToTownEnabled))
                    {
                        Logging.Write("One or more items broken, we are now in to town mode", new object[0]);
                        ToTown.SetToTown(true);
                    }
                    if (GrindingEngine.ShouldTrain && (GrindingSettings.ShouldTrain && !LazyEvo.LGrindEngine.Activity.Train.TrainEnabled))
                    {
                        Logging.Write("Going to train new skills", new object[0]);
                        LazyEvo.LGrindEngine.Activity.Train.SetTrain(true);
                    }
                    if (LazyEvo.LGrindEngine.Activity.Train.TrainEnabled && (!ToTown.ToTownEnabled && (!LazyLib.Wow.ObjectManager.MyPlayer.IsDead && ((GrindingEngine.CurrentProfile.NpcController.GetTrainer(LazyLib.Wow.ObjectManager.MyPlayer.UnitClass) != null) && LazyEvo.LGrindEngine.Activity.Train.TrainEnabled))))
                    {
                        LazyEvo.LGrindEngine.Activity.Train.Pulse();
                    }
                    if ((ToTown.ToTownEnabled && !LazyLib.Wow.ObjectManager.MyPlayer.IsDead) && (((from npc in GrindingEngine.CurrentProfile.NpcController.Npc
                        where npc.VendorType == VendorType.Repair
                        select npc).ToList<VendorsEx>().Count != 0) && ToTown.ToTownEnabled))
                    {
                        ToTown.Pulse();
                    }
                }
                return false;
            }
        }
    }
}

