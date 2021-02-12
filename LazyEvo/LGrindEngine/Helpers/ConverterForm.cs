namespace LazyEvo.LGrindEngine.Helpers
{
    using DevComponents.DotNetBar;
    using LazyEvo.LGrindEngine;
    using LazyLib.Wow;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Globalization;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;
    using System.Xml;

    public class ConverterForm : Office2007Form
    {
        public List<uint> Factions = new List<uint>();
        private XmlDocument _doc;
        private string _folderSelected;
        private List<Location> _ghost = new List<Location>();
        private int _roamDistance;
        private List<Location> _toTownWaypoints = new List<Location>();
        private List<Location> _waypoints = new List<Location>();
        private string _xmlFile;
        private IContainer components;
        private LabelX labelX1;
        private ButtonX buttonX1;
        private ButtonX buttonX2;
        private ButtonX buttonX3;

        public ConverterForm()
        {
            this.InitializeComponent();
        }

        private void ButtonX1Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog {
                InitialDirectory = AppDomain.CurrentDomain.BaseDirectory,
                Filter = "Profiles (*.xml)|*.xml"
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this._xmlFile = dialog.FileName;
                if (!this._xmlFile.Contains(".xml"))
                {
                    MessageBox.Show("Please select a valid profile type.");
                }
            }
        }

        private void ButtonX2Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this._folderSelected = dialog.SelectedPath;
            }
        }

        private void ButtonX3Click(object sender, EventArgs h)
        {
            if (string.IsNullOrEmpty(this._folderSelected))
            {
                MessageBox.Show("Please select a folder.");
            }
            else if (string.IsNullOrEmpty(this._xmlFile))
            {
                MessageBox.Show("Please select a profile to convert.");
            }
            else
            {
                this._waypoints = new List<Location>();
                this._ghost = new List<Location>();
                this._toTownWaypoints = new List<Location>();
                try
                {
                    this._doc = new XmlDocument();
                    this._doc.Load(this._xmlFile);
                }
                catch (Exception exception)
                {
                    MessageBox.Show("Error in loaded profile: " + exception);
                }
                try
                {
                    foreach (XmlNode node in this._doc.ChildNodes)
                    {
                        foreach (XmlNode node2 in node.ChildNodes)
                        {
                            string name = node2.Name;
                            if (name != null)
                            {
                                if (name == "Waypoint")
                                {
                                    this._waypoints.Add(GetCorrectLocation(node2.InnerText));
                                    continue;
                                }
                                if (name == "Vendor")
                                {
                                    this._toTownWaypoints.Add(GetCorrectLocation(node2.InnerText));
                                    continue;
                                }
                                if (name == "GhostWaypoint")
                                {
                                    this._ghost.Add(GetCorrectLocation(node2.InnerText));
                                }
                            }
                        }
                    }
                }
                catch (Exception exception2)
                {
                    MessageBox.Show("Error in converting profile " + exception2);
                }
                try
                {
                    XmlNodeList elementsByTagName = this._doc.GetElementsByTagName("RoamDistance");
                    this._roamDistance = Convert.ToInt32(elementsByTagName[0].ChildNodes[0].Value);
                }
                catch
                {
                }
                if (this._roamDistance == 0f)
                {
                    this._roamDistance = 0x23;
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
                if (this._waypoints.Count == 0)
                {
                    MessageBox.Show("Profile should have more than 1 waypoint");
                }
                PathProfile profile = new PathProfile();
                SubProfile profile2 = new SubProfile();
                for (int i = 0; i < this._waypoints.Count; i++)
                {
                    profile.GetGraph.AddNodeNoConnection(this._waypoints[i]);
                    if (i == (this._waypoints.Count - 1))
                    {
                        profile.GetGraph.AddEdge(this._waypoints[i], this._waypoints[0]);
                    }
                    else
                    {
                        profile.GetGraph.AddNodeNoConnection(this._waypoints[i + 1]);
                        profile.GetGraph.AddEdge(this._waypoints[i], this._waypoints[i + 1]);
                    }
                    profile2.Spots.Add(new Location(this._waypoints[i].X - 2f, this._waypoints[i].Y - 2f, this._waypoints[i].Z));
                }
                for (int j = 0; j < this._ghost.Count; j++)
                {
                    profile.GetGraph.AddNodeNoConnection(this._ghost[j]);
                    if (j != (this._ghost.Count - 1))
                    {
                        profile.GetGraph.AddNodeNoConnection(this._ghost[j + 1]);
                        profile.GetGraph.AddEdge(this._ghost[j], this._ghost[j + 1]);
                    }
                }
                for (int k = 0; k < this._toTownWaypoints.Count; k++)
                {
                    profile.GetGraph.AddNodeNoConnection(this._toTownWaypoints[k]);
                    if (k != (this._toTownWaypoints.Count - 1))
                    {
                        profile.GetGraph.AddNodeNoConnection(this._toTownWaypoints[k + 1]);
                        profile.GetGraph.AddEdge(this._toTownWaypoints[k], this._toTownWaypoints[k + 1]);
                    }
                }
                if (this._ghost.Count != 0)
                {
                    profile.GetGraph.AddEdge(this._ghost[0], this.GetListSortedAfterDistance(this._ghost[0], this._waypoints)[0]);
                }
                if (this._toTownWaypoints.Count != 0)
                {
                    profile.GetGraph.AddEdge(this._toTownWaypoints[0], this.GetListSortedAfterDistance(this._toTownWaypoints[0], this._waypoints)[0]);
                }
                profile2.SpotRoamDistance = this._roamDistance;
                foreach (uint num4 in this.Factions)
                {
                    profile2.Factions.Add(num4);
                }
                profile2.MobMaxLevel = 0x63;
                profile2.MobMinLevel = 0;
                profile2.PlayerMaxLevel = 0x63;
                profile2.PlayerMinLevel = 0;
                profile2.Order = true;
                profile2.Name = "Converted profile";
                profile.AddSubProfile(profile2);
                profile.SaveProfile(this._folderSelected + @"\Converted.xml");
                MessageBox.Show("Convertion done");
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private static Location GetCorrectLocation(string location)
        {
            string[] strArray = location.Split(new char[] { ' ' });
            return ((strArray.Length <= 2) ? new Location((float) Convert.ToDouble(strArray[0], CultureInfo.InvariantCulture), (float) Convert.ToDouble(strArray[1], CultureInfo.InvariantCulture), (float) Convert.ToDouble(0)) : new Location((float) Convert.ToDouble(strArray[0], CultureInfo.InvariantCulture), (float) Convert.ToDouble(strArray[1], CultureInfo.InvariantCulture), (float) Convert.ToDouble(strArray[2], CultureInfo.InvariantCulture)));
        }

        public List<Location> GetListSortedAfterDistance(Location location, List<Location> waypoints) => 
            (waypoints.Count != 0) ? (from p in waypoints
                orderby p.DistanceFrom(location)
                select p).ToList<Location>() : new List<Location>();

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(ConverterForm));
            this.labelX1 = new LabelX();
            this.buttonX1 = new ButtonX();
            this.buttonX2 = new ButtonX();
            this.buttonX3 = new ButtonX();
            base.SuspendLayout();
            this.labelX1.BackgroundStyle.Class = "";
            this.labelX1.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX1.Location = new Point(8, 2);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new Size(0x12b, 0x53);
            this.labelX1.TabIndex = 0;
            this.labelX1.Text = manager.GetString("labelX1.Text");
            this.buttonX1.AccessibleRole = AccessibleRole.PushButton;
            this.buttonX1.ColorTable = eButtonColor.OrangeWithBackground;
            this.buttonX1.Location = new Point(0x5e, 0x5b);
            this.buttonX1.Name = "buttonX1";
            this.buttonX1.Size = new Size(0x5d, 0x17);
            this.buttonX1.Style = eDotNetBarStyle.StyleManagerControlled;
            this.buttonX1.TabIndex = 1;
            this.buttonX1.Text = "Select old profile";
            this.buttonX1.Click += new EventHandler(this.ButtonX1Click);
            this.buttonX2.AccessibleRole = AccessibleRole.PushButton;
            this.buttonX2.ColorTable = eButtonColor.OrangeWithBackground;
            this.buttonX2.Location = new Point(80, 0x79);
            this.buttonX2.Name = "buttonX2";
            this.buttonX2.Size = new Size(0x79, 0x17);
            this.buttonX2.Style = eDotNetBarStyle.StyleManagerControlled;
            this.buttonX2.TabIndex = 2;
            this.buttonX2.Text = "Select folder to save to";
            this.buttonX2.Click += new EventHandler(this.ButtonX2Click);
            this.buttonX3.AccessibleRole = AccessibleRole.PushButton;
            this.buttonX3.ColorTable = eButtonColor.OrangeWithBackground;
            this.buttonX3.Location = new Point(0x65, 160);
            this.buttonX3.Name = "buttonX3";
            this.buttonX3.Size = new Size(0x4b, 0x17);
            this.buttonX3.Style = eDotNetBarStyle.StyleManagerControlled;
            this.buttonX3.TabIndex = 3;
            this.buttonX3.Text = "Convert";
            this.buttonX3.Click += new EventHandler(this.ButtonX3Click);
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.FromArgb(0xdf, 0xea, 0xf6);
            base.ClientSize = new Size(0x12d, 190);
            base.Controls.Add(this.buttonX3);
            base.Controls.Add(this.buttonX2);
            base.Controls.Add(this.buttonX1);
            base.Controls.Add(this.labelX1);
            this.DoubleBuffered = true;
            base.Name = "ConverterForm";
            this.Text = "ConverterForm";
            base.ResumeLayout(false);
        }

        [StructLayout(LayoutKind.Sequential, Size=1)]
        private struct XmlStruct
        {
            public const string Waypoint = "Waypoint";
            public const string ToTown = "Vendor";
            public const string Ghost = "GhostWaypoint";
        }
    }
}

