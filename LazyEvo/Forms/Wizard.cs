namespace LazyEvo.Forms
{
    using DevComponents.DotNetBar;
    using DevComponents.DotNetBar.Controls;
    using DevComponents.Editors;
    using LazyEvo.Classes;
    using LazyEvo.Properties;
    using LazyLib;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Windows.Forms;

    internal class Wizard : Office2007Form
    {
        private IContainer components;
        internal DevComponents.DotNetBar.Wizard Wizard1;
        internal WizardPage WizardWelcomePage;
        internal Label Label1;
        private WizardPage wizardPage3;
        private LabelX labelX6;
        private WizardPage wizardPage4;
        private LabelX labelX8;
        private LabelX labelX9;
        private ReflectionImage reflectionImage2;
        private LabelX labelX10;
        private LabelX labelX2;
        private LabelX labelX1;
        private LabelX labelX7;
        internal Label label2;
        internal Label label3;
        private WizardPage wizardPage1;
        internal Label label4;
        private LabelX labelX4;
        private ReflectionImage reflectionImage1;
        private LabelX labelX5;
        private WizardPage wizardPage2;
        internal Label label5;
        private ReflectionImage reflectionImage3;
        private LabelX labelX3;
        private WizardPage wizardPage5;
        internal Label label6;
        private ReflectionImage reflectionImage4;
        private LabelX labelX11;
        private WizardPage wizardPage6;
        internal Label label7;
        private ReflectionImage reflectionImage5;
        private LabelX labelX12;
        private LabelX labelX13;
        private WizardPage wizardPage7;
        private LabelX labelX14;
        internal Label label8;
        private ReflectionImage reflectionImage6;
        private LabelX labelX15;
        private WizardPage wizardPage9;
        private LabelX labelX18;
        internal Label label10;
        private ReflectionImage reflectionImage8;
        private LabelX labelX19;
        private WizardPage wizardPage10;
        internal Label label11;
        private LabelX labelX23;
        private LabelX labelX21;
        private LabelX labelX20;
        internal CheckBoxX SetupUseHotkeys;
        internal CheckBoxX SetupCBBackground;
        private LabelX labelX17;
        private LabelX labelX16;
        internal IntegerInput SetupTBLogOutOnFollow;
        internal CheckBoxX SetupCBLogoutOnFollow;
        internal CheckBoxX SetupCBSoundFollow;
        internal CheckBoxX SetupCBSoundWhisper;
        internal CheckBoxX SetupCBSoundStop;
        private WizardPage wizardPage8;
        private LabelItem labelItem1;
        private ComboBoxItem SelectEngine;
        private LabelItem labelItem2;
        private ComboBoxItem comboBoxItem1;
        internal Label label9;
        private LabelX labelX22;
        private LabelX labelX43;
        internal ComboBoxEx SelectedEngine;
        private ComboItem comboItem330;
        private ComboItem comboItem331;
        private LabelX labelX24;
        internal ComboBoxEx SelectCombat;
        private ComboItem comboItem3;
        private LabelX labelX25;
        internal ComboBoxEx ClientLanguage;
        private ComboItem comboItem94;
        private ComboItem comboItem95;
        private ComboItem comboItem88;
        private ComboItem comboItem89;
        private ComboItem comboItem90;
        private LabelX labelX46;

        public Wizard()
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
            ComponentResourceManager manager = new ComponentResourceManager(typeof(LazyEvo.Forms.Wizard));
            this.comboItem3 = new ComboItem();
            this.comboItem330 = new ComboItem();
            this.comboItem331 = new ComboItem();
            this.labelItem1 = new LabelItem();
            this.SelectEngine = new ComboBoxItem();
            this.labelItem2 = new LabelItem();
            this.comboBoxItem1 = new ComboBoxItem();
            this.Wizard1 = new DevComponents.DotNetBar.Wizard();
            this.WizardWelcomePage = new WizardPage();
            this.labelX2 = new LabelX();
            this.labelX1 = new LabelX();
            this.Label1 = new Label();
            this.wizardPage3 = new WizardPage();
            this.label2 = new Label();
            this.labelX6 = new LabelX();
            this.labelX7 = new LabelX();
            this.wizardPage4 = new WizardPage();
            this.label3 = new Label();
            this.labelX8 = new LabelX();
            this.labelX9 = new LabelX();
            this.reflectionImage2 = new ReflectionImage();
            this.labelX10 = new LabelX();
            this.wizardPage1 = new WizardPage();
            this.label4 = new Label();
            this.labelX4 = new LabelX();
            this.reflectionImage1 = new ReflectionImage();
            this.labelX5 = new LabelX();
            this.wizardPage2 = new WizardPage();
            this.label5 = new Label();
            this.reflectionImage3 = new ReflectionImage();
            this.labelX3 = new LabelX();
            this.wizardPage5 = new WizardPage();
            this.label6 = new Label();
            this.reflectionImage4 = new ReflectionImage();
            this.labelX11 = new LabelX();
            this.wizardPage6 = new WizardPage();
            this.labelX13 = new LabelX();
            this.label7 = new Label();
            this.reflectionImage5 = new ReflectionImage();
            this.labelX12 = new LabelX();
            this.wizardPage7 = new WizardPage();
            this.labelX14 = new LabelX();
            this.label8 = new Label();
            this.reflectionImage6 = new ReflectionImage();
            this.labelX15 = new LabelX();
            this.wizardPage9 = new WizardPage();
            this.labelX18 = new LabelX();
            this.label10 = new Label();
            this.reflectionImage8 = new ReflectionImage();
            this.labelX19 = new LabelX();
            this.wizardPage10 = new WizardPage();
            this.labelX17 = new LabelX();
            this.labelX16 = new LabelX();
            this.SetupTBLogOutOnFollow = new IntegerInput();
            this.SetupCBLogoutOnFollow = new CheckBoxX();
            this.SetupCBSoundFollow = new CheckBoxX();
            this.SetupCBSoundWhisper = new CheckBoxX();
            this.SetupCBSoundStop = new CheckBoxX();
            this.label11 = new Label();
            this.labelX23 = new LabelX();
            this.labelX21 = new LabelX();
            this.labelX20 = new LabelX();
            this.SetupUseHotkeys = new CheckBoxX();
            this.SetupCBBackground = new CheckBoxX();
            this.wizardPage8 = new WizardPage();
            this.SelectCombat = new ComboBoxEx();
            this.labelX25 = new LabelX();
            this.labelX24 = new LabelX();
            this.labelX43 = new LabelX();
            this.SelectedEngine = new ComboBoxEx();
            this.label9 = new Label();
            this.labelX22 = new LabelX();
            this.ClientLanguage = new ComboBoxEx();
            this.comboItem94 = new ComboItem();
            this.comboItem95 = new ComboItem();
            this.comboItem88 = new ComboItem();
            this.comboItem89 = new ComboItem();
            this.comboItem90 = new ComboItem();
            this.labelX46 = new LabelX();
            this.Wizard1.SuspendLayout();
            this.WizardWelcomePage.SuspendLayout();
            this.wizardPage3.SuspendLayout();
            this.wizardPage4.SuspendLayout();
            this.wizardPage1.SuspendLayout();
            this.wizardPage2.SuspendLayout();
            this.wizardPage5.SuspendLayout();
            this.wizardPage6.SuspendLayout();
            this.wizardPage7.SuspendLayout();
            this.wizardPage9.SuspendLayout();
            this.wizardPage10.SuspendLayout();
            this.SetupTBLogOutOnFollow.BeginInit();
            this.wizardPage8.SuspendLayout();
            base.SuspendLayout();
            this.comboItem3.Text = "Behavior Engine";
            this.comboItem330.Text = "Grinding";
            this.comboItem331.Text = "Flying gathering";
            this.labelItem1.Name = "labelItem1";
            this.labelItem1.Text = "<b>Engine:</b>";
            this.labelItem1.ThemeAware = true;
            this.SelectEngine.DropDownHeight = 0x6a;
            this.SelectEngine.Name = "SelectEngine";
            this.SelectEngine.ThemeAware = true;
            this.labelItem2.Name = "labelItem2";
            this.labelItem2.Text = "<b>Engine:</b>";
            this.labelItem2.ThemeAware = true;
            this.comboBoxItem1.DropDownHeight = 0x6a;
            this.comboBoxItem1.Name = "comboBoxItem1";
            this.comboBoxItem1.ThemeAware = true;
            this.Wizard1.BackColor = Color.FromArgb(0xcd, 0xe5, 0xfd);
            this.Wizard1.BackgroundImage = (Image) manager.GetObject("Wizard1.BackgroundImage");
            this.Wizard1.ButtonStyle = eWizardStyle.Office2007;
            this.Wizard1.Cursor = Cursors.Default;
            this.Wizard1.Dock = DockStyle.Fill;
            this.Wizard1.FinishButtonTabIndex = 3;
            this.Wizard1.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.Wizard1.FooterStyle.BackColor = Color.Transparent;
            this.Wizard1.FooterStyle.Class = "";
            this.Wizard1.FooterStyle.CornerType = eCornerType.Square;
            this.Wizard1.ForeColor = Color.FromArgb(15, 0x39, 0x81);
            this.Wizard1.HeaderCaptionFont = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.Wizard1.HeaderDescriptionVisible = false;
            this.Wizard1.HeaderHeight = 90;
            this.Wizard1.HeaderImageAlignment = eWizardTitleImageAlignment.Left;
            this.Wizard1.HeaderStyle.BackColor = Color.FromArgb(0xbf, 0xd7, 0xf3);
            this.Wizard1.HeaderStyle.BackColor2 = Color.FromArgb(0xdb, 0xf1, 0xfe);
            this.Wizard1.HeaderStyle.BackColorGradientAngle = 90;
            this.Wizard1.HeaderStyle.BorderBottom = eStyleBorderType.Solid;
            this.Wizard1.HeaderStyle.BorderBottomColor = Color.FromArgb(0x79, 0x9d, 0xb6);
            this.Wizard1.HeaderStyle.BorderBottomWidth = 1;
            this.Wizard1.HeaderStyle.BorderColor = SystemColors.Control;
            this.Wizard1.HeaderStyle.BorderLeftWidth = 1;
            this.Wizard1.HeaderStyle.BorderRightWidth = 1;
            this.Wizard1.HeaderStyle.BorderTopWidth = 1;
            this.Wizard1.HeaderStyle.Class = "";
            this.Wizard1.HeaderStyle.CornerType = eCornerType.Square;
            this.Wizard1.HeaderStyle.TextAlignment = eStyleTextAlignment.Center;
            this.Wizard1.HeaderStyle.TextColorSchemePart = eColorSchemePart.PanelText;
            this.Wizard1.HeaderTitleIndent = 0x3e;
            this.Wizard1.HelpButtonVisible = false;
            this.Wizard1.Location = new Point(0, 0);
            this.Wizard1.Name = "Wizard1";
            this.Wizard1.Size = new Size(0x225, 0x27f);
            this.Wizard1.TabIndex = 2;
            WizardPage[] wizardPages = new WizardPage[] { this.WizardWelcomePage, this.wizardPage3, this.wizardPage4, this.wizardPage1, this.wizardPage2, this.wizardPage5, this.wizardPage6, this.wizardPage7, this.wizardPage9 };
            wizardPages[9] = this.wizardPage10;
            wizardPages[10] = this.wizardPage8;
            this.Wizard1.WizardPages.AddRange(wizardPages);
            this.Wizard1.FinishButtonClick += new CancelEventHandler(this.Wizard1FinishButtonClick);
            this.Wizard1.CancelButtonClick += new CancelEventHandler(this.Wizard1CancelButtonClick);
            this.Wizard1.WizardPageChanging += new WizardCancelPageChangeEventHandler(this.Wizard1WizardPageChanging);
            this.Wizard1.WizardPageChanged += new WizardPageChangeEventHandler(this.Wizard1WizardPageChanged);
            this.WizardWelcomePage.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.WizardWelcomePage.AntiAlias = false;
            this.WizardWelcomePage.BackColor = Color.Transparent;
            this.WizardWelcomePage.Controls.Add(this.labelX2);
            this.WizardWelcomePage.Controls.Add(this.labelX1);
            this.WizardWelcomePage.Controls.Add(this.Label1);
            this.WizardWelcomePage.InteriorPage = false;
            this.WizardWelcomePage.Location = new Point(0, 0);
            this.WizardWelcomePage.Name = "WizardWelcomePage";
            this.WizardWelcomePage.Size = new Size(0x225, 0x251);
            this.WizardWelcomePage.Style.Class = "";
            this.WizardWelcomePage.Style.CornerType = eCornerType.Square;
            this.WizardWelcomePage.StyleMouseDown.Class = "";
            this.WizardWelcomePage.StyleMouseDown.CornerType = eCornerType.Square;
            this.WizardWelcomePage.StyleMouseOver.Class = "";
            this.WizardWelcomePage.StyleMouseOver.CornerType = eCornerType.Square;
            this.WizardWelcomePage.TabIndex = 7;
            this.labelX2.BackgroundStyle.Class = "";
            this.labelX2.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX2.ForeColor = Color.Maroon;
            this.labelX2.Location = new Point(12, 0x41);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new Size(0x20d, 0x25);
            this.labelX2.TabIndex = 4;
            this.labelX2.Text = "Before pressing next please open the Wow client and login to the character you want to use <br/> with LazyBot.";
            this.labelX1.BackgroundStyle.Class = "";
            this.labelX1.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX1.Location = new Point(12, 0x29);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new Size(320, 0x12);
            this.labelX1.TabIndex = 3;
            this.labelX1.Text = "<b>This guide will help you setup LazyBot. <br/></b>\r\n";
            this.Label1.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.Label1.BackColor = Color.Transparent;
            this.Label1.Font = new Font("Tahoma", 16f);
            this.Label1.Location = new Point(7, 9);
            this.Label1.Name = "Label1";
            this.Label1.Size = new Size(0x145, 30);
            this.Label1.TabIndex = 0;
            this.Label1.Text = "Welcome to LazyBot";
            this.wizardPage3.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.wizardPage3.AntiAlias = false;
            this.wizardPage3.BackColor = Color.Transparent;
            this.wizardPage3.Controls.Add(this.label2);
            this.wizardPage3.Controls.Add(this.labelX6);
            this.wizardPage3.Controls.Add(this.labelX7);
            this.wizardPage3.InteriorPage = false;
            this.wizardPage3.Location = new Point(0, 0);
            this.wizardPage3.Name = "wizardPage3";
            this.wizardPage3.Size = new Size(0x225, 0x251);
            this.wizardPage3.Style.Class = "";
            this.wizardPage3.Style.CornerType = eCornerType.Square;
            this.wizardPage3.StyleMouseDown.Class = "";
            this.wizardPage3.StyleMouseDown.CornerType = eCornerType.Square;
            this.wizardPage3.StyleMouseOver.Class = "";
            this.wizardPage3.StyleMouseOver.CornerType = eCornerType.Square;
            this.wizardPage3.TabIndex = 10;
            this.label2.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.label2.BackColor = Color.Transparent;
            this.label2.Font = new Font("Tahoma", 16f);
            this.label2.Location = new Point(7, 9);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x145, 0x1d);
            this.label2.TabIndex = 7;
            this.label2.Text = "Requirements";
            this.labelX6.BackgroundStyle.Class = "";
            this.labelX6.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX6.ForeColor = Color.Maroon;
            this.labelX6.Location = new Point(12, 0x59);
            this.labelX6.Name = "labelX6";
            this.labelX6.Size = new Size(0x20d, 40);
            this.labelX6.TabIndex = 6;
            this.labelX6.Text = "Warning: No addon means no addons: Bar mods like Bartender and UI mods like Bagon will not <br/> work well/at all with LazyBot...please disable you addons";
            this.labelX7.BackgroundStyle.Class = "";
            this.labelX7.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX7.Location = new Point(12, 0x29);
            this.labelX7.Name = "labelX7";
            this.labelX7.Size = new Size(0x219, 0x36);
            this.labelX7.TabIndex = 5;
            this.labelX7.Text = "English World of Warcraft client! <br/>\r\nRetail World of Warcraft server! <br/>\r\nNo addons!";
            this.wizardPage4.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.wizardPage4.AntiAlias = false;
            this.wizardPage4.BackColor = Color.Transparent;
            this.wizardPage4.Controls.Add(this.label3);
            this.wizardPage4.Controls.Add(this.labelX8);
            this.wizardPage4.Controls.Add(this.labelX9);
            this.wizardPage4.Controls.Add(this.reflectionImage2);
            this.wizardPage4.Controls.Add(this.labelX10);
            this.wizardPage4.InteriorPage = false;
            this.wizardPage4.Location = new Point(0, 0);
            this.wizardPage4.Name = "wizardPage4";
            this.wizardPage4.Size = new Size(0x225, 0x251);
            this.wizardPage4.Style.Class = "";
            this.wizardPage4.Style.CornerType = eCornerType.Square;
            this.wizardPage4.StyleMouseDown.Class = "";
            this.wizardPage4.StyleMouseDown.CornerType = eCornerType.Square;
            this.wizardPage4.StyleMouseOver.Class = "";
            this.wizardPage4.StyleMouseOver.CornerType = eCornerType.Square;
            this.wizardPage4.TabIndex = 11;
            this.label3.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.label3.BackColor = Color.Transparent;
            this.label3.Font = new Font("Tahoma", 16f);
            this.label3.Location = new Point(7, 9);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x145, 0x1d);
            this.label3.TabIndex = 8;
            this.label3.Text = "Reset Keybindings";
            this.labelX8.BackgroundStyle.Class = "";
            this.labelX8.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX8.Location = new Point(12, 0x229);
            this.labelX8.Name = "labelX8";
            this.labelX8.Size = new Size(0x20f, 0x1c);
            this.labelX8.TabIndex = 7;
            this.labelX8.Text = @"Advanced users: The most common keys can be changed in the settings after this wizard are done. <br/> The rest of the keys used by LazyBot are located in Settings\Keys.xml file.";
            this.labelX9.BackgroundStyle.Class = "";
            this.labelX9.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX9.ForeColor = Color.Maroon;
            this.labelX9.Location = new Point(12, 0x1dc);
            this.labelX9.Name = "labelX9";
            this.labelX9.Size = new Size(530, 0x44);
            this.labelX9.TabIndex = 6;
            this.labelX9.Text = manager.GetString("labelX9.Text");
            this.reflectionImage2.BackgroundStyle.Class = "";
            this.reflectionImage2.BackgroundStyle.CornerType = eCornerType.Square;
            this.reflectionImage2.BackgroundStyle.TextAlignment = eStyleTextAlignment.Center;
            this.reflectionImage2.Image = Resources.Reset_keybindings;
            this.reflectionImage2.Location = new Point(5, 0x57);
            this.reflectionImage2.Name = "reflectionImage2";
            this.reflectionImage2.Size = new Size(0x20f, 0x17f);
            this.reflectionImage2.TabIndex = 5;
            this.labelX10.BackgroundStyle.Class = "";
            this.labelX10.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX10.Location = new Point(12, 0x29);
            this.labelX10.Name = "labelX10";
            this.labelX10.Size = new Size(530, 0x29);
            this.labelX10.TabIndex = 4;
            this.labelX10.Text = "Press ESC <br/>\r\nSelect \"Key Binding\"  <br/>\r\nPress \"Reset to Default\" text.  <br/>";
            this.wizardPage1.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.wizardPage1.AntiAlias = false;
            this.wizardPage1.BackColor = Color.Transparent;
            this.wizardPage1.Controls.Add(this.label4);
            this.wizardPage1.Controls.Add(this.labelX4);
            this.wizardPage1.Controls.Add(this.reflectionImage1);
            this.wizardPage1.Controls.Add(this.labelX5);
            this.wizardPage1.InteriorPage = false;
            this.wizardPage1.Location = new Point(0, 0);
            this.wizardPage1.Name = "wizardPage1";
            this.wizardPage1.Size = new Size(0x225, 0x251);
            this.wizardPage1.Style.Class = "";
            this.wizardPage1.Style.CornerType = eCornerType.Square;
            this.wizardPage1.StyleMouseDown.Class = "";
            this.wizardPage1.StyleMouseDown.CornerType = eCornerType.Square;
            this.wizardPage1.StyleMouseOver.Class = "";
            this.wizardPage1.StyleMouseOver.CornerType = eCornerType.Square;
            this.wizardPage1.TabIndex = 12;
            this.label4.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.label4.BackColor = Color.Transparent;
            this.label4.Font = new Font("Tahoma", 16f);
            this.label4.Location = new Point(7, 9);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x145, 0x1d);
            this.label4.TabIndex = 13;
            this.label4.Text = "Disable Click To Move";
            this.labelX4.BackgroundStyle.Class = "";
            this.labelX4.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX4.ForeColor = Color.Maroon;
            this.labelX4.Location = new Point(12, 0x1d5);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new Size(0x218, 0x60);
            this.labelX4.TabIndex = 11;
            this.labelX4.Text = manager.GetString("labelX4.Text");
            this.reflectionImage1.BackgroundStyle.Class = "";
            this.reflectionImage1.BackgroundStyle.CornerType = eCornerType.Square;
            this.reflectionImage1.BackgroundStyle.TextAlignment = eStyleTextAlignment.Center;
            this.reflectionImage1.Image = Resources.Disable_ctm;
            this.reflectionImage1.Location = new Point(6, 0x39);
            this.reflectionImage1.Name = "reflectionImage1";
            this.reflectionImage1.Size = new Size(0x20f, 0x17f);
            this.reflectionImage1.TabIndex = 10;
            this.labelX5.BackgroundStyle.Class = "";
            this.labelX5.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX5.Location = new Point(12, 0x29);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new Size(530, 14);
            this.labelX5.TabIndex = 9;
            this.labelX5.Text = "ds";
            this.wizardPage2.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.wizardPage2.AntiAlias = false;
            this.wizardPage2.BackColor = Color.Transparent;
            this.wizardPage2.Controls.Add(this.label5);
            this.wizardPage2.Controls.Add(this.reflectionImage3);
            this.wizardPage2.Controls.Add(this.labelX3);
            this.wizardPage2.InteriorPage = false;
            this.wizardPage2.Location = new Point(0, 0);
            this.wizardPage2.Name = "wizardPage2";
            this.wizardPage2.Size = new Size(0x225, 0x251);
            this.wizardPage2.Style.Class = "";
            this.wizardPage2.Style.CornerType = eCornerType.Square;
            this.wizardPage2.StyleMouseDown.Class = "";
            this.wizardPage2.StyleMouseDown.CornerType = eCornerType.Square;
            this.wizardPage2.StyleMouseOver.Class = "";
            this.wizardPage2.StyleMouseOver.CornerType = eCornerType.Square;
            this.wizardPage2.TabIndex = 13;
            this.label5.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.label5.BackColor = Color.Transparent;
            this.label5.Font = new Font("Tahoma", 16f);
            this.label5.Location = new Point(7, 9);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x145, 0x1d);
            this.label5.TabIndex = 0x10;
            this.label5.Text = "Auto loot";
            this.reflectionImage3.BackgroundStyle.Class = "";
            this.reflectionImage3.BackgroundStyle.CornerType = eCornerType.Square;
            this.reflectionImage3.BackgroundStyle.TextAlignment = eStyleTextAlignment.Center;
            this.reflectionImage3.Image = Resources.Enable_auto_loot;
            this.reflectionImage3.Location = new Point(1, 0x6b);
            this.reflectionImage3.Name = "reflectionImage3";
            this.reflectionImage3.Size = new Size(0x20f, 0x1d7);
            this.reflectionImage3.TabIndex = 15;
            this.labelX3.BackgroundStyle.Class = "";
            this.labelX3.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX3.Location = new Point(12, 0x29);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new Size(530, 60);
            this.labelX3.TabIndex = 14;
            this.labelX3.Text = "Press ESC  <br/>\r\nSelect Interface  <br/>\r\nSelect Controls.  <br/>\r\nEnable Auto Loot.  <br/>";
            this.wizardPage5.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.wizardPage5.AntiAlias = false;
            this.wizardPage5.BackColor = Color.Transparent;
            this.wizardPage5.Controls.Add(this.label6);
            this.wizardPage5.Controls.Add(this.reflectionImage4);
            this.wizardPage5.Controls.Add(this.labelX11);
            this.wizardPage5.InteriorPage = false;
            this.wizardPage5.Location = new Point(0, 0);
            this.wizardPage5.Name = "wizardPage5";
            this.wizardPage5.Size = new Size(0x225, 0x251);
            this.wizardPage5.Style.Class = "";
            this.wizardPage5.Style.CornerType = eCornerType.Square;
            this.wizardPage5.StyleMouseDown.Class = "";
            this.wizardPage5.StyleMouseDown.CornerType = eCornerType.Square;
            this.wizardPage5.StyleMouseOver.Class = "";
            this.wizardPage5.StyleMouseOver.CornerType = eCornerType.Square;
            this.wizardPage5.TabIndex = 14;
            this.label6.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.label6.BackColor = Color.Transparent;
            this.label6.Font = new Font("Tahoma", 16f);
            this.label6.Location = new Point(10, 6);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x145, 0x1d);
            this.label6.TabIndex = 0x13;
            this.label6.Text = "Auto Self Cast";
            this.reflectionImage4.BackgroundStyle.Class = "";
            this.reflectionImage4.BackgroundStyle.CornerType = eCornerType.Square;
            this.reflectionImage4.BackgroundStyle.TextAlignment = eStyleTextAlignment.Center;
            this.reflectionImage4.Image = Resources.Enable_auto_selfcast;
            this.reflectionImage4.Location = new Point(4, 0x68);
            this.reflectionImage4.Name = "reflectionImage4";
            this.reflectionImage4.Size = new Size(0x20f, 0x1d7);
            this.reflectionImage4.TabIndex = 0x12;
            this.labelX11.BackgroundStyle.Class = "";
            this.labelX11.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX11.Location = new Point(15, 0x26);
            this.labelX11.Name = "labelX11";
            this.labelX11.Size = new Size(530, 60);
            this.labelX11.TabIndex = 0x11;
            this.labelX11.Text = "Press ESC  <br/>\r\nSelect Interface  <br/>\r\nSelect Combat  <br/>\r\nEnable Auto Self Cast  <br/>";
            this.wizardPage6.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.wizardPage6.AntiAlias = false;
            this.wizardPage6.BackColor = Color.Transparent;
            this.wizardPage6.Controls.Add(this.labelX13);
            this.wizardPage6.Controls.Add(this.label7);
            this.wizardPage6.Controls.Add(this.reflectionImage5);
            this.wizardPage6.Controls.Add(this.labelX12);
            this.wizardPage6.InteriorPage = false;
            this.wizardPage6.Location = new Point(0, 0);
            this.wizardPage6.Name = "wizardPage6";
            this.wizardPage6.Size = new Size(0x225, 0x251);
            this.wizardPage6.Style.Class = "";
            this.wizardPage6.Style.CornerType = eCornerType.Square;
            this.wizardPage6.StyleMouseDown.Class = "";
            this.wizardPage6.StyleMouseDown.CornerType = eCornerType.Square;
            this.wizardPage6.StyleMouseOver.Class = "";
            this.wizardPage6.StyleMouseOver.CornerType = eCornerType.Square;
            this.wizardPage6.TabIndex = 15;
            this.labelX13.BackgroundStyle.Class = "";
            this.labelX13.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX13.ForeColor = Color.Maroon;
            this.labelX13.Location = new Point(15, 0x22f);
            this.labelX13.Name = "labelX13";
            this.labelX13.Size = new Size(0x218, 0x16);
            this.labelX13.TabIndex = 0x17;
            this.labelX13.Text = "Tip: This can be changed later in the LazyBot settings";
            this.label7.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.label7.BackColor = Color.Transparent;
            this.label7.Font = new Font("Tahoma", 16f);
            this.label7.Location = new Point(10, 6);
            this.label7.Name = "label7";
            this.label7.Size = new Size(0x145, 0x1d);
            this.label7.TabIndex = 0x16;
            this.label7.Text = "Interact With Mouseover";
            this.reflectionImage5.BackgroundStyle.Class = "";
            this.reflectionImage5.BackgroundStyle.CornerType = eCornerType.Square;
            this.reflectionImage5.BackgroundStyle.TextAlignment = eStyleTextAlignment.Center;
            this.reflectionImage5.Image = Resources.Interact_with_mouseover;
            this.reflectionImage5.Location = new Point(4, 0x68);
            this.reflectionImage5.Name = "reflectionImage5";
            this.reflectionImage5.Size = new Size(0x20f, 0x1b9);
            this.reflectionImage5.TabIndex = 0x15;
            this.labelX12.BackgroundStyle.Class = "";
            this.labelX12.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX12.Location = new Point(15, 0x26);
            this.labelX12.Name = "labelX12";
            this.labelX12.Size = new Size(530, 60);
            this.labelX12.TabIndex = 20;
            this.labelX12.Text = "Press ESC  <br/>\r\nSelect \"Key Binding\"  <br/>\r\nScroll down until you see \"Interact With Mouseover\"  <br/>\r\nBind \"Interact With Mouseover\" to U  <br/>";
            this.wizardPage7.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.wizardPage7.AntiAlias = false;
            this.wizardPage7.BackColor = Color.Transparent;
            this.wizardPage7.Controls.Add(this.labelX14);
            this.wizardPage7.Controls.Add(this.label8);
            this.wizardPage7.Controls.Add(this.reflectionImage6);
            this.wizardPage7.Controls.Add(this.labelX15);
            this.wizardPage7.InteriorPage = false;
            this.wizardPage7.Location = new Point(0, 0);
            this.wizardPage7.Name = "wizardPage7";
            this.wizardPage7.Size = new Size(0x225, 0x251);
            this.wizardPage7.Style.Class = "";
            this.wizardPage7.Style.CornerType = eCornerType.Square;
            this.wizardPage7.StyleMouseDown.Class = "";
            this.wizardPage7.StyleMouseDown.CornerType = eCornerType.Square;
            this.wizardPage7.StyleMouseOver.Class = "";
            this.wizardPage7.StyleMouseOver.CornerType = eCornerType.Square;
            this.wizardPage7.TabIndex = 0x10;
            this.labelX14.BackgroundStyle.Class = "";
            this.labelX14.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX14.ForeColor = Color.Maroon;
            this.labelX14.Location = new Point(12, 0x22c);
            this.labelX14.Name = "labelX14";
            this.labelX14.Size = new Size(0x218, 0x16);
            this.labelX14.TabIndex = 0x1b;
            this.labelX14.Text = "Tip: This can be changed later in the LazyBot settings";
            this.labelX14.Click += new EventHandler(this.labelX14_Click);
            this.label8.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.label8.BackColor = Color.Transparent;
            this.label8.Font = new Font("Tahoma", 16f);
            this.label8.Location = new Point(7, 3);
            this.label8.Name = "label8";
            this.label8.Size = new Size(0x145, 0x1d);
            this.label8.TabIndex = 0x1a;
            this.label8.Text = "Interact With Target";
            this.reflectionImage6.BackgroundStyle.Class = "";
            this.reflectionImage6.BackgroundStyle.CornerType = eCornerType.Square;
            this.reflectionImage6.BackgroundStyle.TextAlignment = eStyleTextAlignment.Center;
            this.reflectionImage6.Image = Resources.Interact_with_target;
            this.reflectionImage6.Location = new Point(1, 0x65);
            this.reflectionImage6.Name = "reflectionImage6";
            this.reflectionImage6.Size = new Size(0x20f, 0x1b9);
            this.reflectionImage6.TabIndex = 0x19;
            this.labelX15.BackgroundStyle.Class = "";
            this.labelX15.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX15.Location = new Point(12, 0x23);
            this.labelX15.Name = "labelX15";
            this.labelX15.Size = new Size(530, 60);
            this.labelX15.TabIndex = 0x18;
            this.labelX15.Text = "Press ESC  <br/>\r\nSelect \"Key Binding\"  <br/>\r\nScroll down until you see \"Interact With Target\"  <br/>\r\nBind \"Interact With Target\" to P  <br/>";
            this.wizardPage9.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.wizardPage9.AntiAlias = false;
            this.wizardPage9.BackColor = Color.Transparent;
            this.wizardPage9.Controls.Add(this.labelX18);
            this.wizardPage9.Controls.Add(this.label10);
            this.wizardPage9.Controls.Add(this.reflectionImage8);
            this.wizardPage9.Controls.Add(this.labelX19);
            this.wizardPage9.InteriorPage = false;
            this.wizardPage9.Location = new Point(0, 0);
            this.wizardPage9.Name = "wizardPage9";
            this.wizardPage9.Size = new Size(0x225, 0x251);
            this.wizardPage9.Style.Class = "";
            this.wizardPage9.Style.CornerType = eCornerType.Square;
            this.wizardPage9.StyleMouseDown.Class = "";
            this.wizardPage9.StyleMouseDown.CornerType = eCornerType.Square;
            this.wizardPage9.StyleMouseOver.Class = "";
            this.wizardPage9.StyleMouseOver.CornerType = eCornerType.Square;
            this.wizardPage9.TabIndex = 0x12;
            this.labelX18.BackgroundStyle.Class = "";
            this.labelX18.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX18.ForeColor = Color.Maroon;
            this.labelX18.Location = new Point(12, 0x22c);
            this.labelX18.Name = "labelX18";
            this.labelX18.Size = new Size(0x218, 0x16);
            this.labelX18.TabIndex = 0x23;
            this.labelX18.Text = "Tip: This can be changed later in the LazyBot settings";
            this.label10.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.label10.BackColor = Color.Transparent;
            this.label10.Font = new Font("Tahoma", 16f);
            this.label10.Location = new Point(7, 3);
            this.label10.Name = "label10";
            this.label10.Size = new Size(0x145, 0x1d);
            this.label10.TabIndex = 0x22;
            this.label10.Text = "Target Last Target";
            this.reflectionImage8.BackgroundStyle.Class = "";
            this.reflectionImage8.BackgroundStyle.CornerType = eCornerType.Square;
            this.reflectionImage8.BackgroundStyle.TextAlignment = eStyleTextAlignment.Center;
            this.reflectionImage8.Image = Resources.Target_last_target1;
            this.reflectionImage8.Location = new Point(1, 0x65);
            this.reflectionImage8.Name = "reflectionImage8";
            this.reflectionImage8.Size = new Size(0x20f, 0x1b9);
            this.reflectionImage8.TabIndex = 0x21;
            this.labelX19.BackgroundStyle.Class = "";
            this.labelX19.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX19.Location = new Point(12, 0x23);
            this.labelX19.Name = "labelX19";
            this.labelX19.Size = new Size(530, 60);
            this.labelX19.TabIndex = 0x20;
            this.labelX19.Text = "Press ESC  <br/>\r\nSelect \"Key Binding\"  <br/>\r\nScroll down until you see \"Target Last Target\"  <br/>\r\nBind \"Target Last Target\" to G  <br/>";
            this.wizardPage10.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.wizardPage10.AntiAlias = false;
            this.wizardPage10.BackColor = Color.Transparent;
            this.wizardPage10.Controls.Add(this.ClientLanguage);
            this.wizardPage10.Controls.Add(this.labelX46);
            this.wizardPage10.Controls.Add(this.labelX17);
            this.wizardPage10.Controls.Add(this.labelX16);
            this.wizardPage10.Controls.Add(this.SetupTBLogOutOnFollow);
            this.wizardPage10.Controls.Add(this.SetupCBLogoutOnFollow);
            this.wizardPage10.Controls.Add(this.SetupCBSoundFollow);
            this.wizardPage10.Controls.Add(this.SetupCBSoundWhisper);
            this.wizardPage10.Controls.Add(this.SetupCBSoundStop);
            this.wizardPage10.Controls.Add(this.label11);
            this.wizardPage10.Controls.Add(this.labelX23);
            this.wizardPage10.Controls.Add(this.labelX21);
            this.wizardPage10.Controls.Add(this.labelX20);
            this.wizardPage10.Controls.Add(this.SetupUseHotkeys);
            this.wizardPage10.Controls.Add(this.SetupCBBackground);
            this.wizardPage10.InteriorPage = false;
            this.wizardPage10.Location = new Point(0, 0);
            this.wizardPage10.Name = "wizardPage10";
            this.wizardPage10.Size = new Size(0x225, 0x251);
            this.wizardPage10.Style.Class = "";
            this.wizardPage10.Style.CornerType = eCornerType.Square;
            this.wizardPage10.StyleMouseDown.Class = "";
            this.wizardPage10.StyleMouseDown.CornerType = eCornerType.Square;
            this.wizardPage10.StyleMouseOver.Class = "";
            this.wizardPage10.StyleMouseOver.CornerType = eCornerType.Square;
            this.wizardPage10.TabIndex = 0x13;
            this.labelX17.BackColor = Color.Transparent;
            this.labelX17.BackgroundStyle.Class = "";
            this.labelX17.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX17.Location = new Point(12, 0xc9);
            this.labelX17.Name = "labelX17";
            this.labelX17.Size = new Size(0xf4, 0x17);
            this.labelX17.TabIndex = 0xc0;
            this.labelX17.Text = "<b>Anti detection settings</b>";
            this.labelX16.BackColor = Color.Transparent;
            this.labelX16.BackgroundStyle.Class = "";
            this.labelX16.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX16.Location = new Point(0xd0, 0xf5);
            this.labelX16.Name = "labelX16";
            this.labelX16.Size = new Size(0x4b, 0x17);
            this.labelX16.TabIndex = 0xbf;
            this.labelX16.Text = "minuttes";
            this.SetupTBLogOutOnFollow.BackgroundStyle.Class = "DateTimeInputBackground";
            this.SetupTBLogOutOnFollow.BackgroundStyle.CornerType = eCornerType.Square;
            this.SetupTBLogOutOnFollow.ButtonFreeText.Shortcut = eShortcut.F2;
            this.SetupTBLogOutOnFollow.Location = new Point(0x7a, 0xf6);
            this.SetupTBLogOutOnFollow.Name = "SetupTBLogOutOnFollow";
            this.SetupTBLogOutOnFollow.ShowUpDown = true;
            this.SetupTBLogOutOnFollow.Size = new Size(80, 0x15);
            this.SetupTBLogOutOnFollow.TabIndex = 190;
            this.SetupCBLogoutOnFollow.AutoSize = true;
            this.SetupCBLogoutOnFollow.BackColor = Color.Transparent;
            this.SetupCBLogoutOnFollow.BackgroundStyle.Class = "";
            this.SetupCBLogoutOnFollow.BackgroundStyle.CornerType = eCornerType.Square;
            this.SetupCBLogoutOnFollow.Location = new Point(14, 0xf6);
            this.SetupCBLogoutOnFollow.Name = "SetupCBLogoutOnFollow";
            this.SetupCBLogoutOnFollow.Size = new Size(0x67, 0x10);
            this.SetupCBLogoutOnFollow.TabIndex = 0xbd;
            this.SetupCBLogoutOnFollow.Text = "Logout on follow";
            this.SetupCBSoundFollow.AutoSize = true;
            this.SetupCBSoundFollow.BackColor = Color.Transparent;
            this.SetupCBSoundFollow.BackgroundStyle.Class = "";
            this.SetupCBSoundFollow.BackgroundStyle.CornerType = eCornerType.Square;
            this.SetupCBSoundFollow.Location = new Point(14, 0xe1);
            this.SetupCBSoundFollow.Name = "SetupCBSoundFollow";
            this.SetupCBSoundFollow.Size = new Size(0x7b, 0x10);
            this.SetupCBSoundFollow.TabIndex = 0xba;
            this.SetupCBSoundFollow.Text = "Play sound on follow";
            this.SetupCBSoundWhisper.AutoSize = true;
            this.SetupCBSoundWhisper.BackColor = Color.Transparent;
            this.SetupCBSoundWhisper.BackgroundStyle.Class = "";
            this.SetupCBSoundWhisper.BackgroundStyle.CornerType = eCornerType.Square;
            this.SetupCBSoundWhisper.Location = new Point(14, 0x10b);
            this.SetupCBSoundWhisper.Name = "SetupCBSoundWhisper";
            this.SetupCBSoundWhisper.Size = new Size(0x84, 0x10);
            this.SetupCBSoundWhisper.TabIndex = 0xbb;
            this.SetupCBSoundWhisper.Text = "Play sound on whisper";
            this.SetupCBSoundStop.AutoSize = true;
            this.SetupCBSoundStop.BackColor = Color.Transparent;
            this.SetupCBSoundStop.BackgroundStyle.Class = "";
            this.SetupCBSoundStop.BackgroundStyle.CornerType = eCornerType.Square;
            this.SetupCBSoundStop.Location = new Point(14, 0x120);
            this.SetupCBSoundStop.Name = "SetupCBSoundStop";
            this.SetupCBSoundStop.Size = new Size(0x73, 0x10);
            this.SetupCBSoundStop.TabIndex = 0xbc;
            this.SetupCBSoundStop.Text = "Play sound on stop";
            this.label11.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.label11.BackColor = Color.Transparent;
            this.label11.Font = new Font("Tahoma", 16f);
            this.label11.Location = new Point(7, 9);
            this.label11.Name = "label11";
            this.label11.Size = new Size(0x145, 0x1d);
            this.label11.TabIndex = 0xb7;
            this.label11.Text = "Settings";
            this.labelX23.BackgroundStyle.Class = "";
            this.labelX23.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX23.Location = new Point(12, 0x23);
            this.labelX23.Name = "labelX23";
            this.labelX23.Size = new Size(530, 60);
            this.labelX23.TabIndex = 0xb6;
            this.labelX23.Text = manager.GetString("labelX23.Text");
            this.labelX23.Click += new EventHandler(this.labelX23_Click);
            this.labelX21.BackColor = Color.Transparent;
            this.labelX21.BackgroundStyle.Class = "";
            this.labelX21.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX21.Location = new Point(14, 0xa7);
            this.labelX21.Name = "labelX21";
            this.labelX21.Size = new Size(0x1b9, 0x1c);
            this.labelX21.TabIndex = 0xb5;
            this.labelX21.Text = "F9 = Start/stop bot <br/>\r\nF10 = Pause bot";
            this.labelX20.BackColor = Color.Transparent;
            this.labelX20.BackgroundStyle.Class = "";
            this.labelX20.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX20.Location = new Point(12, 0x76);
            this.labelX20.Name = "labelX20";
            this.labelX20.Size = new Size(0xf4, 0x17);
            this.labelX20.TabIndex = 180;
            this.labelX20.Text = "<b>Should the bot use hotkeys?</b>";
            this.SetupUseHotkeys.AutoSize = true;
            this.SetupUseHotkeys.BackColor = Color.Transparent;
            this.SetupUseHotkeys.BackgroundStyle.Class = "";
            this.SetupUseHotkeys.BackgroundStyle.CornerType = eCornerType.Square;
            this.SetupUseHotkeys.Location = new Point(12, 0x93);
            this.SetupUseHotkeys.Name = "SetupUseHotkeys";
            this.SetupUseHotkeys.Size = new Size(0x60, 0x10);
            this.SetupUseHotkeys.TabIndex = 0xb3;
            this.SetupUseHotkeys.Text = "Enable hotkeys";
            this.SetupCBBackground.AutoSize = true;
            this.SetupCBBackground.BackColor = Color.Transparent;
            this.SetupCBBackground.BackgroundStyle.Class = "";
            this.SetupCBBackground.BackgroundStyle.CornerType = eCornerType.Square;
            this.SetupCBBackground.Location = new Point(12, 0x60);
            this.SetupCBBackground.Name = "SetupCBBackground";
            this.SetupCBBackground.Size = new Size(0x75, 0x10);
            this.SetupCBBackground.TabIndex = 0xb1;
            this.SetupCBBackground.Text = "Enable mouse hook";
            this.SetupCBBackground.CheckedChanged += new EventHandler(this.SetupCbBackgroundCheckedChanged);
            this.wizardPage8.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.wizardPage8.AntiAlias = false;
            this.wizardPage8.BackColor = Color.Transparent;
            this.wizardPage8.Controls.Add(this.SelectCombat);
            this.wizardPage8.Controls.Add(this.labelX25);
            this.wizardPage8.Controls.Add(this.labelX24);
            this.wizardPage8.Controls.Add(this.labelX43);
            this.wizardPage8.Controls.Add(this.SelectedEngine);
            this.wizardPage8.Controls.Add(this.label9);
            this.wizardPage8.Controls.Add(this.labelX22);
            this.wizardPage8.InteriorPage = false;
            this.wizardPage8.Location = new Point(0, 0);
            this.wizardPage8.Name = "wizardPage8";
            this.wizardPage8.Size = new Size(0x225, 0x251);
            this.wizardPage8.Style.Class = "";
            this.wizardPage8.Style.CornerType = eCornerType.Square;
            this.wizardPage8.StyleMouseDown.Class = "";
            this.wizardPage8.StyleMouseDown.CornerType = eCornerType.Square;
            this.wizardPage8.StyleMouseOver.Class = "";
            this.wizardPage8.StyleMouseOver.CornerType = eCornerType.Square;
            this.wizardPage8.TabIndex = 20;
            this.SelectCombat.DisplayMember = "Text";
            this.SelectCombat.DrawMode = DrawMode.OwnerDrawFixed;
            this.SelectCombat.DropDownStyle = ComboBoxStyle.DropDownList;
            this.SelectCombat.FormattingEnabled = true;
            this.SelectCombat.ItemHeight = 15;
            object[] items = new object[] { this.comboItem3 };
            this.SelectCombat.Items.AddRange(items);
            this.SelectCombat.Location = new Point(0x5f, 0x92);
            this.SelectCombat.Name = "SelectCombat";
            this.SelectCombat.Size = new Size(0xcb, 0x15);
            this.SelectCombat.Style = eDotNetBarStyle.StyleManagerControlled;
            this.SelectCombat.TabIndex = 190;
            this.labelX25.BackgroundStyle.Class = "";
            this.labelX25.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX25.Location = new Point(12, 0x90);
            this.labelX25.Name = "labelX25";
            this.labelX25.Size = new Size(80, 0x17);
            this.labelX25.TabIndex = 0xbd;
            this.labelX25.Text = "Combat system:";
            this.labelX24.BackgroundStyle.Class = "";
            this.labelX24.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX24.Location = new Point(12, 0x75);
            this.labelX24.Name = "labelX24";
            this.labelX24.Size = new Size(530, 0x15);
            this.labelX24.TabIndex = 0xbc;
            this.labelX24.Text = "Select how you would like to handle combat:";
            this.labelX43.BackColor = Color.Transparent;
            this.labelX43.BackgroundStyle.Class = "";
            this.labelX43.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX43.Location = new Point(12, 90);
            this.labelX43.Name = "labelX43";
            this.labelX43.Size = new Size(0x4b, 0x17);
            this.labelX43.TabIndex = 0xbb;
            this.labelX43.Text = "Select engine:";
            this.SelectedEngine.DisplayMember = "Text";
            this.SelectedEngine.DrawMode = DrawMode.OwnerDrawFixed;
            this.SelectedEngine.DropDownStyle = ComboBoxStyle.DropDownList;
            this.SelectedEngine.FormattingEnabled = true;
            this.SelectedEngine.ItemHeight = 15;
            object[] objArray2 = new object[] { this.comboItem330, this.comboItem331 };
            this.SelectedEngine.Items.AddRange(objArray2);
            this.SelectedEngine.Location = new Point(0x5f, 90);
            this.SelectedEngine.Name = "SelectedEngine";
            this.SelectedEngine.Size = new Size(0xcb, 0x15);
            this.SelectedEngine.Style = eDotNetBarStyle.StyleManagerControlled;
            this.SelectedEngine.TabIndex = 0xba;
            this.label9.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.label9.BackColor = Color.Transparent;
            this.label9.Font = new Font("Tahoma", 16f);
            this.label9.Location = new Point(7, 12);
            this.label9.Name = "label9";
            this.label9.Size = new Size(0x145, 0x1d);
            this.label9.TabIndex = 0xb9;
            this.label9.Text = "Settings";
            this.labelX22.BackgroundStyle.Class = "";
            this.labelX22.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX22.Location = new Point(12, 0x23);
            this.labelX22.Name = "labelX22";
            this.labelX22.Size = new Size(530, 0x37);
            this.labelX22.TabIndex = 0xb8;
            this.labelX22.Text = manager.GetString("labelX22.Text");
            this.ClientLanguage.DisplayMember = "Text";
            this.ClientLanguage.DrawMode = DrawMode.OwnerDrawFixed;
            this.ClientLanguage.DropDownStyle = ComboBoxStyle.DropDownList;
            this.ClientLanguage.FormattingEnabled = true;
            this.ClientLanguage.ItemHeight = 15;
            object[] objArray3 = new object[] { this.comboItem94, this.comboItem95, this.comboItem88, this.comboItem89, this.comboItem90 };
            this.ClientLanguage.Items.AddRange(objArray3);
            this.ClientLanguage.Location = new Point(0x10, 0x158);
            this.ClientLanguage.Name = "ClientLanguage";
            this.ClientLanguage.Size = new Size(0xf4, 0x15);
            this.ClientLanguage.Style = eDotNetBarStyle.StyleManagerControlled;
            this.ClientLanguage.TabIndex = 0x10a;
            this.comboItem94.Text = "English";
            this.comboItem95.Text = "Russian";
            this.comboItem88.Text = "German";
            this.comboItem89.Text = "French";
            this.comboItem90.Text = "Spanish";
            this.labelX46.BackColor = Color.Transparent;
            this.labelX46.BackgroundStyle.Class = "";
            this.labelX46.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX46.Location = new Point(12, 320);
            this.labelX46.Name = "labelX46";
            this.labelX46.Size = new Size(0xa8, 0x17);
            this.labelX46.TabIndex = 4;
            this.labelX46.Text = "<b>Client language:</b>";
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x225, 0x27f);
            base.Controls.Add(this.Wizard1);
            this.DoubleBuffered = true;
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.Name = "Wizard";
            this.Text = "Wizard";
            base.Load += new EventHandler(this.WizardLoad);
            this.Wizard1.ResumeLayout(false);
            this.WizardWelcomePage.ResumeLayout(false);
            this.wizardPage3.ResumeLayout(false);
            this.wizardPage4.ResumeLayout(false);
            this.wizardPage1.ResumeLayout(false);
            this.wizardPage2.ResumeLayout(false);
            this.wizardPage5.ResumeLayout(false);
            this.wizardPage6.ResumeLayout(false);
            this.wizardPage7.ResumeLayout(false);
            this.wizardPage9.ResumeLayout(false);
            this.wizardPage10.ResumeLayout(false);
            this.wizardPage10.PerformLayout();
            this.SetupTBLogOutOnFollow.EndInit();
            this.wizardPage8.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        private void labelX14_Click(object sender, EventArgs e)
        {
        }

        private void labelX23_Click(object sender, EventArgs e)
        {
        }

        private void LoadCustomClasses()
        {
            this.SelectCombat.Items.Clear();
            ClassCompiler.RecompileAll();
            foreach (KeyValuePair<string, CombatEngine> pair in ClassCompiler.Assemblys)
            {
                CustomClass item = new CustomClass(pair.Key, pair.Value.Name);
                this.SelectCombat.Items.Add(item);
            }
            if (this.SelectCombat.Items.Count != 0)
            {
                this.SelectCombat.SelectedIndex = 0;
            }
        }

        private void LoadEngines()
        {
            this.SelectedEngine.Items.Clear();
            EngineCompiler.RecompileAll();
            foreach (KeyValuePair<string, ILazyEngine> pair in EngineCompiler.Assemblys)
            {
                CustomEngine item = new CustomEngine(pair.Key, pair.Value.Name);
                this.SelectedEngine.Items.Add(item);
            }
            if (this.SelectedEngine.Items.Count != 0)
            {
                this.SelectedEngine.SelectedIndex = 0;
            }
        }

        private void LoadSettings()
        {
            this.SetupUseHotkeys.Checked = LazySettings.SetupUseHotkeys;
            this.SetupTBLogOutOnFollow.Text = LazySettings.LogOutOnFollowTime;
            this.SetupCBSoundFollow.Checked = LazySettings.SoundFollow;
            this.SetupCBSoundWhisper.Checked = LazySettings.SoundWhisper;
            this.SetupCBSoundStop.Checked = LazySettings.SoundStop;
            this.SetupCBBackground.Checked = LazySettings.BackgroundMode;
            this.SetupCBLogoutOnFollow.Checked = LazySettings.LogoutOnFollow;
            this.ClientLanguage.SelectedIndex = 0;
        }

        private void SaveSettings()
        {
            LazySettings.SetupUseHotkeys = this.SetupUseHotkeys.Checked;
            LazySettings.LogOutOnFollowTime = this.SetupTBLogOutOnFollow.Text;
            LazySettings.SoundFollow = this.SetupCBSoundFollow.Checked;
            LazySettings.SoundWhisper = this.SetupCBSoundWhisper.Checked;
            LazySettings.SoundStop = this.SetupCBSoundStop.Checked;
            LazySettings.HookMouse = this.SetupCBBackground.Checked;
            LazySettings.BackgroundMode = false;
            LazySettings.LogoutOnFollow = this.SetupCBLogoutOnFollow.Checked;
            string str = this.ClientLanguage.SelectedItem.ToString();
            LazySettings.Language = (LazySettings.LazyLanguage) Enum.Parse(typeof(LazySettings.LazyLanguage), str);
            LazySettings.SaveSettings();
        }

        private void SetupCbBackgroundCheckedChanged(object sender, EventArgs e)
        {
            if ((this.SetupCBBackground.Checked && !LazySettings.HookMouse) && (MessageBox.Show("Enabling this will make the bot manipulate wow in a way that could be detected if warden gets an update. The chance of this getting detected is between now and never. You will have to decide for yourself.", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question).ToString() != "Yes"))
            {
                this.SetupCBBackground.Checked = false;
            }
        }

        private void Wizard1CancelButtonClick(object sender, CancelEventArgs e)
        {
            base.Close();
        }

        private void Wizard1FinishButtonClick(object sender, CancelEventArgs e)
        {
            LazySettings.SelectedCombat = this.SelectCombat.Text;
            LazySettings.SelectedEngine = this.SelectEngine.Text;
            LazySettings.FirstRun = false;
            LazySettings.SaveSettings();
            base.Close();
            Process.Start("Readme.htm");
        }

        private void Wizard1WizardPageChanged(object sender, WizardPageChangeEventArgs e)
        {
            switch (this.Wizard1.SelectedPageIndex)
            {
                case 0:
                    base.Size = new Size(0x225, 220);
                    return;

                case 1:
                    base.Size = new Size(0x225, 220);
                    return;

                case 2:
                    base.Size = new Size(0x235, 0x299);
                    return;

                case 3:
                    base.Size = new Size(0x235, 0x299);
                    return;

                case 4:
                    base.Size = new Size(0x235, 0x299);
                    return;

                case 5:
                    base.Size = new Size(0x235, 0x299);
                    return;

                case 6:
                    base.Size = new Size(0x235, 0x299);
                    return;

                case 7:
                    base.Size = new Size(0x235, 0x299);
                    return;

                case 8:
                    base.Size = new Size(0x235, 0x299);
                    return;

                case 9:
                    this.LoadSettings();
                    base.Size = new Size(0x235, 0x209);
                    return;

                case 10:
                    this.LoadCustomClasses();
                    this.LoadEngines();
                    base.Size = new Size(0x235, 0x1a5);
                    return;
            }
        }

        private void Wizard1WizardPageChanging(object sender, WizardCancelPageChangeEventArgs e)
        {
            if (this.Wizard1.SelectedPageIndex == 9)
            {
                this.SaveSettings();
            }
        }

        private void WizardLoad(object sender, EventArgs e)
        {
            base.Size = new Size(0x225, 220);
        }
    }
}

