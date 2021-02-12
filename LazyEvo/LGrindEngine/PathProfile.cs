namespace LazyEvo.LGrindEngine
{
    using LazyEvo.LGrindEngine.Helpers;
    using LazyEvo.LGrindEngine.NpcClasses;
    using LazyEvo.Public;
    using LazyLib;
    using LazyLib.Wow;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Security;
    using System.Windows.Forms;
    using System.Xml;

    internal class PathProfile
    {
        private readonly QuickGraph _starNode = new QuickGraph();
        private PathControl _pathControl;
        private string _pathName;
        private List<SubProfile> _subProfile = new List<SubProfile>();

        public PathProfile()
        {
            this.TrainList = new List<Train>();
            this.NpcController = new LazyEvo.LGrindEngine.NpcController();
        }

        internal void AddSubProfile(SubProfile profile)
        {
            if (!this._subProfile.Contains(profile))
            {
                this._subProfile.Add(profile);
            }
        }

        internal void AddTrain(Train add)
        {
            this.TrainList.Add(add);
        }

        internal void ClearSubProfile()
        {
            this._subProfile.Clear();
        }

        public List<Location> FindShortsPath(Location start, Location end) => 
            this._starNode.FindPath(start, end);

        public Location GetClosest(Location loc) => 
            this._starNode.GetClosest(loc);

        internal SubProfile GetSubProfile()
        {
            SubProfile profile2;
            using (List<SubProfile>.Enumerator enumerator = this._subProfile.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        SubProfile current = enumerator.Current;
                        if ((LazyLib.Wow.ObjectManager.MyPlayer.Level > current.PlayerMaxLevel) || (LazyLib.Wow.ObjectManager.MyPlayer.Level < current.PlayerMinLevel))
                        {
                            continue;
                        }
                        profile2 = current;
                    }
                    else
                    {
                        LazyEvo.Public.LazyHelpers.StopAll("No more subprofiles ( Did we remember to select a profile?)");
                        return new SubProfile();
                    }
                    break;
                }
            }
            return profile2;
        }

        public void Load()
        {
            OpenFileDialog dialog = new OpenFileDialog {
                Filter = "File (*.xml)|*.xml"
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string fileName = dialog.FileName;
                if (fileName.Contains(".xml"))
                {
                    GrindingSettings.Profile = fileName;
                    GrindingSettings.SaveSettings();
                    this.LoadNoDialog(fileName);
                }
            }
        }

        public bool LoadNoDialog(string profileToLoad)
        {
            bool flag;
            try
            {
                this._subProfile = new List<SubProfile>();
                this.TrainList = new List<Train>();
                this.NpcController = new LazyEvo.LGrindEngine.NpcController();
                if (File.Exists(profileToLoad))
                {
                    XmlDocument document;
                    try
                    {
                        document = new XmlDocument();
                        document.Load(profileToLoad);
                    }
                    catch (Exception exception)
                    {
                        Logging.Write("Error in loaded profile: " + exception, new object[0]);
                        return false;
                    }
                    if (document.GetElementsByTagName("LazyProfile")[0] != null)
                    {
                        this.LoadVendorsEx(document);
                        this.LoadSubProfile(document);
                        this.LoadVendorOld(document);
                        this.LoadPath(profileToLoad, document);
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("The profile you tried to load is not a valid profile for this engine");
                        flag = false;
                    }
                }
                else
                {
                    Logging.Write("Could not find file: " + profileToLoad, new object[0]);
                    flag = false;
                }
            }
            catch (Exception exception2)
            {
                Logging.Write("Exception when loading profile: " + exception2, new object[0]);
                flag = false;
            }
            return flag;
        }

        private void LoadPath(string profileToLoad, XmlDocument doc)
        {
            XmlNodeList elementsByTagName = doc.GetElementsByTagName("PathName");
            this._pathName = elementsByTagName[0].ChildNodes[0].Value;
            object[] objArray = new object[] { Path.GetDirectoryName(profileToLoad), Path.DirectorySeparatorChar, this._pathName, ".path" };
            string path = string.Concat(objArray);
            if (File.Exists(profileToLoad + ".path"))
            {
                this._starNode.LoadGraph(profileToLoad + ".path");
            }
            else if (File.Exists(profileToLoad.Replace(".xml", string.Empty) + ".path"))
            {
                this._starNode.LoadGraph(profileToLoad + ".path");
            }
            else if (File.Exists(path))
            {
                this._starNode.LoadGraph(path);
            }
            else
            {
                Logging.Write("Could not finde: " + path + " no path loaded", new object[0]);
            }
        }

        private void LoadSubProfile(XmlDocument doc)
        {
            foreach (XmlNode node in doc.GetElementsByTagName("SubProfile"))
            {
                SubProfile item = new SubProfile();
                foreach (XmlNode node2 in node.ChildNodes)
                {
                    if (node2.Name.Equals("Name"))
                    {
                        item.Name = node2.InnerText;
                    }
                    if (node2.Name.Equals("MinLevel"))
                    {
                        item.PlayerMinLevel = Convert.ToInt32(node2.InnerText);
                    }
                    if (node2.Name.Equals("MaxLevel"))
                    {
                        item.PlayerMaxLevel = Convert.ToInt32(node2.InnerText);
                    }
                    if (node2.Name.Equals("MobMinLevel"))
                    {
                        item.MobMinLevel = Convert.ToInt32(node2.InnerText);
                    }
                    if (node2.Name.Equals("MobMaxLevel"))
                    {
                        item.MobMaxLevel = Convert.ToInt32(node2.InnerText);
                    }
                    if (node2.Name.Equals("SpotRoamDistance"))
                    {
                        item.SpotRoamDistance = Convert.ToInt32(node2.InnerText);
                    }
                    if (node2.Name.Equals("Order"))
                    {
                        item.Order = Convert.ToBoolean(node2.InnerText);
                    }
                    if (node2.Name.Equals("Factions"))
                    {
                        string[] strArray = node2.InnerText.Split(new char[] { ' ' });
                        CS$<>9__CachedAnonymousMethodDelegate4 ??= s => Convert.ToUInt32(s);
                        item.Factions.AddRange(Enumerable.Select<string, uint>(from s in strArray
                            where s != ""
                            select s, CS$<>9__CachedAnonymousMethodDelegate4));
                    }
                    if (node2.Name.Equals("Ignores"))
                    {
                        string[] strArray2 = node2.InnerText.Split(new char[] { '|' });
                        item.Ignore.AddRange(from s in strArray2
                            where s != ""
                            select s);
                    }
                    if (node2.Name.Equals("Spot"))
                    {
                        string innerText = node2.InnerText;
                        string str4 = innerText;
                        if (Convert.ToString(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator) != ".")
                        {
                            str4 = innerText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
                        }
                        string[] strArray3 = str4.Split(new char[] { ' ' });
                        if (strArray3.Length > 2)
                        {
                            Location location = new Location((float) Convert.ToDouble(strArray3[0]), (float) Convert.ToDouble(strArray3[1]), (float) Convert.ToDouble(strArray3[2]));
                            item.Spots.Add(location);
                        }
                        else
                        {
                            Location location2 = new Location((float) Convert.ToDouble(strArray3[0]), (float) Convert.ToDouble(strArray3[1]), (float) Convert.ToDouble(0));
                            item.Spots.Add(location2);
                        }
                    }
                    if (node2.Name.Equals("Vendor"))
                    {
                        Vendor vendor = new Vendor();
                        vendor.Load(node2);
                        this.NpcController.AddNpc(new VendorsEx(VendorType.Repair, vendor.Name, vendor.Spot, -2147483648));
                    }
                }
                this._subProfile.Add(item);
            }
        }

        private void LoadVendorOld(XmlDocument doc)
        {
            try
            {
                foreach (XmlNode node in doc.GetElementsByTagName("Vendor"))
                {
                    Vendor vendor = new Vendor();
                    vendor.Load(node);
                    if (!string.IsNullOrEmpty(vendor.Name) && (vendor.Spot != null))
                    {
                        this.NpcController.AddNpc(new VendorsEx(VendorType.Repair, vendor.Name, vendor.Spot, -2147483648));
                    }
                }
            }
            catch
            {
            }
        }

        private void LoadVendorsEx(XmlDocument doc)
        {
            try
            {
                XmlNodeList elementsByTagName = doc.GetElementsByTagName("Vendors");
                if (elementsByTagName.Count != 0)
                {
                    this.NpcController.LoadXml(elementsByTagName[0]);
                }
            }
            catch (Exception exception)
            {
                Logging.Write("Could not load Vendors: " + exception, new object[0]);
            }
        }

        public void New()
        {
            this._subProfile.Clear();
            this._starNode.New();
            this.NpcController = new LazyEvo.LGrindEngine.NpcController();
        }

        public void Open()
        {
            if ((this._pathControl == null) || this._pathControl.IsDisposed)
            {
                this._pathControl = new PathControl(this);
                this._pathControl.Start();
            }
        }

        public void Save()
        {
            SaveFileDialog dialog = new SaveFileDialog {
                Filter = "File (*.xml)|*.xml",
                FilterIndex = 2,
                RestoreDirectory = true
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.SaveProfile(dialog.FileName);
            }
        }

        internal void SaveProfile(string fileName)
        {
            string str = (("<?xml version=\"1.0\"?>" + "<LazyProfile>") + "<PathName>" + Path.GetFileName(fileName) + "</PathName>") + "<Vendors>";
            foreach (VendorsEx ex in this.NpcController.Npc)
            {
                string str2 = $"X="{ex.Location.X}" Y="{ex.Location.Y}" Z="{ex.Location.Z}"";
                string str3 = (Convert.ToString(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator) != ".") ? str2.Replace(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator, ".") : str2;
                if (ex.VendorType == VendorType.Train)
                {
                    str = str + $"<Vendor Name="{SecurityElement.Escape(ex.Name)}" Type="{ex.VendorType}" TrainClass="{ex.TrainClass}" EntryId="{ex.EntryId}" {str3} />";
                    continue;
                }
                str = str + $"<Vendor Name="{SecurityElement.Escape(ex.Name)}" Type="{ex.VendorType}" EntryId="{ex.EntryId}" {str3} />";
            }
            str = str + "</Vendors>";
            foreach (SubProfile profile in this._subProfile)
            {
                object obj2 = (str + "<SubProfile>") + "<Name>" + profile.Name + "</Name>";
                object[] objArray3 = new object[] { obj2, "<MinLevel>", profile.PlayerMinLevel, "</MinLevel>" };
                object obj3 = string.Concat(objArray3);
                object[] objArray4 = new object[] { obj3, "<MaxLevel>", profile.PlayerMaxLevel, "</MaxLevel>" };
                object obj4 = string.Concat(objArray4);
                object[] objArray5 = new object[] { obj4, "<MobMinLevel>", profile.MobMinLevel, "</MobMinLevel>" };
                object obj5 = string.Concat(objArray5);
                object[] objArray6 = new object[] { obj5, "<MobMaxLevel>", profile.MobMaxLevel, "</MobMaxLevel>" };
                object obj6 = string.Concat(objArray6);
                object[] objArray7 = new object[] { obj6, "<SpotRoamDistance>", profile.SpotRoamDistance, "</SpotRoamDistance>" };
                object obj7 = string.Concat(objArray7);
                str = string.Concat(new object[] { obj7, "<Order>", profile.Order, "</Order>" }) + "<Factions>";
                int num = 0;
                foreach (uint num2 in profile.Factions)
                {
                    str = (num != 0) ? (str + " " + num2) : (str + num2);
                    num++;
                }
                str = str + "</Factions>" + "<Ignores>";
                num = 0;
                foreach (string str4 in profile.Ignore)
                {
                    str = (num != 0) ? (str + "|" + str4) : (str + str4);
                    num++;
                }
                str = str + "</Ignores>";
                foreach (Location location in profile.Spots)
                {
                    object[] objArray9 = new object[] { location.X, " ", location.Y, " ", location.Z };
                    string str5 = string.Concat(objArray9);
                    string str6 = "";
                    str6 = (Convert.ToString(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator) == ".") ? str5 : str5.Replace(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator, ".");
                    str = str + "<Spot>" + str6 + "</Spot>";
                }
                str = str + "</SubProfile>";
            }
            XmlDocument document = new XmlDocument();
            document.LoadXml(str + "</LazyProfile>");
            document.Save(fileName);
            this._starNode.SaveGraph(fileName + ".path");
        }

        internal LazyEvo.LGrindEngine.NpcController NpcController { get; set; }

        internal List<Train> TrainList { get; private set; }

        internal QuickGraph GetGraph =>
            this._starNode;

        internal List<SubProfile> GetSubProfiles =>
            this._subProfile;
    }
}

