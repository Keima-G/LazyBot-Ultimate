namespace LazyLib.Combat
{
    using System;
    using System.Reflection;
    using System.Runtime.InteropServices;

    [Obfuscation(Feature="renaming", ApplyToMembers=true)]
    public class PDamageAction : PAction
    {
        public PDamageAction(int priority = 0, string spellName = null) : base(priority, spellName, 5)
        {
        }
    }
}

