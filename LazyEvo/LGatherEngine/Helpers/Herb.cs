namespace LazyEvo.LGatherEngine.Helpers
{
    using LazyEvo.LGatherEngine;
    using LazyLib;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Xml;

    internal class Herb
    {
        private static List<string> _herb;
        private static string _herbXmlPath;

        public static void AddHerb(string name)
        {
            _herb.Add(name);
        }

        public static void Clear()
        {
            _herb.Clear();
        }

        public static List<string> GetList() => 
            _herb;

        public static void Load()
        {
            string str;
            _herb = new List<string>();
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
            _herbXmlPath = $"{GatherEngine.OurDirectory}\Collect\Herb_{str}.xml";
            try
            {
                if (!File.Exists(_herbXmlPath))
                {
                    object[] args = new object[] { _herbXmlPath };
                    Logging.Write(LogType.Warning, "Could not find the file {0}", args);
                }
                else
                {
                    XmlDocument document = new XmlDocument();
                    document.Load(_herbXmlPath);
                    try
                    {
                        XmlNodeList elementsByTagName = document.GetElementsByTagName("Herb");
                        _herb.AddRange(from her in elementsByTagName.Cast<XmlNode>() select her.ChildNodes[0].Value);
                    }
                    catch (Exception exception)
                    {
                        Logging.Write(LogType.Warning, "Error loading list with herbs: " + exception, new object[0]);
                    }
                }
            }
            catch (Exception exception2)
            {
                Logging.Write(LogType.Warning, "Error loading herb list: " + exception2, new object[0]);
            }
        }

        public static void Save()
        {
            string str = "<?xml version=\"1.0\"?>" + "<HerbList>";
            foreach (string str2 in _herb)
            {
                str = str + "<Herb>" + str2 + "</Herb>";
            }
            XmlDocument document = new XmlDocument();
            document.LoadXml(str + "</HerbList>");
            document.Save(_herbXmlPath);
        }
    }
}

