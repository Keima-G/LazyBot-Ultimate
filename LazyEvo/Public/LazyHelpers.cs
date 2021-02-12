namespace LazyEvo.Public
{
    using LazyEvo.Forms;
    using LazyEvo.Forms.Helpers;
    using LazyEvo.LGatherEngine;
    using LazyEvo.LGrindEngine;
    using LazyLib;
    using System;
    using System.Reflection;

    [Obfuscation(Feature="renaming", ApplyToMembers=true)]
    public class LazyHelpers
    {
        public static bool LoadGatherProfile(string path) => 
            (Main.EngineHandler is GatherEngine) && ((GatherEngine) Main.EngineHandler).LoadProfile(path);

        public static bool LoadGrindingProfile(string path) => 
            (Main.EngineHandler is GrindingEngine) && ((GrindingEngine) Main.EngineHandler).LoadProfile(path);

        public static void StartBotting()
        {
            LazyForms.MainForm.StartBotting();
        }

        public static void StopAll(string reason)
        {
            LazyForms.MainForm.StopBotting(reason, false);
        }

        public static void StopAll(string reason, bool userStoppedIt)
        {
            LazyForms.MainForm.StopBotting(reason, userStoppedIt);
        }

        public static void StopAllError(string reason, bool somethingWentWrong)
        {
            LazyForms.MainForm.StopBottingError(reason, somethingWentWrong);
        }

        public static void Write(string format, params object[] args)
        {
            Logging.Write(format, args);
        }
    }
}

