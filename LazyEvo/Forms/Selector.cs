namespace LazyEvo.Forms
{
    using DevComponents.DotNetBar;
    using DevComponents.DotNetBar.Controls;
    using LazyEvo;
    using LazyEvo.Forms.Helpers;
    using LazyLib;
    using LazyLib.Helpers;
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Windows.Forms;

    internal class Selector : Office2007Form
    {
        private Process[] _wowProc = Process.GetProcessesByName("Wow");
        private IContainer components;
        private GroupPanel groupPanel1;
        private ButtonX BtnAttach;
        private ButtonX BtnRefresh;
        private ListBox SelectProcess;
        private StyleManager styleManager1;

        public Selector()
        {
            this.InitializeComponent();
            Geometry.GeometryFromString(GeomertrySettings.ProcessSelector, this);
        }

        private void BtnAttach_Click(object sender, EventArgs e)
        {
            if (this.SelectProcess.SelectedItem != null)
            {
                Program.AttachTo = (this.SelectProcess.SelectedItem.ToString() == "No game") ? -1 : this._wowProc[this.SelectProcess.SelectedIndex].Id;
                base.Close();
            }
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            this.RefreshProcess();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void GetName(Process proc)
        {
            string str;
            if (!Memory.OpenProcess(proc.Id))
            {
                return;
            }
            else
            {
                str = "Not ingame";
                try
                {
                    uint[] addresses = new uint[] { Memory.BaseAddress + 0x7d0792 };
                    if (Memory.Read<byte>(addresses) == 1)
                    {
                        try
                        {
                            str = Memory.ReadUtf8(Memory.BaseAddress + 0x879d18, 0x100);
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
            object[] objArray = new object[] { "[", proc.Id, "] - ", str };
            this.SelectProcess.Items.Add(string.Concat(objArray));
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            ComponentResourceManager manager = new ComponentResourceManager(typeof(Selector));
            this.groupPanel1 = new GroupPanel();
            this.SelectProcess = new ListBox();
            this.styleManager1 = new StyleManager(this.components);
            this.BtnAttach = new ButtonX();
            this.BtnRefresh = new ButtonX();
            this.groupPanel1.SuspendLayout();
            base.SuspendLayout();
            this.groupPanel1.CanvasColor = SystemColors.Control;
            this.groupPanel1.ColorSchemeStyle = eDotNetBarStyle.Office2007;
            this.groupPanel1.Controls.Add(this.SelectProcess);
            this.groupPanel1.Location = new Point(3, 4);
            this.groupPanel1.Name = "groupPanel1";
            this.groupPanel1.Size = new Size(290, 0x71);
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
            this.SelectProcess.BackColor = Color.FromArgb(0xdf, 0xea, 0xf6);
            this.SelectProcess.BorderStyle = BorderStyle.None;
            this.SelectProcess.Dock = DockStyle.Fill;
            this.SelectProcess.FormattingEnabled = true;
            this.SelectProcess.Location = new Point(0, 0);
            this.SelectProcess.Name = "SelectProcess";
            this.SelectProcess.Size = new Size(0x11c, 0x6b);
            this.SelectProcess.TabIndex = 0;
            this.styleManager1.ManagerStyle = eStyle.Office2007Blue;
            this.BtnAttach.AccessibleRole = AccessibleRole.PushButton;
            this.BtnAttach.ColorTable = eButtonColor.OrangeWithBackground;
            this.BtnAttach.Location = new Point(3, 0x79);
            this.BtnAttach.Name = "BtnAttach";
            this.BtnAttach.Size = new Size(0xb8, 0x16);
            this.BtnAttach.Style = eDotNetBarStyle.StyleManagerControlled;
            this.BtnAttach.TabIndex = 1;
            this.BtnAttach.Text = "Attach";
            this.BtnAttach.Click += new EventHandler(this.BtnAttach_Click);
            this.BtnRefresh.AccessibleRole = AccessibleRole.PushButton;
            this.BtnRefresh.ColorTable = eButtonColor.OrangeWithBackground;
            this.BtnRefresh.Location = new Point(0xc1, 0x79);
            this.BtnRefresh.Name = "BtnRefresh";
            this.BtnRefresh.Size = new Size(0x62, 0x16);
            this.BtnRefresh.Style = eDotNetBarStyle.StyleManagerControlled;
            this.BtnRefresh.TabIndex = 2;
            this.BtnRefresh.Text = "Refresh";
            this.BtnRefresh.Click += new EventHandler(this.BtnRefresh_Click);
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.FromArgb(0xdf, 0xe9, 0xf5);
            base.ClientSize = new Size(0x130, 0x9c);
            base.Controls.Add(this.BtnRefresh);
            base.Controls.Add(this.BtnAttach);
            base.Controls.Add(this.groupPanel1);
            this.EnableGlass = false;
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            this.MaximumSize = new Size(0x138, 0xb7);
            this.MinimumSize = new Size(0x138, 0xb7);
            base.Name = "Selector";
            this.Text = "Select process";
            base.FormClosing += new FormClosingEventHandler(this.Selector_FormClosing);
            base.Load += new EventHandler(this.Selector_Load);
            this.groupPanel1.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        private void RefreshProcess()
        {
            this.SelectProcess.Items.Clear();
            this._wowProc = Process.GetProcessesByName("Wow");
            foreach (Process process in this._wowProc)
            {
                this.GetName(process);
            }
            if (this.SelectProcess.Items.Count == 0)
            {
                this.SelectProcess.Items.Add("No game");
            }
            this.SelectProcess.SelectedIndex = 0;
        }

        private void Selector_FormClosing(object sender, FormClosingEventArgs e)
        {
            GeomertrySettings.ProcessSelector = Geometry.GeometryToString(this);
        }

        private void Selector_Load(object sender, EventArgs e)
        {
            this.RefreshProcess();
        }
    }
}

