namespace LazyEvo.Plugins.ExtraLazy
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class RankFactory
    {
        public const int RANK_APPRENTICE = 0;
        public const int RANK_JOURNEYMAN = 1;
        public const int RANK_EXPERT = 2;
        public const int RANK_ARTISAN = 3;
        public const int RANK_MASTER = 4;
        public const int RANK_GRAND_MASTER = 5;
        public const int RANK_Illustrious = 6;
        public const int RANK_MAX = 6;
        private static List<Rank> ranks;

        private static void createRanks()
        {
            if (ranks == null)
            {
                ranks = new List<Rank>();
                ranks.Add(new Apprentice());
                ranks.Add(new Journeyman());
                ranks.Add(new Expert());
                ranks.Add(new Artisan());
                ranks.Add(new Master());
                ranks.Add(new GrandMaster());
                ranks.Add(new Illustrious());
            }
        }

        public static Rank getRank(int level)
        {
            createRanks();
            return ((level > 6) ? null : ranks.ElementAt<Rank>(level));
        }

        public static Rank getRank(string rankText)
        {
            Rank rank2;
            createRanks();
            using (List<Rank>.Enumerator enumerator = ranks.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        Rank current = enumerator.Current;
                        if (current.RankText.ToLower() != rankText.ToLower())
                        {
                            continue;
                        }
                        rank2 = current;
                    }
                    else
                    {
                        return null;
                    }
                    break;
                }
            }
            return rank2;
        }

        public static Rank promote(Rank rank)
        {
            createRanks();
            return ((rank.RankLevel >= 6) ? rank : ranks.ElementAt<Rank>((rank.RankLevel + 1)));
        }
    }
}

