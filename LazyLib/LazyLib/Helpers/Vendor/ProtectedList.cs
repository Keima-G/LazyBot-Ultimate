namespace LazyLib.Helpers.Vendor
{
    using LazyLib;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Xml;

    public static class ProtectedList
    {
        private static List<string> _protectedList = new List<string>();

        public static void AddProtected(string name)
        {
            _protectedList.Add(name);
        }

        public static void Clear()
        {
            _protectedList.Clear();
        }

        public static void Load()
        {
            _protectedList = new List<string>();
            try
            {
                if (!File.Exists(LazySettings.OurDirectory + @"\Settings\ProtectedList.xml"))
                {
                    Logging.Write("Could not find the file ProtectedList.xml will not mail anything", new object[0]);
                }
                else
                {
                    XmlDocument document = new XmlDocument();
                    document.Load(LazySettings.OurDirectory + @"\Settings\ProtectedList.xml");
                    try
                    {
                        XmlNodeList elementsByTagName = document.GetElementsByTagName("Protected");
                        _protectedList.AddRange(from mail in elementsByTagName.Cast<XmlNode>() select mail.ChildNodes[0].Value);
                    }
                    catch (Exception exception)
                    {
                        Logging.Write("Error loading ProtectedList: " + exception, new object[0]);
                    }
                }
            }
            catch (Exception exception2)
            {
                Logging.Write("Error loading ProtectedList list: " + exception2, new object[0]);
            }
        }

        public static void Save()
        {
            string str = "<?xml version=\"1.0\"?>" + "<ProtectedList>";
            foreach (string str2 in _protectedList)
            {
                str = str + "<Protected>" + str2 + "</Protected>";
            }
            XmlDocument document = new XmlDocument();
            document.LoadXml(str + "</ProtectedList>");
            if (Directory.Exists(LazySettings.OurDirectory + @"\Settings\"))
            {
                Directory.CreateDirectory(LazySettings.OurDirectory + @"\Settings\");
            }
            document.Save(LazySettings.OurDirectory + @"\Settings\ProtectedList.xml");
        }

        public static bool ShouldVendor(string name) => 
            !_protectedList.Contains(name);

        public static List<string> GetList =>
            _protectedList;
    }
}

