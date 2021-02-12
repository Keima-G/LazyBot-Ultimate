namespace LazyEvo.LGrindEngine.NpcClasses
{
    using System;
    using System.Runtime.CompilerServices;

    internal class Train
    {
        public Train(string @class, int level, int clickTimes, string comment)
        {
            this.Class = @class;
            this.Level = level;
            this.ClickTimes = clickTimes;
            this.Comment = comment;
        }

        public string Class { get; set; }

        public int Level { get; set; }

        public int ClickTimes { get; set; }

        public string Comment { get; set; }
    }
}

