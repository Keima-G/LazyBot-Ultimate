namespace LazyEvo.Forms
{
    using DevComponents.DotNetBar;
    using DevComponents.DotNetBar.Controls;
    using DevComponents.DotNetBar.Metro.ColorTables;
    using DevComponents.Editors;
    using LazyEvo;
    using LazyEvo.Classes;
    using LazyEvo.Forms.Helpers;
    using LazyEvo.Other;
    using LazyEvo.Plugins;
    using LazyEvo.Plugins.RotationPlugin;
    using LazyEvo.PluginSystem;
    using LazyEvo.Public;
    using LazyLib;
    using LazyLib.ActionBar;
    using LazyLib.Combat;
    using LazyLib.FSM;
    using LazyLib.Helpers;
    using LazyLib.IEngine;
    using LazyLib.LazyRadar;
    using LazyLib.LazyRadar.Drawer;
    using LazyLib.Wow;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.IO;
    using System.Media;
    using System.Threading;
    using System.Windows.Forms;

    internal class Main : Office2007Form
    {
        private const string LazyVersion = "Lazybot De-Evolution r.29 For 3.3.5a";
        internal static LazyLib.Combat.CombatEngine CombatEngine;
        internal static ILazyEngine EngineHandler;
        internal static bool OneInstance;
        private readonly List<ButtonItem> _buttons = new List<ButtonItem>();
        private readonly SoundPlayer _soundPlayer = new SoundPlayer();
        internal bool ShouldRelog;
        private Hotkey _f10;
        private Hotkey _f9;
        private bool _hotKeysLoaded;
        private RadarForm _radar;
        public static int _prev_dice;
        public static bool chatcommand = false;
        public static bool randomchatenable = false;
        private Thread _killDummy;
        private Process[] _wowProc = Process.GetProcessesByName("Wow");
        public StyleManager styleManager1;
        public ButtonItem OpenLazySettings;
        public RichTextBox LogWin;
        public ProgressBarX MainPBPlayerXP;
        public RichTextBox ChatAll;
        public RichTextBox ChatWhisper;
        public ButtonItem OpenSettings;
        public ComboItem comboItem330;
        public ComboItem comboItem331;
        public ComboItem comboItem3;
        public ComboItem comboItem1;
        public StatusStrip statusStrip1;
        public ToolStripStatusLabel StatsText;
        private ExpandableSplitter expandableSplitter1;
        private ItemPanel ControlSettings;
        private LabelItem labelItem1;
        private LabelItem labelItem2;
        private ButtonItem CombatSettings;
        private ButtonItem EngineSettings;
        private LabelItem labelItem3;
        private ButtonItem BtnOpenRadar;
        private ButtonItem BtnProfileSettings;
        private ButtonItem BtnDebug;
        private SuperTabControl superTabControl5;
        private SuperTabControlPanel superTabControlPanel31;
        private SuperTabItem superTabItem31;
        private SuperTabControlPanel superTabControlPanel5;
        private SuperTabItem superTabItem5;
        private SuperTabControlPanel superTabControlPanel30;
        private SuperTabItem superTabItem30;
        private ButtonX StartStopEngine;
        private GroupPanel groupPanel3;
        private LabelX LBVersion;
        private LabelX MainLBPlayerXPH;
        private LabelX MainLBPlayerTTL;
        private LabelX labelX36;
        private LabelX labelX5;
        private GroupPanel groupPanel12;
        private GroupPanel groupPanel11;
        private LabelX labelX24;
        private ButtonX ChatSendText;
        private LabelX labelX3;
        private TextBoxX ChatTBSendText;
        private GroupPanel groupPanel10;
        private ButtonX StopnAttackTargetDummy;
        private ButtonX BtnAttackTargetDummy;
        private ButtonX DebugBtnLogTargetBuff;
        private ButtonX DebugBtnLogOwnBuff;
        private ButtonX DebugBtnShouldRepair;
        private ButtonX DebugBtnClassRecompile;
        private GroupPanel groupPanel5;
        private ButtonX MainBtnRefreshProcess;
        private ButtonX MainBtnSelectProcess;
        private ButtonItem GeneralSettings;
        private ControlContainerItem controlContainerItem1;
        private ControlContainerItem controlContainerItem2;
        private CheckBoxX CBTopMost;
        private ButtonX buttonX1;
        private ButtonX buttonX2;
        private LabelX labelX1;
        private CheckBoxX CBDebug;
        private ButtonItem OpenRotator;
        private ComboBoxEx MainComProcessSelection;
        private ComboBoxEx SelectEngine;
        private ComboBoxEx GrindSelectCombat;
        private ComboBoxEx SelectCombat;
        private DevComponents.DotNetBar.Wizard wizard1;
        private IContainer components;
        private DevComponents.DotNetBar.Wizard wizard2;
        public GroupPanel MainGPPlayer;
        public LabelX MainLBPlayerPower;
        public LabelX MainLBPowerType;
        public LabelX MainLBPlayerXP;
        public LabelX MainLBPlayerHP;
        public ProgressBarX MainPBPlayerPower;
        public ProgressBarX MainPBPlayerHP;

        public Main()
        {
            this.InitializeComponent();
            GeomertrySettings.LoadSettings();
            Geometry.GeometryFromString(GeomertrySettings.MainGeometry, this);
            Logging.OnWrite += new Logging.WriteDelegate(this.Logging_OnWrite);
            this.BtnDebug.Visible = true;
        }

        private void AddToControlSettings(ButtonItem button)
        {
            if (!this.ControlSettings.InvokeRequired)
            {
                this.ControlSettings.Items.Add(button);
            }
            else
            {
                object[] args = new object[] { button };
                this.ControlSettings.Invoke(new Action<ButtonItem>(this.AddToControlSettings), args);
            }
        }

        private static void AppendMessage(RichTextBox textBox, string message, Color col)
        {
            try
            {
                if (!textBox.IsDisposed)
                {
                    if (textBox.InvokeRequired)
                    {
                        object[] args = new object[] { textBox, message, col };
                        textBox.Invoke(new Action<RichTextBox, string, Color>(Main.AppendMessage), args);
                    }
                    else
                    {
                        Color selectionColor = textBox.SelectionColor;
                        textBox.SelectionColor = col;
                        textBox.AppendText(message);
                        textBox.SelectionColor = selectionColor;
                        textBox.AppendText(Environment.NewLine);
                        textBox.ScrollToCaret();
                    }
                }
            }
            catch (ThreadAbortException)
            {
            }
            catch (Exception exception)
            {
                Logging.Write("Debug Write Failed:  " + exception, new object[0]);
            }
        }

        private void BtnAttackTargetDummyClick(object sender, EventArgs e)
        {
            if (LazyLib.Wow.ObjectManager.InGame)
            {
                Langs.Load();
                if (LazyLib.Wow.ObjectManager.MyPlayer.IsValid && Langs.TrainingDummy(LazyLib.Wow.ObjectManager.MyPlayer.Target.Name))
                {
                    if ((this._killDummy == null) || !this._killDummy.IsAlive)
                    {
                        KeyHelper.LoadKeys();
                        BarMapper.MapBars();
                        if (!CombatEngine.StartOk)
                        {
                            Logging.Write(LogType.Warning, "CustomClass returned false on StartOk not starting", new object[0]);
                            return;
                        }
                        CombatEngine.BotStarted();
                        this._killDummy = new Thread(new ThreadStart(this.KillTheDummy));
                        this._killDummy.Name = "KillDummy";
                        this._killDummy.IsBackground = true;
                        this._killDummy.Start();
                    }
                    this.BtnAttackTargetDummy.Enabled = false;
                }
                else
                {
                    Logging.Write("Please target a Training dummy ingame", new object[0]);
                }
            }
        }

        private void BtnDebugClick(object sender, EventArgs e)
        {
            new LazyEvo.Forms.Debug().Show();
        }

        private void BtnOpenRadarClick(object sender, EventArgs e)
        {
            this.OpenRadar();
        }

        private void BtnProfileSettingsClick(object sender, EventArgs e)
        {
            if (EngineHandler != null)
            {
                EngineHandler.ProfileForm.Show();
            }
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
        }

        private void CBDebug_CheckedChanged(object sender, EventArgs e)
        {
            if (this.CBDebug.Checked)
            {
                LazySettings.DebugLog = true;
                LazySettings.SaveSettings();
                Logging.OnDebug += new Logging.DebugDelegate(this.Logging_OnDebug);
            }
            else
            {
                LazySettings.DebugLog = false;
                LazySettings.SaveSettings();
                Logging.OnDebug -= new Logging.DebugDelegate(this.Logging_OnDebug);
            }
        }

        private void CBTopMostCheckedChanged(object sender, EventArgs e)
        {
            base.TopMost = this.CBTopMost.Checked;
        }

        public void ChatMessage(string message)
        {
            AppendMessage(this.ChatAll, message, Color.Black);
        }

        public void ChatNewChatMessage(object sender, GChatEventArgs e)
        {
            try
            {
                ChatMsg msg = e.Msg;
                if (randomchatenable && ((msg.Type == Constants.ChatType.Whisper) && (msg.Player == "Sturm")))
                {
                    RandomResponse(msg.Player);
                    if (msg.Msg.Contains("HealMe"))
                    {
                        object[] args = new object[] { msg.Player, msg.Msg };
                        Logging.Debug("Got a Chat Command from: {0}  The Command is: {1}", args);
                        ChatQueu.AddChat("/w " + msg.Player + " I got my eye on YOU \x00d5o!");
                        ChatQueu.AddChat("/s ");
                        Logging.Debug("Target is set to: " + msg.Player, new object[0]);
                        Logging.Debug("Target GUID: " + msg.chatGUID, new object[0]);
                        LazyLib.Wow.ObjectManager.MyPlayer.TargetChatTargetNoMemWrite(msg.chatGUID);
                    }
                }
                if (((msg.Type != Constants.ChatType.Whisper) && (msg.Type != Constants.ChatType.RealId)) || (msg.Player == LazyLib.Wow.ObjectManager.MyPlayer.Name))
                {
                    this.ChatMessage("Type: " + msg.Type.ToString().ToLower() + ", Player Name: " + msg.Player + ", Text: " + msg.Msg);
                }
                else
                {
                    if (LazySettings.SoundWhisper && Engine.Running)
                    {
                        try
                        {
                            if (File.Exists(LazySettings.OurDirectory + @"\palert.wav"))
                            {
                                this._soundPlayer.SoundLocation = LazySettings.OurDirectory + @"\palert.wav";
                                this._soundPlayer.Play();
                            }
                        }
                        catch
                        {
                        }
                    }
                    if (msg.Type == Constants.ChatType.RealId)
                    {
                        this.WhisperMessage("Type: " + msg.Type.ToString().ToLower() + ", Text: " + msg.Msg);
                        Logging.Write(LogType.Warning, string.Concat(new object[] { "Type: ", msg.Type, ", Text: ", msg.Msg }), new object[0]);
                    }
                    else
                    {
                        this.WhisperMessage("Type: " + msg.Type.ToString().ToLower() + ", Player Name: " + msg.Player + ", Text: " + msg.Msg);
                        Logging.Write(LogType.Warning, string.Concat(new object[] { "Type: ", msg.Type, ", Player Name: ", msg.Player, ", Text: ", msg.Msg }), new object[0]);
                    }
                }
            }
            catch
            {
            }
        }

        private void ChatSendTextClick(object sender, EventArgs e)
        {
            if (this.ChatTBSendText.Text != "")
            {
                ChatQueu.AddChat(this.ChatTBSendText.Text);
                this.ChatTBSendText.Text = "";
            }
        }

        private void CombatSettings_Click(object sender, EventArgs e)
        {
            if (CombatEngine != null)
            {
                CombatEngine.Settings().Show();
            }
        }

        private void DebugBtnClassRecompileClick(object sender, EventArgs e)
        {
            ClassCompiler.RecompileAll();
        }

        private void DebugBtnLogOwnBuffClick(object sender, EventArgs e)
        {
            if (LazyLib.Wow.ObjectManager.InGame)
            {
                foreach (PUnit.WoWAura aura in LazyLib.Wow.ObjectManager.MyPlayer.GetAuras)
                {
                    try
                    {
                        object[] objArray = new object[] { aura.SpellId, " : ", WowHeadData.GetWowHeadSpell((double) aura.SpellId), " : Is player owner: " };
                        objArray[4] = (aura.OwnerGUID == LazyLib.Wow.ObjectManager.MyPlayer.GUID) ? ((object) 1) : ((object) (aura.OwnerGUID == LazyLib.Wow.ObjectManager.MyPlayer.PetGUID));
                        objArray[5] = " : Stack:";
                        objArray[6] = aura.Stack;
                        objArray[7] = " : Seconds left: ";
                        objArray[8] = aura.SecondsLeft;
                        Logging.Write(string.Concat(objArray), new object[0]);
                    }
                    catch
                    {
                        Logging.Write(aura.SpellId, new object[0]);
                    }
                }
            }
        }

        private void DebugBtnLogTargetBuffClick(object sender, EventArgs e)
        {
            if (LazyLib.Wow.ObjectManager.InGame && LazyLib.Wow.ObjectManager.MyPlayer.IsValid)
            {
                foreach (PUnit.WoWAura aura in LazyLib.Wow.ObjectManager.MyPlayer.Target.GetAuras)
                {
                    try
                    {
                        object[] objArray = new object[] { aura.SpellId, " : ", WowHeadData.GetWowHeadSpell((double) aura.SpellId), " : Is player owner: " };
                        objArray[4] = (aura.OwnerGUID == LazyLib.Wow.ObjectManager.MyPlayer.GUID) ? ((object) 1) : ((object) (aura.OwnerGUID == LazyLib.Wow.ObjectManager.MyPlayer.PetGUID));
                        objArray[5] = " : Stack:";
                        objArray[6] = aura.Stack;
                        objArray[7] = " : Seconds left: ";
                        objArray[8] = aura.SecondsLeft;
                        Logging.Write(string.Concat(objArray), new object[0]);
                    }
                    catch
                    {
                        Logging.Write(aura.SpellId, new object[0]);
                    }
                }
            }
        }

        private void DebugBtnShouldRepairClick(object sender, EventArgs e)
        {
            if (LazyLib.Wow.ObjectManager.InGame)
            {
                Logging.Write("Should repair: " + LazyLib.Wow.ObjectManager.MyPlayer.ShouldRepair, new object[0]);
            }
        }

        public void DisableButton(ButtonX button)
        {
            MethodInvoker method = null;
            if (!button.InvokeRequired)
            {
                button.Enabled = false;
            }
            else
            {
                if (method == null)
                {
                    method = () => this.DisableButton(button);
                }
                button.Invoke(method);
            }
        }

        private void DisableItems()
        {
            this.UpdateComboBoxEx(this.SelectEngine, false);
            this.UpdateButtonItem(this.GeneralSettings, false);
            this.UpdateButtonItem(this.EngineSettings, false);
            this.UpdateComboBoxEx(this.SelectCombat, false);
            this.UpdateButtonItem(this.CombatSettings, false);
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

        public void EnableButton(ButtonItem button)
        {
            MethodInvoker method = null;
            if (!button.InvokeRequired)
            {
                button.Enabled = true;
            }
            else
            {
                if (method == null)
                {
                    method = () => this.EnableButton(button);
                }
                button.Invoke(method);
            }
        }

        public void EnableButton(ButtonX button)
        {
            MethodInvoker method = null;
            if (!button.InvokeRequired)
            {
                button.Enabled = true;
            }
            else
            {
                if (method == null)
                {
                    method = () => this.EnableButton(button);
                }
                button.Invoke(method);
            }
        }

        public void EnableComboBox(ComboBoxEx combo)
        {
            MethodInvoker method = null;
            if (!combo.InvokeRequired)
            {
                combo.Enabled = true;
            }
            else
            {
                if (method == null)
                {
                    method = () => this.EnableComboBox(combo);
                }
                combo.Invoke(method);
            }
        }

        public void EnableItems()
        {
            this.UpdateComboBoxEx(this.SelectEngine, true);
            this.UpdateButtonItem(this.GeneralSettings, true);
            this.UpdateButtonItem(this.EngineSettings, true);
            this.UpdateComboBoxEx(this.SelectCombat, true);
            this.UpdateButtonItem(this.CombatSettings, true);
        }

        private void EngineSettings_Click(object sender, EventArgs e)
        {
            if (EngineHandler != null)
            {
                EngineHandler.Settings.Show();
            }
        }

        private void Expander(object sender, ExpandedChangeEventArgs e)
        {
            this.Size = this.expandableSplitter1.Expanded ? new Size(0x1e1, 0x1bd) : new Size(350, 0x1bd);
        }

        public void HideThis()
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
                    method = () => this.HideThis();
                }
                this.Invoke(method);
            }
        }

        public void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            ComponentResourceManager manager = new ComponentResourceManager(typeof(Main));
            this.styleManager1 = new StyleManager(this.components);
            this.expandableSplitter1 = new ExpandableSplitter();
            this.ControlSettings = new ItemPanel();
            this.SelectEngine = new ComboBoxEx();
            this.comboItem330 = new ComboItem();
            this.comboItem331 = new ComboItem();
            this.SelectCombat = new ComboBoxEx();
            this.comboItem1 = new ComboItem();
            this.labelItem1 = new LabelItem();
            this.controlContainerItem2 = new ControlContainerItem();
            this.GeneralSettings = new ButtonItem();
            this.EngineSettings = new ButtonItem();
            this.BtnProfileSettings = new ButtonItem();
            this.labelItem2 = new LabelItem();
            this.controlContainerItem1 = new ControlContainerItem();
            this.CombatSettings = new ButtonItem();
            this.BtnDebug = new ButtonItem();
            this.labelItem3 = new LabelItem();
            this.BtnOpenRadar = new ButtonItem();
            this.OpenRotator = new ButtonItem();
            this.superTabControl5 = new SuperTabControl();
            this.superTabControlPanel5 = new SuperTabControlPanel();
            this.CBDebug = new CheckBoxX();
            this.CBTopMost = new CheckBoxX();
            this.buttonX2 = new ButtonX();
            this.MainGPPlayer = new GroupPanel();
            this.labelX1 = new LabelX();
            this.MainPBPlayerHP = new ProgressBarX();
            this.buttonX1 = new ButtonX();
            this.MainLBPlayerPower = new LabelX();
            this.LBVersion = new LabelX();
            this.MainPBPlayerPower = new ProgressBarX();
            this.MainLBPowerType = new LabelX();
            this.MainLBPlayerXPH = new LabelX();
            this.MainLBPlayerTTL = new LabelX();
            this.MainLBPlayerXP = new LabelX();
            this.MainLBPlayerHP = new LabelX();
            this.MainPBPlayerXP = new ProgressBarX();
            this.labelX36 = new LabelX();
            this.labelX5 = new LabelX();
            this.StartStopEngine = new ButtonX();
            this.groupPanel3 = new GroupPanel();
            this.LogWin = new RichTextBox();
            this.superTabItem5 = new SuperTabItem();
            this.superTabControlPanel31 = new SuperTabControlPanel();
            this.groupPanel10 = new GroupPanel();
            this.StopnAttackTargetDummy = new ButtonX();
            this.BtnAttackTargetDummy = new ButtonX();
            this.DebugBtnLogTargetBuff = new ButtonX();
            this.DebugBtnLogOwnBuff = new ButtonX();
            this.DebugBtnShouldRepair = new ButtonX();
            this.DebugBtnClassRecompile = new ButtonX();
            this.groupPanel5 = new GroupPanel();
            this.MainBtnRefreshProcess = new ButtonX();
            this.MainBtnSelectProcess = new ButtonX();
            this.MainComProcessSelection = new ComboBoxEx();
            this.superTabItem31 = new SuperTabItem();
            this.superTabControlPanel30 = new SuperTabControlPanel();
            this.groupPanel12 = new GroupPanel();
            this.ChatAll = new RichTextBox();
            this.groupPanel11 = new GroupPanel();
            this.ChatWhisper = new RichTextBox();
            this.labelX24 = new LabelX();
            this.ChatSendText = new ButtonX();
            this.labelX3 = new LabelX();
            this.ChatTBSendText = new TextBoxX();
            this.superTabItem30 = new SuperTabItem();
            this.GrindSelectCombat = new ComboBoxEx();
            this.comboItem3 = new ComboItem();
            this.statusStrip1 = new StatusStrip();
            this.StatsText = new ToolStripStatusLabel();
            this.wizard1 = new DevComponents.DotNetBar.Wizard();
            this.wizard2 = new DevComponents.DotNetBar.Wizard();
            this.ControlSettings.SuspendLayout();
            ((ISupportInitialize) this.superTabControl5).BeginInit();
            this.superTabControl5.SuspendLayout();
            this.superTabControlPanel5.SuspendLayout();
            this.MainGPPlayer.SuspendLayout();
            this.groupPanel3.SuspendLayout();
            this.superTabControlPanel31.SuspendLayout();
            this.groupPanel10.SuspendLayout();
            this.groupPanel5.SuspendLayout();
            this.superTabControlPanel30.SuspendLayout();
            this.groupPanel12.SuspendLayout();
            this.groupPanel11.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            base.SuspendLayout();
            this.styleManager1.ManagerStyle = eStyle.Windows7Blue;
            this.styleManager1.MetroColorParameters = new MetroColorGeneratorParameters(Color.White, Color.FromArgb(0x2b, 0x57, 0x9a));
            this.expandableSplitter1.BackColor = Color.FromArgb(0xf7, 0xfb, 0xff);
            this.expandableSplitter1.BackColor2 = Color.FromArgb(0x84, 0x92, 0xa6);
            this.expandableSplitter1.BackColor2SchemePart = eColorSchemePart.PanelBorder;
            this.expandableSplitter1.BackColorSchemePart = eColorSchemePart.PanelBackground;
            this.expandableSplitter1.Enabled = false;
            this.expandableSplitter1.ExpandableControl = this.ControlSettings;
            this.expandableSplitter1.ExpandFillColor = Color.FromArgb(0x84, 0x92, 0xa6);
            this.expandableSplitter1.ExpandFillColorSchemePart = eColorSchemePart.PanelBorder;
            this.expandableSplitter1.ExpandLineColor = Color.FromArgb(0x25, 0x42, 100);
            this.expandableSplitter1.ExpandLineColorSchemePart = eColorSchemePart.ItemText;
            this.expandableSplitter1.GripDarkColor = Color.FromArgb(0x25, 0x42, 100);
            this.expandableSplitter1.GripDarkColorSchemePart = eColorSchemePart.ItemText;
            this.expandableSplitter1.GripLightColor = Color.FromArgb(0xf6, 0xfb, 0xff);
            this.expandableSplitter1.GripLightColorSchemePart = eColorSchemePart.BarBackground;
            this.expandableSplitter1.HotBackColor = Color.FromArgb(0xfc, 0x97, 0x3d);
            this.expandableSplitter1.HotBackColor2 = Color.FromArgb(0xff, 0xb8, 0x5e);
            this.expandableSplitter1.HotBackColor2SchemePart = eColorSchemePart.ItemPressedBackground2;
            this.expandableSplitter1.HotBackColorSchemePart = eColorSchemePart.ItemPressedBackground;
            this.expandableSplitter1.HotExpandFillColor = Color.FromArgb(0x84, 0x92, 0xa6);
            this.expandableSplitter1.HotExpandFillColorSchemePart = eColorSchemePart.PanelBorder;
            this.expandableSplitter1.HotExpandLineColor = Color.FromArgb(0x25, 0x42, 100);
            this.expandableSplitter1.HotExpandLineColorSchemePart = eColorSchemePart.ItemText;
            this.expandableSplitter1.HotGripDarkColor = Color.FromArgb(0x84, 0x92, 0xa6);
            this.expandableSplitter1.HotGripDarkColorSchemePart = eColorSchemePart.PanelBorder;
            this.expandableSplitter1.HotGripLightColor = Color.FromArgb(0xf6, 0xfb, 0xff);
            this.expandableSplitter1.HotGripLightColorSchemePart = eColorSchemePart.BarBackground;
            this.expandableSplitter1.Location = new Point(130, 0);
            this.expandableSplitter1.Name = "expandableSplitter1";
            this.expandableSplitter1.Size = new Size(10, 0x196);
            this.expandableSplitter1.Style = eSplitterStyle.Office2007;
            this.expandableSplitter1.TabIndex = 0x40;
            this.expandableSplitter1.TabStop = false;
            this.expandableSplitter1.ExpandedChanged += new ExpandChangeEventHandler(this.Expander);
            this.ControlSettings.BackColor = Color.LightGray;
            this.ControlSettings.BackgroundStyle.BackColor = Color.FromArgb(0xdf, 0xea, 0xf6);
            this.ControlSettings.BackgroundStyle.BorderBottom = eStyleBorderType.Solid;
            this.ControlSettings.BackgroundStyle.BorderBottomWidth = 1;
            this.ControlSettings.BackgroundStyle.BorderColor = Color.FromArgb(0x7f, 0x9d, 0xb9);
            this.ControlSettings.BackgroundStyle.BorderLeft = eStyleBorderType.Solid;
            this.ControlSettings.BackgroundStyle.BorderLeftWidth = 1;
            this.ControlSettings.BackgroundStyle.BorderRight = eStyleBorderType.Solid;
            this.ControlSettings.BackgroundStyle.BorderRightWidth = 1;
            this.ControlSettings.BackgroundStyle.BorderTop = eStyleBorderType.Solid;
            this.ControlSettings.BackgroundStyle.BorderTopWidth = 1;
            this.ControlSettings.BackgroundStyle.CornerType = eCornerType.Square;
            this.ControlSettings.BackgroundStyle.PaddingBottom = 1;
            this.ControlSettings.BackgroundStyle.PaddingLeft = 1;
            this.ControlSettings.BackgroundStyle.PaddingRight = 1;
            this.ControlSettings.BackgroundStyle.PaddingTop = 1;
            this.ControlSettings.ContainerControlProcessDialogKey = true;
            this.ControlSettings.Controls.Add(this.SelectEngine);
            this.ControlSettings.Controls.Add(this.SelectCombat);
            this.ControlSettings.Dock = DockStyle.Left;
            this.ControlSettings.DragDropSupport = true;
            BaseItem[] items = new BaseItem[] { this.labelItem1, this.controlContainerItem2, this.GeneralSettings, this.EngineSettings, this.BtnProfileSettings, this.labelItem2, this.controlContainerItem1, this.CombatSettings, this.BtnDebug };
            items[9] = this.labelItem3;
            items[10] = this.BtnOpenRadar;
            items[11] = this.OpenRotator;
            this.ControlSettings.Items.AddRange(items);
            this.ControlSettings.LayoutOrientation = eOrientation.Vertical;
            this.ControlSettings.Location = new Point(0, 0);
            this.ControlSettings.Name = "ControlSettings";
            this.ControlSettings.Size = new Size(130, 0x196);
            this.ControlSettings.Style = eDotNetBarStyle.Windows7;
            this.ControlSettings.TabIndex = 0x3f;
            this.ControlSettings.Text = "Select e";
            this.ControlSettings.ThemeAware = true;
            this.SelectEngine.DisplayMember = "Text";
            this.SelectEngine.DrawMode = DrawMode.OwnerDrawFixed;
            this.SelectEngine.DropDownStyle = ComboBoxStyle.DropDownList;
            this.SelectEngine.FormattingEnabled = true;
            this.SelectEngine.ItemHeight = 14;
            object[] objArray = new object[] { this.comboItem330, this.comboItem331 };
            this.SelectEngine.Items.AddRange(objArray);
            this.SelectEngine.Location = new Point(4, 0x11);
            this.SelectEngine.Name = "SelectEngine";
            this.SelectEngine.Size = new Size(0x7b, 20);
            this.SelectEngine.Style = eDotNetBarStyle.StyleManagerControlled;
            this.SelectEngine.TabIndex = 0x4c;
            this.SelectEngine.Visible = false;
            this.SelectEngine.SelectedIndexChanged += new EventHandler(this.SelectEngineSelectedIndexChanged);
            this.comboItem330.Text = "Grinding";
            this.comboItem331.Text = "Flying gathering";
            this.SelectCombat.DisplayMember = "Text";
            this.SelectCombat.DrawMode = DrawMode.OwnerDrawFixed;
            this.SelectCombat.DropDownStyle = ComboBoxStyle.DropDownList;
            this.SelectCombat.FormattingEnabled = true;
            this.SelectCombat.ItemHeight = 14;
            object[] objArray2 = new object[] { this.comboItem1 };
            this.SelectCombat.Items.AddRange(objArray2);
            this.SelectCombat.Location = new Point(4, 0x87);
            this.SelectCombat.Name = "SelectCombat";
            this.SelectCombat.Size = new Size(0x7b, 20);
            this.SelectCombat.Style = eDotNetBarStyle.StyleManagerControlled;
            this.SelectCombat.TabIndex = 0x4b;
            this.SelectCombat.Visible = false;
            this.SelectCombat.SelectedIndexChanged += new EventHandler(this.SelectCombatSelectedIndexChanged);
            this.comboItem1.Text = "Behavior Engine";
            this.labelItem1.Name = "labelItem1";
            this.labelItem1.Text = "<b>Engine:</b>";
            this.labelItem1.ThemeAware = true;
            this.controlContainerItem2.AllowItemResize = false;
            this.controlContainerItem2.Control = this.SelectEngine;
            this.controlContainerItem2.MenuVisibility = eMenuVisibility.VisibleAlways;
            this.controlContainerItem2.Name = "controlContainerItem2";
            this.controlContainerItem2.ThemeAware = true;
            this.GeneralSettings.Name = "GeneralSettings";
            this.GeneralSettings.Shape = new RoundRectangleShapeDescriptor(2);
            this.GeneralSettings.Text = "General settings";
            this.GeneralSettings.ThemeAware = true;
            this.GeneralSettings.Click += new EventHandler(this.LazySettingsClick);
            this.EngineSettings.Name = "EngineSettings";
            this.EngineSettings.Text = "Engine settings";
            this.EngineSettings.ThemeAware = true;
            this.EngineSettings.Click += new EventHandler(this.EngineSettings_Click);
            this.BtnProfileSettings.Name = "BtnProfileSettings";
            this.BtnProfileSettings.Text = "Profile settings";
            this.BtnProfileSettings.ThemeAware = true;
            this.BtnProfileSettings.Click += new EventHandler(this.BtnProfileSettingsClick);
            this.labelItem2.Name = "labelItem2";
            this.labelItem2.Text = "<b>Combat system:</b>";
            this.labelItem2.ThemeAware = true;
            this.controlContainerItem1.AllowItemResize = false;
            this.controlContainerItem1.Control = this.SelectCombat;
            this.controlContainerItem1.MenuVisibility = eMenuVisibility.VisibleAlways;
            this.controlContainerItem1.Name = "controlContainerItem1";
            this.controlContainerItem1.ThemeAware = true;
            this.CombatSettings.Name = "CombatSettings";
            this.CombatSettings.Text = "Combat settings";
            this.CombatSettings.ThemeAware = true;
            this.CombatSettings.Click += new EventHandler(this.CombatSettings_Click);
            this.BtnDebug.Name = "BtnDebug";
            this.BtnDebug.Text = "Debug";
            this.BtnDebug.ThemeAware = true;
            this.BtnDebug.Visible = false;
            this.BtnDebug.Click += new EventHandler(this.BtnDebugClick);
            this.labelItem3.Name = "labelItem3";
            this.labelItem3.Text = "<b>Plugins:</b>";
            this.labelItem3.ThemeAware = true;
            this.BtnOpenRadar.Name = "BtnOpenRadar";
            this.BtnOpenRadar.Text = "Open radar";
            this.BtnOpenRadar.ThemeAware = true;
            this.BtnOpenRadar.Click += new EventHandler(this.BtnOpenRadarClick);
            this.OpenRotator.Name = "OpenRotator";
            this.OpenRotator.Text = "Rotator";
            this.OpenRotator.ThemeAware = true;
            this.OpenRotator.Click += new EventHandler(this.OpenRotator_Click);
            this.superTabControl5.ControlBox.CloseBox.Name = "";
            this.superTabControl5.ControlBox.MenuBox.Name = "";
            this.superTabControl5.ControlBox.Name = "";
            BaseItem[] itemArray2 = new BaseItem[] { this.superTabControl5.ControlBox.MenuBox, this.superTabControl5.ControlBox.CloseBox };
            this.superTabControl5.ControlBox.SubItems.AddRange(itemArray2);
            this.superTabControl5.Controls.Add(this.superTabControlPanel5);
            this.superTabControl5.Controls.Add(this.superTabControlPanel31);
            this.superTabControl5.Controls.Add(this.superTabControlPanel30);
            this.superTabControl5.Dock = DockStyle.Top;
            this.superTabControl5.Location = new Point(140, 0);
            this.superTabControl5.Name = "superTabControl5";
            this.superTabControl5.ReorderTabsEnabled = true;
            this.superTabControl5.SelectedTabFont = new Font("Segoe UI", 9f, FontStyle.Bold);
            this.superTabControl5.SelectedTabIndex = 0;
            this.superTabControl5.Size = new Size(0x161, 0x17e);
            this.superTabControl5.TabAlignment = eTabStripAlignment.Bottom;
            this.superTabControl5.TabFont = new Font("Segoe UI", 9f);
            this.superTabControl5.TabIndex = 0x48;
            BaseItem[] itemArray3 = new BaseItem[] { this.superTabItem5, this.superTabItem30, this.superTabItem31 };
            this.superTabControl5.Tabs.AddRange(itemArray3);
            this.superTabControl5.TabStyle = eSuperTabStyle.WinMediaPlayer12;
            this.superTabControl5.Text = "superTabControl5";
            this.superTabControlPanel5.Controls.Add(this.CBDebug);
            this.superTabControlPanel5.Controls.Add(this.CBTopMost);
            this.superTabControlPanel5.Controls.Add(this.buttonX2);
            this.superTabControlPanel5.Controls.Add(this.MainGPPlayer);
            this.superTabControlPanel5.Controls.Add(this.StartStopEngine);
            this.superTabControlPanel5.Controls.Add(this.groupPanel3);
            this.superTabControlPanel5.Dock = DockStyle.Fill;
            this.superTabControlPanel5.Location = new Point(0, 0);
            this.superTabControlPanel5.Name = "superTabControlPanel5";
            this.superTabControlPanel5.Size = new Size(0x161, 0x164);
            this.superTabControlPanel5.TabIndex = 1;
            this.superTabControlPanel5.TabItem = this.superTabItem5;
            this.CBDebug.BackColor = Color.Transparent;
            this.CBDebug.BackgroundStyle.CornerType = eCornerType.Square;
            this.CBDebug.Location = new Point(170, 0x148);
            this.CBDebug.Name = "CBDebug";
            this.CBDebug.Size = new Size(0x4d, 0x17);
            this.CBDebug.Style = eDotNetBarStyle.StyleManagerControlled;
            this.CBDebug.TabIndex = 0x4e;
            this.CBDebug.Text = "Log debug";
            this.CBDebug.CheckedChanged += new EventHandler(this.CBDebug_CheckedChanged);
            this.CBTopMost.BackColor = Color.Transparent;
            this.CBTopMost.BackgroundStyle.CornerType = eCornerType.Square;
            this.CBTopMost.Location = new Point(0xfd, 0x148);
            this.CBTopMost.Name = "CBTopMost";
            this.CBTopMost.Size = new Size(0x45, 0x17);
            this.CBTopMost.Style = eDotNetBarStyle.StyleManagerControlled;
            this.CBTopMost.TabIndex = 0x4b;
            this.CBTopMost.Text = "Top most";
            this.CBTopMost.CheckedChanged += new EventHandler(this.CBTopMostCheckedChanged);
            this.buttonX2.AccessibleRole = AccessibleRole.PushButton;
            this.buttonX2.ColorTable = eButtonColor.OrangeWithBackground;
            this.buttonX2.Location = new Point(0x59, 0x147);
            this.buttonX2.Name = "buttonX2";
            this.buttonX2.Size = new Size(0x30, 20);
            this.buttonX2.Style = eDotNetBarStyle.StyleManagerControlled;
            this.buttonX2.TabIndex = 0x4d;
            this.buttonX2.Text = "buttonX2";
            this.buttonX2.Visible = false;
            this.buttonX2.Click += new EventHandler(this.buttonX2_Click);
            this.MainGPPlayer.BackColor = Color.Transparent;
            this.MainGPPlayer.CanvasColor = SystemColors.Control;
            this.MainGPPlayer.ColorSchemeStyle = eDotNetBarStyle.Office2007;
            this.MainGPPlayer.Controls.Add(this.labelX1);
            this.MainGPPlayer.Controls.Add(this.MainPBPlayerHP);
            this.MainGPPlayer.Controls.Add(this.buttonX1);
            this.MainGPPlayer.Controls.Add(this.MainLBPlayerPower);
            this.MainGPPlayer.Controls.Add(this.LBVersion);
            this.MainGPPlayer.Controls.Add(this.MainPBPlayerPower);
            this.MainGPPlayer.Controls.Add(this.MainLBPowerType);
            this.MainGPPlayer.Controls.Add(this.MainLBPlayerXPH);
            this.MainGPPlayer.Controls.Add(this.MainLBPlayerTTL);
            this.MainGPPlayer.Controls.Add(this.MainLBPlayerXP);
            this.MainGPPlayer.Controls.Add(this.MainLBPlayerHP);
            this.MainGPPlayer.Controls.Add(this.MainPBPlayerXP);
            this.MainGPPlayer.Controls.Add(this.labelX36);
            this.MainGPPlayer.Controls.Add(this.labelX5);
            this.MainGPPlayer.DisabledBackColor = Color.Empty;
            this.MainGPPlayer.Location = new Point(3, 10);
            this.MainGPPlayer.Name = "MainGPPlayer";
            this.MainGPPlayer.Size = new Size(0x13f, 0x65);
            this.MainGPPlayer.Style.BackColor2SchemePart = eColorSchemePart.PanelBackground2;
            this.MainGPPlayer.Style.BackColorGradientAngle = 90;
            this.MainGPPlayer.Style.BackColorSchemePart = eColorSchemePart.PanelBackground;
            this.MainGPPlayer.Style.BorderBottom = eStyleBorderType.Solid;
            this.MainGPPlayer.Style.BorderBottomWidth = 1;
            this.MainGPPlayer.Style.BorderColorSchemePart = eColorSchemePart.PanelBorder;
            this.MainGPPlayer.Style.BorderLeft = eStyleBorderType.Solid;
            this.MainGPPlayer.Style.BorderLeftWidth = 1;
            this.MainGPPlayer.Style.BorderRight = eStyleBorderType.Solid;
            this.MainGPPlayer.Style.BorderRightWidth = 1;
            this.MainGPPlayer.Style.BorderTop = eStyleBorderType.Solid;
            this.MainGPPlayer.Style.BorderTopWidth = 1;
            this.MainGPPlayer.Style.CornerDiameter = 4;
            this.MainGPPlayer.Style.CornerType = eCornerType.Rounded;
            this.MainGPPlayer.Style.TextAlignment = eStyleTextAlignment.Center;
            this.MainGPPlayer.Style.TextColorSchemePart = eColorSchemePart.PanelText;
            this.MainGPPlayer.Style.TextLineAlignment = eStyleTextAlignment.Near;
            this.MainGPPlayer.StyleMouseDown.CornerType = eCornerType.Square;
            this.MainGPPlayer.StyleMouseOver.CornerType = eCornerType.Square;
            this.MainGPPlayer.TabIndex = 0x49;
            this.MainGPPlayer.Text = "-";
            this.labelX1.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX1.Location = new Point(3, 60);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new Size(0x7e, 0x17);
            this.labelX1.TabIndex = 0x3f;
            this.MainPBPlayerHP.BackgroundStyle.CornerType = eCornerType.Square;
            this.MainPBPlayerHP.Location = new Point(0x48, 2);
            this.MainPBPlayerHP.Name = "MainPBPlayerHP";
            this.MainPBPlayerHP.Size = new Size(0x6b, 13);
            this.MainPBPlayerHP.TabIndex = 0x3e;
            this.buttonX1.AccessibleRole = AccessibleRole.PushButton;
            this.buttonX1.ColorTable = eButtonColor.OrangeWithBackground;
            this.buttonX1.Location = new Point(0xe8, 0x1b);
            this.buttonX1.Name = "buttonX1";
            this.buttonX1.Size = new Size(0x4b, 0x17);
            this.buttonX1.Style = eDotNetBarStyle.StyleManagerControlled;
            this.buttonX1.TabIndex = 0x4c;
            this.buttonX1.Text = "buttonX1";
            this.buttonX1.Visible = false;
            this.buttonX1.Click += new EventHandler(this.buttonX1_Click);
            this.MainLBPlayerPower.BackgroundStyle.CornerType = eCornerType.Square;
            this.MainLBPlayerPower.Location = new Point(0xb5, 20);
            this.MainLBPlayerPower.Name = "MainLBPlayerPower";
            this.MainLBPlayerPower.Size = new Size(0x1f, 0x17);
            this.MainLBPlayerPower.TabIndex = 0x3d;
            this.MainLBPlayerPower.Text = "0%";
            this.LBVersion.BackgroundStyle.CornerType = eCornerType.Square;
            this.LBVersion.Location = new Point(130, 60);
            this.LBVersion.Name = "LBVersion";
            this.LBVersion.Size = new Size(200, 0x17);
            this.LBVersion.TabIndex = 1;
            this.LBVersion.Text = "LazyBot Evolution V0.1";
            this.MainPBPlayerPower.BackgroundStyle.CornerType = eCornerType.Square;
            this.MainPBPlayerPower.Location = new Point(0x48, 0x19);
            this.MainPBPlayerPower.Name = "MainPBPlayerPower";
            this.MainPBPlayerPower.Size = new Size(0x6b, 13);
            this.MainPBPlayerPower.TabIndex = 60;
            this.MainLBPowerType.BackgroundStyle.CornerType = eCornerType.Square;
            this.MainLBPowerType.Location = new Point(4, 0x13);
            this.MainLBPowerType.Name = "MainLBPowerType";
            this.MainLBPowerType.Size = new Size(0x4b, 0x17);
            this.MainLBPowerType.TabIndex = 0x3b;
            this.MainLBPowerType.Text = "Power:";
            this.MainLBPlayerXPH.BackgroundStyle.CornerType = eCornerType.Square;
            this.MainLBPlayerXPH.Location = new Point(0x8d, 60);
            this.MainLBPlayerXPH.Name = "MainLBPlayerXPH";
            this.MainLBPlayerXPH.Size = new Size(0x43, 0x17);
            this.MainLBPlayerXPH.TabIndex = 0x38;
            this.MainLBPlayerTTL.BackgroundStyle.CornerType = eCornerType.Square;
            this.MainLBPlayerTTL.Location = new Point(0x21, 0x3a);
            this.MainLBPlayerTTL.Name = "MainLBPlayerTTL";
            this.MainLBPlayerTTL.Size = new Size(0x52, 0x17);
            this.MainLBPlayerTTL.TabIndex = 0x36;
            this.MainLBPlayerXP.BackgroundStyle.CornerType = eCornerType.Square;
            this.MainLBPlayerXP.Location = new Point(0xb5, 40);
            this.MainLBPlayerXP.Name = "MainLBPlayerXP";
            this.MainLBPlayerXP.Size = new Size(0x1f, 0x17);
            this.MainLBPlayerXP.TabIndex = 0x34;
            this.MainLBPlayerXP.Text = "0%";
            this.MainLBPlayerHP.BackgroundStyle.CornerType = eCornerType.Square;
            this.MainLBPlayerHP.Location = new Point(0xb6, -3);
            this.MainLBPlayerHP.Name = "MainLBPlayerHP";
            this.MainLBPlayerHP.Size = new Size(0x1f, 0x17);
            this.MainLBPlayerHP.TabIndex = 0x33;
            this.MainLBPlayerHP.Text = "0%";
            this.MainPBPlayerXP.BackgroundStyle.CornerType = eCornerType.Square;
            this.MainPBPlayerXP.Location = new Point(0x48, 0x2d);
            this.MainPBPlayerXP.Name = "MainPBPlayerXP";
            this.MainPBPlayerXP.Size = new Size(0x6b, 13);
            this.MainPBPlayerXP.TabIndex = 50;
            this.labelX36.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX36.Location = new Point(6, 40);
            this.labelX36.Name = "labelX36";
            this.labelX36.Size = new Size(0x15, 0x17);
            this.labelX36.TabIndex = 0x30;
            this.labelX36.Text = "XP:";
            this.labelX5.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX5.Location = new Point(6, -3);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new Size(0x15, 0x17);
            this.labelX5.TabIndex = 0x2f;
            this.labelX5.Text = "HP:";
            this.StartStopEngine.AccessibleRole = AccessibleRole.PushButton;
            this.StartStopEngine.ColorTable = eButtonColor.OrangeWithBackground;
            this.StartStopEngine.Enabled = false;
            this.StartStopEngine.Location = new Point(3, 0x146);
            this.StartStopEngine.Name = "StartStopEngine";
            this.StartStopEngine.Size = new Size(0x4b, 0x17);
            this.StartStopEngine.Style = eDotNetBarStyle.StyleManagerControlled;
            this.StartStopEngine.TabIndex = 0x4a;
            this.StartStopEngine.Text = "Start";
            this.StartStopEngine.Click += new EventHandler(this.StartEngineClick);
            this.groupPanel3.BackColor = Color.Transparent;
            this.groupPanel3.CanvasColor = SystemColors.Control;
            this.groupPanel3.ColorSchemeStyle = eDotNetBarStyle.Office2007;
            this.groupPanel3.Controls.Add(this.LogWin);
            this.groupPanel3.DisabledBackColor = Color.Empty;
            this.groupPanel3.Location = new Point(3, 0x75);
            this.groupPanel3.Name = "groupPanel3";
            this.groupPanel3.Size = new Size(0x13f, 0xcb);
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
            this.groupPanel3.Style.CornerDiameter = 4;
            this.groupPanel3.Style.CornerType = eCornerType.Rounded;
            this.groupPanel3.Style.TextAlignment = eStyleTextAlignment.Center;
            this.groupPanel3.Style.TextColorSchemePart = eColorSchemePart.PanelText;
            this.groupPanel3.Style.TextLineAlignment = eStyleTextAlignment.Near;
            this.groupPanel3.StyleMouseDown.CornerType = eCornerType.Square;
            this.groupPanel3.StyleMouseOver.CornerType = eCornerType.Square;
            this.groupPanel3.TabIndex = 0x48;
            this.LogWin.BackColor = Color.FromArgb(0xdf, 0xea, 0xf6);
            this.LogWin.BorderStyle = BorderStyle.None;
            this.LogWin.Dock = DockStyle.Fill;
            this.LogWin.Location = new Point(0, 0);
            this.LogWin.Name = "LogWin";
            this.LogWin.ReadOnly = true;
            this.LogWin.Size = new Size(0x139, 0xc5);
            this.LogWin.TabIndex = 6;
            this.LogWin.Text = "";
            this.superTabItem5.AttachedControl = this.superTabControlPanel5;
            this.superTabItem5.GlobalItem = false;
            this.superTabItem5.Name = "superTabItem5";
            this.superTabItem5.Text = "Main";
            this.superTabControlPanel31.Controls.Add(this.groupPanel10);
            this.superTabControlPanel31.Controls.Add(this.groupPanel5);
            this.superTabControlPanel31.Dock = DockStyle.Fill;
            this.superTabControlPanel31.Location = new Point(0, 0);
            this.superTabControlPanel31.Name = "superTabControlPanel31";
            this.superTabControlPanel31.Size = new Size(0x145, 0x163);
            this.superTabControlPanel31.TabIndex = 2;
            this.superTabControlPanel31.TabItem = this.superTabItem31;
            this.groupPanel10.BackColor = Color.Transparent;
            this.groupPanel10.CanvasColor = SystemColors.Control;
            this.groupPanel10.ColorSchemeStyle = eDotNetBarStyle.Office2007;
            this.groupPanel10.Controls.Add(this.StopnAttackTargetDummy);
            this.groupPanel10.Controls.Add(this.BtnAttackTargetDummy);
            this.groupPanel10.Controls.Add(this.DebugBtnLogTargetBuff);
            this.groupPanel10.Controls.Add(this.DebugBtnLogOwnBuff);
            this.groupPanel10.Controls.Add(this.DebugBtnShouldRepair);
            this.groupPanel10.Controls.Add(this.DebugBtnClassRecompile);
            this.groupPanel10.DisabledBackColor = Color.Empty;
            this.groupPanel10.Location = new Point(3, 0x54);
            this.groupPanel10.Name = "groupPanel10";
            this.groupPanel10.Size = new Size(0x132, 0x8b);
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
            this.groupPanel10.Style.CornerDiameter = 4;
            this.groupPanel10.Style.CornerType = eCornerType.Rounded;
            this.groupPanel10.Style.TextAlignment = eStyleTextAlignment.Center;
            this.groupPanel10.Style.TextColorSchemePart = eColorSchemePart.PanelText;
            this.groupPanel10.Style.TextLineAlignment = eStyleTextAlignment.Near;
            this.groupPanel10.StyleMouseDown.CornerType = eCornerType.Square;
            this.groupPanel10.StyleMouseOver.CornerType = eCornerType.Square;
            this.groupPanel10.TabIndex = 0x4a;
            this.groupPanel10.Text = "Debugging";
            this.StopnAttackTargetDummy.AccessibleRole = AccessibleRole.PushButton;
            this.StopnAttackTargetDummy.ColorTable = eButtonColor.OrangeWithBackground;
            this.StopnAttackTargetDummy.Location = new Point(0x8a, 0x56);
            this.StopnAttackTargetDummy.Name = "StopnAttackTargetDummy";
            this.StopnAttackTargetDummy.Size = new Size(0x8f, 0x17);
            this.StopnAttackTargetDummy.Style = eDotNetBarStyle.StyleManagerControlled;
            this.StopnAttackTargetDummy.TabIndex = 0x40;
            this.StopnAttackTargetDummy.Text = "Stop attack training dummy";
            this.StopnAttackTargetDummy.Click += new EventHandler(this.StopnAttackTargetDummyClick);
            this.BtnAttackTargetDummy.AccessibleRole = AccessibleRole.PushButton;
            this.BtnAttackTargetDummy.ColorTable = eButtonColor.OrangeWithBackground;
            this.BtnAttackTargetDummy.Location = new Point(0x8a, 0x3d);
            this.BtnAttackTargetDummy.Name = "BtnAttackTargetDummy";
            this.BtnAttackTargetDummy.Size = new Size(0x8e, 0x17);
            this.BtnAttackTargetDummy.Style = eDotNetBarStyle.StyleManagerControlled;
            this.BtnAttackTargetDummy.TabIndex = 0x3f;
            this.BtnAttackTargetDummy.Text = "Attack training dummy";
            this.BtnAttackTargetDummy.Click += new EventHandler(this.BtnAttackTargetDummyClick);
            this.DebugBtnLogTargetBuff.AccessibleRole = AccessibleRole.PushButton;
            this.DebugBtnLogTargetBuff.ColorTable = eButtonColor.OrangeWithBackground;
            this.DebugBtnLogTargetBuff.Location = new Point(0x11, 0x20);
            this.DebugBtnLogTargetBuff.Name = "DebugBtnLogTargetBuff";
            this.DebugBtnLogTargetBuff.Size = new Size(0x73, 0x17);
            this.DebugBtnLogTargetBuff.Style = eDotNetBarStyle.StyleManagerControlled;
            this.DebugBtnLogTargetBuff.TabIndex = 0x3d;
            this.DebugBtnLogTargetBuff.Text = "Log target buffs";
            this.DebugBtnLogTargetBuff.Click += new EventHandler(this.DebugBtnLogTargetBuffClick);
            this.DebugBtnLogOwnBuff.AccessibleRole = AccessibleRole.PushButton;
            this.DebugBtnLogOwnBuff.ColorTable = eButtonColor.OrangeWithBackground;
            this.DebugBtnLogOwnBuff.Location = new Point(0x8a, 0x20);
            this.DebugBtnLogOwnBuff.Name = "DebugBtnLogOwnBuff";
            this.DebugBtnLogOwnBuff.Size = new Size(0x8e, 0x17);
            this.DebugBtnLogOwnBuff.Style = eDotNetBarStyle.StyleManagerControlled;
            this.DebugBtnLogOwnBuff.TabIndex = 60;
            this.DebugBtnLogOwnBuff.Text = "Log own buffs";
            this.DebugBtnLogOwnBuff.Click += new EventHandler(this.DebugBtnLogOwnBuffClick);
            this.DebugBtnShouldRepair.AccessibleRole = AccessibleRole.PushButton;
            this.DebugBtnShouldRepair.ColorTable = eButtonColor.OrangeWithBackground;
            this.DebugBtnShouldRepair.Location = new Point(0x8a, 3);
            this.DebugBtnShouldRepair.Name = "DebugBtnShouldRepair";
            this.DebugBtnShouldRepair.Size = new Size(0x8e, 0x17);
            this.DebugBtnShouldRepair.Style = eDotNetBarStyle.StyleManagerControlled;
            this.DebugBtnShouldRepair.TabIndex = 0x3b;
            this.DebugBtnShouldRepair.Text = "Check Should repair";
            this.DebugBtnShouldRepair.Click += new EventHandler(this.DebugBtnShouldRepairClick);
            this.DebugBtnClassRecompile.AccessibleRole = AccessibleRole.Graphic;
            this.DebugBtnClassRecompile.ColorTable = eButtonColor.OrangeWithBackground;
            this.DebugBtnClassRecompile.Location = new Point(0x11, 3);
            this.DebugBtnClassRecompile.Name = "DebugBtnClassRecompile";
            this.DebugBtnClassRecompile.Size = new Size(0x73, 0x17);
            this.DebugBtnClassRecompile.Style = eDotNetBarStyle.StyleManagerControlled;
            this.DebugBtnClassRecompile.TabIndex = 0x3a;
            this.DebugBtnClassRecompile.Text = "Recompile class";
            this.DebugBtnClassRecompile.Click += new EventHandler(this.DebugBtnClassRecompileClick);
            this.groupPanel5.BackColor = Color.Transparent;
            this.groupPanel5.CanvasColor = SystemColors.Control;
            this.groupPanel5.ColorSchemeStyle = eDotNetBarStyle.Office2007;
            this.groupPanel5.Controls.Add(this.MainBtnRefreshProcess);
            this.groupPanel5.Controls.Add(this.MainBtnSelectProcess);
            this.groupPanel5.Controls.Add(this.MainComProcessSelection);
            this.groupPanel5.DisabledBackColor = Color.Empty;
            this.groupPanel5.Location = new Point(3, 3);
            this.groupPanel5.Name = "groupPanel5";
            this.groupPanel5.Size = new Size(0x132, 0x4c);
            this.groupPanel5.Style.BackColor2SchemePart = eColorSchemePart.PanelBackground2;
            this.groupPanel5.Style.BackColorGradientAngle = 90;
            this.groupPanel5.Style.BackColorSchemePart = eColorSchemePart.PanelBackground;
            this.groupPanel5.Style.BorderBottom = eStyleBorderType.Solid;
            this.groupPanel5.Style.BorderBottomWidth = 1;
            this.groupPanel5.Style.BorderColorSchemePart = eColorSchemePart.PanelBorder;
            this.groupPanel5.Style.BorderLeft = eStyleBorderType.Solid;
            this.groupPanel5.Style.BorderLeftWidth = 1;
            this.groupPanel5.Style.BorderRight = eStyleBorderType.Solid;
            this.groupPanel5.Style.BorderRightWidth = 1;
            this.groupPanel5.Style.BorderTop = eStyleBorderType.Solid;
            this.groupPanel5.Style.BorderTopWidth = 1;
            this.groupPanel5.Style.CornerDiameter = 4;
            this.groupPanel5.Style.CornerType = eCornerType.Rounded;
            this.groupPanel5.Style.TextAlignment = eStyleTextAlignment.Center;
            this.groupPanel5.Style.TextColorSchemePart = eColorSchemePart.PanelText;
            this.groupPanel5.Style.TextLineAlignment = eStyleTextAlignment.Near;
            this.groupPanel5.StyleMouseDown.CornerType = eCornerType.Square;
            this.groupPanel5.StyleMouseOver.CornerType = eCornerType.Square;
            this.groupPanel5.TabIndex = 0x49;
            this.groupPanel5.Text = "Attach to process";
            this.MainBtnRefreshProcess.AccessibleRole = AccessibleRole.PushButton;
            this.MainBtnRefreshProcess.ColorTable = eButtonColor.OrangeWithBackground;
            this.MainBtnRefreshProcess.Location = new Point(0xa4, 0x1c);
            this.MainBtnRefreshProcess.Name = "MainBtnRefreshProcess";
            this.MainBtnRefreshProcess.Size = new Size(0x6d, 0x17);
            this.MainBtnRefreshProcess.Style = eDotNetBarStyle.StyleManagerControlled;
            this.MainBtnRefreshProcess.TabIndex = 0x17;
            this.MainBtnRefreshProcess.Text = "Refresh";
            this.MainBtnRefreshProcess.Click += new EventHandler(this.MainBtnRefreshProcessClick);
            this.MainBtnSelectProcess.AccessibleRole = AccessibleRole.PushButton;
            this.MainBtnSelectProcess.ColorTable = eButtonColor.OrangeWithBackground;
            this.MainBtnSelectProcess.Location = new Point(0x19, 0x1c);
            this.MainBtnSelectProcess.Name = "MainBtnSelectProcess";
            this.MainBtnSelectProcess.Size = new Size(0x85, 0x17);
            this.MainBtnSelectProcess.Style = eDotNetBarStyle.StyleManagerControlled;
            this.MainBtnSelectProcess.TabIndex = 0x16;
            this.MainBtnSelectProcess.Text = "Attach";
            this.MainBtnSelectProcess.Click += new EventHandler(this.MainBtnSelectProcessClick);
            this.MainComProcessSelection.DisplayMember = "Text";
            this.MainComProcessSelection.DrawMode = DrawMode.OwnerDrawFixed;
            this.MainComProcessSelection.FormattingEnabled = true;
            this.MainComProcessSelection.ItemHeight = 14;
            this.MainComProcessSelection.Location = new Point(0x18, 4);
            this.MainComProcessSelection.Name = "MainComProcessSelection";
            this.MainComProcessSelection.Size = new Size(0xf9, 20);
            this.MainComProcessSelection.Style = eDotNetBarStyle.StyleManagerControlled;
            this.MainComProcessSelection.TabIndex = 0x15;
            this.superTabItem31.AttachedControl = this.superTabControlPanel31;
            this.superTabItem31.GlobalItem = false;
            this.superTabItem31.Name = "superTabItem31";
            this.superTabItem31.Text = "Debug";
            this.superTabControlPanel30.Controls.Add(this.groupPanel12);
            this.superTabControlPanel30.Controls.Add(this.groupPanel11);
            this.superTabControlPanel30.Controls.Add(this.labelX24);
            this.superTabControlPanel30.Controls.Add(this.ChatSendText);
            this.superTabControlPanel30.Controls.Add(this.labelX3);
            this.superTabControlPanel30.Controls.Add(this.ChatTBSendText);
            this.superTabControlPanel30.Dock = DockStyle.Fill;
            this.superTabControlPanel30.Location = new Point(0, 0);
            this.superTabControlPanel30.Name = "superTabControlPanel30";
            this.superTabControlPanel30.Size = new Size(0x145, 0x163);
            this.superTabControlPanel30.TabIndex = 0;
            this.superTabControlPanel30.TabItem = this.superTabItem30;
            this.groupPanel12.BackColor = Color.Transparent;
            this.groupPanel12.CanvasColor = SystemColors.Control;
            this.groupPanel12.ColorSchemeStyle = eDotNetBarStyle.Office2007;
            this.groupPanel12.Controls.Add(this.ChatAll);
            this.groupPanel12.DisabledBackColor = Color.Empty;
            this.groupPanel12.Location = new Point(3, 0);
            this.groupPanel12.Name = "groupPanel12";
            this.groupPanel12.Size = new Size(0x12f, 140);
            this.groupPanel12.Style.BackColor2SchemePart = eColorSchemePart.PanelBackground2;
            this.groupPanel12.Style.BackColorGradientAngle = 90;
            this.groupPanel12.Style.BackColorSchemePart = eColorSchemePart.PanelBackground;
            this.groupPanel12.Style.BorderBottom = eStyleBorderType.Solid;
            this.groupPanel12.Style.BorderBottomWidth = 1;
            this.groupPanel12.Style.BorderColorSchemePart = eColorSchemePart.PanelBorder;
            this.groupPanel12.Style.BorderLeft = eStyleBorderType.Solid;
            this.groupPanel12.Style.BorderLeftWidth = 1;
            this.groupPanel12.Style.BorderRight = eStyleBorderType.Solid;
            this.groupPanel12.Style.BorderRightWidth = 1;
            this.groupPanel12.Style.BorderTop = eStyleBorderType.Solid;
            this.groupPanel12.Style.BorderTopWidth = 1;
            this.groupPanel12.Style.CornerDiameter = 4;
            this.groupPanel12.Style.CornerType = eCornerType.Rounded;
            this.groupPanel12.Style.TextAlignment = eStyleTextAlignment.Center;
            this.groupPanel12.Style.TextColorSchemePart = eColorSchemePart.PanelText;
            this.groupPanel12.Style.TextLineAlignment = eStyleTextAlignment.Near;
            this.groupPanel12.StyleMouseDown.CornerType = eCornerType.Square;
            this.groupPanel12.StyleMouseOver.CornerType = eCornerType.Square;
            this.groupPanel12.TabIndex = 15;
            this.groupPanel12.Text = "All chat";
            this.ChatAll.BackColor = Color.FromArgb(0xdf, 0xea, 0xf6);
            this.ChatAll.BorderStyle = BorderStyle.None;
            this.ChatAll.Dock = DockStyle.Top;
            this.ChatAll.Location = new Point(0, 0);
            this.ChatAll.Name = "ChatAll";
            this.ChatAll.Size = new Size(0x129, 0x73);
            this.ChatAll.TabIndex = 0;
            this.ChatAll.Text = "";
            this.groupPanel11.CanvasColor = SystemColors.Control;
            this.groupPanel11.ColorSchemeStyle = eDotNetBarStyle.Office2007;
            this.groupPanel11.Controls.Add(this.ChatWhisper);
            this.groupPanel11.DisabledBackColor = Color.Empty;
            this.groupPanel11.Location = new Point(3, 0x8f);
            this.groupPanel11.Name = "groupPanel11";
            this.groupPanel11.Size = new Size(0x12f, 0x7f);
            this.groupPanel11.Style.BackColor2SchemePart = eColorSchemePart.PanelBackground2;
            this.groupPanel11.Style.BackColorGradientAngle = 90;
            this.groupPanel11.Style.BackColorSchemePart = eColorSchemePart.PanelBackground;
            this.groupPanel11.Style.BorderBottom = eStyleBorderType.Solid;
            this.groupPanel11.Style.BorderBottomWidth = 1;
            this.groupPanel11.Style.BorderColorSchemePart = eColorSchemePart.PanelBorder;
            this.groupPanel11.Style.BorderLeft = eStyleBorderType.Solid;
            this.groupPanel11.Style.BorderLeftWidth = 1;
            this.groupPanel11.Style.BorderRight = eStyleBorderType.Solid;
            this.groupPanel11.Style.BorderRightWidth = 1;
            this.groupPanel11.Style.BorderTop = eStyleBorderType.Solid;
            this.groupPanel11.Style.BorderTopWidth = 1;
            this.groupPanel11.Style.CornerDiameter = 4;
            this.groupPanel11.Style.CornerType = eCornerType.Rounded;
            this.groupPanel11.Style.TextAlignment = eStyleTextAlignment.Center;
            this.groupPanel11.Style.TextColorSchemePart = eColorSchemePart.PanelText;
            this.groupPanel11.Style.TextLineAlignment = eStyleTextAlignment.Near;
            this.groupPanel11.StyleMouseDown.CornerType = eCornerType.Square;
            this.groupPanel11.StyleMouseOver.CornerType = eCornerType.Square;
            this.groupPanel11.TabIndex = 14;
            this.groupPanel11.Text = "Whispers";
            this.ChatWhisper.BackColor = Color.FromArgb(0xdf, 0xea, 0xf6);
            this.ChatWhisper.BorderStyle = BorderStyle.None;
            this.ChatWhisper.Dock = DockStyle.Fill;
            this.ChatWhisper.Location = new Point(0, 0);
            this.ChatWhisper.Name = "ChatWhisper";
            this.ChatWhisper.Size = new Size(0x129, 0x6a);
            this.ChatWhisper.TabIndex = 1;
            this.ChatWhisper.Text = "";
            this.labelX24.BackColor = Color.Transparent;
            this.labelX24.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX24.Location = new Point(3, 0x147);
            this.labelX24.Name = "labelX24";
            this.labelX24.Size = new Size(0xe1, 0x1b);
            this.labelX24.TabIndex = 13;
            this.labelX24.Text = "The bot will send the text the next time it can.";
            this.ChatSendText.AccessibleRole = AccessibleRole.PushButton;
            this.ChatSendText.ColorTable = eButtonColor.OrangeWithBackground;
            this.ChatSendText.Location = new Point(0xea, 0x148);
            this.ChatSendText.Name = "ChatSendText";
            this.ChatSendText.Size = new Size(0x4b, 0x17);
            this.ChatSendText.Style = eDotNetBarStyle.StyleManagerControlled;
            this.ChatSendText.TabIndex = 12;
            this.ChatSendText.Text = "Send";
            this.ChatSendText.Click += new EventHandler(this.ChatSendTextClick);
            this.labelX3.BackColor = Color.Transparent;
            this.labelX3.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX3.Location = new Point(3, 0x114);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new Size(0x4b, 0x17);
            this.labelX3.TabIndex = 11;
            this.labelX3.Text = "Send text:";
            this.ChatTBSendText.Border.Class = "TextBoxBorder";
            this.ChatTBSendText.Border.CornerType = eCornerType.Square;
            this.ChatTBSendText.Location = new Point(3, 0x12e);
            this.ChatTBSendText.Name = "ChatTBSendText";
            this.ChatTBSendText.Size = new Size(0x132, 20);
            this.ChatTBSendText.TabIndex = 10;
            this.superTabItem30.AttachedControl = this.superTabControlPanel30;
            this.superTabItem30.GlobalItem = false;
            this.superTabItem30.Name = "superTabItem30";
            this.superTabItem30.Text = "Chat";
            this.GrindSelectCombat.DisplayMember = "Text";
            this.GrindSelectCombat.DrawMode = DrawMode.OwnerDrawFixed;
            this.GrindSelectCombat.DropDownStyle = ComboBoxStyle.DropDownList;
            this.GrindSelectCombat.FormattingEnabled = true;
            this.GrindSelectCombat.ItemHeight = 14;
            object[] objArray3 = new object[] { this.comboItem3 };
            this.GrindSelectCombat.Items.AddRange(objArray3);
            this.GrindSelectCombat.Location = new Point(0x13b, 0x68);
            this.GrindSelectCombat.Name = "GrindSelectCombat";
            this.GrindSelectCombat.Size = new Size(0xa8, 20);
            this.GrindSelectCombat.Style = eDotNetBarStyle.StyleManagerControlled;
            this.GrindSelectCombat.TabIndex = 0x4b;
            this.comboItem3.Text = "Behavior Engine";
            ToolStripItem[] toolStripItems = new ToolStripItem[] { this.StatsText };
            this.statusStrip1.Items.AddRange(toolStripItems);
            this.statusStrip1.Location = new Point(140, 0x180);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new Size(0x161, 0x16);
            this.statusStrip1.TabIndex = 0x49;
            this.statusStrip1.Text = "statusStrip1";
            this.StatsText.BackColor = Color.Transparent;
            this.StatsText.Name = "StatsText";
            this.StatsText.Size = new Size(0, 0x11);
            this.wizard1.CancelButtonText = "Cancel";
            this.wizard1.FinishButtonTabIndex = 3;
            this.wizard1.FooterStyle.CornerType = eCornerType.Square;
            this.wizard1.HeaderCaptionFont = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.wizard1.HeaderDescriptionFont = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.wizard1.HeaderDescriptionIndent = 0x10;
            this.wizard1.HeaderStyle.BackColor = Color.FromArgb(0xff, 0xff, 0xff);
            this.wizard1.HeaderStyle.BackColorGradientAngle = 90;
            this.wizard1.HeaderStyle.BorderBottom = eStyleBorderType.Etched;
            this.wizard1.HeaderStyle.BorderBottomWidth = 1;
            this.wizard1.HeaderStyle.BorderColor = SystemColors.Control;
            this.wizard1.HeaderStyle.BorderLeftWidth = 1;
            this.wizard1.HeaderStyle.BorderRightWidth = 1;
            this.wizard1.HeaderStyle.BorderTopWidth = 1;
            this.wizard1.HeaderStyle.CornerType = eCornerType.Square;
            this.wizard1.HeaderStyle.TextAlignment = eStyleTextAlignment.Center;
            this.wizard1.HeaderStyle.TextColorSchemePart = eColorSchemePart.PanelText;
            this.wizard1.Location = new Point(0, 0);
            this.wizard1.Name = "wizard1";
            this.wizard1.Size = new Size(0x224, 0x177);
            this.wizard1.TabIndex = 0;
            this.wizard2.CancelButtonText = "Cancel";
            this.wizard2.FinishButtonTabIndex = 3;
            this.wizard2.FooterStyle.CornerType = eCornerType.Square;
            this.wizard2.HeaderCaptionFont = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.wizard2.HeaderDescriptionFont = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.wizard2.HeaderDescriptionIndent = 0x10;
            this.wizard2.HeaderStyle.BackColor = Color.FromArgb(0xff, 0xff, 0xff);
            this.wizard2.HeaderStyle.BackColorGradientAngle = 90;
            this.wizard2.HeaderStyle.BorderBottom = eStyleBorderType.Etched;
            this.wizard2.HeaderStyle.BorderBottomWidth = 1;
            this.wizard2.HeaderStyle.BorderColor = SystemColors.Control;
            this.wizard2.HeaderStyle.BorderLeftWidth = 1;
            this.wizard2.HeaderStyle.BorderRightWidth = 1;
            this.wizard2.HeaderStyle.BorderTopWidth = 1;
            this.wizard2.HeaderStyle.CornerType = eCornerType.Square;
            this.wizard2.HeaderStyle.TextAlignment = eStyleTextAlignment.Center;
            this.wizard2.HeaderStyle.TextColorSchemePart = eColorSchemePart.PanelText;
            this.wizard2.Location = new Point(0, 0);
            this.wizard2.Name = "wizard2";
            this.wizard2.Size = new Size(0x224, 0x177);
            this.wizard2.TabIndex = 0;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.FromArgb(0xdf, 0xea, 0xf6);
            base.ClientSize = new Size(0x1ed, 0x196);
            base.Controls.Add(this.statusStrip1);
            base.Controls.Add(this.superTabControl5);
            base.Controls.Add(this.expandableSplitter1);
            base.Controls.Add(this.ControlSettings);
            this.DoubleBuffered = true;
            this.EnableGlass = false;
            base.HelpButton = true;
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.MaximizeBox = false;
            base.Name = "Main";
            base.FormClosing += new FormClosingEventHandler(this.MainFormClosing);
            base.Load += new EventHandler(this.MainLoad);
            this.ControlSettings.ResumeLayout(false);
            ((ISupportInitialize) this.superTabControl5).EndInit();
            this.superTabControl5.ResumeLayout(false);
            this.superTabControlPanel5.ResumeLayout(false);
            this.MainGPPlayer.ResumeLayout(false);
            this.groupPanel3.ResumeLayout(false);
            this.superTabControlPanel31.ResumeLayout(false);
            this.groupPanel10.ResumeLayout(false);
            this.groupPanel5.ResumeLayout(false);
            this.superTabControlPanel30.ResumeLayout(false);
            this.groupPanel12.ResumeLayout(false);
            this.groupPanel11.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void KillTheDummy()
        {
            try
            {
                if (LazyLib.Wow.ObjectManager.MyPlayer.IsValid)
                {
                    if (!CombatEngine.StartOk)
                    {
                        Logging.Write(LogType.Warning, "CustomClass returned false on StartOk not starting", new object[0]);
                    }
                    else
                    {
                        CombatEngine.BotStarted();
                        CombatHandler.StartCombat(LazyLib.Wow.ObjectManager.MyPlayer.Target);
                    }
                }
            }
            catch
            {
            }
        }

        private void LazySettingsClick(object sender, EventArgs e)
        {
            LazyForms.SetupForm.ShowDialog();
            LazyForms.SetupForm = new Setup();
            this.LoadPluginButtons();
        }

        public void LicenseOk()
        {
            this.UpdateExpandableSplitter(this.expandableSplitter1, true, true);
            this.EnableButton(this.StartStopEngine);
        }

        private void LoadCustomClasses()
        {
            ClassCompiler.RecompileAll();
            this.SelectCombat.Items.Clear();
            foreach (KeyValuePair<string, LazyLib.Combat.CombatEngine> pair in ClassCompiler.Assemblys)
            {
                CustomClass item = new CustomClass(pair.Key, pair.Value.Name);
                this.SelectCombat.Items.Add(item);
            }
        }

        private void LoadEngines()
        {
            EngineCompiler.RecompileAll();
            this.SelectEngine.Items.Clear();
            foreach (KeyValuePair<string, ILazyEngine> pair in EngineCompiler.Assemblys)
            {
                CustomEngine item = new CustomEngine(pair.Key, pair.Value.Name);
                this.SelectEngine.Items.Add(item);
            }
        }

        public void LoadPluginButtons()
        {
            foreach (ButtonItem item in this._buttons)
            {
                this.ControlSettings.Items.Remove(item);
            }
            this._buttons.Clear();
            foreach (string str in PluginCompiler.LoadedPlugins)
            {
                ButtonItem item2 = new ButtonItem(PluginCompiler.Assemblys[str].GetName(), PluginCompiler.Assemblys[str].GetName()) {
                    Tag = str
                };
                item2.Click += new EventHandler(this.ShowPluginSettings);
                this._buttons.Add(item2);
                this.AddToControlSettings(item2);
            }
        }

        private void Logging_OnDebug(string message, LogType logType)
        {
            Color firebrick;
            switch (logType)
            {
                case LogType.Warning:
                    firebrick = Color.Firebrick;
                    break;

                case LogType.Error:
                    firebrick = Color.Red;
                    break;

                case LogType.Info:
                    firebrick = Color.BlueViolet;
                    break;

                case LogType.Good:
                    firebrick = Color.Green;
                    break;

                default:
                    firebrick = Color.Black;
                    break;
            }
            AppendMessage(this.LogWin, message, firebrick);
        }

        private void Logging_OnWrite(string message, LogType logType)
        {
            Color firebrick;
            switch (logType)
            {
                case LogType.Warning:
                    firebrick = Color.Firebrick;
                    break;

                case LogType.Error:
                    firebrick = Color.Red;
                    break;

                case LogType.Info:
                    firebrick = Color.BlueViolet;
                    break;

                case LogType.Good:
                    firebrick = Color.Green;
                    break;

                default:
                    firebrick = Color.Black;
                    break;
            }
            AppendMessage(this.LogWin, message, firebrick);
        }

        private void LogOut(object sender, NotifyEventNoAttach e)
        {
            if (Engine.Running)
            {
                this.StopBotting(false);
            }
        }

        private void MainBtnRefreshProcessClick(object sender, EventArgs e)
        {
            this.MainComProcessSelection.Items.Clear();
            this.MainComProcessSelection.Update();
            this._wowProc = Process.GetProcessesByName("Wow");
            foreach (Process process in this._wowProc)
            {
                this.MainComProcessSelection.Items.Add(process.MainWindowTitle + "- " + process.Id);
            }
            if (this.MainComProcessSelection.Items.Count == 0)
            {
                this.MainComProcessSelection.Items.Add("No game");
            }
            this.MainComProcessSelection.SelectedIndex = 0;
        }

        private void MainBtnSelectProcessClick(object sender, EventArgs e)
        {
            if ((this.MainComProcessSelection.SelectedItem != null) && ((this.MainComProcessSelection.SelectedText != "No game") && (this.MainComProcessSelection.SelectedItem.ToString() != "No game")))
            {
                LazyLib.Wow.ObjectManager.Initialize(this._wowProc[this.MainComProcessSelection.SelectedIndex].Id);
                Hook.DoHook();
            }
        }

        private void MainFormClosing(object sender, FormClosingEventArgs e)
        {
            if (Engine.Running)
            {
                this.StopBotting(true);
            }
            GeomertrySettings.MainGeometry = Geometry.GeometryToString(this);
            GeomertrySettings.Save();
            this.ReleaseHotKeys();
            DoLoad.Close();
        }

        private void MainLoad(object sender, EventArgs e)
        {
            this.LBVersion.Text = $"{"Lazybot De-Evolution r.29 For 3.3.5a"}";
            this.expandableSplitter1.Expanded = false;
            DoLoad.Load();
            this.LoadCustomClasses();
            this.LoadEngines();
            this.SelectEngine.SelectedIndex = this.SelectEngine.FindStringExact(LazySettings.SelectedEngine, -1);
            this.SelectCombat.SelectedIndex = this.SelectEngine.FindStringExact(LazySettings.SelectedCombat, -1);
            if (this.SelectEngine.SelectedIndex == -1)
            {
                this.SelectEngine.SelectedIndex = 0;
            }
            if (this.SelectCombat.SelectedIndex == -1)
            {
                this.SelectCombat.SelectedIndex = 0;
            }
            PluginCompiler.RecompileAll();
            PluginCompiler.StartSavedPlugins();
            this.LoadPluginButtons();
            this.RegisterHotKeys();
            Engine.StateChange += new EventHandler<Engine.NotifyStateChanged>(this.UpdateStateChange);
            LazyLib.Wow.ObjectManager.NoAttach += new EventHandler<NotifyEventNoAttach>(this.LogOut);
        }

        private void OpenRadar()
        {
            if ((this._radar != null) && !this._radar.IsDisposed)
            {
                this._radar.Close();
            }
            this._radar = new RadarForm();
            foreach (IDrawItem item in EngineHandler.GetRadarDraw())
            {
                this._radar.AddDrawItem(item);
            }
            foreach (IMouseClick click in EngineHandler.GetRadarClick())
            {
                this._radar.AddMonitorMouseClick(click);
            }
            this._radar.Show();
        }

        private void OpenRotator_Click(object sender, EventArgs e)
        {
            RotatorForm form = new RotatorForm();
            form.Show();
            this.OpenRotator.Enabled = false;
            form.Closed += new EventHandler(this.RotatorClosed);
        }

        private void PauseBot()
        {
            Engine.Pause();
        }

        public static void RandomResponse(string RespondTo)
        {
            Logging.Debug("Responding to: " + RespondTo, new object[0]);
            Random random = new Random();
            int num = random.Next(1, 11);
            while (num == _prev_dice)
            {
                Logging.Debug("Dice: " + num, new object[0]);
                num = random.Next(1, 11);
            }
            switch (num)
            {
                case 1:
                    chatcommand = true;
                    _prev_dice = num;
                    ChatQueu.AddChat("/s ");
                    ChatQueu.AddChat("/w " + RespondTo + " People who think they know everything are a great annoyance to those of us who do.");
                    ChatQueu.AddChat("/s ");
                    Thread.Sleep(100);
                    chatcommand = false;
                    return;

                case 2:
                    chatcommand = true;
                    _prev_dice = num;
                    ChatQueu.AddChat("/s ");
                    ChatQueu.AddChat("/w " + RespondTo + " Do not take life too seriously. You will never get out of it alive.");
                    ChatQueu.AddChat("/s ");
                    Thread.Sleep(100);
                    chatcommand = false;
                    return;

                case 3:
                    chatcommand = true;
                    _prev_dice = num;
                    ChatQueu.AddChat("/s ");
                    ChatQueu.AddChat("/w " + RespondTo + " A woman's mind is cleaner than a man's: She changes it more often.");
                    ChatQueu.AddChat("/s ");
                    Thread.Sleep(100);
                    chatcommand = false;
                    return;

                case 4:
                    chatcommand = true;
                    _prev_dice = num;
                    ChatQueu.AddChat("/s ");
                    ChatQueu.AddChat("/w " + RespondTo + " I believe that if life gives you lemons, you should make lemonade... And try to find somebody whose life has given them vodka, and have a party.");
                    ChatQueu.AddChat("/s ");
                    Thread.Sleep(100);
                    chatcommand = false;
                    return;

                case 5:
                    chatcommand = true;
                    _prev_dice = num;
                    ChatQueu.AddChat("/s ");
                    ChatQueu.AddChat("/w " + RespondTo + " One advantage of talking to yourself is that you know at least somebody's listening.");
                    ChatQueu.AddChat("/s ");
                    Thread.Sleep(100);
                    chatcommand = false;
                    return;

                case 6:
                    chatcommand = true;
                    _prev_dice = num;
                    ChatQueu.AddChat("/s ");
                    ChatQueu.AddChat("/w " + RespondTo + " Do not worry about avoiding temptation. As you grow older it will avoid you.");
                    ChatQueu.AddChat("/s ");
                    Thread.Sleep(100);
                    chatcommand = false;
                    return;

                case 7:
                    chatcommand = true;
                    _prev_dice = num;
                    ChatQueu.AddChat("/s ");
                    ChatQueu.AddChat("/w " + RespondTo + " I hate housework! You make the beds, you do the dishes and six months later you have to start all over again.");
                    ChatQueu.AddChat("/s ");
                    Thread.Sleep(100);
                    chatcommand = false;
                    return;

                case 8:
                    chatcommand = true;
                    _prev_dice = num;
                    ChatQueu.AddChat("/s ");
                    ChatQueu.AddChat("/w " + RespondTo + " If you live to be one hundred, you've got it made. Very few people die past that age.");
                    ChatQueu.AddChat("/s ");
                    Thread.Sleep(100);
                    chatcommand = false;
                    return;

                case 9:
                    chatcommand = true;
                    _prev_dice = num;
                    ChatQueu.AddChat("/s ");
                    ChatQueu.AddChat("/w " + RespondTo + " Well, if I called the wrong number, why did you answer the phone?");
                    ChatQueu.AddChat("/s ");
                    Thread.Sleep(100);
                    chatcommand = false;
                    return;

                case 10:
                    chatcommand = true;
                    _prev_dice = num;
                    ChatQueu.AddChat("/s ");
                    ChatQueu.AddChat("/w " + RespondTo + " Frisbeetarianism is the belief that when you die, your soul goes up on the roof and gets stuck.");
                    ChatQueu.AddChat("/s ");
                    Thread.Sleep(100);
                    chatcommand = false;
                    return;
            }
        }

        private void RegisterHotKeys()
        {
            HandledEventHandler handler = null;
            HandledEventHandler handler2 = null;
            if (LazySettings.SetupUseHotkeys && !this._hotKeysLoaded)
            {
                this._f10 = new Hotkey();
                this._f10.KeyCode = Keys.F10;
                this._f10.Windows = false;
                if (handler == null)
                {
                    handler = (param0, param1) => this.PauseBot();
                }
                this._f10.Pressed += handler;
                try
                {
                    if (!this._f10.GetCanRegister(this))
                    {
                        Logging.Write("Cannot register F10 as hotkey", new object[0]);
                    }
                    else
                    {
                        this._f10.Register(this);
                    }
                }
                catch
                {
                    Logging.Write("Cannot register F10 as hotkey", new object[0]);
                }
                this._f9 = new Hotkey();
                this._f9.KeyCode = Keys.F9;
                this._f9.Windows = false;
                if (handler2 == null)
                {
                    handler2 = (param0, param1) => this.StartStopBotting();
                }
                this._f9.Pressed += handler2;
                try
                {
                    if (!this._f9.GetCanRegister(this))
                    {
                        Logging.Write("Cannot register F9 as hotkey", new object[0]);
                    }
                    else
                    {
                        this._f9.Register(this);
                    }
                }
                catch
                {
                    Logging.Write("Cannot register F9 as hotkey", new object[0]);
                }
                this._hotKeysLoaded = true;
            }
        }

        private void ReleaseHotKeys()
        {
            if (LazySettings.SetupUseHotkeys && this._hotKeysLoaded)
            {
                if (this._f10.Registered)
                {
                    this._f10.Unregister();
                }
                if (this._f9.Registered)
                {
                    this._f9.Unregister();
                }
            }
        }

        private void RotatorClosed(object sender, EventArgs e)
        {
            this.OpenRotator.Enabled = true;
        }

        private void SelectCombatSelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.SelectCombat.SelectedIndex != -1)
            {
                LazySettings.SelectedCombat = this.SelectCombat.Text;
                LazySettings.SaveSettings();
                CustomClass selectedItem = (CustomClass) this.SelectCombat.SelectedItem;
                CombatEngine = ClassCompiler.Assemblys[selectedItem.AssemblyName];
            }
        }

        private void SelectEngineSelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.SelectEngine.SelectedIndex != -1)
            {
                CustomEngine selectedItem = (CustomEngine) this.SelectEngine.SelectedItem;
                LazySettings.SelectedEngine = this.SelectEngine.Text;
                LazySettings.SaveSettings();
                EngineHandler = EngineCompiler.Assemblys[selectedItem.AssemblyName];
                EngineHandler.Load();
                if ((this._radar != null) && !this._radar.IsDisposed)
                {
                    this.OpenRadar();
                }
            }
        }

        private void ShowPluginSettings(object sender, EventArgs e)
        {
            ButtonItem item = (ButtonItem) sender;
            if (PluginCompiler.LoadedPlugins.Contains(item.Tag.ToString()))
            {
                PluginCompiler.Assemblys[item.Tag.ToString()].Settings();
            }
        }

        public void StartBotting()
        {
            if (!ValidateKeys.AutoLoot)
            {
                Logging.Write(LogType.Error, "Please enable auto loot", new object[0]);
            }
            else if (ValidateKeys.ClickToMove)
            {
                Logging.Write(LogType.Error, "Please disable click to move", new object[0]);
            }
            else
            {
                BarMapper.MapBars();
                KeyHelper.LoadKeys();
                if (!ValidateKeys.Validate())
                {
                    Thread.Sleep(0x7d0);
                }
                Langs.Load();
                if (!EngineHandler.EngineStart())
                {
                    Logging.Write(LogType.Warning, "Engine returned false on load", new object[0]);
                }
                else
                {
                    LazySettings.SaveSettings();
                    if (!CombatEngine.StartOk)
                    {
                        Logging.Write(LogType.Warning, "CustomClass returned false on StartOk not starting", new object[0]);
                    }
                    else
                    {
                        CombatEngine.BotStarted();
                        Logging.Debug("Relogger: " + ReloggerSettings.ReloggingEnabled, new object[0]);
                        Logging.Debug("Engine: " + EngineHandler.Name, new object[0]);
                        Logging.Write("Bot started", new object[0]);
                        this.UpdateText(this.StartStopEngine, "Stop botting");
                        this.ShouldRelog = ReloggerSettings.ReloggingEnabled;
                        LazyForm.Engine = EngineHandler.Name;
                        this.DisableItems();
                        Engine.StartEngine(EngineHandler);
                        StopAfter.BotStarted();
                        PeriodicRelogger.BotStarted();
                        PluginManager.BotStart();
                        PluginManager.StartPulseThread(true);
                    }
                }
            }
        }

        private void StartEngineClick(object sender, EventArgs e)
        {
            this.StartStopBotting();
        }

        private void StartStopBotting()
        {
            this.StartStopEngine.Enabled = false;
            try
            {
                if (LazySettings.HookMouse)
                {
                    Hook.ReleaseMouse();
                }
            }
            catch
            {
            }
            if (!LazyLib.Wow.ObjectManager.Initialized)
            {
                Logging.Write(LogType.Error, "Please enter the world", new object[0]);
            }
            else if (!Engine.Running)
            {
                this.StartBotting();
            }
            else
            {
                this.ShouldRelog = false;
                this.StopBotting(true);
            }
            this.StartStopEngine.Enabled = true;
        }

        public void StopBotting(bool userStoppedIt)
        {
            CombatHandler.Stop();
            MoveHelper.Stop();
            MouseHelper.ReleaseMouse();
            this.EnableItems();
            StopAfter.BotStopped();
            PeriodicRelogger.BotStopped();
            this.UpdateText(this.StartStopEngine, "Start botting");
            if (Engine.Running)
            {
                if (!userStoppedIt && LazySettings.SoundStop)
                {
                    try
                    {
                        if (File.Exists(LazySettings.OurDirectory + @"\falert.wav"))
                        {
                            this._soundPlayer.SoundLocation = LazySettings.OurDirectory + @"\falert.wav";
                            this._soundPlayer.Play();
                        }
                    }
                    catch
                    {
                    }
                }
                if (LazySettings.UseCtm)
                {
                    MoveHelper.Forwards(true);
                    MoveHelper.Forwards(false);
                }
                MoveHelper.ReleaseKeys();
                Engine.StopEngine();
                EngineHandler.EngineStop();
                MoveHelper.ReleaseKeys();
                PluginManager.TerminatePulseThread();
                PluginManager.BotStop();
                Thread.Sleep(300);
                Logging.Write("Bot stopped", new object[0]);
            }
        }

        public void StopBotting(string reason, bool userStoppedIt)
        {
            Logging.Write("Bot stopping: " + reason, new object[0]);
            this.StopBotting(userStoppedIt);
        }

        public void StopBottingError(bool somethingWentWrong)
        {
            CombatHandler.Stop();
            MouseHelper.ReleaseMouse();
            this.EnableItems();
            StopAfter.BotStopped();
            PeriodicRelogger.BotStopped();
            this.UpdateText(this.StartStopEngine, "Start botting");
            if (Engine.Running)
            {
                if (!somethingWentWrong && LazySettings.SoundStop)
                {
                    try
                    {
                        if (File.Exists(LazySettings.OurDirectory + @"\falert.wav"))
                        {
                            this._soundPlayer.SoundLocation = LazySettings.OurDirectory + @"\falert.wav";
                            this._soundPlayer.Play();
                        }
                    }
                    catch
                    {
                    }
                }
                if (LazySettings.UseCtm)
                {
                    MoveHelper.Forwards(true);
                    MoveHelper.Forwards(false);
                }
                MoveHelper.ReleaseKeys();
                Engine.StopEngine();
                EngineHandler.EngineStop();
                MoveHelper.ReleaseKeys();
                PluginManager.TerminatePulseThread();
                PluginManager.BotStop();
                Thread.Sleep(300);
                Logging.Write("Bot stopped", new object[0]);
            }
        }

        public void StopBottingError(string reason, bool somethingWentWrong)
        {
            Logging.Write("Bot stopping: " + reason, new object[0]);
            this.StopBottingError(somethingWentWrong);
        }

        private void StopnAttackTargetDummyClick(object sender, EventArgs e)
        {
            if ((this._killDummy != null) && this._killDummy.IsAlive)
            {
                CombatHandler.Stop();
                this._killDummy.Abort();
                this._killDummy = null;
            }
            this.BtnAttackTargetDummy.Enabled = true;
        }

        private void UpdateButtonItem(ButtonItem items, bool enabled)
        {
            MethodInvoker method = null;
            if (!items.InvokeRequired)
            {
                items.Enabled = enabled;
            }
            else
            {
                if (method == null)
                {
                    method = () => this.UpdateButtonItem(items, enabled);
                }
                items.Invoke(method);
            }
        }

        private void UpdateComboBoxEx(ComboBoxEx item, bool enabled)
        {
            if (!item.InvokeRequired)
            {
                item.Enabled = enabled;
            }
            else
            {
                object[] args = new object[] { item, enabled };
                item.Invoke(new Action<ComboBoxEx, bool>(this.UpdateComboBoxEx), args);
            }
        }

        public void UpdateExpandableSplitter(ExpandableSplitter expandable, bool expanded, bool enabled)
        {
            MethodInvoker method = null;
            if (!expandable.InvokeRequired)
            {
                expandable.Expanded = expanded;
                expandable.Enabled = enabled;
            }
            else
            {
                if (method == null)
                {
                    method = () => this.UpdateExpandableSplitter(expandable, expanded, enabled);
                }
                expandable.BeginInvoke(method);
            }
        }

        public void UpdateGroupControl(GroupPanel groupControl, string text)
        {
            MethodInvoker method = null;
            if (!groupControl.InvokeRequired)
            {
                groupControl.Text = text;
            }
            else
            {
                if (method == null)
                {
                    method = () => this.UpdateGroupControl(groupControl, text);
                }
                groupControl.BeginInvoke(method);
            }
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

        private void UpdateStateChange(object sender, Engine.NotifyStateChanged e)
        {
            this.UpdateStateText(e.Name);
        }

        public void UpdateStateText(string text)
        {
            if (!base.InvokeRequired)
            {
                this.Text = text;
            }
            else
            {
                object[] args = new object[] { text };
                base.Invoke(new Action<string>(this.UpdateStateText), args);
            }
        }

        public void UpdateStatsText(string text)
        {
            if (!base.InvokeRequired)
            {
                this.StatsText.Text = text;
            }
            else
            {
                object[] args = new object[] { text };
                base.Invoke(new Action<string>(this.UpdateStatsText), args);
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

        public void WhisperMessage(string message)
        {
            AppendMessage(this.ChatWhisper, message, Color.Black);
        }
    }
}

