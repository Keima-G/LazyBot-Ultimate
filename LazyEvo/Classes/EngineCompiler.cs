namespace LazyEvo.Classes
{
    using LazyEvo.Forms.Helpers;
    using LazyEvo.LGatherEngine;
    using LazyEvo.LGrindEngine;
    using LazyLib;
    using LazyLib.IEngine;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;

    internal class EngineCompiler
    {
        internal static Dictionary<string, ILazyEngine> Assemblys = new Dictionary<string, ILazyEngine>();

        internal static void RecompileAll()
        {
            Assemblys.Clear();
            GrindingEngine engine = new GrindingEngine();
            Assemblys.Add(engine.Name, engine);
            GatherEngine engine2 = new GatherEngine();
            Assemblys.Add(engine2.Name, engine2);
            try
            {
                if (Directory.Exists(LazyForms.OurDirectory + @"\Engines"))
                {
                    foreach (FileInfo info2 in new DirectoryInfo(LazyForms.OurDirectory + @"\Engines").GetFiles())
                    {
                        if (info2.Extension.ToLower() == ".dll")
                        {
                            try
                            {
                                Assembly assembly = Assembly.LoadFrom(info2.FullName);
                                if (assembly != null)
                                {
                                    string key = info2.Name.Replace(info2.Extension, string.Empty);
                                    if (Assemblys.ContainsKey(key))
                                    {
                                        Assemblys.Remove(key);
                                    }
                                    foreach (Type type in assembly.GetTypes())
                                    {
                                        if ((type.GetInterface("ILazyEngine") != null) && type.IsClass)
                                        {
                                            Assemblys.Add(key, (ILazyEngine) Activator.CreateInstance(type));
                                            Logging.Write("[ECompiler] Loaded: " + key, new object[0]);
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
            }
            catch (Exception exception2)
            {
                Logging.Write("[Compiler] Exception: " + exception2, new object[0]);
            }
        }
    }
}

