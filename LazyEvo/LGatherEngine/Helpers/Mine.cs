namespace LazyEvo.LGatherEngine.Helpers
{
    using LazyEvo.LGatherEngine;
    using LazyLib;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Xml;

    internal static class Mine
    {
        private static List<string> _mine;
        private static string _mineXmlPath;

        public static void AddMine(string name)
        {
            _mine.Add(name);
        }

        public static void Clear()
        {
            _mine.Clear();
        }

        public static List<string> GetList() => 
            _mine;

        public static List<string> Load()
        {
            string str;
            _mine = new List<string>();
            switch (LazySettings.Language)
            {
                case LazySettings.LazyLanguage.Russian:
                    str = "ru";
                    break;

                case LazySettings.LazyLanguage.German:
                    str = "de";
                    break;

                case LazySettings.LazyLanguage.French:
                    str = "fr";
                    break;

                case LazySettings.LazyLanguage.Spanish:
                    str = "es";
                    break;

                default:
                    str = "en";
                    break;
            }
            _mineXmlPath = $"{GatherEngine.OurDirectory}\Collect\Mine_{str}.xml";
            try
            {
                if (!File.Exists(_mineXmlPath))
                {
                    object[] args = new object[] { _mineXmlPath };
                    Logging.Write(LogType.Warning, "Could not find the file {0}", args);
                }
                else
                {
                    XmlDocument document = new XmlDocument();
                    document.Load(_mineXmlPath);
                    try
                    {
                        XmlNodeList elementsByTagName = document.GetElementsByTagName("Mine");
                        _mine.AddRange(from her in elementsByTagName.Cast<XmlNode>() select her.ChildNodes[0].Value);
                    }
                    catch (Exception exception)
                    {
                        Logging.Write(LogType.Warning, "Error loading list with mines: " + exception, new object[0]);
                    }
                }
            }
            catch (Exception exception2)
            {
                Logging.Write(LogType.Warning, "Error loading mines list: " + exception2, new object[0]);
            }
            return _mine;
        }

        public static void Save()
        {
            string str = "<?xml version=\"1.0\"?>" + "<MineList>";
            foreach (string str2 in _mine)
            {
                str = str + "<Mine>" + str2 + "</Mine>";
            }
            XmlDocument document = new XmlDocument();
            document.LoadXml(str + "</MineList>");
            document.Save(_mineXmlPath);
        }
    }
}

