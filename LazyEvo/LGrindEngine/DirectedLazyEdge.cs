namespace LazyEvo.LGrindEngine
{
    using LazyLib.Wow;
    using QuickGraph;
    using System;
    using System.Reflection;

    [Serializable, Obfuscation(Feature="renaming", ApplyToMembers=true, Exclude=true)]
    public class DirectedLazyEdge : Edge<Location>
    {
        public DirectedLazyEdge(Location source, Location target) : base(source, target)
        {
        }
    }
}

