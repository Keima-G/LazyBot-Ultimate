namespace LazyEvo.LGatherEngine.Helpers
{
    using LazyLib;
    using LazyLib.Wow;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class LootedBlacklist
    {
        private static readonly Dictionary<ulong, DateTime> LootedDic = new Dictionary<ulong, DateTime>();

        private static void Check()
        {
            try
            {
                lock (LootedDic)
                {
                    CS$<>9__CachedAnonymousMethodDelegate5 ??= <>h__TransparentIdentifier0 => (<>h__TransparentIdentifier0.diff.TotalSeconds < 0.0);
                    CS$<>9__CachedAnonymousMethodDelegate6 ??= <>h__TransparentIdentifier0 => <>h__TransparentIdentifier0.node.Key;
                    foreach (ulong num in Enumerable.Select(Enumerable.Where(from node in LootedDic select new { 
                        node = node,
                        diff = node.Value - DateTime.Now
                    }, CS$<>9__CachedAnonymousMethodDelegate5), CS$<>9__CachedAnonymousMethodDelegate6).ToList<ulong>())
                    {
                        Unblacklist(num);
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        public static bool IsLooted(PGameObject target)
        {
            try
            {
                if ((target != null) && LootedDic.ContainsKey(target.GUID))
                {
                    Check();
                    if (LootedDic.ContainsKey(target.GUID))
                    {
                        return true;
                    }
                }
            }
            catch (Exception exception)
            {
                Logging.Write("IsLooted: " + exception, new object[0]);
            }
            return false;
        }

        public static void Looted(PGameObject target)
        {
            if (target != null)
            {
                try
                {
                    if (!LootedDic.ContainsKey(target.GUID))
                    {
                        lock (LootedDic)
                        {
                            LootedDic[target.GUID] = DateTime.Now.AddSeconds(20.0);
                        }
                    }
                }
                catch
                {
                }
            }
        }

        private static void Unblacklist(ulong guid)
        {
            lock (LootedDic)
            {
                LootedDic.Remove(guid);
            }
        }
    }
}

