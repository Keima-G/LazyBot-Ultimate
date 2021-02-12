namespace LazyEvo.PVEBehavior
{
    using DevComponents.AdvTree;
    using DevComponents.DotNetBar;
    using DevComponents.DotNetBar.Controls;
    using DevComponents.Editors;
    using LazyEvo.Forms.Helpers;
    using LazyEvo.PVEBehavior.Behavior;
    using LazyEvo.PVEBehavior.Behavior.Conditions;
    using LazyLib;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    internal class RuleEditor : Office2007RibbonForm
    {
        private const int WM_NCHITTEST = 0x84;
        private const int HTCLIENT = 1;
        private const int HTCAPTION = 2;
        private readonly bool _targetVisible;
        public LazyEvo.PVEBehavior.Behavior.Rule Rule;
        public bool Save;
        private AbstractCondition selected;
        private IContainer components;
        private Bar bar1;
        private ButtonItem BtnCancel;
        private LabelItem labelItem2;
        private StyleManager styleManager1;
        private ButtonItem AddCHealthPower;
        private ButtonItem BuffDetection;
        private DevComponents.AdvTree.AdvTree AllConditions;
        private NodeConnector nodeConnector1;
        private ElementStyle elementStyle1;
        private DevComponents.AdvTree.AdvTree ConditionEditor;
        private ProgressBarX progressBarX1;
        private Node node16;
        private Node node17;
        private Node node18;
        private Cell cell17;
        private Node node20;
        private Node node21;
        private Node node22;
        private Node node24;
        private Cell cell18;
        private Node node25;
        private Node node26;
        private Node node28;
        private Node node30;
        private ElementStyle elementStyle4;
        private NodeConnector nodeConnector2;
        private ElementStyle elementStyle3;
        private Node node15;
        private Node node19;
        private Node node23;
        private Node node27;
        private Node node29;
        private Node node1;
        private GroupPanel groupPanel1;
        private GroupPanel groupPanel2;
        private GroupPanel groupPanel3;
        private GroupPanel groupPanel4;
        private CheckBoxX CBCastSpell;
        private CheckBoxX CBSendKey;
        private LabelX labelX1;
        private TextBoxX TBKey;
        private LabelX labelX3;
        private LabelX labelX2;
        internal ComboBoxEx ComBBar;
        private LabelX labelX4;
        internal ComboBoxEx ComBSpecail;
        private ComboItem comboItem5;
        private ComboItem comboItem6;
        private ComboItem comboItem7;
        private ComboItem comboItem8;
        private ComboItem comboItem3;
        private ComboItem comboItem4;
        private ComboItem comboItem9;
        private ComboItem comboItem10;
        private ComboItem comboItem11;
        private ComboItem comboItem12;
        private ComboItem comboItem13;
        private TextBoxX TBKeyName;
        private GroupPanel GPTarget;
        private RadioButton RBEnemy;
        private RadioButton RBPet;
        private RadioButton RBSelf;
        private RadioButton RBNone;
        private LabelX labelX5;
        private LabelX labelX6;
        private GroupPanel groupPanel6;
        private TextBoxX TBRuleName;
        private LabelX labelX7;
        private ButtonX BtnSave;
        private LabelItem labelItem1;
        private LabelItem labelItem3;
        private SwitchButtonItem SWMatchConditions;
        private ButtonX BCancel;
        private TextBoxX TBSpellName;
        private SuperTooltip superTooltip1;
        private ButtonItem BtnRemoveCon;
        private LabelItem labelItem4;
        private ButtonItem CombatCount;
        private ButtonItem DistanceToTarget;
        private LabelItem labelItem5;
        private ButtonItem SoulShardCount;
        private ButtonItem ComboPointsCondition;
        private ButtonItem MageWaterCondition;
        private ButtonItem MageFoodCondition;
        private LabelItem labelItem6;
        private LabelItem labelItem8;
        private ButtonItem HealthStoneCount;
        private IntegerInput ComBTimes;
        private LabelX labelX8;
        private ButtonItem HasTempEnchant;
        private ButtonItem RuneCondition;
        private ButtonItem PotentialAdds;
        private ButtonItem Functions;
        private LabelItem Othershj;
        private ButtonItem Ticker;
        private ButtonItem HasPet;
        private RadioButton RBUnchanged;
        private ButtonItem BtnSpellDetection;

        public RuleEditor(LazyEvo.PVEBehavior.Behavior.Rule rule, bool targetVisible)
        {
            this.InitializeComponent();
            this.Rule = rule;
            Geometry.GeometryFromString(GeomertrySettings.RuleEditor, this);
            this._targetVisible = targetVisible;
        }

        private void AddCHealthPower_Click(object sender, EventArgs e)
        {
            this.AddCondition("Health/Power", new HealthPowerCondition());
        }

        private void AddCondition(string name, AbstractCondition condition)
        {
            Node node = new Node {
                Text = name,
                Tag = condition
            };
            this.AddNode(node);
        }

        private void AddNode(Node node)
        {
            this.AllConditions.BeginUpdate();
            this.AllConditions.Nodes.Add(node);
            this.AllConditions.EndUpdate();
        }

        private void AllConditions_NodeClick(object sender, TreeNodeMouseEventArgs e)
        {
            Node node = e.Node;
            if (node.Tag is AbstractCondition)
            {
                this.AllConditions.BeginUpdate();
                this.selected = (AbstractCondition) node.Tag;
                this.ConditionEditor.Nodes.Clear();
                foreach (Node node2 in this.selected.GetNodes())
                {
                    this.ConditionEditor.Nodes.Add(node2);
                }
                this.AllConditions.EndUpdate();
            }
        }

        private void BCancel_Click(object sender, EventArgs e)
        {
            this.Save = false;
            base.Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
        }

        private void BtnRemoveCon_Click(object sender, EventArgs e)
        {
            if (this.AllConditions.SelectedNode != null)
            {
                this.AllConditions.Nodes.Remove(this.AllConditions.SelectedNode);
                this.ConditionEditor.Nodes.Clear();
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (this.CBCastSpell.Checked)
            {
                if (this.TBSpellName.Text == "")
                {
                    this.superTooltip1.SetSuperTooltip(this.TBSpellName, new SuperTooltipInfo("", "", "You need to type a spell name.", null, null, eTooltipColor.Gray));
                    this.superTooltip1.ShowTooltip(this.TBSpellName);
                    return;
                }
                this.Rule.Action = new ActionSpell(this.TBSpellName.Text);
            }
            if (this.CBSendKey.Checked)
            {
                if (this.TBKeyName.Text == "")
                {
                    this.superTooltip1.SetSuperTooltip(this.TBKeyName, new SuperTooltipInfo("", "", "You need to type a key name.", null, null, eTooltipColor.Gray));
                    this.superTooltip1.ShowTooltip(this.TBKeyName);
                    return;
                }
                if (this.TBKey.Text == "")
                {
                    this.superTooltip1.SetSuperTooltip(this.TBKeyName, new SuperTooltipInfo("", "", "You need to type a key.", null, null, eTooltipColor.Gray));
                    this.superTooltip1.ShowTooltip(this.TBKeyName);
                    return;
                }
                this.Rule.Action = new ActionKey(this.TBKeyName.Text, this.ComBBar.SelectedItem.ToString(), this.TBKey.Text, this.ComBSpecail.SelectedItem.ToString(), this.ComBTimes.Value);
            }
            if (this.TBRuleName.Text == "")
            {
                this.superTooltip1.SetSuperTooltip(this.TBSpellName, new SuperTooltipInfo("", "", "Please give the rule a name.", null, null, eTooltipColor.Gray));
                this.superTooltip1.ShowTooltip(this.TBSpellName);
            }
            else if (this.AllConditions.Nodes.Count == 0)
            {
                this.superTooltip1.SetSuperTooltip(this.TBSpellName, new SuperTooltipInfo("", "", "Please create one condition", null, null, eTooltipColor.Gray));
                this.superTooltip1.ShowTooltip(this.TBSpellName);
            }
            else
            {
                this.Rule.MatchAll = this.SWMatchConditions.Value;
                this.Rule.Name = this.TBRuleName.Text;
                this.Rule.ClearConditions();
                foreach (Node node in this.AllConditions.Nodes)
                {
                    this.Rule.AddCondition((AbstractCondition) node.Tag);
                }
                if (this.RBEnemy.Checked)
                {
                    this.Rule.ShouldTarget = Target.Enemy;
                }
                if (this.RBNone.Checked)
                {
                    this.Rule.ShouldTarget = Target.None;
                }
                if (this.RBPet.Checked)
                {
                    this.Rule.ShouldTarget = Target.Pet;
                }
                if (this.RBSelf.Checked)
                {
                    this.Rule.ShouldTarget = Target.Self;
                }
                if (this.RBUnchanged.Checked)
                {
                    this.Rule.ShouldTarget = Target.Unchanged;
                }
                this.Save = true;
                base.Close();
            }
        }

        private void BtnSpellDetection_Click(object sender, EventArgs e)
        {
            this.AddCondition(this.BtnSpellDetection.Text, new SpellCondition());
        }

        private void BuffDetection_Click(object sender, EventArgs e)
        {
            this.AddCondition(this.BuffDetection.Text, new BuffCondition());
        }

        private void CBCastSpell_CheckedChanged(object sender, EventArgs e)
        {
            if (!this.CBCastSpell.Checked)
            {
                this.CBSendKey.Checked = true;
                this.CBCastSpell.Checked = false;
            }
            else
            {
                this.TBKeyName.Enabled = false;
                this.ComBTimes.Enabled = false;
                this.ComBBar.Enabled = false;
                this.ComBSpecail.Enabled = false;
                this.TBSpellName.Enabled = true;
                this.TBKey.Enabled = false;
                this.CBSendKey.Checked = false;
            }
        }

        private void CBSendKey_CheckedChanged(object sender, EventArgs e)
        {
            if (!this.CBSendKey.Checked)
            {
                this.CBCastSpell.Checked = true;
                this.CBSendKey.Checked = false;
            }
            else
            {
                this.TBKeyName.Enabled = true;
                this.ComBTimes.Enabled = true;
                this.ComBBar.Enabled = true;
                this.ComBSpecail.Enabled = true;
                this.TBSpellName.Enabled = false;
                this.TBKey.Enabled = true;
                this.CBCastSpell.Checked = false;
            }
        }

        private void CombatCount_Click(object sender, EventArgs e)
        {
            this.AddCondition(this.CombatCount.Text, new CombatCountCondition());
        }

        private void ComboPointsCondition_Click(object sender, EventArgs e)
        {
            this.AddCondition(this.ComboPointsCondition.Text, new LazyEvo.PVEBehavior.Behavior.Conditions.ComboPointsCondition());
        }

        private void ConditionEditor_NodeClick(object sender, TreeNodeMouseEventArgs e)
        {
            if (this.selected != null)
            {
                this.selected.NodeClick(e.Node);
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

        private void DistanceToTarget_Click(object sender, EventArgs e)
        {
            this.AddCondition(this.DistanceToTarget.Text, new LazyEvo.PVEBehavior.Behavior.Conditions.DistanceToTarget());
        }

        private void Functions_Click(object sender, EventArgs e)
        {
            this.AddCondition(this.Functions.Text, new FunctionsCondition());
        }

        private void HasPet_Click(object sender, EventArgs e)
        {
            this.AddCondition(this.HasPet.Text, new PetCondition());
        }

        private void HasTempEnchant_Click(object sender, EventArgs e)
        {
            this.AddCondition(this.HasTempEnchant.Text, new TempEnchantCondition());
        }

        private void HealthStoneCount_Click(object sender, EventArgs e)
        {
            this.AddCondition(this.HealthStoneCount.Text, new LazyEvo.PVEBehavior.Behavior.Conditions.HealthStoneCount());
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            ComponentResourceManager manager = new ComponentResourceManager(typeof(RuleEditor));
            this.bar1 = new Bar();
            this.BtnCancel = new ButtonItem();
            this.labelItem2 = new LabelItem();
            this.AddCHealthPower = new ButtonItem();
            this.BuffDetection = new ButtonItem();
            this.BtnSpellDetection = new ButtonItem();
            this.Othershj = new LabelItem();
            this.Functions = new ButtonItem();
            this.CombatCount = new ButtonItem();
            this.DistanceToTarget = new ButtonItem();
            this.HasTempEnchant = new ButtonItem();
            this.PotentialAdds = new ButtonItem();
            this.Ticker = new ButtonItem();
            this.labelItem5 = new LabelItem();
            this.HasPet = new ButtonItem();
            this.ComboPointsCondition = new ButtonItem();
            this.RuneCondition = new ButtonItem();
            this.labelItem6 = new LabelItem();
            this.SoulShardCount = new ButtonItem();
            this.HealthStoneCount = new ButtonItem();
            this.labelItem8 = new LabelItem();
            this.MageWaterCondition = new ButtonItem();
            this.MageFoodCondition = new ButtonItem();
            this.labelItem1 = new LabelItem();
            this.BtnRemoveCon = new ButtonItem();
            this.labelItem4 = new LabelItem();
            this.labelItem3 = new LabelItem();
            this.SWMatchConditions = new SwitchButtonItem();
            this.styleManager1 = new StyleManager(this.components);
            this.AllConditions = new DevComponents.AdvTree.AdvTree();
            this.groupPanel1 = new GroupPanel();
            this.nodeConnector1 = new NodeConnector();
            this.elementStyle1 = new ElementStyle();
            this.ConditionEditor = new DevComponents.AdvTree.AdvTree();
            this.nodeConnector2 = new NodeConnector();
            this.elementStyle3 = new ElementStyle();
            this.elementStyle4 = new ElementStyle();
            this.progressBarX1 = new ProgressBarX();
            this.node16 = new Node();
            this.node17 = new Node();
            this.node18 = new Node();
            this.cell17 = new Cell();
            this.node20 = new Node();
            this.node21 = new Node();
            this.node22 = new Node();
            this.node24 = new Node();
            this.cell18 = new Cell();
            this.node25 = new Node();
            this.node26 = new Node();
            this.node28 = new Node();
            this.node30 = new Node();
            this.node29 = new Node();
            this.node1 = new Node();
            this.node27 = new Node();
            this.node23 = new Node();
            this.node19 = new Node();
            this.node15 = new Node();
            this.groupPanel2 = new GroupPanel();
            this.groupPanel3 = new GroupPanel();
            this.groupPanel4 = new GroupPanel();
            this.ComBTimes = new IntegerInput();
            this.labelX8 = new LabelX();
            this.TBSpellName = new TextBoxX();
            this.labelX5 = new LabelX();
            this.TBKeyName = new TextBoxX();
            this.labelX4 = new LabelX();
            this.ComBSpecail = new ComboBoxEx();
            this.comboItem5 = new ComboItem();
            this.comboItem6 = new ComboItem();
            this.comboItem7 = new ComboItem();
            this.comboItem8 = new ComboItem();
            this.TBKey = new TextBoxX();
            this.labelX3 = new LabelX();
            this.labelX2 = new LabelX();
            this.ComBBar = new ComboBoxEx();
            this.comboItem13 = new ComboItem();
            this.comboItem3 = new ComboItem();
            this.comboItem4 = new ComboItem();
            this.comboItem9 = new ComboItem();
            this.comboItem10 = new ComboItem();
            this.comboItem11 = new ComboItem();
            this.comboItem12 = new ComboItem();
            this.labelX1 = new LabelX();
            this.CBSendKey = new CheckBoxX();
            this.CBCastSpell = new CheckBoxX();
            this.GPTarget = new GroupPanel();
            this.RBUnchanged = new RadioButton();
            this.labelX6 = new LabelX();
            this.RBEnemy = new RadioButton();
            this.RBNone = new RadioButton();
            this.RBPet = new RadioButton();
            this.RBSelf = new RadioButton();
            this.groupPanel6 = new GroupPanel();
            this.TBRuleName = new TextBoxX();
            this.labelX7 = new LabelX();
            this.BtnSave = new ButtonX();
            this.BCancel = new ButtonX();
            this.superTooltip1 = new SuperTooltip();
            this.bar1.BeginInit();
            this.AllConditions.BeginInit();
            this.AllConditions.SuspendLayout();
            this.ConditionEditor.BeginInit();
            this.groupPanel2.SuspendLayout();
            this.groupPanel3.SuspendLayout();
            this.groupPanel4.SuspendLayout();
            this.ComBTimes.BeginInit();
            this.GPTarget.SuspendLayout();
            this.groupPanel6.SuspendLayout();
            base.SuspendLayout();
            this.bar1.AntiAlias = true;
            BaseItem[] items = new BaseItem[] { this.BtnCancel, this.labelItem1, this.BtnRemoveCon, this.labelItem4, this.labelItem3, this.SWMatchConditions };
            this.bar1.Items.AddRange(items);
            this.bar1.Location = new Point(10, 13);
            this.bar1.Name = "bar1";
            this.bar1.Size = new Size(0x254, 0x19);
            this.bar1.Stretch = true;
            this.bar1.Style = eDotNetBarStyle.StyleManagerControlled;
            this.bar1.TabIndex = 3;
            this.bar1.TabStop = false;
            this.bar1.Text = "bar1";
            this.BtnCancel.Name = "BtnCancel";
            BaseItem[] itemArray2 = new BaseItem[] { this.labelItem2, this.AddCHealthPower, this.BuffDetection, this.BtnSpellDetection, this.Othershj, this.Functions, this.CombatCount, this.DistanceToTarget, this.HasTempEnchant };
            itemArray2[9] = this.PotentialAdds;
            itemArray2[10] = this.Ticker;
            itemArray2[11] = this.labelItem5;
            itemArray2[12] = this.HasPet;
            itemArray2[13] = this.ComboPointsCondition;
            itemArray2[14] = this.RuneCondition;
            itemArray2[15] = this.labelItem6;
            itemArray2[0x10] = this.SoulShardCount;
            itemArray2[0x11] = this.HealthStoneCount;
            itemArray2[0x12] = this.labelItem8;
            itemArray2[0x13] = this.MageWaterCondition;
            itemArray2[20] = this.MageFoodCondition;
            this.BtnCancel.SubItems.AddRange(itemArray2);
            this.BtnCancel.Text = "Add condition";
            this.BtnCancel.Click += new EventHandler(this.BtnCancel_Click);
            this.labelItem2.BackColor = Color.FromArgb(0xdd, 0xe7, 0xee);
            this.labelItem2.BorderSide = eBorderSide.Bottom;
            this.labelItem2.BorderType = eBorderType.SingleLine;
            this.labelItem2.ForeColor = Color.FromArgb(0, 0x15, 110);
            this.labelItem2.Name = "labelItem2";
            this.labelItem2.PaddingBottom = 1;
            this.labelItem2.PaddingLeft = 10;
            this.labelItem2.PaddingTop = 1;
            this.labelItem2.SingleLineColor = Color.FromArgb(0xc5, 0xc5, 0xc5);
            this.labelItem2.Text = "General";
            this.AddCHealthPower.Name = "AddCHealthPower";
            this.AddCHealthPower.Text = "Health / Power";
            this.AddCHealthPower.Click += new EventHandler(this.AddCHealthPower_Click);
            this.BuffDetection.Name = "BuffDetection";
            this.BuffDetection.Text = "Buff Detection";
            this.BuffDetection.Click += new EventHandler(this.BuffDetection_Click);
            this.BtnSpellDetection.Name = "BtnSpellDetection";
            this.BtnSpellDetection.Text = "Spell Detection";
            this.BtnSpellDetection.Click += new EventHandler(this.BtnSpellDetection_Click);
            this.Othershj.BackColor = Color.FromArgb(0xdd, 0xe7, 0xee);
            this.Othershj.BorderSide = eBorderSide.Bottom;
            this.Othershj.BorderType = eBorderType.SingleLine;
            this.Othershj.ForeColor = Color.FromArgb(0, 0x15, 110);
            this.Othershj.Name = "Othershj";
            this.Othershj.PaddingBottom = 1;
            this.Othershj.PaddingLeft = 10;
            this.Othershj.PaddingTop = 1;
            this.Othershj.SingleLineColor = Color.FromArgb(0xc5, 0xc5, 0xc5);
            this.Othershj.Text = "Others";
            this.Functions.Name = "Functions";
            this.Functions.Text = "Functions";
            this.Functions.Click += new EventHandler(this.Functions_Click);
            this.CombatCount.Name = "CombatCount";
            this.CombatCount.Text = "Combat Count";
            this.CombatCount.Click += new EventHandler(this.CombatCount_Click);
            this.DistanceToTarget.Name = "DistanceToTarget";
            this.DistanceToTarget.Text = "Distance To Target";
            this.DistanceToTarget.Click += new EventHandler(this.DistanceToTarget_Click);
            this.HasTempEnchant.Name = "HasTempEnchant";
            this.HasTempEnchant.Text = "Has temporary enchant";
            this.HasTempEnchant.Click += new EventHandler(this.HasTempEnchant_Click);
            this.PotentialAdds.Name = "PotentialAdds";
            this.PotentialAdds.Text = "Potential Mobs Pulled";
            this.PotentialAdds.Click += new EventHandler(this.PotentialAddsCondition_Click);
            this.Ticker.Name = "Ticker";
            this.Ticker.Text = "Ticker";
            this.Ticker.Click += new EventHandler(this.Ticker_Click);
            this.labelItem5.BackColor = Color.FromArgb(0xdd, 0xe7, 0xee);
            this.labelItem5.BorderSide = eBorderSide.Bottom;
            this.labelItem5.BorderType = eBorderType.SingleLine;
            this.labelItem5.ForeColor = Color.FromArgb(0, 0x15, 110);
            this.labelItem5.Name = "labelItem5";
            this.labelItem5.PaddingBottom = 1;
            this.labelItem5.PaddingLeft = 10;
            this.labelItem5.PaddingTop = 1;
            this.labelItem5.SingleLineColor = Color.FromArgb(0xc5, 0xc5, 0xc5);
            this.labelItem5.Text = "Class functions:";
            this.HasPet.Name = "HasPet";
            this.HasPet.Text = "Has Pet";
            this.HasPet.Click += new EventHandler(this.HasPet_Click);
            this.ComboPointsCondition.Name = "ComboPointsCondition";
            this.ComboPointsCondition.Text = "Combo Points";
            this.ComboPointsCondition.Click += new EventHandler(this.ComboPointsCondition_Click);
            this.RuneCondition.Name = "RuneCondition";
            this.RuneCondition.Text = "Rune Condition";
            this.RuneCondition.Click += new EventHandler(this.RuneCondition_Click);
            this.labelItem6.BackColor = Color.FromArgb(0xdd, 0xe7, 0xee);
            this.labelItem6.BorderSide = eBorderSide.Bottom;
            this.labelItem6.BorderType = eBorderType.SingleLine;
            this.labelItem6.ForeColor = Color.FromArgb(0, 0x15, 110);
            this.labelItem6.Name = "labelItem6";
            this.labelItem6.PaddingBottom = 1;
            this.labelItem6.PaddingLeft = 10;
            this.labelItem6.PaddingTop = 1;
            this.labelItem6.SingleLineColor = Color.FromArgb(0xc5, 0xc5, 0xc5);
            this.labelItem6.Text = "Warlock";
            this.SoulShardCount.Name = "SoulShardCount";
            this.SoulShardCount.Text = "Soul Shard Count";
            this.SoulShardCount.Click += new EventHandler(this.SoulShardCount_Click);
            this.HealthStoneCount.Name = "HealthStoneCount";
            this.HealthStoneCount.Text = "Healtstone Count";
            this.HealthStoneCount.Click += new EventHandler(this.HealthStoneCount_Click);
            this.labelItem8.BackColor = Color.FromArgb(0xdd, 0xe7, 0xee);
            this.labelItem8.BorderSide = eBorderSide.Bottom;
            this.labelItem8.BorderType = eBorderType.SingleLine;
            this.labelItem8.ForeColor = Color.FromArgb(0, 0x15, 110);
            this.labelItem8.Name = "labelItem8";
            this.labelItem8.PaddingBottom = 1;
            this.labelItem8.PaddingLeft = 10;
            this.labelItem8.PaddingTop = 1;
            this.labelItem8.SingleLineColor = Color.FromArgb(0xc5, 0xc5, 0xc5);
            this.labelItem8.Text = "Mage";
            this.MageWaterCondition.Name = "MageWaterCondition";
            this.MageWaterCondition.Text = "Mage Water";
            this.MageWaterCondition.Click += new EventHandler(this.MageWaterCondition_Click);
            this.MageFoodCondition.Name = "MageFoodCondition";
            this.MageFoodCondition.Text = "Mage Food";
            this.MageFoodCondition.Click += new EventHandler(this.MageFoodCondition_Click);
            this.labelItem1.Name = "labelItem1";
            this.labelItem1.Tag = "                                ";
            this.labelItem1.Text = "                     ";
            this.BtnRemoveCon.Name = "BtnRemoveCon";
            this.BtnRemoveCon.Text = "Remove Condition";
            this.BtnRemoveCon.Click += new EventHandler(this.BtnRemoveCon_Click);
            this.labelItem4.Name = "labelItem4";
            this.labelItem4.Text = "                                           ";
            this.labelItem3.ForeColor = Color.Black;
            this.labelItem3.Name = "labelItem3";
            this.labelItem3.Text = "Match conditions:";
            this.SWMatchConditions.ButtonHeight = 0x16;
            this.SWMatchConditions.Name = "SWMatchConditions";
            this.SWMatchConditions.OffText = "Any";
            this.SWMatchConditions.OnText = "All";
            this.styleManager1.ManagerStyle = eStyle.Windows7Blue;
            this.AllConditions.AccessibleRole = AccessibleRole.Outline;
            this.AllConditions.AllowDrop = true;
            this.AllConditions.BackColor = SystemColors.Window;
            this.AllConditions.BackgroundStyle.Class = "TreeBorderKey";
            this.AllConditions.BackgroundStyle.CornerType = eCornerType.Square;
            this.AllConditions.Controls.Add(this.groupPanel1);
            this.AllConditions.DragDropEnabled = false;
            this.AllConditions.Location = new Point(3, 3);
            this.AllConditions.Name = "AllConditions";
            this.AllConditions.NodesConnector = this.nodeConnector1;
            this.AllConditions.NodeStyle = this.elementStyle1;
            this.AllConditions.PathSeparator = ";";
            this.AllConditions.Size = new Size(0xae, 0x12f);
            this.AllConditions.Styles.Add(this.elementStyle1);
            this.AllConditions.TabIndex = 4;
            this.AllConditions.Text = "advTree1";
            this.AllConditions.NodeClick += new TreeNodeMouseEventHandler(this.AllConditions_NodeClick);
            this.groupPanel1.CanvasColor = SystemColors.Control;
            this.groupPanel1.ColorSchemeStyle = eDotNetBarStyle.Office2007;
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
            this.groupPanel1.TabIndex = 6;
            this.groupPanel1.Text = "groupPanel1";
            this.nodeConnector1.LineColor = SystemColors.ControlText;
            this.elementStyle1.Class = "";
            this.elementStyle1.CornerType = eCornerType.Square;
            this.elementStyle1.Name = "elementStyle1";
            this.elementStyle1.TextColor = SystemColors.ControlText;
            this.ConditionEditor.AllowDrop = true;
            this.ConditionEditor.BackColor = SystemColors.Window;
            this.ConditionEditor.BackgroundStyle.BackColor = Color.White;
            this.ConditionEditor.BackgroundStyle.BackColor2 = Color.MintCream;
            this.ConditionEditor.BackgroundStyle.BackColorGradientAngle = 90;
            this.ConditionEditor.BackgroundStyle.Class = "TreeBorderKey";
            this.ConditionEditor.BackgroundStyle.CornerType = eCornerType.Square;
            this.ConditionEditor.ColorSchemeStyle = eColorSchemeStyle.VS2005;
            this.ConditionEditor.DragDropEnabled = false;
            this.ConditionEditor.Location = new Point(3, 3);
            this.ConditionEditor.Name = "ConditionEditor";
            this.ConditionEditor.NodesConnector = this.nodeConnector2;
            this.ConditionEditor.NodeStyle = this.elementStyle3;
            this.ConditionEditor.PathSeparator = ";";
            this.ConditionEditor.Size = new Size(390, 0x12e);
            this.ConditionEditor.Styles.Add(this.elementStyle3);
            this.ConditionEditor.Styles.Add(this.elementStyle4);
            this.ConditionEditor.TabIndex = 5;
            this.ConditionEditor.Text = "advTree3";
            this.ConditionEditor.NodeClick += new TreeNodeMouseEventHandler(this.ConditionEditor_NodeClick);
            this.nodeConnector2.LineColor = SystemColors.ControlText;
            this.elementStyle3.Class = "";
            this.elementStyle3.CornerType = eCornerType.Square;
            this.elementStyle3.Name = "elementStyle3";
            this.elementStyle3.TextColor = SystemColors.ControlText;
            this.elementStyle4.BackColor = Color.FromArgb(0xa8, 0xd7, 0xff);
            this.elementStyle4.BackColor2 = Color.FromArgb(0x39, 160, 0xff);
            this.elementStyle4.BackColorGradientAngle = 90;
            this.elementStyle4.BorderBottom = eStyleBorderType.Solid;
            this.elementStyle4.BorderBottomWidth = 1;
            this.elementStyle4.BorderColor = Color.FromArgb(0x44, 0x69, 140);
            this.elementStyle4.Class = "";
            this.elementStyle4.CornerType = eCornerType.Square;
            this.elementStyle4.Description = "Blue";
            this.elementStyle4.Name = "elementStyle4";
            this.elementStyle4.PaddingBottom = 1;
            this.elementStyle4.PaddingLeft = 1;
            this.elementStyle4.PaddingRight = 1;
            this.elementStyle4.PaddingTop = 1;
            this.elementStyle4.TextColor = Color.Black;
            this.progressBarX1.BackgroundStyle.Class = "";
            this.progressBarX1.BackgroundStyle.CornerType = eCornerType.Square;
            this.progressBarX1.ColorTable = eProgressBarItemColor.Paused;
            this.progressBarX1.Location = new Point(0x4a, 0x106);
            this.progressBarX1.Name = "progressBarX1";
            this.progressBarX1.Size = new Size(140, 20);
            this.progressBarX1.TabIndex = 4;
            this.progressBarX1.Text = "progressBarX1";
            this.progressBarX1.Value = 0x3f;
            this.node16.CheckBoxThreeState = true;
            this.node16.CheckBoxVisible = true;
            this.node16.CheckState = CheckState.Indeterminate;
            this.node16.Expanded = true;
            this.node16.Name = "node16";
            this.node16.Text = "Option 1 with 3-state";
            this.node17.CheckBoxVisible = true;
            this.node17.Expanded = true;
            this.node17.Name = "node17";
            this.node17.Text = "Option 2";
            this.node18.Cells.Add(this.cell17);
            this.node18.CheckBoxVisible = true;
            this.node18.Expanded = true;
            this.node18.Name = "node18";
            this.node18.Text = "Option 3";
            this.cell17.CheckBoxVisible = true;
            this.cell17.Name = "cell17";
            this.cell17.StyleMouseOver = null;
            this.cell17.Text = "Option 3";
            this.node20.CheckBoxStyle = eCheckBoxStyle.RadioButton;
            this.node20.CheckBoxVisible = true;
            this.node20.Expanded = true;
            this.node20.Name = "node20";
            this.node20.Text = "Option 1";
            this.node21.CheckBoxStyle = eCheckBoxStyle.RadioButton;
            this.node21.CheckBoxVisible = true;
            this.node21.Expanded = true;
            this.node21.Name = "node21";
            this.node21.Text = "Option 2";
            this.node22.CheckBoxStyle = eCheckBoxStyle.RadioButton;
            this.node22.CheckBoxVisible = true;
            this.node22.Expanded = true;
            this.node22.Name = "node22";
            this.node22.Text = "Option 3";
            this.node24.Cells.Add(this.cell18);
            this.node24.Expanded = true;
            this.node24.Name = "node24";
            this.node24.Text = "Multiple images per node";
            this.cell18.Name = "cell18";
            this.cell18.StyleMouseOver = null;
            this.node25.Expanded = true;
            this.node25.ImageAlignment = eCellPartAlignment.FarCenter;
            this.node25.Name = "node25";
            this.node25.Text = "Image/text alignment";
            this.node26.CellPartLayout = eCellPartLayout.Vertical;
            this.node26.Expanded = true;
            this.node26.ImageAlignment = eCellPartAlignment.CenterTop;
            this.node26.Name = "node26";
            this.node26.Text = "Orientation";
            this.node28.Expanded = true;
            this.node28.Name = "node28";
            this.node28.Text = "DotNetBar <a href=\"textmarkup\">text-markup</a> is fully supported";
            this.node30.Expanded = true;
            this.node30.HostedControl = this.progressBarX1;
            this.node30.Name = "node30";
            this.node30.Text = "Progress bar";
            this.node29.Expanded = true;
            this.node29.Name = "node29";
            Node[] nodes = new Node[] { this.node30, this.node1 };
            this.node29.Nodes.AddRange(nodes);
            this.node29.Text = "Windows Forms Control Hosting";
            this.node1.Expanded = true;
            this.node1.Name = "node1";
            this.node1.Text = "node1";
            this.node27.Expanded = true;
            this.node27.Name = "node27";
            Node[] nodeArray2 = new Node[] { this.node28 };
            this.node27.Nodes.AddRange(nodeArray2);
            this.node27.Text = "Text-markup support";
            this.node23.Expanded = true;
            this.node23.Name = "node23";
            Node[] nodeArray3 = new Node[] { this.node24, this.node25, this.node26 };
            this.node23.Nodes.AddRange(nodeArray3);
            this.node23.Text = "Images";
            this.node19.Expanded = true;
            this.node19.Name = "node19";
            Node[] nodeArray4 = new Node[] { this.node20, this.node21, this.node22 };
            this.node19.Nodes.AddRange(nodeArray4);
            this.node19.Text = "Radio-buttons";
            this.node15.Expanded = true;
            this.node15.Name = "node15";
            Node[] nodeArray5 = new Node[] { this.node16, this.node17, this.node18 };
            this.node15.Nodes.AddRange(nodeArray5);
            this.node15.Text = "Check-boxes";
            this.groupPanel2.CanvasColor = SystemColors.Control;
            this.groupPanel2.ColorSchemeStyle = eDotNetBarStyle.Office2007;
            this.groupPanel2.Controls.Add(this.AllConditions);
            this.groupPanel2.Location = new Point(10, 0x2c);
            this.groupPanel2.Name = "groupPanel2";
            this.groupPanel2.Size = new Size(0xba, 0x149);
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
            this.groupPanel2.TabIndex = 6;
            this.groupPanel2.Text = "Conditions";
            this.groupPanel3.CanvasColor = SystemColors.Control;
            this.groupPanel3.ColorSchemeStyle = eDotNetBarStyle.Office2007;
            this.groupPanel3.Controls.Add(this.ConditionEditor);
            this.groupPanel3.Location = new Point(0xca, 0x2c);
            this.groupPanel3.Name = "groupPanel3";
            this.groupPanel3.Size = new Size(0x194, 0x149);
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
            this.groupPanel3.Style.Class = "";
            this.groupPanel3.Style.CornerDiameter = 4;
            this.groupPanel3.Style.CornerType = eCornerType.Rounded;
            this.groupPanel3.Style.TextAlignment = eStyleTextAlignment.Center;
            this.groupPanel3.Style.TextColorSchemePart = eColorSchemePart.PanelText;
            this.groupPanel3.Style.TextLineAlignment = eStyleTextAlignment.Near;
            this.groupPanel3.StyleMouseDown.Class = "";
            this.groupPanel3.StyleMouseDown.CornerType = eCornerType.Square;
            this.groupPanel3.StyleMouseOver.Class = "";
            this.groupPanel3.StyleMouseOver.CornerType = eCornerType.Square;
            this.groupPanel3.TabIndex = 7;
            this.groupPanel3.Text = "Condition options";
            this.groupPanel4.CanvasColor = SystemColors.Control;
            this.groupPanel4.ColorSchemeStyle = eDotNetBarStyle.Office2007;
            this.groupPanel4.Controls.Add(this.ComBTimes);
            this.groupPanel4.Controls.Add(this.labelX8);
            this.groupPanel4.Controls.Add(this.TBSpellName);
            this.groupPanel4.Controls.Add(this.labelX5);
            this.groupPanel4.Controls.Add(this.TBKeyName);
            this.groupPanel4.Controls.Add(this.labelX4);
            this.groupPanel4.Controls.Add(this.ComBSpecail);
            this.groupPanel4.Controls.Add(this.TBKey);
            this.groupPanel4.Controls.Add(this.labelX3);
            this.groupPanel4.Controls.Add(this.labelX2);
            this.groupPanel4.Controls.Add(this.ComBBar);
            this.groupPanel4.Controls.Add(this.labelX1);
            this.groupPanel4.Controls.Add(this.CBSendKey);
            this.groupPanel4.Controls.Add(this.CBCastSpell);
            this.groupPanel4.Location = new Point(10, 380);
            this.groupPanel4.Name = "groupPanel4";
            this.groupPanel4.Size = new Size(0x254, 0x52);
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
            this.groupPanel4.TabIndex = 8;
            this.ComBTimes.BackgroundStyle.Class = "DateTimeInputBackground";
            this.ComBTimes.BackgroundStyle.CornerType = eCornerType.Square;
            this.ComBTimes.ButtonFreeText.Shortcut = eShortcut.F2;
            this.ComBTimes.Location = new Point(520, 50);
            this.ComBTimes.Name = "ComBTimes";
            this.ComBTimes.ShowUpDown = true;
            this.ComBTimes.Size = new Size(0x43, 20);
            this.ComBTimes.TabIndex = 0x3b;
            this.labelX8.BackColor = Color.Transparent;
            this.labelX8.BackgroundStyle.Class = "";
            this.labelX8.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX8.Location = new Point(0x1e4, 0x31);
            this.labelX8.Name = "labelX8";
            this.labelX8.Size = new Size(0x22, 0x17);
            this.labelX8.TabIndex = 0x3a;
            this.labelX8.Text = "Times:";
            this.TBSpellName.Border.Class = "TextBoxBorder";
            this.TBSpellName.Border.CornerType = eCornerType.Square;
            this.TBSpellName.Location = new Point(0x84, 6);
            this.TBSpellName.Name = "TBSpellName";
            this.TBSpellName.Size = new Size(0xb7, 20);
            this.TBSpellName.TabIndex = 0x39;
            this.labelX5.BackColor = Color.Transparent;
            this.labelX5.BackgroundStyle.Class = "";
            this.labelX5.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX5.Location = new Point(3, 0x18);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new Size(0x26, 0x17);
            this.labelX5.TabIndex = 0x38;
            this.labelX5.Text = "<b>Action:</b>";
            this.TBKeyName.Border.Class = "TextBoxBorder";
            this.TBKeyName.Border.CornerType = eCornerType.Square;
            this.TBKeyName.Location = new Point(0x31, 0x34);
            this.TBKeyName.Name = "TBKeyName";
            this.TBKeyName.Size = new Size(0x49, 20);
            this.TBKeyName.TabIndex = 0x37;
            this.labelX4.BackColor = Color.Transparent;
            this.labelX4.BackgroundStyle.Class = "";
            this.labelX4.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX4.Location = new Point(0x15c, 0x31);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new Size(0x33, 0x17);
            this.labelX4.TabIndex = 0x36;
            this.labelX4.Text = "Special:";
            this.ComBSpecail.DisplayMember = "Text";
            this.ComBSpecail.DrawMode = DrawMode.OwnerDrawFixed;
            this.ComBSpecail.DropDownStyle = ComboBoxStyle.DropDownList;
            this.ComBSpecail.FormattingEnabled = true;
            this.ComBSpecail.ItemHeight = 14;
            object[] objArray = new object[] { this.comboItem5, this.comboItem6, this.comboItem7, this.comboItem8 };
            this.ComBSpecail.Items.AddRange(objArray);
            this.ComBSpecail.Location = new Point(0x195, 50);
            this.ComBSpecail.Name = "ComBSpecail";
            this.ComBSpecail.Size = new Size(0x49, 20);
            this.ComBSpecail.Style = eDotNetBarStyle.StyleManagerControlled;
            this.ComBSpecail.TabIndex = 0x35;
            this.comboItem5.Text = "None";
            this.comboItem6.Text = "Shift";
            this.comboItem7.Text = "Ctrl";
            this.comboItem8.Text = "Alt";
            this.TBKey.Border.Class = "TextBoxBorder";
            this.TBKey.Border.CornerType = eCornerType.Square;
            this.TBKey.Location = new Point(0x10d, 0x33);
            this.TBKey.MaxLength = 1;
            this.TBKey.Name = "TBKey";
            this.TBKey.Size = new Size(0x49, 20);
            this.TBKey.TabIndex = 0x34;
            this.TBKey.TextChanged += new EventHandler(this.TBKey_TextChanged);
            this.labelX3.BackColor = Color.Transparent;
            this.labelX3.BackgroundStyle.Class = "";
            this.labelX3.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX3.Location = new Point(0xed, 0x31);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new Size(0x22, 0x17);
            this.labelX3.TabIndex = 0x33;
            this.labelX3.Text = "Key:";
            this.labelX2.BackColor = Color.Transparent;
            this.labelX2.BackgroundStyle.Class = "";
            this.labelX2.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX2.Location = new Point(0x81, 50);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new Size(0x1d, 0x17);
            this.labelX2.TabIndex = 50;
            this.labelX2.Text = "Bar:";
            this.ComBBar.DisplayMember = "Text";
            this.ComBBar.DrawMode = DrawMode.OwnerDrawFixed;
            this.ComBBar.DropDownStyle = ComboBoxStyle.DropDownList;
            this.ComBBar.FormattingEnabled = true;
            this.ComBBar.ItemHeight = 14;
            object[] objArray2 = new object[] { this.comboItem13, this.comboItem3, this.comboItem4, this.comboItem9, this.comboItem10, this.comboItem11, this.comboItem12 };
            this.ComBBar.Items.AddRange(objArray2);
            this.ComBBar.Location = new Point(0x9f, 0x34);
            this.ComBBar.Name = "ComBBar";
            this.ComBBar.Size = new Size(0x49, 20);
            this.ComBBar.Style = eDotNetBarStyle.StyleManagerControlled;
            this.ComBBar.TabIndex = 0x31;
            this.comboItem13.Text = "None";
            this.comboItem3.Text = "1";
            this.comboItem4.Text = "2";
            this.comboItem9.Text = "3";
            this.comboItem10.Text = "4";
            this.comboItem11.Text = "5";
            this.comboItem12.Text = "6";
            this.labelX1.BackColor = Color.Transparent;
            this.labelX1.BackgroundStyle.Class = "";
            this.labelX1.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX1.Location = new Point(2, 0x31);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new Size(0x26, 0x17);
            this.labelX1.TabIndex = 0x2f;
            this.labelX1.Text = "Name:";
            this.CBSendKey.BackColor = Color.Transparent;
            this.CBSendKey.BackgroundStyle.Class = "";
            this.CBSendKey.BackgroundStyle.CornerType = eCornerType.Square;
            this.CBSendKey.Location = new Point(0x35, 0x1a);
            this.CBSendKey.Name = "CBSendKey";
            this.CBSendKey.Size = new Size(80, 0x17);
            this.CBSendKey.Style = eDotNetBarStyle.StyleManagerControlled;
            this.CBSendKey.TabIndex = 1;
            this.CBSendKey.Text = "Send Key";
            this.CBSendKey.CheckedChanged += new EventHandler(this.CBSendKey_CheckedChanged);
            this.CBCastSpell.BackColor = Color.Transparent;
            this.CBCastSpell.BackgroundStyle.Class = "";
            this.CBCastSpell.BackgroundStyle.CornerType = eCornerType.Square;
            this.CBCastSpell.Location = new Point(0x35, 3);
            this.CBCastSpell.Name = "CBCastSpell";
            this.CBCastSpell.Size = new Size(80, 0x17);
            this.CBCastSpell.Style = eDotNetBarStyle.StyleManagerControlled;
            this.CBCastSpell.TabIndex = 0;
            this.CBCastSpell.Text = "Cast spell";
            this.CBCastSpell.CheckedChanged += new EventHandler(this.CBCastSpell_CheckedChanged);
            this.GPTarget.CanvasColor = SystemColors.Control;
            this.GPTarget.ColorSchemeStyle = eDotNetBarStyle.Office2007;
            this.GPTarget.Controls.Add(this.RBUnchanged);
            this.GPTarget.Controls.Add(this.labelX6);
            this.GPTarget.Controls.Add(this.RBEnemy);
            this.GPTarget.Controls.Add(this.RBNone);
            this.GPTarget.Controls.Add(this.RBPet);
            this.GPTarget.Controls.Add(this.RBSelf);
            this.GPTarget.Location = new Point(10, 0x1d4);
            this.GPTarget.Name = "GPTarget";
            this.GPTarget.Size = new Size(0x254, 0x1b);
            this.GPTarget.Style.BackColor2SchemePart = eColorSchemePart.PanelBackground2;
            this.GPTarget.Style.BackColorGradientAngle = 90;
            this.GPTarget.Style.BackColorSchemePart = eColorSchemePart.PanelBackground;
            this.GPTarget.Style.BorderBottom = eStyleBorderType.Solid;
            this.GPTarget.Style.BorderBottomWidth = 1;
            this.GPTarget.Style.BorderColorSchemePart = eColorSchemePart.PanelBorder;
            this.GPTarget.Style.BorderLeft = eStyleBorderType.Solid;
            this.GPTarget.Style.BorderLeftWidth = 1;
            this.GPTarget.Style.BorderRight = eStyleBorderType.Solid;
            this.GPTarget.Style.BorderRightWidth = 1;
            this.GPTarget.Style.BorderTop = eStyleBorderType.Solid;
            this.GPTarget.Style.BorderTopWidth = 1;
            this.GPTarget.Style.Class = "";
            this.GPTarget.Style.CornerDiameter = 4;
            this.GPTarget.Style.CornerType = eCornerType.Rounded;
            this.GPTarget.Style.TextAlignment = eStyleTextAlignment.Center;
            this.GPTarget.Style.TextColorSchemePart = eColorSchemePart.PanelText;
            this.GPTarget.Style.TextLineAlignment = eStyleTextAlignment.Near;
            this.GPTarget.StyleMouseDown.Class = "";
            this.GPTarget.StyleMouseDown.CornerType = eCornerType.Square;
            this.GPTarget.StyleMouseOver.Class = "";
            this.GPTarget.StyleMouseOver.CornerType = eCornerType.Square;
            this.GPTarget.TabIndex = 9;
            this.RBUnchanged.AutoSize = true;
            this.RBUnchanged.BackColor = Color.Transparent;
            this.RBUnchanged.Location = new Point(0x2f, 3);
            this.RBUnchanged.Name = "RBUnchanged";
            this.RBUnchanged.Size = new Size(0x51, 0x11);
            this.RBUnchanged.TabIndex = 0x3a;
            this.RBUnchanged.TabStop = true;
            this.RBUnchanged.Text = "Unchanged";
            this.RBUnchanged.UseVisualStyleBackColor = false;
            this.labelX6.BackColor = Color.Transparent;
            this.labelX6.BackgroundStyle.Class = "";
            this.labelX6.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX6.Location = new Point(3, 0);
            this.labelX6.Name = "labelX6";
            this.labelX6.Size = new Size(0x26, 0x17);
            this.labelX6.TabIndex = 0x39;
            this.labelX6.Text = "<b>Target:</b>";
            this.RBEnemy.AutoSize = true;
            this.RBEnemy.BackColor = Color.Transparent;
            this.RBEnemy.Location = new Point(0x11a, 3);
            this.RBEnemy.Name = "RBEnemy";
            this.RBEnemy.Size = new Size(0x39, 0x11);
            this.RBEnemy.TabIndex = 3;
            this.RBEnemy.TabStop = true;
            this.RBEnemy.Text = "Enemy";
            this.RBEnemy.UseVisualStyleBackColor = false;
            this.RBNone.AutoSize = true;
            this.RBNone.BackColor = Color.Transparent;
            this.RBNone.Location = new Point(0x81, 3);
            this.RBNone.Name = "RBNone";
            this.RBNone.Size = new Size(0x33, 0x11);
            this.RBNone.TabIndex = 0;
            this.RBNone.TabStop = true;
            this.RBNone.Text = "None";
            this.RBNone.UseVisualStyleBackColor = false;
            this.RBPet.AutoSize = true;
            this.RBPet.BackColor = Color.Transparent;
            this.RBPet.Location = new Point(0xeb, 3);
            this.RBPet.Name = "RBPet";
            this.RBPet.Size = new Size(0x29, 0x11);
            this.RBPet.TabIndex = 2;
            this.RBPet.TabStop = true;
            this.RBPet.Text = "Pet";
            this.RBPet.UseVisualStyleBackColor = false;
            this.RBSelf.AutoSize = true;
            this.RBSelf.BackColor = Color.Transparent;
            this.RBSelf.Location = new Point(0xba, 3);
            this.RBSelf.Name = "RBSelf";
            this.RBSelf.Size = new Size(0x2b, 0x11);
            this.RBSelf.TabIndex = 1;
            this.RBSelf.TabStop = true;
            this.RBSelf.Text = "Self";
            this.RBSelf.UseVisualStyleBackColor = false;
            this.groupPanel6.CanvasColor = SystemColors.Control;
            this.groupPanel6.ColorSchemeStyle = eDotNetBarStyle.Office2007;
            this.groupPanel6.Controls.Add(this.TBRuleName);
            this.groupPanel6.Controls.Add(this.labelX7);
            this.groupPanel6.Location = new Point(10, 0x1f7);
            this.groupPanel6.Name = "groupPanel6";
            this.groupPanel6.Size = new Size(0x254, 0x1b);
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
            this.groupPanel6.TabIndex = 10;
            this.TBRuleName.Border.Class = "TextBoxBorder";
            this.TBRuleName.Border.CornerType = eCornerType.Square;
            this.TBRuleName.Location = new Point(0x84, 1);
            this.TBRuleName.Name = "TBRuleName";
            this.TBRuleName.Size = new Size(0x111, 20);
            this.TBRuleName.TabIndex = 0x3a;
            this.labelX7.BackColor = Color.Transparent;
            this.labelX7.BackgroundStyle.Class = "";
            this.labelX7.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX7.Location = new Point(3, 0);
            this.labelX7.Name = "labelX7";
            this.labelX7.Size = new Size(0x68, 0x17);
            this.labelX7.TabIndex = 0x39;
            this.labelX7.Text = "<b>Name of rule:</b>";
            this.BtnSave.AccessibleRole = AccessibleRole.PushButton;
            this.BtnSave.ColorTable = eButtonColor.OrangeWithBackground;
            this.BtnSave.Location = new Point(0x20b, 0x1fa);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new Size(0x4b, 0x16);
            this.BtnSave.Style = eDotNetBarStyle.StyleManagerControlled;
            this.BtnSave.TabIndex = 0x3b;
            this.BtnSave.Text = "Save";
            this.BtnSave.Click += new EventHandler(this.BtnSave_Click);
            this.BCancel.AccessibleRole = AccessibleRole.PushButton;
            this.BCancel.ColorTable = eButtonColor.OrangeWithBackground;
            this.BCancel.Location = new Point(0x1b1, 0x1fa);
            this.BCancel.Name = "BCancel";
            this.BCancel.Size = new Size(0x4b, 0x16);
            this.BCancel.Style = eDotNetBarStyle.StyleManagerControlled;
            this.BCancel.TabIndex = 0x3b;
            this.BCancel.Text = "Cancel";
            this.BCancel.Click += new EventHandler(this.BCancel_Click);
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.FromArgb(0xdf, 0xe9, 0xf5);
            base.ClientSize = new Size(630, 0x21f);
            base.ControlBox = false;
            base.Controls.Add(this.BtnSave);
            base.Controls.Add(this.BCancel);
            base.Controls.Add(this.groupPanel6);
            base.Controls.Add(this.GPTarget);
            base.Controls.Add(this.groupPanel4);
            base.Controls.Add(this.groupPanel3);
            base.Controls.Add(this.groupPanel2);
            base.Controls.Add(this.bar1);
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.MaximizeBox = false;
            this.MaximumSize = new Size(630, 0x21f);
            base.MinimizeBox = false;
            this.MinimumSize = new Size(630, 0x21f);
            base.Name = "RuleEditor";
            this.Text = "BehaviorCondition";
            base.FormClosing += new FormClosingEventHandler(this.RuleEditor_FormClosing);
            base.Load += new EventHandler(this.RuleEditor_Load);
            this.bar1.EndInit();
            this.AllConditions.EndInit();
            this.AllConditions.ResumeLayout(false);
            this.ConditionEditor.EndInit();
            this.groupPanel2.ResumeLayout(false);
            this.groupPanel3.ResumeLayout(false);
            this.groupPanel4.ResumeLayout(false);
            this.ComBTimes.EndInit();
            this.GPTarget.ResumeLayout(false);
            this.GPTarget.PerformLayout();
            this.groupPanel6.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        private void LoadCondition(string name, AbstractCondition condition)
        {
            Node node = new Node {
                Text = name,
                Tag = condition
            };
            this.AddNode(node);
        }

        private void MageFoodCondition_Click(object sender, EventArgs e)
        {
            this.AddCondition(this.MageFoodCondition.Text, new LazyEvo.PVEBehavior.Behavior.Conditions.MageFoodCondition());
        }

        private void MageWaterCondition_Click(object sender, EventArgs e)
        {
            this.AddCondition(this.MageWaterCondition.Text, new LazyEvo.PVEBehavior.Behavior.Conditions.MageWaterCondition());
        }

        private void PotentialAddsCondition_Click(object sender, EventArgs e)
        {
            this.AddCondition(this.PotentialAdds.Text, new PotentialAddsCondition());
        }

        private void RuleEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            GeomertrySettings.RuleEditor = Geometry.GeometryToString(this);
        }

        private void RuleEditor_Load(object sender, EventArgs e)
        {
            switch (this.Rule.ShouldTarget)
            {
                case Target.None:
                    this.RBNone.Checked = true;
                    break;

                case Target.Self:
                    this.RBSelf.Checked = true;
                    break;

                case Target.Pet:
                    this.RBPet.Checked = true;
                    break;

                case Target.Enemy:
                    this.RBEnemy.Checked = true;
                    break;

                case Target.Unchanged:
                    this.RBUnchanged.Checked = true;
                    break;

                default:
                    this.RBUnchanged.Checked = true;
                    break;
            }
            this.GPTarget.Visible = this._targetVisible;
            this.SWMatchConditions.Value = this.Rule.MatchAll;
            this.TBRuleName.Text = this.Rule.Name;
            List<AbstractCondition> getConditions = this.Rule.GetConditions;
            LazyEvo.PVEBehavior.Behavior.Action action = this.Rule.Action;
            this.ComBBar.SelectedIndex = 0;
            this.ComBSpecail.SelectedIndex = 0;
            this.ComBTimes.Value = 1;
            if (action is ActionSpell)
            {
                this.CBCastSpell.Checked = true;
                ActionSpell spell = (ActionSpell) action;
                this.TBSpellName.Text = spell.Name;
            }
            else if (!(action is ActionKey))
            {
                this.CBCastSpell.Checked = true;
            }
            else
            {
                this.CBSendKey.Checked = true;
                ActionKey key = (ActionKey) action;
                this.TBKeyName.Text = key.Name;
                this.TBKey.Text = key.Key;
                this.ComBBar.SelectedIndex = this.ComBBar.FindStringExact(key.Bar);
                this.ComBSpecail.SelectedIndex = this.ComBSpecail.FindStringExact(key.Special);
                this.ComBTimes.Value = key.Times;
            }
            foreach (AbstractCondition condition in getConditions)
            {
                this.LoadCondition(condition.Name, condition);
            }
            this.Save = false;
        }

        private void RuneCondition_Click(object sender, EventArgs e)
        {
            this.AddCondition(this.RuneCondition.Text, new LazyEvo.PVEBehavior.Behavior.Conditions.RuneCondition());
        }

        private void SoulShardCount_Click(object sender, EventArgs e)
        {
            this.AddCondition(this.SoulShardCount.Text, new SoulShardCountCondition());
        }

        private void TBKey_TextChanged(object sender, EventArgs e)
        {
        }

        private void Ticker_Click(object sender, EventArgs e)
        {
            this.AddCondition(this.Ticker.Text, new TickerCondition());
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg != 0x84)
            {
                base.WndProc(ref m);
            }
            else
            {
                base.WndProc(ref m);
                if (((int) m.Result) == 1)
                {
                    m.Result = (IntPtr) 2;
                }
            }
        }
    }
}

