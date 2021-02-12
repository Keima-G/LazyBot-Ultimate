namespace LazyEvo.Classes
{
    using LazyEvo.Forms.Helpers;
    using LazyEvo.LGrindEngine.Helpers;
    using LazyEvo.Plugins.LazyData;
    using LazyLib;
    using LazyLib.Helpers;
    using LazyLib.IPlugin;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;

    internal class PluginCompiler
    {
        internal static Dictionary<string, ILazyPlugin> Assemblys = new Dictionary<string, ILazyPlugin>();
        public static List<string> LoadedPlugins = new List<string>();

        private static bool LoadPluginSettings(string name)
        {
            bool flag;
            try
            {
                flag = new IniManager(LazyForms.OurDirectory + @"\Settings\lazy_plugins.ini").GetBoolean("Plugins", name, false);
            }
            catch
            {
                return false;
            }
            return flag;
        }

        public static void PluginLoad(string assemblyName)
        {
            Assemblys[assemblyName].PluginLoad();
            LoadedPlugins.Add(assemblyName);
        }

        public static void PluginUnload(string assemblyName)
        {
            Assemblys[assemblyName].PluginUnload();
            LoadedPlugins.Remove(assemblyName);
        }

        internal static void RecompileAll()
        {
            foreach (KeyValuePair<string, ILazyPlugin> pair in Assemblys)
            {
                pair.Value.PluginUnload();
            }
            Assemblys.Clear();
            Converter converter = new Converter();
            Assemblys.Add("Converter", converter);
            Loader loader = new Loader();
            Assemblys.Add("Lazy Data", loader);
            try
            {
                if (Directory.Exists(LazyForms.OurDirectory + @"\Plugins"))
                {
                    foreach (FileInfo info2 in new DirectoryInfo(LazyForms.OurDirectory + @"\Plugins").GetFiles())
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
                                        if ((type.GetInterface("ILazyPlugin") != null) && type.IsClass)
                                        {
                                            Assemblys.Add(key, (ILazyPlugin) Activator.CreateInstance(type));
                                            Logging.Write("[PCompiler] Loaded: " + key, new object[0]);
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

        public static void StartSavedPlugins()
        {
            foreach (KeyValuePair<string, ILazyPlugin> pair in Assemblys)
            {
                if (LoadPluginSettings(pair.Key))
                {
                    if (LoadedPlugins.Contains(pair.Value.GetName()))
                    {
                        continue;
                    }
                    PluginLoad(pair.Key);
                    continue;
                }
                if (LoadedPlugins.Contains(pair.Key))
                {
                    PluginUnload(pair.Key);
                }
            }
        }
    }
}

