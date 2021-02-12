namespace LazyEvo.Plugins.ExtraLazy
{
    using System;

    public class Apprentice : Rank
    {
        public Apprentice()
        {
            base._rankText = "Apprentice";
            base._minLevel = 0;
            base._maxLevel = 0x4b;
            base._rankLevel = 0;
        }
    }
}

