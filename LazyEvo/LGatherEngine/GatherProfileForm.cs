namespace LazyEvo.LGatherEngine
{
    using DevComponents.DotNetBar;
    using DevComponents.DotNetBar.Controls;
    using DevComponents.Editors;
    using LazyEvo;
    using LazyEvo.LGatherEngine.Helpers;
    using LazyLib;
    using LazyLib.Wow;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows.Forms;

    [Obfuscation(Feature="renaming", ApplyToMembers=true)]
    internal class GatherProfileForm : Office2007Form
    {
        private static volatile Dictionary<Thread, bool> _endThread = new Dictionary<Thread, bool>();
        private readonly Hotkey _normalKey = new Hotkey();
        private readonly Hotkey _vendorKey = new Hotkey();
        private bool _autoRecord;
        private string _profileToLoad = "none";
        private FlyingWaypointsType _waypointSelected;
        private Thread _waypointThread;
        private IContainer components;
        private StyleManager styleManager1;
        private ButtonX buttonX1;
        private DotNetBarManager dotNetBarManager1;
        private DockSite dockSite4;
        private DockSite dockSite1;
        private DockSite dockSite2;
        private DockSite dockSite3;
        private DockSite dockSite5;
        private DockSite dockSite6;
        private DockSite dockSite7;
        private Bar bar1;
        private ButtonItem buttonItem1;
        private ButtonItem createProfileButton;
        private ButtonItem loadProfileButton;
        private ButtonItem saveProfileButton;
        private DockSite dockSite8;
        private GroupPanel groupPanel6;
        private ButtonX testToTownButton;
        private ButtonX testNormalButton;
        private GroupPanel groupPanel2;
        private LabelX labelX7;
        private TextBoxX vendorNameBox;
        private ButtonX deleteLastToTownButton;
        private ButtonX addToTownButton;
        private ButtonX deleteToTownButton;
        private LabelX labelX3;
        private TextBoxX toTownCountBox;
        private GroupPanel groupPanel1;
        private ButtonX deleteLastNormalButton;
        private ButtonX addNormalButton;
        private ButtonX deleteNormalButton;
        private LabelX labelX1;
        private TextBoxX normalCountBox;
        private GroupPanel groupPanel7;
        private LabelX labelX10;
        private SwitchButton recordSwitchButton;
        private LabelX labelX9;
        private IntegerInput distanceInput;
        private LabelX labelX4;
        private LabelX labelX8;
        private ComboBoxEx profileTypeComboBox;
        private ButtonX setVendorNameButton;
        private ComboItem comboItem2;
        private ComboItem comboItem1;
        private SwitchButton hotkeySwitchButton;
        private GalleryContainer galleryContainer1;
        private CheckBoxX CBNaturalRun;
        private ButtonItem loadProfileButton1;
        private ButtonItem saveProfileButton1;

        public GatherProfileForm()
        {
            this._waypointThread = new Thread(new ThreadStart(this.WaypointThread));
            this._waypointThread.Name = "WaypointThread";
            this.InitializeComponent();
            this.profileTypeComboBox.SelectedIndex = 0;
            try
            {
                this._normalKey.KeyCode = Keys.Z;
                this._normalKey.Alt = true;
                this._normalKey.Windows = false;
                this._vendorKey.KeyCode = Keys.C;
                this._vendorKey.Alt = true;
                this._vendorKey.Windows = false;
                this._normalKey.Pressed += new HandledEventHandler(this.NormalKeyHotKeyPressed);
                this._vendorKey.Pressed += new HandledEventHandler(this.VendorHotKeyPressed);
            }
            catch (Exception)
            {
            }
        }

        private void AddNormalButtonClick(object sender, EventArgs e)
        {
            if (this.CheckProfile())
            {
                GatherEngine.CurrentProfile.AddSingleWayPoint(LazyLib.Wow.ObjectManager.MyPlayer.Location);
                this.UpdateWaypointsCount();
            }
        }

        private void AddToTownButtonClick(object sender, EventArgs e)
        {
            if (this.CheckProfile())
            {
                GatherEngine.CurrentProfile.AddSingleToTownWayPoint(LazyLib.Wow.ObjectManager.MyPlayer.Location);
                this.UpdateWaypointsCount();
            }
        }

        private void BtnOnlineProfiles_Click(object sender, EventArgs e)
        {
            GatherProfiles profiles = new GatherProfiles();
            profiles.Show();
            profiles.ProfileDownload += new EventHandler<EProfileDownloaded>(this.ProfileChanged);
        }

        private void CBNaturalRun_CheckedChanged(object sender, EventArgs e)
        {
            if (GatherEngine.CurrentProfile != null)
            {
                GatherEngine.CurrentProfile.NaturalRun = this.CBNaturalRun.Checked;
            }
        }

        private bool CheckProfile()
        {
            if (GatherEngine.CurrentProfile != null)
            {
                return true;
            }
            MessageBox.Show("Load or create a new profile");
            return false;
        }

        private void CreateProfileButtonClick(object sender, EventArgs e)
        {
            GatherEngine.CurrentProfile = new GatherProfile();
            this._profileToLoad = "none";
            this.UpdateControls();
        }

        private void DeleteLastNormalButtonClick(object sender, EventArgs e)
        {
            if (this.CheckProfile())
            {
                if (GatherEngine.CurrentProfile.WaypointsNormal.Count > 0)
                {
                    GatherEngine.CurrentProfile.WaypointsNormal.RemoveAt(GatherEngine.CurrentProfile.WaypointsNormal.Count - 1);
                }
                this.UpdateWaypointsCount();
            }
        }

        private void DeleteLastToTownButtonClick(object sender, EventArgs e)
        {
            if (this.CheckProfile())
            {
                if (GatherEngine.CurrentProfile.WaypointsToTown.Count > 0)
                {
                    GatherEngine.CurrentProfile.WaypointsToTown.RemoveAt(GatherEngine.CurrentProfile.WaypointsToTown.Count - 1);
                }
                this.UpdateWaypointsCount();
            }
        }

        private void DeleteNormalButtonClick(object sender, EventArgs e)
        {
            if (this.CheckProfile())
            {
                GatherEngine.CurrentProfile.WaypointsNormal.Clear();
                this.UpdateWaypointsCount();
            }
        }

        private void DeleteToTownButtonClick(object sender, EventArgs e)
        {
            if (this.CheckProfile())
            {
                GatherEngine.CurrentProfile.WaypointsToTown.Clear();
                this.UpdateWaypointsCount();
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

        private bool EndThread(Thread thread)
        {
            bool flag = false;
            if (thread != null)
            {
                _endThread.TryGetValue(thread, out flag);
            }
            return flag;
        }

        private void GatherProfileFormLoad(object sender, EventArgs e)
        {
            if (GatherEngine.CurrentProfile != null)
            {
                this.UpdateControls();
            }
        }

        private void GrindingProfileFormFormClosing(object sender, FormClosingEventArgs e)
        {
            this.TerminateThread(this._waypointThread);
            if (this._normalKey.Registered)
            {
                this._normalKey.Unregister();
            }
            if (this._vendorKey.Registered)
            {
                this._vendorKey.Unregister();
            }
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            ComponentResourceManager manager = new ComponentResourceManager(typeof(GatherProfileForm));
            this.styleManager1 = new StyleManager(this.components);
            this.buttonX1 = new ButtonX();
            this.dotNetBarManager1 = new DotNetBarManager(this.components);
            this.dockSite4 = new DockSite();
            this.dockSite1 = new DockSite();
            this.dockSite2 = new DockSite();
            this.dockSite8 = new DockSite();
            this.dockSite5 = new DockSite();
            this.dockSite6 = new DockSite();
            this.dockSite7 = new DockSite();
            this.bar1 = new Bar();
            this.buttonItem1 = new ButtonItem();
            this.createProfileButton = new ButtonItem();
            this.loadProfileButton = new ButtonItem();
            this.saveProfileButton = new ButtonItem();
            this.loadProfileButton1 = new ButtonItem();
            this.saveProfileButton1 = new ButtonItem();
            this.dockSite3 = new DockSite();
            this.groupPanel1 = new GroupPanel();
            this.deleteLastNormalButton = new ButtonX();
            this.addNormalButton = new ButtonX();
            this.deleteNormalButton = new ButtonX();
            this.labelX1 = new LabelX();
            this.normalCountBox = new TextBoxX();
            this.groupPanel2 = new GroupPanel();
            this.setVendorNameButton = new ButtonX();
            this.labelX7 = new LabelX();
            this.deleteToTownButton = new ButtonX();
            this.vendorNameBox = new TextBoxX();
            this.deleteLastToTownButton = new ButtonX();
            this.addToTownButton = new ButtonX();
            this.labelX3 = new LabelX();
            this.toTownCountBox = new TextBoxX();
            this.groupPanel6 = new GroupPanel();
            this.testToTownButton = new ButtonX();
            this.testNormalButton = new ButtonX();
            this.groupPanel7 = new GroupPanel();
            this.CBNaturalRun = new CheckBoxX();
            this.hotkeySwitchButton = new SwitchButton();
            this.labelX4 = new LabelX();
            this.labelX10 = new LabelX();
            this.recordSwitchButton = new SwitchButton();
            this.labelX9 = new LabelX();
            this.distanceInput = new IntegerInput();
            this.labelX8 = new LabelX();
            this.profileTypeComboBox = new ComboBoxEx();
            this.comboItem1 = new ComboItem();
            this.comboItem2 = new ComboItem();
            this.galleryContainer1 = new GalleryContainer();
            this.dockSite7.SuspendLayout();
            this.bar1.BeginInit();
            this.groupPanel1.SuspendLayout();
            this.groupPanel2.SuspendLayout();
            this.groupPanel6.SuspendLayout();
            this.groupPanel7.SuspendLayout();
            this.distanceInput.BeginInit();
            base.SuspendLayout();
            this.styleManager1.ManagerStyle = eStyle.Windows7Blue;
            this.buttonX1.AccessibleRole = AccessibleRole.PushButton;
            this.buttonX1.Location = new Point(0, 0);
            this.buttonX1.Name = "buttonX1";
            this.buttonX1.Size = new Size(0, 0);
            this.buttonX1.TabIndex = 0;
            this.dotNetBarManager1.AutoDispatchShortcuts.Add(eShortcut.F1);
            this.dotNetBarManager1.AutoDispatchShortcuts.Add(eShortcut.CtrlC);
            this.dotNetBarManager1.AutoDispatchShortcuts.Add(eShortcut.CtrlA);
            this.dotNetBarManager1.AutoDispatchShortcuts.Add(eShortcut.CtrlV);
            this.dotNetBarManager1.AutoDispatchShortcuts.Add(eShortcut.CtrlX);
            this.dotNetBarManager1.AutoDispatchShortcuts.Add(eShortcut.CtrlZ);
            this.dotNetBarManager1.AutoDispatchShortcuts.Add(eShortcut.CtrlY);
            this.dotNetBarManager1.AutoDispatchShortcuts.Add(eShortcut.Del);
            this.dotNetBarManager1.AutoDispatchShortcuts.Add(eShortcut.Ins);
            this.dotNetBarManager1.BottomDockSite = this.dockSite4;
            this.dotNetBarManager1.EnableFullSizeDock = false;
            this.dotNetBarManager1.LeftDockSite = this.dockSite1;
            this.dotNetBarManager1.ParentForm = this;
            this.dotNetBarManager1.RightDockSite = this.dockSite2;
            this.dotNetBarManager1.Style = eDotNetBarStyle.Windows7;
            this.dotNetBarManager1.ToolbarBottomDockSite = this.dockSite8;
            this.dotNetBarManager1.ToolbarLeftDockSite = this.dockSite5;
            this.dotNetBarManager1.ToolbarRightDockSite = this.dockSite6;
            this.dotNetBarManager1.ToolbarTopDockSite = this.dockSite7;
            this.dotNetBarManager1.TopDockSite = this.dockSite3;
            this.dockSite4.AccessibleRole = AccessibleRole.Window;
            this.dockSite4.Dock = DockStyle.Bottom;
            this.dockSite4.DocumentDockContainer = new DocumentDockContainer();
            this.dockSite4.Location = new Point(0, 0x17a);
            this.dockSite4.Name = "dockSite4";
            this.dockSite4.Size = new Size(0x1af, 0);
            this.dockSite4.TabIndex = 4;
            this.dockSite4.TabStop = false;
            this.dockSite1.AccessibleRole = AccessibleRole.Window;
            this.dockSite1.Dock = DockStyle.Left;
            this.dockSite1.DocumentDockContainer = new DocumentDockContainer();
            this.dockSite1.Location = new Point(0, 0x19);
            this.dockSite1.Name = "dockSite1";
            this.dockSite1.Size = new Size(0, 0x161);
            this.dockSite1.TabIndex = 1;
            this.dockSite1.TabStop = false;
            this.dockSite2.AccessibleRole = AccessibleRole.Window;
            this.dockSite2.Dock = DockStyle.Right;
            this.dockSite2.DocumentDockContainer = new DocumentDockContainer();
            this.dockSite2.Location = new Point(0x1af, 0x19);
            this.dockSite2.Name = "dockSite2";
            this.dockSite2.Size = new Size(0, 0x161);
            this.dockSite2.TabIndex = 2;
            this.dockSite2.TabStop = false;
            this.dockSite8.AccessibleRole = AccessibleRole.Window;
            this.dockSite8.Dock = DockStyle.Bottom;
            this.dockSite8.Location = new Point(0, 0x17a);
            this.dockSite8.Name = "dockSite8";
            this.dockSite8.Size = new Size(0x1af, 0);
            this.dockSite8.TabIndex = 8;
            this.dockSite8.TabStop = false;
            this.dockSite5.AccessibleRole = AccessibleRole.Window;
            this.dockSite5.Dock = DockStyle.Left;
            this.dockSite5.Location = new Point(0, 0x19);
            this.dockSite5.Name = "dockSite5";
            this.dockSite5.Size = new Size(0, 0x161);
            this.dockSite5.TabIndex = 5;
            this.dockSite5.TabStop = false;
            this.dockSite6.AccessibleRole = AccessibleRole.Window;
            this.dockSite6.Dock = DockStyle.Right;
            this.dockSite6.Location = new Point(0x1af, 0x19);
            this.dockSite6.Name = "dockSite6";
            this.dockSite6.Size = new Size(0, 0x161);
            this.dockSite6.TabIndex = 6;
            this.dockSite6.TabStop = false;
            this.dockSite7.AccessibleRole = AccessibleRole.Window;
            this.dockSite7.Controls.Add(this.bar1);
            this.dockSite7.Dock = DockStyle.Top;
            this.dockSite7.Location = new Point(0, 0);
            this.dockSite7.Name = "dockSite7";
            this.dockSite7.Size = new Size(0x1af, 0x19);
            this.dockSite7.TabIndex = 7;
            this.dockSite7.TabStop = false;
            this.bar1.AccessibleDescription = "DotNetBar Bar (bar1)";
            this.bar1.AccessibleName = "DotNetBar Bar";
            this.bar1.AccessibleRole = AccessibleRole.MenuBar;
            this.bar1.BackColor = Color.Transparent;
            this.bar1.DockSide = eDockSide.Top;
            BaseItem[] items = new BaseItem[] { this.buttonItem1, this.saveProfileButton1, this.loadProfileButton1 };
            this.bar1.Items.AddRange(items);
            this.bar1.Location = new Point(0, 0);
            this.bar1.MenuBar = true;
            this.bar1.Name = "bar1";
            this.bar1.Size = new Size(0x1af, 0x18);
            this.bar1.Stretch = true;
            this.bar1.Style = eDotNetBarStyle.Windows7;
            this.bar1.TabIndex = 0;
            this.bar1.TabStop = false;
            this.bar1.Text = "bar1";
            this.buttonItem1.Name = "buttonItem1";
            BaseItem[] itemArray2 = new BaseItem[] { this.createProfileButton, this.loadProfileButton, this.saveProfileButton };
            this.buttonItem1.SubItems.AddRange(itemArray2);
            this.buttonItem1.Text = "Profile";
            this.createProfileButton.Icon = (Icon) manager.GetObject("createProfileButton.Icon");
            this.createProfileButton.Name = "createProfileButton";
            this.createProfileButton.Text = "Create New Profile";
            this.createProfileButton.Click += new EventHandler(this.CreateProfileButtonClick);
            this.loadProfileButton.Icon = (Icon) manager.GetObject("loadProfileButton.Icon");
            this.loadProfileButton.Name = "loadProfileButton";
            this.loadProfileButton.Text = "Load Existing Profile";
            this.loadProfileButton.Click += new EventHandler(this.LoadProfileButtonClick);
            this.saveProfileButton.Icon = (Icon) manager.GetObject("saveProfileButton.Icon");
            this.saveProfileButton.Name = "saveProfileButton";
            this.saveProfileButton.Text = "Save Current Profile";
            this.saveProfileButton.Click += new EventHandler(this.SaveProfileButtonClick);
            this.loadProfileButton1.Name = "loadProfileButton1";
            this.loadProfileButton1.Text = "Load Existing Profile";
            this.loadProfileButton1.Click += new EventHandler(this.loadProfileButton1_Click);
            this.saveProfileButton1.Name = "saveProfileButton1";
            this.saveProfileButton1.Text = "Save Current Profile";
            this.saveProfileButton1.Click += new EventHandler(this.saveProfileButton1_Click);
            this.dockSite3.AccessibleRole = AccessibleRole.Window;
            this.dockSite3.Dock = DockStyle.Top;
            this.dockSite3.DocumentDockContainer = new DocumentDockContainer();
            this.dockSite3.Location = new Point(0, 0x19);
            this.dockSite3.Name = "dockSite3";
            this.dockSite3.Size = new Size(0x1af, 0);
            this.dockSite3.TabIndex = 3;
            this.dockSite3.TabStop = false;
            this.groupPanel1.CanvasColor = SystemColors.Control;
            this.groupPanel1.ColorSchemeStyle = eDotNetBarStyle.Office2007;
            this.groupPanel1.Controls.Add(this.deleteLastNormalButton);
            this.groupPanel1.Controls.Add(this.addNormalButton);
            this.groupPanel1.Controls.Add(this.deleteNormalButton);
            this.groupPanel1.Controls.Add(this.labelX1);
            this.groupPanel1.Controls.Add(this.normalCountBox);
            this.groupPanel1.Location = new Point(12, 0x90);
            this.groupPanel1.Name = "groupPanel1";
            this.groupPanel1.Size = new Size(0x198, 0x40);
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
            this.groupPanel1.Style.Class = "";
            this.groupPanel1.Style.CornerDiameter = 4;
            this.groupPanel1.Style.CornerType = eCornerType.Rounded;
            this.groupPanel1.Style.TextAlignment = eStyleTextAlignment.Center;
            this.groupPanel1.Style.TextColorSchemePart = eColorSchemePart.PanelText;
            this.groupPanel1.Style.TextLineAlignment = eStyleTextAlignment.Near;
            this.groupPanel1.StyleMouseDown.Class = "";
            this.groupPanel1.StyleMouseDown.CornerType = eCornerType.Square;
            this.groupPanel1.StyleMouseOver.Class = "";
            this.groupPanel1.StyleMouseOver.CornerType = eCornerType.Square;
            this.groupPanel1.TabIndex = 9;
            this.groupPanel1.Text = "Normal Waypoints";
            this.deleteLastNormalButton.AccessibleRole = AccessibleRole.PushButton;
            this.deleteLastNormalButton.ColorTable = eButtonColor.OrangeWithBackground;
            this.deleteLastNormalButton.Location = new Point(0xc5, 11);
            this.deleteLastNormalButton.Name = "deleteLastNormalButton";
            this.deleteLastNormalButton.Size = new Size(90, 0x17);
            this.deleteLastNormalButton.Style = eDotNetBarStyle.StyleManagerControlled;
            this.deleteLastNormalButton.TabIndex = 4;
            this.deleteLastNormalButton.Text = "Delete Last";
            this.deleteLastNormalButton.Click += new EventHandler(this.DeleteLastNormalButtonClick);
            this.addNormalButton.AccessibleRole = AccessibleRole.PushButton;
            this.addNormalButton.ColorTable = eButtonColor.OrangeWithBackground;
            this.addNormalButton.Location = new Point(0x65, 11);
            this.addNormalButton.Name = "addNormalButton";
            this.addNormalButton.Size = new Size(90, 0x17);
            this.addNormalButton.Style = eDotNetBarStyle.StyleManagerControlled;
            this.addNormalButton.TabIndex = 3;
            this.addNormalButton.Text = "Add Position";
            this.addNormalButton.Click += new EventHandler(this.AddNormalButtonClick);
            this.deleteNormalButton.AccessibleRole = AccessibleRole.PushButton;
            this.deleteNormalButton.ColorTable = eButtonColor.OrangeWithBackground;
            this.deleteNormalButton.Location = new Point(0x125, 11);
            this.deleteNormalButton.Name = "deleteNormalButton";
            this.deleteNormalButton.Size = new Size(90, 0x17);
            this.deleteNormalButton.Style = eDotNetBarStyle.StyleManagerControlled;
            this.deleteNormalButton.TabIndex = 2;
            this.deleteNormalButton.Text = "Delete All";
            this.deleteNormalButton.Click += new EventHandler(this.DeleteNormalButtonClick);
            this.labelX1.BackColor = Color.Transparent;
            this.labelX1.BackgroundStyle.Class = "";
            this.labelX1.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX1.Location = new Point(4, 10);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new Size(0x23, 0x17);
            this.labelX1.Style = eDotNetBarStyle.StyleManagerControlled;
            this.labelX1.TabIndex = 1;
            this.labelX1.Text = "Count:";
            this.normalCountBox.Border.Class = "TextBoxBorder";
            this.normalCountBox.Border.CornerType = eCornerType.Square;
            this.normalCountBox.Location = new Point(0x2e, 12);
            this.normalCountBox.Name = "normalCountBox";
            this.normalCountBox.ReadOnly = true;
            this.normalCountBox.Size = new Size(0x31, 20);
            this.normalCountBox.TabIndex = 0;
            this.groupPanel2.CanvasColor = SystemColors.Control;
            this.groupPanel2.ColorSchemeStyle = eDotNetBarStyle.Office2007;
            this.groupPanel2.Controls.Add(this.setVendorNameButton);
            this.groupPanel2.Controls.Add(this.labelX7);
            this.groupPanel2.Controls.Add(this.deleteToTownButton);
            this.groupPanel2.Controls.Add(this.vendorNameBox);
            this.groupPanel2.Controls.Add(this.deleteLastToTownButton);
            this.groupPanel2.Controls.Add(this.addToTownButton);
            this.groupPanel2.Controls.Add(this.labelX3);
            this.groupPanel2.Controls.Add(this.toTownCountBox);
            this.groupPanel2.Location = new Point(11, 0xd6);
            this.groupPanel2.Name = "groupPanel2";
            this.groupPanel2.Size = new Size(0x198, 0x5b);
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
            this.groupPanel2.Style.Class = "";
            this.groupPanel2.Style.CornerDiameter = 4;
            this.groupPanel2.Style.CornerType = eCornerType.Rounded;
            this.groupPanel2.Style.TextAlignment = eStyleTextAlignment.Center;
            this.groupPanel2.Style.TextColorSchemePart = eColorSchemePart.PanelText;
            this.groupPanel2.Style.TextLineAlignment = eStyleTextAlignment.Near;
            this.groupPanel2.StyleMouseDown.Class = "";
            this.groupPanel2.StyleMouseDown.CornerType = eCornerType.Square;
            this.groupPanel2.StyleMouseOver.Class = "";
            this.groupPanel2.StyleMouseOver.CornerType = eCornerType.Square;
            this.groupPanel2.TabIndex = 10;
            this.groupPanel2.Text = "ToTown Waypoints";
            this.setVendorNameButton.AccessibleRole = AccessibleRole.PushButton;
            this.setVendorNameButton.ColorTable = eButtonColor.OrangeWithBackground;
            this.setVendorNameButton.Location = new Point(0x11b, 0x26);
            this.setVendorNameButton.Name = "setVendorNameButton";
            this.setVendorNameButton.Size = new Size(0x74, 0x17);
            this.setVendorNameButton.Style = eDotNetBarStyle.StyleManagerControlled;
            this.setVendorNameButton.TabIndex = 0x12;
            this.setVendorNameButton.Text = "My Current Target";
            this.setVendorNameButton.Click += new EventHandler(this.SetVendorNameButtonClick);
            this.labelX7.BackColor = Color.Transparent;
            this.labelX7.BackgroundStyle.Class = "";
            this.labelX7.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX7.Location = new Point(4, 40);
            this.labelX7.Name = "labelX7";
            this.labelX7.Size = new Size(0x3e, 0x17);
            this.labelX7.TabIndex = 13;
            this.labelX7.Text = "NPC Name:";
            this.deleteToTownButton.AccessibleRole = AccessibleRole.PushButton;
            this.deleteToTownButton.ColorTable = eButtonColor.OrangeWithBackground;
            this.deleteToTownButton.Location = new Point(0x126, 8);
            this.deleteToTownButton.Name = "deleteToTownButton";
            this.deleteToTownButton.Size = new Size(90, 0x17);
            this.deleteToTownButton.Style = eDotNetBarStyle.StyleManagerControlled;
            this.deleteToTownButton.TabIndex = 7;
            this.deleteToTownButton.Text = "Delete All";
            this.deleteToTownButton.Click += new EventHandler(this.DeleteToTownButtonClick);
            this.vendorNameBox.Border.Class = "TextBoxBorder";
            this.vendorNameBox.Border.CornerType = eCornerType.Square;
            this.vendorNameBox.Location = new Point(0x48, 0x29);
            this.vendorNameBox.Name = "vendorNameBox";
            this.vendorNameBox.Size = new Size(0xcd, 20);
            this.vendorNameBox.TabIndex = 12;
            this.vendorNameBox.TextChanged += new EventHandler(this.VendorNameBoxTextChanged);
            this.deleteLastToTownButton.AccessibleRole = AccessibleRole.PushButton;
            this.deleteLastToTownButton.ColorTable = eButtonColor.OrangeWithBackground;
            this.deleteLastToTownButton.Location = new Point(0xc6, 8);
            this.deleteLastToTownButton.Name = "deleteLastToTownButton";
            this.deleteLastToTownButton.Size = new Size(90, 0x17);
            this.deleteLastToTownButton.Style = eDotNetBarStyle.StyleManagerControlled;
            this.deleteLastToTownButton.TabIndex = 9;
            this.deleteLastToTownButton.Text = "Delete Last";
            this.deleteLastToTownButton.Click += new EventHandler(this.DeleteLastToTownButtonClick);
            this.addToTownButton.AccessibleRole = AccessibleRole.PushButton;
            this.addToTownButton.ColorTable = eButtonColor.OrangeWithBackground;
            this.addToTownButton.Location = new Point(0x65, 8);
            this.addToTownButton.Name = "addToTownButton";
            this.addToTownButton.Size = new Size(0x5d, 0x17);
            this.addToTownButton.Style = eDotNetBarStyle.StyleManagerControlled;
            this.addToTownButton.TabIndex = 8;
            this.addToTownButton.Text = "Add Position";
            this.addToTownButton.Click += new EventHandler(this.AddToTownButtonClick);
            this.labelX3.BackColor = Color.Transparent;
            this.labelX3.BackgroundStyle.Class = "";
            this.labelX3.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX3.Location = new Point(4, 5);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new Size(0x23, 0x17);
            this.labelX3.Style = eDotNetBarStyle.StyleManagerControlled;
            this.labelX3.TabIndex = 6;
            this.labelX3.Text = "Count:";
            this.toTownCountBox.Border.Class = "TextBoxBorder";
            this.toTownCountBox.Border.CornerType = eCornerType.Square;
            this.toTownCountBox.Location = new Point(0x2e, 11);
            this.toTownCountBox.Name = "toTownCountBox";
            this.toTownCountBox.ReadOnly = true;
            this.toTownCountBox.Size = new Size(0x31, 20);
            this.toTownCountBox.TabIndex = 5;
            this.groupPanel6.CanvasColor = SystemColors.Control;
            this.groupPanel6.ColorSchemeStyle = eDotNetBarStyle.Office2007;
            this.groupPanel6.Controls.Add(this.testToTownButton);
            this.groupPanel6.Controls.Add(this.testNormalButton);
            this.groupPanel6.Location = new Point(11, 0x137);
            this.groupPanel6.Name = "groupPanel6";
            this.groupPanel6.Size = new Size(0x198, 0x3d);
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
            this.groupPanel6.Style.Class = "";
            this.groupPanel6.Style.CornerDiameter = 4;
            this.groupPanel6.Style.CornerType = eCornerType.Rounded;
            this.groupPanel6.Style.TextAlignment = eStyleTextAlignment.Center;
            this.groupPanel6.Style.TextColorSchemePart = eColorSchemePart.PanelText;
            this.groupPanel6.Style.TextLineAlignment = eStyleTextAlignment.Near;
            this.groupPanel6.StyleMouseDown.Class = "";
            this.groupPanel6.StyleMouseDown.CornerType = eCornerType.Square;
            this.groupPanel6.StyleMouseOver.Class = "";
            this.groupPanel6.StyleMouseOver.CornerType = eCornerType.Square;
            this.groupPanel6.TabIndex = 14;
            this.groupPanel6.Text = "Test Waypoints";
            this.testToTownButton.AccessibleRole = AccessibleRole.PushButton;
            this.testToTownButton.ColorTable = eButtonColor.OrangeWithBackground;
            this.testToTownButton.Location = new Point(210, 7);
            this.testToTownButton.Name = "testToTownButton";
            this.testToTownButton.Size = new Size(0x74, 0x17);
            this.testToTownButton.Style = eDotNetBarStyle.StyleManagerControlled;
            this.testToTownButton.TabIndex = 1;
            this.testToTownButton.Text = "Test ToTown";
            this.testToTownButton.Click += new EventHandler(this.TestToTownButtonClick);
            this.testNormalButton.AccessibleRole = AccessibleRole.PushButton;
            this.testNormalButton.ColorTable = eButtonColor.OrangeWithBackground;
            this.testNormalButton.Location = new Point(0x48, 7);
            this.testNormalButton.Name = "testNormalButton";
            this.testNormalButton.Size = new Size(0x84, 0x17);
            this.testNormalButton.Style = eDotNetBarStyle.StyleManagerControlled;
            this.testNormalButton.TabIndex = 0;
            this.testNormalButton.Text = "Test Normal";
            this.testNormalButton.Click += new EventHandler(this.TestNormalButtonClick);
            this.groupPanel7.CanvasColor = SystemColors.Control;
            this.groupPanel7.ColorSchemeStyle = eDotNetBarStyle.Office2007;
            this.groupPanel7.Controls.Add(this.CBNaturalRun);
            this.groupPanel7.Controls.Add(this.hotkeySwitchButton);
            this.groupPanel7.Controls.Add(this.labelX4);
            this.groupPanel7.Controls.Add(this.labelX10);
            this.groupPanel7.Controls.Add(this.recordSwitchButton);
            this.groupPanel7.Controls.Add(this.labelX9);
            this.groupPanel7.Controls.Add(this.distanceInput);
            this.groupPanel7.Controls.Add(this.labelX8);
            this.groupPanel7.Controls.Add(this.profileTypeComboBox);
            this.groupPanel7.Location = new Point(12, 0x20);
            this.groupPanel7.Name = "groupPanel7";
            this.groupPanel7.Size = new Size(0x198, 0x6f);
            this.groupPanel7.Style.BackColor2SchemePart = eColorSchemePart.PanelBackground2;
            this.groupPanel7.Style.BackColorGradientAngle = 90;
            this.groupPanel7.Style.BackColorSchemePart = eColorSchemePart.PanelBackground;
            this.groupPanel7.Style.BorderBottom = eStyleBorderType.Solid;
            this.groupPanel7.Style.BorderBottomWidth = 1;
            this.groupPanel7.Style.BorderColorSchemePart = eColorSchemePart.PanelBorder;
            this.groupPanel7.Style.BorderLeft = eStyleBorderType.Solid;
            this.groupPanel7.Style.BorderLeftWidth = 1;
            this.groupPanel7.Style.BorderRight = eStyleBorderType.Solid;
            this.groupPanel7.Style.BorderRightWidth = 1;
            this.groupPanel7.Style.BorderTop = eStyleBorderType.Solid;
            this.groupPanel7.Style.BorderTopWidth = 1;
            this.groupPanel7.Style.Class = "";
            this.groupPanel7.Style.CornerDiameter = 4;
            this.groupPanel7.Style.CornerType = eCornerType.Rounded;
            this.groupPanel7.Style.TextAlignment = eStyleTextAlignment.Center;
            this.groupPanel7.Style.TextColorSchemePart = eColorSchemePart.PanelText;
            this.groupPanel7.Style.TextLineAlignment = eStyleTextAlignment.Near;
            this.groupPanel7.StyleMouseDown.Class = "";
            this.groupPanel7.StyleMouseDown.CornerType = eCornerType.Square;
            this.groupPanel7.StyleMouseOver.Class = "";
            this.groupPanel7.StyleMouseOver.CornerType = eCornerType.Square;
            this.groupPanel7.TabIndex = 15;
            this.groupPanel7.Text = "Global";
            this.CBNaturalRun.BackColor = Color.Transparent;
            this.CBNaturalRun.BackgroundStyle.Class = "";
            this.CBNaturalRun.BackgroundStyle.CornerType = eCornerType.Square;
            this.CBNaturalRun.Location = new Point(3, 0x41);
            this.CBNaturalRun.Name = "CBNaturalRun";
            this.CBNaturalRun.Size = new Size(100, 0x17);
            this.CBNaturalRun.Style = eDotNetBarStyle.StyleManagerControlled;
            this.CBNaturalRun.TabIndex = 0x15;
            this.CBNaturalRun.Text = "Natural run";
            this.CBNaturalRun.CheckedChanged += new EventHandler(this.CBNaturalRun_CheckedChanged);
            this.hotkeySwitchButton.BackgroundStyle.Class = "";
            this.hotkeySwitchButton.BackgroundStyle.CornerType = eCornerType.Square;
            this.hotkeySwitchButton.Location = new Point(0x135, 0x24);
            this.hotkeySwitchButton.Name = "hotkeySwitchButton";
            this.hotkeySwitchButton.Size = new Size(0x58, 0x16);
            this.hotkeySwitchButton.Style = eDotNetBarStyle.StyleManagerControlled;
            this.hotkeySwitchButton.TabIndex = 20;
            this.hotkeySwitchButton.Value = true;
            this.labelX4.BackColor = Color.Transparent;
            this.labelX4.BackgroundStyle.Class = "";
            this.labelX4.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX4.Location = new Point(0xc2, 0x24);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new Size(0x72, 0x17);
            this.labelX4.TabIndex = 0x13;
            this.labelX4.Text = "Control With Hotkeys:";
            this.labelX10.BackColor = Color.Transparent;
            this.labelX10.BackgroundStyle.Class = "";
            this.labelX10.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX10.Location = new Point(0xe9, 12);
            this.labelX10.Name = "labelX10";
            this.labelX10.Size = new Size(0x4b, 0x17);
            this.labelX10.TabIndex = 0x12;
            this.labelX10.Text = "Auto-Record:";
            this.recordSwitchButton.BackgroundStyle.Class = "";
            this.recordSwitchButton.BackgroundStyle.CornerType = eCornerType.Square;
            this.recordSwitchButton.Location = new Point(0x135, 10);
            this.recordSwitchButton.Name = "recordSwitchButton";
            this.recordSwitchButton.Size = new Size(0x58, 0x16);
            this.recordSwitchButton.Style = eDotNetBarStyle.StyleManagerControlled;
            this.recordSwitchButton.TabIndex = 0x11;
            this.recordSwitchButton.ValueChanged += new EventHandler(this.RecordSwitchButtonValueChanged);
            this.labelX9.BackColor = Color.Transparent;
            this.labelX9.BackgroundStyle.Class = "";
            this.labelX9.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX9.Location = new Point(3, 0x24);
            this.labelX9.Name = "labelX9";
            this.labelX9.Size = new Size(0x65, 0x17);
            this.labelX9.TabIndex = 0x10;
            this.labelX9.Text = "Waypoint Distance:";
            this.distanceInput.BackgroundStyle.Class = "DateTimeInputBackground";
            this.distanceInput.BackgroundStyle.CornerType = eCornerType.Square;
            this.distanceInput.ButtonFreeText.Shortcut = eShortcut.F2;
            this.distanceInput.Location = new Point(110, 0x27);
            this.distanceInput.MinValue = 0;
            this.distanceInput.Name = "distanceInput";
            this.distanceInput.ShowUpDown = true;
            this.distanceInput.Size = new Size(0x4e, 20);
            this.distanceInput.TabIndex = 15;
            this.distanceInput.Value = 0x19;
            this.labelX8.BackColor = Color.Transparent;
            this.labelX8.BackgroundStyle.Class = "";
            this.labelX8.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX8.Location = new Point(3, 6);
            this.labelX8.Name = "labelX8";
            this.labelX8.Size = new Size(0x4e, 0x17);
            this.labelX8.TabIndex = 14;
            this.labelX8.Text = "Waypoint Type:";
            this.profileTypeComboBox.DisplayMember = "Text";
            this.profileTypeComboBox.DrawMode = DrawMode.OwnerDrawFixed;
            this.profileTypeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            this.profileTypeComboBox.ForeColor = SystemColors.WindowText;
            this.profileTypeComboBox.FormattingEnabled = true;
            this.profileTypeComboBox.ItemHeight = 14;
            object[] objArray = new object[] { this.comboItem1, this.comboItem2 };
            this.profileTypeComboBox.Items.AddRange(objArray);
            this.profileTypeComboBox.Location = new Point(110, 12);
            this.profileTypeComboBox.Name = "profileTypeComboBox";
            this.profileTypeComboBox.Size = new Size(0x4e, 20);
            this.profileTypeComboBox.Style = eDotNetBarStyle.StyleManagerControlled;
            this.profileTypeComboBox.TabIndex = 0;
            this.profileTypeComboBox.SelectedIndexChanged += new EventHandler(this.ProfileTypeComboBoxSelectedIndexChanged);
            this.comboItem1.Text = "Flying";
            this.comboItem2.Text = "ToTown";
            this.galleryContainer1.BackgroundStyle.Class = "";
            this.galleryContainer1.BackgroundStyle.CornerType = eCornerType.Square;
            this.galleryContainer1.EnableGalleryPopup = false;
            this.galleryContainer1.LayoutOrientation = eOrientation.Vertical;
            this.galleryContainer1.MinimumSize = new Size(150, 200);
            this.galleryContainer1.MultiLine = false;
            this.galleryContainer1.Name = "galleryContainer1";
            this.galleryContainer1.PopupUsesStandardScrollbars = false;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.FromArgb(0xdf, 0xe9, 0xf5);
            base.ClientSize = new Size(0x1af, 0x17a);
            base.Controls.Add(this.groupPanel7);
            base.Controls.Add(this.groupPanel6);
            base.Controls.Add(this.groupPanel2);
            base.Controls.Add(this.groupPanel1);
            base.Controls.Add(this.dockSite2);
            base.Controls.Add(this.dockSite1);
            base.Controls.Add(this.dockSite3);
            base.Controls.Add(this.dockSite4);
            base.Controls.Add(this.dockSite5);
            base.Controls.Add(this.dockSite6);
            base.Controls.Add(this.dockSite7);
            base.Controls.Add(this.dockSite8);
            this.DoubleBuffered = true;
            base.MaximizeBox = false;
            this.MaximumSize = new Size(0x1bf, 0x1a0);
            this.MinimumSize = new Size(0x1bf, 0x1a0);
            base.Name = "GatherProfileForm";
            base.ShowIcon = false;
            this.Text = "Profile Settings";
            base.FormClosing += new FormClosingEventHandler(this.GrindingProfileFormFormClosing);
            base.Load += new EventHandler(this.GatherProfileFormLoad);
            this.dockSite7.ResumeLayout(false);
            this.bar1.EndInit();
            this.groupPanel1.ResumeLayout(false);
            this.groupPanel2.ResumeLayout(false);
            this.groupPanel6.ResumeLayout(false);
            this.groupPanel7.ResumeLayout(false);
            this.distanceInput.EndInit();
            base.ResumeLayout(false);
        }

        private void LoadProfile()
        {
            OpenFileDialog dialog = new OpenFileDialog {
                InitialDirectory = AppDomain.CurrentDomain.BaseDirectory,
                Filter = "Profiles (*.xml)|*.xml"
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this._profileToLoad = dialog.FileName;
                if (this._profileToLoad.Contains(".xml"))
                {
                    GatherEngine.CurrentProfile = new GatherProfile();
                    GatherEngine.CurrentProfile.LoadFile(this._profileToLoad);
                    GatherSettings.Profile = this._profileToLoad;
                    GatherSettings.SaveSettings();
                    this.UpdateControls();
                }
                else
                {
                    Logging.Write(LogType.Warning, "Please select a valid profile type.", new object[0]);
                }
            }
        }

        private void loadProfileButton1_Click(object sender, EventArgs e)
        {
            this.LoadProfile();
        }

        private void LoadProfileButtonClick(object sender, EventArgs e)
        {
            this.LoadProfile();
        }

        private void NormalKeyHotKeyPressed(object sender, EventArgs e)
        {
            if (this.hotkeySwitchButton.Value)
            {
                GatherEngine.CurrentProfile.AddSingleWayPoint(LazyLib.Wow.ObjectManager.MyPlayer.Location);
                Logging.Write(LogType.Info, string.Concat(new object[] { "Added Normal Waypoint at X: ", LazyLib.Wow.ObjectManager.MyPlayer.Location.X, " Y: ", LazyLib.Wow.ObjectManager.MyPlayer.Location.Y, " Z: ", LazyLib.Wow.ObjectManager.MyPlayer.Location.Z }), new object[0]);
                this.UpdateWaypointsCount();
            }
        }

        private void ProfileChanged(object sender, EProfileDownloaded e)
        {
            string path = e.Path;
            if (!path.Contains(".xml"))
            {
                MessageBox.Show("Could not load the downloaded profile, invalid profile type");
            }
            else
            {
                GatherEngine.CurrentProfile = new GatherProfile();
                GatherEngine.CurrentProfile.LoadFile(path);
                GatherSettings.Profile = path;
                GatherSettings.SaveSettings();
                this.UpdateControls();
            }
        }

        private void ProfileTypeComboBoxSelectedIndexChanged(object sender, EventArgs e)
        {
            this._waypointSelected = (FlyingWaypointsType) this.profileTypeComboBox.SelectedIndex;
        }

        private void RecordSwitchButtonValueChanged(object sender, EventArgs e)
        {
            this._autoRecord = this.recordSwitchButton.Value;
            if (this._autoRecord)
            {
                this.StartThread(ref this._waypointThread, new Action(this.WaypointThread), "Waypoint Thread", false);
            }
            else
            {
                this.StopThread(this._waypointThread);
            }
        }

        private void SaveProfile()
        {
            SaveFileDialog dialog = new SaveFileDialog {
                InitialDirectory = AppDomain.CurrentDomain.BaseDirectory,
                Filter = "Profiles (*.xml)|*.xml"
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                GatherEngine.CurrentProfile.SaveFile(dialog.FileName);
                GatherSettings.Profile = dialog.FileName;
                GatherSettings.SaveSettings();
            }
        }

        private void saveProfileButton1_Click(object sender, EventArgs e)
        {
            this.SaveProfile();
        }

        private void SaveProfileButtonClick(object sender, EventArgs e)
        {
            this.SaveProfile();
        }

        private void SetVendorNameButtonClick(object sender, EventArgs e)
        {
            this.vendorNameBox.Text = LazyLib.Wow.ObjectManager.MyPlayer.Target.Name;
        }

        private void StartThread(ref Thread thread, Action work, string name, bool backgroundThread)
        {
            if (thread != null)
            {
                if (thread.IsAlive)
                {
                    thread.Join();
                    GC.Collect();
                }
                Thread thread2 = new Thread(new ThreadStart(work.Invoke)) {
                    Name = name,
                    IsBackground = backgroundThread
                };
                thread = thread2;
                thread.Start();
            }
        }

        private void StopThread(Thread target)
        {
            try
            {
                if (target != null)
                {
                    _endThread[target] = true;
                    target.Join();
                    _endThread[target] = false;
                    GC.Collect();
                }
            }
            catch (ThreadStateException)
            {
            }
        }

        private void TerminateThread(Thread thread)
        {
            try
            {
                if (thread != null)
                {
                    thread.Abort();
                    thread.Join();
                    GC.Collect();
                }
            }
            catch (ThreadStateException)
            {
            }
        }

        private void TestNormalButtonClick(object sender, EventArgs e)
        {
            GatherEngine.CurrentMode = Mode.TestNormal;
            Logging.Write(LogType.Warning, "Set Flying mode to TestNormal, start the bot to test", new object[0]);
        }

        private void TestToTownButtonClick(object sender, EventArgs e)
        {
            GatherEngine.CurrentMode = Mode.TestToTown;
            Logging.Write(LogType.Warning, "Set Flying mode to TestTown, start the bot to test", new object[0]);
        }

        private void UpdateControls()
        {
            this.UpdateWaypointsCount();
            this.vendorNameBox.Text = GatherEngine.CurrentProfile.VendorName;
            this.CBNaturalRun.Checked = GatherEngine.CurrentProfile.NaturalRun;
        }

        public static float UpdateProgressBar(int value)
        {
            float single1 = value;
            ProgressBarCounter = single1;
            return single1;
        }

        public static float UpdateProgressBar(List<Location> waypoints, Location currentLocation)
        {
            bool flag = false;
            double num = 9999999.0;
            for (int i = 0; (i < waypoints.Count) && !flag; i++)
            {
                if (waypoints[i].DistanceFrom(currentLocation) < num)
                {
                    num = waypoints[i].DistanceFrom(currentLocation);
                    ProgressBarCounter = (((float) i) / ((float) waypoints.Count)) * 100f;
                }
                else if (num < waypoints[i].DistanceFrom(currentLocation))
                {
                    flag = true;
                }
            }
            return ProgressBarCounter;
        }

        public static float UpdateProgressBar(List<Location> waypoints, int positionInprofile)
        {
            float single1 = (((float) positionInprofile) / ((float) waypoints.Count)) * 100f;
            ProgressBarCounter = single1;
            return single1;
        }

        public void UpdateText(Control control, string text)
        {
            if (control.InvokeRequired)
            {
                control.BeginInvoke(() => this.UpdateText(control, text));
            }
            else
            {
                control.Text = text;
            }
        }

        private void UpdateWaypointsCount()
        {
            this.UpdateText(this.normalCountBox, Convert.ToString(GatherEngine.CurrentProfile.WaypointsNormal.Count));
            this.UpdateText(this.toTownCountBox, Convert.ToString(GatherEngine.CurrentProfile.WaypointsToTown.Count));
            if (GatherEngine.CurrentProfile.VendorName != null)
            {
                this.UpdateText(this.vendorNameBox, GatherEngine.CurrentProfile.VendorName);
            }
        }

        private void VendorHotKeyPressed(object sender, EventArgs e)
        {
            if (this.hotkeySwitchButton.Value)
            {
                GatherEngine.CurrentProfile.AddSingleToTownWayPoint(LazyLib.Wow.ObjectManager.MyPlayer.Location);
                Logging.Write(LogType.Info, string.Concat(new object[] { "Added Vendor Waypoint at X: ", LazyLib.Wow.ObjectManager.MyPlayer.Location.X, " Y: ", LazyLib.Wow.ObjectManager.MyPlayer.Location.Y, " Z: ", LazyLib.Wow.ObjectManager.MyPlayer.Location.Z }), new object[0]);
                this.UpdateWaypointsCount();
            }
        }

        private void VendorNameBoxTextChanged(object sender, EventArgs e)
        {
            if (this.vendorNameBox.Text.Trim().Length > 0)
            {
                GatherEngine.CurrentProfile.VendorName = this.vendorNameBox.Text;
            }
        }

        private void WaypointThread()
        {
            GatherEngine.CurrentProfile ??= new GatherProfile();
            switch (this._waypointSelected)
            {
                case FlyingWaypointsType.Normal:
                    GatherEngine.CurrentProfile.AddSingleWayPoint(LazyLib.Wow.ObjectManager.MyPlayer.Location);
                    Logging.Write(LogType.Info, string.Concat(new object[] { "Added Normal Waypoint at X: ", LazyLib.Wow.ObjectManager.MyPlayer.Location.X, " Y: ", LazyLib.Wow.ObjectManager.MyPlayer.Location.Y, " Z: ", LazyLib.Wow.ObjectManager.MyPlayer.Location.Z }), new object[0]);
                    break;

                case FlyingWaypointsType.ToTown:
                    GatherEngine.CurrentProfile.AddSingleToTownWayPoint(LazyLib.Wow.ObjectManager.MyPlayer.Location);
                    Logging.Write(LogType.Info, string.Concat(new object[] { "Added ToTown Waypoint at X: ", LazyLib.Wow.ObjectManager.MyPlayer.Location.X, " Y: ", LazyLib.Wow.ObjectManager.MyPlayer.Location.Y, " Z: ", LazyLib.Wow.ObjectManager.MyPlayer.Location.Z }), new object[0]);
                    break;

                default:
                    GatherEngine.CurrentProfile.AddSingleWayPoint(LazyLib.Wow.ObjectManager.MyPlayer.Location);
                    Logging.Write(LogType.Info, string.Concat(new object[] { "Added Normal Waypoint at X: ", LazyLib.Wow.ObjectManager.MyPlayer.Location.X, " Y: ", LazyLib.Wow.ObjectManager.MyPlayer.Location.Y, " Z: ", LazyLib.Wow.ObjectManager.MyPlayer.Location.Z }), new object[0]);
                    break;
            }
            this.UpdateWaypointsCount();
            Location pos = LazyLib.Wow.ObjectManager.MyPlayer.Location;
            while (!this.EndThread(this._waypointThread))
            {
                int num = this.distanceInput.Value;
                if (LazyLib.Wow.ObjectManager.MyPlayer.Location.DistanceFromXY(pos) > num)
                {
                    switch (this._waypointSelected)
                    {
                        case FlyingWaypointsType.Normal:
                            GatherEngine.CurrentProfile.AddSingleWayPoint(LazyLib.Wow.ObjectManager.MyPlayer.Location);
                            Logging.Write(LogType.Info, string.Concat(new object[] { "Added Normal Waypoint at X: ", LazyLib.Wow.ObjectManager.MyPlayer.Location.X, " Y: ", LazyLib.Wow.ObjectManager.MyPlayer.Location.Y, " Z: ", LazyLib.Wow.ObjectManager.MyPlayer.Location.Z }), new object[0]);
                            break;

                        case FlyingWaypointsType.ToTown:
                            GatherEngine.CurrentProfile.AddSingleToTownWayPoint(LazyLib.Wow.ObjectManager.MyPlayer.Location);
                            Logging.Write(LogType.Info, string.Concat(new object[] { "Added ToTown Waypoint at X: ", LazyLib.Wow.ObjectManager.MyPlayer.Location.X, " Y: ", LazyLib.Wow.ObjectManager.MyPlayer.Location.Y, " Z: ", LazyLib.Wow.ObjectManager.MyPlayer.Location.Z }), new object[0]);
                            break;

                        default:
                            GatherEngine.CurrentProfile.AddSingleWayPoint(LazyLib.Wow.ObjectManager.MyPlayer.Location);
                            Logging.Write(LogType.Info, string.Concat(new object[] { "Added Normal Waypoint at X: ", LazyLib.Wow.ObjectManager.MyPlayer.Location.X, " Y: ", LazyLib.Wow.ObjectManager.MyPlayer.Location.Y, " Z: ", LazyLib.Wow.ObjectManager.MyPlayer.Location.Z }), new object[0]);
                            break;
                    }
                    pos = LazyLib.Wow.ObjectManager.MyPlayer.Location;
                    this.UpdateWaypointsCount();
                }
            }
        }

        public static float ProgressBarCounter { get; private set; }
    }
}

