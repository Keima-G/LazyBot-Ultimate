namespace LazyEvo.LGatherEngine.Helpers
{
    using LazyLib;
    using LazyLib.Wow;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Xml;

    internal class GatherProfile
    {
        private List<Location> _badNodes = new List<Location>();
        private XmlDocument _doc;
        private List<Location> _toTownWaypoints = new List<Location>();
        private List<Location> _waypoints = new List<Location>();

        public void AddBadNode(Location badNode)
        {
            if (!this._badNodes.Contains(badNode))
            {
                this._badNodes.Add(badNode);
            }
        }

        public void AddSingleToTownWayPoint(Location position)
        {
            this._toTownWaypoints.Add(position);
        }

        public void AddSingleWayPoint(Location position)
        {
            this._waypoints.Add(position);
        }

        private static Location GetCorrectLocation(string location)
        {
            string[] strArray = location.Split(new char[] { ' ' });
            return ((strArray.Length <= 2) ? new Location((float) Convert.ToDouble(strArray[0], CultureInfo.InvariantCulture), (float) Convert.ToDouble(strArray[1], CultureInfo.InvariantCulture), (float) Convert.ToDouble(0)) : new Location((float) Convert.ToDouble(strArray[0], CultureInfo.InvariantCulture), (float) Convert.ToDouble(strArray[1], CultureInfo.InvariantCulture), (float) Convert.ToDouble(strArray[2], CultureInfo.InvariantCulture)));
        }

        private static string GetCorrectString(Location t)
        {
            object[] objArray = new object[] { t.X, " ", t.Y, " ", t.Z };
            return string.Concat(objArray).Replace(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator, ".");
        }

        public List<Location> GetListSortedAfterDistance(Location location) => 
            this.GetListSortedAfterDistance(location, this._waypoints);

        public List<Location> GetListSortedAfterDistance(Location location, List<Location> waypoints)
        {
            if (waypoints.Count == 0)
            {
                return new List<Location>();
            }
            List<Location> list = new List<Location>();
            if (waypoints.Count != 0)
            {
                Location location2 = waypoints[Location.GetClosestPositionInList(waypoints, location)];
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
            this._badNodes = new List<Location>();
            this._toTownWaypoints = new List<Location>();
        }

        public bool LoadFile(string fileName)
        {
            try
            {
                this.FileName = fileName;
                this._doc = new XmlDocument();
                this._doc.Load(fileName);
            }
            catch (Exception exception)
            {
                Logging.Write("Error in loaded profile: " + exception, new object[0]);
                return false;
            }
            this._waypoints = new List<Location>();
            this._badNodes = new List<Location>();
            this._toTownWaypoints = new List<Location>();
            try
            {
                foreach (XmlNode node in this._doc.ChildNodes)
                {
                    foreach (XmlNode node2 in node.ChildNodes)
                    {
                        string name = node2.Name;
                        if (name != null)
                        {
                            if (name == "VendorName")
                            {
                                this.VendorName = node2.InnerText;
                                continue;
                            }
                            if (name == "NaturalRun")
                            {
                                this.NaturalRun = Convert.ToBoolean(node2.InnerText);
                                continue;
                            }
                            if (name == "Waypoint")
                            {
                                this._waypoints.Add(GetCorrectLocation(node2.InnerText));
                                continue;
                            }
                            if (name == "ToTown")
                            {
                                this._toTownWaypoints.Add(GetCorrectLocation(node2.InnerText));
                                continue;
                            }
                            if (name == "BadLocation")
                            {
                                this.AddBadNode(GetCorrectLocation(node2.InnerText));
                            }
                        }
                    }
                }
            }
            catch (Exception exception2)
            {
                Logging.Write("Error in loading profile " + exception2, new object[0]);
                return false;
            }
            if ((this._badNodes.Count == 0) && (this._waypoints.Count != 0))
            {
                foreach (Location location in BadNodes.GetBadNodeList())
                {
                    if (!this._badNodes.Contains(location) && (this._waypoints[0].DistanceFromXY(location) < 3000.0))
                    {
                        this.AddBadNode(location);
                    }
                }
            }
            return true;
        }

        public void RemoveBadNode(Location location)
        {
            if (this._badNodes.Contains(location))
            {
                this._badNodes.Remove(location);
            }
        }

        public void SaveFile(string saveFile)
        {
            if (!string.IsNullOrEmpty(saveFile))
            {
                StringBuilder builder = new StringBuilder();
                builder.AppendFormat("<?xml version=\"1.0\"?>", new object[0]);
                builder.AppendFormat("<{0}>", "Profile");
                builder.AppendFormat("<{0}>{1}</{0}>", "VendorName", this.VendorName);
                builder.AppendFormat("<{0}>{1}</{0}>", "NaturalRun", this.NaturalRun);
                foreach (Location location in this._waypoints)
                {
                    builder.AppendFormat("<{0}>{1}</{0}>", "Waypoint", GetCorrectString(location));
                }
                foreach (Location location2 in this._toTownWaypoints)
                {
                    builder.AppendFormat("<{0}>{1}</{0}>", "ToTown", GetCorrectString(location2));
                }
                foreach (Location location3 in this._badNodes)
                {
                    builder.AppendFormat("<{0}>{1}</{0}>", "BadLocation", GetCorrectString(location3));
                }
                builder.AppendFormat("</{0}>", "Profile");
                XmlDocument document = new XmlDocument();
                document.LoadXml(builder.ToString());
                document.Save(saveFile);
            }
        }

        public string VendorName { get; set; }

        public bool NaturalRun { get; set; }

        public List<Location> GetBadNodes =>
            this._badNodes;

        public List<Location> WaypointsNormal =>
            this._waypoints;

        public List<Location> WaypointsToTown =>
            this._toTownWaypoints;

        public string FileName { get; private set; }

        public int GetNearestWaypointIndex =>
            Location.GetClosestPositionInList(this._waypoints, LazyLib.Wow.ObjectManager.MyPlayer.Location);

        [StructLayout(LayoutKind.Sequential, Size=1)]
        private struct XmlStruct
        {
            public const string NaturalRun = "NaturalRun";
            public const string Root = "Profile";
            public const string VendorName = "VendorName";
            public const string Waypoint = "Waypoint";
            public const string ToTown = "ToTown";
            public const string BadLocation = "BadLocation";
        }
    }
}

