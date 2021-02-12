namespace LazyEvo.LGatherEngine
{
    using DevComponents.AdvTree;
    using DevComponents.DotNetBar;
    using DevComponents.DotNetBar.Controls;
    using DevComponents.DotNetBar.Metro.ColorTables;
    using DevComponents.Editors;
    using LazyEvo.LGatherEngine.Helpers;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    internal class Settings : Office2007Form
    {
        private IContainer components;
        private StyleManager styleManager1;
        private ButtonX SaveSettings;
        private LabelX labelX40;
        private LabelX labelX33;
        private IntegerInput SetupTBApproachModifier;
        private ComboItem key0;
        private ComboItem key9;
        private ComboItem key8;
        private ComboItem key7;
        private ComboItem key6;
        private ComboItem key5;
        private ComboItem key4;
        private ComboItem key3;
        private ComboItem key2;
        private ComboItem key1;
        private LabelX labelX8;
        private ComboItem bar0;
        private ComboItem bar5;
        private ComboItem bar4;
        private ComboItem bar3;
        private ComboItem bar2;
        private ComboItem bar1;
        private SuperTabControl superTabControl1;
        private SuperTabControlPanel superTabControlPanel1;
        private SuperTabItem Generalddd;
        private SuperTabControlPanel superTabControlPanel2;
        private SuperTabItem superTabItem1;
        private TextBoxX TBHerbName;
        private ButtonX BtnAddHerb;
        private ButtonX BtnRemoveHerb;
        private DevComponents.AdvTree.AdvTree ListHerbItems;
        private NodeConnector nodeConnector1;
        private ElementStyle elementStyle1;
        private ButtonX BtnRemoveMine;
        private TextBoxX TBMineName;
        private ButtonX BtnAddMine;
        private GroupPanel groupPanel1;
        private GroupPanel groupPanel2;
        private DevComponents.AdvTree.AdvTree ListMineItems;
        private NodeConnector nodeConnector2;
        private ElementStyle elementStyle2;
        private GroupPanel groupPanel3;
        private GroupPanel groupPanel4;
        private GroupPanel groupPanel5;
        private LabelX labelX2;
        private LabelX labelX1;
        private CheckBoxX CBFish;
        private CheckBoxX CBUseLure;
        private ComboItem comboItem1;
        private ComboItem comboItem2;
        private ComboItem comboItem3;
        private ComboItem comboItem4;
        private ComboItem comboItem5;
        private ComboItem comboItem6;
        private LabelX labelX3;
        private ComboItem comboItem7;
        private ComboItem comboItem8;
        private ComboItem comboItem9;
        private ComboItem comboItem10;
        private ComboItem comboItem11;
        private ComboItem comboItem12;
        private ComboItem comboItem13;
        private ComboItem comboItem14;
        private ComboItem comboItem15;
        private ComboItem comboItem16;
        private ComboItem comboItem17;
        private ComboItem comboItem18;
        private ComboItem comboItem19;
        private ComboItem comboItem20;
        private ComboItem comboItem21;
        private ComboItem comboItem22;
        private LabelX labelX4;
        private ComboItem comboItem23;
        private ComboItem comboItem24;
        private ComboItem comboItem25;
        private ComboItem comboItem26;
        private ComboItem comboItem27;
        private ComboItem comboItem28;
        private ComboItem comboItem29;
        private ComboItem comboItem30;
        private ComboItem comboItem31;
        private ComboItem comboItem32;
        private SuperTabControlPanel superTabControlPanel3;
        private GroupPanel groupPanel6;
        private DevComponents.AdvTree.AdvTree ListSchoolItems;
        private NodeConnector nodeConnector3;
        private ElementStyle elementStyle3;
        private ButtonX BtnRemoveSchool;
        private TextBoxX TBSchoolName;
        private ButtonX BtnAddSchool;
        private SuperTabItem superTabItem2;
        private ComboItem comboItem33;
        private ComboItem comboItem34;
        private ComboItem comboItem35;
        private ComboItem comboItem36;
        private ComboItem comboItem37;
        private ComboItem comboItem38;
        private ComboItem comboItem39;
        private ComboItem comboItem40;
        private ComboItem comboItem41;
        private ComboItem comboItem42;
        private ComboItem comboItem43;
        private ComboItem comboItem44;
        private ComboItem comboItem45;
        private ComboItem comboItem46;
        private ComboItem comboItem47;
        private ComboItem comboItem48;
        private IntegerInput SetupTBMaxUnits;
        private CheckBoxX SetupCBStopOnDeath;
        private CheckBoxX SetupCBStopHarvest;
        private CheckBoxX SetupCBAvoidPlayers;
        private CheckBoxX SetupCBMine;
        private CheckBoxX SetupCBHerb;
        private CheckBoxX CBAvoidElites;
        private CheckBoxX CBAutoBlacklist;
        private ComboBoxEx KeysFlyingMountKey;
        private ComboBoxEx KeysFlyingMountBar;
        private CheckBoxX SetupCBFindCorpse;
        private CheckBoxX CBStopOnFullBags;
        private CheckBoxX CBRessWait;
        private CheckBoxX CBWaitForLoot;
        private IntegerInput SetupTBFishApproach;
        private IntegerInput SetupTBMaxTimeAtSchool;
        private ComboBoxEx KeysLureBar;
        private ComboBoxEx KeysLureKey;
        private ComboBoxEx KeysWaterwalkBar;
        private ComboBoxEx KeysWaterwalkKey;
        private CheckBoxX CBDruidAvoidCombat;
        private ComboBoxEx KeysExtraBar;
        private ComboBoxEx KeysExtraKey;
        private CheckBoxX CBSendKeyOnStartCombat;
        private LabelX labelX5;
        private CheckBoxX CBschoolOnly;

        public Settings()
        {
            this.InitializeComponent();
        }

        private void AddHerb(string name)
        {
            Node node = new Node(name) {
                Tag = name
            };
            this.ListHerbItems.BeginUpdate();
            this.ListHerbItems.Nodes.Add(node);
            this.ListHerbItems.EndUpdate();
        }

        private void AddMine(string name)
        {
            Node node = new Node(name) {
                Tag = name
            };
            this.ListMineItems.BeginUpdate();
            this.ListMineItems.Nodes.Add(node);
            this.ListMineItems.EndUpdate();
        }

        private void AddSchool(string name)
        {
            Node node = new Node(name) {
                Tag = name
            };
            this.ListSchoolItems.BeginUpdate();
            this.ListSchoolItems.Nodes.Add(node);
            this.ListSchoolItems.EndUpdate();
        }

        private void BtnAddHerbClick(object sender, EventArgs e)
        {
            if (this.TBHerbName.Text != "")
            {
                this.AddHerb(this.TBHerbName.Text);
                this.TBHerbName.Text = "";
            }
        }

        private void BtnAddMineClick(object sender, EventArgs e)
        {
            if (this.TBMineName.Text != "")
            {
                this.AddMine(this.TBMineName.Text);
                this.TBMineName.Text = "";
            }
        }

        private void BtnAddSchool_Click(object sender, EventArgs e)
        {
            if (this.TBSchoolName.Text != "")
            {
                this.AddSchool(this.TBSchoolName.Text);
                this.TBSchoolName.Text = "";
            }
        }

        private void BtnRemoveHerbClick(object sender, EventArgs e)
        {
            if (this.ListHerbItems.SelectedNode != null)
            {
                this.ListHerbItems.Nodes.Remove(this.ListHerbItems.SelectedNode);
            }
        }

        private void BtnRemoveMineClick(object sender, EventArgs e)
        {
            if (this.ListMineItems.SelectedNode != null)
            {
                this.ListMineItems.Nodes.Remove(this.ListMineItems.SelectedNode);
            }
        }

        private void BtnRemoveSchool_Click(object sender, EventArgs e)
        {
            if (this.ListSchoolItems.SelectedNode != null)
            {
                this.ListSchoolItems.Nodes.Remove(this.ListSchoolItems.SelectedNode);
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

        private void InitializeComponent()
        {
            this.components = new Container();
            this.styleManager1 = new StyleManager(this.components);
            this.SaveSettings = new ButtonX();
            this.SetupTBMaxUnits = new IntegerInput();
            this.labelX40 = new LabelX();
            this.labelX33 = new LabelX();
            this.SetupCBStopOnDeath = new CheckBoxX();
            this.SetupCBStopHarvest = new CheckBoxX();
            this.SetupCBAvoidPlayers = new CheckBoxX();
            this.SetupCBMine = new CheckBoxX();
            this.SetupCBHerb = new CheckBoxX();
            this.SetupTBApproachModifier = new IntegerInput();
            this.CBAvoidElites = new CheckBoxX();
            this.CBAutoBlacklist = new CheckBoxX();
            this.key0 = new ComboItem();
            this.key9 = new ComboItem();
            this.key8 = new ComboItem();
            this.key7 = new ComboItem();
            this.key6 = new ComboItem();
            this.key5 = new ComboItem();
            this.key4 = new ComboItem();
            this.key3 = new ComboItem();
            this.key2 = new ComboItem();
            this.key1 = new ComboItem();
            this.KeysFlyingMountKey = new ComboBoxEx();
            this.labelX8 = new LabelX();
            this.bar0 = new ComboItem();
            this.bar5 = new ComboItem();
            this.bar4 = new ComboItem();
            this.bar3 = new ComboItem();
            this.bar2 = new ComboItem();
            this.bar1 = new ComboItem();
            this.KeysFlyingMountBar = new ComboBoxEx();
            this.superTabControl1 = new SuperTabControl();
            this.superTabControlPanel1 = new SuperTabControlPanel();
            this.groupPanel5 = new GroupPanel();
            this.KeysWaterwalkBar = new ComboBoxEx();
            this.comboItem17 = new ComboItem();
            this.comboItem18 = new ComboItem();
            this.comboItem19 = new ComboItem();
            this.comboItem20 = new ComboItem();
            this.comboItem21 = new ComboItem();
            this.comboItem22 = new ComboItem();
            this.labelX4 = new LabelX();
            this.KeysWaterwalkKey = new ComboBoxEx();
            this.comboItem23 = new ComboItem();
            this.comboItem24 = new ComboItem();
            this.comboItem25 = new ComboItem();
            this.comboItem26 = new ComboItem();
            this.comboItem27 = new ComboItem();
            this.comboItem28 = new ComboItem();
            this.comboItem29 = new ComboItem();
            this.comboItem30 = new ComboItem();
            this.comboItem31 = new ComboItem();
            this.comboItem32 = new ComboItem();
            this.KeysLureBar = new ComboBoxEx();
            this.comboItem1 = new ComboItem();
            this.comboItem2 = new ComboItem();
            this.comboItem3 = new ComboItem();
            this.comboItem4 = new ComboItem();
            this.comboItem5 = new ComboItem();
            this.comboItem6 = new ComboItem();
            this.labelX3 = new LabelX();
            this.KeysLureKey = new ComboBoxEx();
            this.comboItem7 = new ComboItem();
            this.comboItem8 = new ComboItem();
            this.comboItem9 = new ComboItem();
            this.comboItem10 = new ComboItem();
            this.comboItem11 = new ComboItem();
            this.comboItem12 = new ComboItem();
            this.comboItem13 = new ComboItem();
            this.comboItem14 = new ComboItem();
            this.comboItem15 = new ComboItem();
            this.comboItem16 = new ComboItem();
            this.CBFish = new CheckBoxX();
            this.CBUseLure = new CheckBoxX();
            this.labelX2 = new LabelX();
            this.SetupTBFishApproach = new IntegerInput();
            this.labelX1 = new LabelX();
            this.SetupTBMaxTimeAtSchool = new IntegerInput();
            this.groupPanel4 = new GroupPanel();
            this.KeysExtraBar = new ComboBoxEx();
            this.comboItem33 = new ComboItem();
            this.comboItem34 = new ComboItem();
            this.comboItem35 = new ComboItem();
            this.comboItem36 = new ComboItem();
            this.comboItem37 = new ComboItem();
            this.comboItem38 = new ComboItem();
            this.KeysExtraKey = new ComboBoxEx();
            this.comboItem39 = new ComboItem();
            this.comboItem40 = new ComboItem();
            this.comboItem41 = new ComboItem();
            this.comboItem42 = new ComboItem();
            this.comboItem43 = new ComboItem();
            this.comboItem44 = new ComboItem();
            this.comboItem45 = new ComboItem();
            this.comboItem46 = new ComboItem();
            this.comboItem47 = new ComboItem();
            this.comboItem48 = new ComboItem();
            this.CBSendKeyOnStartCombat = new CheckBoxX();
            this.CBDruidAvoidCombat = new CheckBoxX();
            this.CBRessWait = new CheckBoxX();
            this.SetupCBFindCorpse = new CheckBoxX();
            this.groupPanel3 = new GroupPanel();
            this.CBWaitForLoot = new CheckBoxX();
            this.CBStopOnFullBags = new CheckBoxX();
            this.Generalddd = new SuperTabItem();
            this.superTabControlPanel3 = new SuperTabControlPanel();
            this.groupPanel6 = new GroupPanel();
            this.ListSchoolItems = new DevComponents.AdvTree.AdvTree();
            this.nodeConnector3 = new NodeConnector();
            this.elementStyle3 = new ElementStyle();
            this.BtnRemoveSchool = new ButtonX();
            this.TBSchoolName = new TextBoxX();
            this.BtnAddSchool = new ButtonX();
            this.superTabItem2 = new SuperTabItem();
            this.superTabControlPanel2 = new SuperTabControlPanel();
            this.groupPanel2 = new GroupPanel();
            this.BtnRemoveMine = new ButtonX();
            this.TBMineName = new TextBoxX();
            this.ListMineItems = new DevComponents.AdvTree.AdvTree();
            this.nodeConnector2 = new NodeConnector();
            this.elementStyle2 = new ElementStyle();
            this.BtnAddMine = new ButtonX();
            this.groupPanel1 = new GroupPanel();
            this.ListHerbItems = new DevComponents.AdvTree.AdvTree();
            this.nodeConnector1 = new NodeConnector();
            this.elementStyle1 = new ElementStyle();
            this.BtnRemoveHerb = new ButtonX();
            this.TBHerbName = new TextBoxX();
            this.BtnAddHerb = new ButtonX();
            this.superTabItem1 = new SuperTabItem();
            this.labelX5 = new LabelX();
            this.CBschoolOnly = new CheckBoxX();
            this.SetupTBMaxUnits.BeginInit();
            this.SetupTBApproachModifier.BeginInit();
            ((ISupportInitialize) this.superTabControl1).BeginInit();
            this.superTabControl1.SuspendLayout();
            this.superTabControlPanel1.SuspendLayout();
            this.groupPanel5.SuspendLayout();
            this.SetupTBFishApproach.BeginInit();
            this.SetupTBMaxTimeAtSchool.BeginInit();
            this.groupPanel4.SuspendLayout();
            this.groupPanel3.SuspendLayout();
            this.superTabControlPanel3.SuspendLayout();
            this.groupPanel6.SuspendLayout();
            this.ListSchoolItems.BeginInit();
            this.superTabControlPanel2.SuspendLayout();
            this.groupPanel2.SuspendLayout();
            this.ListMineItems.BeginInit();
            this.groupPanel1.SuspendLayout();
            this.ListHerbItems.BeginInit();
            base.SuspendLayout();
            this.styleManager1.ManagerStyle = eStyle.Windows7Blue;
            this.styleManager1.MetroColorParameters = new MetroColorGeneratorParameters(Color.White, Color.FromArgb(0x2b, 0x57, 0x9a));
            this.SaveSettings.AccessibleRole = AccessibleRole.PushButton;
            this.SaveSettings.ColorTable = eButtonColor.OrangeWithBackground;
            this.SaveSettings.Location = new Point(0, 0x1a8);
            this.SaveSettings.Name = "SaveSettings";
            this.SaveSettings.Size = new Size(0x65, 0x17);
            this.SaveSettings.Style = eDotNetBarStyle.StyleManagerControlled;
            this.SaveSettings.TabIndex = 0x8a;
            this.SaveSettings.Text = "Save and close";
            this.SaveSettings.Click += new EventHandler(this.SaveSettingsClick);
            this.SetupTBMaxUnits.BackColor = Color.Transparent;
            this.SetupTBMaxUnits.BackgroundStyle.Class = "DateTimeInputBackground";
            this.SetupTBMaxUnits.BackgroundStyle.CornerType = eCornerType.Square;
            this.SetupTBMaxUnits.ButtonFreeText.Shortcut = eShortcut.F2;
            this.SetupTBMaxUnits.Location = new Point(0x88, 6);
            this.SetupTBMaxUnits.Name = "SetupTBMaxUnits";
            this.SetupTBMaxUnits.ShowUpDown = true;
            this.SetupTBMaxUnits.Size = new Size(0x27, 20);
            this.SetupTBMaxUnits.TabIndex = 0x84;
            this.labelX40.BackColor = Color.Transparent;
            this.labelX40.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX40.Location = new Point(3, 3);
            this.labelX40.Name = "labelX40";
            this.labelX40.Size = new Size(0x67, 0x17);
            this.labelX40.TabIndex = 0x83;
            this.labelX40.Text = "Max units at node:";
            this.labelX33.BackColor = Color.Transparent;
            this.labelX33.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX33.Location = new Point(3, 0x41);
            this.labelX33.Name = "labelX33";
            this.labelX33.Size = new Size(0x5f, 0x17);
            this.labelX33.TabIndex = 0x7a;
            this.labelX33.Text = "Z modifier:";
            this.SetupCBStopOnDeath.AutoSize = true;
            this.SetupCBStopOnDeath.BackColor = Color.Transparent;
            this.SetupCBStopOnDeath.BackgroundStyle.CornerType = eCornerType.Square;
            this.SetupCBStopOnDeath.Location = new Point(2, 0x59);
            this.SetupCBStopOnDeath.Name = "SetupCBStopOnDeath";
            this.SetupCBStopOnDeath.Size = new Size(0xa8, 15);
            this.SetupCBStopOnDeath.TabIndex = 0x7d;
            this.SetupCBStopOnDeath.Text = "Stop and play sound on death";
            this.SetupCBStopOnDeath.CheckedChanged += new EventHandler(this.SetupCbStopOnDeathCheckedChanged);
            this.SetupCBStopHarvest.AutoSize = true;
            this.SetupCBStopHarvest.BackColor = Color.Transparent;
            this.SetupCBStopHarvest.BackgroundStyle.CornerType = eCornerType.Square;
            this.SetupCBStopHarvest.Location = new Point(2, 0x44);
            this.SetupCBStopHarvest.Name = "SetupCBStopHarvest";
            this.SetupCBStopHarvest.Size = new Size(0xb1, 15);
            this.SetupCBStopHarvest.TabIndex = 0x7c;
            this.SetupCBStopHarvest.Text = "Stop harvesting if player around";
            this.SetupCBAvoidPlayers.AutoSize = true;
            this.SetupCBAvoidPlayers.BackColor = Color.Transparent;
            this.SetupCBAvoidPlayers.BackgroundStyle.CornerType = eCornerType.Square;
            this.SetupCBAvoidPlayers.Location = new Point(2, 0x1b);
            this.SetupCBAvoidPlayers.Name = "SetupCBAvoidPlayers";
            this.SetupCBAvoidPlayers.Size = new Size(0x80, 15);
            this.SetupCBAvoidPlayers.TabIndex = 0x89;
            this.SetupCBAvoidPlayers.Text = "Avoid players at node";
            this.SetupCBMine.AutoSize = true;
            this.SetupCBMine.BackColor = Color.Transparent;
            this.SetupCBMine.BackgroundStyle.CornerType = eCornerType.Square;
            this.SetupCBMine.Location = new Point(3, 0x2c);
            this.SetupCBMine.Name = "SetupCBMine";
            this.SetupCBMine.Size = new Size(0x2e, 15);
            this.SetupCBMine.TabIndex = 0x77;
            this.SetupCBMine.Text = "Mine";
            this.SetupCBHerb.AutoSize = true;
            this.SetupCBHerb.BackColor = Color.Transparent;
            this.SetupCBHerb.BackgroundStyle.CornerType = eCornerType.Square;
            this.SetupCBHerb.Location = new Point(3, 0x17);
            this.SetupCBHerb.Name = "SetupCBHerb";
            this.SetupCBHerb.Size = new Size(0x2e, 15);
            this.SetupCBHerb.TabIndex = 0x76;
            this.SetupCBHerb.Text = "Herb";
            this.SetupTBApproachModifier.BackgroundStyle.Class = "DateTimeInputBackground";
            this.SetupTBApproachModifier.BackgroundStyle.CornerType = eCornerType.Square;
            this.SetupTBApproachModifier.ButtonFreeText.Shortcut = eShortcut.F2;
            this.SetupTBApproachModifier.Location = new Point(0x88, 0x42);
            this.SetupTBApproachModifier.Name = "SetupTBApproachModifier";
            this.SetupTBApproachModifier.ShowUpDown = true;
            this.SetupTBApproachModifier.Size = new Size(0x27, 20);
            this.SetupTBApproachModifier.TabIndex = 0x88;
            this.CBAvoidElites.AutoSize = true;
            this.CBAvoidElites.BackColor = Color.Transparent;
            this.CBAvoidElites.BackgroundStyle.CornerType = eCornerType.Square;
            this.CBAvoidElites.Location = new Point(2, 0x30);
            this.CBAvoidElites.Name = "CBAvoidElites";
            this.CBAvoidElites.Size = new Size(0x4f, 15);
            this.CBAvoidElites.TabIndex = 0x8a;
            this.CBAvoidElites.Text = "Avoid elites";
            this.CBAutoBlacklist.AutoSize = true;
            this.CBAutoBlacklist.BackColor = Color.Transparent;
            this.CBAutoBlacklist.BackgroundStyle.CornerType = eCornerType.Square;
            this.CBAutoBlacklist.Location = new Point(0xe7, 0x17);
            this.CBAutoBlacklist.Name = "CBAutoBlacklist";
            this.CBAutoBlacklist.Size = new Size(0x57, 15);
            this.CBAutoBlacklist.TabIndex = 0x8b;
            this.CBAutoBlacklist.Text = "Auto blacklist";
            this.key0.Text = "0";
            this.key9.Text = "9";
            this.key8.Text = "8";
            this.key7.Text = "7";
            this.key6.Text = "6";
            this.key5.Text = "5";
            this.key4.Text = "4";
            this.key3.Text = "3";
            this.key2.Text = "2";
            this.key1.Text = "1";
            this.KeysFlyingMountKey.DisplayMember = "Text";
            this.KeysFlyingMountKey.DrawMode = DrawMode.OwnerDrawFixed;
            this.KeysFlyingMountKey.DropDownStyle = ComboBoxStyle.DropDownList;
            this.KeysFlyingMountKey.FormattingEnabled = true;
            this.KeysFlyingMountKey.ItemHeight = 14;
            object[] items = new object[] { this.key1, this.key2, this.key3, this.key4, this.key5, this.key6, this.key7, this.key8, this.key9 };
            items[9] = this.key0;
            this.KeysFlyingMountKey.Items.AddRange(items);
            this.KeysFlyingMountKey.Location = new Point(0xb5, 2);
            this.KeysFlyingMountKey.Name = "KeysFlyingMountKey";
            this.KeysFlyingMountKey.Size = new Size(0x27, 20);
            this.KeysFlyingMountKey.Style = eDotNetBarStyle.StyleManagerControlled;
            this.KeysFlyingMountKey.TabIndex = 0x100;
            this.labelX8.BackColor = Color.Transparent;
            this.labelX8.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX8.Location = new Point(3, 0);
            this.labelX8.Name = "labelX8";
            this.labelX8.Size = new Size(0x70, 0x17);
            this.labelX8.TabIndex = 0xff;
            this.labelX8.Text = "Mount (Bar and Key)";
            this.bar0.Text = "6";
            this.bar5.Text = "5";
            this.bar4.Text = "4";
            this.bar3.Text = "3";
            this.bar2.Text = "2";
            this.bar1.Text = "1";
            this.KeysFlyingMountBar.DisplayMember = "Text";
            this.KeysFlyingMountBar.DrawMode = DrawMode.OwnerDrawFixed;
            this.KeysFlyingMountBar.DropDownStyle = ComboBoxStyle.DropDownList;
            this.KeysFlyingMountBar.FormattingEnabled = true;
            this.KeysFlyingMountBar.ItemHeight = 14;
            object[] objArray2 = new object[] { this.bar1, this.bar2, this.bar3, this.bar4, this.bar5, this.bar0 };
            this.KeysFlyingMountBar.Items.AddRange(objArray2);
            this.KeysFlyingMountBar.Location = new Point(0x88, 2);
            this.KeysFlyingMountBar.Name = "KeysFlyingMountBar";
            this.KeysFlyingMountBar.Size = new Size(0x27, 20);
            this.KeysFlyingMountBar.Style = eDotNetBarStyle.StyleManagerControlled;
            this.KeysFlyingMountBar.TabIndex = 0xfe;
            this.superTabControl1.BackColor = Color.White;
            this.superTabControl1.ControlBox.CloseBox.Name = "";
            this.superTabControl1.ControlBox.MenuBox.Name = "";
            this.superTabControl1.ControlBox.Name = "";
            BaseItem[] itemArray = new BaseItem[] { this.superTabControl1.ControlBox.MenuBox, this.superTabControl1.ControlBox.CloseBox };
            this.superTabControl1.ControlBox.SubItems.AddRange(itemArray);
            this.superTabControl1.Controls.Add(this.superTabControlPanel1);
            this.superTabControl1.Controls.Add(this.superTabControlPanel3);
            this.superTabControl1.Controls.Add(this.superTabControlPanel2);
            this.superTabControl1.Dock = DockStyle.Top;
            this.superTabControl1.Location = new Point(0, 0);
            this.superTabControl1.Name = "superTabControl1";
            this.superTabControl1.ReorderTabsEnabled = true;
            this.superTabControl1.SelectedTabFont = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold);
            this.superTabControl1.SelectedTabIndex = 0;
            this.superTabControl1.Size = new Size(0x17b, 0x193);
            this.superTabControl1.TabFont = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.superTabControl1.TabIndex = 140;
            BaseItem[] itemArray2 = new BaseItem[] { this.Generalddd, this.superTabItem1, this.superTabItem2 };
            this.superTabControl1.Tabs.AddRange(itemArray2);
            this.superTabControl1.TabStyle = eSuperTabStyle.WinMediaPlayer12;
            this.superTabControl1.Text = "d";
            this.superTabControlPanel1.Controls.Add(this.groupPanel5);
            this.superTabControlPanel1.Controls.Add(this.groupPanel4);
            this.superTabControlPanel1.Controls.Add(this.groupPanel3);
            this.superTabControlPanel1.Dock = DockStyle.Fill;
            this.superTabControlPanel1.Location = new Point(0, 0x17);
            this.superTabControlPanel1.Name = "superTabControlPanel1";
            this.superTabControlPanel1.Size = new Size(0x17b, 380);
            this.superTabControlPanel1.TabIndex = 1;
            this.superTabControlPanel1.TabItem = this.Generalddd;
            this.superTabControlPanel1.ThemeAware = true;
            this.groupPanel5.CanvasColor = SystemColors.Control;
            this.groupPanel5.ColorSchemeStyle = eDotNetBarStyle.Office2007;
            this.groupPanel5.Controls.Add(this.CBschoolOnly);
            this.groupPanel5.Controls.Add(this.KeysWaterwalkBar);
            this.groupPanel5.Controls.Add(this.labelX4);
            this.groupPanel5.Controls.Add(this.KeysWaterwalkKey);
            this.groupPanel5.Controls.Add(this.KeysLureBar);
            this.groupPanel5.Controls.Add(this.labelX3);
            this.groupPanel5.Controls.Add(this.KeysLureKey);
            this.groupPanel5.Controls.Add(this.CBFish);
            this.groupPanel5.Controls.Add(this.CBUseLure);
            this.groupPanel5.Controls.Add(this.labelX2);
            this.groupPanel5.Controls.Add(this.SetupTBFishApproach);
            this.groupPanel5.Controls.Add(this.labelX1);
            this.groupPanel5.Controls.Add(this.SetupTBMaxTimeAtSchool);
            this.groupPanel5.DisabledBackColor = Color.Empty;
            this.groupPanel5.Location = new Point(7, 0xf8);
            this.groupPanel5.Name = "groupPanel5";
            this.groupPanel5.Size = new Size(0x16b, 0x81);
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
            this.groupPanel5.TabIndex = 0x11b;
            this.groupPanel5.Text = "Fishing";
            this.groupPanel5.Visible = false;
            this.KeysWaterwalkBar.DisplayMember = "Text";
            this.KeysWaterwalkBar.DrawMode = DrawMode.OwnerDrawFixed;
            this.KeysWaterwalkBar.DropDownStyle = ComboBoxStyle.DropDownList;
            this.KeysWaterwalkBar.FormattingEnabled = true;
            this.KeysWaterwalkBar.ItemHeight = 14;
            object[] objArray3 = new object[] { this.comboItem17, this.comboItem18, this.comboItem19, this.comboItem20, this.comboItem21, this.comboItem22 };
            this.KeysWaterwalkBar.Items.AddRange(objArray3);
            this.KeysWaterwalkBar.Location = new Point(0x88, 0x54);
            this.KeysWaterwalkBar.Name = "KeysWaterwalkBar";
            this.KeysWaterwalkBar.Size = new Size(0x27, 20);
            this.KeysWaterwalkBar.Style = eDotNetBarStyle.StyleManagerControlled;
            this.KeysWaterwalkBar.TabIndex = 0x11f;
            this.comboItem17.Text = "1";
            this.comboItem18.Text = "2";
            this.comboItem19.Text = "3";
            this.comboItem20.Text = "4";
            this.comboItem21.Text = "5";
            this.comboItem22.Text = "6";
            this.labelX4.BackColor = Color.Transparent;
            this.labelX4.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX4.Location = new Point(2, 0x52);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new Size(0x80, 0x17);
            this.labelX4.TabIndex = 0x120;
            this.labelX4.Text = "Waterwalk (Bar and Key)";
            this.KeysWaterwalkKey.DisplayMember = "Text";
            this.KeysWaterwalkKey.DrawMode = DrawMode.OwnerDrawFixed;
            this.KeysWaterwalkKey.DropDownStyle = ComboBoxStyle.DropDownList;
            this.KeysWaterwalkKey.FormattingEnabled = true;
            this.KeysWaterwalkKey.ItemHeight = 14;
            object[] objArray4 = new object[] { this.comboItem23, this.comboItem24, this.comboItem25, this.comboItem26, this.comboItem27, this.comboItem28, this.comboItem29, this.comboItem30, this.comboItem31 };
            objArray4[9] = this.comboItem32;
            this.KeysWaterwalkKey.Items.AddRange(objArray4);
            this.KeysWaterwalkKey.Location = new Point(0xb5, 0x54);
            this.KeysWaterwalkKey.Name = "KeysWaterwalkKey";
            this.KeysWaterwalkKey.Size = new Size(0x27, 20);
            this.KeysWaterwalkKey.Style = eDotNetBarStyle.StyleManagerControlled;
            this.KeysWaterwalkKey.TabIndex = 0x121;
            this.comboItem23.Text = "1";
            this.comboItem24.Text = "2";
            this.comboItem25.Text = "3";
            this.comboItem26.Text = "4";
            this.comboItem27.Text = "5";
            this.comboItem28.Text = "6";
            this.comboItem29.Text = "7";
            this.comboItem30.Text = "8";
            this.comboItem31.Text = "9";
            this.comboItem32.Text = "0";
            this.KeysLureBar.DisplayMember = "Text";
            this.KeysLureBar.DrawMode = DrawMode.OwnerDrawFixed;
            this.KeysLureBar.DropDownStyle = ComboBoxStyle.DropDownList;
            this.KeysLureBar.FormattingEnabled = true;
            this.KeysLureBar.ItemHeight = 14;
            object[] objArray5 = new object[] { this.comboItem1, this.comboItem2, this.comboItem3, this.comboItem4, this.comboItem5, this.comboItem6 };
            this.KeysLureBar.Items.AddRange(objArray5);
            this.KeysLureBar.Location = new Point(0x88, 0x3b);
            this.KeysLureBar.Name = "KeysLureBar";
            this.KeysLureBar.Size = new Size(0x27, 20);
            this.KeysLureBar.Style = eDotNetBarStyle.StyleManagerControlled;
            this.KeysLureBar.TabIndex = 0x11c;
            this.comboItem1.Text = "1";
            this.comboItem2.Text = "2";
            this.comboItem3.Text = "3";
            this.comboItem4.Text = "4";
            this.comboItem5.Text = "5";
            this.comboItem6.Text = "6";
            this.labelX3.BackColor = Color.Transparent;
            this.labelX3.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX3.Location = new Point(3, 0x39);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new Size(0x74, 0x17);
            this.labelX3.TabIndex = 0x11d;
            this.labelX3.Text = "Lure (Bar and Key)";
            this.KeysLureKey.DisplayMember = "Text";
            this.KeysLureKey.DrawMode = DrawMode.OwnerDrawFixed;
            this.KeysLureKey.DropDownStyle = ComboBoxStyle.DropDownList;
            this.KeysLureKey.FormattingEnabled = true;
            this.KeysLureKey.ItemHeight = 14;
            object[] objArray6 = new object[] { this.comboItem7, this.comboItem8, this.comboItem9, this.comboItem10, this.comboItem11, this.comboItem12, this.comboItem13, this.comboItem14, this.comboItem15 };
            objArray6[9] = this.comboItem16;
            this.KeysLureKey.Items.AddRange(objArray6);
            this.KeysLureKey.Location = new Point(0xb5, 0x3b);
            this.KeysLureKey.Name = "KeysLureKey";
            this.KeysLureKey.Size = new Size(0x27, 20);
            this.KeysLureKey.Style = eDotNetBarStyle.StyleManagerControlled;
            this.KeysLureKey.TabIndex = 0x11e;
            this.comboItem7.Text = "1";
            this.comboItem8.Text = "2";
            this.comboItem9.Text = "3";
            this.comboItem10.Text = "4";
            this.comboItem11.Text = "5";
            this.comboItem12.Text = "6";
            this.comboItem13.Text = "7";
            this.comboItem14.Text = "8";
            this.comboItem15.Text = "9";
            this.comboItem16.Text = "0";
            this.CBFish.BackColor = Color.Transparent;
            this.CBFish.BackgroundStyle.CornerType = eCornerType.Square;
            this.CBFish.Location = new Point(0xe7, 9);
            this.CBFish.Name = "CBFish";
            this.CBFish.Size = new Size(0x7e, 0x17);
            this.CBFish.Style = eDotNetBarStyle.StyleManagerControlled;
            this.CBFish.TabIndex = 0x11b;
            this.CBFish.Text = "Fish at school of fish";
            this.CBschoolOnly.BackColor = Color.Transparent;
            this.CBschoolOnly.BackgroundStyle.CornerType = eCornerType.Square;
            this.CBschoolOnly.Location = new Point(0xe7, 0x2f);
            this.CBschoolOnly.Name = "CBschoolOnly";
            this.CBschoolOnly.Size = new Size(120, 0x17);
            this.CBschoolOnly.Style = eDotNetBarStyle.StyleManagerControlled;
            this.CBschoolOnly.TabIndex = 290;
            this.CBschoolOnly.Text = "School only";
            this.CBUseLure.BackColor = Color.Transparent;
            this.CBUseLure.BackgroundStyle.CornerType = eCornerType.Square;
            this.CBUseLure.Location = new Point(0xe7, 0x1c);
            this.CBUseLure.Name = "CBUseLure";
            this.CBUseLure.Size = new Size(100, 0x17);
            this.CBUseLure.Style = eDotNetBarStyle.StyleManagerControlled;
            this.CBUseLure.TabIndex = 0x11a;
            this.CBUseLure.Text = "Use lure";
            this.labelX2.BackColor = Color.Transparent;
            this.labelX2.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX2.Location = new Point(3, 0x1f);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new Size(0x67, 0x17);
            this.labelX2.TabIndex = 0x87;
            this.labelX2.Text = "Approach distance";
            this.SetupTBFishApproach.BackColor = Color.Transparent;
            this.SetupTBFishApproach.BackgroundStyle.Class = "DateTimeInputBackground";
            this.SetupTBFishApproach.BackgroundStyle.CornerType = eCornerType.Square;
            this.SetupTBFishApproach.ButtonFreeText.Shortcut = eShortcut.F2;
            this.SetupTBFishApproach.Location = new Point(0x88, 0x22);
            this.SetupTBFishApproach.Name = "SetupTBFishApproach";
            this.SetupTBFishApproach.ShowUpDown = true;
            this.SetupTBFishApproach.Size = new Size(0x27, 20);
            this.SetupTBFishApproach.TabIndex = 0x88;
            this.SetupTBFishApproach.Value = 0x12;
            this.labelX1.BackColor = Color.Transparent;
            this.labelX1.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX1.Location = new Point(3, 6);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new Size(120, 0x17);
            this.labelX1.TabIndex = 0x85;
            this.labelX1.Text = "Max time at school (min)";
            this.SetupTBMaxTimeAtSchool.BackColor = Color.Transparent;
            this.SetupTBMaxTimeAtSchool.BackgroundStyle.Class = "DateTimeInputBackground";
            this.SetupTBMaxTimeAtSchool.BackgroundStyle.CornerType = eCornerType.Square;
            this.SetupTBMaxTimeAtSchool.ButtonFreeText.Shortcut = eShortcut.F2;
            this.SetupTBMaxTimeAtSchool.Location = new Point(0x88, 9);
            this.SetupTBMaxTimeAtSchool.Name = "SetupTBMaxTimeAtSchool";
            this.SetupTBMaxTimeAtSchool.ShowUpDown = true;
            this.SetupTBMaxTimeAtSchool.Size = new Size(0x27, 20);
            this.SetupTBMaxTimeAtSchool.TabIndex = 0x86;
            this.SetupTBMaxTimeAtSchool.Value = 4;
            this.groupPanel4.CanvasColor = SystemColors.Control;
            this.groupPanel4.ColorSchemeStyle = eDotNetBarStyle.Office2007;
            this.groupPanel4.Controls.Add(this.KeysExtraBar);
            this.groupPanel4.Controls.Add(this.KeysExtraKey);
            this.groupPanel4.Controls.Add(this.CBSendKeyOnStartCombat);
            this.groupPanel4.Controls.Add(this.CBDruidAvoidCombat);
            this.groupPanel4.Controls.Add(this.CBAvoidElites);
            this.groupPanel4.Controls.Add(this.labelX40);
            this.groupPanel4.Controls.Add(this.SetupCBStopHarvest);
            this.groupPanel4.Controls.Add(this.SetupCBStopOnDeath);
            this.groupPanel4.Controls.Add(this.CBRessWait);
            this.groupPanel4.Controls.Add(this.SetupTBMaxUnits);
            this.groupPanel4.Controls.Add(this.SetupCBAvoidPlayers);
            this.groupPanel4.Controls.Add(this.SetupCBFindCorpse);
            this.groupPanel4.DisabledBackColor = Color.Empty;
            this.groupPanel4.Location = new Point(7, 0x73);
            this.groupPanel4.Name = "groupPanel4";
            this.groupPanel4.Size = new Size(0x16b, 0x84);
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
            this.groupPanel4.TabIndex = 0x11a;
            this.groupPanel4.Text = "Combat/Anti detection";
            this.KeysExtraBar.DisplayMember = "Text";
            this.KeysExtraBar.DrawMode = DrawMode.OwnerDrawFixed;
            this.KeysExtraBar.DropDownStyle = ComboBoxStyle.DropDownList;
            this.KeysExtraBar.FormattingEnabled = true;
            this.KeysExtraBar.ItemHeight = 14;
            object[] objArray7 = new object[] { this.comboItem33, this.comboItem34, this.comboItem35, this.comboItem36, this.comboItem37, this.comboItem38 };
            this.KeysExtraBar.Items.AddRange(objArray7);
            this.KeysExtraBar.Location = new Point(0x10b, 0x54);
            this.KeysExtraBar.Name = "KeysExtraBar";
            this.KeysExtraBar.Size = new Size(0x27, 20);
            this.KeysExtraBar.Style = eDotNetBarStyle.StyleManagerControlled;
            this.KeysExtraBar.TabIndex = 280;
            this.comboItem33.Text = "1";
            this.comboItem34.Text = "2";
            this.comboItem35.Text = "3";
            this.comboItem36.Text = "4";
            this.comboItem37.Text = "5";
            this.comboItem38.Text = "6";
            this.KeysExtraKey.DisplayMember = "Text";
            this.KeysExtraKey.DrawMode = DrawMode.OwnerDrawFixed;
            this.KeysExtraKey.DropDownStyle = ComboBoxStyle.DropDownList;
            this.KeysExtraKey.FormattingEnabled = true;
            this.KeysExtraKey.ItemHeight = 14;
            object[] objArray8 = new object[] { this.comboItem39, this.comboItem40, this.comboItem41, this.comboItem42, this.comboItem43, this.comboItem44, this.comboItem45, this.comboItem46, this.comboItem47 };
            objArray8[9] = this.comboItem48;
            this.KeysExtraKey.Items.AddRange(objArray8);
            this.KeysExtraKey.Location = new Point(0x138, 0x54);
            this.KeysExtraKey.Name = "KeysExtraKey";
            this.KeysExtraKey.Size = new Size(0x27, 20);
            this.KeysExtraKey.Style = eDotNetBarStyle.StyleManagerControlled;
            this.KeysExtraKey.TabIndex = 0x119;
            this.comboItem39.Text = "1";
            this.comboItem40.Text = "2";
            this.comboItem41.Text = "3";
            this.comboItem42.Text = "4";
            this.comboItem43.Text = "5";
            this.comboItem44.Text = "6";
            this.comboItem45.Text = "7";
            this.comboItem46.Text = "8";
            this.comboItem47.Text = "9";
            this.comboItem48.Text = "0";
            this.CBSendKeyOnStartCombat.AutoSize = true;
            this.CBSendKeyOnStartCombat.BackColor = Color.Transparent;
            this.CBSendKeyOnStartCombat.BackgroundStyle.CornerType = eCornerType.Square;
            this.CBSendKeyOnStartCombat.Location = new Point(0xe4, 0x42);
            this.CBSendKeyOnStartCombat.Name = "CBSendKeyOnStartCombat";
            this.CBSendKeyOnStartCombat.Size = new Size(0x7d, 15);
            this.CBSendKeyOnStartCombat.TabIndex = 0x117;
            this.CBSendKeyOnStartCombat.Text = "Send key on combat:";
            this.CBDruidAvoidCombat.AutoSize = true;
            this.CBDruidAvoidCombat.BackColor = Color.Transparent;
            this.CBDruidAvoidCombat.BackgroundStyle.CornerType = eCornerType.Square;
            this.CBDruidAvoidCombat.Location = new Point(0xe4, 0x2e);
            this.CBDruidAvoidCombat.Name = "CBDruidAvoidCombat";
            this.CBDruidAvoidCombat.Size = new Size(0x75, 15);
            this.CBDruidAvoidCombat.TabIndex = 0x116;
            this.CBDruidAvoidCombat.Text = "Druid avoid combat";
            this.CBRessWait.AutoSize = true;
            this.CBRessWait.BackColor = Color.Transparent;
            this.CBRessWait.BackgroundStyle.CornerType = eCornerType.Square;
            this.CBRessWait.Location = new Point(0xe4, 0x1b);
            this.CBRessWait.Name = "CBRessWait";
            this.CBRessWait.Size = new Size(0x81, 15);
            this.CBRessWait.TabIndex = 0x115;
            this.CBRessWait.Text = "Wait for ress sickness";
            this.SetupCBFindCorpse.AutoSize = true;
            this.SetupCBFindCorpse.BackColor = Color.Transparent;
            this.SetupCBFindCorpse.BackgroundStyle.CornerType = eCornerType.Square;
            this.SetupCBFindCorpse.Location = new Point(0xe4, 6);
            this.SetupCBFindCorpse.Name = "SetupCBFindCorpse";
            this.SetupCBFindCorpse.Size = new Size(0x7e, 15);
            this.SetupCBFindCorpse.TabIndex = 0x113;
            this.SetupCBFindCorpse.Text = "Find corpse on death";
            this.SetupCBFindCorpse.CheckedChanged += new EventHandler(this.SetupCbFindCorpseCheckedChanged);
            this.groupPanel3.CanvasColor = SystemColors.Control;
            this.groupPanel3.ColorSchemeStyle = eDotNetBarStyle.Office2007;
            this.groupPanel3.Controls.Add(this.KeysFlyingMountBar);
            this.groupPanel3.Controls.Add(this.CBWaitForLoot);
            this.groupPanel3.Controls.Add(this.KeysFlyingMountKey);
            this.groupPanel3.Controls.Add(this.CBStopOnFullBags);
            this.groupPanel3.Controls.Add(this.labelX33);
            this.groupPanel3.Controls.Add(this.labelX8);
            this.groupPanel3.Controls.Add(this.SetupCBHerb);
            this.groupPanel3.Controls.Add(this.SetupCBMine);
            this.groupPanel3.Controls.Add(this.SetupTBApproachModifier);
            this.groupPanel3.Controls.Add(this.CBAutoBlacklist);
            this.groupPanel3.DisabledBackColor = Color.Empty;
            this.groupPanel3.Location = new Point(7, 3);
            this.groupPanel3.Name = "groupPanel3";
            this.groupPanel3.Size = new Size(0x16b, 110);
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
            this.groupPanel3.TabIndex = 0x119;
            this.groupPanel3.Text = "General";
            this.CBWaitForLoot.AutoSize = true;
            this.CBWaitForLoot.BackColor = Color.Transparent;
            this.CBWaitForLoot.BackgroundStyle.CornerType = eCornerType.Square;
            this.CBWaitForLoot.Location = new Point(0xe7, 0x2c);
            this.CBWaitForLoot.Name = "CBWaitForLoot";
            this.CBWaitForLoot.Size = new Size(0x51, 15);
            this.CBWaitForLoot.TabIndex = 0x116;
            this.CBWaitForLoot.Text = "Wait for loot";
            this.CBStopOnFullBags.AutoSize = true;
            this.CBStopOnFullBags.BackColor = Color.Transparent;
            this.CBStopOnFullBags.BackgroundStyle.CornerType = eCornerType.Square;
            this.CBStopOnFullBags.Location = new Point(0xe7, 2);
            this.CBStopOnFullBags.Name = "CBStopOnFullBags";
            this.CBStopOnFullBags.Size = new Size(0x68, 15);
            this.CBStopOnFullBags.TabIndex = 0x114;
            this.CBStopOnFullBags.Text = "Stop on full bags";
            this.Generalddd.AttachedControl = this.superTabControlPanel1;
            this.Generalddd.GlobalItem = false;
            this.Generalddd.Name = "Generalddd";
            this.Generalddd.Text = "General";
            this.superTabControlPanel3.Controls.Add(this.groupPanel6);
            this.superTabControlPanel3.Dock = DockStyle.Fill;
            this.superTabControlPanel3.Location = new Point(0, 0);
            this.superTabControlPanel3.Name = "superTabControlPanel3";
            this.superTabControlPanel3.Size = new Size(0x17b, 0x193);
            this.superTabControlPanel3.TabIndex = 0;
            this.superTabControlPanel3.TabItem = this.superTabItem2;
            this.superTabControlPanel3.ThemeAware = true;
            this.groupPanel6.CanvasColor = SystemColors.Control;
            this.groupPanel6.ColorSchemeStyle = eDotNetBarStyle.Office2007;
            this.groupPanel6.Controls.Add(this.ListSchoolItems);
            this.groupPanel6.Controls.Add(this.BtnRemoveSchool);
            this.groupPanel6.Controls.Add(this.TBSchoolName);
            this.groupPanel6.Controls.Add(this.BtnAddSchool);
            this.groupPanel6.DisabledBackColor = Color.Empty;
            this.groupPanel6.Location = new Point(3, 2);
            this.groupPanel6.Name = "groupPanel6";
            this.groupPanel6.Size = new Size(0xf8, 0x176);
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
            this.groupPanel6.TabIndex = 0xb6;
            this.groupPanel6.Text = "School list";
            this.ListSchoolItems.AccessibleRole = AccessibleRole.Outline;
            this.ListSchoolItems.AllowDrop = true;
            this.ListSchoolItems.BackColor = SystemColors.Window;
            this.ListSchoolItems.BackgroundStyle.Class = "TreeBorderKey";
            this.ListSchoolItems.BackgroundStyle.CornerType = eCornerType.Square;
            this.ListSchoolItems.DragDropEnabled = false;
            this.ListSchoolItems.Location = new Point(3, 3);
            this.ListSchoolItems.Name = "ListSchoolItems";
            this.ListSchoolItems.NodesConnector = this.nodeConnector3;
            this.ListSchoolItems.NodeStyle = this.elementStyle3;
            this.ListSchoolItems.PathSeparator = ";";
            this.ListSchoolItems.Size = new Size(0xec, 0x124);
            this.ListSchoolItems.Styles.Add(this.elementStyle3);
            this.ListSchoolItems.TabIndex = 180;
            this.ListSchoolItems.Text = "advTree1";
            this.nodeConnector3.LineColor = SystemColors.ControlText;
            this.elementStyle3.CornerType = eCornerType.Square;
            this.elementStyle3.Name = "elementStyle3";
            this.elementStyle3.TextColor = SystemColors.ControlText;
            this.BtnRemoveSchool.AccessibleRole = AccessibleRole.PushButton;
            this.BtnRemoveSchool.ColorTable = eButtonColor.OrangeWithBackground;
            this.BtnRemoveSchool.Location = new Point(0x37, 0x147);
            this.BtnRemoveSchool.Name = "BtnRemoveSchool";
            this.BtnRemoveSchool.Size = new Size(0x19, 0x17);
            this.BtnRemoveSchool.Style = eDotNetBarStyle.StyleManagerControlled;
            this.BtnRemoveSchool.TabIndex = 0xb3;
            this.BtnRemoveSchool.Text = "-";
            this.BtnRemoveSchool.Click += new EventHandler(this.BtnRemoveSchool_Click);
            this.TBSchoolName.Border.Class = "TextBoxBorder";
            this.TBSchoolName.Border.CornerType = eCornerType.Square;
            this.TBSchoolName.Location = new Point(3, 0x12d);
            this.TBSchoolName.Name = "TBSchoolName";
            this.TBSchoolName.Size = new Size(0x91, 20);
            this.TBSchoolName.TabIndex = 0xb2;
            this.BtnAddSchool.AccessibleRole = AccessibleRole.PushButton;
            this.BtnAddSchool.ColorTable = eButtonColor.OrangeWithBackground;
            this.BtnAddSchool.Location = new Point(3, 0x147);
            this.BtnAddSchool.Name = "BtnAddSchool";
            this.BtnAddSchool.Size = new Size(0x2e, 0x17);
            this.BtnAddSchool.Style = eDotNetBarStyle.StyleManagerControlled;
            this.BtnAddSchool.TabIndex = 0xaf;
            this.BtnAddSchool.Text = "+";
            this.BtnAddSchool.Click += new EventHandler(this.BtnAddSchool_Click);
            this.superTabItem2.AttachedControl = this.superTabControlPanel3;
            this.superTabItem2.GlobalItem = false;
            this.superTabItem2.Name = "superTabItem2";
            this.superTabItem2.Text = "Schools";
            this.superTabControlPanel2.Controls.Add(this.groupPanel2);
            this.superTabControlPanel2.Controls.Add(this.groupPanel1);
            this.superTabControlPanel2.Dock = DockStyle.Fill;
            this.superTabControlPanel2.Location = new Point(0, 0);
            this.superTabControlPanel2.Name = "superTabControlPanel2";
            this.superTabControlPanel2.Size = new Size(0x17b, 0x193);
            this.superTabControlPanel2.TabIndex = 0;
            this.superTabControlPanel2.TabItem = this.superTabItem1;
            this.superTabControlPanel2.ThemeAware = true;
            this.groupPanel2.CanvasColor = SystemColors.Control;
            this.groupPanel2.ColorSchemeStyle = eDotNetBarStyle.Office2007;
            this.groupPanel2.Controls.Add(this.BtnRemoveMine);
            this.groupPanel2.Controls.Add(this.TBMineName);
            this.groupPanel2.Controls.Add(this.ListMineItems);
            this.groupPanel2.Controls.Add(this.BtnAddMine);
            this.groupPanel2.DisabledBackColor = Color.Empty;
            this.groupPanel2.Location = new Point(0xbb, 3);
            this.groupPanel2.Name = "groupPanel2";
            this.groupPanel2.Size = new Size(0xb7, 0x176);
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
            this.groupPanel2.TabIndex = 0xb6;
            this.groupPanel2.Text = "Mine list";
            this.BtnRemoveMine.AccessibleRole = AccessibleRole.PushButton;
            this.BtnRemoveMine.ColorTable = eButtonColor.OrangeWithBackground;
            this.BtnRemoveMine.Location = new Point(0x36, 0x148);
            this.BtnRemoveMine.Name = "BtnRemoveMine";
            this.BtnRemoveMine.Size = new Size(0x19, 0x17);
            this.BtnRemoveMine.Style = eDotNetBarStyle.StyleManagerControlled;
            this.BtnRemoveMine.TabIndex = 0xb9;
            this.BtnRemoveMine.Text = "-";
            this.BtnRemoveMine.Click += new EventHandler(this.BtnRemoveMineClick);
            this.TBMineName.Border.Class = "TextBoxBorder";
            this.TBMineName.Border.CornerType = eCornerType.Square;
            this.TBMineName.Location = new Point(3, 300);
            this.TBMineName.Name = "TBMineName";
            this.TBMineName.Size = new Size(0x9b, 20);
            this.TBMineName.TabIndex = 0xb8;
            this.ListMineItems.AccessibleRole = AccessibleRole.Outline;
            this.ListMineItems.AllowDrop = true;
            this.ListMineItems.BackColor = SystemColors.Window;
            this.ListMineItems.BackgroundStyle.Class = "TreeBorderKey";
            this.ListMineItems.BackgroundStyle.CornerType = eCornerType.Square;
            this.ListMineItems.DragDropEnabled = false;
            this.ListMineItems.Location = new Point(3, 1);
            this.ListMineItems.Name = "ListMineItems";
            this.ListMineItems.NodesConnector = this.nodeConnector2;
            this.ListMineItems.NodeStyle = this.elementStyle2;
            this.ListMineItems.PathSeparator = ";";
            this.ListMineItems.Size = new Size(0xab, 0x126);
            this.ListMineItems.TabIndex = 0xbb;
            this.ListMineItems.Text = "advTree1";
            this.nodeConnector2.LineColor = SystemColors.ControlText;
            this.elementStyle2.CornerType = eCornerType.Square;
            this.elementStyle2.Name = "elementStyle2";
            this.elementStyle2.TextColor = SystemColors.ControlText;
            this.BtnAddMine.AccessibleRole = AccessibleRole.PushButton;
            this.BtnAddMine.ColorTable = eButtonColor.OrangeWithBackground;
            this.BtnAddMine.Location = new Point(3, 0x148);
            this.BtnAddMine.Name = "BtnAddMine";
            this.BtnAddMine.Size = new Size(0x2e, 0x17);
            this.BtnAddMine.Style = eDotNetBarStyle.StyleManagerControlled;
            this.BtnAddMine.TabIndex = 0xb5;
            this.BtnAddMine.Text = "+";
            this.BtnAddMine.Click += new EventHandler(this.BtnAddMineClick);
            this.groupPanel1.CanvasColor = SystemColors.Control;
            this.groupPanel1.ColorSchemeStyle = eDotNetBarStyle.Office2007;
            this.groupPanel1.Controls.Add(this.ListHerbItems);
            this.groupPanel1.Controls.Add(this.BtnRemoveHerb);
            this.groupPanel1.Controls.Add(this.TBHerbName);
            this.groupPanel1.Controls.Add(this.BtnAddHerb);
            this.groupPanel1.DisabledBackColor = Color.Empty;
            this.groupPanel1.Location = new Point(6, 3);
            this.groupPanel1.Name = "groupPanel1";
            this.groupPanel1.Size = new Size(0xaf, 0x176);
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
            this.groupPanel1.TabIndex = 0xb5;
            this.groupPanel1.Text = "Herb list";
            this.ListHerbItems.AccessibleRole = AccessibleRole.Outline;
            this.ListHerbItems.AllowDrop = true;
            this.ListHerbItems.BackColor = SystemColors.Window;
            this.ListHerbItems.BackgroundStyle.Class = "TreeBorderKey";
            this.ListHerbItems.BackgroundStyle.CornerType = eCornerType.Square;
            this.ListHerbItems.DragDropEnabled = false;
            this.ListHerbItems.Location = new Point(3, 3);
            this.ListHerbItems.Name = "ListHerbItems";
            this.ListHerbItems.NodesConnector = this.nodeConnector1;
            this.ListHerbItems.NodeStyle = this.elementStyle1;
            this.ListHerbItems.PathSeparator = ";";
            this.ListHerbItems.Size = new Size(0xa4, 0x124);
            this.ListHerbItems.Styles.Add(this.elementStyle1);
            this.ListHerbItems.TabIndex = 180;
            this.ListHerbItems.Text = "advTree1";
            this.nodeConnector1.LineColor = SystemColors.ControlText;
            this.elementStyle1.CornerType = eCornerType.Square;
            this.elementStyle1.Name = "elementStyle1";
            this.elementStyle1.TextColor = SystemColors.ControlText;
            this.BtnRemoveHerb.AccessibleRole = AccessibleRole.PushButton;
            this.BtnRemoveHerb.ColorTable = eButtonColor.OrangeWithBackground;
            this.BtnRemoveHerb.Location = new Point(0x37, 0x147);
            this.BtnRemoveHerb.Name = "BtnRemoveHerb";
            this.BtnRemoveHerb.Size = new Size(0x19, 0x17);
            this.BtnRemoveHerb.Style = eDotNetBarStyle.StyleManagerControlled;
            this.BtnRemoveHerb.TabIndex = 0xb3;
            this.BtnRemoveHerb.Text = "-";
            this.BtnRemoveHerb.Click += new EventHandler(this.BtnRemoveHerbClick);
            this.TBHerbName.Border.Class = "TextBoxBorder";
            this.TBHerbName.Border.CornerType = eCornerType.Square;
            this.TBHerbName.Location = new Point(3, 0x12d);
            this.TBHerbName.Name = "TBHerbName";
            this.TBHerbName.Size = new Size(0x91, 20);
            this.TBHerbName.TabIndex = 0xb2;
            this.BtnAddHerb.AccessibleRole = AccessibleRole.PushButton;
            this.BtnAddHerb.ColorTable = eButtonColor.OrangeWithBackground;
            this.BtnAddHerb.Location = new Point(3, 0x147);
            this.BtnAddHerb.Name = "BtnAddHerb";
            this.BtnAddHerb.Size = new Size(0x2e, 0x17);
            this.BtnAddHerb.Style = eDotNetBarStyle.StyleManagerControlled;
            this.BtnAddHerb.TabIndex = 0xaf;
            this.BtnAddHerb.Text = "+";
            this.BtnAddHerb.Click += new EventHandler(this.BtnAddHerbClick);
            this.superTabItem1.AttachedControl = this.superTabControlPanel2;
            this.superTabItem1.GlobalItem = false;
            this.superTabItem1.Name = "superTabItem1";
            this.superTabItem1.Text = "Collect";
            this.labelX5.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX5.Location = new Point(0, 0);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new Size(0x67, 0x17);
            this.labelX5.TabIndex = 0;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.FromArgb(0xdf, 0xea, 0xf6);
            base.ClientSize = new Size(0x17b, 0x1e8);
            base.Controls.Add(this.superTabControl1);
            base.Controls.Add(this.SaveSettings);
            this.DoubleBuffered = true;
            this.EnableGlass = false;
            base.Name = "Settings";
            base.Load += new EventHandler(this.SettingsLoad);
            this.SetupTBMaxUnits.EndInit();
            this.SetupTBApproachModifier.EndInit();
            ((ISupportInitialize) this.superTabControl1).EndInit();
            this.superTabControl1.ResumeLayout(false);
            this.superTabControlPanel1.ResumeLayout(false);
            this.groupPanel5.ResumeLayout(false);
            this.SetupTBFishApproach.EndInit();
            this.SetupTBMaxTimeAtSchool.EndInit();
            this.groupPanel4.ResumeLayout(false);
            this.groupPanel4.PerformLayout();
            this.groupPanel3.ResumeLayout(false);
            this.groupPanel3.PerformLayout();
            this.superTabControlPanel3.ResumeLayout(false);
            this.groupPanel6.ResumeLayout(false);
            this.ListSchoolItems.EndInit();
            this.superTabControlPanel2.ResumeLayout(false);
            this.groupPanel2.ResumeLayout(false);
            this.ListMineItems.EndInit();
            this.groupPanel1.ResumeLayout(false);
            this.ListHerbItems.EndInit();
            base.ResumeLayout(false);
        }

        private void LoadHerbList()
        {
            Herb.Load();
            foreach (string str in Herb.GetList())
            {
                this.AddHerb(str);
            }
        }

        private void LoadMineList()
        {
            Mine.Load();
            foreach (string str in Mine.GetList())
            {
                this.AddMine(str);
            }
        }

        private void LoadSchoolList()
        {
        }

        private void SaveHerbList()
        {
            Herb.Clear();
            foreach (Node node in this.ListHerbItems.Nodes)
            {
                Herb.AddHerb(node.Tag.ToString());
            }
            Herb.Save();
        }

        private void SaveMineList()
        {
            Mine.Clear();
            foreach (Node node in this.ListMineItems.Nodes)
            {
                Mine.AddMine(node.Tag.ToString());
            }
            Mine.Save();
        }

        private void SaveSchoolList()
        {
        }

        private void SaveSettingsClick(object sender, EventArgs e)
        {
            GatherSettings.Herb = this.SetupCBHerb.Checked;
            GatherSettings.Mine = this.SetupCBMine.Checked;
            GatherSettings.ApproachModifier = (float) Convert.ToDouble(this.SetupTBApproachModifier.Text);
            GatherSettings.MaxUnits = this.SetupTBMaxUnits.Text;
            GatherSettings.StopOnDeath = this.SetupCBStopOnDeath.Checked;
            GatherSettings.StopHarvestWithPlayerAround = this.SetupCBStopHarvest.Checked;
            GatherSettings.AvoidPlayers = this.SetupCBAvoidPlayers.Checked;
            GatherSettings.FlyingMountBar = this.KeysFlyingMountBar.SelectedItem.ToString();
            GatherSettings.FlyingMountKey = this.KeysFlyingMountKey.SelectedItem.ToString();
            GatherSettings.AutoBlacklist = this.CBAutoBlacklist.Checked;
            GatherSettings.AvoidElites = this.CBAvoidElites.Checked;
            GatherSettings.FindCorpse = this.SetupCBFindCorpse.Checked;
            GatherSettings.StopOnFullBags = this.CBStopOnFullBags.Checked;
            GatherSettings.WaitForRessSick = this.CBRessWait.Checked;
            GatherSettings.WaitForLoot = this.CBWaitForLoot.Checked;
            GatherSettings.DruidAvoidCombat = this.CBDruidAvoidCombat.Checked;
            GatherSettings.ExtraBar = this.KeysExtraBar.SelectedItem.ToString();
            GatherSettings.ExtraKey = this.KeysExtraKey.SelectedItem.ToString();
            GatherSettings.SendKeyOnStartCombat = this.CBSendKeyOnStartCombat.Checked;
            GatherSettings.Fish = this.CBFish.Checked;
            GatherSettings.Lure = this.CBUseLure.Checked;
            GatherSettings.MaxTimeAtSchool = this.SetupTBMaxTimeAtSchool.Value;
            GatherSettings.FishApproach = this.SetupTBFishApproach.Value;
            GatherSettings.LureBar = this.KeysLureBar.SelectedItem.ToString();
            GatherSettings.LureKey = this.KeysLureKey.SelectedItem.ToString();
            GatherSettings.WaterwalkBar = this.KeysWaterwalkBar.SelectedItem.ToString();
            GatherSettings.WaterwalkKey = this.KeysWaterwalkKey.SelectedItem.ToString();
            GatherSettings.SaveSettings();
            this.SaveHerbList();
            this.SaveMineList();
            this.SaveSchoolList();
            base.Close();
        }

        private void SettingsLoad(object sender, EventArgs e)
        {
            this.groupPanel5.Visible = true;
            this.SetupCBHerb.Checked = GatherSettings.Herb;
            this.SetupCBMine.Checked = GatherSettings.Mine;
            this.SetupTBApproachModifier.Text = GatherSettings.ApproachModifier.ToString();
            this.SetupTBMaxUnits.Text = GatherSettings.MaxUnits;
            this.SetupCBStopOnDeath.Checked = GatherSettings.StopOnDeath;
            this.SetupCBStopHarvest.Checked = GatherSettings.StopHarvestWithPlayerAround;
            this.SetupCBAvoidPlayers.Checked = GatherSettings.AvoidPlayers;
            this.CBAutoBlacklist.Checked = GatherSettings.AutoBlacklist;
            this.CBAvoidElites.Checked = GatherSettings.AvoidElites;
            this.SetupCBFindCorpse.Checked = GatherSettings.FindCorpse;
            this.CBStopOnFullBags.Checked = GatherSettings.StopOnFullBags;
            this.KeysFlyingMountBar.SelectedIndex = this.KeysFlyingMountBar.FindStringExact(GatherSettings.FlyingMountBar);
            this.KeysFlyingMountKey.SelectedIndex = this.KeysFlyingMountKey.FindStringExact(GatherSettings.FlyingMountKey);
            this.CBRessWait.Checked = GatherSettings.WaitForRessSick;
            this.CBWaitForLoot.Checked = GatherSettings.WaitForLoot;
            this.CBDruidAvoidCombat.Checked = GatherSettings.DruidAvoidCombat;
            this.KeysExtraBar.SelectedIndex = this.KeysExtraBar.FindStringExact(GatherSettings.ExtraBar);
            this.KeysExtraKey.SelectedIndex = this.KeysExtraKey.FindStringExact(GatherSettings.ExtraKey);
            this.CBSendKeyOnStartCombat.Checked = GatherSettings.SendKeyOnStartCombat;
            this.CBFish.Checked = GatherSettings.Fish;
            this.CBUseLure.Checked = GatherSettings.Lure;
            this.SetupTBMaxTimeAtSchool.Value = Convert.ToInt32(GatherSettings.MaxTimeAtSchool);
            this.SetupTBFishApproach.Value = Convert.ToInt32(GatherSettings.FishApproach);
            this.KeysLureBar.SelectedIndex = this.KeysLureBar.FindStringExact(GatherSettings.LureBar);
            this.KeysLureKey.SelectedIndex = this.KeysLureKey.FindStringExact(GatherSettings.LureKey);
            this.KeysWaterwalkBar.SelectedIndex = this.KeysWaterwalkBar.FindStringExact(GatherSettings.WaterwalkBar);
            this.KeysWaterwalkKey.SelectedIndex = this.KeysWaterwalkKey.FindStringExact(GatherSettings.WaterwalkKey);
            this.LoadHerbList();
            this.LoadMineList();
            this.LoadSchoolList();
        }

        private void SetupCbFindCorpseCheckedChanged(object sender, EventArgs e)
        {
            if (this.SetupCBFindCorpse.Checked)
            {
                this.SetupCBStopOnDeath.Checked = false;
            }
        }

        private void SetupCbStopOnDeathCheckedChanged(object sender, EventArgs e)
        {
            if (this.SetupCBStopOnDeath.Checked)
            {
                this.SetupCBFindCorpse.Checked = false;
            }
        }
    }
}

