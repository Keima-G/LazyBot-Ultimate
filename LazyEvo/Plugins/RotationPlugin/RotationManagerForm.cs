namespace LazyEvo.Plugins.RotationPlugin
{
    using DevComponents.AdvTree;
    using DevComponents.DotNetBar;
    using DevComponents.DotNetBar.Controls;
    using LazyEvo.PVEBehavior;
    using LazyEvo.PVEBehavior.Behavior;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Windows.Forms;

    internal class RotationManagerForm : Office2007Form
    {
        internal static string OurDirectory;
        private Node _selected;
        private DevComponents.AdvTree.AdvTree _selectedTree;
        private RotationManagerController rotationManagerController;
        private IContainer components;
        private StyleManager styleManager1;
        private GroupPanel groupPanel4;
        internal ComboBoxEx BeTBSelectBehavior;
        private LabelX labelX20;
        private TextBoxX BeTBNewBehavior;
        private LabelX labelX18;
        private SuperTabControl BeTabs;
        private SuperTabControlPanel superTabControlPanel2;
        private DevComponents.AdvTree.AdvTree BeRotations;
        private NodeConnector nodeConnector1;
        private ElementStyle elementStyle1;
        private SuperTabItem TabRotations;
        private LabelItem labelItem3;
        private ButtonItem BeSaveBeheavior;
        private Bar BeBarRuleModifier;
        private ButtonItem BeComAddRule;
        private LabelItem labelItem1;
        private ButtonItem BeComEditRule;
        private LabelItem labelItem2;
        private ButtonItem BeComDeleteRule;
        private LabelItem labelItem4;
        private CheckBoxX BtnAllowScripts;
        private ButtonX BtnSave;
        private ButtonX BtnSaveAndClose;
        private ButtonX BtnCopy;

        public RotationManagerForm(RotationManagerController rotationManagerController)
        {
            this.InitializeComponent();
            OurDirectory = new FileInfo(Application.ExecutablePath).DirectoryName;
            RotationSettings.LoadSettings();
            RotationManagerController controller1 = rotationManagerController;
            if (rotationManagerController == null)
            {
                RotationManagerController local1 = rotationManagerController;
                controller1 = new RotationManagerController();
            }
            this.rotationManagerController = controller1;
        }

        private void AddCondition(Rotation rotation, DevComponents.AdvTree.AdvTree advTree)
        {
            Node node = new Node {
                Text = rotation.Name,
                Tag = rotation
            };
            this.AddNode(node, advTree);
        }

        private void AddNode(Node node, DevComponents.AdvTree.AdvTree advTree)
        {
            advTree.BeginUpdate();
            advTree.Nodes.Add(node);
            advTree.EndUpdate();
        }

        private void BeComAddRuleClick(object sender, EventArgs e)
        {
            RotationForm form = new RotationForm(new Rotation()) {
                Location = base.Location
            };
            form.ShowDialog();
            if (form.Save)
            {
                Rotation rotation = form.Rotation;
                if (this.BeTabs.SelectedTab.Name.Equals("TabRotations"))
                {
                    this.AddCondition(rotation, this.BeRotations);
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
            this._selectedTree = this.BeRotations;
        }

        private void BeComRulesNodeDoubleClick(object sender, TreeNodeMouseEventArgs e)
        {
            this.EditRule(e.Node);
        }

        private void BehaviorFormFormClosing(object sender, FormClosingEventArgs e)
        {
            PveBehaviorSettings.AllowScripts = this.BtnAllowScripts.Checked;
            RotationSettings.SaveSettings();
        }

        private void BehaviorFormLoad(object sender, EventArgs e)
        {
            this.LoadRotationManager();
            this.BtnAllowScripts.Checked = PveBehaviorSettings.AllowScripts;
        }

        private void BeSaveBeheaviorClick(object sender, EventArgs e)
        {
            this.SaveRotationManager();
        }

        private void BeTbNewBehaviorClick(object sender, EventArgs e)
        {
            if (this.BeTBNewBehavior.Text.Equals("Enter name and press return to create new."))
            {
                this.BeTBNewBehavior.Text = "";
            }
        }

        private void BeTbNewBehaviorPreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                this.SaveRotationManager();
                RotationManagerController controller = new RotationManagerController {
                    Name = this.BeTBNewBehavior.Text
                };
                this.rotationManagerController = controller;
                this.rotationManagerController.ResetControllers();
                this.ClearTree(this.BeRotations);
                this.BeTBSelectBehavior.Items.Add(this.rotationManagerController.Name);
                this.BeTBSelectBehavior.SelectedIndex = this.BeTBSelectBehavior.FindStringExact(this.rotationManagerController.Name);
                this.BeTBNewBehavior.Text = "Enter name and press return to create.";
                RotationSettings.LoadedRotationManager = this.rotationManagerController.Name;
                this.SaveRotationManager();
                this.LoadBehavior();
            }
        }

        private void BeTbSelectBehaviorSelectedIndexChanged(object sender, EventArgs e)
        {
            if (Directory.Exists(OurDirectory + @"\Rotations"))
            {
                object[] objArray = new object[] { OurDirectory, @"\Rotations\", this.BeTBSelectBehavior.SelectedItem, ".xml" };
                if (File.Exists(string.Concat(objArray)))
                {
                    this.LoadBehavior();
                }
            }
        }

        private void BtnAllowScripts_CheckedChanged(object sender, EventArgs e)
        {
            if (this.BtnAllowScripts.Checked && (this.BtnAllowScripts.Checked != PveBehaviorSettings.AllowScripts))
            {
                MessageBox.Show("This opens a potential security hole if you use rotations from a unknown 3d party as it allows C# code to be run");
            }
        }

        private void BtnCopy_Click(object sender, EventArgs e)
        {
            if ((this._selected != null) && ((this._selectedTree != null) && (this._selected.Tag is Rotation)))
            {
                Rotation tag = (Rotation) this._selected.Tag;
                Rotation rotation = new Rotation {
                    Active = tag.Active,
                    Alt = tag.Alt,
                    Ctrl = tag.Ctrl,
                    GlobalCooldown = tag.GlobalCooldown,
                    Key = tag.Key,
                    Name = tag.Name + " - copy",
                    Shift = tag.Shift,
                    Windows = tag.Windows
                };
                foreach (Rule rule in tag.Rules.GetRules)
                {
                    rotation.Rules.AddRule(rule);
                }
                this.AddCondition(rotation, this.BeRotations);
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            this.Save();
        }

        private void BtnSaveAndClose_Click(object sender, EventArgs e)
        {
            this.Save();
            base.Close();
        }

        private void ClearTree(DevComponents.AdvTree.AdvTree advTree)
        {
            advTree.BeginUpdate();
            advTree.Nodes.Clear();
            advTree.EndUpdate(true);
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
            if (node.Tag is Rotation)
            {
                Rotation tag = (Rotation) node.Tag;
                RotationForm form = new RotationForm(tag) {
                    Location = base.Location
                };
                form.ShowDialog();
                if (form.Save)
                {
                    node.Tag = tag;
                    node.Text = tag.Name;
                }
            }
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            this.styleManager1 = new StyleManager(this.components);
            this.groupPanel4 = new GroupPanel();
            this.BtnAllowScripts = new CheckBoxX();
            this.BeTBSelectBehavior = new ComboBoxEx();
            this.labelX20 = new LabelX();
            this.BeTBNewBehavior = new TextBoxX();
            this.labelX18 = new LabelX();
            this.BeTabs = new SuperTabControl();
            this.superTabControlPanel2 = new SuperTabControlPanel();
            this.BeRotations = new DevComponents.AdvTree.AdvTree();
            this.nodeConnector1 = new NodeConnector();
            this.elementStyle1 = new ElementStyle();
            this.TabRotations = new SuperTabItem();
            this.labelItem3 = new LabelItem();
            this.BeSaveBeheavior = new ButtonItem();
            this.BeBarRuleModifier = new Bar();
            this.labelItem1 = new LabelItem();
            this.BeComAddRule = new ButtonItem();
            this.labelItem4 = new LabelItem();
            this.BeComEditRule = new ButtonItem();
            this.labelItem2 = new LabelItem();
            this.BeComDeleteRule = new ButtonItem();
            this.BtnSave = new ButtonX();
            this.BtnSaveAndClose = new ButtonX();
            this.BtnCopy = new ButtonX();
            this.groupPanel4.SuspendLayout();
            ((ISupportInitialize) this.BeTabs).BeginInit();
            this.BeTabs.SuspendLayout();
            this.superTabControlPanel2.SuspendLayout();
            this.BeRotations.BeginInit();
            this.BeBarRuleModifier.BeginInit();
            base.SuspendLayout();
            this.styleManager1.ManagerStyle = eStyle.Windows7Blue;
            this.groupPanel4.CanvasColor = SystemColors.Control;
            this.groupPanel4.ColorSchemeStyle = eDotNetBarStyle.Office2007;
            this.groupPanel4.Controls.Add(this.BtnAllowScripts);
            this.groupPanel4.Controls.Add(this.BeTBSelectBehavior);
            this.groupPanel4.Controls.Add(this.labelX20);
            this.groupPanel4.Controls.Add(this.BeTBNewBehavior);
            this.groupPanel4.Controls.Add(this.labelX18);
            this.groupPanel4.Location = new Point(3, 0);
            this.groupPanel4.Name = "groupPanel4";
            this.groupPanel4.Size = new Size(0x214, 0x61);
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
            this.groupPanel4.Style.Class = "";
            this.groupPanel4.Style.CornerDiameter = 4;
            this.groupPanel4.Style.CornerType = eCornerType.Rounded;
            this.groupPanel4.Style.TextAlignment = eStyleTextAlignment.Center;
            this.groupPanel4.Style.TextColorSchemePart = eColorSchemePart.PanelText;
            this.groupPanel4.Style.TextLineAlignment = eStyleTextAlignment.Near;
            this.groupPanel4.StyleMouseDown.Class = "";
            this.groupPanel4.StyleMouseDown.CornerType = eCornerType.Square;
            this.groupPanel4.StyleMouseOver.Class = "";
            this.groupPanel4.StyleMouseOver.CornerType = eCornerType.Square;
            this.groupPanel4.TabIndex = 11;
            this.BtnAllowScripts.BackColor = Color.Transparent;
            this.BtnAllowScripts.BackgroundStyle.Class = "";
            this.BtnAllowScripts.BackgroundStyle.CornerType = eCornerType.Square;
            this.BtnAllowScripts.Location = new Point(0x1ab, 60);
            this.BtnAllowScripts.Name = "BtnAllowScripts";
            this.BtnAllowScripts.Size = new Size(90, 0x17);
            this.BtnAllowScripts.Style = eDotNetBarStyle.StyleManagerControlled;
            this.BtnAllowScripts.TabIndex = 10;
            this.BtnAllowScripts.Text = "Allow scripts ";
            this.BtnAllowScripts.CheckedChanged += new EventHandler(this.BtnAllowScripts_CheckedChanged);
            this.BeTBSelectBehavior.DisplayMember = "Text";
            this.BeTBSelectBehavior.DrawMode = DrawMode.OwnerDrawFixed;
            this.BeTBSelectBehavior.DropDownStyle = ComboBoxStyle.DropDownList;
            this.BeTBSelectBehavior.FormattingEnabled = true;
            this.BeTBSelectBehavior.ItemHeight = 14;
            this.BeTBSelectBehavior.Location = new Point(140, 0x22);
            this.BeTBSelectBehavior.Name = "BeTBSelectBehavior";
            this.BeTBSelectBehavior.Size = new Size(0x179, 20);
            this.BeTBSelectBehavior.Style = eDotNetBarStyle.StyleManagerControlled;
            this.BeTBSelectBehavior.TabIndex = 0x2f;
            this.BeTBSelectBehavior.SelectedIndexChanged += new EventHandler(this.BeTbSelectBehaviorSelectedIndexChanged);
            this.labelX20.BackColor = Color.Transparent;
            this.labelX20.BackgroundStyle.Class = "";
            this.labelX20.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX20.Location = new Point(3, 0x20);
            this.labelX20.Name = "labelX20";
            this.labelX20.Size = new Size(0x83, 0x17);
            this.labelX20.TabIndex = 2;
            this.labelX20.Text = "Select rotation manager:";
            this.BeTBNewBehavior.Border.Class = "TextBoxBorder";
            this.BeTBNewBehavior.Border.CornerType = eCornerType.Square;
            this.BeTBNewBehavior.Location = new Point(140, 6);
            this.BeTBNewBehavior.Name = "BeTBNewBehavior";
            this.BeTBNewBehavior.Size = new Size(0x179, 20);
            this.BeTBNewBehavior.TabIndex = 1;
            this.BeTBNewBehavior.Text = "Enter name and press return to create new";
            this.BeTBNewBehavior.Click += new EventHandler(this.BeTbNewBehaviorClick);
            this.BeTBNewBehavior.PreviewKeyDown += new PreviewKeyDownEventHandler(this.BeTbNewBehaviorPreviewKeyDown);
            this.labelX18.BackColor = Color.Transparent;
            this.labelX18.BackgroundStyle.Class = "";
            this.labelX18.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX18.Location = new Point(3, 3);
            this.labelX18.Name = "labelX18";
            this.labelX18.Size = new Size(0x83, 0x17);
            this.labelX18.TabIndex = 0;
            this.labelX18.Text = "Create rotation manager:";
            this.BeTabs.BackColor = Color.Silver;
            this.BeTabs.ControlBox.CloseBox.Name = "";
            this.BeTabs.ControlBox.MenuBox.Name = "";
            this.BeTabs.ControlBox.Name = "";
            BaseItem[] items = new BaseItem[] { this.BeTabs.ControlBox.MenuBox, this.BeTabs.ControlBox.CloseBox };
            this.BeTabs.ControlBox.SubItems.AddRange(items);
            this.BeTabs.Controls.Add(this.superTabControlPanel2);
            this.BeTabs.Location = new Point(3, 0x67);
            this.BeTabs.Name = "BeTabs";
            this.BeTabs.ReorderTabsEnabled = true;
            this.BeTabs.SelectedTabFont = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold);
            this.BeTabs.SelectedTabIndex = 0;
            this.BeTabs.Size = new Size(0x214, 0x133);
            this.BeTabs.TabFont = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.BeTabs.TabIndex = 10;
            BaseItem[] itemArray2 = new BaseItem[] { this.TabRotations, this.labelItem3, this.BeSaveBeheavior };
            this.BeTabs.Tabs.AddRange(itemArray2);
            this.superTabControlPanel2.Controls.Add(this.BeRotations);
            this.superTabControlPanel2.Dock = DockStyle.Fill;
            this.superTabControlPanel2.Location = new Point(0, 0x1a);
            this.superTabControlPanel2.Name = "superTabControlPanel2";
            this.superTabControlPanel2.Size = new Size(0x214, 0x119);
            this.superTabControlPanel2.TabIndex = 0;
            this.superTabControlPanel2.TabItem = this.TabRotations;
            this.BeRotations.AccessibleRole = AccessibleRole.Outline;
            this.BeRotations.AllowDrop = true;
            this.BeRotations.BackColor = SystemColors.Window;
            this.BeRotations.BackgroundStyle.Class = "TreeBorderKey";
            this.BeRotations.BackgroundStyle.CornerType = eCornerType.Square;
            this.BeRotations.Dock = DockStyle.Top;
            this.BeRotations.Location = new Point(0, 0);
            this.BeRotations.Name = "BeRotations";
            this.BeRotations.NodesConnector = this.nodeConnector1;
            this.BeRotations.NodeStyle = this.elementStyle1;
            this.BeRotations.PathSeparator = ";";
            this.BeRotations.Size = new Size(0x214, 0x116);
            this.BeRotations.Styles.Add(this.elementStyle1);
            this.BeRotations.TabIndex = 0;
            this.BeRotations.Text = "advTree1";
            this.BeRotations.NodeDragFeedback += new TreeDragFeedbackEventHander(this.BeComRules_NodeDragFeedback);
            this.BeRotations.NodeClick += new TreeNodeMouseEventHandler(this.BeComRulesNodeClick);
            this.BeRotations.NodeDoubleClick += new TreeNodeMouseEventHandler(this.BeComRulesNodeDoubleClick);
            this.nodeConnector1.LineColor = SystemColors.ControlText;
            this.elementStyle1.Class = "";
            this.elementStyle1.CornerType = eCornerType.Square;
            this.elementStyle1.Name = "elementStyle1";
            this.elementStyle1.TextColor = SystemColors.ControlText;
            this.TabRotations.AttachedControl = this.superTabControlPanel2;
            this.TabRotations.GlobalItem = false;
            this.TabRotations.Name = "TabRotations";
            this.TabRotations.Text = "Rotations";
            this.labelItem3.Name = "labelItem3";
            this.labelItem3.Text = "                                                                                                                              ";
            this.BeSaveBeheavior.Name = "BeSaveBeheavior";
            this.BeSaveBeheavior.Text = "Save rotations";
            this.BeSaveBeheavior.Click += new EventHandler(this.BeSaveBeheaviorClick);
            this.BeBarRuleModifier.AntiAlias = true;
            BaseItem[] itemArray3 = new BaseItem[] { this.labelItem1, this.BeComAddRule, this.labelItem4, this.BeComEditRule, this.labelItem2, this.BeComDeleteRule };
            this.BeBarRuleModifier.Items.AddRange(itemArray3);
            this.BeBarRuleModifier.Location = new Point(3, 0x1a0);
            this.BeBarRuleModifier.Name = "BeBarRuleModifier";
            this.BeBarRuleModifier.Size = new Size(0x214, 0x19);
            this.BeBarRuleModifier.Stretch = true;
            this.BeBarRuleModifier.Style = eDotNetBarStyle.StyleManagerControlled;
            this.BeBarRuleModifier.TabIndex = 12;
            this.BeBarRuleModifier.TabStop = false;
            this.BeBarRuleModifier.Text = "bar1";
            this.labelItem1.Name = "labelItem1";
            this.labelItem1.Text = "              ";
            this.BeComAddRule.Name = "BeComAddRule";
            this.BeComAddRule.Text = "Add Rotation";
            this.BeComAddRule.Click += new EventHandler(this.BeComAddRuleClick);
            this.labelItem4.Name = "labelItem4";
            this.labelItem4.Text = "              ";
            this.BeComEditRule.Name = "BeComEditRule";
            this.BeComEditRule.Text = "Double click on rotation to edit";
            this.labelItem2.Name = "labelItem2";
            this.labelItem2.Text = "              ";
            this.BeComDeleteRule.Name = "BeComDeleteRule";
            this.BeComDeleteRule.Text = "Delete Rotation";
            this.BeComDeleteRule.Click += new EventHandler(this.BeComDeleteRuleClick);
            this.BtnSave.AccessibleRole = AccessibleRole.PushButton;
            this.BtnSave.ColorTable = eButtonColor.OrangeWithBackground;
            this.BtnSave.Location = new Point(0x160, 0x1bf);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new Size(0x4b, 0x16);
            this.BtnSave.Style = eDotNetBarStyle.StyleManagerControlled;
            this.BtnSave.TabIndex = 0x3e;
            this.BtnSave.Text = "Save";
            this.BtnSave.Click += new EventHandler(this.BtnSave_Click);
            this.BtnSaveAndClose.AccessibleRole = AccessibleRole.PushButton;
            this.BtnSaveAndClose.ColorTable = eButtonColor.OrangeWithBackground;
            this.BtnSaveAndClose.Location = new Point(0x1b1, 0x1bf);
            this.BtnSaveAndClose.Name = "BtnSaveAndClose";
            this.BtnSaveAndClose.Size = new Size(0x66, 0x16);
            this.BtnSaveAndClose.Style = eDotNetBarStyle.StyleManagerControlled;
            this.BtnSaveAndClose.TabIndex = 0x3f;
            this.BtnSaveAndClose.Text = "Save and close";
            this.BtnSaveAndClose.Click += new EventHandler(this.BtnSaveAndClose_Click);
            this.BtnCopy.AccessibleRole = AccessibleRole.PushButton;
            this.BtnCopy.ColorTable = eButtonColor.OrangeWithBackground;
            this.BtnCopy.Location = new Point(3, 0x1bf);
            this.BtnCopy.Name = "BtnCopy";
            this.BtnCopy.Size = new Size(120, 0x17);
            this.BtnCopy.Style = eDotNetBarStyle.StyleManagerControlled;
            this.BtnCopy.TabIndex = 0x40;
            this.BtnCopy.Text = "Copy selected rotation";
            this.BtnCopy.Click += new EventHandler(this.BtnCopy_Click);
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.FromArgb(0xdf, 0xe9, 0xf5);
            base.ClientSize = new Size(540, 0x1db);
            base.Controls.Add(this.BtnCopy);
            base.Controls.Add(this.BtnSaveAndClose);
            base.Controls.Add(this.BtnSave);
            base.Controls.Add(this.groupPanel4);
            base.Controls.Add(this.BeTabs);
            base.Controls.Add(this.BeBarRuleModifier);
            this.DoubleBuffered = true;
            base.Name = "RotationManagerForm";
            base.FormClosing += new FormClosingEventHandler(this.BehaviorFormFormClosing);
            base.Load += new EventHandler(this.BehaviorFormLoad);
            this.groupPanel4.ResumeLayout(false);
            ((ISupportInitialize) this.BeTabs).EndInit();
            this.BeTabs.ResumeLayout(false);
            this.superTabControlPanel2.ResumeLayout(false);
            this.BeRotations.EndInit();
            this.BeBarRuleModifier.EndInit();
            base.ResumeLayout(false);
        }

        private void LoadBehavior()
        {
            this.rotationManagerController = new RotationManagerController();
            this.ClearTree(this.BeRotations);
            this.rotationManagerController.Load(string.Concat(new object[] { OurDirectory, @"\Rotations\", this.BeTBSelectBehavior.SelectedItem, ".xml" }));
            foreach (Rotation rotation in this.rotationManagerController.Rotations)
            {
                this.AddCondition(rotation, this.BeRotations);
            }
            this.BeTabs.Enabled = true;
            this.BeBarRuleModifier.Enabled = true;
            RotationSettings.LoadedRotationManager = this.BeTBSelectBehavior.SelectedItem.ToString();
            RotationSettings.SaveSettings();
        }

        private void LoadRotationManager()
        {
            if (Directory.Exists(OurDirectory + @"\Rotations"))
            {
                this.BeTBSelectBehavior.Items.Clear();
                string[] files = Directory.GetFiles(OurDirectory + @"\Rotations", "*xml");
                int index = 0;
                while (true)
                {
                    if (index >= files.Length)
                    {
                        if (this.BeTBSelectBehavior.Items.Contains(RotationSettings.LoadedRotationManager))
                        {
                            this.BeTBSelectBehavior.SelectedIndex = this.BeTBSelectBehavior.FindStringExact(RotationSettings.LoadedRotationManager);
                        }
                        break;
                    }
                    string path = files[index];
                    this.BeTBSelectBehavior.Items.Add(Path.GetFileNameWithoutExtension(path));
                    index++;
                }
            }
            if (string.IsNullOrEmpty(this.rotationManagerController.Name))
            {
                this.BeTabs.Enabled = false;
                this.BeBarRuleModifier.Enabled = false;
            }
        }

        private void Save()
        {
            if (this.rotationManagerController.Name != string.Empty)
            {
                this.rotationManagerController.ResetControllers();
                foreach (Node node in this.BeRotations.Nodes)
                {
                    Rotation tag = (Rotation) node.Tag;
                    this.rotationManagerController.Rotations.Add(tag);
                }
                this.rotationManagerController.Save();
                RotationSettings.LoadedRotationManager = this.BeTBSelectBehavior.SelectedItem.ToString();
                RotationSettings.SaveSettings();
            }
        }

        private void SaveRotationManager()
        {
            if (this.rotationManagerController.Name != string.Empty)
            {
                this.rotationManagerController.ResetControllers();
                foreach (Node node in this.BeRotations.Nodes)
                {
                    Rotation tag = (Rotation) node.Tag;
                    this.rotationManagerController.Rotations.Add(tag);
                }
                this.rotationManagerController.Save();
            }
        }
    }
}

