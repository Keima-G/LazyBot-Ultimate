namespace LazyEvo.Public
{
    using LazyLib;
    using LazyLib.Wow;
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Threading;

    [Obfuscation(Feature="renaming", ApplyToMembers=true)]
    public class PBlackList
    {
        private static readonly Dictionary<string, DateTime> blacklist = new Dictionary<string, DateTime>();

        public static void Blacklist(PUnit target, uint length, bool writeText)
        {
            if (target != null)
            {
                lock (blacklist)
                {
                    blacklist["GUID" + target.GUID] = DateTime.Now.AddSeconds((double) length);
                }
                if (writeText)
                {
                    Logging.Write(string.Concat(new object[] { "Added GUID: '", target.GUID, "' to bad list for ", length, " seconds" }), new object[0]);
                }
            }
        }

        public static void Blacklist(uint guid, uint length, bool writeText)
        {
            lock (blacklist)
            {
                blacklist["GUID" + guid] = DateTime.Now.AddSeconds((double) length);
            }
            if (writeText)
            {
                Logging.Write(string.Concat(new object[] { "Added GUID: '", guid, "' to bad list for ", length, " seconds" }), new object[0]);
            }
        }

        public static bool IsBlacklisted(PUnit target)
        {
            try
            {
                lock (blacklist)
                {
                    if ((target != null) && blacklist.ContainsKey("GUID" + target.GUID))
                    {
                        if ((blacklist["GUID" + target.GUID] - DateTime.Now).TotalSeconds <= 0.0)
                        {
                            Unblacklist(target);
                        }
                        else
                        {
                            return true;
                        }
                    }
                }
            }
            catch (ThreadAbortException)
            {
            }
            catch (Exception exception)
            {
                Logging.Debug("PBlackList: " + exception, new object[0]);
            }
            return false;
        }

        public static void Unblacklist(PUnit target)
        {
            if (target != null)
            {
                lock (blacklist)
                {
                    blacklist.Remove("GUID" + target.GUID);
                }
                Logging.Write("Removed: '" + target.GUID + " from badlist'", new object[0]);
            }
        }
    }
}

