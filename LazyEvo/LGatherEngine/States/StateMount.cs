namespace LazyEvo.LGatherEngine.States
{
    using LazyEvo.Forms;
    using LazyEvo.LGatherEngine;
    using LazyEvo.LGatherEngine.Activity;
    using LazyEvo.LGatherEngine.Helpers;
    using LazyLib;
    using LazyLib.FSM;
    using LazyLib.Helpers;
    using LazyLib.Wow;
    using System;

    internal class StateMount : MainState
    {
        public override void DoWork()
        {
            GatherEngine.Navigator.Stop();
            MoveHelper.ReleaseKeys();
            Main.CombatEngine.RunningAction();
            Mount.MountUp();
        }

        public override string Name() => 
            "Mounting";

        public override int Priority =>
            Prio.Mount;

        public override bool NeedToRun
        {
            get
            {
                if ((Inventory.FreeBagSlots <= Convert.ToInt32(LazySettings.FreeBackspace)) && (LazySettings.ShouldVendor && !ToTown.ToTownEnabled))
                {
                    Logging.Write("Inventory full, we are now in to town mode", new object[0]);
                    ToTown.SetToTown(true);
                }
                if (LazyLib.Wow.ObjectManager.MyPlayer.ShouldRepair && (LazySettings.ShouldRepair && !ToTown.ToTownEnabled))
                {
                    Logging.Write("One or more items broken, we are now in to town mode", new object[0]);
                    ToTown.SetToTown(true);
                }
                return !Mount.IsMounted();
            }
        }
    }
}

