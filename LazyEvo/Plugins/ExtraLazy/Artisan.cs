namespace LazyEvo.Plugins.ExtraLazy
{
    using System;

    public class Artisan : Rank
    {
        public Artisan()
        {
            base._rankText = "Artisan";
            base._minLevel = 200;
            base._maxLevel = 300;
            base._rankLevel = 3;
        }
    }
}

