namespace LazyEvo.Plugins.ExtraLazy
{
    using System;

    public class GrandMaster : Rank
    {
        public GrandMaster()
        {
            base._rankText = "Grand Master";
            base._minLevel = 350;
            base._maxLevel = 450;
            base._rankLevel = 5;
        }
    }
}

