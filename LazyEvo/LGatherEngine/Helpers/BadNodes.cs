namespace LazyEvo.LGatherEngine.Helpers
{
    using LazyEvo.LGatherEngine;
    using LazyLib;
    using LazyLib.Wow;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Xml;

    internal static class BadNodes
    {
        private static readonly XmlDocument _doc = new XmlDocument();

        public static List<Location> GetBadNodeList()
        {
            List<Location> list = new List<Location>();
            if (!File.Exists(GatherEngine.OurDirectory + @"\badNodes.xml"))
            {
                return new List<Location>();
            }
            try
            {
                _doc.Load(GatherEngine.OurDirectory + @"\badNodes.xml");
            }
            catch (Exception exception)
            {
                Logging.Write("Could not load badNodes.xml did you add something invalid? Try opening the file in your browser " + exception, new object[0]);
            }
            XmlNodeList elementsByTagName = _doc.GetElementsByTagName("bad_location");
            try
            {
                foreach (XmlNode node in elementsByTagName)
                {
                    try
                    {
                        string innerText = node.ChildNodes[1].InnerText;
                        string loc = innerText;
                        if (Convert.ToString(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator) != ".")
                        {
                            loc = innerText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
                        }
                        list.Add(new Location(loc));
                    }
                    catch
                    {
                    }
                }
            }
            catch
            {
            }
            return list;
        }
    }
}

