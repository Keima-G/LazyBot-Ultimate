namespace LazyEvo.LGrindEngine.NpcClasses
{
    using LazyLib.Wow;
    using System;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Xml;

    internal class Trainer : Npc
    {
        public Trainer()
        {
        }

        public Trainer(string name, Location spot, string @class)
        {
            this.Name = name;
            this.Spot = spot;
            this.Class = @class;
        }

        public override void Load(XmlNode xml)
        {
            foreach (XmlNode node in xml.ChildNodes)
            {
                if (node.Name.Equals("Name"))
                {
                    this.Name = node.InnerText;
                }
                if (node.Name.Equals("Spot"))
                {
                    string innerText = node.InnerText;
                    string str2 = innerText;
                    if (Convert.ToString(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator) != ".")
                    {
                        str2 = innerText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
                    }
                    string[] strArray = str2.Split(new char[] { ' ' });
                    if (strArray.Length > 2)
                    {
                        this.Spot = new Location((float) Convert.ToDouble(strArray[0]), (float) Convert.ToDouble(strArray[1]), (float) Convert.ToDouble(strArray[2]));
                    }
                }
                if (node.Name.Equals("Class"))
                {
                    this.Class = node.InnerText;
                }
            }
        }

        public override string Save()
        {
            try
            {
                string str = "<Trainer>" + "<Name>" + this.Name + "</Name>";
                object[] objArray = new object[] { this.Spot.X, " ", this.Spot.Y, " ", this.Spot.Z };
                string str2 = string.Concat(objArray);
                string str3 = (Convert.ToString(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator) != ".") ? str2.Replace(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator, ".") : str2;
                return (((str + "<Spot>" + str3 + "</Spot>") + "<Class>" + this.Class + "</Class>") + "</Trainer>");
            }
            catch
            {
                return "";
            }
        }

        public string Name { get; set; }

        public Location Spot { get; set; }

        public string Class { get; set; }
    }
}

