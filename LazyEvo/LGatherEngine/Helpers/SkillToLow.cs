namespace LazyEvo.LGatherEngine.Helpers
{
    using LazyLib;
    using System;
    using System.Collections.Generic;

    public class SkillToLow
    {
        private static readonly Dictionary<string, DateTime> blacklist = new Dictionary<string, DateTime>();

        public static void Blacklist(string target, uint length)
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

        public static bool IsBlacklisted(string target)
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

        private static void Unblacklist(string target)
        {
            lock (blacklist)
            {
                blacklist.Remove(target);
            }
        }
    }
}

