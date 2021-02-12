namespace LazyEvo.Plugins.ExtraLazy
{
    using System;

    public class Expert : Rank
    {
        public Expert()
        {
            base._rankText = "Expert";
            base._minLevel = 0x7d;
            base._maxLevel = 0xe1;
            base._rankLevel = 2;
        }
    }
}

