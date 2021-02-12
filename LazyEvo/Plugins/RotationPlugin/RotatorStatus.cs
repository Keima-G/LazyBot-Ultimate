namespace LazyEvo.Plugins.RotationPlugin
{
    using DevComponents.DotNetBar;
    using LazyEvo.Forms.Helpers;
    using LazyLib;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class RotatorStatus : Form
    {
        private const int WM_NCHITTEST = 0x84;
        private const int HTCLIENT = 1;
        private const int HTCAPTION = 2;
        private IContainer components;
        private LabelX labelX1;

        public RotatorStatus()
        {
            this.InitializeComponent();
            Geometry.GeometryFromString(GeomertrySettings.RotatorStatus, this);
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
            this.labelX1 = new LabelX();
            base.SuspendLayout();
            this.labelX1.BackgroundStyle.Class = "";
            this.labelX1.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX1.Location = new Point(0x21, 2);
            this.labelX1.MaximumSize = new Size(0x2e, 0x17);
            this.labelX1.MinimumSize = new Size(0x2e, 0x17);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new Size(0x2e, 0x17);
            this.labelX1.TabIndex = 0;
            this.labelX1.Text = "Stopped";
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x74, 0x1b);
            base.ControlBox = false;
            base.Controls.Add(this.labelX1);
            base.Name = "RotatorStatus";
            base.ShowIcon = false;
            base.ShowInTaskbar = false;
            base.TopMost = true;
            base.FormClosing += new FormClosingEventHandler(this.RotatorStatus_FormClosing);
            base.ResumeLayout(false);
        }

        private void RotatorStatus_FormClosing(object sender, FormClosingEventArgs e)
        {
            GeomertrySettings.RotatorStatus = Geometry.GeometryToString(this);
        }

        public void UpdateStatus(bool running)
        {
            MethodInvoker method = null;
            if (base.InvokeRequired)
            {
                if (method == null)
                {
                    method = () => this.UpdateStatus(running);
                }
                this.Invoke(method);
            }
            else if (running)
            {
                this.labelX1.Text = "Running";
            }
            else
            {
                this.labelX1.Text = "Stopped";
            }
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

