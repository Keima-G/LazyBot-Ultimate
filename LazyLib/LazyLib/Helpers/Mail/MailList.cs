namespace LazyLib.Helpers.Mail
{
    using LazyLib;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Xml;

    public static class MailList
    {
        private static List<string> _mailList = new List<string>();

        public static void AddMail(string name)
        {
            _mailList.Add(name);
        }

        public static void Clear()
        {
            _mailList.Clear();
        }

        public static void Load()
        {
            _mailList = new List<string>();
            try
            {
                if (!File.Exists(LazySettings.OurDirectory + @"\Settings\MailList.xml"))
                {
                    Logging.Write("Could not find the file MailList.xml will not mail anything", new object[0]);
                }
                else
                {
                    XmlDocument document = new XmlDocument();
                    document.Load(LazySettings.OurDirectory + @"\Settings\MailList.xml");
                    try
                    {
                        XmlNodeList elementsByTagName = document.GetElementsByTagName("Mail");
                        _mailList.AddRange(from mail in elementsByTagName.Cast<XmlNode>() select mail.ChildNodes[0].Value);
                    }
                    catch (Exception exception)
                    {
                        Logging.Write("Error loading MailList: " + exception, new object[0]);
                    }
                }
            }
            catch (Exception exception2)
            {
                Logging.Write("Error loading mail list: " + exception2, new object[0]);
            }
        }

        public static void Save()
        {
            string str = "<?xml version=\"1.0\"?>" + "<MailList>";
            foreach (string str2 in _mailList)
            {
                str = str + "<Mail>" + str2 + "</Mail>";
            }
            XmlDocument document = new XmlDocument();
            document.LoadXml(str + "</MailList>");
            if (Directory.Exists(LazySettings.OurDirectory + @"\Settings\"))
            {
                Directory.CreateDirectory(LazySettings.OurDirectory + @"\Settings\");
            }
            document.Save(LazySettings.OurDirectory + @"\Settings\MailList.xml");
        }

        public static bool ShouldMail(string name) => 
            _mailList.Contains(name);

        public static List<string> GetList =>
            _mailList;
    }
}

