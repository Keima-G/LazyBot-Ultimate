namespace LazyEvo.Plugins.ExtraLazy
{
    using System;

    public class Journeyman : Rank
    {
        public Journeyman()
        {
            base._rankText = "Journeyman";
            base._minLevel = 50;
            base._maxLevel = 150;
            base._rankLevel = 1;
        }
    }
}

