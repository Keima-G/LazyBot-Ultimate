namespace LazyLib.Combat
{
    using LazyLib.Wow;
    using System;
    using System.Reflection;
    using System.Runtime.InteropServices;

    [Obfuscation(Feature="renaming", ApplyToMembers=true)]
    public class PSelfBuffAction : PAction
    {
        public PSelfBuffAction(int priority = 0, string spellName = null) : base(priority, spellName, 5)
        {
        }

        public override bool IsWanted =>
            base.IsWanted && !LazyLib.Wow.ObjectManager.MyPlayer.HasWellKnownBuff(this.SpellName);
    }
}

