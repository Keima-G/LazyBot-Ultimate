namespace LazyEvo.LGrindEngine
{
    using LazyLib;
    using LazyLib.Wow;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    internal class PlayerData
    {
        private bool _changed;
        private Dictionary<string, string> _dic = new Dictionary<string, string>();

        public string Get(string key)
        {
            string str;
            return (!this._dic.TryGetValue(key, out str) ? null : str);
        }

        public List<string> GetKeysContaining(string str) => 
            (from key in this._dic.Keys
                where key.Contains(str)
                select key).ToList<string>();

        public void Load()
        {
            string name = LazyLib.Wow.ObjectManager.MyPlayer.Name;
            lock (this)
            {
                if (name != null)
                {
                    this._dic = new Dictionary<string, string>();
                    try
                    {
                        if (!Directory.Exists(GrindingEngine.OurDirectory + @"\PlayerData\"))
                        {
                            Directory.CreateDirectory(GrindingEngine.OurDirectory + @"\PlayerData\");
                        }
                        TextReader reader = File.OpenText(GrindingEngine.OurDirectory + @"\PlayerData\" + LazyLib.Wow.ObjectManager.MyPlayer.Name + ".txt");
                        int num = 0;
                        while (true)
                        {
                            string str3 = reader.ReadLine();
                            if (str3 == null)
                            {
                                Logging.Write("Found player data: " + num, new object[0]);
                                reader.Close();
                                break;
                            }
                            char[] separator = new char[] { '@' };
                            string[] strArray = str3.Split(separator);
                            if (strArray.Length == 2)
                            {
                                string key = strArray[0];
                                this.Set(key, strArray[1]);
                                num++;
                            }
                        }
                    }
                    catch (Exception)
                    {
                    }
                    this._changed = false;
                }
            }
        }

        public void Save()
        {
            lock (this)
            {
                if (this._changed)
                {
                    if (!Directory.Exists(GrindingEngine.OurDirectory + @"\PlayerData\"))
                    {
                        Directory.CreateDirectory(GrindingEngine.OurDirectory + @"\PlayerData\");
                    }
                    string path = GrindingEngine.OurDirectory + @"\PlayerData\" + LazyLib.Wow.ObjectManager.MyPlayer.Name + ".txt";
                    try
                    {
                        TextWriter writer = File.CreateText(path);
                        foreach (string str2 in this._dic.Keys)
                        {
                            writer.WriteLine(str2 + "@" + this._dic[str2]);
                        }
                        writer.Close();
                    }
                    catch (Exception exception)
                    {
                        Logging.Debug(LogType.Warning, "Could not write player data: " + exception, new object[0]);
                    }
                    this._changed = false;
                }
            }
        }

        public void Set(string key, string name)
        {
            if (this._dic.ContainsKey(key))
            {
                this._dic.Remove(key);
            }
            this._dic.Add(key, name);
            this._changed = true;
        }
    }
}

