namespace LazyEvo.Forms
{
    using DevComponents.AdvTree;
    using DevComponents.DotNetBar;
    using DevComponents.DotNetBar.Controls;
    using DevComponents.Editors;
    using LazyEvo.Classes;
    using LazyEvo.Forms.Helpers;
    using LazyEvo.Plugins;
    using LazyLib;
    using LazyLib.Helpers;
    using LazyLib.Helpers.Mail;
    using LazyLib.Helpers.Vendor;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    internal class Setup : Office2007Form
    {
        private IContainer components;
        private SuperTabControlPanel superTabControlPanel2;
        private GroupPanel groupPanel4;
        private SuperTabControlPanel superTabControlPanel8;
        private SuperTabItem superTabItem2;
        private ComboItem comboItem329;
        private ComboItem comboItem328;
        private ComboItem comboItem327;
        private ComboItem comboItem303;
        private ComboItem comboItem304;
        private ComboItem comboItem305;
        private ComboItem comboItem306;
        private ComboItem comboItem307;
        private ComboItem comboItem308;
        private ComboItem comboItem309;
        private ComboItem comboItem310;
        private ComboItem comboItem311;
        private ComboItem comboItem312;
        private ComboItem comboItem313;
        private ComboItem comboItem314;
        private ComboItem comboItem315;
        private ComboItem comboItem316;
        private ComboItem comboItem317;
        private ComboItem comboItem318;
        private ComboItem comboItem319;
        private ComboItem comboItem320;
        private ComboItem comboItem321;
        private ComboItem comboItem322;
        private ComboItem comboItem323;
        private ComboItem comboItem324;
        private ComboItem comboItem325;
        private LabelX labelX42;
        private ComboItem comboItem326;
        private ComboItem comboItem280;
        private ComboItem comboItem281;
        private ComboItem comboItem282;
        private ComboItem comboItem283;
        private ComboItem comboItem284;
        private ComboItem comboItem285;
        private ComboItem comboItem286;
        private ComboItem comboItem287;
        private ComboItem comboItem288;
        private ComboItem comboItem289;
        private ComboItem comboItem290;
        private ComboItem comboItem291;
        private ComboItem comboItem292;
        private ComboItem comboItem293;
        private ComboItem comboItem294;
        private ComboItem comboItem295;
        private ComboItem comboItem296;
        private ComboItem comboItem297;
        private ComboItem comboItem298;
        private ComboItem comboItem299;
        private ComboItem comboItem300;
        private ComboItem comboItem301;
        private ComboItem comboItem302;
        private LabelX labelX41;
        private ComboItem comboItem251;
        private ComboItem comboItem252;
        private ComboItem comboItem253;
        private ComboItem comboItem254;
        private ComboItem comboItem256;
        private ComboItem comboItem257;
        private ComboItem comboItem258;
        private ComboItem comboItem259;
        private ComboItem comboItem260;
        private ComboItem comboItem261;
        private ComboItem comboItem262;
        private ComboItem comboItem263;
        private ComboItem comboItem264;
        private ComboItem comboItem265;
        private ComboItem comboItem266;
        private ComboItem comboItem268;
        private ComboItem comboItem269;
        private ComboItem comboItem270;
        private ComboItem comboItem271;
        private ComboItem comboItem272;
        private ComboItem comboItem273;
        private ComboItem comboItem274;
        private ComboItem comboItem275;
        private LabelX labelX34;
        private LabelX labelX25;
        private LabelX labelX6;
        private LabelX labelX9;
        private LabelX labelX4;
        private LabelX labelX3;
        private ComboItem comboItem18;
        private ComboItem comboItem19;
        private ComboItem comboItem20;
        private ComboItem comboItem21;
        private ComboItem comboItem22;
        private ComboItem comboItem23;
        private ComboItem comboItem24;
        private ComboItem comboItem25;
        private ComboItem comboItem26;
        private ComboItem comboItem17;
        private LabelX labelX2;
        private ComboItem comboItem28;
        private ComboItem comboItem29;
        private ComboItem comboItem30;
        private ComboItem comboItem31;
        private ComboItem comboItem32;
        private ComboItem comboItem27;
        private ComboItem comboItem2;
        private ComboItem comboItem3;
        private ComboItem comboItem4;
        private ComboItem comboItem5;
        private ComboItem comboItem6;
        private ComboItem comboItem7;
        private ComboItem comboItem8;
        private ComboItem comboItem9;
        private ComboItem comboItem10;
        private ComboItem comboItem1;
        private LabelX labelX10;
        private ComboItem comboItem11;
        private ComboItem comboItem12;
        private ComboItem comboItem13;
        private ComboItem comboItem14;
        private ComboItem comboItem15;
        private ComboItem comboItem16;
        private ComboItem comboItem50;
        private ComboItem comboItem51;
        private ComboItem comboItem52;
        private ComboItem comboItem53;
        private ComboItem comboItem54;
        private ComboItem comboItem55;
        private ComboItem comboItem56;
        private ComboItem comboItem57;
        private ComboItem comboItem58;
        private ComboItem comboItem49;
        private LabelX labelX16;
        private ComboItem comboItem60;
        private ComboItem comboItem61;
        private ComboItem comboItem62;
        private ComboItem comboItem63;
        private ComboItem comboItem64;
        private ComboItem comboItem59;
        private LabelX labelX15;
        private LabelX labelX20;
        private ComboItem comboItem34;
        private ComboItem comboItem35;
        private ComboItem comboItem36;
        private ComboItem comboItem37;
        private ComboItem comboItem38;
        private ComboItem comboItem39;
        private ComboItem comboItem40;
        private ComboItem comboItem41;
        private ComboItem comboItem42;
        private ComboItem comboItem33;
        private LabelX labelX14;
        private ComboItem comboItem44;
        private ComboItem comboItem45;
        private ComboItem comboItem46;
        private ComboItem comboItem47;
        private ComboItem comboItem48;
        private ComboItem comboItem43;
        private LabelX labelX48;
        internal Label label12;
        internal Label label11;
        private ComboItem comboItem145;
        private ComboItem comboItem146;
        private ComboItem comboItem147;
        private ComboItem comboItem148;
        private ComboItem comboItem149;
        private ComboItem comboItem150;
        private ComboItem comboItem151;
        private ComboItem comboItem152;
        private ComboItem comboItem153;
        private ComboItem comboItem154;
        private ComboItem comboItem155;
        private ComboItem comboItem156;
        private ComboItem comboItem157;
        private ComboItem comboItem158;
        private ComboItem comboItem160;
        private ComboItem comboItem225;
        private ComboItem comboItem226;
        private ComboItem comboItem227;
        private ComboItem comboItem228;
        private ComboItem comboItem229;
        private ComboItem comboItem230;
        private ComboItem comboItem231;
        private LabelX labelX30;
        private ComboItem comboItem232;
        private IntegerInput CombatTBDrinkAt;
        private IntegerInput CombatTBEatAt;
        private LabelX labelX1;
        private StyleManager styleManager1;
        private SuperTabControl superTabControl2;
        private SuperTabControlPanel superTabControlPanel4;
        private SuperTabItem superTabItem4;
        private SuperTabControlPanel superTabControlPanel7;
        private SuperTabItem superTabItem7;
        private GroupPanel groupPanel7;
        private GroupPanel groupPanel6;
        private SuperTabControlPanel superTabControlPanel12;
        private SuperTabItem superTabItem11;
        private ButtonX buttonX1;
        private LabelX labelX12;
        private LabelX labelX21;
        private LabelX labelX19;
        private SuperTabControlPanel superTabControlPanel1;
        private SuperTabItem superTabItem1;
        private CheckedListBox PluginsList;
        private GroupPanel groupPanel1;
        private ComboItem comboItem65;
        private ComboItem comboItem66;
        private ComboItem comboItem67;
        private ComboItem comboItem68;
        private ComboItem comboItem69;
        private ComboItem comboItem70;
        private ComboItem comboItem71;
        private ComboItem comboItem72;
        private ComboItem comboItem73;
        private ComboItem comboItem74;
        private ComboItem comboItem75;
        private ComboItem comboItem76;
        private ComboItem comboItem77;
        private ComboItem comboItem78;
        private ComboItem comboItem79;
        private ComboItem comboItem80;
        private ComboItem comboItem81;
        private ComboItem comboItem82;
        private ComboItem comboItem83;
        private ComboItem comboItem84;
        private ComboItem comboItem85;
        private ComboItem comboItem86;
        private ComboItem comboItem87;
        private LabelX labelX13;
        private SuperTabControlPanel superTabControlPanel3;
        private SuperTabItem superTabItem3;
        private GroupPanel groupPanel3;
        private LabelX labelX8;
        private GroupPanel groupPanel2;
        private ButtonX BtnAddMailItem;
        private LabelX labelX18;
        private LabelX labelX22;
        private TextBoxX TBMailName;
        private LabelX labelX17;
        private TextBoxX TBMailTo;
        private ButtonX BtnRemoveMailItem;
        private SuperTabControlPanel superTabControlPanel5;
        private GroupPanel groupPanel8;
        private LabelX labelX23;
        private DevComponents.AdvTree.AdvTree ListProtectedItems;
        private NodeConnector nodeConnector1;
        private ElementStyle elementStyle1;
        private GroupPanel groupPanel9;
        private ButtonX BtnRemoveProtected;
        private TextBoxX TBProtectedName;
        private ButtonX BtnAddProtected;
        private LabelX labelX26;
        private LabelX labelX27;
        private SuperTabItem superTabItem5;
        private IntegerInput IMinFreeBagSlots;
        private LabelX labelX24;
        private CheckBoxX CBSellUnCommon;
        private CheckBoxX CBSellCommon;
        private CheckBoxX CBSellPoor;
        private DevComponents.AdvTree.AdvTree ListMailItems;
        private NodeConnector nodeConnector2;
        private ElementStyle elementStyle2;
        private SuperTabControlPanel superTabControlPanel6;
        private GroupPanel SetupRelogLoginData;
        private LabelX labelX32;
        private TextBoxX SetupTBRelogCharacter;
        private LabelX labelX28;
        private LabelX labelX29;
        private LabelX labelX31;
        private TextBoxX SetupTBRelogUsername;
        private TextBoxX SetupTBRelogPW;
        private SuperTabItem tabRelog;
        private CheckBoxX SetupCBRelogEnableRelogger;
        private CheckBoxX SetupCBRelogEnablePeriodicRelog;
        private IntegerInput SetupIIRelogLogInAfter;
        private LabelX labelX36;
        private LabelX labelX37;
        private IntegerInput SetupIIRelogLogOutAfter;
        private LabelX labelX35;
        private LabelX labelX33;
        private LabelX labelX38;
        private ButtonX buttonReloggerClearData;
        private LabelX labelX40;
        private SuperTooltip superTooltip1;
        private LabelX labelX43;
        private LabelX labelX39;
        private IntegerInput SetupIIRelogLogAccount;
        private CheckBoxX SetupDebugMode;
        private IntegerInput Latency;
        private LabelX labelX44;
        private CheckBoxX MacroForMail;
        private ComboItem bar1;
        private ComboItem bar2;
        private ComboItem bar3;
        private ComboItem bar4;
        private ComboItem bar5;
        private ComboItem bar0;
        private ComboItem key1;
        private ComboItem key2;
        private ComboItem key3;
        private ComboItem key4;
        private ComboItem key5;
        private ComboItem key6;
        private ComboItem key7;
        private ComboItem key8;
        private ComboItem key9;
        private ComboItem key0;
        private LabelX labelX45;
        private GroupPanel groupPanel10;
        private ComboItem comboItem94;
        private ComboItem comboItem95;
        private LabelX labelX46;
        private ComboItem comboItem88;
        private ComboItem comboItem89;
        private ComboItem comboItem90;
        private ComboBoxEx KeysStafeRightKey;
        private ComboBoxEx KeysStafeLeftKey;
        private ComboBoxEx KeysInteractKey;
        private ComboBoxEx KeysDrinkKey;
        private ComboBoxEx KeysDrinkBar;
        private ComboBoxEx KeysEatKey;
        private ComboBoxEx KeysEatBar;
        private ComboBoxEx KeysGroundMountKey;
        private ComboBoxEx KeysGroundMountBar;
        private ComboBoxEx KeysAttack1Key;
        private ComboBoxEx KeysAttack1Bar;
        private CheckBoxX SetupCBSoundStop;
        private CheckBoxX SetupCBSoundWhisper;
        private CheckBoxX SetupCBSoundFollow;
        private IntegerInput SetupTBStopAfter;
        private CheckBoxX SetupCBStopAfter;
        private CheckBoxX CombatCBDrink;
        private CheckBoxX CombatCBEat;
        private CheckBoxX SetupCBShutdown;
        private ComboBoxEx KeysInteractTarget;
        private CheckBoxX SetupCBBackground;
        private CheckBoxX SetupCBLogoutOnFollow;
        private IntegerInput SetupTBLogOutOnFollow;
        private CheckBoxX SetupUseHotkeys;
        private ComboBoxEx KeysTargetLast;
        private CheckBoxX CBMail;
        private CheckBoxX CBDoVendor;
        private CheckBoxX CBDoRepair;
        private CheckBoxX CBHookMouse;
        private ComboBoxEx KeysMailMacroBar;
        private ComboBoxEx KeysMailMacroKey;
        private ComboBoxEx ClientLanguage;

        public Setup()
        {
            this.InitializeComponent();
            base.Hide();
        }

        private void AddMail(string name)
        {
            Node node = new Node(name) {
                Tag = name
            };
            this.ListMailItems.BeginUpdate();
            this.ListMailItems.Nodes.Add(node);
            this.ListMailItems.EndUpdate();
        }

        private void AddProtected(string name)
        {
            Node node = new Node(name) {
                Tag = name
            };
            this.ListProtectedItems.BeginUpdate();
            this.ListProtectedItems.Nodes.Add(node);
            this.ListProtectedItems.EndUpdate();
        }

        private void BtnAddMailItemClick(object sender, EventArgs e)
        {
            if (this.TBMailName.Text != "")
            {
                this.AddMail(this.TBMailName.Text);
                this.TBMailName.Text = "";
            }
        }

        private void BtnAddProtected_Click(object sender, EventArgs e)
        {
            if (this.TBProtectedName.Text != "")
            {
                this.AddProtected(this.TBProtectedName.Text);
                this.TBProtectedName.Text = "";
            }
        }

        private void BtnRemoveMailItemClick(object sender, EventArgs e)
        {
            if (this.ListMailItems.SelectedNode != null)
            {
                this.ListMailItems.Nodes.Remove(this.ListMailItems.SelectedNode);
            }
        }

        private void BtnRemoveProtected_Click(object sender, EventArgs e)
        {
            if (this.ListProtectedItems.SelectedNode != null)
            {
                this.ListProtectedItems.Nodes.Remove(this.ListProtectedItems.SelectedNode);
            }
        }

        private void buttonReloggerClearData_Click(object sender, EventArgs e)
        {
            ReloggerSettings.AccountName = string.Empty;
            this.SetupTBRelogUsername.Text = string.Empty;
            ReloggerSettings.AccountPw = string.Empty;
            this.SetupTBRelogPW.Text = string.Empty;
            ReloggerSettings.AccountAccount = 1;
            ReloggerSettings._CharacterName = "";
            this.SetupTBRelogCharacter.Text = "";
            ReloggerSettings.PeriodicLogIn = 30;
            this.SetupIIRelogLogInAfter.Value = 30;
            ReloggerSettings.PeriodicLogOut = 60;
            this.SetupIIRelogLogOutAfter.Value = 60;
            ReloggerSettings.PeriodicReloggingEnabled = false;
            this.SetupCBRelogEnablePeriodicRelog.Checked = false;
            ReloggerSettings.ReloggingEnabled = false;
            this.SetupCBRelogEnableRelogger.Checked = false;
        }

        private void CBHookMouse_CheckedChanged(object sender, EventArgs e)
        {
            if (this.CBHookMouse.Checked != LazySettings.HookMouse)
            {
                if ((this.CBHookMouse.Checked && !LazySettings.HookMouse) && (MessageBox.Show("Enabling this will make the bot manipulate wow in a way that could be detected if warden gets an update. The chance of this getting detected is between now and never. You will have to decide for yourself.", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question).ToString() != "Yes"))
                {
                    this.CBHookMouse.Checked = false;
                }
                Logging.Write(LogType.Info, "Please restart the bot and the client for this to take effect", new object[0]);
            }
        }

        private void CBMail_CheckedChanged(object sender, EventArgs e)
        {
            if (!this.CBDoVendor.Checked && this.CBMail.Checked)
            {
                MessageBox.Show("You need to also enable 'To Town on full bags' in the settings (Vendor tab) to enable mailing");
            }
        }

        private void ClientLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void CloseForm(object sender, EventArgs e)
        {
            this.SaveSettings();
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

        public void DoRefresh()
        {
            if (base.InvokeRequired)
            {
                base.Invoke(new MethodInvoker(this.DoRefresh));
            }
            else
            {
                this.Refresh();
            }
        }

        public void EnableBtn(ButtonX buttonX)
        {
            MethodInvoker method = null;
            if (!buttonX.InvokeRequired)
            {
                buttonX.Enabled = true;
            }
            else
            {
                if (method == null)
                {
                    method = () => this.EnableBtn(buttonX);
                }
                buttonX.BeginInvoke(method);
            }
        }

        private void Form1Load(object sender, EventArgs e)
        {
            this.LoadSettings();
            this.HideAgain();
            this.PluginsList.Items.Clear();
            this.LoadP();
        }

        private void HideAgain()
        {
            MethodInvoker method = null;
            if (!base.InvokeRequired)
            {
                base.Hide();
            }
            else
            {
                if (method == null)
                {
                    method = () => this.HideAgain();
                }
                this.BeginInvoke(method);
            }
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            ComponentResourceManager manager = new ComponentResourceManager(typeof(Setup));
            this.superTabControlPanel8 = new SuperTabControlPanel();
            this.superTabItem2 = new SuperTabItem();
            this.groupPanel4 = new GroupPanel();
            this.superTabControlPanel2 = new SuperTabControlPanel();
            this.comboItem329 = new ComboItem();
            this.comboItem328 = new ComboItem();
            this.labelX1 = new LabelX();
            this.SetupCBBackground = new CheckBoxX();
            this.KeysInteractTarget = new ComboBoxEx();
            this.comboItem232 = new ComboItem();
            this.comboItem145 = new ComboItem();
            this.comboItem146 = new ComboItem();
            this.comboItem147 = new ComboItem();
            this.comboItem148 = new ComboItem();
            this.comboItem149 = new ComboItem();
            this.comboItem150 = new ComboItem();
            this.comboItem151 = new ComboItem();
            this.comboItem152 = new ComboItem();
            this.comboItem153 = new ComboItem();
            this.comboItem154 = new ComboItem();
            this.comboItem155 = new ComboItem();
            this.comboItem156 = new ComboItem();
            this.comboItem157 = new ComboItem();
            this.comboItem158 = new ComboItem();
            this.comboItem160 = new ComboItem();
            this.comboItem225 = new ComboItem();
            this.comboItem226 = new ComboItem();
            this.comboItem227 = new ComboItem();
            this.comboItem228 = new ComboItem();
            this.comboItem229 = new ComboItem();
            this.comboItem230 = new ComboItem();
            this.comboItem231 = new ComboItem();
            this.labelX30 = new LabelX();
            this.KeysStafeRightKey = new ComboBoxEx();
            this.comboItem327 = new ComboItem();
            this.comboItem303 = new ComboItem();
            this.comboItem304 = new ComboItem();
            this.comboItem305 = new ComboItem();
            this.comboItem306 = new ComboItem();
            this.comboItem307 = new ComboItem();
            this.comboItem308 = new ComboItem();
            this.comboItem309 = new ComboItem();
            this.comboItem310 = new ComboItem();
            this.comboItem311 = new ComboItem();
            this.comboItem312 = new ComboItem();
            this.comboItem313 = new ComboItem();
            this.comboItem314 = new ComboItem();
            this.comboItem315 = new ComboItem();
            this.comboItem316 = new ComboItem();
            this.comboItem317 = new ComboItem();
            this.comboItem318 = new ComboItem();
            this.comboItem319 = new ComboItem();
            this.comboItem320 = new ComboItem();
            this.comboItem321 = new ComboItem();
            this.comboItem322 = new ComboItem();
            this.comboItem323 = new ComboItem();
            this.comboItem324 = new ComboItem();
            this.comboItem325 = new ComboItem();
            this.labelX42 = new LabelX();
            this.KeysStafeLeftKey = new ComboBoxEx();
            this.comboItem326 = new ComboItem();
            this.comboItem280 = new ComboItem();
            this.comboItem281 = new ComboItem();
            this.comboItem282 = new ComboItem();
            this.comboItem283 = new ComboItem();
            this.comboItem284 = new ComboItem();
            this.comboItem285 = new ComboItem();
            this.comboItem286 = new ComboItem();
            this.comboItem287 = new ComboItem();
            this.comboItem288 = new ComboItem();
            this.comboItem289 = new ComboItem();
            this.comboItem290 = new ComboItem();
            this.comboItem291 = new ComboItem();
            this.comboItem292 = new ComboItem();
            this.comboItem293 = new ComboItem();
            this.comboItem294 = new ComboItem();
            this.comboItem295 = new ComboItem();
            this.comboItem296 = new ComboItem();
            this.comboItem297 = new ComboItem();
            this.comboItem298 = new ComboItem();
            this.comboItem299 = new ComboItem();
            this.comboItem300 = new ComboItem();
            this.comboItem301 = new ComboItem();
            this.comboItem302 = new ComboItem();
            this.labelX41 = new LabelX();
            this.KeysInteractKey = new ComboBoxEx();
            this.comboItem251 = new ComboItem();
            this.comboItem252 = new ComboItem();
            this.comboItem253 = new ComboItem();
            this.comboItem254 = new ComboItem();
            this.comboItem256 = new ComboItem();
            this.comboItem257 = new ComboItem();
            this.comboItem258 = new ComboItem();
            this.comboItem259 = new ComboItem();
            this.comboItem260 = new ComboItem();
            this.comboItem261 = new ComboItem();
            this.comboItem262 = new ComboItem();
            this.comboItem263 = new ComboItem();
            this.comboItem264 = new ComboItem();
            this.comboItem265 = new ComboItem();
            this.comboItem266 = new ComboItem();
            this.comboItem268 = new ComboItem();
            this.comboItem269 = new ComboItem();
            this.comboItem270 = new ComboItem();
            this.comboItem271 = new ComboItem();
            this.comboItem272 = new ComboItem();
            this.comboItem273 = new ComboItem();
            this.comboItem274 = new ComboItem();
            this.comboItem275 = new ComboItem();
            this.labelX34 = new LabelX();
            this.labelX25 = new LabelX();
            this.labelX6 = new LabelX();
            this.labelX9 = new LabelX();
            this.labelX4 = new LabelX();
            this.labelX3 = new LabelX();
            this.KeysDrinkKey = new ComboBoxEx();
            this.comboItem18 = new ComboItem();
            this.comboItem19 = new ComboItem();
            this.comboItem20 = new ComboItem();
            this.comboItem21 = new ComboItem();
            this.comboItem22 = new ComboItem();
            this.comboItem23 = new ComboItem();
            this.comboItem24 = new ComboItem();
            this.comboItem25 = new ComboItem();
            this.comboItem26 = new ComboItem();
            this.comboItem17 = new ComboItem();
            this.labelX2 = new LabelX();
            this.KeysDrinkBar = new ComboBoxEx();
            this.comboItem28 = new ComboItem();
            this.comboItem29 = new ComboItem();
            this.comboItem30 = new ComboItem();
            this.comboItem31 = new ComboItem();
            this.comboItem32 = new ComboItem();
            this.comboItem27 = new ComboItem();
            this.KeysEatKey = new ComboBoxEx();
            this.comboItem2 = new ComboItem();
            this.comboItem3 = new ComboItem();
            this.comboItem4 = new ComboItem();
            this.comboItem5 = new ComboItem();
            this.comboItem6 = new ComboItem();
            this.comboItem7 = new ComboItem();
            this.comboItem8 = new ComboItem();
            this.comboItem9 = new ComboItem();
            this.comboItem10 = new ComboItem();
            this.comboItem1 = new ComboItem();
            this.labelX10 = new LabelX();
            this.KeysEatBar = new ComboBoxEx();
            this.comboItem11 = new ComboItem();
            this.comboItem12 = new ComboItem();
            this.comboItem13 = new ComboItem();
            this.comboItem14 = new ComboItem();
            this.comboItem15 = new ComboItem();
            this.comboItem16 = new ComboItem();
            this.KeysGroundMountKey = new ComboBoxEx();
            this.comboItem50 = new ComboItem();
            this.comboItem51 = new ComboItem();
            this.comboItem52 = new ComboItem();
            this.comboItem53 = new ComboItem();
            this.comboItem54 = new ComboItem();
            this.comboItem55 = new ComboItem();
            this.comboItem56 = new ComboItem();
            this.comboItem57 = new ComboItem();
            this.comboItem58 = new ComboItem();
            this.comboItem49 = new ComboItem();
            this.labelX16 = new LabelX();
            this.KeysGroundMountBar = new ComboBoxEx();
            this.comboItem60 = new ComboItem();
            this.comboItem61 = new ComboItem();
            this.comboItem62 = new ComboItem();
            this.comboItem63 = new ComboItem();
            this.comboItem64 = new ComboItem();
            this.comboItem59 = new ComboItem();
            this.labelX15 = new LabelX();
            this.labelX20 = new LabelX();
            this.KeysAttack1Key = new ComboBoxEx();
            this.comboItem34 = new ComboItem();
            this.comboItem35 = new ComboItem();
            this.comboItem36 = new ComboItem();
            this.comboItem37 = new ComboItem();
            this.comboItem38 = new ComboItem();
            this.comboItem39 = new ComboItem();
            this.comboItem40 = new ComboItem();
            this.comboItem41 = new ComboItem();
            this.comboItem42 = new ComboItem();
            this.comboItem33 = new ComboItem();
            this.labelX14 = new LabelX();
            this.KeysAttack1Bar = new ComboBoxEx();
            this.comboItem44 = new ComboItem();
            this.comboItem45 = new ComboItem();
            this.comboItem46 = new ComboItem();
            this.comboItem47 = new ComboItem();
            this.comboItem48 = new ComboItem();
            this.comboItem43 = new ComboItem();
            this.CombatTBDrinkAt = new IntegerInput();
            this.CombatTBEatAt = new IntegerInput();
            this.SetupCBShutdown = new CheckBoxX();
            this.CombatCBDrink = new CheckBoxX();
            this.CombatCBEat = new CheckBoxX();
            this.label12 = new Label();
            this.label11 = new Label();
            this.labelX48 = new LabelX();
            this.SetupTBStopAfter = new IntegerInput();
            this.SetupCBStopAfter = new CheckBoxX();
            this.SetupCBSoundStop = new CheckBoxX();
            this.SetupCBSoundWhisper = new CheckBoxX();
            this.SetupCBSoundFollow = new CheckBoxX();
            this.styleManager1 = new StyleManager(this.components);
            this.superTabControl2 = new SuperTabControl();
            this.superTabControlPanel4 = new SuperTabControlPanel();
            this.groupPanel10 = new GroupPanel();
            this.ClientLanguage = new ComboBoxEx();
            this.comboItem94 = new ComboItem();
            this.comboItem95 = new ComboItem();
            this.comboItem88 = new ComboItem();
            this.comboItem89 = new ComboItem();
            this.comboItem90 = new ComboItem();
            this.labelX46 = new LabelX();
            this.Latency = new IntegerInput();
            this.labelX44 = new LabelX();
            this.SetupDebugMode = new CheckBoxX();
            this.CBHookMouse = new CheckBoxX();
            this.labelX21 = new LabelX();
            this.labelX19 = new LabelX();
            this.SetupUseHotkeys = new CheckBoxX();
            this.superTabItem4 = new SuperTabItem();
            this.superTabControlPanel3 = new SuperTabControlPanel();
            this.groupPanel3 = new GroupPanel();
            this.ListMailItems = new DevComponents.AdvTree.AdvTree();
            this.nodeConnector2 = new NodeConnector();
            this.elementStyle2 = new ElementStyle();
            this.labelX8 = new LabelX();
            this.groupPanel2 = new GroupPanel();
            this.KeysMailMacroBar = new ComboBoxEx();
            this.bar1 = new ComboItem();
            this.bar2 = new ComboItem();
            this.bar3 = new ComboItem();
            this.bar4 = new ComboItem();
            this.bar5 = new ComboItem();
            this.bar0 = new ComboItem();
            this.KeysMailMacroKey = new ComboBoxEx();
            this.key1 = new ComboItem();
            this.key2 = new ComboItem();
            this.key3 = new ComboItem();
            this.key4 = new ComboItem();
            this.key5 = new ComboItem();
            this.key6 = new ComboItem();
            this.key7 = new ComboItem();
            this.key8 = new ComboItem();
            this.key9 = new ComboItem();
            this.key0 = new ComboItem();
            this.MacroForMail = new CheckBoxX();
            this.labelX45 = new LabelX();
            this.labelX40 = new LabelX();
            this.BtnRemoveMailItem = new ButtonX();
            this.TBMailTo = new TextBoxX();
            this.labelX17 = new LabelX();
            this.TBMailName = new TextBoxX();
            this.CBMail = new CheckBoxX();
            this.BtnAddMailItem = new ButtonX();
            this.labelX18 = new LabelX();
            this.labelX22 = new LabelX();
            this.superTabItem3 = new SuperTabItem();
            this.superTabControlPanel6 = new SuperTabControlPanel();
            this.labelX38 = new LabelX();
            this.SetupCBRelogEnableRelogger = new CheckBoxX();
            this.SetupRelogLoginData = new GroupPanel();
            this.SetupIIRelogLogAccount = new IntegerInput();
            this.labelX43 = new LabelX();
            this.labelX39 = new LabelX();
            this.buttonReloggerClearData = new ButtonX();
            this.SetupIIRelogLogInAfter = new IntegerInput();
            this.labelX36 = new LabelX();
            this.labelX37 = new LabelX();
            this.SetupIIRelogLogOutAfter = new IntegerInput();
            this.labelX35 = new LabelX();
            this.labelX33 = new LabelX();
            this.SetupCBRelogEnablePeriodicRelog = new CheckBoxX();
            this.labelX32 = new LabelX();
            this.SetupTBRelogCharacter = new TextBoxX();
            this.labelX28 = new LabelX();
            this.labelX29 = new LabelX();
            this.labelX31 = new LabelX();
            this.SetupTBRelogUsername = new TextBoxX();
            this.SetupTBRelogPW = new TextBoxX();
            this.tabRelog = new SuperTabItem();
            this.superTabControlPanel1 = new SuperTabControlPanel();
            this.PluginsList = new CheckedListBox();
            this.superTabItem1 = new SuperTabItem();
            this.superTabControlPanel12 = new SuperTabControlPanel();
            this.groupPanel1 = new GroupPanel();
            this.KeysTargetLast = new ComboBoxEx();
            this.comboItem65 = new ComboItem();
            this.comboItem66 = new ComboItem();
            this.comboItem67 = new ComboItem();
            this.comboItem68 = new ComboItem();
            this.comboItem69 = new ComboItem();
            this.comboItem70 = new ComboItem();
            this.comboItem71 = new ComboItem();
            this.comboItem72 = new ComboItem();
            this.comboItem73 = new ComboItem();
            this.comboItem74 = new ComboItem();
            this.comboItem75 = new ComboItem();
            this.comboItem76 = new ComboItem();
            this.comboItem77 = new ComboItem();
            this.comboItem78 = new ComboItem();
            this.comboItem79 = new ComboItem();
            this.comboItem80 = new ComboItem();
            this.comboItem81 = new ComboItem();
            this.comboItem82 = new ComboItem();
            this.comboItem83 = new ComboItem();
            this.comboItem84 = new ComboItem();
            this.comboItem85 = new ComboItem();
            this.comboItem86 = new ComboItem();
            this.comboItem87 = new ComboItem();
            this.labelX13 = new LabelX();
            this.superTabItem11 = new SuperTabItem();
            this.superTabControlPanel5 = new SuperTabControlPanel();
            this.groupPanel8 = new GroupPanel();
            this.labelX23 = new LabelX();
            this.ListProtectedItems = new DevComponents.AdvTree.AdvTree();
            this.nodeConnector1 = new NodeConnector();
            this.elementStyle1 = new ElementStyle();
            this.groupPanel9 = new GroupPanel();
            this.IMinFreeBagSlots = new IntegerInput();
            this.labelX24 = new LabelX();
            this.CBSellUnCommon = new CheckBoxX();
            this.CBSellCommon = new CheckBoxX();
            this.CBSellPoor = new CheckBoxX();
            this.CBDoRepair = new CheckBoxX();
            this.BtnRemoveProtected = new ButtonX();
            this.TBProtectedName = new TextBoxX();
            this.CBDoVendor = new CheckBoxX();
            this.BtnAddProtected = new ButtonX();
            this.labelX26 = new LabelX();
            this.labelX27 = new LabelX();
            this.superTabItem5 = new SuperTabItem();
            this.superTabControlPanel7 = new SuperTabControlPanel();
            this.groupPanel7 = new GroupPanel();
            this.labelX12 = new LabelX();
            this.SetupTBLogOutOnFollow = new IntegerInput();
            this.SetupCBLogoutOnFollow = new CheckBoxX();
            this.groupPanel6 = new GroupPanel();
            this.superTabItem7 = new SuperTabItem();
            this.buttonX1 = new ButtonX();
            this.superTooltip1 = new SuperTooltip();
            this.superTabControlPanel2.SuspendLayout();
            this.CombatTBDrinkAt.BeginInit();
            this.CombatTBEatAt.BeginInit();
            this.SetupTBStopAfter.BeginInit();
            ((ISupportInitialize) this.superTabControl2).BeginInit();
            this.superTabControl2.SuspendLayout();
            this.superTabControlPanel4.SuspendLayout();
            this.groupPanel10.SuspendLayout();
            this.Latency.BeginInit();
            this.superTabControlPanel3.SuspendLayout();
            this.groupPanel3.SuspendLayout();
            this.ListMailItems.BeginInit();
            this.groupPanel2.SuspendLayout();
            this.superTabControlPanel6.SuspendLayout();
            this.SetupRelogLoginData.SuspendLayout();
            this.SetupIIRelogLogAccount.BeginInit();
            this.SetupIIRelogLogInAfter.BeginInit();
            this.SetupIIRelogLogOutAfter.BeginInit();
            this.superTabControlPanel1.SuspendLayout();
            this.superTabControlPanel12.SuspendLayout();
            this.groupPanel1.SuspendLayout();
            this.superTabControlPanel5.SuspendLayout();
            this.groupPanel8.SuspendLayout();
            this.ListProtectedItems.BeginInit();
            this.groupPanel9.SuspendLayout();
            this.IMinFreeBagSlots.BeginInit();
            this.superTabControlPanel7.SuspendLayout();
            this.groupPanel7.SuspendLayout();
            this.SetupTBLogOutOnFollow.BeginInit();
            this.groupPanel6.SuspendLayout();
            base.SuspendLayout();
            this.superTabControlPanel8.Dock = DockStyle.Fill;
            this.superTabControlPanel8.Location = new Point(0, 0);
            this.superTabControlPanel8.Name = "superTabControlPanel8";
            this.superTabControlPanel8.Size = new Size(0x165, 0x1d7);
            this.superTabControlPanel8.TabIndex = 0;
            this.superTabControlPanel8.TabItem = this.superTabItem2;
            this.superTabItem2.AttachedControl = this.superTabControlPanel8;
            this.superTabItem2.GlobalItem = false;
            this.superTabItem2.Name = "superTabItem2";
            this.superTabItem2.TabStripItem = null;
            this.superTabItem2.Text = "Config";
            this.groupPanel4.Location = new Point(0, 0);
            this.groupPanel4.Name = "groupPanel4";
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
            this.groupPanel4.TabIndex = 0;
            this.groupPanel4.Text = "Setup";
            this.superTabControlPanel2.Controls.Add(this.groupPanel4);
            this.superTabControlPanel2.Dock = DockStyle.Fill;
            this.superTabControlPanel2.Location = new Point(0, 0);
            this.superTabControlPanel2.Name = "superTabControlPanel2";
            this.superTabControlPanel2.Size = new Size(0x165, 0x1d7);
            this.superTabControlPanel2.TabIndex = 0;
            this.comboItem329.Text = "Flying gathering";
            this.comboItem328.Text = "Grinding";
            this.labelX1.BackColor = Color.Transparent;
            this.labelX1.BackgroundStyle.Class = "";
            this.labelX1.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX1.Location = new Point(8, 0x3d);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new Size(0x72, 0x17);
            this.labelX1.TabIndex = 0xad;
            this.labelX1.Text = "<b>Background mode:</b>";
            this.SetupCBBackground.AutoSize = true;
            this.SetupCBBackground.BackColor = Color.Transparent;
            this.SetupCBBackground.BackgroundStyle.Class = "";
            this.SetupCBBackground.BackgroundStyle.CornerType = eCornerType.Square;
            this.SetupCBBackground.Location = new Point(8, 0x6b);
            this.SetupCBBackground.Name = "SetupCBBackground";
            this.SetupCBBackground.Size = new Size(0x86, 15);
            this.SetupCBBackground.TabIndex = 0xac;
            this.SetupCBBackground.Text = "Enable memory writing";
            this.SetupCBBackground.CheckedChanged += new EventHandler(this.SetupCbBackgroundCheckedChanged);
            this.KeysInteractTarget.DisplayMember = "Text";
            this.KeysInteractTarget.DrawMode = DrawMode.OwnerDrawFixed;
            this.KeysInteractTarget.DropDownStyle = ComboBoxStyle.DropDownList;
            this.KeysInteractTarget.FlatStyle = FlatStyle.Flat;
            this.KeysInteractTarget.FormattingEnabled = true;
            this.KeysInteractTarget.ItemHeight = 14;
            object[] items = new object[] { this.comboItem232, this.comboItem145, this.comboItem146, this.comboItem147, this.comboItem148, this.comboItem149, this.comboItem150, this.comboItem151, this.comboItem152 };
            items[9] = this.comboItem153;
            items[10] = this.comboItem154;
            items[11] = this.comboItem155;
            items[12] = this.comboItem156;
            items[13] = this.comboItem157;
            items[14] = this.comboItem158;
            items[15] = this.comboItem160;
            items[0x10] = this.comboItem225;
            items[0x11] = this.comboItem226;
            items[0x12] = this.comboItem227;
            items[0x13] = this.comboItem228;
            items[20] = this.comboItem229;
            items[0x15] = this.comboItem230;
            items[0x16] = this.comboItem231;
            this.KeysInteractTarget.Items.AddRange(items);
            this.KeysInteractTarget.Location = new Point(310, 0x4b);
            this.KeysInteractTarget.Name = "KeysInteractTarget";
            this.KeysInteractTarget.Size = new Size(0x27, 20);
            this.KeysInteractTarget.Style = eDotNetBarStyle.StyleManagerControlled;
            this.KeysInteractTarget.TabIndex = 0x141;
            this.comboItem232.Text = "P";
            this.comboItem145.Text = "A";
            this.comboItem146.Text = "B";
            this.comboItem147.Text = "C";
            this.comboItem148.Text = "D";
            this.comboItem149.Text = "F";
            this.comboItem150.Text = "G";
            this.comboItem151.Text = "H";
            this.comboItem152.Text = "I";
            this.comboItem153.Text = "J";
            this.comboItem154.Text = "K";
            this.comboItem155.Text = "L";
            this.comboItem156.Text = "M";
            this.comboItem157.Text = "N";
            this.comboItem158.Text = "O";
            this.comboItem160.Text = "R";
            this.comboItem225.Text = "S";
            this.comboItem226.Text = "T";
            this.comboItem227.Text = "U";
            this.comboItem228.Text = "V";
            this.comboItem229.Text = "X";
            this.comboItem230.Text = "Y";
            this.comboItem231.Text = "Z";
            this.labelX30.BackColor = Color.Transparent;
            this.labelX30.BackgroundStyle.Class = "";
            this.labelX30.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX30.Location = new Point(180, 0x49);
            this.labelX30.Name = "labelX30";
            this.labelX30.Size = new Size(0x7e, 0x17);
            this.labelX30.TabIndex = 320;
            this.labelX30.Text = "Interact with target:";
            this.KeysStafeRightKey.DisplayMember = "Text";
            this.KeysStafeRightKey.DrawMode = DrawMode.OwnerDrawFixed;
            this.KeysStafeRightKey.DropDownStyle = ComboBoxStyle.DropDownList;
            this.KeysStafeRightKey.FlatStyle = FlatStyle.Flat;
            this.KeysStafeRightKey.FormattingEnabled = true;
            this.KeysStafeRightKey.ItemHeight = 14;
            object[] objArray2 = new object[] { this.comboItem327, this.comboItem303, this.comboItem304, this.comboItem305, this.comboItem306, this.comboItem307, this.comboItem308, this.comboItem309, this.comboItem310 };
            objArray2[9] = this.comboItem311;
            objArray2[10] = this.comboItem312;
            objArray2[11] = this.comboItem313;
            objArray2[12] = this.comboItem314;
            objArray2[13] = this.comboItem315;
            objArray2[14] = this.comboItem316;
            objArray2[15] = this.comboItem317;
            objArray2[0x10] = this.comboItem318;
            objArray2[0x11] = this.comboItem319;
            objArray2[0x12] = this.comboItem320;
            objArray2[0x13] = this.comboItem321;
            objArray2[20] = this.comboItem322;
            objArray2[0x15] = this.comboItem323;
            objArray2[0x16] = this.comboItem324;
            objArray2[0x17] = this.comboItem325;
            this.KeysStafeRightKey.Items.AddRange(objArray2);
            this.KeysStafeRightKey.Location = new Point(310, 0x7d);
            this.KeysStafeRightKey.Name = "KeysStafeRightKey";
            this.KeysStafeRightKey.Size = new Size(0x27, 20);
            this.KeysStafeRightKey.Style = eDotNetBarStyle.StyleManagerControlled;
            this.KeysStafeRightKey.TabIndex = 0x13f;
            this.comboItem327.Text = "E";
            this.comboItem303.Text = "A";
            this.comboItem304.Text = "B";
            this.comboItem305.Text = "C";
            this.comboItem306.Text = "D";
            this.comboItem307.Text = "F";
            this.comboItem308.Text = "G";
            this.comboItem309.Text = "H";
            this.comboItem310.Text = "I";
            this.comboItem311.Text = "J";
            this.comboItem312.Text = "K";
            this.comboItem313.Text = "L";
            this.comboItem314.Text = "M";
            this.comboItem315.Text = "N";
            this.comboItem316.Text = "O";
            this.comboItem317.Text = "P";
            this.comboItem318.Text = "R";
            this.comboItem319.Text = "S";
            this.comboItem320.Text = "T";
            this.comboItem321.Text = "U";
            this.comboItem322.Text = "V";
            this.comboItem323.Text = "X";
            this.comboItem324.Text = "Y";
            this.comboItem325.Text = "Z";
            this.labelX42.BackColor = Color.Transparent;
            this.labelX42.BackgroundStyle.Class = "";
            this.labelX42.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX42.Location = new Point(180, 0x7b);
            this.labelX42.Name = "labelX42";
            this.labelX42.Size = new Size(0x7e, 0x17);
            this.labelX42.TabIndex = 0x13e;
            this.labelX42.Text = "Stafe Right:";
            this.KeysStafeLeftKey.DisplayMember = "Text";
            this.KeysStafeLeftKey.DrawMode = DrawMode.OwnerDrawFixed;
            this.KeysStafeLeftKey.DropDownStyle = ComboBoxStyle.DropDownList;
            this.KeysStafeLeftKey.FlatStyle = FlatStyle.Flat;
            this.KeysStafeLeftKey.FormattingEnabled = true;
            this.KeysStafeLeftKey.ItemHeight = 14;
            object[] objArray3 = new object[] { this.comboItem326, this.comboItem280, this.comboItem281, this.comboItem282, this.comboItem283, this.comboItem284, this.comboItem285, this.comboItem286, this.comboItem287 };
            objArray3[9] = this.comboItem288;
            objArray3[10] = this.comboItem289;
            objArray3[11] = this.comboItem290;
            objArray3[12] = this.comboItem291;
            objArray3[13] = this.comboItem292;
            objArray3[14] = this.comboItem293;
            objArray3[15] = this.comboItem294;
            objArray3[0x10] = this.comboItem295;
            objArray3[0x11] = this.comboItem296;
            objArray3[0x12] = this.comboItem297;
            objArray3[0x13] = this.comboItem298;
            objArray3[20] = this.comboItem299;
            objArray3[0x15] = this.comboItem300;
            objArray3[0x16] = this.comboItem301;
            objArray3[0x17] = this.comboItem302;
            this.KeysStafeLeftKey.Items.AddRange(objArray3);
            this.KeysStafeLeftKey.Location = new Point(310, 100);
            this.KeysStafeLeftKey.Name = "KeysStafeLeftKey";
            this.KeysStafeLeftKey.Size = new Size(0x27, 20);
            this.KeysStafeLeftKey.Style = eDotNetBarStyle.StyleManagerControlled;
            this.KeysStafeLeftKey.TabIndex = 0x13d;
            this.comboItem326.Text = "Q";
            this.comboItem280.Text = "A";
            this.comboItem281.Text = "B";
            this.comboItem282.Text = "C";
            this.comboItem283.Text = "D";
            this.comboItem284.Text = "F";
            this.comboItem285.Text = "G";
            this.comboItem286.Text = "H";
            this.comboItem287.Text = "I";
            this.comboItem288.Text = "J";
            this.comboItem289.Text = "K";
            this.comboItem290.Text = "L";
            this.comboItem291.Text = "M";
            this.comboItem292.Text = "N";
            this.comboItem293.Text = "O";
            this.comboItem294.Text = "P";
            this.comboItem295.Text = "R";
            this.comboItem296.Text = "S";
            this.comboItem297.Text = "T";
            this.comboItem298.Text = "U";
            this.comboItem299.Text = "V";
            this.comboItem300.Text = "X";
            this.comboItem301.Text = "Y";
            this.comboItem302.Text = "Z";
            this.labelX41.BackColor = Color.Transparent;
            this.labelX41.BackgroundStyle.Class = "";
            this.labelX41.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX41.Location = new Point(180, 0x62);
            this.labelX41.Name = "labelX41";
            this.labelX41.Size = new Size(0x7e, 0x17);
            this.labelX41.TabIndex = 0x13c;
            this.labelX41.Text = "Stafe Left:";
            this.KeysInteractKey.DisplayMember = "Text";
            this.KeysInteractKey.DrawMode = DrawMode.OwnerDrawFixed;
            this.KeysInteractKey.DropDownStyle = ComboBoxStyle.DropDownList;
            this.KeysInteractKey.FlatStyle = FlatStyle.Flat;
            this.KeysInteractKey.FormattingEnabled = true;
            this.KeysInteractKey.ItemHeight = 14;
            object[] objArray4 = new object[] { this.comboItem251, this.comboItem252, this.comboItem253, this.comboItem254, this.comboItem256, this.comboItem257, this.comboItem258, this.comboItem259, this.comboItem260 };
            objArray4[9] = this.comboItem261;
            objArray4[10] = this.comboItem262;
            objArray4[11] = this.comboItem263;
            objArray4[12] = this.comboItem264;
            objArray4[13] = this.comboItem265;
            objArray4[14] = this.comboItem266;
            objArray4[15] = this.comboItem268;
            objArray4[0x10] = this.comboItem269;
            objArray4[0x11] = this.comboItem270;
            objArray4[0x12] = this.comboItem271;
            objArray4[0x13] = this.comboItem272;
            objArray4[20] = this.comboItem273;
            objArray4[0x15] = this.comboItem274;
            objArray4[0x16] = this.comboItem275;
            this.KeysInteractKey.Items.AddRange(objArray4);
            this.KeysInteractKey.Location = new Point(310, 0x1b);
            this.KeysInteractKey.Name = "KeysInteractKey";
            this.KeysInteractKey.Size = new Size(0x27, 20);
            this.KeysInteractKey.Style = eDotNetBarStyle.StyleManagerControlled;
            this.KeysInteractKey.TabIndex = 0x138;
            this.comboItem251.Text = "A";
            this.comboItem252.Text = "B";
            this.comboItem253.Text = "C";
            this.comboItem254.Text = "D";
            this.comboItem256.Text = "F";
            this.comboItem257.Text = "G";
            this.comboItem258.Text = "H";
            this.comboItem259.Text = "I";
            this.comboItem260.Text = "J";
            this.comboItem261.Text = "K";
            this.comboItem262.Text = "L";
            this.comboItem263.Text = "M";
            this.comboItem264.Text = "N";
            this.comboItem265.Text = "O";
            this.comboItem266.Text = "P";
            this.comboItem268.Text = "R";
            this.comboItem269.Text = "S";
            this.comboItem270.Text = "T";
            this.comboItem271.Text = "U";
            this.comboItem272.Text = "V";
            this.comboItem273.Text = "X";
            this.comboItem274.Text = "Y";
            this.comboItem275.Text = "Z";
            this.labelX34.BackColor = Color.Transparent;
            this.labelX34.BackgroundStyle.Class = "";
            this.labelX34.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX34.Location = new Point(180, 0x19);
            this.labelX34.Name = "labelX34";
            this.labelX34.Size = new Size(0x7e, 0x17);
            this.labelX34.TabIndex = 0x137;
            this.labelX34.Text = "Interact with mouseover:";
            this.labelX25.BackColor = Color.Transparent;
            this.labelX25.BackgroundStyle.Class = "";
            this.labelX25.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX25.Location = new Point(180, 5);
            this.labelX25.Name = "labelX25";
            this.labelX25.Size = new Size(50, 0x17);
            this.labelX25.TabIndex = 0x12d;
            this.labelX25.Text = "<u>Other:</u>";
            this.labelX6.BackColor = Color.Transparent;
            this.labelX6.BackgroundStyle.Class = "";
            this.labelX6.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX6.Location = new Point(0x7e, 180);
            this.labelX6.Name = "labelX6";
            this.labelX6.Size = new Size(0x27, 0x17);
            this.labelX6.TabIndex = 0x11e;
            this.labelX6.Text = "Key:";
            this.labelX9.BackColor = Color.Transparent;
            this.labelX9.BackgroundStyle.Class = "";
            this.labelX9.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX9.Location = new Point(0x4a, 0xb5);
            this.labelX9.Name = "labelX9";
            this.labelX9.Size = new Size(0x27, 0x17);
            this.labelX9.TabIndex = 0x11d;
            this.labelX9.Text = "Bar:";
            this.labelX4.BackColor = Color.Transparent;
            this.labelX4.BackgroundStyle.Class = "";
            this.labelX4.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX4.Location = new Point(0x86, 0);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new Size(0x27, 0x17);
            this.labelX4.TabIndex = 0x110;
            this.labelX4.Text = "Keys:";
            this.labelX3.BackColor = Color.Transparent;
            this.labelX3.BackgroundStyle.Class = "";
            this.labelX3.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX3.Location = new Point(0x52, 0);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new Size(0x27, 0x17);
            this.labelX3.TabIndex = 0x10f;
            this.labelX3.Text = "Bar:";
            this.KeysDrinkKey.DisplayMember = "Text";
            this.KeysDrinkKey.DrawMode = DrawMode.OwnerDrawFixed;
            this.KeysDrinkKey.DropDownStyle = ComboBoxStyle.DropDownList;
            this.KeysDrinkKey.FlatStyle = FlatStyle.Flat;
            this.KeysDrinkKey.FormattingEnabled = true;
            this.KeysDrinkKey.ItemHeight = 14;
            object[] objArray5 = new object[] { this.comboItem18, this.comboItem19, this.comboItem20, this.comboItem21, this.comboItem22, this.comboItem23, this.comboItem24, this.comboItem25, this.comboItem26 };
            objArray5[9] = this.comboItem17;
            this.KeysDrinkKey.Items.AddRange(objArray5);
            this.KeysDrinkKey.Location = new Point(0x87, 0x37);
            this.KeysDrinkKey.Name = "KeysDrinkKey";
            this.KeysDrinkKey.Size = new Size(0x27, 20);
            this.KeysDrinkKey.Style = eDotNetBarStyle.StyleManagerControlled;
            this.KeysDrinkKey.TabIndex = 270;
            this.comboItem18.Text = "1";
            this.comboItem19.Text = "2";
            this.comboItem20.Text = "3";
            this.comboItem21.Text = "4";
            this.comboItem22.Text = "5";
            this.comboItem23.Text = "6";
            this.comboItem24.Text = "7";
            this.comboItem25.Text = "8";
            this.comboItem26.Text = "9";
            this.comboItem17.Text = "0";
            this.labelX2.BackColor = Color.Transparent;
            this.labelX2.BackgroundStyle.Class = "";
            this.labelX2.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX2.Location = new Point(3, 0x34);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new Size(50, 0x17);
            this.labelX2.TabIndex = 0x10d;
            this.labelX2.Text = "Drink:";
            this.KeysDrinkBar.DisplayMember = "Text";
            this.KeysDrinkBar.DrawMode = DrawMode.OwnerDrawFixed;
            this.KeysDrinkBar.DropDownStyle = ComboBoxStyle.DropDownList;
            this.KeysDrinkBar.FlatStyle = FlatStyle.Flat;
            this.KeysDrinkBar.FormattingEnabled = true;
            this.KeysDrinkBar.ItemHeight = 14;
            object[] objArray6 = new object[] { this.comboItem28, this.comboItem29, this.comboItem30, this.comboItem31, this.comboItem32, this.comboItem27 };
            this.KeysDrinkBar.Items.AddRange(objArray6);
            this.KeysDrinkBar.Location = new Point(0x53, 0x37);
            this.KeysDrinkBar.Name = "KeysDrinkBar";
            this.KeysDrinkBar.Size = new Size(0x27, 20);
            this.KeysDrinkBar.Style = eDotNetBarStyle.StyleManagerControlled;
            this.KeysDrinkBar.TabIndex = 0x10c;
            this.comboItem28.Text = "1";
            this.comboItem29.Text = "2";
            this.comboItem30.Text = "3";
            this.comboItem31.Text = "4";
            this.comboItem32.Text = "5";
            this.comboItem27.Text = "6";
            this.KeysEatKey.DisplayMember = "Text";
            this.KeysEatKey.DrawMode = DrawMode.OwnerDrawFixed;
            this.KeysEatKey.DropDownStyle = ComboBoxStyle.DropDownList;
            this.KeysEatKey.FlatStyle = FlatStyle.Flat;
            this.KeysEatKey.FormattingEnabled = true;
            this.KeysEatKey.ItemHeight = 14;
            object[] objArray7 = new object[] { this.comboItem2, this.comboItem3, this.comboItem4, this.comboItem5, this.comboItem6, this.comboItem7, this.comboItem8, this.comboItem9, this.comboItem10 };
            objArray7[9] = this.comboItem1;
            this.KeysEatKey.Items.AddRange(objArray7);
            this.KeysEatKey.Location = new Point(0x87, 0x1a);
            this.KeysEatKey.Name = "KeysEatKey";
            this.KeysEatKey.Size = new Size(0x27, 20);
            this.KeysEatKey.Style = eDotNetBarStyle.StyleManagerControlled;
            this.KeysEatKey.TabIndex = 0x10b;
            this.comboItem2.Text = "1";
            this.comboItem3.Text = "2";
            this.comboItem4.Text = "3";
            this.comboItem5.Text = "4";
            this.comboItem6.Text = "5";
            this.comboItem7.Text = "6";
            this.comboItem8.Text = "7";
            this.comboItem9.Text = "8";
            this.comboItem10.Text = "9";
            this.comboItem1.Text = "0";
            this.labelX10.BackColor = Color.Transparent;
            this.labelX10.BackgroundStyle.Class = "";
            this.labelX10.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX10.Location = new Point(3, 0x17);
            this.labelX10.Name = "labelX10";
            this.labelX10.Size = new Size(50, 0x17);
            this.labelX10.TabIndex = 0x10a;
            this.labelX10.Text = "Eat:";
            this.KeysEatBar.DisplayMember = "Text";
            this.KeysEatBar.DrawMode = DrawMode.OwnerDrawFixed;
            this.KeysEatBar.DropDownStyle = ComboBoxStyle.DropDownList;
            this.KeysEatBar.FlatStyle = FlatStyle.Flat;
            this.KeysEatBar.FormattingEnabled = true;
            this.KeysEatBar.ItemHeight = 14;
            object[] objArray8 = new object[] { this.comboItem11, this.comboItem12, this.comboItem13, this.comboItem14, this.comboItem15, this.comboItem16 };
            this.KeysEatBar.Items.AddRange(objArray8);
            this.KeysEatBar.Location = new Point(0x53, 0x1a);
            this.KeysEatBar.Name = "KeysEatBar";
            this.KeysEatBar.Size = new Size(0x27, 20);
            this.KeysEatBar.Style = eDotNetBarStyle.StyleManagerControlled;
            this.KeysEatBar.TabIndex = 0x109;
            this.comboItem11.Text = "1";
            this.comboItem12.Text = "2";
            this.comboItem13.Text = "3";
            this.comboItem14.Text = "4";
            this.comboItem15.Text = "5";
            this.comboItem16.Text = "6";
            this.KeysGroundMountKey.DisplayMember = "Text";
            this.KeysGroundMountKey.DrawMode = DrawMode.OwnerDrawFixed;
            this.KeysGroundMountKey.DropDownStyle = ComboBoxStyle.DropDownList;
            this.KeysGroundMountKey.FlatStyle = FlatStyle.Flat;
            this.KeysGroundMountKey.FormattingEnabled = true;
            this.KeysGroundMountKey.ItemHeight = 14;
            object[] objArray9 = new object[] { this.comboItem50, this.comboItem51, this.comboItem52, this.comboItem53, this.comboItem54, this.comboItem55, this.comboItem56, this.comboItem57, this.comboItem58 };
            objArray9[9] = this.comboItem49;
            this.KeysGroundMountKey.Items.AddRange(objArray9);
            this.KeysGroundMountKey.Location = new Point(0x87, 80);
            this.KeysGroundMountKey.Name = "KeysGroundMountKey";
            this.KeysGroundMountKey.Size = new Size(0x27, 20);
            this.KeysGroundMountKey.Style = eDotNetBarStyle.StyleManagerControlled;
            this.KeysGroundMountKey.TabIndex = 0x105;
            this.comboItem50.Text = "1";
            this.comboItem51.Text = "2";
            this.comboItem52.Text = "3";
            this.comboItem53.Text = "4";
            this.comboItem54.Text = "5";
            this.comboItem55.Text = "6";
            this.comboItem56.Text = "7";
            this.comboItem57.Text = "8";
            this.comboItem58.Text = "9";
            this.comboItem49.Text = "0";
            this.labelX16.BackColor = Color.Transparent;
            this.labelX16.BackgroundStyle.Class = "";
            this.labelX16.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX16.Location = new Point(2, 0x4d);
            this.labelX16.Name = "labelX16";
            this.labelX16.Size = new Size(80, 0x17);
            this.labelX16.TabIndex = 260;
            this.labelX16.Text = "Ground mount:";
            this.KeysGroundMountBar.DisplayMember = "Text";
            this.KeysGroundMountBar.DrawMode = DrawMode.OwnerDrawFixed;
            this.KeysGroundMountBar.DropDownStyle = ComboBoxStyle.DropDownList;
            this.KeysGroundMountBar.FlatStyle = FlatStyle.Flat;
            this.KeysGroundMountBar.FormattingEnabled = true;
            this.KeysGroundMountBar.ItemHeight = 14;
            object[] objArray10 = new object[] { this.comboItem60, this.comboItem61, this.comboItem62, this.comboItem63, this.comboItem64, this.comboItem59 };
            this.KeysGroundMountBar.Items.AddRange(objArray10);
            this.KeysGroundMountBar.Location = new Point(0x53, 80);
            this.KeysGroundMountBar.Name = "KeysGroundMountBar";
            this.KeysGroundMountBar.Size = new Size(0x27, 20);
            this.KeysGroundMountBar.Style = eDotNetBarStyle.StyleManagerControlled;
            this.KeysGroundMountBar.TabIndex = 0x103;
            this.comboItem60.Text = "1";
            this.comboItem61.Text = "2";
            this.comboItem62.Text = "3";
            this.comboItem63.Text = "4";
            this.comboItem64.Text = "5";
            this.comboItem59.Text = "6";
            this.labelX15.BackColor = Color.Transparent;
            this.labelX15.BackgroundStyle.Class = "";
            this.labelX15.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX15.Location = new Point(2, 0x8d);
            this.labelX15.Name = "labelX15";
            this.labelX15.Size = new Size(420, 0x2a);
            this.labelX15.TabIndex = 0x102;
            this.labelX15.Text = "<u>Reset redmessage:</u><br/>\r\n(Just an attack that will give a error message when used on self e.g. Shadow Bolt.)";
            this.labelX20.BackColor = Color.Transparent;
            this.labelX20.BackgroundStyle.Class = "";
            this.labelX20.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX20.Location = new Point(5, 14);
            this.labelX20.Name = "labelX20";
            this.labelX20.Size = new Size(0x1b5, 0x54);
            this.labelX20.TabIndex = 0x101;
            this.labelX20.Text = manager.GetString("labelX20.Text");
            this.labelX20.TextLineAlignment = StringAlignment.Near;
            this.labelX20.WordWrap = true;
            this.KeysAttack1Key.DisplayMember = "Text";
            this.KeysAttack1Key.DrawMode = DrawMode.OwnerDrawFixed;
            this.KeysAttack1Key.DropDownStyle = ComboBoxStyle.DropDownList;
            this.KeysAttack1Key.FlatStyle = FlatStyle.Flat;
            this.KeysAttack1Key.FormattingEnabled = true;
            this.KeysAttack1Key.ItemHeight = 14;
            object[] objArray11 = new object[] { this.comboItem34, this.comboItem35, this.comboItem36, this.comboItem37, this.comboItem38, this.comboItem39, this.comboItem40, this.comboItem41, this.comboItem42 };
            objArray11[9] = this.comboItem33;
            this.KeysAttack1Key.Items.AddRange(objArray11);
            this.KeysAttack1Key.Location = new Point(0x7e, 0xcc);
            this.KeysAttack1Key.Name = "KeysAttack1Key";
            this.KeysAttack1Key.Size = new Size(0x27, 20);
            this.KeysAttack1Key.Style = eDotNetBarStyle.StyleManagerControlled;
            this.KeysAttack1Key.TabIndex = 0x100;
            this.comboItem34.Text = "1";
            this.comboItem35.Text = "2";
            this.comboItem36.Text = "3";
            this.comboItem37.Text = "4";
            this.comboItem38.Text = "5";
            this.comboItem39.Text = "6";
            this.comboItem40.Text = "7";
            this.comboItem41.Text = "8";
            this.comboItem42.Text = "9";
            this.comboItem33.Text = "0";
            this.labelX14.BackColor = Color.Transparent;
            this.labelX14.BackgroundStyle.Class = "";
            this.labelX14.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX14.Location = new Point(2, 0xc9);
            this.labelX14.Name = "labelX14";
            this.labelX14.Size = new Size(0x35, 0x17);
            this.labelX14.TabIndex = 0xff;
            this.labelX14.Text = "Attack";
            this.KeysAttack1Bar.DisplayMember = "Text";
            this.KeysAttack1Bar.DrawMode = DrawMode.OwnerDrawFixed;
            this.KeysAttack1Bar.DropDownStyle = ComboBoxStyle.DropDownList;
            this.KeysAttack1Bar.FlatStyle = FlatStyle.Flat;
            this.KeysAttack1Bar.FormattingEnabled = true;
            this.KeysAttack1Bar.ItemHeight = 14;
            object[] objArray12 = new object[] { this.comboItem44, this.comboItem45, this.comboItem46, this.comboItem47, this.comboItem48, this.comboItem43 };
            this.KeysAttack1Bar.Items.AddRange(objArray12);
            this.KeysAttack1Bar.Location = new Point(0x4a, 0xcc);
            this.KeysAttack1Bar.Name = "KeysAttack1Bar";
            this.KeysAttack1Bar.Size = new Size(0x27, 20);
            this.KeysAttack1Bar.Style = eDotNetBarStyle.StyleManagerControlled;
            this.KeysAttack1Bar.TabIndex = 0xfe;
            this.comboItem44.Text = "1";
            this.comboItem45.Text = "2";
            this.comboItem46.Text = "3";
            this.comboItem47.Text = "4";
            this.comboItem48.Text = "5";
            this.comboItem43.Text = "6";
            this.CombatTBDrinkAt.BackgroundStyle.Class = "DateTimeInputBackground";
            this.CombatTBDrinkAt.BackgroundStyle.CornerType = eCornerType.Square;
            this.CombatTBDrinkAt.ButtonFreeText.Shortcut = eShortcut.F2;
            this.CombatTBDrinkAt.Location = new Point(0x40, 0x1a);
            this.CombatTBDrinkAt.Name = "CombatTBDrinkAt";
            this.CombatTBDrinkAt.ShowUpDown = true;
            this.CombatTBDrinkAt.Size = new Size(0x31, 20);
            this.CombatTBDrinkAt.TabIndex = 0xab;
            this.CombatTBEatAt.BackgroundStyle.Class = "DateTimeInputBackground";
            this.CombatTBEatAt.BackgroundStyle.CornerType = eCornerType.Square;
            this.CombatTBEatAt.ButtonFreeText.Shortcut = eShortcut.F2;
            this.CombatTBEatAt.Location = new Point(0x40, 3);
            this.CombatTBEatAt.Name = "CombatTBEatAt";
            this.CombatTBEatAt.ShowUpDown = true;
            this.CombatTBEatAt.Size = new Size(0x31, 20);
            this.CombatTBEatAt.TabIndex = 170;
            this.SetupCBShutdown.AutoSize = true;
            this.SetupCBShutdown.BackColor = Color.Transparent;
            this.SetupCBShutdown.BackgroundStyle.Class = "";
            this.SetupCBShutdown.BackgroundStyle.CornerType = eCornerType.Square;
            this.SetupCBShutdown.Location = new Point(3, 0x6d);
            this.SetupCBShutdown.Name = "SetupCBShutdown";
            this.SetupCBShutdown.Size = new Size(120, 15);
            this.SetupCBShutdown.TabIndex = 0xa9;
            this.SetupCBShutdown.Text = "Shutdown computer";
            this.CombatCBDrink.AutoSize = true;
            this.CombatCBDrink.BackColor = Color.Transparent;
            this.CombatCBDrink.BackgroundStyle.Class = "";
            this.CombatCBDrink.BackgroundStyle.CornerType = eCornerType.Square;
            this.CombatCBDrink.Location = new Point(3, 0x1a);
            this.CombatCBDrink.Name = "CombatCBDrink";
            this.CombatCBDrink.Size = new Size(60, 15);
            this.CombatCBDrink.TabIndex = 0xa3;
            this.CombatCBDrink.Text = "Drink at";
            this.CombatCBEat.AutoSize = true;
            this.CombatCBEat.BackColor = Color.Transparent;
            this.CombatCBEat.BackgroundStyle.Class = "";
            this.CombatCBEat.BackgroundStyle.CornerType = eCornerType.Square;
            this.CombatCBEat.Location = new Point(3, 4);
            this.CombatCBEat.Name = "CombatCBEat";
            this.CombatCBEat.Size = new Size(0x33, 15);
            this.CombatCBEat.TabIndex = 0xa4;
            this.CombatCBEat.Text = "Eat at";
            this.label12.AutoSize = true;
            this.label12.BackColor = Color.Transparent;
            this.label12.Location = new Point(0x77, 8);
            this.label12.Name = "label12";
            this.label12.Size = new Size(0x31, 13);
            this.label12.TabIndex = 0xa6;
            this.label12.Text = "% Health";
            this.label11.AutoSize = true;
            this.label11.BackColor = Color.Transparent;
            this.label11.Location = new Point(0x77, 0x1d);
            this.label11.Name = "label11";
            this.label11.Size = new Size(0x2d, 13);
            this.label11.TabIndex = 0xa8;
            this.label11.Text = "% Mana";
            this.labelX48.BackColor = Color.Transparent;
            this.labelX48.BackgroundStyle.Class = "";
            this.labelX48.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX48.Location = new Point(0xa8, 0x56);
            this.labelX48.Name = "labelX48";
            this.labelX48.Size = new Size(0x4b, 0x17);
            this.labelX48.TabIndex = 0x55;
            this.labelX48.Text = "minuttes";
            this.SetupTBStopAfter.BackgroundStyle.Class = "DateTimeInputBackground";
            this.SetupTBStopAfter.BackgroundStyle.CornerType = eCornerType.Square;
            this.SetupTBStopAfter.ButtonFreeText.Shortcut = eShortcut.F2;
            this.SetupTBStopAfter.Location = new Point(0x52, 0x57);
            this.SetupTBStopAfter.Name = "SetupTBStopAfter";
            this.SetupTBStopAfter.ShowUpDown = true;
            this.SetupTBStopAfter.TabIndex = 0x54;
            this.SetupCBStopAfter.AutoSize = true;
            this.SetupCBStopAfter.BackColor = Color.Transparent;
            this.SetupCBStopAfter.BackgroundStyle.Class = "";
            this.SetupCBStopAfter.BackgroundStyle.CornerType = eCornerType.Square;
            this.SetupCBStopAfter.Location = new Point(3, 0x58);
            this.SetupCBStopAfter.Name = "SetupCBStopAfter";
            this.SetupCBStopAfter.Size = new Size(70, 15);
            this.SetupCBStopAfter.TabIndex = 0x53;
            this.SetupCBStopAfter.Text = "Stop after ";
            this.SetupCBStopAfter.CheckedChanged += new EventHandler(this.SetupCBStopAfter_CheckedChanged);
            this.SetupCBSoundStop.AutoSize = true;
            this.SetupCBSoundStop.BackColor = Color.Transparent;
            this.SetupCBSoundStop.BackgroundStyle.Class = "";
            this.SetupCBSoundStop.BackgroundStyle.CornerType = eCornerType.Square;
            this.SetupCBSoundStop.Location = new Point(3, 0x42);
            this.SetupCBSoundStop.Name = "SetupCBSoundStop";
            this.SetupCBSoundStop.Size = new Size(0x74, 15);
            this.SetupCBSoundStop.TabIndex = 0x52;
            this.SetupCBSoundStop.Text = "Play sound on stop";
            this.SetupCBSoundWhisper.AutoSize = true;
            this.SetupCBSoundWhisper.BackColor = Color.Transparent;
            this.SetupCBSoundWhisper.BackgroundStyle.Class = "";
            this.SetupCBSoundWhisper.BackgroundStyle.CornerType = eCornerType.Square;
            this.SetupCBSoundWhisper.Location = new Point(3, 0x2b);
            this.SetupCBSoundWhisper.Name = "SetupCBSoundWhisper";
            this.SetupCBSoundWhisper.Size = new Size(0x85, 15);
            this.SetupCBSoundWhisper.TabIndex = 0x47;
            this.SetupCBSoundWhisper.Text = "Play sound on whisper";
            this.SetupCBSoundFollow.AutoSize = true;
            this.SetupCBSoundFollow.BackColor = Color.Transparent;
            this.SetupCBSoundFollow.BackgroundStyle.Class = "";
            this.SetupCBSoundFollow.BackgroundStyle.CornerType = eCornerType.Square;
            this.SetupCBSoundFollow.Location = new Point(3, 3);
            this.SetupCBSoundFollow.Name = "SetupCBSoundFollow";
            this.SetupCBSoundFollow.Size = new Size(0x7b, 15);
            this.SetupCBSoundFollow.TabIndex = 70;
            this.SetupCBSoundFollow.Text = "Play sound on follow";
            this.styleManager1.ManagerStyle = eStyle.Windows7Blue;
            this.superTabControl2.BackColor = Color.FromArgb(0xdf, 0xea, 0xf6);
            this.superTabControl2.ControlBox.CloseBox.Name = "";
            this.superTabControl2.ControlBox.MenuBox.Name = "";
            this.superTabControl2.ControlBox.Name = "";
            BaseItem[] itemArray = new BaseItem[] { this.superTabControl2.ControlBox.MenuBox, this.superTabControl2.ControlBox.CloseBox };
            this.superTabControl2.ControlBox.SubItems.AddRange(itemArray);
            this.superTabControl2.Controls.Add(this.superTabControlPanel4);
            this.superTabControl2.Controls.Add(this.superTabControlPanel3);
            this.superTabControl2.Controls.Add(this.superTabControlPanel6);
            this.superTabControl2.Controls.Add(this.superTabControlPanel1);
            this.superTabControl2.Controls.Add(this.superTabControlPanel12);
            this.superTabControl2.Controls.Add(this.superTabControlPanel5);
            this.superTabControl2.Controls.Add(this.superTabControlPanel7);
            this.superTabControl2.Dock = DockStyle.Top;
            this.superTabControl2.Location = new Point(0, 0);
            this.superTabControl2.Name = "superTabControl2";
            this.superTabControl2.ReorderTabsEnabled = true;
            this.superTabControl2.SelectedTabFont = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold);
            this.superTabControl2.SelectedTabIndex = 0;
            this.superTabControl2.Size = new Size(0x1bd, 370);
            this.superTabControl2.TabFont = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.superTabControl2.TabIndex = 3;
            BaseItem[] itemArray2 = new BaseItem[] { this.superTabItem4, this.superTabItem7, this.superTabItem11, this.superTabItem5, this.superTabItem3, this.tabRelog, this.superTabItem1 };
            this.superTabControl2.Tabs.AddRange(itemArray2);
            this.superTabControl2.TabStyle = eSuperTabStyle.WinMediaPlayer12;
            this.superTabControl2.Text = "Grinder settings";
            this.superTabControlPanel4.Controls.Add(this.groupPanel10);
            this.superTabControlPanel4.Controls.Add(this.Latency);
            this.superTabControlPanel4.Controls.Add(this.labelX44);
            this.superTabControlPanel4.Controls.Add(this.SetupDebugMode);
            this.superTabControlPanel4.Controls.Add(this.CBHookMouse);
            this.superTabControlPanel4.Controls.Add(this.labelX21);
            this.superTabControlPanel4.Controls.Add(this.labelX19);
            this.superTabControlPanel4.Controls.Add(this.SetupUseHotkeys);
            this.superTabControlPanel4.Controls.Add(this.labelX1);
            this.superTabControlPanel4.Controls.Add(this.SetupCBBackground);
            this.superTabControlPanel4.Dock = DockStyle.Fill;
            this.superTabControlPanel4.Location = new Point(0, 0x17);
            this.superTabControlPanel4.Name = "superTabControlPanel4";
            this.superTabControlPanel4.Size = new Size(0x1bd, 0x15b);
            this.superTabControlPanel4.TabIndex = 1;
            this.superTabControlPanel4.TabItem = this.superTabItem4;
            this.superTabControlPanel4.ThemeAware = true;
            this.superTabControlPanel4.Click += new EventHandler(this.superTabControlPanel4_Click);
            this.groupPanel10.BackColor = Color.White;
            this.groupPanel10.CanvasColor = SystemColors.Control;
            this.groupPanel10.ColorSchemeStyle = eDotNetBarStyle.Office2007;
            this.groupPanel10.Controls.Add(this.ClientLanguage);
            this.groupPanel10.Controls.Add(this.labelX46);
            this.groupPanel10.Location = new Point(5, 6);
            this.groupPanel10.Name = "groupPanel10";
            this.groupPanel10.Size = new Size(0x1b1, 0x37);
            this.groupPanel10.Style.BackColor2SchemePart = eColorSchemePart.PanelBackground2;
            this.groupPanel10.Style.BackColorGradientAngle = 90;
            this.groupPanel10.Style.BackColorSchemePart = eColorSchemePart.PanelBackground;
            this.groupPanel10.Style.BorderBottom = eStyleBorderType.Solid;
            this.groupPanel10.Style.BorderBottomWidth = 1;
            this.groupPanel10.Style.BorderColorSchemePart = eColorSchemePart.PanelBorder;
            this.groupPanel10.Style.BorderLeft = eStyleBorderType.Solid;
            this.groupPanel10.Style.BorderLeftWidth = 1;
            this.groupPanel10.Style.BorderRight = eStyleBorderType.Solid;
            this.groupPanel10.Style.BorderRightWidth = 1;
            this.groupPanel10.Style.BorderTop = eStyleBorderType.Solid;
            this.groupPanel10.Style.BorderTopWidth = 1;
            this.groupPanel10.Style.Class = "";
            this.groupPanel10.Style.CornerDiameter = 4;
            this.groupPanel10.Style.CornerType = eCornerType.Rounded;
            this.groupPanel10.Style.TextAlignment = eStyleTextAlignment.Center;
            this.groupPanel10.Style.TextColorSchemePart = eColorSchemePart.PanelText;
            this.groupPanel10.Style.TextLineAlignment = eStyleTextAlignment.Near;
            this.groupPanel10.StyleMouseDown.Class = "";
            this.groupPanel10.StyleMouseDown.CornerType = eCornerType.Square;
            this.groupPanel10.StyleMouseOver.Class = "";
            this.groupPanel10.StyleMouseOver.CornerType = eCornerType.Square;
            this.groupPanel10.TabIndex = 0xb5;
            this.groupPanel10.Text = "Client language";
            this.ClientLanguage.DisplayMember = "Text";
            this.ClientLanguage.DrawMode = DrawMode.OwnerDrawFixed;
            this.ClientLanguage.DropDownStyle = ComboBoxStyle.DropDownList;
            this.ClientLanguage.FlatStyle = FlatStyle.Flat;
            this.ClientLanguage.FormattingEnabled = true;
            this.ClientLanguage.ItemHeight = 14;
            object[] objArray13 = new object[] { this.comboItem94, this.comboItem95, this.comboItem88, this.comboItem89, this.comboItem90 };
            this.ClientLanguage.Items.AddRange(objArray13);
            this.ClientLanguage.Location = new Point(0x43, 3);
            this.ClientLanguage.Name = "ClientLanguage";
            this.ClientLanguage.Size = new Size(0xf4, 20);
            this.ClientLanguage.Style = eDotNetBarStyle.StyleManagerControlled;
            this.ClientLanguage.TabIndex = 0x10a;
            this.ClientLanguage.SelectedIndexChanged += new EventHandler(this.ClientLanguage_SelectedIndexChanged);
            this.comboItem94.Text = "English";
            this.comboItem95.Text = "Russian";
            this.comboItem88.Text = "German";
            this.comboItem89.Text = "French";
            this.comboItem90.Text = "Spanish";
            this.labelX46.BackColor = Color.Transparent;
            this.labelX46.BackgroundStyle.Class = "";
            this.labelX46.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX46.Location = new Point(5, 2);
            this.labelX46.Name = "labelX46";
            this.labelX46.Size = new Size(0x3b, 0x17);
            this.labelX46.TabIndex = 4;
            this.labelX46.Text = "Language:";
            this.Latency.BackgroundStyle.Class = "DateTimeInputBackground";
            this.Latency.BackgroundStyle.CornerType = eCornerType.Square;
            this.Latency.ButtonFreeText.Shortcut = eShortcut.F2;
            this.Latency.Location = new Point(0x53, 180);
            this.Latency.Name = "Latency";
            this.Latency.ShowUpDown = true;
            this.Latency.Size = new Size(0x3d, 20);
            this.Latency.TabIndex = 180;
            this.labelX44.BackColor = Color.Transparent;
            this.labelX44.BackgroundStyle.Class = "";
            this.labelX44.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX44.Location = new Point(8, 0xb2);
            this.labelX44.Name = "labelX44";
            this.labelX44.Size = new Size(0x4b, 0x17);
            this.labelX44.TabIndex = 0xb3;
            this.labelX44.Text = "Latency (milli)";
            this.SetupDebugMode.BackColor = Color.Transparent;
            this.SetupDebugMode.BackgroundStyle.Class = "";
            this.SetupDebugMode.BackgroundStyle.CornerType = eCornerType.Square;
            this.SetupDebugMode.Location = new Point(0x156, 0xae);
            this.SetupDebugMode.Name = "SetupDebugMode";
            this.SetupDebugMode.Size = new Size(100, 0x17);
            this.SetupDebugMode.Style = eDotNetBarStyle.StyleManagerControlled;
            this.SetupDebugMode.TabIndex = 0xb2;
            this.SetupDebugMode.Text = "Debug mode";
            this.CBHookMouse.AutoSize = true;
            this.CBHookMouse.BackColor = Color.Transparent;
            this.CBHookMouse.BackgroundStyle.Class = "";
            this.CBHookMouse.BackgroundStyle.CornerType = eCornerType.Square;
            this.CBHookMouse.Location = new Point(8, 0x55);
            this.CBHookMouse.Name = "CBHookMouse";
            this.CBHookMouse.Size = new Size(0x54, 15);
            this.CBHookMouse.TabIndex = 0xb1;
            this.CBHookMouse.Text = "Hook mouse";
            this.CBHookMouse.CheckedChanged += new EventHandler(this.CBHookMouse_CheckedChanged);
            this.labelX21.BackColor = Color.Transparent;
            this.labelX21.BackgroundStyle.Class = "";
            this.labelX21.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX21.Location = new Point(0x74, 0x9e);
            this.labelX21.Name = "labelX21";
            this.labelX21.Size = new Size(290, 0x11);
            this.labelX21.TabIndex = 0xb0;
            this.labelX21.Text = "F9 = Start/stop bot - F10 = Pause bot";
            this.labelX19.BackColor = Color.Transparent;
            this.labelX19.BackgroundStyle.Class = "";
            this.labelX19.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX19.Location = new Point(8, 0x80);
            this.labelX19.Name = "labelX19";
            this.labelX19.Size = new Size(0x72, 0x17);
            this.labelX19.TabIndex = 0xaf;
            this.labelX19.Text = "<b>Hotkeys:</b>";
            this.SetupUseHotkeys.AutoSize = true;
            this.SetupUseHotkeys.BackColor = Color.Transparent;
            this.SetupUseHotkeys.BackgroundStyle.Class = "";
            this.SetupUseHotkeys.BackgroundStyle.CornerType = eCornerType.Square;
            this.SetupUseHotkeys.Location = new Point(8, 0x9d);
            this.SetupUseHotkeys.Name = "SetupUseHotkeys";
            this.SetupUseHotkeys.Size = new Size(0x62, 15);
            this.SetupUseHotkeys.TabIndex = 0xae;
            this.SetupUseHotkeys.Text = "Enable hotkeys";
            this.superTabItem4.AttachedControl = this.superTabControlPanel4;
            this.superTabItem4.GlobalItem = false;
            this.superTabItem4.Name = "superTabItem4";
            this.superTabItem4.Text = "General";
            this.superTabControlPanel3.Controls.Add(this.groupPanel3);
            this.superTabControlPanel3.Controls.Add(this.groupPanel2);
            this.superTabControlPanel3.Dock = DockStyle.Fill;
            this.superTabControlPanel3.Location = new Point(0, 0);
            this.superTabControlPanel3.Name = "superTabControlPanel3";
            this.superTabControlPanel3.Size = new Size(0x1bd, 370);
            this.superTabControlPanel3.TabIndex = 0;
            this.superTabControlPanel3.TabItem = this.superTabItem3;
            this.superTabControlPanel3.ThemeAware = true;
            this.groupPanel3.BackColor = Color.Transparent;
            this.groupPanel3.CanvasColor = SystemColors.Control;
            this.groupPanel3.ColorSchemeStyle = eDotNetBarStyle.Office2007;
            this.groupPanel3.Controls.Add(this.ListMailItems);
            this.groupPanel3.Controls.Add(this.labelX8);
            this.groupPanel3.Location = new Point(0xcd, 5);
            this.groupPanel3.Name = "groupPanel3";
            this.groupPanel3.Size = new Size(0xe8, 0x153);
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
            this.groupPanel3.TabIndex = 1;
            this.groupPanel3.Text = "Items";
            this.ListMailItems.AccessibleRole = AccessibleRole.Outline;
            this.ListMailItems.AllowDrop = true;
            this.ListMailItems.BackColor = SystemColors.Window;
            this.ListMailItems.BackgroundStyle.Class = "TreeBorderKey";
            this.ListMailItems.BackgroundStyle.CornerType = eCornerType.Square;
            this.ListMailItems.DragDropEnabled = false;
            this.ListMailItems.Location = new Point(3, 0x1c);
            this.ListMailItems.Name = "ListMailItems";
            this.ListMailItems.NodesConnector = this.nodeConnector2;
            this.ListMailItems.NodeStyle = this.elementStyle2;
            this.ListMailItems.PathSeparator = ";";
            this.ListMailItems.Size = new Size(0xdd, 0x11b);
            this.ListMailItems.Styles.Add(this.elementStyle2);
            this.ListMailItems.TabIndex = 0x1c;
            this.ListMailItems.Text = "advTree1";
            this.nodeConnector2.LineColor = SystemColors.ControlText;
            this.elementStyle2.Class = "";
            this.elementStyle2.CornerType = eCornerType.Square;
            this.elementStyle2.Name = "elementStyle2";
            this.elementStyle2.TextColor = SystemColors.ControlText;
            this.labelX8.BackColor = Color.Transparent;
            this.labelX8.BackgroundStyle.Class = "";
            this.labelX8.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX8.Location = new Point(3, 4);
            this.labelX8.Name = "labelX8";
            this.labelX8.Size = new Size(0x59, 0x17);
            this.labelX8.TabIndex = 0x1b;
            this.labelX8.Text = "Mail items:";
            this.groupPanel2.BackColor = Color.Transparent;
            this.groupPanel2.CanvasColor = SystemColors.Control;
            this.groupPanel2.ColorSchemeStyle = eDotNetBarStyle.Office2007;
            this.groupPanel2.Controls.Add(this.KeysMailMacroBar);
            this.groupPanel2.Controls.Add(this.KeysMailMacroKey);
            this.groupPanel2.Controls.Add(this.MacroForMail);
            this.groupPanel2.Controls.Add(this.labelX45);
            this.groupPanel2.Controls.Add(this.labelX40);
            this.groupPanel2.Controls.Add(this.BtnRemoveMailItem);
            this.groupPanel2.Controls.Add(this.TBMailTo);
            this.groupPanel2.Controls.Add(this.labelX17);
            this.groupPanel2.Controls.Add(this.TBMailName);
            this.groupPanel2.Controls.Add(this.CBMail);
            this.groupPanel2.Controls.Add(this.BtnAddMailItem);
            this.groupPanel2.Controls.Add(this.labelX18);
            this.groupPanel2.Controls.Add(this.labelX22);
            this.groupPanel2.Location = new Point(4, 5);
            this.groupPanel2.Name = "groupPanel2";
            this.groupPanel2.Size = new Size(0xc3, 0x153);
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
            this.groupPanel2.TabIndex = 2;
            this.KeysMailMacroBar.DisplayMember = "Text";
            this.KeysMailMacroBar.DrawMode = DrawMode.OwnerDrawFixed;
            this.KeysMailMacroBar.DropDownStyle = ComboBoxStyle.DropDownList;
            this.KeysMailMacroBar.FlatStyle = FlatStyle.Flat;
            this.KeysMailMacroBar.FormattingEnabled = true;
            this.KeysMailMacroBar.ItemHeight = 14;
            object[] objArray14 = new object[] { this.bar1, this.bar2, this.bar3, this.bar4, this.bar5, this.bar0 };
            this.KeysMailMacroBar.Items.AddRange(objArray14);
            this.KeysMailMacroBar.Location = new Point(0x6a, 0xee);
            this.KeysMailMacroBar.Name = "KeysMailMacroBar";
            this.KeysMailMacroBar.Size = new Size(0x27, 20);
            this.KeysMailMacroBar.Style = eDotNetBarStyle.StyleManagerControlled;
            this.KeysMailMacroBar.TabIndex = 0x101;
            this.KeysMailMacroBar.Visible = false;
            this.bar1.Text = "1";
            this.bar2.Text = "2";
            this.bar3.Text = "3";
            this.bar4.Text = "4";
            this.bar5.Text = "5";
            this.bar0.Text = "6";
            this.KeysMailMacroKey.DisplayMember = "Text";
            this.KeysMailMacroKey.DrawMode = DrawMode.OwnerDrawFixed;
            this.KeysMailMacroKey.DropDownStyle = ComboBoxStyle.DropDownList;
            this.KeysMailMacroKey.FlatStyle = FlatStyle.Flat;
            this.KeysMailMacroKey.FormattingEnabled = true;
            this.KeysMailMacroKey.ItemHeight = 14;
            object[] objArray15 = new object[] { this.key1, this.key2, this.key3, this.key4, this.key5, this.key6, this.key7, this.key8, this.key9 };
            objArray15[9] = this.key0;
            this.KeysMailMacroKey.Items.AddRange(objArray15);
            this.KeysMailMacroKey.Location = new Point(150, 0xee);
            this.KeysMailMacroKey.Name = "KeysMailMacroKey";
            this.KeysMailMacroKey.Size = new Size(0x27, 20);
            this.KeysMailMacroKey.Style = eDotNetBarStyle.StyleManagerControlled;
            this.KeysMailMacroKey.TabIndex = 0x103;
            this.KeysMailMacroKey.Visible = false;
            this.key1.Text = "1";
            this.key2.Text = "2";
            this.key3.Text = "3";
            this.key4.Text = "4";
            this.key5.Text = "5";
            this.key6.Text = "6";
            this.key7.Text = "7";
            this.key8.Text = "8";
            this.key9.Text = "9";
            this.key0.Text = "0";
            this.MacroForMail.BackgroundStyle.Class = "";
            this.MacroForMail.BackgroundStyle.CornerType = eCornerType.Square;
            this.MacroForMail.Location = new Point(1, 210);
            this.MacroForMail.Name = "MacroForMail";
            this.MacroForMail.Size = new Size(0x8b, 0x17);
            this.MacroForMail.Style = eDotNetBarStyle.StyleManagerControlled;
            this.MacroForMail.TabIndex = 0xbd;
            this.MacroForMail.Text = "Use macro for mail";
            this.MacroForMail.Visible = false;
            this.MacroForMail.CheckedChanged += new EventHandler(this.MacroForMail_CheckedChanged);
            this.labelX45.BackColor = Color.Transparent;
            this.labelX45.BackgroundStyle.Class = "";
            this.labelX45.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX45.Location = new Point(2, 0xec);
            this.labelX45.Name = "labelX45";
            this.labelX45.Size = new Size(0x70, 0x17);
            this.labelX45.TabIndex = 0x102;
            this.labelX45.Text = "Macro (Bar and Key)";
            this.labelX45.Visible = false;
            this.labelX40.BackgroundStyle.Class = "";
            this.labelX40.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX40.Location = new Point(2, 170);
            this.labelX40.Name = "labelX40";
            this.labelX40.Size = new Size(0xb6, 0x22);
            this.labelX40.TabIndex = 0xbc;
            this.labelX40.Text = "<b>Items on the mail list will not get <br/> vendored</b>";
            this.BtnRemoveMailItem.AccessibleRole = AccessibleRole.PushButton;
            this.BtnRemoveMailItem.ColorTable = eButtonColor.OrangeWithBackground;
            this.BtnRemoveMailItem.Location = new Point(2, 0x8e);
            this.BtnRemoveMailItem.Name = "BtnRemoveMailItem";
            this.BtnRemoveMailItem.Size = new Size(0x7d, 0x17);
            this.BtnRemoveMailItem.Style = eDotNetBarStyle.StyleManagerControlled;
            this.BtnRemoveMailItem.TabIndex = 0xb1;
            this.BtnRemoveMailItem.Text = "Remove selected item";
            this.BtnRemoveMailItem.Click += new EventHandler(this.BtnRemoveMailItemClick);
            this.TBMailTo.Border.Class = "TextBoxBorder";
            this.TBMailTo.Border.CornerType = eCornerType.Square;
            this.TBMailTo.Location = new Point(0x2b, 0x24);
            this.TBMailTo.Name = "TBMailTo";
            this.TBMailTo.Size = new Size(0x8f, 20);
            this.TBMailTo.TabIndex = 0xb0;
            this.labelX17.BackgroundStyle.Class = "";
            this.labelX17.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX17.Location = new Point(0, 0x21);
            this.labelX17.Name = "labelX17";
            this.labelX17.Size = new Size(0x2d, 0x17);
            this.labelX17.TabIndex = 0xaf;
            this.labelX17.Text = "Send to:";
            this.TBMailName.Border.Class = "TextBoxBorder";
            this.TBMailName.Border.CornerType = eCornerType.Square;
            this.TBMailName.Location = new Point(0x2b, 0x57);
            this.TBMailName.Name = "TBMailName";
            this.TBMailName.Size = new Size(0x8f, 20);
            this.TBMailName.TabIndex = 0xae;
            this.CBMail.AutoSize = true;
            this.CBMail.BackColor = Color.Transparent;
            this.CBMail.BackgroundStyle.Class = "";
            this.CBMail.BackgroundStyle.CornerType = eCornerType.Square;
            this.CBMail.Location = new Point(-2, 12);
            this.CBMail.Name = "CBMail";
            this.CBMail.Size = new Size(0x41, 15);
            this.CBMail.TabIndex = 0xad;
            this.CBMail.Text = "Use mail";
            this.CBMail.CheckedChanged += new EventHandler(this.CBMail_CheckedChanged);
            this.BtnAddMailItem.AccessibleRole = AccessibleRole.PushButton;
            this.BtnAddMailItem.ColorTable = eButtonColor.OrangeWithBackground;
            this.BtnAddMailItem.Location = new Point(2, 0x71);
            this.BtnAddMailItem.Name = "BtnAddMailItem";
            this.BtnAddMailItem.Size = new Size(0x7d, 0x17);
            this.BtnAddMailItem.Style = eDotNetBarStyle.StyleManagerControlled;
            this.BtnAddMailItem.TabIndex = 0x1d;
            this.BtnAddMailItem.Text = "Add mail item";
            this.BtnAddMailItem.Click += new EventHandler(this.BtnAddMailItemClick);
            this.labelX18.BackColor = Color.Transparent;
            this.labelX18.BackgroundStyle.Class = "";
            this.labelX18.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX18.Location = new Point(2, 0x58);
            this.labelX18.Name = "labelX18";
            this.labelX18.Size = new Size(0x34, 0x17);
            this.labelX18.TabIndex = 0x1f;
            this.labelX18.Text = "Name:";
            this.labelX22.BackColor = Color.Transparent;
            this.labelX22.BackgroundStyle.Class = "";
            this.labelX22.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX22.Location = new Point(2, 0x40);
            this.labelX22.Name = "labelX22";
            this.labelX22.Size = new Size(0x6c, 0x17);
            this.labelX22.TabIndex = 30;
            this.labelX22.Text = "<b>Add mail item:</b>";
            this.superTabItem3.AttachedControl = this.superTabControlPanel3;
            this.superTabItem3.GlobalItem = false;
            this.superTabItem3.Name = "superTabItem3";
            this.superTabItem3.Text = "Mail";
            this.superTabControlPanel6.Controls.Add(this.labelX38);
            this.superTabControlPanel6.Controls.Add(this.SetupCBRelogEnableRelogger);
            this.superTabControlPanel6.Controls.Add(this.SetupRelogLoginData);
            this.superTabControlPanel6.Dock = DockStyle.Fill;
            this.superTabControlPanel6.Location = new Point(0, 0);
            this.superTabControlPanel6.Name = "superTabControlPanel6";
            this.superTabControlPanel6.Size = new Size(0x1bd, 370);
            this.superTabControlPanel6.TabIndex = 0;
            this.superTabControlPanel6.TabItem = this.tabRelog;
            this.labelX38.BackColor = Color.Transparent;
            this.labelX38.BackgroundStyle.Class = "";
            this.labelX38.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX38.Location = new Point(5, 0x12b);
            this.labelX38.Name = "labelX38";
            this.labelX38.Size = new Size(0x1ad, 0x2b);
            this.labelX38.TabIndex = 4;
            this.labelX38.Text = "Username and password are stored locally in encrypted state. <br/>\r\nYou should still be carefull about sharing the file as the encryption can be broken.";
            this.SetupCBRelogEnableRelogger.BackColor = Color.Transparent;
            this.SetupCBRelogEnableRelogger.BackgroundStyle.Class = "";
            this.SetupCBRelogEnableRelogger.BackgroundStyle.CornerType = eCornerType.Square;
            this.SetupCBRelogEnableRelogger.Location = new Point(7, 5);
            this.SetupCBRelogEnableRelogger.Name = "SetupCBRelogEnableRelogger";
            this.SetupCBRelogEnableRelogger.Size = new Size(0x138, 0x17);
            this.SetupCBRelogEnableRelogger.Style = eDotNetBarStyle.StyleManagerControlled;
            this.SetupCBRelogEnableRelogger.TabIndex = 0;
            this.SetupCBRelogEnableRelogger.Text = "Enable relogger";
            this.SetupCBRelogEnableRelogger.CheckedChanged += new EventHandler(this.SetupCBRelogEnableRelogger_CheckedChanged);
            this.SetupRelogLoginData.BackColor = Color.Transparent;
            this.SetupRelogLoginData.CanvasColor = SystemColors.Control;
            this.SetupRelogLoginData.ColorSchemeStyle = eDotNetBarStyle.Office2007;
            this.SetupRelogLoginData.Controls.Add(this.SetupIIRelogLogAccount);
            this.SetupRelogLoginData.Controls.Add(this.labelX43);
            this.SetupRelogLoginData.Controls.Add(this.labelX39);
            this.SetupRelogLoginData.Controls.Add(this.buttonReloggerClearData);
            this.SetupRelogLoginData.Controls.Add(this.SetupIIRelogLogInAfter);
            this.SetupRelogLoginData.Controls.Add(this.labelX36);
            this.SetupRelogLoginData.Controls.Add(this.labelX37);
            this.SetupRelogLoginData.Controls.Add(this.SetupIIRelogLogOutAfter);
            this.SetupRelogLoginData.Controls.Add(this.labelX35);
            this.SetupRelogLoginData.Controls.Add(this.labelX33);
            this.SetupRelogLoginData.Controls.Add(this.SetupCBRelogEnablePeriodicRelog);
            this.SetupRelogLoginData.Controls.Add(this.labelX32);
            this.SetupRelogLoginData.Controls.Add(this.SetupTBRelogCharacter);
            this.SetupRelogLoginData.Controls.Add(this.labelX28);
            this.SetupRelogLoginData.Controls.Add(this.labelX29);
            this.SetupRelogLoginData.Controls.Add(this.labelX31);
            this.SetupRelogLoginData.Controls.Add(this.SetupTBRelogUsername);
            this.SetupRelogLoginData.Controls.Add(this.SetupTBRelogPW);
            this.SetupRelogLoginData.Location = new Point(7, 0x1d);
            this.SetupRelogLoginData.Name = "SetupRelogLoginData";
            this.SetupRelogLoginData.Size = new Size(0x1af, 0x108);
            this.SetupRelogLoginData.Style.BackColor2SchemePart = eColorSchemePart.PanelBackground2;
            this.SetupRelogLoginData.Style.BackColorGradientAngle = 90;
            this.SetupRelogLoginData.Style.BackColorSchemePart = eColorSchemePart.PanelBackground;
            this.SetupRelogLoginData.Style.BorderBottom = eStyleBorderType.Solid;
            this.SetupRelogLoginData.Style.BorderBottomWidth = 1;
            this.SetupRelogLoginData.Style.BorderColorSchemePart = eColorSchemePart.PanelBorder;
            this.SetupRelogLoginData.Style.BorderLeft = eStyleBorderType.Solid;
            this.SetupRelogLoginData.Style.BorderLeftWidth = 1;
            this.SetupRelogLoginData.Style.BorderRight = eStyleBorderType.Solid;
            this.SetupRelogLoginData.Style.BorderRightWidth = 1;
            this.SetupRelogLoginData.Style.BorderTop = eStyleBorderType.Solid;
            this.SetupRelogLoginData.Style.BorderTopWidth = 1;
            this.SetupRelogLoginData.Style.Class = "";
            this.SetupRelogLoginData.Style.CornerDiameter = 4;
            this.SetupRelogLoginData.Style.CornerType = eCornerType.Rounded;
            this.SetupRelogLoginData.Style.TextAlignment = eStyleTextAlignment.Center;
            this.SetupRelogLoginData.Style.TextColorSchemePart = eColorSchemePart.PanelText;
            this.SetupRelogLoginData.Style.TextLineAlignment = eStyleTextAlignment.Near;
            this.SetupRelogLoginData.StyleMouseDown.Class = "";
            this.SetupRelogLoginData.StyleMouseDown.CornerType = eCornerType.Square;
            this.SetupRelogLoginData.StyleMouseOver.Class = "";
            this.SetupRelogLoginData.StyleMouseOver.CornerType = eCornerType.Square;
            this.SetupRelogLoginData.TabIndex = 3;
            this.SetupRelogLoginData.Text = "Login data";
            this.SetupRelogLoginData.Visible = false;
            this.SetupIIRelogLogAccount.BackgroundStyle.Class = "DateTimeInputBackground";
            this.SetupIIRelogLogAccount.BackgroundStyle.CornerType = eCornerType.Square;
            this.SetupIIRelogLogAccount.ButtonFreeText.Shortcut = eShortcut.F2;
            this.SetupIIRelogLogAccount.Location = new Point(0x41, 0x57);
            this.SetupIIRelogLogAccount.MaxValue = 8;
            this.SetupIIRelogLogAccount.MinValue = 1;
            this.SetupIIRelogLogAccount.Name = "SetupIIRelogLogAccount";
            this.SetupIIRelogLogAccount.ShowUpDown = true;
            this.SetupIIRelogLogAccount.TabIndex = 0x12;
            this.SetupIIRelogLogAccount.Value = 1;
            this.labelX43.BackgroundStyle.Class = "";
            this.labelX43.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX43.Location = new Point(0x41, 0x6c);
            this.labelX43.Name = "labelX43";
            this.labelX43.Size = new Size(0x11c, 0x17);
            this.labelX43.TabIndex = 0x11;
            this.labelX43.Text = "Leave at 1 if you only have one account";
            this.labelX39.BackgroundStyle.Class = "";
            this.labelX39.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX39.Location = new Point(4, 0x54);
            this.labelX39.Name = "labelX39";
            this.labelX39.Size = new Size(0x3b, 0x17);
            this.labelX39.TabIndex = 0x10;
            this.labelX39.Text = "Account:";
            this.buttonReloggerClearData.AccessibleRole = AccessibleRole.PushButton;
            this.buttonReloggerClearData.ColorTable = eButtonColor.OrangeWithBackground;
            this.buttonReloggerClearData.Location = new Point(0x15b, 0xd7);
            this.buttonReloggerClearData.Name = "buttonReloggerClearData";
            this.buttonReloggerClearData.Size = new Size(0x4b, 0x17);
            this.buttonReloggerClearData.Style = eDotNetBarStyle.StyleManagerControlled;
            this.buttonReloggerClearData.TabIndex = 14;
            this.buttonReloggerClearData.Text = "Clear data";
            this.buttonReloggerClearData.Click += new EventHandler(this.buttonReloggerClearData_Click);
            this.SetupIIRelogLogInAfter.BackgroundStyle.Class = "DateTimeInputBackground";
            this.SetupIIRelogLogInAfter.BackgroundStyle.CornerType = eCornerType.Square;
            this.SetupIIRelogLogInAfter.ButtonFreeText.Shortcut = eShortcut.F2;
            this.SetupIIRelogLogInAfter.Enabled = false;
            this.SetupIIRelogLogInAfter.Location = new Point(180, 210);
            this.SetupIIRelogLogInAfter.Name = "SetupIIRelogLogInAfter";
            this.SetupIIRelogLogInAfter.ShowUpDown = true;
            this.SetupIIRelogLogInAfter.TabIndex = 13;
            this.SetupIIRelogLogInAfter.Value = 30;
            this.labelX36.BackgroundStyle.Class = "";
            this.labelX36.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX36.Location = new Point(0x10a, 0xcf);
            this.labelX36.Name = "labelX36";
            this.labelX36.Size = new Size(0x2f, 0x17);
            this.labelX36.TabIndex = 12;
            this.labelX36.Text = "minutes";
            this.labelX37.BackgroundStyle.Class = "";
            this.labelX37.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX37.Location = new Point(3, 0xd0);
            this.labelX37.Name = "labelX37";
            this.labelX37.Size = new Size(0x5e, 0x17);
            this.labelX37.TabIndex = 11;
            this.labelX37.Text = "Log back in after:";
            this.SetupIIRelogLogOutAfter.BackgroundStyle.Class = "DateTimeInputBackground";
            this.SetupIIRelogLogOutAfter.BackgroundStyle.CornerType = eCornerType.Square;
            this.SetupIIRelogLogOutAfter.ButtonFreeText.Shortcut = eShortcut.F2;
            this.SetupIIRelogLogOutAfter.Enabled = false;
            this.SetupIIRelogLogOutAfter.Location = new Point(180, 0xb8);
            this.SetupIIRelogLogOutAfter.Name = "SetupIIRelogLogOutAfter";
            this.SetupIIRelogLogOutAfter.ShowUpDown = true;
            this.SetupIIRelogLogOutAfter.TabIndex = 10;
            this.SetupIIRelogLogOutAfter.Value = 60;
            this.labelX35.BackgroundStyle.Class = "";
            this.labelX35.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX35.Location = new Point(0x10a, 0xb5);
            this.labelX35.Name = "labelX35";
            this.labelX35.Size = new Size(0x2f, 0x17);
            this.labelX35.TabIndex = 9;
            this.labelX35.Text = "minutes";
            this.labelX33.BackgroundStyle.Class = "";
            this.labelX33.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX33.Location = new Point(3, 0xb6);
            this.labelX33.Name = "labelX33";
            this.labelX33.Size = new Size(0x47, 0x17);
            this.labelX33.TabIndex = 8;
            this.labelX33.Text = "Log out after: ";
            this.SetupCBRelogEnablePeriodicRelog.BackgroundStyle.Class = "";
            this.SetupCBRelogEnablePeriodicRelog.BackgroundStyle.CornerType = eCornerType.Square;
            this.SetupCBRelogEnablePeriodicRelog.Location = new Point(3, 0x99);
            this.SetupCBRelogEnablePeriodicRelog.Name = "SetupCBRelogEnablePeriodicRelog";
            this.SetupCBRelogEnablePeriodicRelog.Size = new Size(0xa2, 0x17);
            this.SetupCBRelogEnablePeriodicRelog.Style = eDotNetBarStyle.StyleManagerControlled;
            this.SetupCBRelogEnablePeriodicRelog.TabIndex = 7;
            this.SetupCBRelogEnablePeriodicRelog.Text = "Enable periodic relogging";
            this.SetupCBRelogEnablePeriodicRelog.CheckedChanged += new EventHandler(this.SetupCBRelogEnablePeriodicRelog_CheckedChanged);
            this.labelX32.BackgroundStyle.Class = "";
            this.labelX32.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX32.Location = new Point(3, 0x80);
            this.labelX32.Name = "labelX32";
            this.labelX32.Size = new Size(0x3b, 0x17);
            this.labelX32.TabIndex = 6;
            this.labelX32.Text = "Character:";
            this.labelX32.Visible = false;
            this.SetupTBRelogCharacter.Border.Class = "TextBoxBorder";
            this.SetupTBRelogCharacter.Border.CornerType = eCornerType.Square;
            this.SetupTBRelogCharacter.Location = new Point(0x41, 0x83);
            this.SetupTBRelogCharacter.Name = "SetupTBRelogCharacter";
            this.SetupTBRelogCharacter.Size = new Size(0xf4, 20);
            this.SetupTBRelogCharacter.TabIndex = 3;
            this.SetupTBRelogCharacter.Visible = false;
            this.labelX28.BackgroundStyle.Class = "";
            this.labelX28.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX28.Location = new Point(3, 3);
            this.labelX28.Name = "labelX28";
            this.labelX28.Size = new Size(0x18b, 0x17);
            this.labelX28.TabIndex = 4;
            this.labelX28.Text = "Please input  your account username, password and name of character to relog";
            this.labelX29.BackgroundStyle.Class = "";
            this.labelX29.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX29.Location = new Point(3, 0x37);
            this.labelX29.Name = "labelX29";
            this.labelX29.Size = new Size(0x3b, 0x17);
            this.labelX29.TabIndex = 3;
            this.labelX29.Text = "Password:";
            this.labelX31.BackgroundStyle.Class = "";
            this.labelX31.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX31.Location = new Point(3, 0x19);
            this.labelX31.Name = "labelX31";
            this.labelX31.Size = new Size(0x3b, 0x17);
            this.labelX31.TabIndex = 3;
            this.labelX31.Text = "Username:";
            this.SetupTBRelogUsername.Border.Class = "TextBoxBorder";
            this.SetupTBRelogUsername.Border.CornerType = eCornerType.Square;
            this.SetupTBRelogUsername.Location = new Point(0x41, 0x1c);
            this.SetupTBRelogUsername.Name = "SetupTBRelogUsername";
            this.SetupTBRelogUsername.Size = new Size(0xf4, 20);
            this.SetupTBRelogUsername.TabIndex = 1;
            this.SetupTBRelogPW.Border.Class = "TextBoxBorder";
            this.SetupTBRelogPW.Border.CornerType = eCornerType.Square;
            this.SetupTBRelogPW.Location = new Point(0x41, 0x3a);
            this.SetupTBRelogPW.Name = "SetupTBRelogPW";
            this.SetupTBRelogPW.PasswordChar = '●';
            this.SetupTBRelogPW.Size = new Size(0xf4, 20);
            this.SetupTBRelogPW.TabIndex = 2;
            this.SetupTBRelogPW.UseSystemPasswordChar = true;
            this.tabRelog.AttachedControl = this.superTabControlPanel6;
            this.tabRelog.GlobalItem = false;
            this.tabRelog.Name = "tabRelog";
            this.tabRelog.Text = "Relog options";
            this.superTabControlPanel1.Controls.Add(this.PluginsList);
            this.superTabControlPanel1.Dock = DockStyle.Fill;
            this.superTabControlPanel1.ImeMode = ImeMode.On;
            this.superTabControlPanel1.Location = new Point(0, 0);
            this.superTabControlPanel1.Name = "superTabControlPanel1";
            this.superTabControlPanel1.Size = new Size(0x1bd, 370);
            this.superTabControlPanel1.TabIndex = 0;
            this.superTabControlPanel1.TabItem = this.superTabItem1;
            this.PluginsList.Dock = DockStyle.Fill;
            this.PluginsList.FormattingEnabled = true;
            this.PluginsList.Location = new Point(0, 0);
            this.PluginsList.Name = "PluginsList";
            this.PluginsList.Size = new Size(0x1bd, 370);
            this.PluginsList.TabIndex = 0;
            this.PluginsList.ItemCheck += new ItemCheckEventHandler(this.PluginsListItemCheck);
            this.superTabItem1.AttachedControl = this.superTabControlPanel1;
            this.superTabItem1.GlobalItem = false;
            this.superTabItem1.Name = "superTabItem1";
            this.superTabItem1.Text = "Plugins";
            this.superTabControlPanel12.ColorSchemeStyle = eDotNetBarStyle.Windows7;
            this.superTabControlPanel12.Controls.Add(this.groupPanel1);
            this.superTabControlPanel12.Controls.Add(this.labelX20);
            this.superTabControlPanel12.Dock = DockStyle.Fill;
            this.superTabControlPanel12.Location = new Point(0, 0);
            this.superTabControlPanel12.Name = "superTabControlPanel12";
            this.superTabControlPanel12.Size = new Size(0x1bd, 370);
            this.superTabControlPanel12.TabIndex = 0;
            this.superTabControlPanel12.TabItem = this.superTabItem11;
            this.superTabControlPanel12.ThemeAware = true;
            this.groupPanel1.BackColor = Color.White;
            this.groupPanel1.CanvasColor = SystemColors.Control;
            this.groupPanel1.ColorSchemeStyle = eDotNetBarStyle.Office2007;
            this.groupPanel1.Controls.Add(this.KeysTargetLast);
            this.groupPanel1.Controls.Add(this.labelX13);
            this.groupPanel1.Controls.Add(this.labelX10);
            this.groupPanel1.Controls.Add(this.KeysInteractTarget);
            this.groupPanel1.Controls.Add(this.KeysDrinkBar);
            this.groupPanel1.Controls.Add(this.labelX2);
            this.groupPanel1.Controls.Add(this.labelX30);
            this.groupPanel1.Controls.Add(this.KeysEatKey);
            this.groupPanel1.Controls.Add(this.KeysDrinkKey);
            this.groupPanel1.Controls.Add(this.KeysStafeRightKey);
            this.groupPanel1.Controls.Add(this.labelX3);
            this.groupPanel1.Controls.Add(this.KeysEatBar);
            this.groupPanel1.Controls.Add(this.labelX42);
            this.groupPanel1.Controls.Add(this.labelX4);
            this.groupPanel1.Controls.Add(this.KeysStafeLeftKey);
            this.groupPanel1.Controls.Add(this.labelX9);
            this.groupPanel1.Controls.Add(this.KeysAttack1Bar);
            this.groupPanel1.Controls.Add(this.labelX41);
            this.groupPanel1.Controls.Add(this.labelX6);
            this.groupPanel1.Controls.Add(this.labelX14);
            this.groupPanel1.Controls.Add(this.labelX25);
            this.groupPanel1.Controls.Add(this.KeysAttack1Key);
            this.groupPanel1.Controls.Add(this.KeysGroundMountKey);
            this.groupPanel1.Controls.Add(this.labelX34);
            this.groupPanel1.Controls.Add(this.labelX15);
            this.groupPanel1.Controls.Add(this.labelX16);
            this.groupPanel1.Controls.Add(this.KeysInteractKey);
            this.groupPanel1.Controls.Add(this.KeysGroundMountBar);
            this.groupPanel1.Location = new Point(7, 0x57);
            this.groupPanel1.Name = "groupPanel1";
            this.groupPanel1.Size = new Size(0x1aa, 0xfe);
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
            this.groupPanel1.TabIndex = 0x142;
            this.groupPanel1.Text = "Keys";
            this.KeysTargetLast.DisplayMember = "Text";
            this.KeysTargetLast.DrawMode = DrawMode.OwnerDrawFixed;
            this.KeysTargetLast.DropDownStyle = ComboBoxStyle.DropDownList;
            this.KeysTargetLast.FlatStyle = FlatStyle.Flat;
            this.KeysTargetLast.FormattingEnabled = true;
            this.KeysTargetLast.ItemHeight = 14;
            object[] objArray16 = new object[] { this.comboItem65, this.comboItem66, this.comboItem67, this.comboItem68, this.comboItem69, this.comboItem70, this.comboItem71, this.comboItem72, this.comboItem73 };
            objArray16[9] = this.comboItem74;
            objArray16[10] = this.comboItem75;
            objArray16[11] = this.comboItem76;
            objArray16[12] = this.comboItem77;
            objArray16[13] = this.comboItem78;
            objArray16[14] = this.comboItem79;
            objArray16[15] = this.comboItem80;
            objArray16[0x10] = this.comboItem81;
            objArray16[0x11] = this.comboItem82;
            objArray16[0x12] = this.comboItem83;
            objArray16[0x13] = this.comboItem84;
            objArray16[20] = this.comboItem85;
            objArray16[0x15] = this.comboItem86;
            objArray16[0x16] = this.comboItem87;
            this.KeysTargetLast.Items.AddRange(objArray16);
            this.KeysTargetLast.Location = new Point(310, 0x33);
            this.KeysTargetLast.Name = "KeysTargetLast";
            this.KeysTargetLast.Size = new Size(0x27, 20);
            this.KeysTargetLast.Style = eDotNetBarStyle.StyleManagerControlled;
            this.KeysTargetLast.TabIndex = 0x143;
            this.comboItem65.Text = "P";
            this.comboItem66.Text = "A";
            this.comboItem67.Text = "B";
            this.comboItem68.Text = "C";
            this.comboItem69.Text = "D";
            this.comboItem70.Text = "F";
            this.comboItem71.Text = "G";
            this.comboItem72.Text = "H";
            this.comboItem73.Text = "I";
            this.comboItem74.Text = "J";
            this.comboItem75.Text = "K";
            this.comboItem76.Text = "L";
            this.comboItem77.Text = "M";
            this.comboItem78.Text = "N";
            this.comboItem79.Text = "O";
            this.comboItem80.Text = "R";
            this.comboItem81.Text = "S";
            this.comboItem82.Text = "T";
            this.comboItem83.Text = "U";
            this.comboItem84.Text = "V";
            this.comboItem85.Text = "X";
            this.comboItem86.Text = "Y";
            this.comboItem87.Text = "Z";
            this.labelX13.BackColor = Color.Transparent;
            this.labelX13.BackgroundStyle.Class = "";
            this.labelX13.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX13.Location = new Point(180, 0x31);
            this.labelX13.Name = "labelX13";
            this.labelX13.Size = new Size(0x7e, 0x17);
            this.labelX13.TabIndex = 0x142;
            this.labelX13.Text = "Target last target:";
            this.superTabItem11.AttachedControl = this.superTabControlPanel12;
            this.superTabItem11.GlobalItem = false;
            this.superTabItem11.Name = "superTabItem11";
            this.superTabItem11.Text = "Keys";
            this.superTabControlPanel5.Controls.Add(this.groupPanel8);
            this.superTabControlPanel5.Controls.Add(this.groupPanel9);
            this.superTabControlPanel5.Dock = DockStyle.Fill;
            this.superTabControlPanel5.Location = new Point(0, 0);
            this.superTabControlPanel5.Name = "superTabControlPanel5";
            this.superTabControlPanel5.Size = new Size(0x1bd, 370);
            this.superTabControlPanel5.TabIndex = 0;
            this.superTabControlPanel5.TabItem = this.superTabItem5;
            this.superTabControlPanel5.ThemeAware = true;
            this.groupPanel8.BackColor = Color.Transparent;
            this.groupPanel8.CanvasColor = SystemColors.Control;
            this.groupPanel8.ColorSchemeStyle = eDotNetBarStyle.Office2007;
            this.groupPanel8.Controls.Add(this.labelX23);
            this.groupPanel8.Controls.Add(this.ListProtectedItems);
            this.groupPanel8.Location = new Point(0xcf, 4);
            this.groupPanel8.Name = "groupPanel8";
            this.groupPanel8.Size = new Size(0xe8, 0x153);
            this.groupPanel8.Style.BackColor2SchemePart = eColorSchemePart.PanelBackground2;
            this.groupPanel8.Style.BackColorGradientAngle = 90;
            this.groupPanel8.Style.BackColorSchemePart = eColorSchemePart.PanelBackground;
            this.groupPanel8.Style.BorderBottom = eStyleBorderType.Solid;
            this.groupPanel8.Style.BorderBottomWidth = 1;
            this.groupPanel8.Style.BorderColorSchemePart = eColorSchemePart.PanelBorder;
            this.groupPanel8.Style.BorderLeft = eStyleBorderType.Solid;
            this.groupPanel8.Style.BorderLeftWidth = 1;
            this.groupPanel8.Style.BorderRight = eStyleBorderType.Solid;
            this.groupPanel8.Style.BorderRightWidth = 1;
            this.groupPanel8.Style.BorderTop = eStyleBorderType.Solid;
            this.groupPanel8.Style.BorderTopWidth = 1;
            this.groupPanel8.Style.Class = "";
            this.groupPanel8.Style.CornerDiameter = 4;
            this.groupPanel8.Style.CornerType = eCornerType.Rounded;
            this.groupPanel8.Style.TextAlignment = eStyleTextAlignment.Center;
            this.groupPanel8.Style.TextColorSchemePart = eColorSchemePart.PanelText;
            this.groupPanel8.Style.TextLineAlignment = eStyleTextAlignment.Near;
            this.groupPanel8.StyleMouseDown.Class = "";
            this.groupPanel8.StyleMouseDown.CornerType = eCornerType.Square;
            this.groupPanel8.StyleMouseOver.Class = "";
            this.groupPanel8.StyleMouseOver.CornerType = eCornerType.Square;
            this.groupPanel8.TabIndex = 3;
            this.groupPanel8.Text = "Items";
            this.labelX23.BackColor = Color.Transparent;
            this.labelX23.BackgroundStyle.Class = "";
            this.labelX23.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX23.Location = new Point(3, 4);
            this.labelX23.Name = "labelX23";
            this.labelX23.Size = new Size(0x59, 0x17);
            this.labelX23.TabIndex = 0x1b;
            this.labelX23.Text = "Protected items:";
            this.ListProtectedItems.AccessibleRole = AccessibleRole.Outline;
            this.ListProtectedItems.AllowDrop = true;
            this.ListProtectedItems.BackColor = SystemColors.Window;
            this.ListProtectedItems.BackgroundStyle.Class = "TreeBorderKey";
            this.ListProtectedItems.BackgroundStyle.CornerType = eCornerType.Square;
            this.ListProtectedItems.DragDropEnabled = false;
            this.ListProtectedItems.Location = new Point(3, 0x20);
            this.ListProtectedItems.Name = "ListProtectedItems";
            this.ListProtectedItems.NodesConnector = this.nodeConnector1;
            this.ListProtectedItems.NodeStyle = this.elementStyle1;
            this.ListProtectedItems.PathSeparator = ";";
            this.ListProtectedItems.Size = new Size(0xdd, 0x11b);
            this.ListProtectedItems.Styles.Add(this.elementStyle1);
            this.ListProtectedItems.TabIndex = 0x1a;
            this.ListProtectedItems.Text = "advTree1";
            this.nodeConnector1.LineColor = SystemColors.ControlText;
            this.elementStyle1.Class = "";
            this.elementStyle1.CornerType = eCornerType.Square;
            this.elementStyle1.Name = "elementStyle1";
            this.elementStyle1.TextColor = SystemColors.ControlText;
            this.groupPanel9.BackColor = Color.Transparent;
            this.groupPanel9.CanvasColor = SystemColors.Control;
            this.groupPanel9.ColorSchemeStyle = eDotNetBarStyle.Office2007;
            this.groupPanel9.Controls.Add(this.IMinFreeBagSlots);
            this.groupPanel9.Controls.Add(this.labelX24);
            this.groupPanel9.Controls.Add(this.CBSellUnCommon);
            this.groupPanel9.Controls.Add(this.CBSellCommon);
            this.groupPanel9.Controls.Add(this.CBSellPoor);
            this.groupPanel9.Controls.Add(this.CBDoRepair);
            this.groupPanel9.Controls.Add(this.BtnRemoveProtected);
            this.groupPanel9.Controls.Add(this.TBProtectedName);
            this.groupPanel9.Controls.Add(this.CBDoVendor);
            this.groupPanel9.Controls.Add(this.BtnAddProtected);
            this.groupPanel9.Controls.Add(this.labelX26);
            this.groupPanel9.Controls.Add(this.labelX27);
            this.groupPanel9.Location = new Point(6, 4);
            this.groupPanel9.Name = "groupPanel9";
            this.groupPanel9.Size = new Size(0xc3, 0x153);
            this.groupPanel9.Style.BackColor2SchemePart = eColorSchemePart.PanelBackground2;
            this.groupPanel9.Style.BackColorGradientAngle = 90;
            this.groupPanel9.Style.BackColorSchemePart = eColorSchemePart.PanelBackground;
            this.groupPanel9.Style.BorderBottom = eStyleBorderType.Solid;
            this.groupPanel9.Style.BorderBottomWidth = 1;
            this.groupPanel9.Style.BorderColorSchemePart = eColorSchemePart.PanelBorder;
            this.groupPanel9.Style.BorderLeft = eStyleBorderType.Solid;
            this.groupPanel9.Style.BorderLeftWidth = 1;
            this.groupPanel9.Style.BorderRight = eStyleBorderType.Solid;
            this.groupPanel9.Style.BorderRightWidth = 1;
            this.groupPanel9.Style.BorderTop = eStyleBorderType.Solid;
            this.groupPanel9.Style.BorderTopWidth = 1;
            this.groupPanel9.Style.Class = "";
            this.groupPanel9.Style.CornerDiameter = 4;
            this.groupPanel9.Style.CornerType = eCornerType.Rounded;
            this.groupPanel9.Style.TextAlignment = eStyleTextAlignment.Center;
            this.groupPanel9.Style.TextColorSchemePart = eColorSchemePart.PanelText;
            this.groupPanel9.Style.TextLineAlignment = eStyleTextAlignment.Near;
            this.groupPanel9.StyleMouseDown.Class = "";
            this.groupPanel9.StyleMouseDown.CornerType = eCornerType.Square;
            this.groupPanel9.StyleMouseOver.Class = "";
            this.groupPanel9.StyleMouseOver.CornerType = eCornerType.Square;
            this.groupPanel9.TabIndex = 2;
            this.groupPanel9.Text = "Options";
            this.IMinFreeBagSlots.BackgroundStyle.Class = "DateTimeInputBackground";
            this.IMinFreeBagSlots.BackgroundStyle.CornerType = eCornerType.Square;
            this.IMinFreeBagSlots.ButtonFreeText.Shortcut = eShortcut.F2;
            this.IMinFreeBagSlots.Location = new Point(0x6a, 0x7c);
            this.IMinFreeBagSlots.Name = "IMinFreeBagSlots";
            this.IMinFreeBagSlots.ShowUpDown = true;
            this.IMinFreeBagSlots.Size = new Size(0x52, 20);
            this.IMinFreeBagSlots.TabIndex = 0xb7;
            this.labelX24.BackColor = Color.Transparent;
            this.labelX24.BackgroundStyle.Class = "";
            this.labelX24.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX24.Location = new Point(3, 0x79);
            this.labelX24.Name = "labelX24";
            this.labelX24.Size = new Size(0x61, 0x17);
            this.labelX24.TabIndex = 0xb6;
            this.labelX24.Text = "Min free bagslots:";
            this.CBSellUnCommon.BackColor = Color.Transparent;
            this.CBSellUnCommon.BackgroundStyle.Class = "";
            this.CBSellUnCommon.BackgroundStyle.CornerType = eCornerType.Square;
            this.CBSellUnCommon.Location = new Point(3, 0x61);
            this.CBSellUnCommon.Name = "CBSellUnCommon";
            this.CBSellUnCommon.Size = new Size(0x61, 0x17);
            this.CBSellUnCommon.Style = eDotNetBarStyle.StyleManagerControlled;
            this.CBSellUnCommon.TabIndex = 0xb5;
            this.CBSellUnCommon.Text = "Sell uncommon";
            this.CBSellCommon.BackColor = Color.Transparent;
            this.CBSellCommon.BackgroundStyle.Class = "";
            this.CBSellCommon.BackgroundStyle.CornerType = eCornerType.Square;
            this.CBSellCommon.Location = new Point(3, 0x4b);
            this.CBSellCommon.Name = "CBSellCommon";
            this.CBSellCommon.Size = new Size(0x61, 0x17);
            this.CBSellCommon.Style = eDotNetBarStyle.StyleManagerControlled;
            this.CBSellCommon.TabIndex = 180;
            this.CBSellCommon.Text = "Sell common";
            this.CBSellPoor.BackColor = Color.Transparent;
            this.CBSellPoor.BackgroundStyle.Class = "";
            this.CBSellPoor.BackgroundStyle.CornerType = eCornerType.Square;
            this.CBSellPoor.Location = new Point(3, 0x34);
            this.CBSellPoor.Name = "CBSellPoor";
            this.CBSellPoor.Size = new Size(0x61, 0x17);
            this.CBSellPoor.Style = eDotNetBarStyle.StyleManagerControlled;
            this.CBSellPoor.TabIndex = 0xb3;
            this.CBSellPoor.Text = "Sell poor";
            this.CBDoRepair.AutoSize = true;
            this.CBDoRepair.BackColor = Color.Transparent;
            this.CBDoRepair.BackgroundStyle.Class = "";
            this.CBDoRepair.BackgroundStyle.CornerType = eCornerType.Square;
            this.CBDoRepair.Location = new Point(3, 0x21);
            this.CBDoRepair.Name = "CBDoRepair";
            this.CBDoRepair.Size = new Size(0x37, 15);
            this.CBDoRepair.TabIndex = 0xb2;
            this.CBDoRepair.Text = "Repair";
            this.BtnRemoveProtected.AccessibleRole = AccessibleRole.PushButton;
            this.BtnRemoveProtected.ColorTable = eButtonColor.OrangeWithBackground;
            this.BtnRemoveProtected.Location = new Point(2, 0xe3);
            this.BtnRemoveProtected.Name = "BtnRemoveProtected";
            this.BtnRemoveProtected.Size = new Size(0x7d, 0x17);
            this.BtnRemoveProtected.Style = eDotNetBarStyle.StyleManagerControlled;
            this.BtnRemoveProtected.TabIndex = 0xb1;
            this.BtnRemoveProtected.Text = "Remove selected item";
            this.BtnRemoveProtected.Click += new EventHandler(this.BtnRemoveProtected_Click);
            this.TBProtectedName.Border.Class = "TextBoxBorder";
            this.TBProtectedName.Border.CornerType = eCornerType.Square;
            this.TBProtectedName.Location = new Point(0x2b, 0xac);
            this.TBProtectedName.Name = "TBProtectedName";
            this.TBProtectedName.Size = new Size(0x8f, 20);
            this.TBProtectedName.TabIndex = 0xae;
            this.CBDoVendor.AutoSize = true;
            this.CBDoVendor.BackColor = Color.Transparent;
            this.CBDoVendor.BackgroundStyle.Class = "";
            this.CBDoVendor.BackgroundStyle.CornerType = eCornerType.Square;
            this.CBDoVendor.Location = new Point(3, 10);
            this.CBDoVendor.Name = "CBDoVendor";
            this.CBDoVendor.Size = new Size(0x79, 15);
            this.CBDoVendor.TabIndex = 0xad;
            this.CBDoVendor.Text = "To town on full bags";
            this.BtnAddProtected.AccessibleRole = AccessibleRole.PushButton;
            this.BtnAddProtected.ColorTable = eButtonColor.OrangeWithBackground;
            this.BtnAddProtected.Location = new Point(2, 0xc6);
            this.BtnAddProtected.Name = "BtnAddProtected";
            this.BtnAddProtected.Size = new Size(0x7d, 0x17);
            this.BtnAddProtected.Style = eDotNetBarStyle.StyleManagerControlled;
            this.BtnAddProtected.TabIndex = 0x1d;
            this.BtnAddProtected.Text = "Add protected item";
            this.BtnAddProtected.Click += new EventHandler(this.BtnAddProtected_Click);
            this.labelX26.BackColor = Color.Transparent;
            this.labelX26.BackgroundStyle.Class = "";
            this.labelX26.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX26.Location = new Point(2, 0xad);
            this.labelX26.Name = "labelX26";
            this.labelX26.Size = new Size(0x34, 0x17);
            this.labelX26.TabIndex = 0x1f;
            this.labelX26.Text = "Name:";
            this.labelX27.BackColor = Color.Transparent;
            this.labelX27.BackgroundStyle.Class = "";
            this.labelX27.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX27.Location = new Point(2, 0x95);
            this.labelX27.Name = "labelX27";
            this.labelX27.Size = new Size(0x6c, 0x17);
            this.labelX27.TabIndex = 30;
            this.labelX27.Text = "<b>Add protected item:</b>";
            this.superTabItem5.AttachedControl = this.superTabControlPanel5;
            this.superTabItem5.GlobalItem = false;
            this.superTabItem5.Name = "superTabItem5";
            this.superTabItem5.Text = "Vendor";
            this.superTabControlPanel7.Controls.Add(this.groupPanel7);
            this.superTabControlPanel7.Controls.Add(this.groupPanel6);
            this.superTabControlPanel7.Dock = DockStyle.Fill;
            this.superTabControlPanel7.Location = new Point(0, 0);
            this.superTabControlPanel7.Name = "superTabControlPanel7";
            this.superTabControlPanel7.Size = new Size(0x1bd, 370);
            this.superTabControlPanel7.TabIndex = 0;
            this.superTabControlPanel7.TabItem = this.superTabItem7;
            this.superTabControlPanel7.ThemeAware = true;
            this.groupPanel7.BackColor = Color.Transparent;
            this.groupPanel7.CanvasColor = SystemColors.Control;
            this.groupPanel7.ColorSchemeStyle = eDotNetBarStyle.Office2007;
            this.groupPanel7.Controls.Add(this.labelX12);
            this.groupPanel7.Controls.Add(this.SetupTBLogOutOnFollow);
            this.groupPanel7.Controls.Add(this.SetupCBLogoutOnFollow);
            this.groupPanel7.Controls.Add(this.SetupCBSoundFollow);
            this.groupPanel7.Controls.Add(this.labelX48);
            this.groupPanel7.Controls.Add(this.SetupTBStopAfter);
            this.groupPanel7.Controls.Add(this.SetupCBSoundWhisper);
            this.groupPanel7.Controls.Add(this.SetupCBShutdown);
            this.groupPanel7.Controls.Add(this.SetupCBSoundStop);
            this.groupPanel7.Controls.Add(this.SetupCBStopAfter);
            this.groupPanel7.Location = new Point(5, 0x7f);
            this.groupPanel7.Name = "groupPanel7";
            this.groupPanel7.Size = new Size(0x1b1, 0xa9);
            this.groupPanel7.Style.BackColor2SchemePart = eColorSchemePart.PanelBackground2;
            this.groupPanel7.Style.BackColorGradientAngle = 90;
            this.groupPanel7.Style.BackColorSchemePart = eColorSchemePart.PanelBackground;
            this.groupPanel7.Style.BorderBottom = eStyleBorderType.Solid;
            this.groupPanel7.Style.BorderBottomWidth = 1;
            this.groupPanel7.Style.BorderColorSchemePart = eColorSchemePart.PanelBorder;
            this.groupPanel7.Style.BorderLeft = eStyleBorderType.Solid;
            this.groupPanel7.Style.BorderLeftWidth = 1;
            this.groupPanel7.Style.BorderRight = eStyleBorderType.Solid;
            this.groupPanel7.Style.BorderRightWidth = 1;
            this.groupPanel7.Style.BorderTop = eStyleBorderType.Solid;
            this.groupPanel7.Style.BorderTopWidth = 1;
            this.groupPanel7.Style.Class = "";
            this.groupPanel7.Style.CornerDiameter = 4;
            this.groupPanel7.Style.CornerType = eCornerType.Rounded;
            this.groupPanel7.Style.TextAlignment = eStyleTextAlignment.Center;
            this.groupPanel7.Style.TextColorSchemePart = eColorSchemePart.PanelText;
            this.groupPanel7.Style.TextLineAlignment = eStyleTextAlignment.Near;
            this.groupPanel7.StyleMouseDown.Class = "";
            this.groupPanel7.StyleMouseDown.CornerType = eCornerType.Square;
            this.groupPanel7.StyleMouseOver.Class = "";
            this.groupPanel7.StyleMouseOver.CornerType = eCornerType.Square;
            this.groupPanel7.TabIndex = 0xad;
            this.groupPanel7.Text = "Anti detection";
            this.labelX12.BackColor = Color.Transparent;
            this.labelX12.BackgroundStyle.Class = "";
            this.labelX12.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX12.Location = new Point(0xc5, 0x17);
            this.labelX12.Name = "labelX12";
            this.labelX12.Size = new Size(0x4b, 0x17);
            this.labelX12.TabIndex = 0xac;
            this.labelX12.Text = "minuttes";
            this.SetupTBLogOutOnFollow.BackgroundStyle.Class = "DateTimeInputBackground";
            this.SetupTBLogOutOnFollow.BackgroundStyle.CornerType = eCornerType.Square;
            this.SetupTBLogOutOnFollow.ButtonFreeText.Shortcut = eShortcut.F2;
            this.SetupTBLogOutOnFollow.Location = new Point(0x6f, 0x18);
            this.SetupTBLogOutOnFollow.Name = "SetupTBLogOutOnFollow";
            this.SetupTBLogOutOnFollow.ShowUpDown = true;
            this.SetupTBLogOutOnFollow.TabIndex = 0xab;
            this.SetupCBLogoutOnFollow.AutoSize = true;
            this.SetupCBLogoutOnFollow.BackColor = Color.Transparent;
            this.SetupCBLogoutOnFollow.BackgroundStyle.Class = "";
            this.SetupCBLogoutOnFollow.BackgroundStyle.CornerType = eCornerType.Square;
            this.SetupCBLogoutOnFollow.Location = new Point(3, 0x18);
            this.SetupCBLogoutOnFollow.Name = "SetupCBLogoutOnFollow";
            this.SetupCBLogoutOnFollow.Size = new Size(0x66, 15);
            this.SetupCBLogoutOnFollow.TabIndex = 170;
            this.SetupCBLogoutOnFollow.Text = "Logout on follow";
            this.groupPanel6.BackColor = Color.Transparent;
            this.groupPanel6.CanvasColor = SystemColors.Control;
            this.groupPanel6.ColorSchemeStyle = eDotNetBarStyle.Office2007;
            this.groupPanel6.Controls.Add(this.CombatCBEat);
            this.groupPanel6.Controls.Add(this.label12);
            this.groupPanel6.Controls.Add(this.label11);
            this.groupPanel6.Controls.Add(this.CombatTBDrinkAt);
            this.groupPanel6.Controls.Add(this.CombatCBDrink);
            this.groupPanel6.Controls.Add(this.CombatTBEatAt);
            this.groupPanel6.Location = new Point(4, 6);
            this.groupPanel6.Name = "groupPanel6";
            this.groupPanel6.Size = new Size(0x1b1, 0x73);
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
            this.groupPanel6.TabIndex = 0xac;
            this.groupPanel6.Text = "Resting";
            this.superTabItem7.AttachedControl = this.superTabControlPanel7;
            this.superTabItem7.GlobalItem = false;
            this.superTabItem7.Name = "superTabItem7";
            this.superTabItem7.Text = "Limits";
            this.buttonX1.AccessibleRole = AccessibleRole.PushButton;
            this.buttonX1.ColorTable = eButtonColor.OrangeWithBackground;
            this.buttonX1.Location = new Point(340, 0x177);
            this.buttonX1.Name = "buttonX1";
            this.buttonX1.Size = new Size(0x65, 0x17);
            this.buttonX1.Style = eDotNetBarStyle.StyleManagerControlled;
            this.buttonX1.TabIndex = 4;
            this.buttonX1.Text = "Save and close";
            this.buttonX1.Click += new EventHandler(this.CloseForm);
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.FromArgb(0xdf, 0xe9, 0xf5);
            base.ClientSize = new Size(0x1bd, 0x191);
            base.Controls.Add(this.buttonX1);
            base.Controls.Add(this.superTabControl2);
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.ImeMode = ImeMode.Off;
            this.MaximumSize = new Size(0x1cd, 0x1b7);
            base.Name = "Setup";
            base.FormClosing += new FormClosingEventHandler(this.Main_FormClosing);
            base.Load += new EventHandler(this.Form1Load);
            this.superTabControlPanel2.ResumeLayout(false);
            this.CombatTBDrinkAt.EndInit();
            this.CombatTBEatAt.EndInit();
            this.SetupTBStopAfter.EndInit();
            ((ISupportInitialize) this.superTabControl2).EndInit();
            this.superTabControl2.ResumeLayout(false);
            this.superTabControlPanel4.ResumeLayout(false);
            this.superTabControlPanel4.PerformLayout();
            this.groupPanel10.ResumeLayout(false);
            this.Latency.EndInit();
            this.superTabControlPanel3.ResumeLayout(false);
            this.groupPanel3.ResumeLayout(false);
            this.ListMailItems.EndInit();
            this.groupPanel2.ResumeLayout(false);
            this.groupPanel2.PerformLayout();
            this.superTabControlPanel6.ResumeLayout(false);
            this.SetupRelogLoginData.ResumeLayout(false);
            this.SetupIIRelogLogAccount.EndInit();
            this.SetupIIRelogLogInAfter.EndInit();
            this.SetupIIRelogLogOutAfter.EndInit();
            this.superTabControlPanel1.ResumeLayout(false);
            this.superTabControlPanel12.ResumeLayout(false);
            this.groupPanel1.ResumeLayout(false);
            this.superTabControlPanel5.ResumeLayout(false);
            this.groupPanel8.ResumeLayout(false);
            this.ListProtectedItems.EndInit();
            this.groupPanel9.ResumeLayout(false);
            this.groupPanel9.PerformLayout();
            this.IMinFreeBagSlots.EndInit();
            this.superTabControlPanel7.ResumeLayout(false);
            this.groupPanel7.ResumeLayout(false);
            this.groupPanel7.PerformLayout();
            this.SetupTBLogOutOnFollow.EndInit();
            this.groupPanel6.ResumeLayout(false);
            this.groupPanel6.PerformLayout();
            base.ResumeLayout(false);
        }

        private void LoadMailList()
        {
            MailList.Load();
            foreach (string str in MailList.GetList)
            {
                this.AddMail(str);
            }
        }

        private void LoadP()
        {
            foreach (KeyValuePair<string, ILazyPlugin> pair in PluginCompiler.Assemblys)
            {
                CustomPlugin item = new CustomPlugin(pair.Key, pair.Value.GetName());
                this.PluginsList.Items.Add(item);
                this.PluginsList.SetItemChecked(this.PluginsList.Items.Count - 1, LoadPluginSettings(item.AssemblyName));
            }
        }

        private static bool LoadPluginSettings(string name)
        {
            bool flag;
            try
            {
                flag = new IniManager(LazyForms.OurDirectory + @"\Settings\lazy_plugins.ini").GetBoolean("Plugins", name, false);
            }
            catch
            {
                return false;
            }
            return flag;
        }

        private void LoadProtectedList()
        {
            ProtectedList.Load();
            foreach (string str in ProtectedList.GetList)
            {
                this.AddProtected(str);
            }
        }

        public void LoadSettings()
        {
            this.SetupDebugMode.Checked = LazySettings.DebugMode;
            this.SetupUseHotkeys.Checked = LazySettings.SetupUseHotkeys;
            this.SetupCBStopAfter.Checked = LazySettings.StopAfterBool;
            this.SetupTBStopAfter.Text = LazySettings.StopAfter;
            this.SetupTBLogOutOnFollow.Text = LazySettings.LogOutOnFollowTime;
            this.SetupCBSoundFollow.Checked = LazySettings.SoundFollow;
            this.SetupCBSoundWhisper.Checked = LazySettings.SoundWhisper;
            this.SetupCBSoundStop.Checked = LazySettings.SoundStop;
            this.SetupCBShutdown.Checked = LazySettings.Shutdown;
            this.SetupCBBackground.Checked = LazySettings.BackgroundMode;
            this.SetupCBLogoutOnFollow.Checked = LazySettings.LogoutOnFollow;
            this.CBHookMouse.Checked = LazySettings.HookMouse;
            this.Latency.Value = LazySettings.Latency;
            this.CombatCBEat.Checked = LazySettings.CombatBoolEat;
            this.CombatCBDrink.Checked = LazySettings.CombatBoolDrink;
            this.CombatTBEatAt.Text = LazySettings.CombatEatAt;
            this.CombatTBDrinkAt.Text = LazySettings.CombatDrinkAt;
            this.KeysGroundMountBar.SelectedIndex = this.KeysGroundMountBar.FindStringExact(LazySettings.KeysGroundMountBar);
            this.KeysGroundMountKey.SelectedIndex = this.KeysGroundMountKey.FindStringExact(LazySettings.KeysGroundMountKey);
            this.KeysAttack1Bar.SelectedIndex = this.KeysAttack1Bar.FindStringExact(LazySettings.KeysAttack1Bar);
            this.KeysAttack1Key.SelectedIndex = this.KeysAttack1Key.FindStringExact(LazySettings.KeysAttack1Key);
            this.KeysEatBar.SelectedIndex = this.KeysEatBar.FindStringExact(LazySettings.KeysEatBar);
            this.KeysEatKey.SelectedIndex = this.KeysEatKey.FindStringExact(LazySettings.KeysEatKey);
            this.KeysDrinkBar.SelectedIndex = this.KeysDrinkBar.FindStringExact(LazySettings.KeysDrinkBar);
            this.KeysDrinkKey.SelectedIndex = this.KeysDrinkKey.FindStringExact(LazySettings.KeysDrinkKey);
            this.KeysInteractKey.SelectedIndex = this.KeysInteractKey.FindStringExact(LazySettings.KeysInteractKeyText);
            this.KeysInteractTarget.SelectedIndex = this.KeysInteractTarget.FindStringExact(LazySettings.KeysInteractTargetText);
            this.KeysStafeLeftKey.SelectedIndex = this.KeysStafeLeftKey.FindStringExact(LazySettings.KeysStafeLeftKeyText);
            this.KeysStafeRightKey.SelectedIndex = this.KeysStafeRightKey.FindStringExact(LazySettings.KeysStafeRightKeyText);
            this.KeysTargetLast.SelectedIndex = this.KeysTargetLast.FindStringExact(LazySettings.KeysTargetLastTargetText);
            this.CBDoVendor.Checked = LazySettings.ShouldVendor;
            this.CBDoRepair.Checked = LazySettings.ShouldRepair;
            this.CBSellCommon.Checked = LazySettings.SellCommon;
            this.CBSellUnCommon.Checked = LazySettings.SellUncommon;
            this.CBSellPoor.Checked = LazySettings.SellPoor;
            this.IMinFreeBagSlots.Text = LazySettings.FreeBackspace;
            this.CBMail.Checked = LazySettings.ShouldMail;
            this.TBMailTo.Text = LazySettings.MailTo;
            this.MacroForMail.Checked = LazySettings.MacroForMail;
            this.KeysMailMacroBar.SelectedIndex = this.KeysMailMacroBar.FindStringExact(LazySettings.KeysMailMacroBar);
            this.KeysMailMacroKey.SelectedIndex = this.KeysMailMacroKey.FindStringExact(LazySettings.KeysMailMacroKey);
            this.SetupTBRelogUsername.Text = ReloggerSettings.AccountName;
            this.SetupTBRelogPW.Text = ReloggerSettings.AccountPw;
            this.SetupTBRelogCharacter.Text = ReloggerSettings.CharacterName;
            this.SetupCBRelogEnableRelogger.Checked = ReloggerSettings.ReloggingEnabled;
            this.SetupCBRelogEnablePeriodicRelog.Checked = ReloggerSettings.PeriodicReloggingEnabled;
            this.SetupIIRelogLogOutAfter.Value = ReloggerSettings.PeriodicLogOut;
            this.SetupIIRelogLogInAfter.Value = ReloggerSettings.PeriodicLogIn;
            this.SetupIIRelogLogAccount.Value = ReloggerSettings.AccountAccount;
            this.ClientLanguage.SelectedIndex = (LazySettings.Language == LazySettings.LazyLanguage.Unknown) ? 0 : this.ClientLanguage.FindStringExact(LazySettings.Language.ToString());
            this.LoadMailList();
            this.LoadProtectedList();
        }

        private void MacroForMail_CheckedChanged(object sender, EventArgs e)
        {
            if ((this.MacroForMail.Checked != LazySettings.MacroForMail) && this.MacroForMail.Checked)
            {
                MessageBox.Show("You should create a macro: /script SendMailNameEditBox:SetText(\"RECEIVERNAME\") and place it on a bar");
            }
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            base.Hide();
            this.LoadSettings();
            e.Cancel = true;
        }

        private void PluginsListItemCheck(object sender, ItemCheckEventArgs e)
        {
            try
            {
                CustomPlugin plugin = (CustomPlugin) this.PluginsList.Items[e.Index];
                WritePluginSettings(plugin.AssemblyName, e.NewValue.Equals(CheckState.Checked));
                if (e.NewValue.Equals(CheckState.Checked))
                {
                    if (!PluginCompiler.LoadedPlugins.Contains(plugin.AssemblyName))
                    {
                        PluginCompiler.PluginLoad(plugin.AssemblyName);
                    }
                }
                else if (PluginCompiler.LoadedPlugins.Contains(plugin.AssemblyName))
                {
                    PluginCompiler.PluginUnload(plugin.AssemblyName);
                }
            }
            catch (Exception exception)
            {
                Logging.Write("Could not load plugin: " + exception, new object[0]);
            }
        }

        private void SaveMailList()
        {
            MailList.Clear();
            foreach (Node node in this.ListMailItems.Nodes)
            {
                MailList.AddMail(node.Tag.ToString());
            }
            MailList.Save();
        }

        private void SaveProtectedList()
        {
            ProtectedList.Clear();
            foreach (Node node in this.ListProtectedItems.Nodes)
            {
                ProtectedList.AddProtected(node.Tag.ToString());
            }
            ProtectedList.Save();
        }

        public void SaveSettings()
        {
            LazySettings.DebugMode = this.SetupDebugMode.Checked;
            LazySettings.SetupUseHotkeys = this.SetupUseHotkeys.Checked;
            LazySettings.StopAfterBool = this.SetupCBStopAfter.Checked;
            LazySettings.StopAfter = this.SetupTBStopAfter.Text;
            LazySettings.LogOutOnFollowTime = this.SetupTBLogOutOnFollow.Text;
            LazySettings.SoundFollow = this.SetupCBSoundFollow.Checked;
            LazySettings.SoundWhisper = this.SetupCBSoundWhisper.Checked;
            LazySettings.SoundStop = this.SetupCBSoundStop.Checked;
            LazySettings.Shutdown = this.SetupCBShutdown.Checked;
            LazySettings.BackgroundMode = this.SetupCBBackground.Checked;
            LazySettings.LogoutOnFollow = this.SetupCBLogoutOnFollow.Checked;
            LazySettings.HookMouse = this.CBHookMouse.Checked;
            LazySettings.Latency = this.Latency.Value;
            LazySettings.CombatBoolEat = this.CombatCBEat.Checked;
            LazySettings.CombatBoolDrink = this.CombatCBDrink.Checked;
            LazySettings.CombatEatAt = this.CombatTBEatAt.Text;
            LazySettings.CombatDrinkAt = this.CombatTBDrinkAt.Text;
            LazySettings.KeysGroundMountBar = this.KeysGroundMountBar.SelectedItem.ToString();
            LazySettings.KeysGroundMountKey = this.KeysGroundMountKey.SelectedItem.ToString();
            LazySettings.KeysAttack1Bar = this.KeysAttack1Bar.SelectedItem.ToString();
            LazySettings.KeysAttack1Key = this.KeysAttack1Key.SelectedItem.ToString();
            LazySettings.KeysEatBar = this.KeysEatBar.SelectedItem.ToString();
            LazySettings.KeysEatKey = this.KeysEatKey.SelectedItem.ToString();
            LazySettings.KeysDrinkBar = this.KeysDrinkBar.SelectedItem.ToString();
            LazySettings.KeysDrinkKey = this.KeysDrinkKey.SelectedItem.ToString();
            LazySettings.KeysStafeLeftKeyText = this.KeysStafeLeftKey.SelectedItem.ToString();
            LazySettings.KeysStafeRightKeyText = this.KeysStafeRightKey.SelectedItem.ToString();
            LazySettings.KeysInteractKeyText = this.KeysInteractKey.SelectedItem.ToString();
            LazySettings.KeysInteractTargetText = this.KeysInteractTarget.SelectedItem.ToString();
            LazySettings.KeysTargetLastTargetText = this.KeysTargetLast.SelectedItem.ToString();
            LazySettings.ShouldMail = this.CBMail.Checked;
            LazySettings.MailTo = this.TBMailTo.Text;
            LazySettings.MacroForMail = this.MacroForMail.Checked;
            LazySettings.KeysMailMacroBar = this.KeysMailMacroBar.SelectedItem.ToString();
            LazySettings.KeysMailMacroKey = this.KeysMailMacroKey.SelectedItem.ToString();
            LazySettings.ShouldVendor = this.CBDoVendor.Checked;
            LazySettings.ShouldRepair = this.CBDoRepair.Checked;
            LazySettings.SellCommon = this.CBSellCommon.Checked;
            LazySettings.SellUncommon = this.CBSellUnCommon.Checked;
            LazySettings.SellPoor = this.CBSellPoor.Checked;
            LazySettings.FreeBackspace = this.IMinFreeBagSlots.Text;
            ReloggerSettings.AccountName = this.SetupTBRelogUsername.Text;
            ReloggerSettings.AccountPw = this.SetupTBRelogPW.Text;
            ReloggerSettings.CharacterName = this.SetupTBRelogCharacter.Text;
            ReloggerSettings.ReloggingEnabled = this.SetupCBRelogEnableRelogger.Checked;
            ReloggerSettings.PeriodicReloggingEnabled = this.SetupCBRelogEnablePeriodicRelog.Checked;
            ReloggerSettings.PeriodicLogIn = this.SetupIIRelogLogInAfter.Value;
            ReloggerSettings.PeriodicLogOut = this.SetupIIRelogLogOutAfter.Value;
            ReloggerSettings.AccountAccount = this.SetupIIRelogLogAccount.Value;
            ReloggerSettings.SaveSettings();
            string str = this.ClientLanguage.SelectedItem.ToString();
            LazySettings.LazyLanguage language = (LazySettings.LazyLanguage) Enum.Parse(typeof(LazySettings.LazyLanguage), str);
            if (LazySettings.Language != language)
            {
                if (!ItemDatabase.IsOpen)
                {
                    ItemDatabase.Open();
                }
                ItemDatabase.ClearDatabase();
            }
            LazySettings.Language = language;
            LazySettings.SaveSettings();
            this.SaveMailList();
            this.SaveProtectedList();
        }

        private void SetupCbBackgroundCheckedChanged(object sender, EventArgs e)
        {
            if ((this.SetupCBBackground.Checked && !LazySettings.BackgroundMode) && (MessageBox.Show("Enabling this will make the bot manipulate wow in a way that could be detected if warden gets an update. The chance of this getting detected is between now and never. You will have to decide for yourself.", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question).ToString() != "Yes"))
            {
                this.SetupCBBackground.Checked = false;
            }
        }

        private void SetupCBRelogEnablePeriodicRelog_CheckedChanged(object sender, EventArgs e)
        {
            if (this.SetupCBStopAfter.Checked && this.SetupCBRelogEnablePeriodicRelog.Checked)
            {
                this.SetupCBStopAfter.Checked = false;
            }
            if (this.SetupCBRelogEnablePeriodicRelog.Checked)
            {
                this.SetupIIRelogLogInAfter.Enabled = true;
                this.SetupIIRelogLogOutAfter.Enabled = true;
            }
            else
            {
                this.SetupIIRelogLogInAfter.Enabled = false;
                this.SetupIIRelogLogOutAfter.Enabled = false;
            }
        }

        private void SetupCBRelogEnableRelogger_CheckedChanged(object sender, EventArgs e)
        {
            if (this.SetupCBRelogEnableRelogger.Checked)
            {
                this.SetupRelogLoginData.Visible = true;
            }
            else
            {
                this.SetupCBRelogEnablePeriodicRelog.Checked = false;
                this.SetupRelogLoginData.Visible = false;
            }
        }

        private void SetupCBStopAfter_CheckedChanged(object sender, EventArgs e)
        {
            if (this.SetupCBRelogEnablePeriodicRelog.Checked && this.SetupCBStopAfter.Checked)
            {
                this.SetupCBRelogEnablePeriodicRelog.Checked = false;
            }
        }

        private void superTabControlPanel4_Click(object sender, EventArgs e)
        {
        }

        public void UpdateProgressBar(ProgressBarX progressBarX, int healtPercentage)
        {
            MethodInvoker method = null;
            if (!progressBarX.InvokeRequired)
            {
                progressBarX.Value = healtPercentage;
            }
            else
            {
                if (method == null)
                {
                    method = () => this.UpdateProgressBar(progressBarX, healtPercentage);
                }
                progressBarX.BeginInvoke(method);
            }
        }

        public void UpdateText(ButtonX lab, string text)
        {
            MethodInvoker method = null;
            if (!lab.InvokeRequired)
            {
                lab.Text = text;
            }
            else
            {
                if (method == null)
                {
                    method = () => this.UpdateText(lab, text);
                }
                lab.Invoke(method);
            }
        }

        public void UpdateTextLabel(LabelX labelX, string text)
        {
            MethodInvoker method = null;
            if (!labelX.InvokeRequired)
            {
                labelX.Text = text;
            }
            else
            {
                if (method == null)
                {
                    method = () => this.UpdateTextLabel(labelX, text);
                }
                labelX.BeginInvoke(method);
            }
        }

        public void UpdateTitle(string text)
        {
            MethodInvoker method = null;
            if (!base.InvokeRequired)
            {
                this.Text = text;
            }
            else
            {
                if (method == null)
                {
                    method = () => this.UpdateTitle(text);
                }
                this.Invoke(method);
            }
        }

        private static void WritePluginSettings(string name, bool enabled)
        {
            try
            {
                new IniManager(LazyForms.OurDirectory + @"\Settings\lazy_plugins.ini").IniWriteValue("Plugins", name, enabled.ToString());
            }
            catch
            {
            }
        }
    }
}

