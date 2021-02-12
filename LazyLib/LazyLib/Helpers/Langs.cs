namespace LazyLib.Helpers
{
    using LazyLib;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Xml;

    [Obfuscation(Feature="renaming", ApplyToMembers=true)]
    public class Langs
    {
        private static List<string> _bagsFull;
        private static List<string> _skillToLow;
        private static List<string> _druidFlying;
        private static List<string> _mountCantMount;
        private static List<string> _cantDoThatWhileMoving;
        private static XmlDocument _xmlDoc;
        private static List<string> _trainingDummy;
        private static List<string> _youHaveBeen;

        public static bool BagsFull(string text) => 
            Enumerable.Any<string>(_bagsFull, new Func<string, bool>(text.ToUpper().Contains));

        public static bool CantDoThatWhileMoving(string text) => 
            Enumerable.Any<string>(_cantDoThatWhileMoving, new Func<string, bool>(text.ToUpper().Contains));

        public static bool DruidFlying(string text) => 
            Enumerable.Any<string>(_druidFlying, new Func<string, bool>(text.ToUpper().Contains));

        public static void Load()
        {
            _bagsFull = new List<string>();
            _skillToLow = new List<string>();
            _druidFlying = new List<string>();
            _mountCantMount = new List<string>();
            _cantDoThatWhileMoving = new List<string>();
            _trainingDummy = new List<string>();
            _youHaveBeen = new List<string>();
            try
            {
                _xmlDoc = new XmlDocument();
                _xmlDoc.Load(LazySettings.OurDirectory + @"\Langs.xml");
            }
            catch (Exception)
            {
                Logging.Write("Could not load Langs.xml, check if the file is corrupted", new object[0]);
                return;
            }
            try
            {
                LoadLang("en");
                LoadLang("de");
                LoadLang("fr");
                LoadLang("ru");
                LoadLang("pt");
            }
            catch (Exception)
            {
                Logging.Write("Could not load Langs.xml (Load langs), check if the file is corrupted", new object[0]);
            }
        }

        private static void LoadLang(string lang)
        {
            foreach (XmlNode node2 in _xmlDoc.GetElementsByTagName(lang)[0].ChildNodes)
            {
                if (node2.Name.Equals("BagsFull"))
                {
                    _bagsFull.Add(node2.InnerText.ToUpper());
                }
                if (node2.Name.Equals("SkillToLow"))
                {
                    _skillToLow.Add(node2.InnerText.ToUpper());
                }
                if (node2.Name.Equals("DruidFlying"))
                {
                    _druidFlying.Add(node2.InnerText.ToUpper());
                }
                if (node2.Name.Equals("ShapeShift"))
                {
                    _druidFlying.Add(node2.InnerText.ToUpper());
                }
                if (node2.Name.Equals("CantMount"))
                {
                    _mountCantMount.Add(node2.InnerText.ToUpper());
                }
                if (node2.Name.Equals("WhileMoving"))
                {
                    _cantDoThatWhileMoving.Add(node2.InnerText.ToUpper());
                }
                if (node2.Name.Equals("TrainingDummy"))
                {
                    _trainingDummy.Add(node2.InnerText.ToUpper());
                }
                if (node2.Name.Equals("YouHaveBeen"))
                {
                    _youHaveBeen.Add(node2.InnerText.ToUpper());
                }
            }
        }

        public static bool MountCantMount(string text) => 
            Enumerable.Any<string>(_mountCantMount, new Func<string, bool>(text.ToUpper().Contains));

        public static bool SkillToLow(string text) => 
            Enumerable.Any<string>(_skillToLow, new Func<string, bool>(text.ToUpper().Contains));

        public static bool TrainingDummy(string text) => 
            Enumerable.Any<string>(_trainingDummy, new Func<string, bool>(text.ToUpper().Contains));

        public static bool YouhaveBeen(string text) => 
            Enumerable.Any<string>(_youHaveBeen, new Func<string, bool>(text.ToUpper().Contains));
    }
}

