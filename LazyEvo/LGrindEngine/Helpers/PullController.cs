namespace LazyEvo.LGrindEngine.Helpers
{
    using LazyEvo.LGrindEngine;
    using LazyEvo.Public;
    using LazyLib;
    using LazyLib.Wow;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;

    internal class PullController
    {
        internal static List<PUnit> ValidUnits = new List<PUnit>();
        private static Thread _searchThread;

        private static void FindMobToPull()
        {
            while (true)
            {
                List<PUnit> list = new List<PUnit>();
                List<PUnit> getUnits = LazyLib.Wow.ObjectManager.GetUnits;
                SubProfile currentSubprofile = GrindingEngine.CurrentProfile.GetSubProfile();
                foreach (PUnit unit in from u in getUnits
                    where u.IsValid
                    select u)
                {
                    try
                    {
                        if ((IsValidTarget(unit) && (!PPullBlackList.IsBlacklisted(unit) && !PBlackList.IsBlacklisted(unit))) && (unit.Target.Type != 4))
                        {
                            if (!GrindingSettings.SkipMobsWithAdds || (LazyLib.Wow.ObjectManager.CheckForMobsAtLoc(unit.Location, (float) GrindingSettings.SkipAddsDistance, false).Count < GrindingSettings.SkipAddsCount))
                            {
                                PUnit unit = unit;
                                if (Enumerable.Any<Location>(currentSubprofile.Spots, s => unit.Location.DistanceFrom(s) < currentSubprofile.SpotRoamDistance) && !unit.IsPet)
                                {
                                    list.Add(unit);
                                }
                            }
                        }
                    }
                    catch
                    {
                    }
                }
                ValidUnits = (from t in list
                    orderby t.Location.DistanceToSelf
                    select t).ToList<PUnit>();
                Thread.Sleep(500);
            }
        }

        private static bool IsValidTarget(PUnit unitTofind)
        {
            Func<string, bool> func = null;
            Func<uint, bool> func2 = null;
            if (unitTofind.IsPlayer || (unitTofind.IsTagged || (unitTofind.IsDead || unitTofind.IsTotem)))
            {
                return false;
            }
            SubProfile subProfile = GrindingEngine.CurrentProfile.GetSubProfile();
            if ((unitTofind.Level < subProfile.MobMinLevel) || ((unitTofind.Level > subProfile.MobMaxLevel) || (unitTofind.DistanceToSelf > LazySettings.PullControlDistance)))
            {
                return false;
            }
            if (subProfile.Ignore != null)
            {
                if (func == null)
                {
                    func = tstIgnore => tstIgnore == unitTofind.Name;
                }
                if (Enumerable.Any<string>(subProfile.Ignore, func))
                {
                    return false;
                }
            }
            if (subProfile.Factions == null)
            {
                return false;
            }
            if (func2 == null)
            {
                func2 = faction => unitTofind.Faction == faction;
            }
            return Enumerable.Any<uint>(subProfile.Factions, func2);
        }

        public static void Start()
        {
            Thread thread = new Thread(new ThreadStart(PullController.FindMobToPull)) {
                IsBackground = true
            };
            _searchThread = thread;
            _searchThread.Name = "FindMobToPull";
            _searchThread.Start();
        }

        public static void Stop()
        {
            if ((_searchThread != null) && _searchThread.IsAlive)
            {
                _searchThread.Abort();
                _searchThread = null;
            }
        }
    }
}

