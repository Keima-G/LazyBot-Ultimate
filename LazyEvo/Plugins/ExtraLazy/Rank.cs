namespace LazyEvo.Plugins.ExtraLazy
{
    using System;

    public abstract class Rank
    {
        protected string _rankText;
        protected int _minLevel;
        protected int _maxLevel;
        protected int _rankLevel;

        protected Rank()
        {
        }

        public static Rank operator ++(Rank c1) => 
            RankFactory.promote(c1);

        public string RankText =>
            this._rankText;

        public int MinLevel =>
            this._minLevel;

        public int MaxLevel =>
            this._maxLevel;

        public int RankLevel =>
            this._rankLevel;
    }
}

