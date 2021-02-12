namespace LazyLib.Combat
{
    using LazyLib;
    using LazyLib.ActionBar;
    using LazyLib.Wow;
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    [Obfuscation(Feature="renaming", ApplyToMembers=true)]
    public abstract class PAction : IComparable<PAction>, IComparer<PAction>
    {
        protected PAction(int priority = 0, string spellName = null, int range = 5)
        {
            this.Priority = priority;
            this.SpellName = spellName;
            this.Range = range;
        }

        public int Compare(PAction x, PAction y) => 
            -x.Priority.CompareTo(y.Priority);

        public int CompareTo(PAction other)
        {
            if (other != null)
            {
                return -this.Priority.CompareTo(other.Priority);
            }
            Logging.Write("Tried to compare null PAction to another - check class code!", new object[0]);
            return 0;
        }

        public virtual void Execute()
        {
            BarMapper.CastSpell(this.SpellName);
        }

        public BarSpell GetKey() => 
            BarMapper.GetSpellByName(this.SpellName);

        public int Priority { virtual get; private set; }

        public string SpellName { virtual get; private set; }

        public int Range { virtual get; private set; }

        public virtual bool IsWanted =>
            this.IsReady;

        public virtual bool IsReady =>
            BarMapper.IsSpellReadyByName(this.SpellName) && !LazyLib.Wow.ObjectManager.MyPlayer.IsCasting;

        public virtual bool WaitUntilReady =>
            false;
    }
}

