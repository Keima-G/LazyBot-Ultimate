namespace LazyEvo.LGrindEngine.Helpers
{
    using LazyLib;
    using LazyLib.Wow;
    using System;
    using System.Collections.Generic;

    internal class GrindingBlackList
    {
        internal static List<Location> BadNodes = new List<Location>();
        private static readonly Dictionary<string, DateTime> blacklist = new Dictionary<string, DateTime>();

        public static void Blacklist(PObject target, uint length, bool writeText)
        {
            if (target != null)
            {
                Blacklist(target.GUID, length, writeText);
            }
        }

        public static void Blacklist(string target, uint length, bool writeText)
        {
            if (target != null)
            {
                lock (blacklist)
                {
                    blacklist["GUID" + target] = DateTime.Now.AddSeconds((double) length);
                }
                if (writeText)
                {
                    Logging.Write(string.Concat(new object[] { "Added GUID: '", target, "' to bad list for ", length, " seconds" }), new object[0]);
                }
            }
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
                    if (!IsBlacklisted(target.GUID))
                    {
                        if (IsBlacklisted(target.Name))
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

        public static bool IsBlacklisted(string target)
        {
            try
            {
                if ((target != null) && blacklist.ContainsKey("GUID" + target))
                {
                    if ((blacklist["GUID" + target] - DateTime.Now).TotalSeconds <= 0.0)
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

        public static void Unblacklist(PObject target)
        {
            if (target != null)
            {
                Unblacklist(target.GUID);
            }
        }

        public static void Unblacklist(string target)
        {
            lock (blacklist)
            {
                blacklist.Remove("GUID" + target);
            }
            Logging.Write("Removed: '" + target + " from badlist'", new object[0]);
        }
    }
}

