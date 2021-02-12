namespace LazyEvo.LGrindEngine.NpcClasses
{
    using DevComponents.DotNetBar;
    using DevComponents.DotNetBar.Controls;
    using DevComponents.Editors;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    internal class TrainerDialog : Office2007Form
    {
        public string Class;
        public bool Ok;
        private IContainer components;
        private ButtonX buttonX1;
        private ButtonX buttonX2;
        private LabelX labelX3;
        internal ComboBoxEx CClass;
        private ComboItem comboItem1;
        private ComboItem comboItem2;
        private ComboItem comboItem3;
        private ComboItem comboItem5;
        private ComboItem comboItem6;
        private ComboItem comboItem7;
        private ComboItem comboItem8;
        private ComboItem comboItem9;
        private ComboItem comboItem10;
        private ComboItem comboItem11;
        private SuperTooltip superTooltip1;

        public TrainerDialog()
        {
            this.InitializeComponent();
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            this.Class = this.CClass.SelectedItem.ToString();
            this.Ok = true;
            base.Close();
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            this.Ok = false;
            base.Close();
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
            this.buttonX1 = new ButtonX();
            this.buttonX2 = new ButtonX();
            this.labelX3 = new LabelX();
            this.CClass = new ComboBoxEx();
            this.comboItem5 = new ComboItem();
            this.comboItem2 = new ComboItem();
            this.comboItem3 = new ComboItem();
            this.comboItem8 = new ComboItem();
            this.comboItem11 = new ComboItem();
            this.comboItem7 = new ComboItem();
            this.comboItem1 = new ComboItem();
            this.comboItem9 = new ComboItem();
            this.comboItem10 = new ComboItem();
            this.comboItem6 = new ComboItem();
            this.superTooltip1 = new SuperTooltip();
            base.SuspendLayout();
            this.buttonX1.AccessibleRole = AccessibleRole.PushButton;
            this.buttonX1.ColorTable = eButtonColor.OrangeWithBackground;
            this.buttonX1.Location = new Point(0x34, 0x55);
            this.buttonX1.Name = "buttonX1";
            this.buttonX1.Size = new Size(0x4b, 0x17);
            this.buttonX1.Style = eDotNetBarStyle.StyleManagerControlled;
            this.buttonX1.TabIndex = 0;
            this.buttonX1.Text = "OK";
            this.buttonX1.Click += new EventHandler(this.buttonX1_Click);
            this.buttonX2.AccessibleRole = AccessibleRole.PushButton;
            this.buttonX2.ColorTable = eButtonColor.OrangeWithBackground;
            this.buttonX2.Location = new Point(0x85, 0x55);
            this.buttonX2.Name = "buttonX2";
            this.buttonX2.Size = new Size(0x4b, 0x17);
            this.buttonX2.Style = eDotNetBarStyle.StyleManagerControlled;
            this.buttonX2.TabIndex = 1;
            this.buttonX2.Text = "Cancel";
            this.buttonX2.Click += new EventHandler(this.buttonX2_Click);
            this.labelX3.BackgroundStyle.Class = "";
            this.labelX3.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX3.Location = new Point(0x1c, 0x2e);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new Size(0x4b, 0x17);
            this.labelX3.TabIndex = 6;
            this.labelX3.Text = "Class:";
            this.CClass.DisplayMember = "Text";
            this.CClass.DrawMode = DrawMode.OwnerDrawFixed;
            this.CClass.DropDownStyle = ComboBoxStyle.DropDownList;
            this.CClass.FormattingEnabled = true;
            this.CClass.ItemHeight = 14;
            object[] items = new object[] { this.comboItem5, this.comboItem2, this.comboItem3, this.comboItem8, this.comboItem11, this.comboItem7, this.comboItem1, this.comboItem9, this.comboItem10 };
            items[9] = this.comboItem6;
            this.CClass.Items.AddRange(items);
            this.CClass.Location = new Point(0x5d, 0x31);
            this.CClass.Name = "CClass";
            this.CClass.Size = new Size(0x74, 20);
            this.CClass.Style = eDotNetBarStyle.StyleManagerControlled;
            this.CClass.TabIndex = 0x2e;
            this.comboItem5.Text = "Warrior";
            this.comboItem2.Text = "Paladin";
            this.comboItem3.Text = "Hunter";
            this.comboItem8.Text = "Rogue";
            this.comboItem11.Text = "Priest";
            this.comboItem7.Text = "Shaman";
            this.comboItem1.Text = "Mage";
            this.comboItem9.Text = "Warlock";
            this.comboItem10.Text = "Druid";
            this.comboItem6.Text = "DeathKnight";
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.FromArgb(0xdf, 0xe9, 0xf5);
            base.ClientSize = new Size(0x108, 0x95);
            base.Controls.Add(this.CClass);
            base.Controls.Add(this.labelX3);
            base.Controls.Add(this.buttonX2);
            base.Controls.Add(this.buttonX1);
            this.DoubleBuffered = true;
            base.FormBorderStyle = FormBorderStyle.None;
            base.Name = "TrainerDialog";
            base.ShowIcon = false;
            base.ShowInTaskbar = false;
            base.StartPosition = FormStartPosition.CenterParent;
            base.ResumeLayout(false);
        }
    }
}

