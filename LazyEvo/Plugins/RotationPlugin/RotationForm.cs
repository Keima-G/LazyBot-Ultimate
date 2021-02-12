namespace LazyEvo.Plugins.RotationPlugin
{
    using DevComponents.AdvTree;
    using DevComponents.DotNetBar;
    using DevComponents.DotNetBar.Controls;
    using DevComponents.Editors;
    using LazyEvo.Forms.Helpers;
    using LazyEvo.PVEBehavior;
    using LazyEvo.PVEBehavior.Behavior;
    using LazyLib;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    internal class RotationForm : Office2007Form
    {
        public LazyEvo.Plugins.RotationPlugin.Rotation Rotation;
        public bool Save;
        private Node _selected;
        private DevComponents.AdvTree.AdvTree _selectedTree;
        private IContainer components;
        private StyleManager styleManager1;
        private GroupPanel BeGMisc;
        private IntegerInput BeGlobalCooldown;
        private LabelX labelX25;
        private SuperTabControl BeTabs;
        private SuperTabControlPanel superTabControlPanel2;
        private DevComponents.AdvTree.AdvTree BeComRules;
        private NodeConnector nodeConnector1;
        private ElementStyle elementStyle1;
        private SuperTabItem TabCombat;
        private LabelItem labelItem3;
        private Bar BeBarRuleModifier;
        private ButtonItem BeComAddRule;
        private LabelItem labelItem1;
        private ButtonItem BeComEditRule;
        private LabelItem labelItem2;
        private ButtonItem BeComDeleteRule;
        private ButtonItem BtnAddScript;
        private LabelItem labelItem4;
        private CheckBoxX CBActive;
        private LabelX labelX1;
        private ButtonX BtnSave;
        private ButtonX BCancel;
        private GroupPanel groupPanel6;
        private TextBoxX TBRotationName;
        private LabelX labelX7;
        private SuperTooltip superTooltip1;
        private CheckBoxX CBWin;
        private CheckBoxX CBShift;
        private CheckBoxX CBCtrl;
        private CheckBoxX CBAlt;
        private ComboBox HotKey;

        public RotationForm(LazyEvo.Plugins.RotationPlugin.Rotation rotation)
        {
            this.InitializeComponent();
            this.Rotation = rotation;
            Geometry.GeometryFromString(GeomertrySettings.RotationForm, this);
        }

        private void AddCondition(Rule rule, DevComponents.AdvTree.AdvTree advTree)
        {
            Node node = new Node {
                Text = rule.Name,
                Tag = rule
            };
            this.AddNode(node, advTree);
        }

        private void AddNode(Node node, DevComponents.AdvTree.AdvTree advTree)
        {
            advTree.BeginUpdate();
            advTree.Nodes.Add(node);
            advTree.EndUpdate();
        }

        private void BCancel_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void BeComAddRuleClick(object sender, EventArgs e)
        {
            RuleEditor editor = new RuleEditor(new Rule(), false) {
                Location = base.Location
            };
            editor.ShowDialog();
            if (editor.Save)
            {
                Rule rule = editor.Rule;
                if (this.BeTabs.SelectedTab.Name.Equals("TabCombat"))
                {
                    this.AddCondition(rule, this.BeComRules);
                }
            }
        }

        private void BeComDeleteRuleClick(object sender, EventArgs e)
        {
            if ((this._selected != null) && (this._selectedTree != null))
            {
                this._selectedTree.Nodes.Remove(this._selected);
                this._selectedTree = null;
                this._selected = null;
            }
        }

        private void BeComRules_NodeDragFeedback(object sender, TreeDragFeedbackEventArgs e)
        {
            if (e.ParentNode != null)
            {
                e.AllowDrop = false;
            }
        }

        private void BeComRulesNodeClick(object sender, TreeNodeMouseEventArgs e)
        {
            this._selected = e.Node;
            this._selectedTree = this.BeComRules;
        }

        private void BeComRulesNodeDoubleClick(object sender, TreeNodeMouseEventArgs e)
        {
            this.EditRule(e.Node);
        }

        private void BtnAddScript_Click(object sender, EventArgs e)
        {
            Rule rule = new Rule();
            ScriptEditor editor = new ScriptEditor(rule) {
                Location = base.Location
            };
            editor.ShowDialog();
            if (editor.Save && this.BeTabs.SelectedTab.Name.Equals("TabCombat"))
            {
                this.AddCondition(rule, this.BeComRules);
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (this.TBRotationName.Text == "")
            {
                this.superTooltip1.SetSuperTooltip(this.TBRotationName, new SuperTooltipInfo("", "", "Please give the rotation a name.", null, null, eTooltipColor.Gray));
                this.superTooltip1.ShowTooltip(this.TBRotationName);
            }
            else
            {
                this.Rotation.Active = this.CBActive.Checked;
                this.Rotation.Name = this.TBRotationName.Text;
                this.Rotation.Shift = this.CBShift.Checked;
                this.Rotation.Alt = this.CBAlt.Checked;
                this.Rotation.Windows = this.CBWin.Checked;
                this.Rotation.Ctrl = this.CBCtrl.Checked;
                this.Rotation.Key = this.HotKey.SelectedItem.ToString();
                this.Rotation.ResetControllers();
                this.Rotation.GlobalCooldown = this.BeGlobalCooldown.Value;
                foreach (Node node in this.BeComRules.Nodes)
                {
                    Rule tag = (Rule) node.Tag;
                    tag.Priority = node.Index;
                    this.Rotation.Rules.AddRule(tag);
                }
                this.Save = true;
                base.Close();
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

        private void EditRule(Node node)
        {
            if (node.Tag is Rule)
            {
                Rule tag = (Rule) node.Tag;
                if (!string.IsNullOrEmpty(tag.Script))
                {
                    ScriptEditor editor2 = new ScriptEditor(tag) {
                        Location = base.Location
                    };
                    editor2.ShowDialog();
                    if (editor2.Save)
                    {
                        node.Tag = tag;
                        node.Text = tag.Name;
                    }
                }
                else
                {
                    RuleEditor editor = new RuleEditor(tag, false) {
                        Location = base.Location
                    };
                    editor.ShowDialog();
                    if (editor.Save)
                    {
                        node.Tag = tag;
                        node.Text = tag.Name;
                    }
                }
            }
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            this.styleManager1 = new StyleManager(this.components);
            this.BeGMisc = new GroupPanel();
            this.CBWin = new CheckBoxX();
            this.CBShift = new CheckBoxX();
            this.CBCtrl = new CheckBoxX();
            this.CBAlt = new CheckBoxX();
            this.HotKey = new ComboBox();
            this.labelX1 = new LabelX();
            this.CBActive = new CheckBoxX();
            this.BeGlobalCooldown = new IntegerInput();
            this.labelX25 = new LabelX();
            this.BeTabs = new SuperTabControl();
            this.superTabControlPanel2 = new SuperTabControlPanel();
            this.BeComRules = new DevComponents.AdvTree.AdvTree();
            this.nodeConnector1 = new NodeConnector();
            this.elementStyle1 = new ElementStyle();
            this.TabCombat = new SuperTabItem();
            this.labelItem3 = new LabelItem();
            this.BeBarRuleModifier = new Bar();
            this.BeComAddRule = new ButtonItem();
            this.labelItem1 = new LabelItem();
            this.BtnAddScript = new ButtonItem();
            this.labelItem4 = new LabelItem();
            this.BeComEditRule = new ButtonItem();
            this.labelItem2 = new LabelItem();
            this.BeComDeleteRule = new ButtonItem();
            this.BtnSave = new ButtonX();
            this.BCancel = new ButtonX();
            this.groupPanel6 = new GroupPanel();
            this.TBRotationName = new TextBoxX();
            this.labelX7 = new LabelX();
            this.superTooltip1 = new SuperTooltip();
            this.BeGMisc.SuspendLayout();
            this.BeGlobalCooldown.BeginInit();
            ((ISupportInitialize) this.BeTabs).BeginInit();
            this.BeTabs.SuspendLayout();
            this.superTabControlPanel2.SuspendLayout();
            this.BeComRules.BeginInit();
            this.BeBarRuleModifier.BeginInit();
            this.groupPanel6.SuspendLayout();
            base.SuspendLayout();
            this.styleManager1.ManagerStyle = eStyle.Windows7Blue;
            this.BeGMisc.BackColor = Color.Transparent;
            this.BeGMisc.BackgroundImageLayout = ImageLayout.None;
            this.BeGMisc.CanvasColor = SystemColors.Control;
            this.BeGMisc.ColorSchemeStyle = eDotNetBarStyle.Office2007;
            this.BeGMisc.Controls.Add(this.CBWin);
            this.BeGMisc.Controls.Add(this.CBShift);
            this.BeGMisc.Controls.Add(this.CBCtrl);
            this.BeGMisc.Controls.Add(this.CBAlt);
            this.BeGMisc.Controls.Add(this.HotKey);
            this.BeGMisc.Controls.Add(this.labelX1);
            this.BeGMisc.Controls.Add(this.CBActive);
            this.BeGMisc.Controls.Add(this.BeGlobalCooldown);
            this.BeGMisc.Controls.Add(this.labelX25);
            this.BeGMisc.Location = new Point(0, 0x15a);
            this.BeGMisc.Name = "BeGMisc";
            this.BeGMisc.Size = new Size(0x211, 0x3e);
            this.BeGMisc.Style.BackColor2SchemePart = eColorSchemePart.PanelBackground2;
            this.BeGMisc.Style.BackColorGradientAngle = 90;
            this.BeGMisc.Style.BackColorSchemePart = eColorSchemePart.PanelBackground;
            this.BeGMisc.Style.BorderBottom = eStyleBorderType.Solid;
            this.BeGMisc.Style.BorderBottomWidth = 1;
            this.BeGMisc.Style.BorderColorSchemePart = eColorSchemePart.PanelBorder;
            this.BeGMisc.Style.BorderLeft = eStyleBorderType.Solid;
            this.BeGMisc.Style.BorderLeftWidth = 1;
            this.BeGMisc.Style.BorderRight = eStyleBorderType.Solid;
            this.BeGMisc.Style.BorderRightWidth = 1;
            this.BeGMisc.Style.BorderTop = eStyleBorderType.Solid;
            this.BeGMisc.Style.BorderTopWidth = 1;
            this.BeGMisc.Style.Class = "";
            this.BeGMisc.Style.CornerDiameter = 4;
            this.BeGMisc.Style.CornerType = eCornerType.Rounded;
            this.BeGMisc.Style.TextAlignment = eStyleTextAlignment.Center;
            this.BeGMisc.Style.TextColorSchemePart = eColorSchemePart.PanelText;
            this.BeGMisc.Style.TextLineAlignment = eStyleTextAlignment.Near;
            this.BeGMisc.StyleMouseDown.Class = "";
            this.BeGMisc.StyleMouseDown.CornerType = eCornerType.Square;
            this.BeGMisc.StyleMouseOver.Class = "";
            this.BeGMisc.StyleMouseOver.CornerType = eCornerType.Square;
            this.BeGMisc.TabIndex = 13;
            this.CBWin.BackgroundStyle.Class = "";
            this.CBWin.BackgroundStyle.CornerType = eCornerType.Square;
            this.CBWin.Location = new Point(0x112, 0x21);
            this.CBWin.Name = "CBWin";
            this.CBWin.Size = new Size(0x3e, 0x17);
            this.CBWin.Style = eDotNetBarStyle.StyleManagerControlled;
            this.CBWin.TabIndex = 0x18;
            this.CBWin.Text = "Win";
            this.CBShift.BackgroundStyle.Class = "";
            this.CBShift.BackgroundStyle.CornerType = eCornerType.Square;
            this.CBShift.Location = new Point(0xbf, 0x21);
            this.CBShift.Name = "CBShift";
            this.CBShift.Size = new Size(0x3e, 0x17);
            this.CBShift.Style = eDotNetBarStyle.StyleManagerControlled;
            this.CBShift.TabIndex = 0x17;
            this.CBShift.Text = "Shift";
            this.CBCtrl.BackgroundStyle.Class = "";
            this.CBCtrl.BackgroundStyle.CornerType = eCornerType.Square;
            this.CBCtrl.Location = new Point(0x72, 0x21);
            this.CBCtrl.Name = "CBCtrl";
            this.CBCtrl.Size = new Size(0x3e, 0x17);
            this.CBCtrl.Style = eDotNetBarStyle.StyleManagerControlled;
            this.CBCtrl.TabIndex = 0x16;
            this.CBCtrl.Text = "Ctrl";
            this.CBAlt.BackgroundStyle.Class = "";
            this.CBAlt.BackgroundStyle.CornerType = eCornerType.Square;
            this.CBAlt.Location = new Point(0x31, 0x20);
            this.CBAlt.Name = "CBAlt";
            this.CBAlt.Size = new Size(0x3e, 0x17);
            this.CBAlt.Style = eDotNetBarStyle.StyleManagerControlled;
            this.CBAlt.TabIndex = 0x15;
            this.CBAlt.Text = "Alt";
            this.HotKey.FormattingEnabled = true;
            this.HotKey.Location = new Point(0x169, 0x23);
            this.HotKey.Name = "HotKey";
            this.HotKey.Size = new Size(150, 0x15);
            this.HotKey.TabIndex = 20;
            this.labelX1.BackgroundStyle.Class = "";
            this.labelX1.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX1.Location = new Point(6, 0x20);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new Size(0x2c, 0x17);
            this.labelX1.TabIndex = 15;
            this.labelX1.Text = "Hotkey:";
            this.CBActive.BackgroundStyle.Class = "";
            this.CBActive.BackgroundStyle.CornerType = eCornerType.Square;
            this.CBActive.Checked = true;
            this.CBActive.CheckState = CheckState.Checked;
            this.CBActive.CheckValue = "Y";
            this.CBActive.Location = new Point(3, 3);
            this.CBActive.Name = "CBActive";
            this.CBActive.Size = new Size(0x3e, 0x17);
            this.CBActive.Style = eDotNetBarStyle.StyleManagerControlled;
            this.CBActive.TabIndex = 14;
            this.CBActive.Text = "Active";
            this.BeGlobalCooldown.BackgroundStyle.Class = "DateTimeInputBackground";
            this.BeGlobalCooldown.BackgroundStyle.CornerType = eCornerType.Square;
            this.BeGlobalCooldown.ButtonFreeText.Shortcut = eShortcut.F2;
            this.BeGlobalCooldown.Location = new Point(0x1bf, 6);
            this.BeGlobalCooldown.Name = "BeGlobalCooldown";
            this.BeGlobalCooldown.ShowUpDown = true;
            this.BeGlobalCooldown.Size = new Size(0x40, 20);
            this.BeGlobalCooldown.TabIndex = 9;
            this.BeGlobalCooldown.Value = 0x7d0;
            this.labelX25.BackgroundStyle.Class = "";
            this.labelX25.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX25.Location = new Point(0x169, 3);
            this.labelX25.Name = "labelX25";
            this.labelX25.Size = new Size(0x5b, 0x1b);
            this.labelX25.TabIndex = 8;
            this.labelX25.Text = "Global cooldown:";
            this.BeTabs.BackColor = Color.Silver;
            this.BeTabs.ControlBox.CloseBox.Name = "";
            this.BeTabs.ControlBox.MenuBox.Name = "";
            this.BeTabs.ControlBox.Name = "";
            BaseItem[] items = new BaseItem[] { this.BeTabs.ControlBox.MenuBox, this.BeTabs.ControlBox.CloseBox };
            this.BeTabs.ControlBox.SubItems.AddRange(items);
            this.BeTabs.Controls.Add(this.superTabControlPanel2);
            this.BeTabs.Location = new Point(0, 2);
            this.BeTabs.Name = "BeTabs";
            this.BeTabs.ReorderTabsEnabled = true;
            this.BeTabs.SelectedTabFont = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold);
            this.BeTabs.SelectedTabIndex = 0;
            this.BeTabs.Size = new Size(0x214, 0x133);
            this.BeTabs.TabFont = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.BeTabs.TabIndex = 10;
            BaseItem[] itemArray2 = new BaseItem[] { this.TabCombat, this.labelItem3 };
            this.BeTabs.Tabs.AddRange(itemArray2);
            this.superTabControlPanel2.Controls.Add(this.BeComRules);
            this.superTabControlPanel2.Dock = DockStyle.Fill;
            this.superTabControlPanel2.Location = new Point(0, 0x19);
            this.superTabControlPanel2.Name = "superTabControlPanel2";
            this.superTabControlPanel2.Size = new Size(0x214, 0x11a);
            this.superTabControlPanel2.TabIndex = 0;
            this.superTabControlPanel2.TabItem = this.TabCombat;
            this.BeComRules.AccessibleRole = AccessibleRole.Outline;
            this.BeComRules.AllowDrop = true;
            this.BeComRules.BackColor = SystemColors.Window;
            this.BeComRules.BackgroundStyle.Class = "TreeBorderKey";
            this.BeComRules.BackgroundStyle.CornerType = eCornerType.Square;
            this.BeComRules.Dock = DockStyle.Top;
            this.BeComRules.Location = new Point(0, 0);
            this.BeComRules.Name = "BeComRules";
            this.BeComRules.NodesConnector = this.nodeConnector1;
            this.BeComRules.NodeStyle = this.elementStyle1;
            this.BeComRules.PathSeparator = ";";
            this.BeComRules.Size = new Size(0x214, 0x116);
            this.BeComRules.Styles.Add(this.elementStyle1);
            this.BeComRules.TabIndex = 0;
            this.BeComRules.Text = "advTree1";
            this.BeComRules.NodeDragFeedback += new TreeDragFeedbackEventHander(this.BeComRules_NodeDragFeedback);
            this.BeComRules.NodeClick += new TreeNodeMouseEventHandler(this.BeComRulesNodeClick);
            this.BeComRules.NodeDoubleClick += new TreeNodeMouseEventHandler(this.BeComRulesNodeDoubleClick);
            this.nodeConnector1.LineColor = SystemColors.ControlText;
            this.elementStyle1.Class = "";
            this.elementStyle1.CornerType = eCornerType.Square;
            this.elementStyle1.Name = "elementStyle1";
            this.elementStyle1.TextColor = SystemColors.ControlText;
            this.TabCombat.AttachedControl = this.superTabControlPanel2;
            this.TabCombat.GlobalItem = false;
            this.TabCombat.Name = "TabCombat";
            this.TabCombat.Text = "Rules";
            this.labelItem3.Name = "labelItem3";
            this.labelItem3.Text = "                                                                                                                              ";
            this.BeBarRuleModifier.AntiAlias = true;
            BaseItem[] itemArray3 = new BaseItem[] { this.BeComAddRule, this.labelItem1, this.BtnAddScript, this.labelItem4, this.BeComEditRule, this.labelItem2, this.BeComDeleteRule };
            this.BeBarRuleModifier.Items.AddRange(itemArray3);
            this.BeBarRuleModifier.Location = new Point(0, 0x13b);
            this.BeBarRuleModifier.Name = "BeBarRuleModifier";
            this.BeBarRuleModifier.Size = new Size(0x214, 0x19);
            this.BeBarRuleModifier.Stretch = true;
            this.BeBarRuleModifier.Style = eDotNetBarStyle.StyleManagerControlled;
            this.BeBarRuleModifier.TabIndex = 12;
            this.BeBarRuleModifier.TabStop = false;
            this.BeBarRuleModifier.Text = "bar1";
            this.BeComAddRule.Name = "BeComAddRule";
            this.BeComAddRule.Text = "Add Rule";
            this.BeComAddRule.Click += new EventHandler(this.BeComAddRuleClick);
            this.labelItem1.Name = "labelItem1";
            this.labelItem1.Text = "              ";
            this.BtnAddScript.Name = "BtnAddScript";
            this.BtnAddScript.Text = "Add script";
            this.BtnAddScript.Click += new EventHandler(this.BtnAddScript_Click);
            this.labelItem4.Name = "labelItem4";
            this.labelItem4.Text = "              ";
            this.BeComEditRule.Name = "BeComEditRule";
            this.BeComEditRule.Text = "Double click on rule to edit";
            this.labelItem2.Name = "labelItem2";
            this.labelItem2.Text = "              ";
            this.BeComDeleteRule.Name = "BeComDeleteRule";
            this.BeComDeleteRule.Text = "Delete Rule";
            this.BeComDeleteRule.Click += new EventHandler(this.BeComDeleteRuleClick);
            this.BtnSave.AccessibleRole = AccessibleRole.PushButton;
            this.BtnSave.ColorTable = eButtonColor.OrangeWithBackground;
            this.BtnSave.Location = new Point(0x1bf, 0);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new Size(0x4b, 0x15);
            this.BtnSave.Style = eDotNetBarStyle.StyleManagerControlled;
            this.BtnSave.TabIndex = 0x3d;
            this.BtnSave.Text = "Ok";
            this.BtnSave.Click += new EventHandler(this.BtnSave_Click);
            this.BCancel.AccessibleRole = AccessibleRole.PushButton;
            this.BCancel.ColorTable = eButtonColor.OrangeWithBackground;
            this.BCancel.Location = new Point(360, 0x1a9);
            this.BCancel.Name = "BCancel";
            this.BCancel.Size = new Size(0x4b, 0x16);
            this.BCancel.Style = eDotNetBarStyle.StyleManagerControlled;
            this.BCancel.TabIndex = 0x3e;
            this.BCancel.Text = "Cancel";
            this.BCancel.Click += new EventHandler(this.BCancel_Click);
            this.groupPanel6.CanvasColor = SystemColors.Control;
            this.groupPanel6.ColorSchemeStyle = eDotNetBarStyle.Office2007;
            this.groupPanel6.Controls.Add(this.BtnSave);
            this.groupPanel6.Controls.Add(this.TBRotationName);
            this.groupPanel6.Controls.Add(this.labelX7);
            this.groupPanel6.Location = new Point(0, 0x1a7);
            this.groupPanel6.Name = "groupPanel6";
            this.groupPanel6.Size = new Size(0x214, 0x1b);
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
            this.groupPanel6.TabIndex = 60;
            this.TBRotationName.Border.Class = "TextBoxBorder";
            this.TBRotationName.Border.CornerType = eCornerType.Square;
            this.TBRotationName.Location = new Point(0x67, 0);
            this.TBRotationName.Name = "TBRotationName";
            this.TBRotationName.Size = new Size(0xf6, 20);
            this.TBRotationName.TabIndex = 0x3a;
            this.labelX7.BackColor = Color.Transparent;
            this.labelX7.BackgroundStyle.Class = "";
            this.labelX7.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX7.Location = new Point(3, 0);
            this.labelX7.Name = "labelX7";
            this.labelX7.Size = new Size(0x5e, 0x17);
            this.labelX7.TabIndex = 0x39;
            this.labelX7.Text = "<b>Name of rotation:</b>";
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.FromArgb(0xdf, 0xe9, 0xf5);
            base.ClientSize = new Size(540, 0x1d1);
            base.Controls.Add(this.BCancel);
            base.Controls.Add(this.groupPanel6);
            base.Controls.Add(this.BeGMisc);
            base.Controls.Add(this.BeTabs);
            base.Controls.Add(this.BeBarRuleModifier);
            this.DoubleBuffered = true;
            base.Name = "RotationForm";
            base.FormClosing += new FormClosingEventHandler(this.RotationForm_FormClosing);
            base.Load += new EventHandler(this.RotationFormLoad);
            this.BeGMisc.ResumeLayout(false);
            this.BeGlobalCooldown.EndInit();
            ((ISupportInitialize) this.BeTabs).EndInit();
            this.BeTabs.ResumeLayout(false);
            this.superTabControlPanel2.ResumeLayout(false);
            this.BeComRules.EndInit();
            this.BeBarRuleModifier.EndInit();
            this.groupPanel6.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        private void RotationForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            GeomertrySettings.RotationForm = Geometry.GeometryToString(this);
        }

        private void RotationFormLoad(object sender, EventArgs e)
        {
            this.TBRotationName.Text = this.Rotation.Name;
            this.CBShift.Checked = this.Rotation.Shift;
            this.CBAlt.Checked = this.Rotation.Alt;
            this.CBWin.Checked = this.Rotation.Windows;
            this.CBCtrl.Checked = this.Rotation.Ctrl;
            this.CBActive.Checked = this.Rotation.Active;
            this.BeGlobalCooldown.Value = this.Rotation.GlobalCooldown;
            this.Rotation.Rules.GetRules.Sort();
            foreach (Rule rule in this.Rotation.Rules.GetRules)
            {
                this.AddCondition(rule, this.BeComRules);
            }
            foreach (KeysData data in RotationSettings.KeysList)
            {
                this.HotKey.Items.Add(data.Text);
            }
            if (this.HotKey.Items.Contains(this.Rotation.Key))
            {
                this.HotKey.SelectedIndex = this.HotKey.FindStringExact(this.Rotation.Key);
            }
            else
            {
                this.HotKey.SelectedIndex = 1;
            }
        }
    }
}

