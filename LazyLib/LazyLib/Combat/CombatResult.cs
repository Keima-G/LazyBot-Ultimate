namespace LazyLib.Combat
{
    using System;
    using System.Reflection;

    [Obfuscation(Feature="renaming", ApplyToMembers=false)]
    public enum CombatResult
    {
        Unknown = 1,
        Success = 2,
        Bugged = 3,
        SuccessWithAdd = 4,
        Died = 5,
        OtherPlayerTag = 7,
        Pet = 8,
        Failed = 9
    }
}

