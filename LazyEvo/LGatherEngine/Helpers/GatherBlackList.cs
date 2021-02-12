namespace LazyEvo.LGatherEngine.Helpers
{
    using LazyEvo.LGatherEngine;
    using LazyLib;
    using LazyLib.Wow;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal class GatherBlackList
    {
        internal static List<Location> BadNodes = new List<Location>();
        private static readonly Dictionary<ulong, DateTime> blacklist = new Dictionary<ulong, DateTime>();

        public static void AddBadNode(Location location)
        {
            GatherEngine.CurrentProfile.AddBadNode(location);
            GatherEngine.CurrentProfile.SaveFile(GatherEngine.CurrentProfile.FileName);
            Load();
        }

        public static void AddBadNode(PObject pObject)
        {
            AddBadNode(pObject.Location);
        }

        public static void Blacklist(PObject target, uint length, bool writeText)
        {
            Blacklist(target.GUID, length, writeText);
        }

        public static void Blacklist(ulong target, uint length, bool writeText)
        {
            try
            {
                if (!blacklist.ContainsKey(target))
                {
                    lock (blacklist)
                    {
                        blacklist[target] = DateTime.Now.AddSeconds((double) length);
                    }
                }
            }
            catch
            {
            }
        }

        private static bool IsBadNode(PObject checkNode)
        {
            Func<Location, bool> func = null;
            if (checkNode != null)
            {
                try
                {
                    lock (BadNodes)
                    {
                        if (func == null)
                        {
                            func = node => FindNode.GetLocation(checkNode).GetDistanceTo(node) < 5.0;
                        }
                        if (Enumerable.Any<Location>(BadNodes, func))
                        {
                            return true;
                        }
                    }
                }
                catch
                {
                }
            }
            return false;
        }

        public static bool IsBlacklisted(PGameObject target)
        {
            bool flag;
            if (target == null)
            {
                goto TR_0000;
            }
            else
            {
                try
                {
                    if (!IsBadNode(target))
                    {
                        if (IsBlacklisted(target.GUID))
                        {
                            return true;
                        }
                    }
                    else
                    {
                        return true;
                    }
                }
                catch (Exception exception)
                {
                    Logging.Write("IsObjectBlacklisted: " + exception, new object[0]);
                }
                goto TR_0000;
            }
            return flag;
        TR_0000:
            return false;
        }

        public static bool IsBlacklisted(ulong target)
        {
            try
            {
                if (blacklist.ContainsKey(target))
                {
                    if ((blacklist[target] - DateTime.Now).TotalSeconds <= 0.0)
                    {
                        Unblacklist(target);
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            catch (Exception exception)
            {
                Logging.Write("IsObjectBlacklisted: " + exception, new object[0]);
            }
            return false;
        }

        public static void Load()
        {
            BadNodes = GatherEngine.CurrentProfile.GetBadNodes;
        }

        public static void Unblacklist(PObject target)
        {
            if (target != null)
            {
                Unblacklist(target.GUID);
                GatherEngine.CurrentProfile.RemoveBadNode(target.Location);
                GatherEngine.CurrentProfile.SaveFile(GatherEngine.CurrentProfile.FileName);
            }
        }

        public static void Unblacklist(ulong target)
        {
            lock (blacklist)
            {
                blacklist.Remove(target);
            }
        }
    }
}

