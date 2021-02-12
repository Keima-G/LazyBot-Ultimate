namespace LazyEvo.Plugins.ExtraLazy
{
    using System;

    public class Master : Rank
    {
        public Master()
        {
            base._rankText = "Master";
            base._minLevel = 0x113;
            base._maxLevel = 0x177;
            base._rankLevel = 4;
        }
    }
}

