namespace LazyEvo.Public
{
    using LazyEvo.Forms.Helpers;
    using System;
    using System.Reflection;

    [Obfuscation(Feature="renaming", ApplyToMembers=true)]
    public class LazyForm
    {
        public static int Deaths;
        public static int Loots;
        public static int Kills;
        public static string TimeToLevel;
        public static string LPH;
        public static int Harvests;
        public static string Engine;

        public static void UpdateStatsText(string text)
        {
            LazyForms.MainForm.UpdateStatsText(text);
        }
    }
}

