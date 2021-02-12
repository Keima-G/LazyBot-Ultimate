namespace LazyEvo.LGrindEngine.Activity
{
    using LazyEvo.LGrindEngine;
    using LazyEvo.LGrindEngine.Helpers;
    using LazyEvo.LGrindEngine.NpcClasses;
    using LazyLib;
    using LazyLib.Wow;
    using System;

    internal class Train
    {
        internal static bool TrainEnabled;
        internal static VendorsEx Trainer;

        internal static void Pulse()
        {
            if (Trainer == null)
            {
                GrindingEngine.Navigator.Stop();
                Trainer = GrindingEngine.CurrentProfile.NpcController.GetTrainer(LazyLib.Wow.ObjectManager.MyPlayer.UnitClass);
                GrindingEngine.Navigation.SetNewSpot(Trainer.Location);
                Logging.Write("Going to train at: " + Trainer.Name, new object[0]);
            }
            else if (Trainer.Location.DistanceToSelf2D < 5.0)
            {
                Logging.Write("Train done, going back", new object[0]);
                GrindingEngine.Navigator.Stop();
                GrindingEngine.Navigation = new GrindingNavigation(GrindingEngine.CurrentProfile);
                GrindingBlackList.Blacklist(Trainer.Name, 300, false);
                SetTrain(false);
            }
            if (!ReferenceEquals(GrindingEngine.Navigation.SpotToHit, Trainer.Location))
            {
                Logging.Write("Set spot", new object[0]);
                GrindingEngine.Navigation.SetNewSpot(Trainer.Location);
            }
            GrindingEngine.Navigation.Pulse();
        }

        internal static void SetTrain(bool enable)
        {
            if (enable)
            {
                TrainEnabled = true;
            }
            else
            {
                TrainEnabled = false;
                Trainer = null;
            }
        }
    }
}

