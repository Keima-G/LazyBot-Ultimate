namespace LazyEvo.Plugins.ExtraLazy
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class frmFrameDumper : Form
    {
        private IContainer components;
        private TextBox txtOutput;
        private TextBox txtParentName;
        private Button button1;

        public frmFrameDumper()
        {
            this.InitializeComponent();
        }

        public void addFrame(string frameName)
        {
            this.txtOutput.Text = this.txtOutput.Text + Environment.NewLine + frameName;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrameViewer.getChildren(this.txtParentName.Text);
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
            this.txtOutput = new TextBox();
            this.txtParentName = new TextBox();
            this.button1 = new Button();
            base.SuspendLayout();
            this.txtOutput.Location = new Point(12, 12);
            this.txtOutput.Multiline = true;
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.Size = new Size(0x1ab, 0x15b);
            this.txtOutput.TabIndex = 0;
            this.txtParentName.Location = new Point(12, 0x16d);
            this.txtParentName.Name = "txtParentName";
            this.txtParentName.Size = new Size(0x15a, 20);
            this.txtParentName.TabIndex = 1;
            this.button1.Location = new Point(0x16c, 0x16d);
            this.button1.Name = "button1";
            this.button1.Size = new Size(0x4b, 0x17);
            this.button1.TabIndex = 2;
            this.button1.Text = "Get Children";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new EventHandler(this.button1_Click);
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x1c3, 0x18d);
            base.Controls.Add(this.button1);
            base.Controls.Add(this.txtParentName);
            base.Controls.Add(this.txtOutput);
            base.Name = "frmFrameDumper";
            this.Text = "Frames";
            base.ResumeLayout(false);
            base.PerformLayout();
        }
    }
}

