namespace LazyEvo.PVEBehavior
{
    using DevComponents.DotNetBar;
    using DevComponents.DotNetBar.Controls;
    using LazyEvo.Forms.Helpers;
    using LazyEvo.PVEBehavior.Behavior;
    using LazyLib;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Text;
    using System.Windows.Forms;

    internal class ScriptEditor : Office2007RibbonForm
    {
        private const int WM_NCHITTEST = 0x84;
        private const int HTCLIENT = 1;
        private const int HTCAPTION = 2;
        public LazyEvo.PVEBehavior.Behavior.Rule Rule;
        public bool Save;
        private IContainer components;
        private StyleManager styleManager1;
        private GroupPanel groupPanel6;
        private LabelX labelX7;
        private TextBoxX TBRuleName;
        private ButtonX BtnSave;
        private ButtonX BCancel;
        private RichTextBox TBScript;
        private SuperTooltip superTooltip1;

        public ScriptEditor(LazyEvo.PVEBehavior.Behavior.Rule rule)
        {
            this.InitializeComponent();
            this.Rule = rule;
            Geometry.GeometryFromString(GeomertrySettings.ScriptEditor, this);
        }

        private void BCancelClick(object sender, EventArgs e)
        {
            this.Save = false;
            base.Close();
        }

        private void BtnSaveClick(object sender, EventArgs e)
        {
            if (this.TBRuleName.Text == "")
            {
                this.superTooltip1.SetSuperTooltip(this.TBRuleName, new SuperTooltipInfo("", "", "Please give the rule a name.", null, null, eTooltipColor.Gray));
                this.superTooltip1.ShowTooltip(this.TBRuleName);
            }
            else
            {
                this.Rule.Script = this.TBScript.Text;
                this.Rule.Name = this.TBRuleName.Text;
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

        private void InitializeComponent()
        {
            this.components = new Container();
            this.styleManager1 = new StyleManager(this.components);
            this.groupPanel6 = new GroupPanel();
            this.BtnSave = new ButtonX();
            this.BCancel = new ButtonX();
            this.TBRuleName = new TextBoxX();
            this.labelX7 = new LabelX();
            this.TBScript = new RichTextBox();
            this.superTooltip1 = new SuperTooltip();
            this.groupPanel6.SuspendLayout();
            base.SuspendLayout();
            this.styleManager1.ManagerStyle = eStyle.Windows7Blue;
            this.groupPanel6.CanvasColor = SystemColors.Control;
            this.groupPanel6.ColorSchemeStyle = eDotNetBarStyle.Office2007;
            this.groupPanel6.Controls.Add(this.BtnSave);
            this.groupPanel6.Controls.Add(this.BCancel);
            this.groupPanel6.Controls.Add(this.TBRuleName);
            this.groupPanel6.Controls.Add(this.labelX7);
            this.groupPanel6.Location = new Point(10, 0x17d);
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
            this.groupPanel6.TabIndex = 11;
            this.BtnSave.AccessibleRole = AccessibleRole.PushButton;
            this.BtnSave.ColorTable = eButtonColor.OrangeWithBackground;
            this.BtnSave.Location = new Point(0x1f5, 0);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new Size(0x4b, 0x15);
            this.BtnSave.Style = eDotNetBarStyle.StyleManagerControlled;
            this.BtnSave.TabIndex = 0x3d;
            this.BtnSave.Text = "Save";
            this.BtnSave.Click += new EventHandler(this.BtnSaveClick);
            this.BCancel.AccessibleRole = AccessibleRole.PushButton;
            this.BCancel.ColorTable = eButtonColor.OrangeWithBackground;
            this.BCancel.Location = new Point(0x19b, 0);
            this.BCancel.Name = "BCancel";
            this.BCancel.Size = new Size(0x4b, 0x15);
            this.BCancel.Style = eDotNetBarStyle.StyleManagerControlled;
            this.BCancel.TabIndex = 60;
            this.BCancel.Text = "Cancel";
            this.BCancel.Click += new EventHandler(this.BCancelClick);
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
            this.TBScript.AcceptsTab = true;
            this.TBScript.Location = new Point(10, 13);
            this.TBScript.Name = "TBScript";
            this.TBScript.Size = new Size(0x254, 0x16a);
            this.TBScript.TabIndex = 12;
            this.TBScript.Text = "";
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.FromArgb(0xdf, 0xe9, 0xf5);
            base.ClientSize = new Size(0x269, 0x1c4);
            base.ControlBox = false;
            base.Controls.Add(this.TBScript);
            base.Controls.Add(this.groupPanel6);
            base.Name = "ScriptEditor";
            this.Text = "ScriptEditor";
            base.FormClosing += new FormClosingEventHandler(this.ScriptEditor_FormClosing);
            base.Load += new EventHandler(this.ScriptEditor_Load);
            this.groupPanel6.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        private void ScriptEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            GeomertrySettings.ScriptEditor = Geometry.GeometryToString(this);
        }

        private void ScriptEditor_Load(object sender, EventArgs e)
        {
            this.TBScript.Text = this.Rule.Script;
            this.TBRuleName.Text = this.Rule.Name;
            if (string.IsNullOrEmpty(this.Rule.Script))
            {
                StringBuilder builder = new StringBuilder();
                builder.AppendLine("public static bool ShouldRun()");
                builder.AppendLine("{");
                builder.AppendLine("     return true;");
                builder.AppendLine("}");
                builder.AppendLine("");
                builder.AppendLine("public static void Run()");
                builder.AppendLine("{");
                builder.AppendLine("     Public.Write(\"Running\");");
                builder.AppendLine("}");
                this.TBScript.Text = builder.ToString();
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

