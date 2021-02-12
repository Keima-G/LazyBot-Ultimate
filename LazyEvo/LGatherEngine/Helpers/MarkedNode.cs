namespace LazyEvo.LGatherEngine.Helpers
{
    using LazyLib;
    using LazyLib.Wow;
    using System;
    using System.Collections.Generic;

    public class MarkedNode
    {
        private static readonly Dictionary<string, DateTime> IsMarkedDictionary = new Dictionary<string, DateTime>();

        private static bool HasTold(string target)
        {
            try
            {
                if ((target != null) && IsMarkedDictionary.ContainsKey("GUID" + target))
                {
                    if ((IsMarkedDictionary["GUID" + target] - DateTime.Now).TotalSeconds <= 0.0)
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
                Logging.Debug("Marked: " + exception, new object[0]);
            }
            return false;
        }

        private static void HasToldAbout(PObject target)
        {
            if (target != null)
            {
                lock (IsMarkedDictionary)
                {
                    IsMarkedDictionary["GUID" + target.GUID] = DateTime.Now.AddSeconds(30.0);
                }
            }
        }

        internal static bool IsMarked(PGameObject target)
        {
            if (target != null)
            {
                try
                {
                    if (HasTold(target.GUID))
                    {
                        return true;
                    }
                }
                catch (Exception exception)
                {
                    Logging.Debug("Marked: " + exception, new object[0]);
                }
            }
            return false;
        }

        internal static void MarkNode(PGameObject pGameObject)
        {
            HasToldAbout(pGameObject);
        }

        private static void Unblacklist(string target)
        {
            lock (IsMarkedDictionary)
            {
                IsMarkedDictionary.Remove("GUID" + target);
            }
        }
    }
}

