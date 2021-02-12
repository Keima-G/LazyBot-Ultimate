namespace LazyLib.Helpers
{
    using LazyLib;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Reflection;
    using System.Xml;

    [Obfuscation(Feature="renaming", ApplyToMembers=true)]
    public class WowHeadData
    {
        internal static string GetWebResponse(double id)
        {
            try
            {
                Uri uri;
                string str = "item=" + id + "&xml";
                switch (LazySettings.Language)
                {
                    case LazySettings.LazyLanguage.Russian:
                        uri = new Uri("http://ru.wowhead.com/" + str);
                        break;

                    case LazySettings.LazyLanguage.German:
                        uri = new Uri("http://de.wowhead.com/" + str);
                        break;

                    case LazySettings.LazyLanguage.French:
                        uri = new Uri("http://fr.wowhead.com/" + str);
                        break;

                    case LazySettings.LazyLanguage.Spanish:
                        uri = new Uri("http://es.wowhead.com/" + str);
                        break;

                    default:
                        uri = new Uri("http://www.wowhead.com/" + str);
                        break;
                }
                HttpWebRequest request = (HttpWebRequest) WebRequest.Create(uri);
                request.Method = "GET";
                HttpWebResponse response = (HttpWebResponse) request.GetResponse();
                string str2 = new StreamReader(response.GetResponseStream()).ReadToEnd();
                response.Close();
                return str2;
            }
            catch
            {
                return null;
            }
        }

        internal static string GetWebResponseSpell(double id)
        {
            try
            {
                string str = "spell=" + id + "&xml";
                HttpWebRequest request = (HttpWebRequest) WebRequest.Create(new Uri("http://wotlk.openwow.com/" + str));
                request.Method = "GET";
                HttpWebResponse response = (HttpWebResponse) request.GetResponse();
                string str2 = new StreamReader(response.GetResponseStream()).ReadToEnd();
                response.Close();
                return str2;
            }
            catch
            {
                return null;
            }
        }

        public static Dictionary<string, string> GetWowHeadItem(double id)
        {
            string webResponse = GetWebResponse(id);
            if (webResponse == null)
            {
                return null;
            }
            if (webResponse.Equals("<?xml version=\"1.0\" encoding=\"UTF-8\"?><wowhead><error>Item not found!</error></wowhead>"))
            {
                webResponse = GetWebResponse(id);
            }
            if (webResponse == null)
            {
                return null;
            }
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            try
            {
                dictionary = ProcessWhData(webResponse);
            }
            catch (Exception)
            {
                return null;
            }
            return dictionary;
        }

        public static string GetWowHeadSpell(double id)
        {
            string str2;
            string webResponseSpell = GetWebResponseSpell(id);
            if (webResponseSpell == null)
            {
                return null;
            }
            if (webResponseSpell.Equals("<?xml version=\"1.0\" encoding=\"UTF-8\"?><wowhead><error>Item not found!</error></wowhead>"))
            {
                webResponseSpell = GetWebResponseSpell(id);
            }
            if (webResponseSpell == null)
            {
                return null;
            }
            try
            {
                int startIndex = webResponseSpell.IndexOf("<title>") + 7;
                str2 = webResponseSpell.Substring(startIndex, webResponseSpell.IndexOf("- Spell", startIndex) - startIndex);
            }
            catch (Exception)
            {
                return null;
            }
            return str2;
        }

        private static Dictionary<string, string> ProcessWhData(string data)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            string innerText = string.Empty;
            string innerText = string.Empty;
            data = data.Replace("<?xml version=\"1.0\" encoding=\"UTF-8\"?>", "");
            XmlDocument document = new XmlDocument();
            document.LoadXml(data);
            XmlNode node = document.SelectSingleNode("wowhead");
            if (node != null)
            {
                foreach (XmlNode node2 in node.ChildNodes)
                {
                    if (node2.Name == "item")
                    {
                        foreach (XmlNode node3 in node2.ChildNodes)
                        {
                            if (node3.Name == "name")
                            {
                                innerText = node3.InnerText;
                            }
                            if (node3.Name == "quality")
                            {
                                innerText = node3.InnerText;
                            }
                        }
                    }
                }
            }
            dictionary.Add("name", innerText);
            dictionary.Add("quality", innerText);
            return dictionary;
        }
    }
}

