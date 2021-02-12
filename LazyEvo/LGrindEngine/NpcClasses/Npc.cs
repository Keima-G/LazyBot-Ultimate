namespace LazyEvo.LGrindEngine.NpcClasses
{
    using System;
    using System.Xml;

    internal abstract class Npc
    {
        protected Npc()
        {
        }

        public abstract void Load(XmlNode xml);
        public abstract string Save();
    }
}

