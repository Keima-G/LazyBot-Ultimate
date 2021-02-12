namespace LazyEvo.LGrindEngine.Activity
{
    using LazyEvo.LGrindEngine;
    using LazyEvo.LGrindEngine.NpcClasses;
    using LazyLib;
    using System;

    internal class ToTown
    {
        internal static bool ToTownEnabled;
        internal static bool ToTownDoMail;
        internal static bool ToTownDoRepair;
        internal static bool ToTownDoVendor;
        internal static VendorsEx Vendor;

        internal static void Pulse()
        {
            if (Vendor == null)
            {
                GrindingEngine.Navigator.Stop();
                Vendor = GrindingEngine.CurrentProfile.NpcController.GetNearestRepair();
                GrindingEngine.Navigation.SetNewSpot(Vendor.Location);
                Logging.Write("Going to vendor at: " + Vendor.Name, new object[0]);
            }
            if (!ReferenceEquals(GrindingEngine.Navigation.SpotToHit, Vendor.Location))
            {
                GrindingEngine.Navigation.SetNewSpot(Vendor.Location);
            }
        }

        internal static void SetToTown(bool enable)
        {
            if (!enable)
            {
                ToTownEnabled = false;
                Vendor = null;
            }
            else
            {
                ToTownEnabled = true;
                ToTownDoRepair = true;
                ToTownDoVendor = true;
                ToTownDoMail = LazySettings.ShouldMail;
            }
        }
    }
}

