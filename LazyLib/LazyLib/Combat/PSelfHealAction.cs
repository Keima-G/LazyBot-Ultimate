namespace LazyLib.Combat
{
    using System;
    using System.Reflection;
    using System.Runtime.InteropServices;

    [Obfuscation(Feature="renaming", ApplyToMembers=true)]
    public class PSelfHealAction : PAction
    {
        public PSelfHealAction(int priority = 0, string spellName = null) : base(priority, spellName, 5)
        {
        }
    }
}

