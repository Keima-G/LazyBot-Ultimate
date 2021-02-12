namespace LazyLib.Wow
{
    using System;
    using System.Reflection;

    [Obfuscation(Feature="renaming", ApplyToMembers=true)]
    public class PublicPointers
    {
        public enum Globals
        {
            PlayerName = 0x879d18
        }

        public enum InGame
        {
            InGame = 0x7d0792
        }
    }
}

