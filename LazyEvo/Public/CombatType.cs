namespace LazyEvo.Public
{
    using System;
    using System.Reflection;

    [Obfuscation(Feature="renaming", ApplyToMembers=true)]
    public enum CombatType
    {
        CombatStarted,
        CombatDone
    }
}

