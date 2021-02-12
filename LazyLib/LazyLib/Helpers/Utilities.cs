namespace LazyLib.Helpers
{
    using System;
    using System.IO;
    using System.Reflection;

    [Obfuscation(Feature="renaming", ApplyToMembers=true)]
    public class Utilities
    {
        public static string ApplicationPath =>
            Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
    }
}

