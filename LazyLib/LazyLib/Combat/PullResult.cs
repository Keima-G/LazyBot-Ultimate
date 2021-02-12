namespace LazyLib.Combat
{
    using System;
    using System.Reflection;

    [Obfuscation(Feature="renaming", ApplyToMembers=false)]
    public enum PullResult
    {
        Success = 1,
        CouldNotPull = 2
    }
}

