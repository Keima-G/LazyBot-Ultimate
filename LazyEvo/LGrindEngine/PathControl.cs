namespace LazyEvo.LGrindEngine
{
    using DevComponents.AdvTree;
    using DevComponents.DotNetBar;
    using DevComponents.DotNetBar.Controls;
    using DevComponents.DotNetBar.Metro.ColorTables;
    using DevComponents.Editors;
    using LazyEvo;
    using LazyEvo.LGrindEngine.NpcClasses;
    using LazyLib;
    using LazyLib.FSM;
    using LazyLib.Wow;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows.Forms;

    internal sealed class PathControl : Office2007Form
    {
        private const float ImageSize = 256f;
        private const float TileSize = 533f;
        private readonly Color _colorMe;
        private readonly Hotkey _f7;
        private readonly Hotkey _f8;
        private readonly PathProfile _pathProfile;
        private Font _fontText;
        private Thread _mapThread;
        private Bitmap _offScreenBmp;
        private Graphics _offScreenDc;
        private Thread _pathLoadedThread;
        private int _refreshTime;
        private double _scale;
        private bool _updateFormSize;
        private List<DirectedLazyEdge> _nodesToDraw;
        private readonly List<Location> _selectedList;
        private Rectangle _rect;
        private SubProfile _selected;
        private Location _spotSelectedForEdit;
        private IContainer components;
        private SuperTabControlPanel superTabControlPanel1;
        private DevComponents.AdvTree.AdvTree BePullRules;
        private NodeConnector nodeConnector3;
        private ElementStyle elementStyle3;
        private SuperTabItem TabPull;
        private LabelItem labelItem3;
        private ButtonItem BeSaveBeheavior;
        private ButtonItem BeComAddRule;
        private LabelItem labelItem1;
        private ButtonItem BeComEditRule;
        private LabelItem labelItem2;
        private ButtonItem BeComDeleteRule;
        private SuperTabControl superTabControl1;
        private SuperTabControlPanel superTabControlPanel2;
        private SuperTabItem superTabItem1;
        private SuperTabControlPanel superTabControlPanel3;
        private SuperTabItem superTabItem2;
        private GroupPanel GraphView;
        private ButtonX BtnLoad;
        private ButtonX BtnSave;
        private CheckBoxX CBRecordGraph;
        private ButtonX BtnAddNode;
        private CheckBoxX CBSpotOrder;
        private ButtonX BtnAddSpot;
        private LabelX LBSpotCount;
        private ButtonX BtnFaction;
        private LabelX LBFactionCount;
        private IntegerInput PMaxLevel;
        private IntegerInput PMinLevel;
        private TextBoxX TBName;
        private LabelX labelX5;
        private LabelX labelX4;
        private LabelX labelX2;
        private LabelX labelX1;
        private ButtonX BtnAdd;
        private DevComponents.AdvTree.AdvTree ListSubProfiles;
        private GroupPanel groupPanel1;
        private NodeConnector nodeConnector1;
        private ElementStyle elementStyle1;
        private ButtonX BtnRemoveNode;
        private ButtonX BtnNew;
        private ButtonX BtnAddNodeNo;
        private ButtonX BtnAddNodeConnection;
        private SuperTooltip superTooltip1;
        private LabelX labelX6;
        private IntegerInput INodeDistance;
        private LabelX labelX14;
        internal ComboBoxEx SelectNodeType;
        private ComboItem comboItem1;
        private ComboItem comboItem2;
        private StyleManager styleManager1;
        private CheckBoxX CBTopMost;
        private ButtonX BtnRemove;
        private IntegerInput UMaxLevel;
        private IntegerInput UMinLevel;
        private LabelX labelX10;
        private IntegerInput SpotRoamDistance;
        private LabelX labelX11;
        private CheckBoxX CBShowPullZones;
        private LabelX labelX12;
        private IntegerInput DrawEdgesValue;
        private GroupPanel groupPanel2;
        private GroupPanel groupPanel3;
        private ButtonX OtherAddSpot;
        private ButtonX BtnRemoveSpot;
        private ButtonX BtnClearSelection;
        private GroupPanel groupPanel6;
        private GroupPanel groupPanel5;
        private GroupPanel groupPanel4;
        private LabelX labelX8;
        private LabelX LBIgnoreCount;
        private ButtonX BtnAddIgnore;
        private TextBoxX TBFactionList;
        private LabelX labelX3;
        private LabelX labelX13;
        private LabelX LBTrainerCount;
        private LabelX labelX9;
        private LabelX LBVendorCount;
        private LabelX labelX7;
        private ButtonX BtnAddTrainer;
        private ButtonX BtnAddRepair;
        private TextBoxX TBIgnore;
        private LabelX labelX15;
        private ComboItem comboItem3;

        public PathControl(PathProfile pathProfile)
        {
            HandledEventHandler handler = null;
            HandledEventHandler handler2 = null;
            this._colorMe = Color.Green;
            this._refreshTime = 0x55;
            this._scale = 3.0;
            this._updateFormSize = true;
            this._nodesToDraw = new List<DirectedLazyEdge>();
            this._selectedList = new List<Location>();
            this._rect = new Rectangle(-1, -1, -1, -1);
            this.InitializeComponent();
            this.DoubleBuffered = true;
            this.GraphView.BringToFront();
            this._pathProfile = pathProfile;
            Thread thread = new Thread(new ThreadStart(this.LoadPath)) {
                IsBackground = true
            };
            this._pathLoadedThread = thread;
            this._pathLoadedThread.Start();
            if (LazySettings.SetupUseHotkeys)
            {
                this._f7 = new Hotkey();
                this._f7.KeyCode = Keys.F7;
                this._f7.Windows = false;
                if (handler == null)
                {
                    handler = (param0, param1) => this.AddSpot();
                }
                this._f7.Pressed += handler;
                try
                {
                    if (!this._f7.GetCanRegister(this))
                    {
                        Logging.Write("Cannot register F7 as hotkey", new object[0]);
                    }
                    else
                    {
                        this._f7.Register(this);
                    }
                }
                catch
                {
                    Logging.Write("Cannot register F7 as hotkey", new object[0]);
                }
                this._f8 = new Hotkey();
                this._f8.KeyCode = Keys.F8;
                this._f8.Windows = false;
                if (handler2 == null)
                {
                    handler2 = (param0, param1) => this.AddNode();
                }
                this._f8.Pressed += handler2;
                try
                {
                    if (!this._f8.GetCanRegister(this))
                    {
                        Logging.Write("Cannot register F8 as hotkey", new object[0]);
                    }
                    else
                    {
                        this._f8.Register(this);
                    }
                }
                catch
                {
                    Logging.Write("Cannot register F8 as hotkey", new object[0]);
                }
            }
        }

        private void AddNode()
        {
            this._pathProfile.GetGraph.AddNode(LazyLib.Wow.ObjectManager.MyPlayer.Location);
        }

        private void AddNode(Node node)
        {
            this.ListSubProfiles.BeginUpdate();
            this.ListSubProfiles.Nodes.Add(node);
            this.ListSubProfiles.EndUpdate();
        }

        private void AddNode(string name, SubProfile profile)
        {
            Node node = new Node {
                Text = name,
                Tag = profile
            };
            this.AddNode(node);
        }

        private void AddSpot()
        {
            if ((this._selected != null) && !this._selected.Spots.Contains(LazyLib.Wow.ObjectManager.MyPlayer.Location))
            {
                this._selected.Spots.Add(LazyLib.Wow.ObjectManager.MyPlayer.Location);
                this.LBSpotCount.Text = this._selected.Spots.Count;
            }
        }

        private void BtnAddClick(object sender, EventArgs e)
        {
            foreach (Node node in this.ListSubProfiles.Nodes)
            {
                if ((node.Tag is SubProfile) && ReferenceEquals(this._selected, node.Tag))
                {
                    node.Text = this._selected.Name;
                }
            }
            this._selected = new SubProfile();
            this.AddNode(this._selected.Name, this._selected);
            this.UpdateFields(this._selected);
            this.ListSubProfiles.SelectedIndex = this.ListSubProfiles.Nodes.Count - 1;
        }

        private void BtnAddIgnoreClick(object sender, EventArgs e)
        {
            if ((LazyLib.Wow.ObjectManager.MyPlayer.Target != null) && !this._selected.Ignore.Contains(LazyLib.Wow.ObjectManager.MyPlayer.Target.Name))
            {
                this._selected.Ignore.Add(LazyLib.Wow.ObjectManager.MyPlayer.Target.Name);
                this.TBIgnore.Text = Enumerable.Aggregate<string, string>(this._selected.Ignore, string.Empty, (current, faction) => current + $"{faction}|");
                this.LBIgnoreCount.Text = this._selected.Ignore.Count;
            }
        }

        private void BtnAddNodeClick(object sender, EventArgs e)
        {
            this.AddNode();
        }

        private void BtnAddNodeConnectionClick(object sender, EventArgs e)
        {
            lock (this._selectedList)
            {
                List<DirectedLazyEdge> list = new List<DirectedLazyEdge>();
                int num = 0;
                while (true)
                {
                    if (num >= this._selectedList.Count)
                    {
                        this._selectedList.Clear();
                        break;
                    }
                    Location loc = this._selectedList[num];
                    foreach (Location location in from l in this._selectedList
                        where !ReferenceEquals(l, loc)
                        select l)
                    {
                        Location location1 = location;
                        if (!Enumerable.Any<DirectedLazyEdge>(list, k => ((k.Source != loc) || (k.Target != location1)) ? ((k.Source == location1) && (k.Target == loc)) : true))
                        {
                            this._pathProfile.GetGraph.AddConnection(loc, location);
                            list.Add(new DirectedLazyEdge(loc, location));
                        }
                    }
                    num++;
                }
            }
        }

        private void BtnAddNodeNoClick(object sender, EventArgs e)
        {
            this._pathProfile.GetGraph.AddNodeNoConnection(LazyLib.Wow.ObjectManager.MyPlayer.Location);
        }

        private void BtnAddRepairClick(object sender, EventArgs e)
        {
            try
            {
                if ((LazyLib.Wow.ObjectManager.MyPlayer.Target != null) && (this._pathProfile != null))
                {
                    this._pathProfile.NpcController.AddNpc(new VendorsEx(VendorType.Repair, LazyLib.Wow.ObjectManager.MyPlayer.Target.Name, LazyLib.Wow.ObjectManager.MyPlayer.Target.Location, LazyLib.Wow.ObjectManager.MyPlayer.Target.Entry));
                    this.LBVendorCount.Text = (from npc in this._pathProfile.NpcController.Npc
                        where npc.VendorType == VendorType.Repair
                        select npc).ToList<VendorsEx>().Count.ToString();
                }
            }
            catch (Exception exception)
            {
                Logging.Write("Exception when BtnAddRepair_Click: " + exception, new object[0]);
            }
        }

        private void BtnAddSpotClick(object sender, EventArgs e)
        {
            this.AddSpot();
        }

        private void BtnAddTrainerClick(object sender, EventArgs e)
        {
            if (LazyLib.Wow.ObjectManager.MyPlayer.Target != null)
            {
                PUnit target = LazyLib.Wow.ObjectManager.MyPlayer.Target;
                TrainerDialog dialog = new TrainerDialog();
                dialog.ShowDialog();
                if (dialog.Ok)
                {
                    this._pathProfile.NpcController.AddNpc(new VendorsEx(VendorType.Train, target.Name, target.Location, (TrainClass) Enum.Parse(typeof(TrainClass), dialog.Class, true), target.Entry));
                    this.LBTrainerCount.Text = (from npc in this._pathProfile.NpcController.Npc
                        where npc.VendorType == VendorType.Train
                        select npc).ToList<VendorsEx>().Count.ToString();
                }
                this.UpdateFields(this._selected);
            }
        }

        private void BtnClearSelectionClick(object sender, EventArgs e)
        {
            this._selectedList.Clear();
        }

        private void BtnFactionClick(object sender, EventArgs e)
        {
            if ((LazyLib.Wow.ObjectManager.MyPlayer.Target != null) && !this._selected.Factions.Contains(LazyLib.Wow.ObjectManager.MyPlayer.Target.Faction))
            {
                this._selected.Factions.Add(LazyLib.Wow.ObjectManager.MyPlayer.Target.Faction);
                this.LBFactionCount.Text = this._selected.Factions.Count;
                this.TBFactionList.Text = Enumerable.Aggregate<uint, string>(this._selected.Factions, string.Empty, (current, faction) => current + $"{faction} ");
            }
        }

        private void BtnLoadClick(object sender, EventArgs e)
        {
            this._pathProfile.Load();
            this.DoLoad();
        }

        private void BtnNewClick(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question).ToString() == "Yes")
            {
                this._pathProfile.New();
                this.ListSubProfiles.BeginUpdate();
                this.ListSubProfiles.Nodes.Clear();
                this.ListSubProfiles.EndUpdate();
                this.TBName.Text = "";
                this.LBFactionCount.Text = "0";
                this.LBSpotCount.Text = "0";
                this.LBVendorCount.Text = "0";
                this.LBTrainerCount.Text = "0";
                this.TBName.Enabled = false;
                this.PMinLevel.Enabled = false;
                this.PMaxLevel.Enabled = false;
                this.CBSpotOrder.Enabled = false;
                this.BtnFaction.Enabled = false;
                this.BtnAddSpot.Enabled = false;
            }
        }

        private void BtnRemoveClick(object sender, EventArgs e)
        {
            if (this.ListSubProfiles.SelectedNode != null)
            {
                Node node = null;
                foreach (Node node2 in this.ListSubProfiles.Nodes)
                {
                    if (node2.Tag.Equals(this.ListSubProfiles.SelectedNode.Tag))
                    {
                        node = node2;
                    }
                }
                if (node != null)
                {
                    this.ListSubProfiles.Nodes.Remove(node);
                    this._selected = new SubProfile();
                    this.UpdateFields(this._selected);
                    this.DisableSubProfiles();
                }
            }
        }

        private void BtnRemoveNodeClick(object sender, EventArgs e)
        {
            lock (this._selectedList)
            {
                foreach (Location location in this._selectedList)
                {
                    this._pathProfile.GetGraph.RemoveNode(location);
                }
                this._selectedList.Clear();
            }
        }

        private void BtnRemoveSpotClick(object sender, EventArgs e)
        {
            if (this._spotSelectedForEdit != null)
            {
                Location item = null;
                foreach (SubProfile profile in this._pathProfile.GetSubProfiles)
                {
                    CS$<>9__CachedAnonymousMethodDelegate2b ??= pL => pL.DistanceToSelf2D;
                    foreach (Location location2 in Enumerable.OrderBy<Location, double>(from u in profile.Spots select u, CS$<>9__CachedAnonymousMethodDelegate2b).Take<Location>(50))
                    {
                        if (ReferenceEquals(location2, this._spotSelectedForEdit))
                        {
                            item = location2;
                            break;
                        }
                    }
                    if (item != null)
                    {
                        profile.Spots.Remove(item);
                        break;
                    }
                }
            }
        }

        private void BtnSaveClick(object sender, EventArgs e)
        {
            this.SaveSubProfiles();
            this._pathProfile.Save();
        }

        private void CbRecordGraphCheckedChanged(object sender, EventArgs e)
        {
            if (this.CBRecordGraph.Checked)
            {
                this._pathProfile.GetGraph.RecordMesh();
            }
            else
            {
                this._pathProfile.GetGraph.StopRecordMesh();
            }
        }

        private void CbSpotOrderCheckedChanged(object sender, EventArgs e)
        {
            this._selected.Order = this.CBSpotOrder.Checked;
        }

        private void CbTopMostCheckedChanged(object sender, EventArgs e)
        {
            base.TopMost = this.CBTopMost.Checked;
        }

        private double ConvertHeading(double heading) => 
            (heading * -1.0) - 1.5707963267948966;

        private void DisableSubProfiles()
        {
            this.TBName.Enabled = false;
            this.PMinLevel.Enabled = false;
            this.PMaxLevel.Enabled = false;
            this.UMaxLevel.Enabled = false;
            this.UMinLevel.Enabled = false;
            this.CBSpotOrder.Enabled = false;
            this.SpotRoamDistance.Enabled = false;
            this.TBFactionList.Enabled = false;
            this.BtnAddIgnore.Enabled = false;
            this.TBIgnore.Enabled = false;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void DoLoad()
        {
            this.DisableSubProfiles();
            this.ListSubProfiles.BeginUpdate();
            this.ListSubProfiles.Nodes.Clear();
            this.ListSubProfiles.EndUpdate();
            foreach (SubProfile profile in this._pathProfile.GetSubProfiles)
            {
                this.AddNode(profile.Name, profile);
            }
            int num = 0;
            int num2 = 0;
            foreach (VendorsEx ex in this._pathProfile.NpcController.Npc)
            {
                if (ex.VendorType == VendorType.Repair)
                {
                    num++;
                }
                if (ex.VendorType == VendorType.Train)
                {
                    num2++;
                }
            }
            this.LBVendorCount.Text = num.ToString();
            this.LBTrainerCount.Text = num2.ToString();
            this.SelectNodeType.SelectedIndex = 0;
        }

        private void GraphViewClick(object sender, EventArgs e)
        {
            try
            {
                Point point = base.PointToClient(Cursor.Position);
                Rectangle b = new Rectangle(point.X - 6, point.Y - 20, 6, 6);
                float x = LazyLib.Wow.ObjectManager.MyPlayer.Location.X;
                float y = LazyLib.Wow.ObjectManager.MyPlayer.Location.Y;
                foreach (Location location in this._pathProfile.GetGraph.GetNodes())
                {
                    float num3 = this.OffsetX(location.X, x);
                    float num4 = this.OffsetY(location.Y, y);
                    Rectangle a = new Rectangle((int) num4, (int) num3, 6, 6);
                    if (Rectangle.Intersect(a, b) != Rectangle.Empty)
                    {
                        lock (this._selectedList)
                        {
                            if (!this._selectedList.Contains(location))
                            {
                                this._selectedList.Add(location);
                            }
                        }
                    }
                }
                foreach (SubProfile profile in this._pathProfile.GetSubProfiles)
                {
                    CS$<>9__CachedAnonymousMethodDelegate1d ??= pL => pL.DistanceToSelf2D;
                    foreach (Location location2 in Enumerable.OrderBy<Location, double>(from u in profile.Spots select u, CS$<>9__CachedAnonymousMethodDelegate1d).Take<Location>(20))
                    {
                        float num5 = this.OffsetX(location2.X, x);
                        float num6 = this.OffsetY(location2.Y, y);
                        Rectangle a = new Rectangle((int) num6, (int) num5, 6, 6);
                        if (Rectangle.Intersect(a, b) != Rectangle.Empty)
                        {
                            this._spotSelectedForEdit = location2;
                            break;
                        }
                    }
                }
            }
            catch
            {
            }
        }

        private void GraphViewMouseClick(object sender, MouseEventArgs e)
        {
            this._rect = new Rectangle(-1, -1, -1, -1);
        }

        private void GraphViewMouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if ((this._rect.X == -1) && (this._rect.Y == -1))
                {
                    this._rect = new Rectangle(e.X, e.Y, 0, 0);
                }
                Rectangle rectangle = new Rectangle(this._rect.Left, this._rect.Top, e.X - this._rect.Left, e.Y - this._rect.Top);
                this._rect = ((rectangle.Width >= 0) || (rectangle.Height >= 0)) ? (((rectangle.Width >= 0) || (rectangle.Height <= 0)) ? new Rectangle(this._rect.Left, this._rect.Top, Math.Abs((int) (e.X - this._rect.Left)), Math.Abs((int) (e.Y - this._rect.Top))) : new Rectangle(e.X - this._rect.Left, e.Y, Math.Abs(this._rect.Left), e.Y - this._rect.Top)) : new Rectangle(e.X, e.Y, this._rect.Left, this._rect.Top);
                foreach (Location location in this._pathProfile.GetGraph.GetNodes())
                {
                    float num = this.OffsetX(location.X, LazyLib.Wow.ObjectManager.MyPlayer.Location.X);
                    float num2 = this.OffsetY(location.Y, LazyLib.Wow.ObjectManager.MyPlayer.Location.Y);
                    Rectangle a = new Rectangle(((int) num2) - 6, ((int) num) - 6, 6, 6);
                    if (Rectangle.Intersect(a, this._rect) != Rectangle.Empty)
                    {
                        lock (this._selectedList)
                        {
                            if (!this._selectedList.Contains(location))
                            {
                                this._selectedList.Add(location);
                            }
                        }
                    }
                }
            }
        }

        private void GraphViewMouseUp(object sender, MouseEventArgs e)
        {
            this._rect = new Rectangle(-1, -1, -1, -1);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.superTabControlPanel1 = new SuperTabControlPanel();
            this.BePullRules = new DevComponents.AdvTree.AdvTree();
            this.nodeConnector3 = new NodeConnector();
            this.elementStyle3 = new ElementStyle();
            this.TabPull = new SuperTabItem();
            this.labelItem3 = new LabelItem();
            this.BeSaveBeheavior = new ButtonItem();
            this.BeComAddRule = new ButtonItem();
            this.labelItem1 = new LabelItem();
            this.BeComEditRule = new ButtonItem();
            this.labelItem2 = new LabelItem();
            this.BeComDeleteRule = new ButtonItem();
            this.superTabControl1 = new SuperTabControl();
            this.superTabControlPanel2 = new SuperTabControlPanel();
            this.groupPanel6 = new GroupPanel();
            this.LBTrainerCount = new LabelX();
            this.labelX9 = new LabelX();
            this.LBVendorCount = new LabelX();
            this.labelX7 = new LabelX();
            this.BtnAddTrainer = new ButtonX();
            this.BtnAddRepair = new ButtonX();
            this.groupPanel5 = new GroupPanel();
            this.ListSubProfiles = new DevComponents.AdvTree.AdvTree();
            this.groupPanel1 = new GroupPanel();
            this.nodeConnector1 = new NodeConnector();
            this.elementStyle1 = new ElementStyle();
            this.BtnRemove = new ButtonX();
            this.BtnAdd = new ButtonX();
            this.groupPanel4 = new GroupPanel();
            this.TBIgnore = new TextBoxX();
            this.labelX8 = new LabelX();
            this.LBIgnoreCount = new LabelX();
            this.BtnAddIgnore = new ButtonX();
            this.TBFactionList = new TextBoxX();
            this.labelX3 = new LabelX();
            this.labelX13 = new LabelX();
            this.labelX1 = new LabelX();
            this.SpotRoamDistance = new IntegerInput();
            this.labelX2 = new LabelX();
            this.labelX11 = new LabelX();
            this.CBSpotOrder = new CheckBoxX();
            this.UMaxLevel = new IntegerInput();
            this.labelX4 = new LabelX();
            this.UMinLevel = new IntegerInput();
            this.labelX5 = new LabelX();
            this.TBName = new TextBoxX();
            this.labelX10 = new LabelX();
            this.PMinLevel = new IntegerInput();
            this.PMaxLevel = new IntegerInput();
            this.LBFactionCount = new LabelX();
            this.BtnFaction = new ButtonX();
            this.LBSpotCount = new LabelX();
            this.BtnAddSpot = new ButtonX();
            this.superTabItem1 = new SuperTabItem();
            this.superTabControlPanel3 = new SuperTabControlPanel();
            this.groupPanel3 = new GroupPanel();
            this.BtnRemoveSpot = new ButtonX();
            this.OtherAddSpot = new ButtonX();
            this.CBShowPullZones = new CheckBoxX();
            this.GraphView = new GroupPanel();
            this.groupPanel2 = new GroupPanel();
            this.BtnClearSelection = new ButtonX();
            this.BtnAddNode = new ButtonX();
            this.BtnRemoveNode = new ButtonX();
            this.BtnAddNodeNo = new ButtonX();
            this.BtnAddNodeConnection = new ButtonX();
            this.labelX14 = new LabelX();
            this.INodeDistance = new IntegerInput();
            this.CBRecordGraph = new CheckBoxX();
            this.SelectNodeType = new ComboBoxEx();
            this.comboItem1 = new ComboItem();
            this.comboItem2 = new ComboItem();
            this.labelX6 = new LabelX();
            this.superTabItem2 = new SuperTabItem();
            this.BtnLoad = new ButtonX();
            this.BtnSave = new ButtonX();
            this.BtnNew = new ButtonX();
            this.superTooltip1 = new SuperTooltip();
            this.styleManager1 = new StyleManager(this.components);
            this.CBTopMost = new CheckBoxX();
            this.labelX12 = new LabelX();
            this.DrawEdgesValue = new IntegerInput();
            this.labelX15 = new LabelX();
            this.comboItem3 = new ComboItem();
            this.superTabControlPanel1.SuspendLayout();
            this.BePullRules.BeginInit();
            ((ISupportInitialize) this.superTabControl1).BeginInit();
            this.superTabControl1.SuspendLayout();
            this.superTabControlPanel2.SuspendLayout();
            this.groupPanel6.SuspendLayout();
            this.groupPanel5.SuspendLayout();
            this.ListSubProfiles.BeginInit();
            this.ListSubProfiles.SuspendLayout();
            this.groupPanel4.SuspendLayout();
            this.SpotRoamDistance.BeginInit();
            this.UMaxLevel.BeginInit();
            this.UMinLevel.BeginInit();
            this.PMinLevel.BeginInit();
            this.PMaxLevel.BeginInit();
            this.superTabControlPanel3.SuspendLayout();
            this.groupPanel3.SuspendLayout();
            this.groupPanel2.SuspendLayout();
            this.INodeDistance.BeginInit();
            this.DrawEdgesValue.BeginInit();
            base.SuspendLayout();
            this.superTabControlPanel1.Controls.Add(this.BePullRules);
            this.superTabControlPanel1.Dock = DockStyle.Fill;
            this.superTabControlPanel1.Location = new Point(0, 0);
            this.superTabControlPanel1.Name = "superTabControlPanel1";
            this.superTabControlPanel1.Size = new Size(0x214, 0x133);
            this.superTabControlPanel1.TabIndex = 0;
            this.superTabControlPanel1.TabItem = this.TabPull;
            this.BePullRules.AccessibleRole = AccessibleRole.Outline;
            this.BePullRules.AllowDrop = true;
            this.BePullRules.BackColor = SystemColors.Window;
            this.BePullRules.BackgroundStyle.Class = "TreeBorderKey";
            this.BePullRules.BackgroundStyle.CornerType = eCornerType.Square;
            this.BePullRules.Dock = DockStyle.Top;
            this.BePullRules.Location = new Point(0, 0);
            this.BePullRules.Name = "BePullRules";
            this.BePullRules.NodesConnector = this.nodeConnector3;
            this.BePullRules.NodeStyle = this.elementStyle3;
            this.BePullRules.PathSeparator = ";";
            this.BePullRules.Size = new Size(0x214, 0x116);
            this.BePullRules.Styles.Add(this.elementStyle3);
            this.BePullRules.TabIndex = 1;
            this.nodeConnector3.LineColor = SystemColors.ControlText;
            this.elementStyle3.CornerType = eCornerType.Square;
            this.elementStyle3.Name = "elementStyle3";
            this.elementStyle3.TextColor = SystemColors.ControlText;
            this.TabPull.AttachedControl = this.superTabControlPanel1;
            this.TabPull.GlobalItem = false;
            this.TabPull.Name = "TabPull";
            this.TabPull.Text = "Pull";
            this.labelItem3.Name = "labelItem3";
            this.labelItem3.Text = "                                                           ";
            this.BeSaveBeheavior.Name = "BeSaveBeheavior";
            this.BeSaveBeheavior.Text = "Save Behavior";
            this.BeComAddRule.Name = "BeComAddRule";
            this.BeComAddRule.Text = "Add Rule";
            this.labelItem1.Name = "labelItem1";
            this.labelItem1.Text = "              ";
            this.BeComEditRule.Name = "BeComEditRule";
            this.BeComEditRule.Text = "Double click on rule to edit";
            this.labelItem2.Name = "labelItem2";
            this.labelItem2.Text = "              ";
            this.BeComDeleteRule.Name = "BeComDeleteRule";
            this.BeComDeleteRule.Text = "Delete Rule";
            this.superTabControl1.ControlBox.CloseBox.Name = "";
            this.superTabControl1.ControlBox.MenuBox.Name = "";
            this.superTabControl1.ControlBox.Name = "";
            BaseItem[] items = new BaseItem[] { this.superTabControl1.ControlBox.MenuBox, this.superTabControl1.ControlBox.CloseBox };
            this.superTabControl1.ControlBox.SubItems.AddRange(items);
            this.superTabControl1.Controls.Add(this.superTabControlPanel3);
            this.superTabControl1.Controls.Add(this.superTabControlPanel2);
            this.superTabControl1.Location = new Point(1, 0);
            this.superTabControl1.Name = "superTabControl1";
            this.superTabControl1.ReorderTabsEnabled = true;
            this.superTabControl1.SelectedTabFont = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold);
            this.superTabControl1.SelectedTabIndex = 1;
            this.superTabControl1.Size = new Size(0x2ca, 0x147);
            this.superTabControl1.TabFont = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.superTabControl1.TabIndex = 0;
            BaseItem[] itemArray2 = new BaseItem[] { this.superTabItem2, this.superTabItem1 };
            this.superTabControl1.Tabs.AddRange(itemArray2);
            this.superTabControl1.TabStyle = eSuperTabStyle.WinMediaPlayer12;
            this.superTabControl1.Text = "superTabControl1";
            this.superTabControlPanel2.Controls.Add(this.groupPanel6);
            this.superTabControlPanel2.Controls.Add(this.groupPanel5);
            this.superTabControlPanel2.Controls.Add(this.groupPanel4);
            this.superTabControlPanel2.Dock = DockStyle.Fill;
            this.superTabControlPanel2.Location = new Point(0, 0x17);
            this.superTabControlPanel2.Name = "superTabControlPanel2";
            this.superTabControlPanel2.Size = new Size(0x2ca, 0x130);
            this.superTabControlPanel2.TabIndex = 1;
            this.superTabControlPanel2.TabItem = this.superTabItem1;
            this.superTabControlPanel2.ThemeAware = true;
            this.groupPanel6.BackColor = Color.White;
            this.groupPanel6.CanvasColor = SystemColors.Control;
            this.groupPanel6.ColorSchemeStyle = eDotNetBarStyle.Office2007;
            this.groupPanel6.Controls.Add(this.LBTrainerCount);
            this.groupPanel6.Controls.Add(this.labelX9);
            this.groupPanel6.Controls.Add(this.LBVendorCount);
            this.groupPanel6.Controls.Add(this.labelX7);
            this.groupPanel6.Controls.Add(this.BtnAddTrainer);
            this.groupPanel6.Controls.Add(this.BtnAddRepair);
            this.groupPanel6.DisabledBackColor = Color.Empty;
            this.groupPanel6.Location = new Point(0x216, 9);
            this.groupPanel6.Name = "groupPanel6";
            this.groupPanel6.Size = new Size(0xb1, 0x124);
            this.groupPanel6.Style.BackColor2SchemePart = eColorSchemePart.PanelBackground2;
            this.groupPanel6.Style.BackColorGradientAngle = 90;
            this.groupPanel6.Style.BackColorSchemePart = eColorSchemePart.PanelBackground;
            this.groupPanel6.Style.BorderBottom = eStyleBorderType.Solid;
            this.groupPanel6.Style.BorderBottomWidth = 1;
            this.groupPanel6.Style.BorderColorSchemePart = eColorSchemePart.PanelBorder;
            this.groupPanel6.Style.BorderLeft = eStyleBorderType.Solid;
            this.groupPanel6.Style.BorderLeftWidth = 1;
            this.groupPanel6.Style.BorderRight = eStyleBorderType.Solid;
            this.groupPanel6.Style.BorderRightWidth = 1;
            this.groupPanel6.Style.BorderTop = eStyleBorderType.Solid;
            this.groupPanel6.Style.BorderTopWidth = 1;
            this.groupPanel6.Style.CornerDiameter = 4;
            this.groupPanel6.Style.CornerType = eCornerType.Rounded;
            this.groupPanel6.Style.TextAlignment = eStyleTextAlignment.Center;
            this.groupPanel6.Style.TextColorSchemePart = eColorSchemePart.PanelText;
            this.groupPanel6.Style.TextLineAlignment = eStyleTextAlignment.Near;
            this.groupPanel6.StyleMouseDown.CornerType = eCornerType.Square;
            this.groupPanel6.StyleMouseOver.CornerType = eCornerType.Square;
            this.groupPanel6.TabIndex = 0x1f;
            this.groupPanel6.Text = "Global";
            this.LBTrainerCount.BackColor = Color.Transparent;
            this.LBTrainerCount.BackgroundStyle.CornerType = eCornerType.Square;
            this.LBTrainerCount.Location = new Point(0x4d, 0x20);
            this.LBTrainerCount.Name = "LBTrainerCount";
            this.LBTrainerCount.Size = new Size(0x1b, 0x17);
            this.LBTrainerCount.TabIndex = 0x3b;
            this.LBTrainerCount.Text = "0";
            this.LBTrainerCount.Visible = false;
            this.labelX9.BackColor = Color.Transparent;
            this.labelX9.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX9.Location = new Point(3, 0x1f);
            this.labelX9.Name = "labelX9";
            this.labelX9.Size = new Size(0x44, 0x17);
            this.labelX9.TabIndex = 0x3a;
            this.labelX9.Text = "Trainer count:";
            this.labelX9.Visible = false;
            this.LBVendorCount.BackColor = Color.Transparent;
            this.LBVendorCount.BackgroundStyle.CornerType = eCornerType.Square;
            this.LBVendorCount.Location = new Point(0x4d, 4);
            this.LBVendorCount.Name = "LBVendorCount";
            this.LBVendorCount.Size = new Size(0x1b, 0x17);
            this.LBVendorCount.TabIndex = 0x39;
            this.LBVendorCount.Text = "0";
            this.labelX7.BackColor = Color.Transparent;
            this.labelX7.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX7.Location = new Point(3, 4);
            this.labelX7.Name = "labelX7";
            this.labelX7.Size = new Size(0x4b, 0x17);
            this.labelX7.TabIndex = 0x38;
            this.labelX7.Text = "Vendor count:";
            this.BtnAddTrainer.AccessibleRole = AccessibleRole.PushButton;
            this.BtnAddTrainer.ColorTable = eButtonColor.OrangeWithBackground;
            this.BtnAddTrainer.Location = new Point(110, 0x20);
            this.BtnAddTrainer.Name = "BtnAddTrainer";
            this.BtnAddTrainer.Size = new Size(0x37, 0x17);
            this.BtnAddTrainer.Style = eDotNetBarStyle.StyleManagerControlled;
            this.BtnAddTrainer.TabIndex = 0x37;
            this.BtnAddTrainer.Text = "+";
            this.BtnAddTrainer.Visible = false;
            this.BtnAddTrainer.Click += new EventHandler(this.BtnAddTrainerClick);
            this.BtnAddRepair.AccessibleRole = AccessibleRole.PushButton;
            this.BtnAddRepair.ColorTable = eButtonColor.OrangeWithBackground;
            this.BtnAddRepair.Location = new Point(110, 3);
            this.BtnAddRepair.Name = "BtnAddRepair";
            this.BtnAddRepair.Size = new Size(0x37, 0x17);
            this.BtnAddRepair.Style = eDotNetBarStyle.StyleManagerControlled;
            this.BtnAddRepair.TabIndex = 0x25;
            this.BtnAddRepair.Text = "+";
            this.BtnAddRepair.Click += new EventHandler(this.BtnAddRepairClick);
            this.groupPanel5.BackColor = Color.White;
            this.groupPanel5.CanvasColor = SystemColors.Control;
            this.groupPanel5.ColorSchemeStyle = eDotNetBarStyle.Office2007;
            this.groupPanel5.Controls.Add(this.ListSubProfiles);
            this.groupPanel5.Controls.Add(this.BtnRemove);
            this.groupPanel5.Controls.Add(this.BtnAdd);
            this.groupPanel5.DisabledBackColor = Color.Empty;
            this.groupPanel5.Location = new Point(6, 9);
            this.groupPanel5.Name = "groupPanel5";
            this.groupPanel5.Size = new Size(0xc7, 0x124);
            this.groupPanel5.Style.BackColor2SchemePart = eColorSchemePart.PanelBackground2;
            this.groupPanel5.Style.BackColorGradientAngle = 90;
            this.groupPanel5.Style.BackColorSchemePart = eColorSchemePart.PanelBackground;
            this.groupPanel5.Style.BorderBottom = eStyleBorderType.Solid;
            this.groupPanel5.Style.BorderBottomWidth = 1;
            this.groupPanel5.Style.BorderColorSchemePart = eColorSchemePart.PanelBorder;
            this.groupPanel5.Style.BorderLeft = eStyleBorderType.Solid;
            this.groupPanel5.Style.BorderLeftWidth = 1;
            this.groupPanel5.Style.BorderRight = eStyleBorderType.Solid;
            this.groupPanel5.Style.BorderRightWidth = 1;
            this.groupPanel5.Style.BorderTop = eStyleBorderType.Solid;
            this.groupPanel5.Style.BorderTopWidth = 1;
            this.groupPanel5.Style.CornerDiameter = 4;
            this.groupPanel5.Style.CornerType = eCornerType.Rounded;
            this.groupPanel5.Style.TextAlignment = eStyleTextAlignment.Center;
            this.groupPanel5.Style.TextColorSchemePart = eColorSchemePart.PanelText;
            this.groupPanel5.Style.TextLineAlignment = eStyleTextAlignment.Near;
            this.groupPanel5.StyleMouseDown.CornerType = eCornerType.Square;
            this.groupPanel5.StyleMouseOver.CornerType = eCornerType.Square;
            this.groupPanel5.TabIndex = 30;
            this.groupPanel5.Text = "Sub profiles";
            this.ListSubProfiles.AccessibleRole = AccessibleRole.Outline;
            this.ListSubProfiles.AllowDrop = true;
            this.ListSubProfiles.BackColor = SystemColors.Window;
            this.ListSubProfiles.BackgroundStyle.Class = "TreeBorderKey";
            this.ListSubProfiles.BackgroundStyle.CornerType = eCornerType.Square;
            this.ListSubProfiles.Controls.Add(this.groupPanel1);
            this.ListSubProfiles.DragDropEnabled = false;
            this.ListSubProfiles.Location = new Point(3, 4);
            this.ListSubProfiles.Name = "ListSubProfiles";
            this.ListSubProfiles.NodesConnector = this.nodeConnector1;
            this.ListSubProfiles.NodeStyle = this.elementStyle1;
            this.ListSubProfiles.PathSeparator = ";";
            this.ListSubProfiles.Size = new Size(0xb5, 0xe9);
            this.ListSubProfiles.Styles.Add(this.elementStyle1);
            this.ListSubProfiles.TabIndex = 0x15;
            this.ListSubProfiles.Text = "advTree1";
            this.ListSubProfiles.NodeClick += new TreeNodeMouseEventHandler(this.ListSubProfilesNodeClick);
            this.ListSubProfiles.SelectedIndexChanged += new EventHandler(this.ListSubProfilesSelectedIndexChanged);
            this.groupPanel1.CanvasColor = SystemColors.Control;
            this.groupPanel1.ColorSchemeStyle = eDotNetBarStyle.Office2007;
            this.groupPanel1.DisabledBackColor = Color.Empty;
            this.groupPanel1.Location = new Point(0xca, 0x56);
            this.groupPanel1.Name = "groupPanel1";
            this.groupPanel1.Size = new Size(200, 100);
            this.groupPanel1.Style.BackColor2SchemePart = eColorSchemePart.PanelBackground2;
            this.groupPanel1.Style.BackColorGradientAngle = 90;
            this.groupPanel1.Style.BackColorSchemePart = eColorSchemePart.PanelBackground;
            this.groupPanel1.Style.BorderBottom = eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderBottomWidth = 1;
            this.groupPanel1.Style.BorderColorSchemePart = eColorSchemePart.PanelBorder;
            this.groupPanel1.Style.BorderLeft = eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderLeftWidth = 1;
            this.groupPanel1.Style.BorderRight = eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderRightWidth = 1;
            this.groupPanel1.Style.BorderTop = eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderTopWidth = 1;
            this.groupPanel1.Style.CornerDiameter = 4;
            this.groupPanel1.Style.CornerType = eCornerType.Rounded;
            this.groupPanel1.Style.TextAlignment = eStyleTextAlignment.Center;
            this.groupPanel1.Style.TextColorSchemePart = eColorSchemePart.PanelText;
            this.groupPanel1.Style.TextLineAlignment = eStyleTextAlignment.Near;
            this.groupPanel1.StyleMouseDown.CornerType = eCornerType.Square;
            this.groupPanel1.StyleMouseOver.CornerType = eCornerType.Square;
            this.groupPanel1.TabIndex = 6;
            this.groupPanel1.Text = "groupPanel1";
            this.nodeConnector1.LineColor = SystemColors.ControlText;
            this.elementStyle1.CornerType = eCornerType.Square;
            this.elementStyle1.Name = "elementStyle1";
            this.elementStyle1.TextColor = SystemColors.ControlText;
            this.BtnRemove.AccessibleRole = AccessibleRole.PushButton;
            this.BtnRemove.ColorTable = eButtonColor.OrangeWithBackground;
            this.BtnRemove.Location = new Point(0x6c, 0xf3);
            this.BtnRemove.Name = "BtnRemove";
            this.BtnRemove.Size = new Size(0x4c, 0x17);
            this.BtnRemove.Style = eDotNetBarStyle.StyleManagerControlled;
            this.BtnRemove.TabIndex = 0x17;
            this.BtnRemove.Text = "Remove";
            this.BtnRemove.Click += new EventHandler(this.BtnRemoveClick);
            this.BtnAdd.AccessibleRole = AccessibleRole.PushButton;
            this.BtnAdd.ColorTable = eButtonColor.OrangeWithBackground;
            this.BtnAdd.Location = new Point(3, 0xf3);
            this.BtnAdd.Name = "BtnAdd";
            this.BtnAdd.Size = new Size(0x63, 0x17);
            this.BtnAdd.Style = eDotNetBarStyle.StyleManagerControlled;
            this.BtnAdd.TabIndex = 2;
            this.BtnAdd.Text = "Add";
            this.BtnAdd.Click += new EventHandler(this.BtnAddClick);
            this.groupPanel4.BackColor = Color.White;
            this.groupPanel4.CanvasColor = SystemColors.Control;
            this.groupPanel4.ColorSchemeStyle = eDotNetBarStyle.Office2007;
            this.groupPanel4.Controls.Add(this.TBIgnore);
            this.groupPanel4.Controls.Add(this.labelX8);
            this.groupPanel4.Controls.Add(this.LBIgnoreCount);
            this.groupPanel4.Controls.Add(this.BtnAddIgnore);
            this.groupPanel4.Controls.Add(this.TBFactionList);
            this.groupPanel4.Controls.Add(this.labelX3);
            this.groupPanel4.Controls.Add(this.labelX13);
            this.groupPanel4.Controls.Add(this.labelX1);
            this.groupPanel4.Controls.Add(this.SpotRoamDistance);
            this.groupPanel4.Controls.Add(this.labelX2);
            this.groupPanel4.Controls.Add(this.labelX11);
            this.groupPanel4.Controls.Add(this.CBSpotOrder);
            this.groupPanel4.Controls.Add(this.UMaxLevel);
            this.groupPanel4.Controls.Add(this.labelX4);
            this.groupPanel4.Controls.Add(this.UMinLevel);
            this.groupPanel4.Controls.Add(this.labelX5);
            this.groupPanel4.Controls.Add(this.TBName);
            this.groupPanel4.Controls.Add(this.labelX10);
            this.groupPanel4.Controls.Add(this.PMinLevel);
            this.groupPanel4.Controls.Add(this.PMaxLevel);
            this.groupPanel4.Controls.Add(this.LBFactionCount);
            this.groupPanel4.Controls.Add(this.BtnFaction);
            this.groupPanel4.Controls.Add(this.LBSpotCount);
            this.groupPanel4.Controls.Add(this.BtnAddSpot);
            this.groupPanel4.DisabledBackColor = Color.Empty;
            this.groupPanel4.Location = new Point(0xd3, 9);
            this.groupPanel4.Name = "groupPanel4";
            this.groupPanel4.Size = new Size(0x13d, 0x124);
            this.groupPanel4.Style.BackColor2SchemePart = eColorSchemePart.PanelBackground2;
            this.groupPanel4.Style.BackColorGradientAngle = 90;
            this.groupPanel4.Style.BackColorSchemePart = eColorSchemePart.PanelBackground;
            this.groupPanel4.Style.BorderBottom = eStyleBorderType.Solid;
            this.groupPanel4.Style.BorderBottomWidth = 1;
            this.groupPanel4.Style.BorderColorSchemePart = eColorSchemePart.PanelBorder;
            this.groupPanel4.Style.BorderLeft = eStyleBorderType.Solid;
            this.groupPanel4.Style.BorderLeftWidth = 1;
            this.groupPanel4.Style.BorderRight = eStyleBorderType.Solid;
            this.groupPanel4.Style.BorderRightWidth = 1;
            this.groupPanel4.Style.BorderTop = eStyleBorderType.Solid;
            this.groupPanel4.Style.BorderTopWidth = 1;
            this.groupPanel4.Style.CornerDiameter = 4;
            this.groupPanel4.Style.CornerType = eCornerType.Rounded;
            this.groupPanel4.Style.TextAlignment = eStyleTextAlignment.Center;
            this.groupPanel4.Style.TextColorSchemePart = eColorSchemePart.PanelText;
            this.groupPanel4.Style.TextLineAlignment = eStyleTextAlignment.Near;
            this.groupPanel4.StyleMouseDown.CornerType = eCornerType.Square;
            this.groupPanel4.StyleMouseOver.CornerType = eCornerType.Square;
            this.groupPanel4.TabIndex = 30;
            this.groupPanel4.Text = "Sub profile";
            this.TBIgnore.Border.Class = "TextBoxBorder";
            this.TBIgnore.Border.CornerType = eCornerType.Square;
            this.TBIgnore.Location = new Point(0x65, 0xa3);
            this.TBIgnore.Name = "TBIgnore";
            this.TBIgnore.Size = new Size(190, 20);
            this.TBIgnore.TabIndex = 0x24;
            this.TBIgnore.TextChanged += new EventHandler(this.TbIgnoreTextChanged);
            this.labelX8.BackColor = Color.Transparent;
            this.labelX8.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX8.Location = new Point(3, 0x8a);
            this.labelX8.Name = "labelX8";
            this.labelX8.Size = new Size(0x34, 0x17);
            this.labelX8.TabIndex = 0x21;
            this.labelX8.Text = "Ignore:";
            this.LBIgnoreCount.BackColor = Color.Transparent;
            this.LBIgnoreCount.BackgroundStyle.CornerType = eCornerType.Square;
            this.LBIgnoreCount.Location = new Point(0x65, 0x8a);
            this.LBIgnoreCount.Name = "LBIgnoreCount";
            this.LBIgnoreCount.Size = new Size(0x1b, 0x17);
            this.LBIgnoreCount.TabIndex = 0x22;
            this.LBIgnoreCount.Text = "0";
            this.BtnAddIgnore.AccessibleRole = AccessibleRole.PushButton;
            this.BtnAddIgnore.ColorTable = eButtonColor.OrangeWithBackground;
            this.BtnAddIgnore.Enabled = false;
            this.BtnAddIgnore.Location = new Point(0xd3, 0x89);
            this.BtnAddIgnore.Name = "BtnAddIgnore";
            this.BtnAddIgnore.Size = new Size(80, 0x17);
            this.BtnAddIgnore.Style = eDotNetBarStyle.StyleManagerControlled;
            this.BtnAddIgnore.TabIndex = 0x23;
            this.BtnAddIgnore.Text = "+";
            this.BtnAddIgnore.Click += new EventHandler(this.BtnAddIgnoreClick);
            this.TBFactionList.Border.Class = "TextBoxBorder";
            this.TBFactionList.Border.CornerType = eCornerType.Square;
            this.TBFactionList.Enabled = false;
            this.TBFactionList.Location = new Point(0x65, 0x6f);
            this.TBFactionList.Name = "TBFactionList";
            this.TBFactionList.Size = new Size(190, 20);
            this.TBFactionList.TabIndex = 0x20;
            this.TBFactionList.TextChanged += new EventHandler(this.TbIgnoreListTextChanged);
            this.labelX3.BackColor = Color.Transparent;
            this.labelX3.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX3.Location = new Point(0xc0, 0x34);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new Size(10, 0x17);
            this.labelX3.TabIndex = 0x1f;
            this.labelX3.Text = "-";
            this.labelX13.BackColor = Color.Transparent;
            this.labelX13.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX13.Location = new Point(0xc1, 0x1b);
            this.labelX13.Name = "labelX13";
            this.labelX13.Size = new Size(10, 0x17);
            this.labelX13.TabIndex = 30;
            this.labelX13.Text = "-";
            this.labelX1.BackColor = Color.Transparent;
            this.labelX1.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX1.Location = new Point(3, 2);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new Size(0x56, 0x17);
            this.labelX1.TabIndex = 8;
            this.labelX1.Text = "Unique Name:";
            this.SpotRoamDistance.BackgroundStyle.Class = "DateTimeInputBackground";
            this.SpotRoamDistance.BackgroundStyle.CornerType = eCornerType.Square;
            this.SpotRoamDistance.ButtonFreeText.Shortcut = eShortcut.F2;
            this.SpotRoamDistance.Enabled = false;
            this.SpotRoamDistance.Location = new Point(0x65, 0xd8);
            this.SpotRoamDistance.Name = "SpotRoamDistance";
            this.SpotRoamDistance.ShowUpDown = true;
            this.SpotRoamDistance.Size = new Size(80, 20);
            this.SpotRoamDistance.TabIndex = 0x1d;
            this.SpotRoamDistance.ValueChanged += new EventHandler(this.SpotRoamDistanceValueChanged);
            this.labelX2.BackColor = Color.Transparent;
            this.labelX2.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX2.Location = new Point(3, 0x1d);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new Size(0x5c, 0x17);
            this.labelX2.TabIndex = 9;
            this.labelX2.Text = "Player level range:";
            this.labelX11.BackColor = Color.Transparent;
            this.labelX11.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX11.Location = new Point(3, 0xd8);
            this.labelX11.Name = "labelX11";
            this.labelX11.Size = new Size(0x5c, 0x17);
            this.labelX11.TabIndex = 0x1c;
            this.labelX11.Text = "Spot pull distance:";
            this.CBSpotOrder.BackColor = Color.Transparent;
            this.CBSpotOrder.BackgroundStyle.CornerType = eCornerType.Square;
            this.CBSpotOrder.Enabled = false;
            this.CBSpotOrder.Location = new Point(0x65, 0xf2);
            this.CBSpotOrder.Name = "CBSpotOrder";
            this.CBSpotOrder.Size = new Size(0x81, 0x17);
            this.CBSpotOrder.Style = eDotNetBarStyle.StyleManagerControlled;
            this.CBSpotOrder.TabIndex = 20;
            this.CBSpotOrder.Text = "Follow spots in order";
            this.CBSpotOrder.CheckedChanged += new EventHandler(this.CbSpotOrderCheckedChanged);
            this.UMaxLevel.BackgroundStyle.Class = "DateTimeInputBackground";
            this.UMaxLevel.BackgroundStyle.CornerType = eCornerType.Square;
            this.UMaxLevel.ButtonFreeText.Shortcut = eShortcut.F2;
            this.UMaxLevel.Enabled = false;
            this.UMaxLevel.Location = new Point(0xd3, 0x37);
            this.UMaxLevel.Name = "UMaxLevel";
            this.UMaxLevel.ShowUpDown = true;
            this.UMaxLevel.Size = new Size(80, 20);
            this.UMaxLevel.TabIndex = 0x1b;
            this.UMaxLevel.ValueChanged += new EventHandler(this.UMaxLevelValueChanged);
            this.labelX4.BackColor = Color.Transparent;
            this.labelX4.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX4.Location = new Point(3, 0x53);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new Size(0x34, 0x17);
            this.labelX4.TabIndex = 11;
            this.labelX4.Text = "Factions:";
            this.UMinLevel.BackgroundStyle.Class = "DateTimeInputBackground";
            this.UMinLevel.BackgroundStyle.CornerType = eCornerType.Square;
            this.UMinLevel.ButtonFreeText.Shortcut = eShortcut.F2;
            this.UMinLevel.Enabled = false;
            this.UMinLevel.Location = new Point(0x65, 0x37);
            this.UMinLevel.Name = "UMinLevel";
            this.UMinLevel.ShowUpDown = true;
            this.UMinLevel.Size = new Size(80, 20);
            this.UMinLevel.TabIndex = 0x1a;
            this.UMinLevel.ValueChanged += new EventHandler(this.UMinLevelValueChanged);
            this.labelX5.BackColor = Color.Transparent;
            this.labelX5.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX5.Location = new Point(3, 0xbb);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new Size(0x2e, 0x17);
            this.labelX5.TabIndex = 12;
            this.labelX5.Text = "Spots:";
            this.TBName.Border.Class = "TextBoxBorder";
            this.TBName.Border.CornerType = eCornerType.Square;
            this.TBName.Enabled = false;
            this.TBName.Location = new Point(0x65, 4);
            this.TBName.Name = "TBName";
            this.TBName.Size = new Size(190, 20);
            this.TBName.TabIndex = 13;
            this.TBName.TextChanged += new EventHandler(this.TbNameTextChanged);
            this.labelX10.BackColor = Color.Transparent;
            this.labelX10.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX10.Location = new Point(3, 0x36);
            this.labelX10.Name = "labelX10";
            this.labelX10.Size = new Size(0x5c, 0x17);
            this.labelX10.TabIndex = 0x18;
            this.labelX10.Text = "Mob level range:";
            this.PMinLevel.BackgroundStyle.Class = "DateTimeInputBackground";
            this.PMinLevel.BackgroundStyle.CornerType = eCornerType.Square;
            this.PMinLevel.ButtonFreeText.Shortcut = eShortcut.F2;
            this.PMinLevel.Enabled = false;
            this.PMinLevel.Location = new Point(0x65, 0x1d);
            this.PMinLevel.Name = "PMinLevel";
            this.PMinLevel.ShowUpDown = true;
            this.PMinLevel.Size = new Size(80, 20);
            this.PMinLevel.TabIndex = 14;
            this.PMinLevel.ValueChanged += new EventHandler(this.MinLevelValueChanged);
            this.PMaxLevel.BackgroundStyle.Class = "DateTimeInputBackground";
            this.PMaxLevel.BackgroundStyle.CornerType = eCornerType.Square;
            this.PMaxLevel.ButtonFreeText.Shortcut = eShortcut.F2;
            this.PMaxLevel.Enabled = false;
            this.PMaxLevel.Location = new Point(0xd3, 30);
            this.PMaxLevel.Name = "PMaxLevel";
            this.PMaxLevel.ShowUpDown = true;
            this.PMaxLevel.Size = new Size(80, 20);
            this.PMaxLevel.TabIndex = 15;
            this.PMaxLevel.ValueChanged += new EventHandler(this.MaxLevelValueChanged);
            this.LBFactionCount.BackColor = Color.Transparent;
            this.LBFactionCount.BackgroundStyle.CornerType = eCornerType.Square;
            this.LBFactionCount.Location = new Point(0x65, 0x53);
            this.LBFactionCount.Name = "LBFactionCount";
            this.LBFactionCount.Size = new Size(0x1b, 0x17);
            this.LBFactionCount.TabIndex = 0x10;
            this.LBFactionCount.Text = "0";
            this.BtnFaction.AccessibleRole = AccessibleRole.PushButton;
            this.BtnFaction.ColorTable = eButtonColor.OrangeWithBackground;
            this.BtnFaction.Enabled = false;
            this.BtnFaction.Location = new Point(0xd3, 0x52);
            this.BtnFaction.Name = "BtnFaction";
            this.BtnFaction.Size = new Size(80, 0x17);
            this.BtnFaction.Style = eDotNetBarStyle.StyleManagerControlled;
            this.BtnFaction.TabIndex = 0x11;
            this.BtnFaction.Text = "+";
            this.BtnFaction.Click += new EventHandler(this.BtnFactionClick);
            this.LBSpotCount.BackColor = Color.Transparent;
            this.LBSpotCount.BackgroundStyle.CornerType = eCornerType.Square;
            this.LBSpotCount.Location = new Point(0x65, 0xba);
            this.LBSpotCount.Name = "LBSpotCount";
            this.LBSpotCount.Size = new Size(0x1b, 0x17);
            this.LBSpotCount.TabIndex = 0x12;
            this.LBSpotCount.Text = "0";
            this.BtnAddSpot.AccessibleRole = AccessibleRole.PushButton;
            this.BtnAddSpot.ColorTable = eButtonColor.OrangeWithBackground;
            this.BtnAddSpot.Enabled = false;
            this.BtnAddSpot.Location = new Point(0xd3, 0xbb);
            this.BtnAddSpot.Name = "BtnAddSpot";
            this.BtnAddSpot.Size = new Size(80, 0x17);
            this.BtnAddSpot.Style = eDotNetBarStyle.StyleManagerControlled;
            this.BtnAddSpot.TabIndex = 0x13;
            this.BtnAddSpot.Text = "+";
            this.BtnAddSpot.Click += new EventHandler(this.BtnAddSpotClick);
            this.superTabItem1.AttachedControl = this.superTabControlPanel2;
            this.superTabItem1.GlobalItem = false;
            this.superTabItem1.Name = "superTabItem1";
            this.superTabItem1.Text = "Sub profiles";
            this.superTabControlPanel3.Controls.Add(this.groupPanel3);
            this.superTabControlPanel3.Controls.Add(this.GraphView);
            this.superTabControlPanel3.Controls.Add(this.groupPanel2);
            this.superTabControlPanel3.Dock = DockStyle.Fill;
            this.superTabControlPanel3.Location = new Point(0, 0x17);
            this.superTabControlPanel3.Name = "superTabControlPanel3";
            this.superTabControlPanel3.Size = new Size(0x2ca, 0x130);
            this.superTabControlPanel3.TabIndex = 0;
            this.superTabControlPanel3.TabItem = this.superTabItem2;
            this.superTabControlPanel3.ThemeAware = true;
            this.groupPanel3.BackColor = Color.White;
            this.groupPanel3.CanvasColor = SystemColors.Control;
            this.groupPanel3.ColorSchemeStyle = eDotNetBarStyle.Office2007;
            this.groupPanel3.Controls.Add(this.BtnRemoveSpot);
            this.groupPanel3.Controls.Add(this.OtherAddSpot);
            this.groupPanel3.Controls.Add(this.CBShowPullZones);
            this.groupPanel3.DisabledBackColor = Color.Empty;
            this.groupPanel3.Location = new Point(0x195, 0xbf);
            this.groupPanel3.Name = "groupPanel3";
            this.groupPanel3.Size = new Size(0x12f, 110);
            this.groupPanel3.Style.BackColor2SchemePart = eColorSchemePart.PanelBackground2;
            this.groupPanel3.Style.BackColorGradientAngle = 90;
            this.groupPanel3.Style.BackColorSchemePart = eColorSchemePart.PanelBackground;
            this.groupPanel3.Style.BorderBottom = eStyleBorderType.Solid;
            this.groupPanel3.Style.BorderBottomWidth = 1;
            this.groupPanel3.Style.BorderColorSchemePart = eColorSchemePart.PanelBorder;
            this.groupPanel3.Style.BorderLeft = eStyleBorderType.Solid;
            this.groupPanel3.Style.BorderLeftWidth = 1;
            this.groupPanel3.Style.BorderRight = eStyleBorderType.Solid;
            this.groupPanel3.Style.BorderRightWidth = 1;
            this.groupPanel3.Style.BorderTop = eStyleBorderType.Solid;
            this.groupPanel3.Style.BorderTopWidth = 1;
            this.groupPanel3.Style.CornerDiameter = 4;
            this.groupPanel3.Style.CornerType = eCornerType.Rounded;
            this.groupPanel3.Style.TextAlignment = eStyleTextAlignment.Center;
            this.groupPanel3.Style.TextColorSchemePart = eColorSchemePart.PanelText;
            this.groupPanel3.Style.TextLineAlignment = eStyleTextAlignment.Near;
            this.groupPanel3.StyleMouseDown.CornerType = eCornerType.Square;
            this.groupPanel3.StyleMouseOver.CornerType = eCornerType.Square;
            this.groupPanel3.TabIndex = 0x34;
            this.groupPanel3.Text = "Other";
            this.BtnRemoveSpot.AccessibleRole = AccessibleRole.PushButton;
            this.BtnRemoveSpot.ColorTable = eButtonColor.OrangeWithBackground;
            this.BtnRemoveSpot.Location = new Point(4, 0x20);
            this.BtnRemoveSpot.Name = "BtnRemoveSpot";
            this.BtnRemoveSpot.Size = new Size(0x4b, 0x17);
            this.BtnRemoveSpot.Style = eDotNetBarStyle.StyleManagerControlled;
            this.BtnRemoveSpot.TabIndex = 50;
            this.BtnRemoveSpot.Text = "Remove Spot";
            this.BtnRemoveSpot.Click += new EventHandler(this.BtnRemoveSpotClick);
            this.OtherAddSpot.AccessibleRole = AccessibleRole.PushButton;
            this.OtherAddSpot.ColorTable = eButtonColor.OrangeWithBackground;
            this.OtherAddSpot.Location = new Point(4, 3);
            this.OtherAddSpot.Name = "OtherAddSpot";
            this.OtherAddSpot.Size = new Size(0x4b, 0x17);
            this.OtherAddSpot.Style = eDotNetBarStyle.StyleManagerControlled;
            this.OtherAddSpot.TabIndex = 0x31;
            this.OtherAddSpot.Text = "Add spot";
            this.OtherAddSpot.Click += new EventHandler(this.OtherAddSpotClick);
            this.CBShowPullZones.BackColor = Color.Transparent;
            this.CBShowPullZones.BackgroundStyle.CornerType = eCornerType.Square;
            this.CBShowPullZones.Checked = true;
            this.CBShowPullZones.CheckState = CheckState.Checked;
            this.CBShowPullZones.CheckValue = "Y";
            this.CBShowPullZones.Location = new Point(0xc2, 0x63);
            this.CBShowPullZones.Name = "CBShowPullZones";
            this.CBShowPullZones.Size = new Size(100, 0x17);
            this.CBShowPullZones.Style = eDotNetBarStyle.StyleManagerControlled;
            this.CBShowPullZones.TabIndex = 0x30;
            this.CBShowPullZones.Text = "Show pull zones";
            this.GraphView.CanvasColor = SystemColors.Control;
            this.GraphView.ColorSchemeStyle = eDotNetBarStyle.Office2007;
            this.GraphView.DisabledBackColor = Color.Empty;
            this.GraphView.Dock = DockStyle.Left;
            this.GraphView.Location = new Point(0, 0);
            this.GraphView.Name = "GraphView";
            this.GraphView.Size = new Size(0x18f, 0x130);
            this.GraphView.Style.BackColor2SchemePart = eColorSchemePart.PanelBackground2;
            this.GraphView.Style.BackColorGradientAngle = 90;
            this.GraphView.Style.BackColorSchemePart = eColorSchemePart.PanelBackground;
            this.GraphView.Style.BorderBottom = eStyleBorderType.Solid;
            this.GraphView.Style.BorderBottomWidth = 1;
            this.GraphView.Style.BorderColorSchemePart = eColorSchemePart.PanelBorder;
            this.GraphView.Style.BorderLeft = eStyleBorderType.Solid;
            this.GraphView.Style.BorderLeftWidth = 1;
            this.GraphView.Style.BorderRight = eStyleBorderType.Solid;
            this.GraphView.Style.BorderRightWidth = 1;
            this.GraphView.Style.BorderTop = eStyleBorderType.Solid;
            this.GraphView.Style.BorderTopWidth = 1;
            this.GraphView.Style.CornerDiameter = 4;
            this.GraphView.Style.CornerType = eCornerType.Rounded;
            this.GraphView.Style.TextAlignment = eStyleTextAlignment.Center;
            this.GraphView.Style.TextColorSchemePart = eColorSchemePart.PanelText;
            this.GraphView.Style.TextLineAlignment = eStyleTextAlignment.Near;
            this.GraphView.StyleMouseDown.CornerType = eCornerType.Square;
            this.GraphView.StyleMouseOver.CornerType = eCornerType.Square;
            this.GraphView.TabIndex = 0;
            this.GraphView.Click += new EventHandler(this.GraphViewClick);
            this.GraphView.MouseClick += new MouseEventHandler(this.GraphViewMouseClick);
            this.GraphView.MouseMove += new MouseEventHandler(this.GraphViewMouseMove);
            this.GraphView.MouseUp += new MouseEventHandler(this.GraphViewMouseUp);
            this.groupPanel2.BackColor = Color.White;
            this.groupPanel2.CanvasColor = SystemColors.Control;
            this.groupPanel2.ColorSchemeStyle = eDotNetBarStyle.Office2007;
            this.groupPanel2.Controls.Add(this.BtnClearSelection);
            this.groupPanel2.Controls.Add(this.BtnAddNode);
            this.groupPanel2.Controls.Add(this.BtnRemoveNode);
            this.groupPanel2.Controls.Add(this.BtnAddNodeNo);
            this.groupPanel2.Controls.Add(this.BtnAddNodeConnection);
            this.groupPanel2.Controls.Add(this.labelX14);
            this.groupPanel2.Controls.Add(this.INodeDistance);
            this.groupPanel2.Controls.Add(this.CBRecordGraph);
            this.groupPanel2.Controls.Add(this.SelectNodeType);
            this.groupPanel2.Controls.Add(this.labelX6);
            this.groupPanel2.DisabledBackColor = Color.Empty;
            this.groupPanel2.Location = new Point(0x195, 3);
            this.groupPanel2.Name = "groupPanel2";
            this.groupPanel2.Size = new Size(0x12f, 0xb6);
            this.groupPanel2.Style.BackColor2SchemePart = eColorSchemePart.PanelBackground2;
            this.groupPanel2.Style.BackColorGradientAngle = 90;
            this.groupPanel2.Style.BackColorSchemePart = eColorSchemePart.PanelBackground;
            this.groupPanel2.Style.BorderBottom = eStyleBorderType.Solid;
            this.groupPanel2.Style.BorderBottomWidth = 1;
            this.groupPanel2.Style.BorderColorSchemePart = eColorSchemePart.PanelBorder;
            this.groupPanel2.Style.BorderLeft = eStyleBorderType.Solid;
            this.groupPanel2.Style.BorderLeftWidth = 1;
            this.groupPanel2.Style.BorderRight = eStyleBorderType.Solid;
            this.groupPanel2.Style.BorderRightWidth = 1;
            this.groupPanel2.Style.BorderTop = eStyleBorderType.Solid;
            this.groupPanel2.Style.BorderTopWidth = 1;
            this.groupPanel2.Style.CornerDiameter = 4;
            this.groupPanel2.Style.CornerType = eCornerType.Rounded;
            this.groupPanel2.Style.TextAlignment = eStyleTextAlignment.Center;
            this.groupPanel2.Style.TextColorSchemePart = eColorSchemePart.PanelText;
            this.groupPanel2.Style.TextLineAlignment = eStyleTextAlignment.Near;
            this.groupPanel2.StyleMouseDown.CornerType = eCornerType.Square;
            this.groupPanel2.StyleMouseOver.CornerType = eCornerType.Square;
            this.groupPanel2.TabIndex = 0x33;
            this.groupPanel2.Text = "Vertice functions";
            this.BtnClearSelection.AccessibleRole = AccessibleRole.PushButton;
            this.BtnClearSelection.ColorTable = eButtonColor.OrangeWithBackground;
            this.BtnClearSelection.Location = new Point(4, 0x89);
            this.BtnClearSelection.Name = "BtnClearSelection";
            this.BtnClearSelection.Size = new Size(0x61, 0x17);
            this.BtnClearSelection.Style = eDotNetBarStyle.StyleManagerControlled;
            this.BtnClearSelection.TabIndex = 0x2f;
            this.BtnClearSelection.Text = "Clear selection";
            this.BtnClearSelection.Click += new EventHandler(this.BtnClearSelectionClick);
            this.BtnAddNode.AccessibleRole = AccessibleRole.PushButton;
            this.BtnAddNode.ColorTable = eButtonColor.OrangeWithBackground;
            this.BtnAddNode.Location = new Point(4, 6);
            this.BtnAddNode.Name = "BtnAddNode";
            this.BtnAddNode.Size = new Size(0x61, 0x17);
            this.BtnAddNode.Style = eDotNetBarStyle.StyleManagerControlled;
            this.superTooltip1.SetSuperTooltip(this.BtnAddNode, new SuperTooltipInfo("Add node", "", "This will add a single node to the graph, the node will get connected to all nodes near it.", null, null, eTooltipColor.Gray));
            this.BtnAddNode.TabIndex = 4;
            this.BtnAddNode.Text = "Add vertice";
            this.BtnAddNode.Click += new EventHandler(this.BtnAddNodeClick);
            this.BtnRemoveNode.AccessibleRole = AccessibleRole.PushButton;
            this.BtnRemoveNode.ColorTable = eButtonColor.OrangeWithBackground;
            this.BtnRemoveNode.Location = new Point(4, 0x61);
            this.BtnRemoveNode.Name = "BtnRemoveNode";
            this.BtnRemoveNode.Size = new Size(0x61, 0x17);
            this.BtnRemoveNode.Style = eDotNetBarStyle.StyleManagerControlled;
            this.superTooltip1.SetSuperTooltip(this.BtnRemoveNode, new SuperTooltipInfo("Remove node", "", "Remove a node and all connections to it - mark the node by clicking it on the map (it should be dark orange)", null, null, eTooltipColor.Gray));
            this.BtnRemoveNode.TabIndex = 5;
            this.BtnRemoveNode.Text = "Remove vertice(s)";
            this.BtnRemoveNode.Click += new EventHandler(this.BtnRemoveNodeClick);
            this.BtnAddNodeNo.AccessibleRole = AccessibleRole.PushButton;
            this.BtnAddNodeNo.ColorTable = eButtonColor.OrangeWithBackground;
            this.BtnAddNodeNo.Location = new Point(4, 0x41);
            this.BtnAddNodeNo.Name = "BtnAddNodeNo";
            this.BtnAddNodeNo.Size = new Size(0x61, 0x1a);
            this.BtnAddNodeNo.Style = eDotNetBarStyle.StyleManagerControlled;
            this.superTooltip1.SetSuperTooltip(this.BtnAddNodeNo, new SuperTooltipInfo("Add node - no connection", "", "Add a single node without any connections", null, null, eTooltipColor.Gray));
            this.BtnAddNodeNo.TabIndex = 7;
            this.BtnAddNodeNo.Text = "Add vertice - no edge(s)";
            this.BtnAddNodeNo.Click += new EventHandler(this.BtnAddNodeNoClick);
            this.BtnAddNodeConnection.AccessibleRole = AccessibleRole.PushButton;
            this.BtnAddNodeConnection.ColorTable = eButtonColor.OrangeWithBackground;
            this.BtnAddNodeConnection.Location = new Point(4, 0x23);
            this.BtnAddNodeConnection.Name = "BtnAddNodeConnection";
            this.BtnAddNodeConnection.Size = new Size(0x61, 0x18);
            this.BtnAddNodeConnection.Style = eDotNetBarStyle.StyleManagerControlled;
            this.superTooltip1.SetSuperTooltip(this.BtnAddNodeConnection, new SuperTooltipInfo("Add node connection", "", "Add a connection between two nodes.\r\nClick the first node on the map then the second and then press this button.", null, null, eTooltipColor.Gray));
            this.BtnAddNodeConnection.TabIndex = 8;
            this.BtnAddNodeConnection.Text = "Add edge(s)";
            this.BtnAddNodeConnection.Click += new EventHandler(this.BtnAddNodeConnectionClick);
            this.labelX14.BackColor = Color.Transparent;
            this.labelX14.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX14.Location = new Point(0x6b, 3);
            this.labelX14.Name = "labelX14";
            this.labelX14.Size = new Size(0x4b, 0x17);
            this.labelX14.TabIndex = 11;
            this.labelX14.Text = "Vertice type:";
            this.INodeDistance.BackgroundStyle.Class = "DateTimeInputBackground";
            this.INodeDistance.BackgroundStyle.CornerType = eCornerType.Square;
            this.INodeDistance.ButtonFreeText.Shortcut = eShortcut.F2;
            this.INodeDistance.Location = new Point(0xc5, 0x25);
            this.INodeDistance.Name = "INodeDistance";
            this.INodeDistance.ShowUpDown = true;
            this.INodeDistance.Size = new Size(0x5f, 20);
            this.INodeDistance.TabIndex = 10;
            this.INodeDistance.Value = 15;
            this.INodeDistance.ValueChanged += new EventHandler(this.NodeDistanceValueChanged);
            this.CBRecordGraph.BackColor = Color.Transparent;
            this.CBRecordGraph.BackgroundStyle.CornerType = eCornerType.Square;
            this.CBRecordGraph.Location = new Point(0xc3, 0x89);
            this.CBRecordGraph.Name = "CBRecordGraph";
            this.CBRecordGraph.Size = new Size(0x61, 0x17);
            this.CBRecordGraph.Style = eDotNetBarStyle.StyleManagerControlled;
            this.superTooltip1.SetSuperTooltip(this.CBRecordGraph, new SuperTooltipInfo("Record graph", "", "Auto add a node for each X yards and connect it to other nodes.", null, null, eTooltipColor.Gray));
            this.CBRecordGraph.TabIndex = 3;
            this.CBRecordGraph.Text = "Record vertices";
            this.CBRecordGraph.CheckedChanged += new EventHandler(this.CbRecordGraphCheckedChanged);
            this.SelectNodeType.DisplayMember = "Text";
            this.SelectNodeType.DrawMode = DrawMode.OwnerDrawFixed;
            this.SelectNodeType.DropDownStyle = ComboBoxStyle.DropDownList;
            this.SelectNodeType.FormattingEnabled = true;
            this.SelectNodeType.ItemHeight = 14;
            object[] objArray = new object[] { this.comboItem1, this.comboItem2, this.comboItem3 };
            this.SelectNodeType.Items.AddRange(objArray);
            this.SelectNodeType.Location = new Point(0xc5, 6);
            this.SelectNodeType.Name = "SelectNodeType";
            this.SelectNodeType.Size = new Size(0x5f, 20);
            this.SelectNodeType.Style = eDotNetBarStyle.StyleManagerControlled;
            this.superTooltip1.SetSuperTooltip(this.SelectNodeType, new SuperTooltipInfo("Node type", "", "Use the \"Ground mount\" option to mount when moving between grinding places or to the vendor/trainer etc.", null, null, eTooltipColor.Gray));
            this.SelectNodeType.TabIndex = 0x2e;
            this.SelectNodeType.SelectedIndexChanged += new EventHandler(this.SelectEngineSelectedIndexChanged);
            this.comboItem1.Text = "Normal";
            this.comboItem2.Text = "Ground mount";
            this.labelX6.BackColor = Color.Transparent;
            this.labelX6.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX6.Location = new Point(0x6b, 0x24);
            this.labelX6.Name = "labelX6";
            this.labelX6.Size = new Size(0x60, 0x17);
            this.labelX6.TabIndex = 9;
            this.labelX6.Text = "Vertice distance:";
            this.superTabItem2.AttachedControl = this.superTabControlPanel3;
            this.superTabItem2.GlobalItem = false;
            this.superTabItem2.Name = "superTabItem2";
            this.superTabItem2.Text = "Graph";
            this.BtnLoad.AccessibleRole = AccessibleRole.PushButton;
            this.BtnLoad.ColorTable = eButtonColor.OrangeWithBackground;
            this.BtnLoad.Location = new Point(3, 330);
            this.BtnLoad.Name = "BtnLoad";
            this.BtnLoad.Size = new Size(0x4b, 0x17);
            this.BtnLoad.Style = eDotNetBarStyle.StyleManagerControlled;
            this.BtnLoad.TabIndex = 1;
            this.BtnLoad.Text = "Load";
            this.BtnLoad.Click += new EventHandler(this.BtnLoadClick);
            this.BtnSave.AccessibleRole = AccessibleRole.PushButton;
            this.BtnSave.ColorTable = eButtonColor.OrangeWithBackground;
            this.BtnSave.Location = new Point(0x54, 330);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new Size(0x4b, 0x17);
            this.BtnSave.Style = eDotNetBarStyle.StyleManagerControlled;
            this.BtnSave.TabIndex = 2;
            this.BtnSave.Text = "Save";
            this.BtnSave.Click += new EventHandler(this.BtnSaveClick);
            this.BtnNew.AccessibleRole = AccessibleRole.PushButton;
            this.BtnNew.ColorTable = eButtonColor.OrangeWithBackground;
            this.BtnNew.Location = new Point(0xa5, 330);
            this.BtnNew.Name = "BtnNew";
            this.BtnNew.Size = new Size(0x4b, 0x17);
            this.BtnNew.Style = eDotNetBarStyle.StyleManagerControlled;
            this.BtnNew.TabIndex = 6;
            this.BtnNew.Text = "New";
            this.BtnNew.Click += new EventHandler(this.BtnNewClick);
            this.superTooltip1.DefaultTooltipSettings = new SuperTooltipInfo("", "", "", null, null, eTooltipColor.Gray);
            this.styleManager1.ManagerStyle = eStyle.Windows7Blue;
            this.styleManager1.MetroColorParameters = new MetroColorGeneratorParameters(Color.White, Color.FromArgb(0x2b, 0x57, 0x9a));
            this.CBTopMost.BackgroundStyle.CornerType = eCornerType.Square;
            this.CBTopMost.Location = new Point(0x196, 330);
            this.CBTopMost.Name = "CBTopMost";
            this.CBTopMost.Size = new Size(100, 0x17);
            this.CBTopMost.Style = eDotNetBarStyle.StyleManagerControlled;
            this.CBTopMost.TabIndex = 0x2f;
            this.CBTopMost.Text = "Top most";
            this.CBTopMost.CheckedChanged += new EventHandler(this.CbTopMostCheckedChanged);
            this.labelX12.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX12.Location = new Point(0xf6, 330);
            this.labelX12.Name = "labelX12";
            this.labelX12.Size = new Size(0x3f, 0x17);
            this.labelX12.TabIndex = 0x31;
            this.labelX12.Text = "Draw edges:";
            this.DrawEdgesValue.BackgroundStyle.Class = "DateTimeInputBackground";
            this.DrawEdgesValue.BackgroundStyle.CornerType = eCornerType.Square;
            this.DrawEdgesValue.ButtonFreeText.Shortcut = eShortcut.F2;
            this.DrawEdgesValue.Location = new Point(0x13b, 0x14d);
            this.DrawEdgesValue.Name = "DrawEdgesValue";
            this.DrawEdgesValue.ShowUpDown = true;
            this.DrawEdgesValue.Size = new Size(80, 20);
            this.DrawEdgesValue.TabIndex = 50;
            this.DrawEdgesValue.Value = 400;
            this.labelX15.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX15.Location = new Point(0x25e, 0x147);
            this.labelX15.Name = "labelX15";
            this.labelX15.Size = new Size(0x73, 0x23);
            this.labelX15.TabIndex = 0x33;
            this.labelX15.Text = "Tip: Use F7 for spots <br/>\r\nUse F8 for vertice";
            this.comboItem3.Text = "TrainerPath";
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.FromArgb(0xdf, 0xe9, 0xf5);
            base.ClientSize = new Size(0x2cb, 0x166);
            base.Controls.Add(this.labelX15);
            base.Controls.Add(this.DrawEdgesValue);
            base.Controls.Add(this.labelX12);
            base.Controls.Add(this.CBTopMost);
            base.Controls.Add(this.BtnNew);
            base.Controls.Add(this.BtnSave);
            base.Controls.Add(this.BtnLoad);
            base.Controls.Add(this.superTabControl1);
            this.DoubleBuffered = true;
            base.MaximizeBox = false;
            base.Name = "PathControl";
            base.FormClosed += new FormClosedEventHandler(this.MapControlFormClosed);
            base.Load += new EventHandler(this.MapControlLoad);
            base.ResizeBegin += new EventHandler(this.MapControlResizeBegin);
            base.MouseUp += new MouseEventHandler(this.PathControlMouseUp);
            base.MouseWheel += new MouseEventHandler(this.MapMouseWheel);
            base.Resize += new EventHandler(this.MapControlResize);
            this.superTabControlPanel1.ResumeLayout(false);
            this.BePullRules.EndInit();
            ((ISupportInitialize) this.superTabControl1).EndInit();
            this.superTabControl1.ResumeLayout(false);
            this.superTabControlPanel2.ResumeLayout(false);
            this.groupPanel6.ResumeLayout(false);
            this.groupPanel5.ResumeLayout(false);
            this.ListSubProfiles.EndInit();
            this.ListSubProfiles.ResumeLayout(false);
            this.groupPanel4.ResumeLayout(false);
            this.SpotRoamDistance.EndInit();
            this.UMaxLevel.EndInit();
            this.UMinLevel.EndInit();
            this.PMinLevel.EndInit();
            this.PMaxLevel.EndInit();
            this.superTabControlPanel3.ResumeLayout(false);
            this.groupPanel3.ResumeLayout(false);
            this.groupPanel2.ResumeLayout(false);
            this.INodeDistance.EndInit();
            this.DrawEdgesValue.EndInit();
            base.ResumeLayout(false);
        }

        private void ListSubProfilesNodeClick(object sender, TreeNodeMouseEventArgs e)
        {
            Node node = e.Node;
            if (node.Tag is SubProfile)
            {
                this.SelectNode(node);
            }
        }

        private void ListSubProfilesSelectedIndexChanged(object sender, EventArgs e)
        {
            if ((this.ListSubProfiles.SelectedNode != null) && (this.ListSubProfiles.SelectedNode.Tag is SubProfile))
            {
                this.SelectNode(this.ListSubProfiles.SelectedNode);
            }
        }

        private void LoadPath()
        {
            while (true)
            {
                try
                {
                    while (true)
                    {
                        IEnumerable<DirectedLazyEdge> enumerable;
                        List<DirectedLazyEdge> edges = this._pathProfile.GetGraph.GetEdges();
                        List<Location> nodes = this._pathProfile.GetGraph.GetNodes();
                        lock (edges)
                        {
                            CS$<>9__CachedAnonymousMethodDelegatea ??= u => u;
                            enumerable = Enumerable.Select<DirectedLazyEdge, DirectedLazyEdge>(from i in edges
                                where i.Source.DistanceToSelf2D < 200.0
                                select i, CS$<>9__CachedAnonymousMethodDelegatea);
                        }
                        List<DirectedLazyEdge> list3 = (from pL in enumerable
                            orderby pL.Source.DistanceToSelf2D
                            select pL).Take<DirectedLazyEdge>(this.DrawEdgesValue.Value).ToList<DirectedLazyEdge>();
                        foreach (Location location in nodes)
                        {
                            Func<DirectedLazyEdge, bool> func = null;
                            if (func == null)
                            {
                                func = x => (x.Source == location) || (x.Target == location);
                            }
                            if (!Enumerable.Any<DirectedLazyEdge>(list3, func))
                            {
                                list3.Add(new DirectedLazyEdge(location, location));
                            }
                        }
                        this._nodesToDraw = list3;
                        Thread.Sleep(50);
                        break;
                    }
                }
                catch
                {
                }
            }
        }

        public void Log(string message)
        {
            Logging.Write(message, new object[0]);
        }

        private void MapControlFormClosed(object sender, FormClosedEventArgs e)
        {
            this.ReleaseHotKeys();
            this.StopMap();
        }

        private void MapControlLoad(object sender, EventArgs e)
        {
            this._fontText = new Font("Verdana", 6.5f);
            this.DoLoad();
            Thread thread = new Thread(new ThreadStart(this.UpdateLoop)) {
                IsBackground = true
            };
            this._mapThread = thread;
            this._mapThread.IsBackground = true;
            this._mapThread.Start();
            this.superTabControl1.SelectedTabIndex = 0;
        }

        private void MapControlResize(object sender, EventArgs e)
        {
            this._updateFormSize = true;
        }

        private void MapControlResizeBegin(object sender, EventArgs e)
        {
            this._updateFormSize = true;
        }

        private void MapMouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                if (this._scale > 2.0)
                {
                    this._scale += 0.6;
                }
                else
                {
                    this._scale += 0.3;
                }
            }
            else if (this._scale > 2.0)
            {
                this._scale -= 0.6;
            }
            else if (this._scale > 0.3)
            {
                this._scale -= 0.3;
            }
        }

        private void MaxLevelValueChanged(object sender, EventArgs e)
        {
            this._selected.PlayerMaxLevel = this.PMaxLevel.Value;
        }

        private void MinLevelValueChanged(object sender, EventArgs e)
        {
            this._selected.PlayerMinLevel = this.PMinLevel.Value;
        }

        private void NodeDistanceValueChanged(object sender, EventArgs e)
        {
            this._pathProfile.GetGraph.SetNodeDistance(this.INodeDistance.Value);
        }

        private int OffsetX(float obj, float me) => 
            (this.GraphView.Height / 2) - Convert.ToInt32((float) (((obj - me) * 0.4803002f) * ((float) this._scale)));

        private int OffsetY(float obj, float me) => 
            (this.GraphView.Width / 2) - Convert.ToInt32((float) (((obj - me) * 0.4803002f) * ((float) this._scale)));

        private void OtherAddSpotClick(object sender, EventArgs e)
        {
            this.AddSpot();
        }

        private void PathControlMouseUp(object sender, MouseEventArgs e)
        {
            this._rect = new Rectangle(0, 0, 0, 0);
        }

        private void PrintArrow(Color color, int x, int y, double heading, string topString, string botString)
        {
            try
            {
                heading = this.ConvertHeading(heading);
                Point[] points = new Point[] { new Point(Convert.ToInt32((double) (x + (Math.Cos(heading) * 10.0))), Convert.ToInt32((double) (y + (Math.Sin(heading) * 10.0)))), new Point(Convert.ToInt32((double) (x + (Math.Cos(heading + 2.0943951023931953) * 2.0))), Convert.ToInt32((double) (y + (Math.Sin(heading + 2.0943951023931953) * 2.0)))), new Point(x, y), new Point(Convert.ToInt32((double) (x + (Math.Cos(heading + -2.0943951023931953) * 2.0))), Convert.ToInt32((double) (y + (Math.Sin(heading + -2.0943951023931953) * 2.0)))), new Point(Convert.ToInt32((double) (x + (Math.Cos(heading) * 10.0))), Convert.ToInt32((double) (y + (Math.Sin(heading) * 10.0)))) };
                this._offScreenDc.DrawLines(new Pen(color), points);
                if (topString.Length > 0)
                {
                    this._offScreenDc.DrawString(topString, this._fontText, new SolidBrush(color), new PointF(x - (topString.Length * 2.2f), (float) (y - 15)));
                }
                if (botString.Length > 0)
                {
                    this._offScreenDc.DrawString(botString, this._fontText, new SolidBrush(color), new PointF((float) (x - (botString.Length * 2)), (float) (y + 6)));
                }
                SolidBrush brush = new SolidBrush(color);
                this._offScreenDc.DrawEllipse(new Pen(color), x - 3, y - 3, 6, 6);
                this._offScreenDc.FillEllipse(brush, x - 3, y - 3, 6, 6);
            }
            catch (Exception)
            {
            }
        }

        private void PrintCircle(Color color, int x, int y, string name)
        {
            try
            {
                SolidBrush brush = new SolidBrush(color);
                this._offScreenDc.DrawEllipse(new Pen(color), x - 3, y - 3, 6, 6);
                this._offScreenDc.FillEllipse(brush, x - 3, y - 3, 6, 6);
                this._offScreenDc.DrawString(name, this._fontText, new SolidBrush(color), new PointF((float) (x - (name.Length * 2)), (float) (y - 15)));
            }
            catch (Exception)
            {
            }
        }

        private void PrintPlayer()
        {
            try
            {
                this.PrintArrow(this._colorMe, this.GraphView.Width / 2, this.GraphView.Height / 2, (double) LazyLib.Wow.ObjectManager.MyPlayer.Facing, "", "");
            }
            catch (Exception)
            {
            }
        }

        private void PrintSelected()
        {
            foreach (Location location in this._selectedList)
            {
                Location location2 = location;
                Color dodgerBlue = Color.DodgerBlue;
                this.PrintCircle(dodgerBlue, this.OffsetY(location2.Y, LazyLib.Wow.ObjectManager.MyPlayer.Location.Y), this.OffsetX(location2.X, LazyLib.Wow.ObjectManager.MyPlayer.Location.X), "");
            }
        }

        private void PrintSpots()
        {
            try
            {
                foreach (SubProfile profile in this._pathProfile.GetSubProfiles)
                {
                    CS$<>9__CachedAnonymousMethodDelegate13 ??= pL => pL.DistanceToSelf2D;
                    foreach (Location location in Enumerable.OrderBy<Location, double>(from u in profile.Spots select u, CS$<>9__CachedAnonymousMethodDelegate13).Take<Location>(20))
                    {
                        try
                        {
                            if (this.CBShowPullZones.Checked)
                            {
                                this.PrintTransparentCircle(this.OffsetY(location.Y, LazyLib.Wow.ObjectManager.MyPlayer.Location.Y), this.OffsetX(location.X, LazyLib.Wow.ObjectManager.MyPlayer.Location.X), "", profile.SpotRoamDistance);
                            }
                        }
                        catch
                        {
                        }
                        Color forestGreen = Color.ForestGreen;
                        if (ReferenceEquals(this._spotSelectedForEdit, location))
                        {
                            forestGreen = Color.DarkOrange;
                        }
                        this.PrintCircle(forestGreen, this.OffsetY(location.Y, LazyLib.Wow.ObjectManager.MyPlayer.Location.Y), this.OffsetX(location.X, LazyLib.Wow.ObjectManager.MyPlayer.Location.X), "");
                    }
                }
                if (Engine.Running)
                {
                    try
                    {
                        this.PrintCircle(Color.GreenYellow, this.OffsetY(this._pathProfile.GetSubProfile().CurrentSpot.Y, LazyLib.Wow.ObjectManager.MyPlayer.Location.Y), this.OffsetX(this._pathProfile.GetSubProfile().CurrentSpot.X, LazyLib.Wow.ObjectManager.MyPlayer.Location.X), "");
                    }
                    catch
                    {
                    }
                }
            }
            catch
            {
            }
        }

        private void PrintTarget()
        {
            try
            {
                if (LazyLib.Wow.ObjectManager.MyPlayer.Target.IsValid)
                {
                    this.PrintArrow(Color.Red, this.OffsetY(LazyLib.Wow.ObjectManager.MyPlayer.Target.Y, LazyLib.Wow.ObjectManager.MyPlayer.Location.Y), this.OffsetX(LazyLib.Wow.ObjectManager.MyPlayer.Target.X, LazyLib.Wow.ObjectManager.MyPlayer.Location.X), (double) LazyLib.Wow.ObjectManager.MyPlayer.Target.Facing, "", "");
                }
            }
            catch (Exception)
            {
            }
        }

        private void PrintTransparentCircle(int x, int y, string name, int radius)
        {
            try
            {
                radius = Convert.ToInt32((double) ((radius * this._scale) / 3.0));
                this._offScreenDc.DrawEllipse(new Pen(Color.ForestGreen), (int) (x - radius), (int) (y - radius), (int) (2 * radius), (int) (2 * radius));
                Color color = Color.FromArgb(50, Color.Green);
                this._offScreenDc.FillEllipse(new SolidBrush(color), (int) (x - radius), (int) (y - radius), (int) (2 * radius), (int) (2 * radius));
                this._offScreenDc.DrawString(name, this._fontText, new SolidBrush(Color.ForestGreen), new PointF((float) (x - (name.Length * 2)), (float) (y - 15)));
            }
            catch (Exception)
            {
            }
        }

        private void PrintWay()
        {
            float y = LazyLib.Wow.ObjectManager.MyPlayer.Location.Y;
            float x = LazyLib.Wow.ObjectManager.MyPlayer.Location.X;
            foreach (DirectedLazyEdge edge in this._nodesToDraw)
            {
                Location source = edge.Source;
                Point point2 = new Point(this.OffsetY(edge.Source.Y, y), this.OffsetX(edge.Source.X, x));
                Point point = new Point(this.OffsetY(edge.Target.Y, y), this.OffsetX(edge.Target.X, x));
                Color color = source.NodeType.Equals(NodeType.GroundMount) ? Color.White : Color.Red;
                this.PrintCircle(color, this.OffsetY(source.Y, y), this.OffsetX(source.X, x), "");
                this._offScreenDc.DrawLine(new Pen(Color.Blue), point2, point);
            }
        }

        private void ReleaseHotKeys()
        {
            if (LazySettings.SetupUseHotkeys)
            {
                if (this._f7.Registered)
                {
                    this._f7.Unregister();
                }
                if (this._f8.Registered)
                {
                    this._f8.Unregister();
                }
            }
        }

        private void SaveSubProfiles()
        {
            this._pathProfile.ClearSubProfile();
            foreach (Node node in this.ListSubProfiles.Nodes)
            {
                if (node.Tag is SubProfile)
                {
                    this._pathProfile.AddSubProfile((SubProfile) node.Tag);
                }
            }
        }

        private void SelectEngineSelectedIndexChanged(object sender, EventArgs e)
        {
            string str = this.SelectNodeType.SelectedItem.ToString();
            if (str != null)
            {
                if (str == "Normal")
                {
                    this._pathProfile.GetGraph.SetNodeType(NodeType.Normal);
                }
                else if (str == "Ground mount")
                {
                    this._pathProfile.GetGraph.SetNodeType(NodeType.GroundMount);
                }
            }
        }

        private void SelectNode(Node node)
        {
            if (this._selected != null)
            {
                this._selected.Name = this.TBName.Text;
                this._selected.PlayerMaxLevel = this.PMaxLevel.Value;
                this._selected.PlayerMinLevel = this.PMinLevel.Value;
                this._selected.MobMaxLevel = this.UMaxLevel.Value;
                this._selected.MobMinLevel = this.UMinLevel.Value;
                this._selected.Order = this.CBSpotOrder.Checked;
                this._selected.SpotRoamDistance = this.SpotRoamDistance.Value;
                foreach (Node node2 in this.ListSubProfiles.Nodes)
                {
                    if ((node2.Tag is SubProfile) && ReferenceEquals(this._selected, node2.Tag))
                    {
                        node2.Text = this._selected.Name;
                    }
                }
            }
            this.ListSubProfiles.BeginUpdate();
            this._selected = (SubProfile) node.Tag;
            this.UpdateFields(this._selected);
            this.ListSubProfiles.EndUpdate();
            this.TBName.Enabled = true;
            this.PMinLevel.Enabled = true;
            this.PMaxLevel.Enabled = true;
            this.CBSpotOrder.Enabled = true;
            this.BtnFaction.Enabled = true;
            this.BtnAddSpot.Enabled = true;
            this.UMaxLevel.Enabled = true;
            this.UMinLevel.Enabled = true;
            this.SpotRoamDistance.Enabled = true;
            this.TBFactionList.Enabled = true;
            this.BtnAddIgnore.Enabled = true;
            this.TBIgnore.Enabled = true;
        }

        private void SpotRoamDistanceValueChanged(object sender, EventArgs e)
        {
            this._selected.SpotRoamDistance = this.SpotRoamDistance.Value;
        }

        public void Start()
        {
            base.Show();
        }

        public void StopMap()
        {
            if (this._pathLoadedThread != null)
            {
                this._pathLoadedThread.Abort();
                this._pathLoadedThread = null;
            }
            if (this._mapThread != null)
            {
                this._mapThread.Abort();
                this._mapThread = null;
            }
            this._offScreenBmp = null;
            this._offScreenDc = null;
            GC.Collect();
        }

        private void TbIgnoreListTextChanged(object sender, EventArgs e)
        {
            try
            {
                string[] strArray = this.TBFactionList.Text.Split(new char[] { ' ' });
                this._selected.Factions.Clear();
                CS$<>9__CachedAnonymousMethodDelegate2f ??= s => Convert.ToUInt32(s);
                this._selected.Factions.AddRange(Enumerable.Select<string, uint>(from s in strArray
                    where (s != "") && (s != " ")
                    select s, CS$<>9__CachedAnonymousMethodDelegate2f));
                this.LBFactionCount.Text = this._selected.Factions.Count;
            }
            catch
            {
            }
        }

        private void TbIgnoreTextChanged(object sender, EventArgs e)
        {
            try
            {
                string[] strArray = this.TBIgnore.Text.Split(new char[] { '|' });
                this._selected.Ignore.Clear();
                this._selected.Ignore.AddRange(from s in strArray
                    where (s != "") && (s != " ")
                    select s);
                this.LBIgnoreCount.Text = this._selected.Ignore.Count;
            }
            catch
            {
            }
        }

        private void TbNameTextChanged(object sender, EventArgs e)
        {
            this._selected.Name = this.TBName.Text;
        }

        private void UMaxLevelValueChanged(object sender, EventArgs e)
        {
            this._selected.MobMaxLevel = this.UMaxLevel.Value;
        }

        private void UMinLevelValueChanged(object sender, EventArgs e)
        {
            this._selected.MobMinLevel = this.UMinLevel.Value;
        }

        private void UpdateFields(SubProfile subProfile)
        {
            try
            {
                this.SaveSubProfiles();
                this.TBName.Text = subProfile.Name;
                this.PMaxLevel.Value = subProfile.PlayerMaxLevel;
                this.PMinLevel.Value = subProfile.PlayerMinLevel;
                this.UMaxLevel.Value = subProfile.MobMaxLevel;
                this.UMinLevel.Value = subProfile.MobMinLevel;
                this.SpotRoamDistance.Value = subProfile.SpotRoamDistance;
                this.LBFactionCount.Text = subProfile.Factions.Count;
                this.LBSpotCount.Text = subProfile.Spots.Count;
                this.CBSpotOrder.Checked = subProfile.Order;
                this.TBFactionList.Text = Enumerable.Aggregate<uint, string>(subProfile.Factions, string.Empty, (current, faction) => current + $"{faction} ");
                CS$<>9__CachedAnonymousMethodDelegate17 ??= (current, faction) => (current + $"{faction}|");
                this.TBIgnore.Text = Enumerable.Aggregate<string, string>(this._selected.Ignore, string.Empty, CS$<>9__CachedAnonymousMethodDelegate17);
            }
            catch (Exception exception)
            {
                Logging.Write("Exception when updatingFields: " + exception, new object[0]);
            }
        }

        private void UpdateLoop()
        {
            while (true)
            {
                if (!LazyLib.Wow.ObjectManager.Initialized)
                {
                    Thread.Sleep(0x3e8);
                    continue;
                }
                try
                {
                    if (this._updateFormSize)
                    {
                        this._offScreenBmp = new Bitmap(this.GraphView.Width, this.GraphView.Height);
                        this._updateFormSize = false;
                    }
                    this._offScreenDc = Graphics.FromImage(this._offScreenBmp);
                    this._offScreenDc.FillRectangle(new SolidBrush(Color.Black), new Rectangle(0, 0, this._offScreenBmp.Width, this._offScreenBmp.Height));
                    this.PrintTarget();
                    this.PrintPlayer();
                    this.PrintWay();
                    this.PrintSpots();
                    this.PrintSelected();
                    this.GraphView.CreateGraphics().DrawImage(this._offScreenBmp, 0, 0);
                }
                catch (ThreadAbortException)
                {
                }
                catch (Exception exception)
                {
                    Logging.Debug("Error in radar: " + exception, new object[0]);
                }
                Thread.Sleep(this._refreshTime);
            }
        }

        public int RefreshTime
        {
            get => 
                this._refreshTime;
            set => 
                this._refreshTime = value;
        }

        public delegate void WriteDelegate(string message);
    }
}

