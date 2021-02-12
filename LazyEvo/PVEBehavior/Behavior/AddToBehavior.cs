namespace LazyEvo.PVEBehavior.Behavior
{
    using System;
    using System.Runtime.CompilerServices;

    internal class AddToBehavior
    {
        public AddToBehavior(string name, LazyEvo.PVEBehavior.Behavior.Type type, LazyEvo.PVEBehavior.Behavior.Spec spec, LazyEvo.PVEBehavior.Behavior.Rule rule)
        {
            this.Name = name;
            this.Type = type;
            this.Spec = spec;
            this.Rule = rule;
        }

        public override string ToString() => 
            this.Name;

        public string Name { get; set; }

        public LazyEvo.PVEBehavior.Behavior.Type Type { get; set; }

        public LazyEvo.PVEBehavior.Behavior.Spec Spec { get; set; }

        public LazyEvo.PVEBehavior.Behavior.Rule Rule { get; set; }
    }
}

