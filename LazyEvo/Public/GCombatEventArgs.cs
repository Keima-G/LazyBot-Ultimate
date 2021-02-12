namespace LazyEvo.Public
{
    using LazyLib.Wow;
    using System;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    [Obfuscation(Feature="renaming", ApplyToMembers=true)]
    public class GCombatEventArgs : EventArgs
    {
        public GCombatEventArgs(LazyEvo.Public.CombatType type)
        {
            this.CombatType = type;
            this.Unit = new PUnit(0);
        }

        public GCombatEventArgs(LazyEvo.Public.CombatType type, PUnit unit)
        {
            this.CombatType = type;
            this.Unit = unit;
        }

        public LazyEvo.Public.CombatType CombatType { get; private set; }

        public PUnit Unit { get; private set; }
    }
}

