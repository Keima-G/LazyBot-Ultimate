namespace LazyEvo.LGatherEngine.Helpers
{
    using LazyLib;
    using LazyLib.Helpers;
    using LazyLib.Wow;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal class FindNode
    {
        private static List<string> _mines = new List<string>();
        private static List<string> _herbs = new List<string>();
        private static IEnumerable<PGameObject> _cached = new List<PGameObject>();
        private static readonly Ticker ReloadCache = new Ticker(1200.0);
        public static Location _schoolLoc = new Location(0f, 0f, 0f);

        public static Location GetLocation(PObject node)
        {
            switch (node.Type)
            {
                case 3:
                    return node.Location;

                case 5:
                    return node.Location;
            }
            return new Location(0f, 0f, 0f);
        }

        public static string GetName(PObject node)
        {
            switch (node.Type)
            {
                case 3:
                    return ((PUnit) node).Name;

                case 5:
                    return ((PGameObject) node).Name;
            }
            return "";
        }

        public static bool IsHerb(PObject node) => 
            (node.Type == 5) && _herbs.Contains(((PGameObject) node).Name);

        public static bool IsMine(PObject node) => 
            (node.Type == 5) && _mines.Contains(((PGameObject) node).Name);

        public static bool IsSchool(PObject node)
        {
            if ((node.Type != 5) || (((PGameObject) node).GameObjectType != 0x19))
            {
                return false;
            }
            LazyLib.Wow.Globals._schoolLocX = node.Location.X;
            LazyLib.Wow.Globals._schoolLocY = node.Location.Y;
            return true;
        }

        public static void LoadHarvest()
        {
            Mine.Load();
            Herb.Load();
            _mines = Mine.GetList();
            _herbs = Herb.GetList();
            Logging.Debug(string.Concat(new object[] { "Mines: ", _mines.Count, " - Herbs: ", _herbs.Count }), new object[0]);
        }

        public static PGameObject SearchForNode()
        {
            PGameObject obj4;
            try
            {
                if (ReloadCache.IsReady)
                {
                    _cached = from u in LazyLib.Wow.ObjectManager.GetGameObject
                        where (((_herbs.Contains(u.Name) && GatherSettings.Herb) || (_mines.Contains(u.Name) && GatherSettings.Mine)) || (GatherSettings.Fish && ((u.GameObjectType == 0x19) && (u.Location.DistanceToSelf2D < GatherSettings.FishApproach)))) && (!GatherBlackList.IsBlacklisted(u) && (!SkillToLow.IsBlacklisted(u.Name) && !LootedBlacklist.IsLooted(u)))
                        select u;
                    ReloadCache.Reset();
                }
                if (GatherSettings.Fish)
                {
                    PGameObject obj2 = (from h in _cached
                        where h.GameObjectType == 0x19
                        select h).FirstOrDefault<PGameObject>();
                    if (obj2 != null)
                    {
                        return obj2;
                    }
                }
                IOrderedEnumerable<PGameObject> source = from p in _cached
                    orderby p.Location.DistanceToSelf
                    select p;
                if (source.Count<PGameObject>() <= 0)
                {
                    goto TR_0000;
                }
                else
                {
                    PGameObject target = source.ToList<PGameObject>()[0];
                    if (!MarkedNode.IsMarked(target))
                    {
                        object[] args = new object[] { target.Name };
                        Logging.Write(LogType.Info, "Node Found: {0}", args);
                        MarkedNode.MarkNode(target);
                    }
                    obj4 = target;
                }
            }
            catch (Exception)
            {
                goto TR_0000;
            }
            return obj4;
        TR_0000:
            return null;
        }
    }
}

