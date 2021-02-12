namespace LazyEvo.Other
{
    using CSScriptLibrary;
    using LazyLib;
    using System;
    using System.Text;
    using System.Threading;

    internal class ScriptRunner
    {
        private static string GetCode(string code)
        {
            if (code.ToUpper().Contains("using".ToUpper()))
            {
                throw new Exception("You are not allowed to use 'using' in your scripts");
            }
            if (code.ToUpper().Contains("System.".ToUpper()))
            {
                throw new Exception("You are not allowed to use 'System.' in your scripts");
            }
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("using LazyLib.Helpers;");
            builder.AppendLine("using LazyLib.Wow;");
            builder.AppendLine("using LazyLib.Combat;");
            builder.AppendLine("using LazyLib.ActionBar;");
            builder.AppendLine("using LazyEvo.Public;");
            builder.AppendLine("using System.Threading;");
            builder.AppendLine("using System.Text;");
            builder.AppendLine("private static PPlayerSelf Player = ObjectManager.MyPlayer;");
            builder.AppendLine("private static PUnit Target = ObjectManager.MyPlayer.Target;");
            builder.AppendLine("private static void CastSpell(string spell)");
            builder.AppendLine("{");
            builder.AppendLine("BarMapper.CastSpell(spell);");
            builder.AppendLine("}");
            builder.AppendLine("private static bool IsSpellReadyByName(string spell)");
            builder.AppendLine("{");
            builder.AppendLine("return BarMapper.IsSpellReadyByName(spell);");
            builder.AppendLine("}");
            code = builder + code;
            return code;
        }

        public static void RunCode(string name, string code)
        {
            if (!string.IsNullOrEmpty(code))
            {
                try
                {
                    code = GetCode(code);
                    new AsmHelper(CSScript.LoadMethod(code, new string[0])).Invoke("*.Run", new object[0]);
                }
                catch (ThreadAbortException)
                {
                }
                catch (Exception exception1)
                {
                    object[] args = new object[] { name, exception1.Message };
                    Logging.Debug(LogType.Error, "Error running script: {0}:{1} ", args);
                }
            }
        }

        public static bool ShouldRun(string name, string code)
        {
            if (!string.IsNullOrEmpty(code))
            {
                try
                {
                    code = GetCode(code);
                    return (bool) new AsmHelper(CSScript.LoadMethod(code, new string[0])).Invoke("*.ShouldRun", new object[0]);
                }
                catch (ThreadAbortException)
                {
                }
                catch (Exception exception1)
                {
                    object[] args = new object[] { name, exception1.Message };
                    Logging.Debug(LogType.Error, "Error running script (ShouldRun): {0}:{1} ", args);
                }
            }
            return false;
        }
    }
}

