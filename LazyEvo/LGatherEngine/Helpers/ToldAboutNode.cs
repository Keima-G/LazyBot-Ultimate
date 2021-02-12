namespace LazyEvo.LGatherEngine.Helpers
{
    using LazyLib;
    using LazyLib.Wow;
    using System;
    using System.Collections.Generic;

    internal class ToldAboutNode
    {
        private static readonly Dictionary<string, DateTime> HasToldDictionary = new Dictionary<string, DateTime>();

        internal static bool HasTold(PGameObject target)
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
                    Logging.Debug("HasTold: " + exception, new object[0]);
                }
            }
            return false;
        }

        private static bool HasTold(string target)
        {
            try
            {
                if ((target != null) && HasToldDictionary.ContainsKey("GUID" + target))
                {
                    if ((HasToldDictionary["GUID" + target] - DateTime.Now).TotalSeconds <= 0.0)
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
                Logging.Debug("HasTold: " + exception, new object[0]);
            }
            return false;
        }

        private static void HasToldAbout(PObject target)
        {
            if (target != null)
            {
                lock (HasToldDictionary)
                {
                    HasToldDictionary["GUID" + target.GUID] = DateTime.Now.AddSeconds(500.0);
                }
            }
        }

        internal static void TellAbout(string text, PGameObject pGameObject)
        {
            if (!HasTold(pGameObject))
            {
                object[] args = new object[] { pGameObject.Name, text };
                Logging.ExtendedDebug("Not picking up node {0} due to: {1}", args);
            }
            HasToldAbout(pGameObject);
        }

        private static void Unblacklist(string target)
        {
            lock (HasToldDictionary)
            {
                HasToldDictionary.Remove("GUID" + target);
            }
        }
    }
}

