namespace LazyLib.Dialogs.UnhandledExceptionDlg
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Reflection;
    using System.Windows.Forms;

    [Obfuscation(Feature="renaming", ApplyToMembers=true)]
    public class UnhandledExDlgForm : Form
    {
        private IContainer components;
        private Panel panelTop;
        private Panel panelDevider;
        private Label labelExceptionDate;
        private Label labelCaption;
        private Button buttonNotSend;
        internal Label labelTitle;
        internal Button buttonSend;
        internal CheckBox checkBoxRestart;
        public TextBox textBox1;

        public UnhandledExDlgForm()
        {
            this.InitializeComponent();
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
            this.panelTop = new Panel();
            this.labelTitle = new Label();
            this.panelDevider = new Panel();
            this.labelExceptionDate = new Label();
            this.labelCaption = new Label();
            this.buttonNotSend = new Button();
            this.buttonSend = new Button();
            this.checkBoxRestart = new CheckBox();
            this.textBox1 = new TextBox();
            this.panelTop.SuspendLayout();
            base.SuspendLayout();
            this.panelTop.BackColor = SystemColors.Window;
            this.panelTop.Controls.Add(this.labelTitle);
            this.panelTop.Dock = DockStyle.Top;
            this.panelTop.Location = new Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new Size(0x1e5, 0x3f);
            this.panelTop.TabIndex = 0;
            this.labelTitle.Font = new Font("Tahoma", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.labelTitle.Location = new Point(13, 13);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new Size(0x183, 0x2c);
            this.labelTitle.TabIndex = 0;
            this.labelTitle.Text = "\"{0}\" encountered a problem and needed to close";
            this.panelDevider.BorderStyle = BorderStyle.FixedSingle;
            this.panelDevider.Dock = DockStyle.Top;
            this.panelDevider.Location = new Point(0, 0x3f);
            this.panelDevider.Name = "panelDevider";
            this.panelDevider.Size = new Size(0x1e5, 2);
            this.panelDevider.TabIndex = 1;
            this.labelExceptionDate.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.labelExceptionDate.Location = new Point(12, 0x44);
            this.labelExceptionDate.Name = "labelExceptionDate";
            this.labelExceptionDate.Size = new Size(0x180, 0x17);
            this.labelExceptionDate.TabIndex = 2;
            this.labelExceptionDate.Text = "This error occured on {0}";
            this.labelCaption.Font = new Font("Tahoma", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.labelCaption.Location = new Point(13, 0x5b);
            this.labelCaption.Name = "labelCaption";
            this.labelCaption.Size = new Size(0x1bb, 0x17);
            this.labelCaption.TabIndex = 3;
            this.labelCaption.Text = "Please notify Arutha about this problem with a copy of the following:";
            this.buttonNotSend.DialogResult = DialogResult.Cancel;
            this.buttonNotSend.Location = new Point(0x141, 0x142);
            this.buttonNotSend.Name = "buttonNotSend";
            this.buttonNotSend.Size = new Size(0x4b, 0x17);
            this.buttonNotSend.TabIndex = 6;
            this.buttonNotSend.Text = "Close";
            this.buttonNotSend.UseVisualStyleBackColor = true;
            this.buttonSend.DialogResult = DialogResult.Yes;
            this.buttonSend.Location = new Point(0xd0, 0x142);
            this.buttonSend.Name = "buttonSend";
            this.buttonSend.Size = new Size(0x6b, 0x17);
            this.buttonSend.TabIndex = 7;
            this.buttonSend.Text = "&Send Error Report";
            this.buttonSend.UseVisualStyleBackColor = true;
            this.buttonSend.Visible = false;
            this.checkBoxRestart.AutoSize = true;
            this.checkBoxRestart.Checked = true;
            this.checkBoxRestart.CheckState = CheckState.Checked;
            this.checkBoxRestart.Enabled = false;
            this.checkBoxRestart.Location = new Point(9, 0x148);
            this.checkBoxRestart.Name = "checkBoxRestart";
            this.checkBoxRestart.Size = new Size(0x57, 0x11);
            this.checkBoxRestart.TabIndex = 5;
            this.checkBoxRestart.Text = "&Restart \"{0}\"";
            this.checkBoxRestart.UseVisualStyleBackColor = true;
            this.checkBoxRestart.Visible = false;
            this.textBox1.Location = new Point(0, 0x71);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = ScrollBars.Both;
            this.textBox1.Size = new Size(480, 0xcc);
            this.textBox1.TabIndex = 10;
            base.AcceptButton = this.buttonNotSend;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.CancelButton = this.buttonNotSend;
            base.ClientSize = new Size(0x1e5, 0x163);
            base.ControlBox = false;
            base.Controls.Add(this.textBox1);
            base.Controls.Add(this.checkBoxRestart);
            base.Controls.Add(this.buttonSend);
            base.Controls.Add(this.buttonNotSend);
            base.Controls.Add(this.labelCaption);
            base.Controls.Add(this.labelExceptionDate);
            base.Controls.Add(this.panelDevider);
            base.Controls.Add(this.panelTop);
            this.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            base.FormBorderStyle = FormBorderStyle.FixedDialog;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "UnhandledExDlgForm";
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "fsdbvcbs";
            base.TopMost = true;
            base.Load += new EventHandler(this.UnhandledExDlgFormLoad);
            this.panelTop.ResumeLayout(false);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void UnhandledExDlgFormLoad(object sender, EventArgs e)
        {
            this.buttonNotSend.Focus();
            this.labelExceptionDate.Text = string.Format(this.labelExceptionDate.Text, DateTime.Now);
        }
    }
}

