namespace LazyEvo.Plugins.RotationPlugin
{
    using DevComponents.DotNetBar;
    using DevComponents.DotNetBar.Controls;
    using LazyEvo;
    using LazyEvo.Forms.Helpers;
    using LazyEvo.PVEBehavior;
    using LazyEvo.PVEBehavior.Behavior;
    using LazyEvo.PVEBehavior.Behavior.Conditions;
    using LazyLib;
    using LazyLib.ActionBar;
    using LazyLib.Helpers;
    using LazyLib.Wow;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Windows.Forms;

    internal class RotatorForm : Office2007Form
    {
        private readonly List<Hotkey> _hotKeys = new List<Hotkey>();
        internal string OurDirectory;
        internal LazyEvo.Plugins.RotationPlugin.RotationManagerController RotationManagerController;
        private bool _firstTime = true;
        private Rotation _rotation;
        private Thread _rotationThread;
        private RotatorStatus status;
        private IContainer components;
        private StyleManager styleManager1;
        private SuperTooltip superTooltip1;
        private GroupPanel groupPanel1;
        private CheckBoxX StartMonitoring;
        private ButtonX CBOpenRotationManager;
        private CheckBoxX CBShowStatusWindow;

        public RotatorForm()
        {
            this.InitializeComponent();
            PveBehaviorSettings.LoadSettings();
            Geometry.GeometryFromString(GeomertrySettings.RotatorForm, this);
        }

        private void CbOpenRotationManagerClick(object sender, EventArgs e)
        {
            base.Hide();
            RotationManagerForm form = new RotationManagerForm(this.RotationManagerController);
            form.Show();
            form.Closed += new EventHandler(this.ShowAgain);
        }

        private void CBShowStatusWindow_CheckedChanged(object sender, EventArgs e)
        {
            if (!this.CBShowStatusWindow.Checked)
            {
                this.status.Close();
            }
            else
            {
                this.status = new RotatorStatus();
                this.status.Show();
            }
        }

        private static void CheckBuffAndKeys(IEnumerable<Rule> rules)
        {
            foreach (Rule rule in rules)
            {
                if (!rule.IsScript)
                {
                    rule.BotStarting();
                    if (!rule.Action.DoesKeyExist)
                    {
                        Logging.Write(LogType.Warning, "Key: " + rule.Action.Name + " does not exist on your bars", new object[0]);
                    }
                    foreach (AbstractCondition condition in rule.GetConditions)
                    {
                        if ((condition is BuffCondition) && (!string.IsNullOrEmpty(((BuffCondition) condition).GetBuffName()) && !BarMapper.DoesBuffExist(((BuffCondition) condition).GetBuffName())))
                        {
                            Logging.Write(LogType.Warning, "Buff: " + ((BuffCondition) condition).GetBuffName() + " does not exist in HasWellKnownBuff will not detect it correctly", new object[0]);
                        }
                    }
                }
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

        private void DoRotation()
        {
            List<Rule> getRules = this._rotation.Rules.GetRules;
            getRules.Sort();
            while (true)
            {
                try
                {
                    while (true)
                    {
                        if (LazyLib.Wow.ObjectManager.MyPlayer.HasTarget)
                        {
                            foreach (Rule rule in from rule in getRules
                                where rule.IsOk
                                select rule)
                            {
                                PUnit target = LazyLib.Wow.ObjectManager.MyPlayer.Target;
                                if (target.IsValid && target.IsAlive)
                                {
                                    rule.ExecuteAction(this._rotation.GlobalCooldown);
                                    break;
                                }
                            }
                        }
                        Thread.Sleep(10);
                        Application.DoEvents();
                        break;
                    }
                }
                catch (ThreadAbortException)
                {
                }
                catch (Exception exception)
                {
                    Logging.Debug("Exception in rotation: " + exception, new object[0]);
                }
            }
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.styleManager1 = new StyleManager(this.components);
            this.superTooltip1 = new SuperTooltip();
            this.groupPanel1 = new GroupPanel();
            this.StartMonitoring = new CheckBoxX();
            this.CBOpenRotationManager = new ButtonX();
            this.CBShowStatusWindow = new CheckBoxX();
            this.groupPanel1.SuspendLayout();
            base.SuspendLayout();
            this.styleManager1.ManagerStyle = eStyle.Windows7Blue;
            this.groupPanel1.CanvasColor = SystemColors.Control;
            this.groupPanel1.ColorSchemeStyle = eDotNetBarStyle.Office2007;
            this.groupPanel1.Controls.Add(this.StartMonitoring);
            this.groupPanel1.Controls.Add(this.CBOpenRotationManager);
            this.groupPanel1.Location = new Point(2, 6);
            this.groupPanel1.Name = "groupPanel1";
            this.groupPanel1.Size = new Size(0xf4, 0x3a);
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
            this.StartMonitoring.BackColor = Color.Transparent;
            this.StartMonitoring.BackgroundStyle.Class = "";
            this.StartMonitoring.BackgroundStyle.CornerType = eCornerType.Square;
            this.StartMonitoring.Location = new Point(7, 13);
            this.StartMonitoring.Name = "StartMonitoring";
            this.StartMonitoring.Size = new Size(80, 0x17);
            this.StartMonitoring.Style = eDotNetBarStyle.StyleManagerControlled;
            this.StartMonitoring.TabIndex = 1;
            this.StartMonitoring.Text = "Enabled";
            this.StartMonitoring.CheckedChanged += new EventHandler(this.StartMonitoringCheckedChanged);
            this.CBOpenRotationManager.AccessibleRole = AccessibleRole.PushButton;
            this.CBOpenRotationManager.ColorTable = eButtonColor.OrangeWithBackground;
            this.CBOpenRotationManager.Location = new Point(0x5d, 3);
            this.CBOpenRotationManager.Name = "CBOpenRotationManager";
            this.CBOpenRotationManager.Size = new Size(120, 0x2a);
            this.CBOpenRotationManager.Style = eDotNetBarStyle.StyleManagerControlled;
            this.CBOpenRotationManager.TabIndex = 0;
            this.CBOpenRotationManager.Text = "Open rotation manager";
            this.CBOpenRotationManager.Click += new EventHandler(this.CbOpenRotationManagerClick);
            this.CBShowStatusWindow.BackgroundStyle.Class = "";
            this.CBShowStatusWindow.BackgroundStyle.CornerType = eCornerType.Square;
            this.CBShowStatusWindow.Location = new Point(0x76, 70);
            this.CBShowStatusWindow.Name = "CBShowStatusWindow";
            this.CBShowStatusWindow.Size = new Size(0x80, 0x17);
            this.CBShowStatusWindow.Style = eDotNetBarStyle.StyleManagerControlled;
            this.CBShowStatusWindow.TabIndex = 1;
            this.CBShowStatusWindow.Text = "Show status window";
            this.CBShowStatusWindow.CheckedChanged += new EventHandler(this.CBShowStatusWindow_CheckedChanged);
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.FromArgb(0xdf, 0xe9, 0xf5);
            base.ClientSize = new Size(250, 0x5d);
            base.Controls.Add(this.CBShowStatusWindow);
            base.Controls.Add(this.groupPanel1);
            this.DoubleBuffered = true;
            base.Name = "RotatorForm";
            base.FormClosing += new FormClosingEventHandler(this.RotatorFormFormClosing);
            base.Load += new EventHandler(this.RotatorFormLoad);
            this.groupPanel1.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        private static void LoadFirstTime()
        {
            Langs.Load();
            KeyHelper.LoadKeys();
            BarMapper.MapBars();
        }

        private void RotatorFormFormClosing(object sender, FormClosingEventArgs e)
        {
            GeomertrySettings.RotatorForm = Geometry.GeometryToString(this);
            this.Stop();
        }

        private void RotatorFormLoad(object sender, EventArgs e)
        {
            string directoryName = new FileInfo(Application.ExecutablePath).DirectoryName;
            this.OurDirectory = directoryName;
            RotationSettings.LoadSettings();
            if (this.RotationManagerController == null)
            {
                if (File.Exists(this.OurDirectory + @"\Rotations\" + RotationSettings.LoadedRotationManager + ".xml"))
                {
                    this.RotationManagerController = new LazyEvo.Plugins.RotationPlugin.RotationManagerController();
                    this.RotationManagerController.Load(this.OurDirectory + @"\Rotations\" + RotationSettings.LoadedRotationManager + ".xml");
                }
                else
                {
                    Logging.Write("Could not load the rotation manager", new object[0]);
                    this.RotationManagerController = null;
                }
            }
        }

        private void ShowAgain(object sender, EventArgs e)
        {
            base.Show();
        }

        private void Start()
        {
            if (this._firstTime)
            {
                LoadFirstTime();
                this._firstTime = false;
            }
            if (File.Exists(this.OurDirectory + @"\Rotations\" + RotationSettings.LoadedRotationManager + ".xml"))
            {
                this.RotationManagerController = new LazyEvo.Plugins.RotationPlugin.RotationManagerController();
                this.RotationManagerController.Load(this.OurDirectory + @"\Rotations\" + RotationSettings.LoadedRotationManager + ".xml");
            }
            foreach (Rotation rotation in from r in this.RotationManagerController.Rotations
                where r.Active
                select r)
            {
                try
                {
                    CheckBuffAndKeys(rotation.Rules.GetRules);
                    Hotkey item = new Hotkey {
                        Windows = rotation.Windows,
                        Shift = rotation.Shift,
                        Alt = rotation.Alt,
                        Control = rotation.Ctrl
                    };
                    Rotation rotation1 = rotation;
                    item.KeyCode = (Keys) Enumerable.FirstOrDefault<KeysData>(RotationSettings.KeysList, k => k.Text == rotation1.Key).Code;
                    item.Pressed += (param0, param1) => this.StartRotation(rotation1.Name);
                    if (!item.GetCanRegister(this))
                    {
                        object[] args = new object[] { rotation.Key };
                        Logging.Write("Cannot register {0} as hotkey", args);
                    }
                    else
                    {
                        item.Register(this);
                        this._hotKeys.Add(item);
                    }
                }
                catch
                {
                    object[] args = new object[] { rotation.Key };
                    Logging.Write("Cannot register {0} as hotkey", args);
                }
            }
        }

        private void StartMonitoringCheckedChanged(object sender, EventArgs e)
        {
            this.CBOpenRotationManager.Enabled = !this.StartMonitoring.Checked;
            if (!this.StartMonitoring.Checked)
            {
                this.Stop();
                Logging.Write(LogType.Info, "Stopped rotator", new object[0]);
            }
            else if (!File.Exists(this.OurDirectory + @"\Rotations\" + RotationSettings.LoadedRotationManager + ".xml"))
            {
                Logging.Write(LogType.Error, "No rotation loaded", new object[0]);
                this.StartMonitoring.Checked = false;
            }
            else if (LazyLib.Wow.ObjectManager.InGame)
            {
                this.Start();
            }
            else
            {
                Logging.Write(LogType.Error, "Please enter the game", new object[0]);
                this.StartMonitoring.Checked = false;
            }
        }

        private void StartRotation(string name)
        {
            Func<Rotation, bool> func = null;
            if ((this._rotationThread != null) && this._rotationThread.IsAlive)
            {
                this._rotationThread.Abort();
                this._rotationThread = null;
            }
            if (Enumerable.FirstOrDefault<Rotation>(this.RotationManagerController.Rotations, r => r.Name == name) == this._rotation)
            {
                this._rotation = null;
                Logging.Write(LogType.Info, "Stopped rotator", new object[0]);
                this.UpdateStatus(false);
            }
            else
            {
                if (func == null)
                {
                    func = r => r.Name == name;
                }
                this._rotation = Enumerable.FirstOrDefault<Rotation>(this.RotationManagerController.Rotations, func);
                Thread thread = new Thread(new ThreadStart(this.DoRotation)) {
                    IsBackground = true
                };
                this._rotationThread = thread;
                this._rotationThread.Start();
                Logging.Write(LogType.Info, "Started rotator", new object[0]);
                this.UpdateStatus(true);
            }
        }

        private void Stop()
        {
            if ((this._rotationThread != null) && this._rotationThread.IsAlive)
            {
                this._rotationThread.Abort();
                this._rotationThread = null;
            }
            foreach (Hotkey hotkey in this._hotKeys)
            {
                try
                {
                    if (hotkey.Registered)
                    {
                        hotkey.Unregister();
                    }
                }
                catch
                {
                }
            }
            this._hotKeys.Clear();
        }

        private void UpdateStatus(bool running)
        {
            if ((this.status != null) && !this.status.IsDisposed)
            {
                this.status.UpdateStatus(running);
            }
        }
    }
}

