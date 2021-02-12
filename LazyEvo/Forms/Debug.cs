namespace LazyEvo.Forms
{
    using DevComponents.DotNetBar;
    using DevComponents.DotNetBar.Controls;
    using DevComponents.DotNetBar.Metro.ColorTables;
    using LazyEvo.Debug;
    using LazyLib;
    using LazyLib.Helpers;
    using LazyLib.Wow;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Threading;
    using System.Windows.Forms;

    internal class Debug : Office2007Form
    {
        private Frame _item;
        private Frame _WotlkTrain;
        private ListViewItem[] _wowPlayerListViewItemArray;
        private ListViewItem[] _wowTargetListViewItemArray;
        private IContainer components;
        private ListView listView1;
        private System.Windows.Forms.Timer timer1;
        private StyleManager styleManager1;
        private SuperTabControl superTabControl1;
        private SuperTabControlPanel superTabControlPanel1;
        private SuperTabItem superTabItem1;
        private SuperTabControlPanel superTabControlPanel2;
        private ButtonX DebugBtnClick;
        private ButtonX DebugBtnFindUI;
        private TextBoxX DebugTBUIName;
        private LabelX labelX7;
        private ButtonX UIBtnDump;
        private SuperTabItem superTabItem2;
        private RichTextBox DebugTBLog;
        private ListView listView2;
        private SuperTabControlPanel superTabControlPanel3;
        public SuperTabItem superTabItem3;
        private ButtonX _btnDoWotlkTrain;
        private GroupPanel groupPanel1;
        private LabelX labelX1;
        private TextBoxX dbg_lookupResult;
        private ButtonX dbg_addLookUpbtn;
        private TextBoxX dbgAddLookup;

        public Debug()
        {
            this.InitializeComponent();
            this.LoadListView();
        }

        private void _btnDoWotlkTrain_Click(object sender, EventArgs e)
        {
            this.DoWotlkTrain();
        }

        private void dbg_addLookUpbtn_Click(object sender, EventArgs e)
        {
            this.dbg_lookupResult.Text = Convert.ToString(Memory.Read<uint>(new uint[] { Convert.ToUInt32(this.dbgAddLookup.Text) }));
        }

        private void Debug_Load(object sender, EventArgs e)
        {
            this.superTabControl1.SelectedTab = this.superTabItem1;
        }

        private void DebugBtnClickClick(object sender, EventArgs e)
        {
            if (this._item != null)
            {
                this._item.LeftClick();
            }
        }

        private void DebugBtnFindUiClick(object sender, EventArgs e)
        {
            this.WriteLine("Going to try and find: " + this.DebugTBUIName.Text);
            this.DebugTBLog.Clear();
            if (this.DebugTBUIName.Text != "")
            {
                try
                {
                    this._item = InterfaceHelper.GetFrameByName(this.DebugTBUIName.Text);
                    if (this._item == null)
                    {
                        this.WriteLine("Could not find the item");
                    }
                    else
                    {
                        this.WriteLine("Found the item, dumping info");
                        this.WriteLine("Address: " + this._item.BaseAddress);
                        this.WriteLine("Visible: " + this._item.IsVisible);
                        this.WriteLine("Text: " + this._item.GetText);
                        this.WriteLine("IsEnabled: " + this._item.IsEnabled);
                        this.WriteLine("ButtonEnabled: " + this._item.Enabled);
                        foreach (Frame frame in this._item.GetChilds)
                        {
                            this.WriteLine("Name: " + frame.GetName);
                            this.WriteLine("Address: " + frame.BaseAddress);
                            this.WriteLine("Visible: " + frame.IsVisible);
                            this.WriteLine("Text: " + frame.GetText);
                            this.WriteLine("IsEnabled: " + frame.IsEnabled);
                            this.WriteLine("ButtonEnabled: " + frame.Enabled);
                        }
                    }
                }
                catch (Exception exception)
                {
                    this.WriteLine("Error when trying to log interface item: " + exception);
                }
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

        public void DoWotlkTrain()
        {
            int num = 10;
            int num2 = 1;
            if (!InterfaceHelper.GetFrameByName("ClassTrainerFrame").IsVisible)
            {
                Logging.Write(LogType.Warning, "TrainerFrame not open", new object[0]);
            }
            else
            {
                try
                {
                    while (InterfaceHelper.GetFrameByName("ClassTrainerSkill" + num2).IsVisible && (num2 <= num))
                    {
                        try
                        {
                            this._WotlkTrain = InterfaceHelper.GetFrameByName("ClassTrainerSkill" + num2);
                            if ((this._WotlkTrain != null) && ((InterfaceHelper.GetFrameByName("ClassTrainerSkill" + num2).GetChildObject("ClassTrainerSkill" + num2 + "Text").GetText != null) && ((InterfaceHelper.GetFrameByName("ClassTrainerSkill" + num2).GetChildObject("ClassTrainerSkill" + num2 + "Text").GetText != "Mail") && ((InterfaceHelper.GetFrameByName("ClassTrainerSkill" + num2).GetChildObject("ClassTrainerSkill" + num2 + "Text").GetText != "Plate") && ((InterfaceHelper.GetFrameByName("ClassTrainerSkill" + num2).GetChildObject("ClassTrainerSkill" + num2 + "Text").GetText != "Arcane") && ((InterfaceHelper.GetFrameByName("ClassTrainerSkill" + num2).GetChildObject("ClassTrainerSkill" + num2 + "Text").GetText != "Frost") && ((InterfaceHelper.GetFrameByName("ClassTrainerSkill" + num2).GetChildObject("ClassTrainerSkill" + num2 + "Text").GetText != "Fire") && ((InterfaceHelper.GetFrameByName("ClassTrainerSkill" + num2).GetChildObject("ClassTrainerSkill" + num2 + "Text").GetText != "Discipline") && ((InterfaceHelper.GetFrameByName("ClassTrainerSkill" + num2).GetChildObject("ClassTrainerSkill" + num2 + "Text").GetText != "Holy") && ((InterfaceHelper.GetFrameByName("ClassTrainerSkill" + num2).GetChildObject("ClassTrainerSkill" + num2 + "Text").GetText != "Shadow Magic") && ((InterfaceHelper.GetFrameByName("ClassTrainerSkill" + num2).GetChildObject("ClassTrainerSkill" + num2 + "Text").GetText != "Mounts") && ((InterfaceHelper.GetFrameByName("ClassTrainerSkill" + num2).GetChildObject("ClassTrainerSkill" + num2 + "Text").GetText != "Elemental") && ((InterfaceHelper.GetFrameByName("ClassTrainerSkill" + num2).GetChildObject("ClassTrainerSkill" + num2 + "Text").GetText != "Enhancment") && ((InterfaceHelper.GetFrameByName("ClassTrainerSkill" + num2).GetChildObject("ClassTrainerSkill" + num2 + "Text").GetText != "Restoration") && ((InterfaceHelper.GetFrameByName("ClassTrainerSkill" + num2).GetChildObject("ClassTrainerSkill" + num2 + "Text").GetText != "Holy") && ((InterfaceHelper.GetFrameByName("ClassTrainerSkill" + num2).GetChildObject("ClassTrainerSkill" + num2 + "Text").GetText != "Retribution") && ((InterfaceHelper.GetFrameByName("ClassTrainerSkill" + num2).GetChildObject("ClassTrainerSkill" + num2 + "Text").GetText != "Protection") && ((InterfaceHelper.GetFrameByName("ClassTrainerSkill" + num2).GetChildObject("ClassTrainerSkill" + num2 + "Text").GetText != "Defense") && ((InterfaceHelper.GetFrameByName("ClassTrainerSkill" + num2).GetChildObject("ClassTrainerSkill" + num2 + "Text").GetText != "Arms") && ((InterfaceHelper.GetFrameByName("ClassTrainerSkill" + num2).GetChildObject("ClassTrainerSkill" + num2 + "Text").GetText != "Fury") && ((InterfaceHelper.GetFrameByName("ClassTrainerSkill" + num2).GetChildObject("ClassTrainerSkill" + num2 + "Text").GetText != "Protection") && ((InterfaceHelper.GetFrameByName("ClassTrainerSkill" + num2).GetChildObject("ClassTrainerSkill" + num2 + "Text").GetText != "Blood") && ((InterfaceHelper.GetFrameByName("ClassTrainerSkill" + num2).GetChildObject("ClassTrainerSkill" + num2 + "Text").GetText != "Frost") && ((InterfaceHelper.GetFrameByName("ClassTrainerSkill" + num2).GetChildObject("ClassTrainerSkill" + num2 + "Text").GetText != "Unholy") && ((InterfaceHelper.GetFrameByName("ClassTrainerSkill" + num2).GetChildObject("ClassTrainerSkill" + num2 + "Text").GetText != "Runeforging") && ((InterfaceHelper.GetFrameByName("ClassTrainerSkill" + num2).GetChildObject("ClassTrainerSkill" + num2 + "Text").GetText != "Feral") && ((InterfaceHelper.GetFrameByName("ClassTrainerSkill" + num2).GetChildObject("ClassTrainerSkill" + num2 + "Text").GetText != "Balance") && (InterfaceHelper.GetFrameByName("ClassTrainerSkill" + num2).GetChildObject("ClassTrainerSkill" + num2 + "Text").GetText != "Restoration"))))))))))))))))))))))))))))
                            {
                                this._WotlkTrain.LeftClick();
                                Thread.Sleep(50);
                                if (InterfaceHelper.GetFrameByName("ClassTrainerTrainButton").GetChildObject("ClassTrainerTrainButtonText").Enabled)
                                {
                                    object[] args = new object[] { InterfaceHelper.GetFrameByName("ClassTrainerSkill" + num2).GetChildObject("ClassTrainerSkill" + num2 + "Text").GetText };
                                    Logging.Write(LogType.Good, "Learning: {0}", args);
                                    Thread.Sleep(100);
                                    InterfaceHelper.GetFrameByName("ClassTrainerTrainButton").LeftClick();
                                    num2--;
                                }
                            }
                            num2++;
                        }
                        catch (Exception exception1)
                        {
                            Logging.Debug("{0}", new object[] { exception1 });
                        }
                    }
                }
                catch
                {
                }
            }
        }

        private static NameValuePair[] GetPlayerMeNameValuePairs(PPlayerSelf me) => 
            new PPlayerSelfUtils(me.BaseAddress).GetNameValuePairs().ToArray();

        private static NameValuePair[] GetPlayerNameValuePairs(PPlayer me) => 
            new PPlayerUtils(me.BaseAddress).GetNameValuePairs().ToArray();

        private static NameValuePair[] GetTargetNameValuePairs(PUnit target) => 
            new PUnitUtils(target.BaseAddress).GetNameValuePairs().ToArray();

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.listView1 = new ListView();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.styleManager1 = new StyleManager(this.components);
            this.superTabControl1 = new SuperTabControl();
            this.superTabControlPanel3 = new SuperTabControlPanel();
            this.groupPanel1 = new GroupPanel();
            this.labelX1 = new LabelX();
            this.dbg_lookupResult = new TextBoxX();
            this.dbg_addLookUpbtn = new ButtonX();
            this.dbgAddLookup = new TextBoxX();
            this._btnDoWotlkTrain = new ButtonX();
            this.superTabItem3 = new SuperTabItem();
            this.superTabControlPanel2 = new SuperTabControlPanel();
            this.DebugTBLog = new RichTextBox();
            this.DebugBtnClick = new ButtonX();
            this.DebugBtnFindUI = new ButtonX();
            this.DebugTBUIName = new TextBoxX();
            this.labelX7 = new LabelX();
            this.UIBtnDump = new ButtonX();
            this.superTabItem2 = new SuperTabItem();
            this.superTabControlPanel1 = new SuperTabControlPanel();
            this.listView2 = new ListView();
            this.superTabItem1 = new SuperTabItem();
            ((ISupportInitialize) this.superTabControl1).BeginInit();
            this.superTabControl1.SuspendLayout();
            this.superTabControlPanel3.SuspendLayout();
            this.groupPanel1.SuspendLayout();
            this.superTabControlPanel2.SuspendLayout();
            this.superTabControlPanel1.SuspendLayout();
            base.SuspendLayout();
            this.listView1.Location = new Point(3, 3);
            this.listView1.Name = "listView1";
            this.listView1.Size = new Size(0x133, 0x1f9);
            this.listView1.TabIndex = 2;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.timer1.Enabled = true;
            this.timer1.Interval = 0x3e8;
            this.timer1.Tick += new EventHandler(this.timer1_Tick);
            this.styleManager1.ManagerStyle = eStyle.Windows7Blue;
            this.styleManager1.MetroColorParameters = new MetroColorGeneratorParameters(Color.White, Color.FromArgb(0x2b, 0x57, 0x9a));
            this.superTabControl1.ControlBox.CloseBox.Name = "";
            this.superTabControl1.ControlBox.MenuBox.Name = "";
            this.superTabControl1.ControlBox.Name = "";
            BaseItem[] items = new BaseItem[] { this.superTabControl1.ControlBox.MenuBox, this.superTabControl1.ControlBox.CloseBox };
            this.superTabControl1.ControlBox.SubItems.AddRange(items);
            this.superTabControl1.Controls.Add(this.superTabControlPanel3);
            this.superTabControl1.Controls.Add(this.superTabControlPanel1);
            this.superTabControl1.Controls.Add(this.superTabControlPanel2);
            this.superTabControl1.Dock = DockStyle.Fill;
            this.superTabControl1.Location = new Point(0, 0);
            this.superTabControl1.Name = "superTabControl1";
            this.superTabControl1.ReorderTabsEnabled = true;
            this.superTabControl1.SelectedTabFont = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold);
            this.superTabControl1.SelectedTabIndex = 1;
            this.superTabControl1.Size = new Size(0x279, 0x23e);
            this.superTabControl1.TabFont = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.superTabControl1.TabIndex = 4;
            BaseItem[] itemArray2 = new BaseItem[] { this.superTabItem1, this.superTabItem2, this.superTabItem3 };
            this.superTabControl1.Tabs.AddRange(itemArray2);
            this.superTabControl1.Text = "UI";
            this.superTabControlPanel3.Controls.Add(this.groupPanel1);
            this.superTabControlPanel3.Controls.Add(this._btnDoWotlkTrain);
            this.superTabControlPanel3.Dock = DockStyle.Fill;
            this.superTabControlPanel3.Location = new Point(0, 0x19);
            this.superTabControlPanel3.Name = "superTabControlPanel3";
            this.superTabControlPanel3.Size = new Size(0x279, 0x225);
            this.superTabControlPanel3.TabIndex = 0;
            this.superTabControlPanel3.TabItem = this.superTabItem3;
            this.groupPanel1.CanvasColor = SystemColors.Control;
            this.groupPanel1.ColorSchemeStyle = eDotNetBarStyle.Office2007;
            this.groupPanel1.Controls.Add(this.labelX1);
            this.groupPanel1.Controls.Add(this.dbg_lookupResult);
            this.groupPanel1.Controls.Add(this.dbg_addLookUpbtn);
            this.groupPanel1.Controls.Add(this.dbgAddLookup);
            this.groupPanel1.DisabledBackColor = Color.Empty;
            this.groupPanel1.Location = new Point(430, 3);
            this.groupPanel1.Name = "groupPanel1";
            this.groupPanel1.Size = new Size(200, 0x85);
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
            this.groupPanel1.TabIndex = 1;
            this.groupPanel1.Text = "Address Lookup";
            this.labelX1.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX1.Location = new Point(0x4d, 0x3a);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new Size(0x29, 0x17);
            this.labelX1.TabIndex = 3;
            this.labelX1.Text = "Result:";
            this.dbg_lookupResult.Border.Class = "TextBoxBorder";
            this.dbg_lookupResult.Border.CornerType = eCornerType.Square;
            this.dbg_lookupResult.Location = new Point(13, 0x57);
            this.dbg_lookupResult.Name = "dbg_lookupResult";
            this.dbg_lookupResult.PreventEnterBeep = true;
            this.dbg_lookupResult.Size = new Size(0xa6, 20);
            this.dbg_lookupResult.TabIndex = 2;
            this.dbg_addLookUpbtn.AccessibleRole = AccessibleRole.PushButton;
            this.dbg_addLookUpbtn.ColorTable = eButtonColor.OrangeWithBackground;
            this.dbg_addLookUpbtn.Location = new Point(0x3a, 0x1d);
            this.dbg_addLookUpbtn.Name = "dbg_addLookUpbtn";
            this.dbg_addLookUpbtn.Size = new Size(0x4b, 0x17);
            this.dbg_addLookUpbtn.Style = eDotNetBarStyle.StyleManagerControlled;
            this.dbg_addLookUpbtn.TabIndex = 1;
            this.dbg_addLookUpbtn.Text = "Peek!";
            this.dbg_addLookUpbtn.Tooltip = "Peeks whats inside the address above";
            this.dbg_addLookUpbtn.Click += new EventHandler(this.dbg_addLookUpbtn_Click);
            this.dbgAddLookup.Border.Class = "TextBoxBorder";
            this.dbgAddLookup.Border.CornerType = eCornerType.Square;
            this.dbgAddLookup.Location = new Point(13, 3);
            this.dbgAddLookup.MaxLength = 0x20;
            this.dbgAddLookup.Name = "dbgAddLookup";
            this.dbgAddLookup.PreventEnterBeep = true;
            this.dbgAddLookup.Size = new Size(0xa6, 20);
            this.dbgAddLookup.TabIndex = 0;
            this._btnDoWotlkTrain.AccessibleRole = AccessibleRole.PushButton;
            this._btnDoWotlkTrain.BackColor = Color.FromArgb(0xc0, 0xff, 0xc0);
            this._btnDoWotlkTrain.ColorTable = eButtonColor.OrangeWithBackground;
            this._btnDoWotlkTrain.Location = new Point(12, 3);
            this._btnDoWotlkTrain.Name = "_btnDoWotlkTrain";
            this._btnDoWotlkTrain.Size = new Size(0x59, 0x17);
            this._btnDoWotlkTrain.Style = eDotNetBarStyle.StyleManagerControlled;
            this._btnDoWotlkTrain.TabIndex = 0;
            this._btnDoWotlkTrain.Text = "DoWotlkTrain()";
            this._btnDoWotlkTrain.TextAlignment = eButtonTextAlignment.Left;
            this._btnDoWotlkTrain.Tooltip = "Trainer Test ( training menu needs to be open )";
            this._btnDoWotlkTrain.Click += new EventHandler(this._btnDoWotlkTrain_Click);
            this.superTabItem3.AttachedControl = this.superTabControlPanel3;
            this.superTabItem3.GlobalItem = false;
            this.superTabItem3.Name = "superTabItem3";
            this.superTabItem3.Text = "Misc Debug Stuff";
            this.superTabControlPanel2.Controls.Add(this.DebugTBLog);
            this.superTabControlPanel2.Controls.Add(this.DebugBtnClick);
            this.superTabControlPanel2.Controls.Add(this.DebugBtnFindUI);
            this.superTabControlPanel2.Controls.Add(this.DebugTBUIName);
            this.superTabControlPanel2.Controls.Add(this.labelX7);
            this.superTabControlPanel2.Controls.Add(this.UIBtnDump);
            this.superTabControlPanel2.Dock = DockStyle.Fill;
            this.superTabControlPanel2.Location = new Point(0, 0x19);
            this.superTabControlPanel2.Name = "superTabControlPanel2";
            this.superTabControlPanel2.Size = new Size(0x279, 0x225);
            this.superTabControlPanel2.TabIndex = 0;
            this.superTabControlPanel2.TabItem = this.superTabItem2;
            this.DebugTBLog.Location = new Point(12, 90);
            this.DebugTBLog.Name = "DebugTBLog";
            this.DebugTBLog.Size = new Size(0xf9, 0x70);
            this.DebugTBLog.TabIndex = 11;
            this.DebugTBLog.Text = "";
            this.DebugBtnClick.AccessibleRole = AccessibleRole.PushButton;
            this.DebugBtnClick.ColorTable = eButtonColor.OrangeWithBackground;
            this.DebugBtnClick.Location = new Point(0x5d, 0x3d);
            this.DebugBtnClick.Name = "DebugBtnClick";
            this.DebugBtnClick.Size = new Size(0x4b, 0x17);
            this.DebugBtnClick.Style = eDotNetBarStyle.StyleManagerControlled;
            this.DebugBtnClick.TabIndex = 10;
            this.DebugBtnClick.Text = "Click";
            this.DebugBtnClick.Click += new EventHandler(this.DebugBtnClickClick);
            this.DebugBtnFindUI.AccessibleRole = AccessibleRole.PushButton;
            this.DebugBtnFindUI.ColorTable = eButtonColor.OrangeWithBackground;
            this.DebugBtnFindUI.Location = new Point(12, 0x3d);
            this.DebugBtnFindUI.Name = "DebugBtnFindUI";
            this.DebugBtnFindUI.Size = new Size(0x4b, 0x17);
            this.DebugBtnFindUI.Style = eDotNetBarStyle.StyleManagerControlled;
            this.DebugBtnFindUI.TabIndex = 9;
            this.DebugBtnFindUI.Text = "Search";
            this.DebugBtnFindUI.Click += new EventHandler(this.DebugBtnFindUiClick);
            this.DebugTBUIName.Border.Class = "TextBoxBorder";
            this.DebugTBUIName.Border.CornerType = eCornerType.Square;
            this.DebugTBUIName.Location = new Point(12, 0x23);
            this.DebugTBUIName.Name = "DebugTBUIName";
            this.DebugTBUIName.Size = new Size(0x80, 20);
            this.DebugTBUIName.TabIndex = 8;
            this.labelX7.BackColor = Color.Transparent;
            this.labelX7.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX7.Location = new Point(12, 13);
            this.labelX7.Name = "labelX7";
            this.labelX7.Size = new Size(0x92, 0x17);
            this.labelX7.TabIndex = 7;
            this.labelX7.Text = "Find UI object by name:";
            this.UIBtnDump.AccessibleRole = AccessibleRole.PushButton;
            this.UIBtnDump.ColorTable = eButtonColor.OrangeWithBackground;
            this.UIBtnDump.Location = new Point(15, 0xd0);
            this.UIBtnDump.Name = "UIBtnDump";
            this.UIBtnDump.Size = new Size(0xc4, 0x17);
            this.UIBtnDump.Style = eDotNetBarStyle.StyleManagerControlled;
            this.UIBtnDump.TabIndex = 6;
            this.UIBtnDump.Text = "Dump all UI object to the log file";
            this.UIBtnDump.Click += new EventHandler(this.UiBtnDumpClick);
            this.superTabItem2.AttachedControl = this.superTabControlPanel2;
            this.superTabItem2.GlobalItem = false;
            this.superTabItem2.Name = "superTabItem2";
            this.superTabItem2.Text = "Ui";
            this.superTabControlPanel1.ColorSchemeStyle = eDotNetBarStyle.StyleManagerControlled;
            this.superTabControlPanel1.Controls.Add(this.listView2);
            this.superTabControlPanel1.Controls.Add(this.listView1);
            this.superTabControlPanel1.Dock = DockStyle.Fill;
            this.superTabControlPanel1.Location = new Point(0, 0x19);
            this.superTabControlPanel1.Name = "superTabControlPanel1";
            this.superTabControlPanel1.Size = new Size(0x279, 0x225);
            this.superTabControlPanel1.TabIndex = 1;
            this.superTabControlPanel1.TabItem = this.superTabItem1;
            this.superTabControlPanel1.Visible = false;
            this.listView2.Location = new Point(0x13c, 3);
            this.listView2.Name = "listView2";
            this.listView2.Size = new Size(0x133, 0x1f9);
            this.listView2.TabIndex = 3;
            this.listView2.UseCompatibleStateImageBehavior = false;
            this.superTabItem1.AttachedControl = this.superTabControlPanel1;
            this.superTabItem1.GlobalItem = false;
            this.superTabItem1.Name = "superTabItem1";
            this.superTabItem1.Text = "Unit info";
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.FromArgb(0xdf, 0xea, 0xf6);
            base.ClientSize = new Size(0x279, 0x23e);
            base.Controls.Add(this.superTabControl1);
            this.DoubleBuffered = true;
            base.Name = "Debug";
            base.Load += new EventHandler(this.Debug_Load);
            ((ISupportInitialize) this.superTabControl1).EndInit();
            this.superTabControl1.ResumeLayout(false);
            this.superTabControlPanel3.ResumeLayout(false);
            this.groupPanel1.ResumeLayout(false);
            this.superTabControlPanel2.ResumeLayout(false);
            this.superTabControlPanel1.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        private void InitializePlayerListViewItem()
        {
            NameValuePair[] playerMeNameValuePairs = GetPlayerMeNameValuePairs(LazyLib.Wow.ObjectManager.MyPlayer);
            this._wowPlayerListViewItemArray = new ListViewItem[playerMeNameValuePairs.Length];
            for (int i = 0; i < playerMeNameValuePairs.Length; i++)
            {
                this._wowPlayerListViewItemArray[i] = new ListViewItem(playerMeNameValuePairs[i].Name) { SubItems = { playerMeNameValuePairs[i].Value } };
            }
            this.listView1.Items.AddRange(this._wowPlayerListViewItemArray);
        }

        private void InitializeTargetListViewItem()
        {
            PUnit target = LazyLib.Wow.ObjectManager.MyPlayer.Target;
            NameValuePair[] targetNameValuePairs = new NameValuePair[0];
            if ((target != null) && (target.BaseAddress != 0))
            {
                if (target.Type == 3)
                {
                    targetNameValuePairs = GetTargetNameValuePairs(target);
                }
                else if (target.Type == 4)
                {
                    targetNameValuePairs = GetPlayerNameValuePairs((PPlayer) target);
                }
            }
            this._wowTargetListViewItemArray = new ListViewItem[targetNameValuePairs.Length];
            for (int i = 0; i < targetNameValuePairs.Length; i++)
            {
                this._wowTargetListViewItemArray[i] = new ListViewItem(targetNameValuePairs[i].Name) { SubItems = { targetNameValuePairs[i].Value } };
            }
            this.listView2.Items.AddRange(this._wowTargetListViewItemArray);
        }

        private void LoadListView()
        {
            this.listView1.View = View.Details;
            this.listView2.View = View.Details;
            this.listView1.Columns.Add("Name", 100, HorizontalAlignment.Center);
            this.listView1.Columns.Add("Value", 200, HorizontalAlignment.Left);
            this.listView2.Columns.Add("Name", 100, HorizontalAlignment.Center);
            this.listView2.Columns.Add("Value", 200, HorizontalAlignment.Left);
            this.InitializePlayerListViewItem();
            this.InitializeTargetListViewItem();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (LazyLib.Wow.ObjectManager.InGame)
            {
                this.UpdatePlayerTabValues();
                this.UpdateTargetTabValues();
            }
        }

        private void UiBtnDumpClick(object sender, EventArgs e)
        {
            MessageBox.Show("This will make LazyBot freeze until done");
            foreach (Frame frame in InterfaceHelper.GetFrames)
            {
                Logging.Debug(string.Concat(new object[] { "Name: ", frame.GetName, " Visible: ", frame.IsVisible }), new object[0]);
                foreach (Frame frame2 in frame.GetChilds)
                {
                    Logging.Debug(string.Concat(new object[] { "     Child: Name: ", frame2.GetName, " Visible: ", frame2.IsVisible }), new object[0]);
                }
            }
        }

        public void UpdatePlayerTabValues()
        {
            NameValuePair[] playerMeNameValuePairs = GetPlayerMeNameValuePairs(LazyLib.Wow.ObjectManager.MyPlayer);
            for (int i = 0; i < this._wowPlayerListViewItemArray.Length; i++)
            {
                this._wowPlayerListViewItemArray[i].SubItems.RemoveAt(1);
                this._wowPlayerListViewItemArray[i].SubItems.Add(playerMeNameValuePairs[i].Value);
            }
        }

        public void UpdateTargetTabValues()
        {
            PUnit target = LazyLib.Wow.ObjectManager.MyPlayer.Target;
            if (target == null)
            {
                this._wowTargetListViewItemArray = new ListViewItem[0];
                this.listView2.Items.Clear();
            }
            else if ((this._wowTargetListViewItemArray != null) && (this._wowTargetListViewItemArray.Length < 2))
            {
                this.InitializeTargetListViewItem();
            }
            else
            {
                NameValuePair[] targetNameValuePairs = new NameValuePair[0];
                if (target.BaseAddress != 0)
                {
                    if (target.Type == 3)
                    {
                        targetNameValuePairs = GetTargetNameValuePairs(target);
                    }
                    else if (target.Type == 4)
                    {
                        targetNameValuePairs = GetPlayerNameValuePairs((PPlayer) target);
                    }
                    if (this._wowTargetListViewItemArray != null)
                    {
                        for (int i = 0; i < this._wowTargetListViewItemArray.Length; i++)
                        {
                            try
                            {
                                this._wowTargetListViewItemArray[i].SubItems.RemoveAt(1);
                                this._wowTargetListViewItemArray[i].SubItems.Add(targetNameValuePairs[i].Value);
                            }
                            catch
                            {
                            }
                        }
                    }
                }
            }
        }

        private void WriteLine(string text)
        {
            this.DebugTBLog.AppendText(text + Environment.NewLine);
        }
    }
}

