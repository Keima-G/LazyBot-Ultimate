namespace LazyEvo.PVEBehavior
{
    using DevComponents.AdvTree;
    using DevComponents.DotNetBar;
    using DevComponents.DotNetBar.Controls;
    using DevComponents.Editors;
    using LazyEvo.PVEBehavior.Behavior;
    using LazyEvo.PVEBehavior.Builders;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Windows.Forms;

    internal class BehaviorForm : Office2007Form
    {
        private readonly string _ourDirectory;
        private BehaviorController _behavior;
        private bool _isLoading;
        private Node _selected;
        private DevComponents.AdvTree.AdvTree _selectedTree;
        private IContainer components;
        private StyleManager styleManager1;
        private GroupPanel groupPanel4;
        internal ComboBoxEx BeTBSelectBehavior;
        private LabelX labelX20;
        private TextBoxX BeTBNewBehavior;
        private LabelX labelX18;
        private GroupPanel BeGMisc;
        private IntegerInput BeGlobalCooldown;
        private LabelX labelX25;
        private IntegerInput BePrePullDistance;
        private LabelX labelX23;
        private IntegerInput BePullDistance;
        private LabelX labelX22;
        private CheckBoxX BeSendPet;
        private CheckBoxX BeEnableAutoAttack;
        private IntegerInput BeCombatDistance;
        private LabelX labelX21;
        private SuperTabControl BeTabs;
        private SuperTabControlPanel superTabControlPanel5;
        private DevComponents.AdvTree.AdvTree BePrePullRules;
        private NodeConnector nodeConnector5;
        private ElementStyle elementStyle5;
        private SuperTabItem TabPrePull;
        private SuperTabControlPanel superTabControlPanel3;
        private DevComponents.AdvTree.AdvTree BeBuffRules;
        private NodeConnector nodeConnector2;
        private ElementStyle elementStyle2;
        private SuperTabItem TabBuffs;
        private SuperTabControlPanel superTabControlPanel1;
        private DevComponents.AdvTree.AdvTree BePullRules;
        private NodeConnector nodeConnector3;
        private ElementStyle elementStyle3;
        private SuperTabItem TabPull;
        private SuperTabControlPanel superTabControlPanel4;
        private DevComponents.AdvTree.AdvTree BeRestRules;
        private NodeConnector nodeConnector4;
        private ElementStyle elementStyle4;
        private SuperTabItem TabRest;
        private SuperTabControlPanel superTabControlPanel2;
        private DevComponents.AdvTree.AdvTree BeComRules;
        private NodeConnector nodeConnector1;
        private ElementStyle elementStyle1;
        private SuperTabItem TabCombat;
        private LabelItem labelItem3;
        private ButtonItem BeSaveBeheavior;
        private Bar BeBarRuleModifier;
        private ButtonItem BeComAddRule;
        private LabelItem labelItem1;
        private ButtonItem BeComEditRule;
        private LabelItem labelItem2;
        private ButtonItem BeComDeleteRule;
        private ButtonX BtnBehaviorGenerator;
        internal ComboBoxEx SelectEngine;
        private ComboItem Deathknight;
        private ComboItem Paladin;
        private ButtonItem BtnAddScript;
        private LabelItem labelItem4;
        private CheckBoxX BtnAllowScripts;

        public BehaviorForm(BehaviorController behaviorController)
        {
            this.InitializeComponent();
            string directoryName = new FileInfo(Application.ExecutablePath).DirectoryName;
            this._ourDirectory = directoryName;
            PveBehaviorSettings.LoadSettings();
            BehaviorController controller1 = behaviorController;
            if (behaviorController == null)
            {
                BehaviorController local1 = behaviorController;
                controller1 = new BehaviorController();
            }
            this._behavior = controller1;
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

        private void AskForSave()
        {
            if (MessageBox.Show("Save currently selected behavior?", "Save", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                this.SaveCurrentBehavior();
            }
        }

        private void BeBuffRules_NodeDragFeedback(object sender, TreeDragFeedbackEventArgs e)
        {
            if (e.ParentNode != null)
            {
                e.AllowDrop = false;
            }
        }

        private void BeBuffRulesNodeClick(object sender, TreeNodeMouseEventArgs e)
        {
            this._selected = e.Node;
            this._selectedTree = this.BeBuffRules;
        }

        private void BeBuffRulesNodeDoubleClick(object sender, TreeNodeMouseEventArgs e)
        {
            this.EditRule(e.Node);
        }

        private void BeComAddRuleClick(object sender, EventArgs e)
        {
            RuleEditor editor = new RuleEditor(new Rule(), true) {
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
                if (this.BeTabs.SelectedTab.Name.Equals("TabBuffs"))
                {
                    this.AddCondition(rule, this.BeBuffRules);
                }
                if (this.BeTabs.SelectedTab.Name.Equals("TabPull"))
                {
                    this.AddCondition(rule, this.BePullRules);
                }
                if (this.BeTabs.SelectedTab.Name.Equals("TabRest"))
                {
                    this.AddCondition(rule, this.BeRestRules);
                }
                if (this.BeTabs.SelectedTab.Name.Equals("TabPrePull"))
                {
                    this.AddCondition(rule, this.BePrePullRules);
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

        private void BehaviorFormFormClosing(object sender, FormClosingEventArgs e)
        {
            PveBehaviorSettings.AllowScripts = this.BtnAllowScripts.Checked;
            PveBehaviorSettings.SaveSettings();
            this.AskForSave();
        }

        private void BehaviorFormLoad(object sender, EventArgs e)
        {
            this._isLoading = true;
            this.LoadBehaviors();
            this._isLoading = false;
            this.SelectEngine.SelectedIndex = 0;
            this.BtnAllowScripts.Checked = PveBehaviorSettings.AllowScripts;
        }

        private void BePrePullRules_NodeDragFeedback(object sender, TreeDragFeedbackEventArgs e)
        {
            if (e.ParentNode != null)
            {
                e.AllowDrop = false;
            }
        }

        private void BePrePullRulesNodeClick(object sender, TreeNodeMouseEventArgs e)
        {
            this._selected = e.Node;
            this._selectedTree = this.BePrePullRules;
        }

        private void BePrePullRulesNodeDoubleClick(object sender, TreeNodeMouseEventArgs e)
        {
            this.EditRule(e.Node);
        }

        private void BePullRules_NodeDragFeedback(object sender, TreeDragFeedbackEventArgs e)
        {
            if (e.ParentNode != null)
            {
                e.AllowDrop = false;
            }
        }

        private void BePullRulesNodeClick(object sender, TreeNodeMouseEventArgs e)
        {
            this._selected = e.Node;
            this._selectedTree = this.BePullRules;
        }

        private void BePullRulesNodeDoubleClick(object sender, TreeNodeMouseEventArgs e)
        {
            this.EditRule(e.Node);
        }

        private void BeRestRules_NodeDragFeedback(object sender, TreeDragFeedbackEventArgs e)
        {
            if (e.ParentNode != null)
            {
                e.AllowDrop = false;
            }
        }

        private void BeRestRulesNodeClick(object sender, TreeNodeMouseEventArgs e)
        {
            this._selected = e.Node;
            this._selectedTree = this.BeRestRules;
        }

        private void BeRestRulesNodeDoubleClick(object sender, TreeNodeMouseEventArgs e)
        {
            this.EditRule(e.Node);
        }

        private void BeSaveBeheaviorClick(object sender, EventArgs e)
        {
            this.SaveCurrentBehavior();
        }

        private void BeTbNewBehaviorClick(object sender, EventArgs e)
        {
            if (this.BeTBNewBehavior.Text.Equals("Enter name and press return to create new behavior."))
            {
                this.BeTBNewBehavior.Text = "";
            }
        }

        private void BeTbNewBehaviorPreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                this.SaveCurrentBehavior();
                BehaviorController controller = new BehaviorController {
                    Name = this.BeTBNewBehavior.Text
                };
                this._behavior = controller;
                this._behavior.ResetControllers();
                this.ClearTree(this.BeComRules);
                this.ClearTree(this.BePullRules);
                this.ClearTree(this.BePrePullRules);
                this.ClearTree(this.BeBuffRules);
                this.ClearTree(this.BeRestRules);
                this.BeTBSelectBehavior.Items.Add(this._behavior.Name);
                this.BeTBSelectBehavior.SelectedIndex = this.BeTBSelectBehavior.FindStringExact(this._behavior.Name);
                this.BeTBNewBehavior.Text = "Enter name and press return to create new behavior.";
                PveBehaviorSettings.LoadedBeharvior = this._behavior.Name;
                this.SaveCurrentBehavior();
                this.LoadBehavior();
            }
        }

        private void BeTbSelectBehaviorSelectedIndexChanged(object sender, EventArgs e)
        {
            if (Directory.Exists(this._ourDirectory + @"\Behaviors"))
            {
                object[] objArray = new object[] { this._ourDirectory, @"\Behaviors\", this.BeTBSelectBehavior.SelectedItem, ".xml" };
                if (File.Exists(string.Concat(objArray)))
                {
                    if (!this._isLoading)
                    {
                        this.AskForSave();
                    }
                    this.LoadBehavior();
                }
            }
        }

        private void BtnAddScript_Click(object sender, EventArgs e)
        {
            Rule rule = new Rule();
            ScriptEditor editor = new ScriptEditor(rule) {
                Location = base.Location
            };
            editor.ShowDialog();
            if (editor.Save)
            {
                if (this.BeTabs.SelectedTab.Name.Equals("TabCombat"))
                {
                    this.AddCondition(rule, this.BeComRules);
                }
                if (this.BeTabs.SelectedTab.Name.Equals("TabBuffs"))
                {
                    this.AddCondition(rule, this.BeBuffRules);
                }
                if (this.BeTabs.SelectedTab.Name.Equals("TabPull"))
                {
                    this.AddCondition(rule, this.BePullRules);
                }
                if (this.BeTabs.SelectedTab.Name.Equals("TabRest"))
                {
                    this.AddCondition(rule, this.BeRestRules);
                }
                if (this.BeTabs.SelectedTab.Name.Equals("TabPrePull"))
                {
                    this.AddCondition(rule, this.BePrePullRules);
                }
            }
        }

        private void BtnAllowScripts_CheckedChanged(object sender, EventArgs e)
        {
            if (this.BtnAllowScripts.Checked && (this.BtnAllowScripts.Checked != PveBehaviorSettings.AllowScripts))
            {
                MessageBox.Show("This opens a potential security hole if you use behaviors from a unknown 3d party as it allows C# code to be run");
            }
        }

        private void BtnBehaviorGenerator_Click(object sender, EventArgs e)
        {
            switch (this.SelectEngine.SelectedIndex)
            {
                case 0:
                    new DeathknightBuilder().Show();
                    return;

                case 1:
                    new PaladinBuilder().Show();
                    return;
            }
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
                    RuleEditor editor = new RuleEditor(tag, true) {
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
            this.groupPanel4 = new GroupPanel();
            this.BtnAllowScripts = new CheckBoxX();
            this.BtnBehaviorGenerator = new ButtonX();
            this.SelectEngine = new ComboBoxEx();
            this.Deathknight = new ComboItem();
            this.Paladin = new ComboItem();
            this.BeTBSelectBehavior = new ComboBoxEx();
            this.labelX20 = new LabelX();
            this.BeTBNewBehavior = new TextBoxX();
            this.labelX18 = new LabelX();
            this.BeGMisc = new GroupPanel();
            this.BeGlobalCooldown = new IntegerInput();
            this.labelX25 = new LabelX();
            this.BePrePullDistance = new IntegerInput();
            this.labelX23 = new LabelX();
            this.BePullDistance = new IntegerInput();
            this.labelX22 = new LabelX();
            this.BeSendPet = new CheckBoxX();
            this.BeEnableAutoAttack = new CheckBoxX();
            this.BeCombatDistance = new IntegerInput();
            this.labelX21 = new LabelX();
            this.BeTabs = new SuperTabControl();
            this.superTabControlPanel5 = new SuperTabControlPanel();
            this.BePrePullRules = new DevComponents.AdvTree.AdvTree();
            this.nodeConnector5 = new NodeConnector();
            this.elementStyle5 = new ElementStyle();
            this.TabPrePull = new SuperTabItem();
            this.superTabControlPanel3 = new SuperTabControlPanel();
            this.BeBuffRules = new DevComponents.AdvTree.AdvTree();
            this.nodeConnector2 = new NodeConnector();
            this.elementStyle2 = new ElementStyle();
            this.TabBuffs = new SuperTabItem();
            this.superTabControlPanel4 = new SuperTabControlPanel();
            this.BeRestRules = new DevComponents.AdvTree.AdvTree();
            this.nodeConnector4 = new NodeConnector();
            this.elementStyle4 = new ElementStyle();
            this.TabRest = new SuperTabItem();
            this.superTabControlPanel2 = new SuperTabControlPanel();
            this.BeComRules = new DevComponents.AdvTree.AdvTree();
            this.nodeConnector1 = new NodeConnector();
            this.elementStyle1 = new ElementStyle();
            this.TabCombat = new SuperTabItem();
            this.superTabControlPanel1 = new SuperTabControlPanel();
            this.BePullRules = new DevComponents.AdvTree.AdvTree();
            this.nodeConnector3 = new NodeConnector();
            this.elementStyle3 = new ElementStyle();
            this.TabPull = new SuperTabItem();
            this.labelItem3 = new LabelItem();
            this.BeSaveBeheavior = new ButtonItem();
            this.BeBarRuleModifier = new Bar();
            this.BeComAddRule = new ButtonItem();
            this.labelItem1 = new LabelItem();
            this.BtnAddScript = new ButtonItem();
            this.labelItem4 = new LabelItem();
            this.BeComEditRule = new ButtonItem();
            this.labelItem2 = new LabelItem();
            this.BeComDeleteRule = new ButtonItem();
            this.groupPanel4.SuspendLayout();
            this.BeGMisc.SuspendLayout();
            this.BeGlobalCooldown.BeginInit();
            this.BePrePullDistance.BeginInit();
            this.BePullDistance.BeginInit();
            this.BeCombatDistance.BeginInit();
            ((ISupportInitialize) this.BeTabs).BeginInit();
            this.BeTabs.SuspendLayout();
            this.superTabControlPanel5.SuspendLayout();
            this.BePrePullRules.BeginInit();
            this.superTabControlPanel3.SuspendLayout();
            this.BeBuffRules.BeginInit();
            this.superTabControlPanel4.SuspendLayout();
            this.BeRestRules.BeginInit();
            this.superTabControlPanel2.SuspendLayout();
            this.BeComRules.BeginInit();
            this.superTabControlPanel1.SuspendLayout();
            this.BePullRules.BeginInit();
            this.BeBarRuleModifier.BeginInit();
            base.SuspendLayout();
            this.styleManager1.ManagerStyle = eStyle.Windows7Blue;
            this.groupPanel4.CanvasColor = SystemColors.Control;
            this.groupPanel4.ColorSchemeStyle = eDotNetBarStyle.Office2007;
            this.groupPanel4.Controls.Add(this.BtnAllowScripts);
            this.groupPanel4.Controls.Add(this.BtnBehaviorGenerator);
            this.groupPanel4.Controls.Add(this.SelectEngine);
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
            this.BtnAllowScripts.Location = new Point(0x1a9, 0x3b);
            this.BtnAllowScripts.Name = "BtnAllowScripts";
            this.BtnAllowScripts.Size = new Size(0x5c, 0x17);
            this.BtnAllowScripts.Style = eDotNetBarStyle.StyleManagerControlled;
            this.BtnAllowScripts.TabIndex = 10;
            this.BtnAllowScripts.Text = "Allow scripts";
            this.BtnAllowScripts.CheckedChanged += new EventHandler(this.BtnAllowScripts_CheckedChanged);
            this.BtnBehaviorGenerator.AccessibleRole = AccessibleRole.PushButton;
            this.BtnBehaviorGenerator.ColorTable = eButtonColor.OrangeWithBackground;
            this.BtnBehaviorGenerator.Location = new Point(2, 0x3b);
            this.BtnBehaviorGenerator.Name = "BtnBehaviorGenerator";
            this.BtnBehaviorGenerator.Size = new Size(0x67, 0x1b);
            this.BtnBehaviorGenerator.Style = eDotNetBarStyle.StyleManagerControlled;
            this.BtnBehaviorGenerator.TabIndex = 0x4e;
            this.BtnBehaviorGenerator.Text = "Behavior generator";
            this.BtnBehaviorGenerator.Click += new EventHandler(this.BtnBehaviorGenerator_Click);
            this.SelectEngine.DisplayMember = "Text";
            this.SelectEngine.DrawMode = DrawMode.OwnerDrawFixed;
            this.SelectEngine.DropDownStyle = ComboBoxStyle.DropDownList;
            this.SelectEngine.FormattingEnabled = true;
            this.SelectEngine.ItemHeight = 14;
            object[] items = new object[] { this.Deathknight, this.Paladin };
            this.SelectEngine.Items.AddRange(items);
            this.SelectEngine.Location = new Point(0x6f, 0x3f);
            this.SelectEngine.Name = "SelectEngine";
            this.SelectEngine.Size = new Size(150, 20);
            this.SelectEngine.Style = eDotNetBarStyle.StyleManagerControlled;
            this.SelectEngine.TabIndex = 0x4d;
            this.Deathknight.Text = "Deathknight";
            this.Paladin.Text = "Paladin";
            this.BeTBSelectBehavior.DisplayMember = "Text";
            this.BeTBSelectBehavior.DrawMode = DrawMode.OwnerDrawFixed;
            this.BeTBSelectBehavior.DropDownStyle = ComboBoxStyle.DropDownList;
            this.BeTBSelectBehavior.FormattingEnabled = true;
            this.BeTBSelectBehavior.ItemHeight = 14;
            this.BeTBSelectBehavior.Location = new Point(0x6f, 0x22);
            this.BeTBSelectBehavior.Name = "BeTBSelectBehavior";
            this.BeTBSelectBehavior.Size = new Size(0x196, 20);
            this.BeTBSelectBehavior.Style = eDotNetBarStyle.StyleManagerControlled;
            this.BeTBSelectBehavior.TabIndex = 0x2f;
            this.BeTBSelectBehavior.SelectedIndexChanged += new EventHandler(this.BeTbSelectBehaviorSelectedIndexChanged);
            this.labelX20.BackColor = Color.Transparent;
            this.labelX20.BackgroundStyle.Class = "";
            this.labelX20.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX20.Location = new Point(3, 0x20);
            this.labelX20.Name = "labelX20";
            this.labelX20.Size = new Size(0x52, 0x17);
            this.labelX20.TabIndex = 2;
            this.labelX20.Text = "Select Behavior:";
            this.BeTBNewBehavior.Border.Class = "TextBoxBorder";
            this.BeTBNewBehavior.Border.CornerType = eCornerType.Square;
            this.BeTBNewBehavior.Location = new Point(0x6f, 6);
            this.BeTBNewBehavior.Name = "BeTBNewBehavior";
            this.BeTBNewBehavior.Size = new Size(0x196, 20);
            this.BeTBNewBehavior.TabIndex = 1;
            this.BeTBNewBehavior.Text = "Enter name and press return to create new behavior.";
            this.BeTBNewBehavior.Click += new EventHandler(this.BeTbNewBehaviorClick);
            this.BeTBNewBehavior.PreviewKeyDown += new PreviewKeyDownEventHandler(this.BeTbNewBehaviorPreviewKeyDown);
            this.labelX18.BackColor = Color.Transparent;
            this.labelX18.BackgroundStyle.Class = "";
            this.labelX18.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX18.Location = new Point(3, 3);
            this.labelX18.Name = "labelX18";
            this.labelX18.Size = new Size(0x5b, 0x17);
            this.labelX18.TabIndex = 0;
            this.labelX18.Text = "Create Behavior:";
            this.BeGMisc.BackColor = Color.Transparent;
            this.BeGMisc.BackgroundImageLayout = ImageLayout.None;
            this.BeGMisc.CanvasColor = SystemColors.Control;
            this.BeGMisc.ColorSchemeStyle = eDotNetBarStyle.Office2007;
            this.BeGMisc.Controls.Add(this.BeGlobalCooldown);
            this.BeGMisc.Controls.Add(this.labelX25);
            this.BeGMisc.Controls.Add(this.BePrePullDistance);
            this.BeGMisc.Controls.Add(this.labelX23);
            this.BeGMisc.Controls.Add(this.BePullDistance);
            this.BeGMisc.Controls.Add(this.labelX22);
            this.BeGMisc.Controls.Add(this.BeSendPet);
            this.BeGMisc.Controls.Add(this.BeEnableAutoAttack);
            this.BeGMisc.Controls.Add(this.BeCombatDistance);
            this.BeGMisc.Controls.Add(this.labelX21);
            this.BeGMisc.Location = new Point(3, 0x1bf);
            this.BeGMisc.Name = "BeGMisc";
            this.BeGMisc.Size = new Size(0x211, 0x6d);
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
            this.BeGMisc.Text = "Misc settings";
            this.BeGlobalCooldown.BackgroundStyle.Class = "DateTimeInputBackground";
            this.BeGlobalCooldown.BackgroundStyle.CornerType = eCornerType.Square;
            this.BeGlobalCooldown.ButtonFreeText.Shortcut = eShortcut.F2;
            this.BeGlobalCooldown.Location = new Point(410, 0x25);
            this.BeGlobalCooldown.Name = "BeGlobalCooldown";
            this.BeGlobalCooldown.ShowUpDown = true;
            this.BeGlobalCooldown.Size = new Size(0x40, 20);
            this.BeGlobalCooldown.TabIndex = 9;
            this.BeGlobalCooldown.Value = 0x7d0;
            this.labelX25.BackgroundStyle.Class = "";
            this.labelX25.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX25.Location = new Point(0x13c, 0x20);
            this.labelX25.Name = "labelX25";
            this.labelX25.Size = new Size(0x5b, 0x1b);
            this.labelX25.TabIndex = 8;
            this.labelX25.Text = "Global cooldown:";
            this.BePrePullDistance.BackgroundStyle.Class = "DateTimeInputBackground";
            this.BePrePullDistance.BackgroundStyle.CornerType = eCornerType.Square;
            this.BePrePullDistance.ButtonFreeText.Shortcut = eShortcut.F2;
            this.BePrePullDistance.Location = new Point(0x63, 11);
            this.BePrePullDistance.Name = "BePrePullDistance";
            this.BePrePullDistance.ShowUpDown = true;
            this.BePrePullDistance.Size = new Size(0x40, 20);
            this.BePrePullDistance.TabIndex = 7;
            this.BePrePullDistance.Value = 0x19;
            this.labelX23.BackgroundStyle.Class = "";
            this.labelX23.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX23.Location = new Point(6, 6);
            this.labelX23.Name = "labelX23";
            this.labelX23.Size = new Size(0x5b, 0x1b);
            this.labelX23.TabIndex = 6;
            this.labelX23.Text = "Pre-Pull distance:";
            this.BePullDistance.BackgroundStyle.Class = "DateTimeInputBackground";
            this.BePullDistance.BackgroundStyle.CornerType = eCornerType.Square;
            this.BePullDistance.ButtonFreeText.Shortcut = eShortcut.F2;
            this.BePullDistance.Location = new Point(0xf6, 11);
            this.BePullDistance.Name = "BePullDistance";
            this.BePullDistance.ShowUpDown = true;
            this.BePullDistance.Size = new Size(0x40, 20);
            this.BePullDistance.TabIndex = 5;
            this.BePullDistance.Value = 0x19;
            this.labelX22.BackgroundStyle.Class = "";
            this.labelX22.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX22.Location = new Point(170, 6);
            this.labelX22.Name = "labelX22";
            this.labelX22.Size = new Size(0x5b, 0x1b);
            this.labelX22.TabIndex = 4;
            this.labelX22.Text = "Pull distance:";
            this.BeSendPet.BackgroundStyle.Class = "";
            this.BeSendPet.BackgroundStyle.CornerType = eCornerType.Square;
            this.BeSendPet.Location = new Point(3, 0x3d);
            this.BeSendPet.Name = "BeSendPet";
            this.BeSendPet.Size = new Size(0x9b, 0x17);
            this.BeSendPet.Style = eDotNetBarStyle.StyleManagerControlled;
            this.BeSendPet.TabIndex = 3;
            this.BeSendPet.Text = "Send pet into combat";
            this.BeEnableAutoAttack.BackgroundStyle.Class = "";
            this.BeEnableAutoAttack.BackgroundStyle.CornerType = eCornerType.Square;
            this.BeEnableAutoAttack.Location = new Point(3, 0x20);
            this.BeEnableAutoAttack.Name = "BeEnableAutoAttack";
            this.BeEnableAutoAttack.Size = new Size(0x9b, 0x17);
            this.BeEnableAutoAttack.Style = eDotNetBarStyle.StyleManagerControlled;
            this.BeEnableAutoAttack.TabIndex = 2;
            this.BeEnableAutoAttack.Text = "Auto attack when pulling";
            this.BeCombatDistance.BackgroundStyle.Class = "DateTimeInputBackground";
            this.BeCombatDistance.BackgroundStyle.CornerType = eCornerType.Square;
            this.BeCombatDistance.ButtonFreeText.Shortcut = eShortcut.F2;
            this.BeCombatDistance.Location = new Point(410, 11);
            this.BeCombatDistance.Name = "BeCombatDistance";
            this.BeCombatDistance.ShowUpDown = true;
            this.BeCombatDistance.Size = new Size(0x40, 20);
            this.BeCombatDistance.TabIndex = 1;
            this.BeCombatDistance.Value = 0x19;
            this.labelX21.BackgroundStyle.Class = "";
            this.labelX21.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX21.Location = new Point(0x13c, 6);
            this.labelX21.Name = "labelX21";
            this.labelX21.Size = new Size(0x5b, 0x1b);
            this.labelX21.TabIndex = 0;
            this.labelX21.Text = "Combat distance:";
            this.BeTabs.BackColor = Color.Silver;
            this.BeTabs.ControlBox.CloseBox.Name = "";
            this.BeTabs.ControlBox.MenuBox.Name = "";
            this.BeTabs.ControlBox.Name = "";
            BaseItem[] itemArray = new BaseItem[] { this.BeTabs.ControlBox.MenuBox, this.BeTabs.ControlBox.CloseBox };
            this.BeTabs.ControlBox.SubItems.AddRange(itemArray);
            this.BeTabs.Controls.Add(this.superTabControlPanel5);
            this.BeTabs.Controls.Add(this.superTabControlPanel3);
            this.BeTabs.Controls.Add(this.superTabControlPanel4);
            this.BeTabs.Controls.Add(this.superTabControlPanel2);
            this.BeTabs.Controls.Add(this.superTabControlPanel1);
            this.BeTabs.Location = new Point(3, 0x67);
            this.BeTabs.Name = "BeTabs";
            this.BeTabs.ReorderTabsEnabled = true;
            this.BeTabs.SelectedTabFont = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold);
            this.BeTabs.SelectedTabIndex = 0;
            this.BeTabs.Size = new Size(0x214, 0x133);
            this.BeTabs.TabFont = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.BeTabs.TabIndex = 10;
            BaseItem[] itemArray2 = new BaseItem[] { this.TabPrePull, this.TabPull, this.TabCombat, this.TabRest, this.TabBuffs, this.labelItem3, this.BeSaveBeheavior };
            this.BeTabs.Tabs.AddRange(itemArray2);
            this.superTabControlPanel5.Controls.Add(this.BePrePullRules);
            this.superTabControlPanel5.Dock = DockStyle.Fill;
            this.superTabControlPanel5.Location = new Point(0, 0x1a);
            this.superTabControlPanel5.Name = "superTabControlPanel5";
            this.superTabControlPanel5.Size = new Size(0x214, 0x119);
            this.superTabControlPanel5.TabIndex = 0;
            this.superTabControlPanel5.TabItem = this.TabPrePull;
            this.BePrePullRules.AccessibleRole = AccessibleRole.Outline;
            this.BePrePullRules.AllowDrop = true;
            this.BePrePullRules.BackColor = SystemColors.Window;
            this.BePrePullRules.BackgroundStyle.Class = "TreeBorderKey";
            this.BePrePullRules.BackgroundStyle.CornerType = eCornerType.Square;
            this.BePrePullRules.Dock = DockStyle.Top;
            this.BePrePullRules.Location = new Point(0, 0);
            this.BePrePullRules.Name = "BePrePullRules";
            this.BePrePullRules.NodesConnector = this.nodeConnector5;
            this.BePrePullRules.NodeStyle = this.elementStyle5;
            this.BePrePullRules.PathSeparator = ";";
            this.BePrePullRules.Size = new Size(0x214, 0x116);
            this.BePrePullRules.Styles.Add(this.elementStyle5);
            this.BePrePullRules.TabIndex = 2;
            this.BePrePullRules.Text = "advTree1";
            this.BePrePullRules.NodeDragFeedback += new TreeDragFeedbackEventHander(this.BePrePullRules_NodeDragFeedback);
            this.BePrePullRules.NodeClick += new TreeNodeMouseEventHandler(this.BePrePullRulesNodeClick);
            this.BePrePullRules.NodeDoubleClick += new TreeNodeMouseEventHandler(this.BePrePullRulesNodeDoubleClick);
            this.nodeConnector5.LineColor = SystemColors.ControlText;
            this.elementStyle5.Class = "";
            this.elementStyle5.CornerType = eCornerType.Square;
            this.elementStyle5.Name = "elementStyle5";
            this.elementStyle5.TextColor = SystemColors.ControlText;
            this.TabPrePull.AttachedControl = this.superTabControlPanel5;
            this.TabPrePull.GlobalItem = false;
            this.TabPrePull.Name = "TabPrePull";
            this.TabPrePull.Text = "Pre-Pull";
            this.superTabControlPanel3.Controls.Add(this.BeBuffRules);
            this.superTabControlPanel3.Dock = DockStyle.Fill;
            this.superTabControlPanel3.Location = new Point(0, 0);
            this.superTabControlPanel3.Name = "superTabControlPanel3";
            this.superTabControlPanel3.Size = new Size(0x214, 0x133);
            this.superTabControlPanel3.TabIndex = 0;
            this.superTabControlPanel3.TabItem = this.TabBuffs;
            this.BeBuffRules.AccessibleRole = AccessibleRole.Outline;
            this.BeBuffRules.AllowDrop = true;
            this.BeBuffRules.BackColor = SystemColors.Window;
            this.BeBuffRules.BackgroundStyle.Class = "TreeBorderKey";
            this.BeBuffRules.BackgroundStyle.CornerType = eCornerType.Square;
            this.BeBuffRules.Dock = DockStyle.Top;
            this.BeBuffRules.Location = new Point(0, 0);
            this.BeBuffRules.Name = "BeBuffRules";
            this.BeBuffRules.NodesConnector = this.nodeConnector2;
            this.BeBuffRules.NodeStyle = this.elementStyle2;
            this.BeBuffRules.PathSeparator = ";";
            this.BeBuffRules.Size = new Size(0x214, 0x116);
            this.BeBuffRules.Styles.Add(this.elementStyle2);
            this.BeBuffRules.TabIndex = 1;
            this.BeBuffRules.Text = "advTree1";
            this.BeBuffRules.NodeDragFeedback += new TreeDragFeedbackEventHander(this.BeBuffRules_NodeDragFeedback);
            this.BeBuffRules.NodeClick += new TreeNodeMouseEventHandler(this.BeBuffRulesNodeClick);
            this.BeBuffRules.NodeDoubleClick += new TreeNodeMouseEventHandler(this.BeBuffRulesNodeDoubleClick);
            this.nodeConnector2.LineColor = SystemColors.ControlText;
            this.elementStyle2.Class = "";
            this.elementStyle2.CornerType = eCornerType.Square;
            this.elementStyle2.Name = "elementStyle2";
            this.elementStyle2.TextColor = SystemColors.ControlText;
            this.TabBuffs.AttachedControl = this.superTabControlPanel3;
            this.TabBuffs.GlobalItem = false;
            this.TabBuffs.Name = "TabBuffs";
            this.TabBuffs.Text = "Buffs";
            this.superTabControlPanel4.Controls.Add(this.BeRestRules);
            this.superTabControlPanel4.Dock = DockStyle.Fill;
            this.superTabControlPanel4.Location = new Point(0, 0);
            this.superTabControlPanel4.Name = "superTabControlPanel4";
            this.superTabControlPanel4.Size = new Size(0x214, 0x133);
            this.superTabControlPanel4.TabIndex = 0;
            this.superTabControlPanel4.TabItem = this.TabRest;
            this.BeRestRules.AccessibleRole = AccessibleRole.Outline;
            this.BeRestRules.AllowDrop = true;
            this.BeRestRules.BackColor = SystemColors.Window;
            this.BeRestRules.BackgroundStyle.Class = "TreeBorderKey";
            this.BeRestRules.BackgroundStyle.CornerType = eCornerType.Square;
            this.BeRestRules.Dock = DockStyle.Top;
            this.BeRestRules.Location = new Point(0, 0);
            this.BeRestRules.Name = "BeRestRules";
            this.BeRestRules.NodesConnector = this.nodeConnector4;
            this.BeRestRules.NodeStyle = this.elementStyle4;
            this.BeRestRules.PathSeparator = ";";
            this.BeRestRules.Size = new Size(0x214, 0x116);
            this.BeRestRules.Styles.Add(this.elementStyle4);
            this.BeRestRules.TabIndex = 1;
            this.BeRestRules.Text = "advTree1";
            this.BeRestRules.NodeDragFeedback += new TreeDragFeedbackEventHander(this.BeRestRules_NodeDragFeedback);
            this.BeRestRules.NodeClick += new TreeNodeMouseEventHandler(this.BeRestRulesNodeClick);
            this.BeRestRules.NodeDoubleClick += new TreeNodeMouseEventHandler(this.BeRestRulesNodeDoubleClick);
            this.nodeConnector4.LineColor = SystemColors.ControlText;
            this.elementStyle4.Class = "";
            this.elementStyle4.CornerType = eCornerType.Square;
            this.elementStyle4.Name = "elementStyle4";
            this.elementStyle4.TextColor = SystemColors.ControlText;
            this.TabRest.AttachedControl = this.superTabControlPanel4;
            this.TabRest.GlobalItem = false;
            this.TabRest.Name = "TabRest";
            this.TabRest.Text = "Rest";
            this.superTabControlPanel2.Controls.Add(this.BeComRules);
            this.superTabControlPanel2.Dock = DockStyle.Fill;
            this.superTabControlPanel2.Location = new Point(0, 0);
            this.superTabControlPanel2.Name = "superTabControlPanel2";
            this.superTabControlPanel2.Size = new Size(0x214, 0x133);
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
            this.TabCombat.Text = "Combat";
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
            this.BePullRules.Text = "advTree1";
            this.BePullRules.NodeDragFeedback += new TreeDragFeedbackEventHander(this.BePullRules_NodeDragFeedback);
            this.BePullRules.NodeClick += new TreeNodeMouseEventHandler(this.BePullRulesNodeClick);
            this.BePullRules.NodeDoubleClick += new TreeNodeMouseEventHandler(this.BePullRulesNodeDoubleClick);
            this.nodeConnector3.LineColor = SystemColors.ControlText;
            this.elementStyle3.Class = "";
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
            this.BeSaveBeheavior.Click += new EventHandler(this.BeSaveBeheaviorClick);
            this.BeBarRuleModifier.AntiAlias = true;
            BaseItem[] itemArray3 = new BaseItem[] { this.BeComAddRule, this.labelItem1, this.BtnAddScript, this.labelItem4, this.BeComEditRule, this.labelItem2, this.BeComDeleteRule };
            this.BeBarRuleModifier.Items.AddRange(itemArray3);
            this.BeBarRuleModifier.Location = new Point(3, 0x1a0);
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
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.FromArgb(0xdf, 0xe9, 0xf5);
            base.ClientSize = new Size(540, 0x239);
            base.Controls.Add(this.groupPanel4);
            base.Controls.Add(this.BeGMisc);
            base.Controls.Add(this.BeTabs);
            base.Controls.Add(this.BeBarRuleModifier);
            this.DoubleBuffered = true;
            base.Name = "BehaviorForm";
            base.FormClosing += new FormClosingEventHandler(this.BehaviorFormFormClosing);
            base.Load += new EventHandler(this.BehaviorFormLoad);
            this.groupPanel4.ResumeLayout(false);
            this.BeGMisc.ResumeLayout(false);
            this.BeGlobalCooldown.EndInit();
            this.BePrePullDistance.EndInit();
            this.BePullDistance.EndInit();
            this.BeCombatDistance.EndInit();
            ((ISupportInitialize) this.BeTabs).EndInit();
            this.BeTabs.ResumeLayout(false);
            this.superTabControlPanel5.ResumeLayout(false);
            this.BePrePullRules.EndInit();
            this.superTabControlPanel3.ResumeLayout(false);
            this.BeBuffRules.EndInit();
            this.superTabControlPanel4.ResumeLayout(false);
            this.BeRestRules.EndInit();
            this.superTabControlPanel2.ResumeLayout(false);
            this.BeComRules.EndInit();
            this.superTabControlPanel1.ResumeLayout(false);
            this.BePullRules.EndInit();
            this.BeBarRuleModifier.EndInit();
            base.ResumeLayout(false);
        }

        private void LoadBehavior()
        {
            this._behavior = new BehaviorController();
            this.ClearTree(this.BeComRules);
            this.ClearTree(this.BePullRules);
            this.ClearTree(this.BeRestRules);
            this.ClearTree(this.BeBuffRules);
            this.ClearTree(this.BePrePullRules);
            this._behavior.Load(string.Concat(new object[] { this._ourDirectory, @"\Behaviors\", this.BeTBSelectBehavior.SelectedItem, ".xml" }));
            this._behavior.CombatController.GetRules.Sort();
            this._behavior.RestController.GetRules.Sort();
            this._behavior.PullController.GetRules.Sort();
            this._behavior.BuffController.GetRules.Sort();
            this._behavior.PrePullController.GetRules.Sort();
            this.BeCombatDistance.Value = this._behavior.CombatDistance;
            this.BePullDistance.Value = this._behavior.PullDistance;
            this.BePrePullDistance.Value = this._behavior.PrePullDistance;
            this.BeEnableAutoAttack.Checked = this._behavior.UseAutoAttack;
            this.BeSendPet.Checked = this._behavior.SendPet;
            this.BeGlobalCooldown.Value = this._behavior.GlobalCooldown;
            foreach (Rule rule in this._behavior.CombatController.GetRules)
            {
                this.AddCondition(rule, this.BeComRules);
            }
            foreach (Rule rule2 in this._behavior.PullController.GetRules)
            {
                this.AddCondition(rule2, this.BePullRules);
            }
            foreach (Rule rule3 in this._behavior.RestController.GetRules)
            {
                this.AddCondition(rule3, this.BeRestRules);
            }
            foreach (Rule rule4 in this._behavior.BuffController.GetRules)
            {
                this.AddCondition(rule4, this.BeBuffRules);
            }
            foreach (Rule rule5 in this._behavior.PrePullController.GetRules)
            {
                this.AddCondition(rule5, this.BePrePullRules);
            }
            this.BeTabs.Enabled = true;
            this.BeBarRuleModifier.Enabled = true;
            this.BeGMisc.Enabled = true;
            PveBehaviorSettings.LoadedBeharvior = this.BeTBSelectBehavior.SelectedItem.ToString();
            PveBehaviorSettings.SaveSettings();
        }

        private void LoadBehaviors()
        {
            if (Directory.Exists(this._ourDirectory + @"\Behaviors"))
            {
                this.BeTBSelectBehavior.Items.Clear();
                string[] files = Directory.GetFiles(this._ourDirectory + @"\Behaviors", "*xml");
                int index = 0;
                while (true)
                {
                    if (index >= files.Length)
                    {
                        if (this.BeTBSelectBehavior.Items.Contains(PveBehaviorSettings.LoadedBeharvior))
                        {
                            this.BeTBSelectBehavior.SelectedIndex = this.BeTBSelectBehavior.FindStringExact(PveBehaviorSettings.LoadedBeharvior);
                        }
                        break;
                    }
                    string path = files[index];
                    this.BeTBSelectBehavior.Items.Add(Path.GetFileNameWithoutExtension(path));
                    index++;
                }
            }
            if (string.IsNullOrEmpty(this._behavior.Name))
            {
                this.BeTabs.Enabled = false;
                this.BeBarRuleModifier.Enabled = false;
                this.BeGMisc.Enabled = false;
            }
        }

        private void SaveCurrentBehavior()
        {
            if (this._behavior.Name != string.Empty)
            {
                this._behavior.ResetControllers();
                foreach (Node node in this.BeComRules.Nodes)
                {
                    Rule tag = (Rule) node.Tag;
                    tag.Priority = node.Index;
                    this._behavior.CombatController.AddRule(tag);
                }
                foreach (Node node2 in this.BeBuffRules.Nodes)
                {
                    Rule tag = (Rule) node2.Tag;
                    tag.Priority = node2.Index;
                    this._behavior.BuffController.AddRule(tag);
                }
                foreach (Node node3 in this.BePullRules.Nodes)
                {
                    Rule tag = (Rule) node3.Tag;
                    tag.Priority = node3.Index;
                    this._behavior.PullController.AddRule(tag);
                }
                foreach (Node node4 in this.BeRestRules.Nodes)
                {
                    Rule tag = (Rule) node4.Tag;
                    tag.Priority = node4.Index;
                    this._behavior.RestController.AddRule(tag);
                }
                foreach (Node node5 in this.BePrePullRules.Nodes)
                {
                    Rule tag = (Rule) node5.Tag;
                    tag.Priority = node5.Index;
                    this._behavior.PrePullController.AddRule(tag);
                }
                this._behavior.CombatDistance = this.BeCombatDistance.Value;
                this._behavior.PullDistance = this.BePullDistance.Value;
                this._behavior.PrePullDistance = this.BePrePullDistance.Value;
                this._behavior.UseAutoAttack = this.BeEnableAutoAttack.Checked;
                this._behavior.SendPet = this.BeSendPet.Checked;
                this._behavior.GlobalCooldown = this.BeGlobalCooldown.Value;
                this._behavior.Save();
            }
        }
    }
}

