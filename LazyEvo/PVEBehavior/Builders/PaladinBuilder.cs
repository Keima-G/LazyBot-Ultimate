namespace LazyEvo.PVEBehavior.Builders
{
    using DevComponents.DotNetBar;
    using DevComponents.DotNetBar.Controls;
    using DevComponents.Editors;
    using LazyEvo.PVEBehavior;
    using LazyEvo.PVEBehavior.Behavior;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Windows.Forms;

    internal class PaladinBuilder : Office2007Form
    {
        private List<AddToBehavior> _items;
        private IContainer components;
        private GroupPanel groupPanel1;
        private GroupPanel groupPanel2;
        private GroupPanel groupPanel3;
        private ButtonX BtnCreate;
        private CheckedListBox Spec3;
        private CheckedListBox Spec2;
        private CheckedListBox Spec1;
        private CheckedListBox Normal;
        private LabelX labelX1;
        internal ComboBoxEx CBSelectSpecial;
        private RadioButton RBSpec3;
        private RadioButton RBSpec2;
        private RadioButton RBSpec1;
        private IntegerInput BeGlobalCooldown;
        private LabelX labelX25;
        private TextBoxX TBName;
        private LabelX labelX2;
        private LabelX labelX3;
        internal ComboBoxEx CBSelectSpecial2;
        private LabelX labelX4;
        internal ComboBoxEx CBSelectSpecial3;

        public PaladinBuilder()
        {
            this.InitializeComponent();
        }

        private static void AddToController(AddToBehavior addToBehavior, BehaviorController controller)
        {
            switch (addToBehavior.Type)
            {
                case LazyEvo.PVEBehavior.Behavior.Type.Combat:
                    controller.CombatController.AddRule(addToBehavior.Rule);
                    return;

                case LazyEvo.PVEBehavior.Behavior.Type.Pull:
                    controller.PullController.AddRule(addToBehavior.Rule);
                    return;

                case LazyEvo.PVEBehavior.Behavior.Type.Buff:
                    controller.BuffController.AddRule(addToBehavior.Rule);
                    return;

                case LazyEvo.PVEBehavior.Behavior.Type.Rest:
                    controller.RestController.AddRule(addToBehavior.Rule);
                    return;

                case LazyEvo.PVEBehavior.Behavior.Type.PrePull:
                    controller.PrePullController.AddRule(addToBehavior.Rule);
                    return;
            }
            throw new ArgumentOutOfRangeException();
        }

        private void BtnCreateClick(object sender, EventArgs e)
        {
            if (!File.Exists(PVEBehaviorCombat.OurDirectory + @"\Behaviors\" + this.TBName.Text + ".xml") || (MessageBoxEx.Show("Behavior exist - overwrite?", "Behavior exist - overwrite?", MessageBoxButtons.OKCancel) != DialogResult.Cancel))
            {
                BehaviorController controller = new BehaviorController {
                    SendPet = false,
                    UseAutoAttack = true,
                    PullDistance = 9,
                    PrePullDistance = 30,
                    CombatDistance = 3,
                    GlobalCooldown = this.BeGlobalCooldown.Value,
                    Name = this.TBName.Text,
                    BuffController = new RuleController(),
                    PrePullController = new RuleController(),
                    PullController = new RuleController(),
                    RestController = new RuleController(),
                    CombatController = new RuleController()
                };
                for (int i = 0; i < this.Normal.Items.Count; i++)
                {
                    if (this.Normal.GetItemChecked(i))
                    {
                        AddToController((AddToBehavior) this.Normal.Items[i], controller);
                    }
                }
                for (int j = 0; j < this.Spec1.Items.Count; j++)
                {
                    if (this.Spec1.GetItemChecked(j))
                    {
                        AddToController((AddToBehavior) this.Spec1.Items[j], controller);
                    }
                }
                for (int k = 0; k < this.Spec2.Items.Count; k++)
                {
                    if (this.Spec2.GetItemChecked(k))
                    {
                        AddToController((AddToBehavior) this.Spec2.Items[k], controller);
                    }
                }
                for (int m = 0; m < this.Spec3.Items.Count; m++)
                {
                    if (this.Spec3.GetItemChecked(m))
                    {
                        AddToController((AddToBehavior) this.Spec3.Items[m], controller);
                    }
                }
                AddToController((AddToBehavior) this.CBSelectSpecial.SelectedItem, controller);
                AddToController((AddToBehavior) this.CBSelectSpecial2.SelectedItem, controller);
                AddToController((AddToBehavior) this.CBSelectSpecial3.SelectedItem, controller);
                controller.Save();
                PveBehaviorSettings.LoadedBeharvior = this.TBName.Text;
                PveBehaviorSettings.SaveSettings();
                MessageBoxEx.Show("Created behavior, re-open the behavior settings window to load it");
            }
        }

        private void BuilderLoad(object sender, EventArgs e)
        {
            this.TBName.Text = "Paladin";
            this._items = Paladin.Load();
            foreach (AddToBehavior behavior in from addToBehavior in this._items
                where addToBehavior.Spec == Spec.Normal
                select addToBehavior)
            {
                this.Normal.Items.Add(behavior, false);
            }
            foreach (AddToBehavior behavior2 in from addToBehavior in this._items
                where addToBehavior.Spec == Spec.Tree1
                select addToBehavior)
            {
                this.Spec1.Items.Add(behavior2, false);
            }
            foreach (AddToBehavior behavior3 in from addToBehavior in this._items
                where addToBehavior.Spec == Spec.Tree2
                select addToBehavior)
            {
                this.Spec2.Items.Add(behavior3, false);
            }
            foreach (AddToBehavior behavior4 in from addToBehavior in this._items
                where addToBehavior.Spec == Spec.Tree3
                select addToBehavior)
            {
                this.Spec3.Items.Add(behavior4, false);
            }
            foreach (AddToBehavior behavior5 in from addToBehavior in this._items
                where addToBehavior.Spec == Spec.Special
                select addToBehavior)
            {
                this.CBSelectSpecial.Items.Add(behavior5);
            }
            foreach (AddToBehavior behavior6 in from addToBehavior in this._items
                where addToBehavior.Spec == Spec.Special2
                select addToBehavior)
            {
                this.CBSelectSpecial2.Items.Add(behavior6);
            }
            foreach (AddToBehavior behavior7 in from addToBehavior in this._items
                where addToBehavior.Spec == Spec.Special3
                select addToBehavior)
            {
                this.CBSelectSpecial3.Items.Add(behavior7);
            }
            this.CBSelectSpecial.SelectedIndex = 0;
            this.CBSelectSpecial2.SelectedIndex = 0;
            this.CBSelectSpecial3.SelectedIndex = 0;
            this.RBSpec1.Checked = true;
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
            this.groupPanel1 = new GroupPanel();
            this.RBSpec3 = new RadioButton();
            this.RBSpec2 = new RadioButton();
            this.RBSpec1 = new RadioButton();
            this.Spec3 = new CheckedListBox();
            this.Spec2 = new CheckedListBox();
            this.Spec1 = new CheckedListBox();
            this.groupPanel2 = new GroupPanel();
            this.BeGlobalCooldown = new IntegerInput();
            this.labelX25 = new LabelX();
            this.labelX1 = new LabelX();
            this.CBSelectSpecial = new ComboBoxEx();
            this.groupPanel3 = new GroupPanel();
            this.Normal = new CheckedListBox();
            this.BtnCreate = new ButtonX();
            this.TBName = new TextBoxX();
            this.labelX2 = new LabelX();
            this.labelX3 = new LabelX();
            this.CBSelectSpecial2 = new ComboBoxEx();
            this.labelX4 = new LabelX();
            this.CBSelectSpecial3 = new ComboBoxEx();
            this.groupPanel1.SuspendLayout();
            this.groupPanel2.SuspendLayout();
            this.BeGlobalCooldown.BeginInit();
            this.groupPanel3.SuspendLayout();
            base.SuspendLayout();
            this.groupPanel1.CanvasColor = SystemColors.Control;
            this.groupPanel1.ColorSchemeStyle = eDotNetBarStyle.Office2007;
            this.groupPanel1.Controls.Add(this.RBSpec3);
            this.groupPanel1.Controls.Add(this.RBSpec2);
            this.groupPanel1.Controls.Add(this.RBSpec1);
            this.groupPanel1.Controls.Add(this.Spec3);
            this.groupPanel1.Controls.Add(this.Spec2);
            this.groupPanel1.Controls.Add(this.Spec1);
            this.groupPanel1.Location = new Point(7, 4);
            this.groupPanel1.Name = "groupPanel1";
            this.groupPanel1.Size = new Size(0x18a, 0x106);
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
            this.groupPanel1.TabIndex = 0;
            this.groupPanel1.Text = "Talents";
            this.RBSpec3.AutoSize = true;
            this.RBSpec3.BackColor = Color.Transparent;
            this.RBSpec3.Location = new Point(0x105, 3);
            this.RBSpec3.Name = "RBSpec3";
            this.RBSpec3.Size = new Size(0x4c, 0x11);
            this.RBSpec3.TabIndex = 8;
            this.RBSpec3.TabStop = true;
            this.RBSpec3.Text = "Retribution";
            this.RBSpec3.UseVisualStyleBackColor = false;
            this.RBSpec3.CheckedChanged += new EventHandler(this.RbSpecChanged);
            this.RBSpec2.AutoSize = true;
            this.RBSpec2.BackColor = Color.Transparent;
            this.RBSpec2.Location = new Point(0x83, 3);
            this.RBSpec2.Name = "RBSpec2";
            this.RBSpec2.Size = new Size(0x49, 0x11);
            this.RBSpec2.TabIndex = 7;
            this.RBSpec2.TabStop = true;
            this.RBSpec2.Text = "Protection";
            this.RBSpec2.UseVisualStyleBackColor = false;
            this.RBSpec2.CheckedChanged += new EventHandler(this.RbSpecChanged);
            this.RBSpec1.AutoSize = true;
            this.RBSpec1.BackColor = Color.Transparent;
            this.RBSpec1.Location = new Point(3, 3);
            this.RBSpec1.Name = "RBSpec1";
            this.RBSpec1.Size = new Size(0x2e, 0x11);
            this.RBSpec1.TabIndex = 6;
            this.RBSpec1.TabStop = true;
            this.RBSpec1.Text = "Holy";
            this.RBSpec1.UseVisualStyleBackColor = false;
            this.RBSpec1.CheckedChanged += new EventHandler(this.RbSpecChanged);
            this.Spec3.FormattingEnabled = true;
            this.Spec3.Location = new Point(260, 0x17);
            this.Spec3.Name = "Spec3";
            this.Spec3.Size = new Size(0x7b, 0xd6);
            this.Spec3.TabIndex = 2;
            this.Spec2.FormattingEnabled = true;
            this.Spec2.Location = new Point(0x83, 0x17);
            this.Spec2.Name = "Spec2";
            this.Spec2.Size = new Size(0x7b, 0xd6);
            this.Spec2.TabIndex = 1;
            this.Spec1.FormattingEnabled = true;
            this.Spec1.Location = new Point(2, 0x17);
            this.Spec1.Name = "Spec1";
            this.Spec1.Size = new Size(0x7b, 0xd6);
            this.Spec1.TabIndex = 0;
            this.groupPanel2.CanvasColor = SystemColors.Control;
            this.groupPanel2.ColorSchemeStyle = eDotNetBarStyle.Office2007;
            this.groupPanel2.Controls.Add(this.labelX4);
            this.groupPanel2.Controls.Add(this.CBSelectSpecial3);
            this.groupPanel2.Controls.Add(this.labelX3);
            this.groupPanel2.Controls.Add(this.CBSelectSpecial2);
            this.groupPanel2.Controls.Add(this.labelX1);
            this.groupPanel2.Controls.Add(this.CBSelectSpecial);
            this.groupPanel2.Location = new Point(7, 0x10b);
            this.groupPanel2.Name = "groupPanel2";
            this.groupPanel2.Size = new Size(0xb7, 0x4a);
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
            this.groupPanel2.TabIndex = 1;
            this.BeGlobalCooldown.BackgroundStyle.Class = "DateTimeInputBackground";
            this.BeGlobalCooldown.BackgroundStyle.CornerType = eCornerType.Square;
            this.BeGlobalCooldown.ButtonFreeText.Shortcut = eShortcut.F2;
            this.BeGlobalCooldown.Location = new Point(0x14f, 0x110);
            this.BeGlobalCooldown.Name = "BeGlobalCooldown";
            this.BeGlobalCooldown.ShowUpDown = true;
            this.BeGlobalCooldown.Size = new Size(0x40, 20);
            this.BeGlobalCooldown.TabIndex = 0x58;
            this.BeGlobalCooldown.Value = 0x7d0;
            this.labelX25.BackColor = Color.Transparent;
            this.labelX25.BackgroundStyle.Class = "";
            this.labelX25.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX25.Location = new Point(240, 0x10d);
            this.labelX25.Name = "labelX25";
            this.labelX25.Size = new Size(0x5b, 0x1b);
            this.labelX25.TabIndex = 0x57;
            this.labelX25.Text = "Global cooldown:";
            this.labelX1.BackColor = Color.Transparent;
            this.labelX1.BackgroundStyle.Class = "";
            this.labelX1.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX1.Location = new Point(2, -1);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new Size(0x1f, 0x17);
            this.labelX1.TabIndex = 0x4e;
            this.labelX1.Tag = "";
            this.labelX1.Text = "Seal";
            this.CBSelectSpecial.DisplayMember = "Text";
            this.CBSelectSpecial.DrawMode = DrawMode.OwnerDrawFixed;
            this.CBSelectSpecial.DropDownStyle = ComboBoxStyle.DropDownList;
            this.CBSelectSpecial.FormattingEnabled = true;
            this.CBSelectSpecial.ItemHeight = 14;
            this.CBSelectSpecial.Location = new Point(0x37, 1);
            this.CBSelectSpecial.Name = "CBSelectSpecial";
            this.CBSelectSpecial.Size = new Size(0x7b, 20);
            this.CBSelectSpecial.Style = eDotNetBarStyle.StyleManagerControlled;
            this.CBSelectSpecial.TabIndex = 0x4d;
            this.groupPanel3.CanvasColor = SystemColors.Control;
            this.groupPanel3.ColorSchemeStyle = eDotNetBarStyle.Office2007;
            this.groupPanel3.Controls.Add(this.Normal);
            this.groupPanel3.Location = new Point(0x197, 4);
            this.groupPanel3.Name = "groupPanel3";
            this.groupPanel3.Size = new Size(0xb7, 0x138);
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
            this.groupPanel3.TabIndex = 2;
            this.groupPanel3.Text = "Normal";
            this.Normal.FormattingEnabled = true;
            this.Normal.Location = new Point(3, 15);
            this.Normal.Name = "Normal";
            this.Normal.Size = new Size(0xab, 0x112);
            this.Normal.TabIndex = 3;
            this.BtnCreate.AccessibleRole = AccessibleRole.PushButton;
            this.BtnCreate.ColorTable = eButtonColor.OrangeWithBackground;
            this.BtnCreate.Location = new Point(0x203, 0x13e);
            this.BtnCreate.Name = "BtnCreate";
            this.BtnCreate.Size = new Size(0x4b, 0x17);
            this.BtnCreate.Style = eDotNetBarStyle.StyleManagerControlled;
            this.BtnCreate.TabIndex = 3;
            this.BtnCreate.Text = "Create";
            this.BtnCreate.Click += new EventHandler(this.BtnCreateClick);
            this.TBName.Border.Class = "TextBoxBorder";
            this.TBName.Border.CornerType = eCornerType.Square;
            this.TBName.Location = new Point(0x14f, 320);
            this.TBName.Name = "TBName";
            this.TBName.Size = new Size(0xb0, 20);
            this.TBName.TabIndex = 4;
            this.labelX2.BackgroundStyle.Class = "";
            this.labelX2.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX2.Location = new Point(0x125, 0x143);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new Size(0x26, 0x11);
            this.labelX2.TabIndex = 5;
            this.labelX2.Text = "Name:";
            this.labelX3.BackColor = Color.Transparent;
            this.labelX3.BackgroundStyle.Class = "";
            this.labelX3.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX3.Location = new Point(2, 0x16);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new Size(0x1f, 0x17);
            this.labelX3.TabIndex = 90;
            this.labelX3.Tag = "";
            this.labelX3.Text = "Aura";
            this.CBSelectSpecial2.DisplayMember = "Text";
            this.CBSelectSpecial2.DrawMode = DrawMode.OwnerDrawFixed;
            this.CBSelectSpecial2.DropDownStyle = ComboBoxStyle.DropDownList;
            this.CBSelectSpecial2.FormattingEnabled = true;
            this.CBSelectSpecial2.ItemHeight = 14;
            this.CBSelectSpecial2.Location = new Point(0x37, 0x18);
            this.CBSelectSpecial2.Name = "CBSelectSpecial2";
            this.CBSelectSpecial2.Size = new Size(0x7b, 20);
            this.CBSelectSpecial2.Style = eDotNetBarStyle.StyleManagerControlled;
            this.CBSelectSpecial2.TabIndex = 0x59;
            this.labelX4.BackColor = Color.Transparent;
            this.labelX4.BackgroundStyle.Class = "";
            this.labelX4.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX4.Location = new Point(2, 0x2e);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new Size(0x1f, 0x17);
            this.labelX4.TabIndex = 0x5c;
            this.labelX4.Tag = "";
            this.labelX4.Text = "Buff";
            this.CBSelectSpecial3.DisplayMember = "Text";
            this.CBSelectSpecial3.DrawMode = DrawMode.OwnerDrawFixed;
            this.CBSelectSpecial3.DropDownStyle = ComboBoxStyle.DropDownList;
            this.CBSelectSpecial3.FormattingEnabled = true;
            this.CBSelectSpecial3.ItemHeight = 14;
            this.CBSelectSpecial3.Location = new Point(0x37, 0x2f);
            this.CBSelectSpecial3.Name = "CBSelectSpecial3";
            this.CBSelectSpecial3.Size = new Size(0x7b, 20);
            this.CBSelectSpecial3.Style = eDotNetBarStyle.StyleManagerControlled;
            this.CBSelectSpecial3.TabIndex = 0x5b;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.FromArgb(0xdf, 0xe9, 0xf5);
            base.ClientSize = new Size(0x251, 0x15d);
            base.Controls.Add(this.labelX2);
            base.Controls.Add(this.TBName);
            base.Controls.Add(this.BeGlobalCooldown);
            base.Controls.Add(this.BtnCreate);
            base.Controls.Add(this.labelX25);
            base.Controls.Add(this.groupPanel3);
            base.Controls.Add(this.groupPanel2);
            base.Controls.Add(this.groupPanel1);
            this.DoubleBuffered = true;
            base.Name = "PaladinBuilder";
            base.Load += new EventHandler(this.BuilderLoad);
            this.groupPanel1.ResumeLayout(false);
            this.groupPanel1.PerformLayout();
            this.groupPanel2.ResumeLayout(false);
            this.BeGlobalCooldown.EndInit();
            this.groupPanel3.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        private void RbSpecChanged(object sender, EventArgs e)
        {
            this.Spec1.Enabled = this.RBSpec1.Checked;
            this.Spec2.Enabled = this.RBSpec2.Checked;
            this.Spec3.Enabled = this.RBSpec3.Checked;
            if (!this.Spec1.Enabled)
            {
                for (int i = 0; i < this.Spec1.Items.Count; i++)
                {
                    this.Spec1.SetItemChecked(i, false);
                }
            }
            if (!this.Spec2.Enabled)
            {
                for (int i = 0; i < this.Spec2.Items.Count; i++)
                {
                    this.Spec2.SetItemChecked(i, false);
                }
            }
            if (!this.Spec3.Enabled)
            {
                for (int i = 0; i < this.Spec3.Items.Count; i++)
                {
                    this.Spec3.SetItemChecked(i, false);
                }
            }
        }
    }
}

