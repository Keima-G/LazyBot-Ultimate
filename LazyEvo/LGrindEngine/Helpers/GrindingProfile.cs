namespace LazyEvo.LGrindEngine.Helpers
{
    using LazyLib;
    using LazyLib.Wow;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Xml;

    internal class GrindingProfile
    {
        public List<uint> Factions = new List<uint>();
        public List<string> Ignore = new List<string>();
        public bool NaturalRun = true;
        public bool Reverse;
        public int RoamDistance = 0x23;
        public int TargetMaxLevel = 200;
        public int TargetMinLevel;
        private XmlDocument _doc;
        private List<Location> _ghostWaypoints = new List<Location>();
        private List<Location> _toTownWaypoints = new List<Location>();
        private List<Location> _waypoints = new List<Location>();

        public void AddSingleGhostWayPoint(Location position)
        {
            this._ghostWaypoints.Add(position);
        }

        public void AddSingleToTownWayPoint(Location position)
        {
            this._toTownWaypoints.Add(position);
        }

        public void AddSingleWayPoint(Location position)
        {
            this._waypoints.Add(position);
        }

        public List<Location> GetListSortedAfterDistance(Location location) => 
            this.GetListSortedAfterDistance(location, this._waypoints);

        public List<Location> GetListSortedAfterDistance(Location location, List<Location> waypoints)
        {
            List<Location> list = new List<Location>();
            if (waypoints.Count != 0)
            {
                Location location2 = waypoints[Location.GetClosestPositionInList(this._toTownWaypoints, LazyLib.Wow.ObjectManager.MyPlayer.Location)];
                int num = 0;
                for (int i = 0; i < waypoints.Count; i++)
                {
                    if (location2 == waypoints[i])
                    {
                        num = i;
                    }
                }
                for (int j = num; j < waypoints.Count; j++)
                {
                    list.Add(waypoints[j]);
                }
                for (int k = 0; k < num; k++)
                {
                    list.Add(waypoints[k]);
                }
            }
            return list;
        }

        public void LoadDefault()
        {
            this._waypoints = new List<Location>();
            this._ghostWaypoints = new List<Location>();
            this._toTownWaypoints = new List<Location>();
            this.TargetMaxLevel = 0x63;
            this.RoamDistance = 0x23;
            this.NaturalRun = true;
        }

        public void LoadFile(string fileName)
        {
            try
            {
                this.Profile = fileName;
                this._doc = new XmlDocument();
                this._doc.Load(fileName);
            }
            catch (Exception exception)
            {
                Logging.Write("Error in loaded profile: " + exception, new object[0]);
            }
            this._waypoints = new List<Location>();
            this._ghostWaypoints = new List<Location>();
            try
            {
                XmlNodeList elementsByTagName = this._doc.GetElementsByTagName("MinLevel");
                this.TargetMinLevel = Convert.ToInt32(elementsByTagName[0].ChildNodes[0].Value);
                XmlNodeList list2 = this._doc.GetElementsByTagName("MaxLevel");
                this.TargetMaxLevel = Convert.ToInt32(list2[0].ChildNodes[0].Value);
            }
            catch
            {
            }
            try
            {
                XmlNodeList elementsByTagName = this._doc.GetElementsByTagName("VendorName");
                this.VendorName = elementsByTagName[0].ChildNodes[0].Value;
            }
            catch
            {
            }
            if (this.TargetMaxLevel == 0)
            {
                this.TargetMaxLevel = 0x63;
            }
            try
            {
                string[] strArray = this._doc.GetElementsByTagName("Factions")[0].InnerText.Split(new char[] { ' ' });
                CS$<>9__CachedAnonymousMethodDelegate4 ??= s => Convert.ToUInt32(s);
                this.Factions.AddRange(Enumerable.Select<string, uint>(from s in strArray
                    where s != ""
                    select s, CS$<>9__CachedAnonymousMethodDelegate4));
            }
            catch
            {
            }
            try
            {
                string[] strArray2 = this._doc.GetElementsByTagName("Ignore")[0].InnerText.Split(new char[] { '#' });
                this.Ignore.AddRange(from s in strArray2
                    where s != ""
                    select s);
            }
            catch
            {
            }
            try
            {
                XmlNodeList elementsByTagName = this._doc.GetElementsByTagName("RoamDistance");
                this.RoamDistance = Convert.ToInt32(elementsByTagName[0].ChildNodes[0].Value);
            }
            catch
            {
            }
            if (this.RoamDistance == 0f)
            {
                this.RoamDistance = 0x23;
            }
            try
            {
                XmlNodeList elementsByTagName = this._doc.GetElementsByTagName("NaturalRun");
                this.NaturalRun = Convert.ToBoolean(elementsByTagName[0].ChildNodes[0].Value);
            }
            catch
            {
            }
            try
            {
                XmlNodeList elementsByTagName = this._doc.GetElementsByTagName("GhostWaypoint");
                if (elementsByTagName.Count != 0)
                {
                    foreach (XmlNode node in elementsByTagName)
                    {
                        string str3 = node.ChildNodes[0].Value;
                        string str4 = str3;
                        if (Convert.ToString(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator) != ".")
                        {
                            str4 = str3.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
                        }
                        string[] strArray3 = str4.Split(new char[] { ' ' });
                        if (strArray3.Length > 2)
                        {
                            Location item = new Location((float) Convert.ToDouble(strArray3[0]), (float) Convert.ToDouble(strArray3[1]), (float) Convert.ToDouble(strArray3[2]));
                            this._ghostWaypoints.Add(item);
                        }
                        else
                        {
                            Location item = new Location((float) Convert.ToDouble(strArray3[0]), (float) Convert.ToDouble(strArray3[1]), (float) Convert.ToDouble(0));
                            this._ghostWaypoints.Add(item);
                        }
                    }
                }
            }
            catch (Exception exception2)
            {
                Logging.Write("Error in ghost waypoints: " + exception2, new object[0]);
            }
            try
            {
                foreach (XmlNode node2 in this._doc.GetElementsByTagName("Waypoint"))
                {
                    string str6 = node2.ChildNodes[0].Value;
                    string str7 = str6;
                    if (Convert.ToString(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator) != ".")
                    {
                        str7 = str6.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
                    }
                    string[] strArray4 = str7.Split(new char[] { ' ' });
                    if (strArray4.Length > 2)
                    {
                        Location item = new Location((float) Convert.ToDouble(strArray4[0]), (float) Convert.ToDouble(strArray4[1]), (float) Convert.ToDouble(strArray4[2]));
                        this._waypoints.Add(item);
                    }
                    else
                    {
                        Location item = new Location((float) Convert.ToDouble(strArray4[0]), (float) Convert.ToDouble(strArray4[1]), (float) Convert.ToDouble(0));
                        this._waypoints.Add(item);
                    }
                }
            }
            catch (Exception exception3)
            {
                Logging.Write("Error in loading waypoints " + exception3, new object[0]);
            }
            try
            {
                foreach (XmlNode node3 in this._doc.GetElementsByTagName("ToTown"))
                {
                    string str9 = node3.ChildNodes[0].Value;
                    string str10 = str9;
                    if (Convert.ToString(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator) != ".")
                    {
                        str10 = str9.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
                    }
                    string[] strArray5 = str10.Split(new char[] { ' ' });
                    if (strArray5.Length > 2)
                    {
                        Location item = new Location((float) Convert.ToDouble(strArray5[0]), (float) Convert.ToDouble(strArray5[1]), (float) Convert.ToDouble(strArray5[2]));
                        this._toTownWaypoints.Add(item);
                    }
                    else
                    {
                        Location item = new Location((float) Convert.ToDouble(strArray5[0]), (float) Convert.ToDouble(strArray5[1]), (float) Convert.ToDouble(0));
                        this._toTownWaypoints.Add(item);
                    }
                }
            }
            catch (Exception exception4)
            {
                Logging.Write("Error in loading waypoints " + exception4, new object[0]);
            }
        }

        public void SaveFile(string saveFile)
        {
            string str2;
            string str3;
            object[] objArray = new object[] { "<?xml version=\"1.0\"?>" + "<Profile>", "<MinLevel>", this.TargetMinLevel, "</MinLevel>" };
            object[] objArray2 = new object[] { string.Concat(objArray), "<MaxLevel>", this.TargetMaxLevel, "</MaxLevel>" };
            object[] objArray3 = new object[] { string.Concat(objArray2), "<RoamDistance>", this.RoamDistance, "</RoamDistance>" };
            string str = (string.Concat(new object[] { string.Concat(objArray3), "<NaturalRun>", this.NaturalRun, "</NaturalRun>" }) + "<VendorName>" + this.VendorName + "</VendorName>") + "<Factions>";
            int num = 0;
            foreach (uint num2 in this.Factions)
            {
                str = (num != 0) ? (str + " " + num2) : (str + num2);
                num++;
            }
            str = str + "</Factions>" + "<Ignore>";
            num = 0;
            foreach (string str4 in this.Ignore)
            {
                str = (num != 0) ? (str + "#" + str4) : (str + str4);
                num++;
            }
            str = str + "</Ignore>";
            foreach (Location location in this._waypoints)
            {
                object[] objArray5 = new object[] { location.X, " ", location.Y, " ", location.Z };
                str3 = string.Concat(objArray5);
                str2 = (Convert.ToString(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator) != ".") ? str3.Replace(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator, ".") : str3;
                str = str + "<Waypoint>" + str2 + "</Waypoint>";
            }
            foreach (Location location2 in this._toTownWaypoints)
            {
                object[] objArray6 = new object[] { location2.X, " ", location2.Y, " ", location2.Z };
                str3 = string.Concat(objArray6);
                str2 = (Convert.ToString(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator) != ".") ? str3.Replace(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator, ".") : str3;
                str = str + "<ToTown>" + str2 + "</ToTown>";
            }
            foreach (Location location3 in this._ghostWaypoints)
            {
                object[] objArray7 = new object[] { location3.X, " ", location3.Y, " ", location3.Z };
                str3 = string.Concat(objArray7);
                str2 = (Convert.ToString(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator) != ".") ? str3.Replace(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator, ".") : str3;
                str = str + "<GhostWaypoint>" + str2 + "</GhostWaypoint>";
            }
            XmlDocument document = new XmlDocument();
            document.LoadXml(str + "</Profile>");
            document.Save(saveFile);
        }

        public string Profile { get; private set; }

        public string VendorName { get; set; }

        public List<Location> WaypointsGhost =>
            this._ghostWaypoints;

        public List<Location> WaypointsNormal =>
            this._waypoints;

        public List<Location> WaypointsToTown =>
            this._toTownWaypoints;

        public int GetNearestNormalIndex =>
            Location.GetClosestPositionInList(this._waypoints, LazyLib.Wow.ObjectManager.MyPlayer.Location);

        public int GetNearestGhostIndex =>
            Location.GetClosestPositionInList(this._ghostWaypoints, LazyLib.Wow.ObjectManager.MyPlayer.Location);

        public int GetNearestToTownIndex =>
            Location.GetClosestPositionInList(this._toTownWaypoints, LazyLib.Wow.ObjectManager.MyPlayer.Location);
    }
}

