namespace LazyEvo.Plugins.ExtraLazy
{
    using System;
    using System.Runtime.CompilerServices;

    public class Profession
    {
        public string Name { get; internal set; }

        public int Level { get; internal set; }

        public LazyEvo.Plugins.ExtraLazy.Rank Rank { get; internal set; }
    }
}

