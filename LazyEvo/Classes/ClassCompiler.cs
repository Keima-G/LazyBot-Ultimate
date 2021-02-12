namespace LazyEvo.Classes
{
    using LazyEvo.Forms.Helpers;
    using LazyEvo.PVEBehavior;
    using LazyLib;
    using LazyLib.Combat;
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;

    internal class ClassCompiler
    {
        internal static Dictionary<string, CombatEngine> Assemblys = new Dictionary<string, CombatEngine>();

        internal static Assembly CompileFile(string path)
        {
            FileInfo info = new FileInfo(path);
            CodeDomProvider provider = CodeDomProvider.CreateProvider("CSharp");
            CompilerParameters options = new CompilerParameters();
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                options.ReferencedAssemblies.Add(assembly.Location);
            }
            string[] fileNames = new string[] { path };
            CompilerResults results = provider.CompileAssemblyFromFile(options, fileNames);
            if (results.Errors.Count <= 0)
            {
                return results.CompiledAssembly;
            }
            Logging.Write("[Compiler] Error when compiling " + info.Name + " :", new object[0]);
            foreach (CompilerError error in results.Errors)
            {
                Logging.Write(string.Concat(new object[] { "Line number ", error.Line, ", Error Number: ", error.ErrorNumber, ", '", error.ErrorText }), new object[0]);
            }
            return null;
        }

        internal static void RecompileAll()
        {
            Assemblys.Clear();
            PVEBehaviorCombat combat = new PVEBehaviorCombat();
            Assemblys.Add(combat.Name, combat);
            try
            {
                if (Directory.Exists(LazyForms.OurDirectory + @"\Classes"))
                {
                    foreach (FileInfo info2 in new DirectoryInfo(LazyForms.OurDirectory + @"\Classes").GetFiles())
                    {
                        if (info2.Extension.ToLower() == ".cs")
                        {
                            Assembly assembly = CompileFile(info2.FullName);
                            if (assembly != null)
                            {
                                string key = info2.Name.Replace(info2.Extension, string.Empty);
                                if (Assemblys.ContainsKey(key))
                                {
                                    Assemblys.Remove(key);
                                }
                                foreach (Type type in assembly.GetTypes())
                                {
                                    if (type.IsSubclassOf(typeof(CombatEngine)) && type.IsClass)
                                    {
                                        Assemblys.Add(key, (CombatEngine) Activator.CreateInstance(type));
                                        Logging.Write("[Compiler] Compiled: " + key, new object[0]);
                                    }
                                }
                            }
                        }
                        if (info2.Extension.ToLower() == ".dll")
                        {
                            Assembly assembly2 = Assembly.LoadFrom(info2.FullName);
                            if (assembly2 != null)
                            {
                                string key = info2.Name.Replace(info2.Extension, string.Empty);
                                if (Assemblys.ContainsKey(key))
                                {
                                    Assemblys.Remove(key);
                                }
                                foreach (Type type2 in assembly2.GetTypes())
                                {
                                    if (type2.IsSubclassOf(typeof(CombatEngine)) && type2.IsClass)
                                    {
                                        Assemblys.Add(key, (CombatEngine) Activator.CreateInstance(type2));
                                        Logging.Write("[Compiler] Loaded: " + key, new object[0]);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                Logging.Write("[Compiler] Exception: " + exception, new object[0]);
            }
        }
    }
}

