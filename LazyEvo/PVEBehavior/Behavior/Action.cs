namespace LazyEvo.PVEBehavior.Behavior
{
    using System;
    using System.Xml;

    internal abstract class Action
    {
        protected Action()
        {
        }

        public abstract void Execute(int globalCooldown);
        public abstract string GetXml();
        public abstract void Load(XmlNode node);

        public abstract bool IsReady { get; }

        public abstract bool DoesKeyExist { get; }

        public abstract string Name { get; }
    }
}

