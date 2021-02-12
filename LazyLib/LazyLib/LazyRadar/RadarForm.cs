namespace LazyLib.LazyRadar
{
    using DevComponents.DotNetBar;
    using LazyLib;
    using LazyLib.Helpers;
    using LazyLib.LazyRadar.Drawer;
    using LazyLib.Wow;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.IO;
    using System.Windows.Forms;

    public class RadarForm : Office2007Form
    {
        private const string SettingsName = @"\Settings\lazy_radars.ini";
        private static string OurDirectory;
        private readonly Color _colorMe = Color.SpringGreen;
        private readonly Dictionary<string, bool> _itemsShouldDraw = new Dictionary<string, bool>();
        private readonly List<IDrawItem> _itemsToDraw = new List<IDrawItem>();
        private readonly object _locker = new object();
        private readonly List<IMouseClick> _mouseFunctions = new List<IMouseClick>();
        private readonly IniManager pIniManager;
        public Graphics ScreenDc;
        private Font _fontText;
        private Bitmap _offScreenBmp;
        private double _scale = 1.0;
        private bool _updateFormSize = true;
        private IContainer components;
        private Timer MapTimer;
        private ExpandableSplitter expandableSplitter1;
        private ItemPanel ControlSettings;
        private CheckBoxItem CBTopMost;

        public RadarForm()
        {
            this.InitializeComponent();
            OurDirectory = new FileInfo(Application.ExecutablePath).DirectoryName;
            this.pIniManager = new IniManager(OurDirectory + @"\Settings\lazy_radars.ini");
        }

        public void AddDrawItem(IDrawItem item)
        {
            this._itemsToDraw.Add(item);
            CheckBoxItem item2 = new CheckBoxItem(item.SettingName());
            this._itemsShouldDraw.Add(item.SettingName(), false);
            item2.Tag = item.SettingName();
            item2.Text = item.CheckBoxName();
            if (this.pIniManager.GetBoolean("Radar", item.SettingName(), false))
            {
                item2.Checked = true;
                this._itemsShouldDraw[item.SettingName()] = true;
            }
            item2.Click += new EventHandler(this.DrawItemClick);
            this.ControlSettings.Items.Add(item2);
        }

        public void AddMonitorMouseClick(IMouseClick click)
        {
            this._mouseFunctions.Add(click);
        }

        private void CbTopMostClick(object sender, EventArgs e)
        {
            base.TopMost = this.CBTopMost.Checked;
        }

        private static double ConvertHeading(double heading) => 
            (heading * -1.0) - 1.5707963267948966;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void DrawItemClick(object sender, EventArgs e)
        {
            CheckBoxItem item = (CheckBoxItem) sender;
            lock (this._locker)
            {
                this._itemsShouldDraw[(string) item.Tag] = item.Checked;
                this.pIniManager.IniWriteValue("Radar", (string) item.Tag, item.Checked.ToString());
            }
        }

        public void DrawString(int i, int x, int y)
        {
            object[] args = new object[] { i };
            Logging.Debug("DrawString i = {0}", args);
            Graphics graphics = base.CreateGraphics();
            Font font = new Font("Arial", 16f);
            SolidBrush brush = new SolidBrush(Color.Black);
            graphics.DrawString(Convert.ToString(i), font, brush, (float) x, (float) y);
            font.Dispose();
            brush.Dispose();
            graphics.Dispose();
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.MapTimer = new Timer(this.components);
            this.expandableSplitter1 = new ExpandableSplitter();
            this.ControlSettings = new ItemPanel();
            this.CBTopMost = new CheckBoxItem();
            base.SuspendLayout();
            this.MapTimer.Enabled = true;
            this.MapTimer.Interval = 150;
            this.MapTimer.Tick += new EventHandler(this.MapTimerTick);
            this.expandableSplitter1.BackColor = Color.FromArgb(0xf6, 0xfb, 0xff);
            this.expandableSplitter1.BackColor2 = Color.FromArgb(0x84, 0x92, 0xa6);
            this.expandableSplitter1.BackColor2SchemePart = eColorSchemePart.PanelBorder;
            this.expandableSplitter1.BackColorSchemePart = eColorSchemePart.PanelBackground;
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
            this.expandableSplitter1.Location = new Point(0x6c, 0);
            this.expandableSplitter1.Name = "expandableSplitter1";
            this.expandableSplitter1.Size = new Size(10, 0x10f);
            this.expandableSplitter1.Style = eSplitterStyle.Office2007;
            this.expandableSplitter1.TabIndex = 8;
            this.expandableSplitter1.TabStop = false;
            this.ControlSettings.BackColor = Color.LightGray;
            this.ControlSettings.BackgroundStyle.BackColor = Color.White;
            this.ControlSettings.BackgroundStyle.BorderBottom = eStyleBorderType.Solid;
            this.ControlSettings.BackgroundStyle.BorderBottomWidth = 1;
            this.ControlSettings.BackgroundStyle.BorderColor = Color.FromArgb(0x7f, 0x9d, 0xb9);
            this.ControlSettings.BackgroundStyle.BorderLeft = eStyleBorderType.Solid;
            this.ControlSettings.BackgroundStyle.BorderLeftWidth = 1;
            this.ControlSettings.BackgroundStyle.BorderRight = eStyleBorderType.Solid;
            this.ControlSettings.BackgroundStyle.BorderRightWidth = 1;
            this.ControlSettings.BackgroundStyle.BorderTop = eStyleBorderType.Solid;
            this.ControlSettings.BackgroundStyle.BorderTopWidth = 1;
            this.ControlSettings.BackgroundStyle.Class = "";
            this.ControlSettings.BackgroundStyle.CornerType = eCornerType.Square;
            this.ControlSettings.BackgroundStyle.PaddingBottom = 1;
            this.ControlSettings.BackgroundStyle.PaddingLeft = 1;
            this.ControlSettings.BackgroundStyle.PaddingRight = 1;
            this.ControlSettings.BackgroundStyle.PaddingTop = 1;
            this.ControlSettings.ContainerControlProcessDialogKey = true;
            this.ControlSettings.Dock = DockStyle.Left;
            BaseItem[] items = new BaseItem[] { this.CBTopMost };
            this.ControlSettings.Items.AddRange(items);
            this.ControlSettings.LayoutOrientation = eOrientation.Vertical;
            this.ControlSettings.Location = new Point(0, 0);
            this.ControlSettings.Name = "ControlSettings";
            this.ControlSettings.Size = new Size(0x6c, 0x10f);
            this.ControlSettings.Style = eDotNetBarStyle.Windows7;
            this.ControlSettings.TabIndex = 7;
            this.ControlSettings.Text = "itemPanel1";
            this.CBTopMost.Name = "CBTopMost";
            this.CBTopMost.Text = "Top Most";
            this.CBTopMost.Click += new EventHandler(this.CbTopMostClick);
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.FromArgb(0xdf, 0xe9, 0xf5);
            base.ClientSize = new Size(0x1f5, 0x10f);
            base.Controls.Add(this.expandableSplitter1);
            base.Controls.Add(this.ControlSettings);
            this.DoubleBuffered = true;
            base.Name = "RadarForm";
            base.FormClosed += new FormClosedEventHandler(this.MapControlFormClosed);
            base.Load += new EventHandler(this.MapControlLoad);
            base.ResizeBegin += new EventHandler(this.MapControlResizeBegin);
            base.MouseClick += new MouseEventHandler(this.MapMouseClick);
            base.MouseWheel += new MouseEventHandler(this.MapMouseWheel);
            base.Resize += new EventHandler(this.MapControlResize);
            base.ResumeLayout(false);
        }

        public void Log(string message)
        {
            Logging.Write(message, new object[0]);
        }

        private void MapControlFormClosed(object sender, FormClosedEventArgs e)
        {
            this.StopMap();
        }

        private void MapControlLoad(object sender, EventArgs e)
        {
            this._fontText = new Font("Verdana", 6.5f);
            this.Log("Radar starting");
            this.AddDrawItem(new DrawEnemies());
            this.AddDrawItem(new DrawFriends());
            this.AddDrawItem(new DrawObjects());
            this.AddDrawItem(new DrawUnits());
        }

        private void MapControlResize(object sender, EventArgs e)
        {
            this._updateFormSize = true;
        }

        private void MapControlResizeBegin(object sender, EventArgs e)
        {
            this._updateFormSize = true;
        }

        private void MapMouseClick(object sender, MouseEventArgs e)
        {
            foreach (IMouseClick click in this._mouseFunctions)
            {
                click.Click(this, e);
            }
        }

        private void MapMouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                if (this._scale > 2.0)
                {
                    this._scale += 0.6;
                }
                else
                {
                    this._scale += 0.3;
                }
            }
            else if (this._scale > 2.0)
            {
                this._scale -= 0.6;
            }
            else if (this._scale > 0.3)
            {
                this._scale -= 0.3;
            }
        }

        private void MapTimerTick(object sender, EventArgs eventArgs)
        {
            if (LazyLib.Wow.ObjectManager.Initialized)
            {
                try
                {
                    if (this._updateFormSize)
                    {
                        this._offScreenBmp = new Bitmap(base.Width, base.Height);
                        this._updateFormSize = false;
                    }
                    this.ScreenDc = Graphics.FromImage(this._offScreenBmp);
                    this.ScreenDc.FillRectangle(new SolidBrush(Color.LightGray), new Rectangle(0, 0, this._offScreenBmp.Width, this._offScreenBmp.Height));
                    foreach (IDrawItem item in this._itemsToDraw)
                    {
                        lock (this._locker)
                        {
                            if (this._itemsShouldDraw[item.SettingName()])
                            {
                                item.Draw(this);
                            }
                        }
                    }
                    this.PrintPlayer();
                    Graphics graphics = base.CreateGraphics();
                    this.ScreenDc.SmoothingMode = SmoothingMode.HighQuality;
                    graphics.DrawImage(this._offScreenBmp, 0, 0);
                    this.ScreenDc.Dispose();
                }
                catch (Exception exception)
                {
                    Logging.Write("Error in radar: " + exception, new object[0]);
                }
            }
        }

        public int OffsetX(float obj, float me) => 
            (base.Height / 2) - Convert.ToInt32((float) ((obj - me) * ((float) this._scale)));

        public int OffsetY(float obj, float me) => 
            (base.Width / 2) - Convert.ToInt32((float) ((obj - me) * ((float) this._scale)));

        public void PrintArrow(Color color, int x, int y, double heading, string topString, string botString)
        {
            heading = ConvertHeading(heading);
            Point[] points = new Point[] { new Point(Convert.ToInt32((double) (x + (Math.Cos(heading) * 10.0))), Convert.ToInt32((double) (y + (Math.Sin(heading) * 10.0)))), new Point(Convert.ToInt32((double) (x + (Math.Cos(heading + 2.0943951023931953) * 2.0))), Convert.ToInt32((double) (y + (Math.Sin(heading + 2.0943951023931953) * 2.0)))), new Point(x, y), new Point(Convert.ToInt32((double) (x + (Math.Cos(heading + -2.0943951023931953) * 2.0))), Convert.ToInt32((double) (y + (Math.Sin(heading + -2.0943951023931953) * 2.0)))), new Point(Convert.ToInt32((double) (x + (Math.Cos(heading) * 10.0))), Convert.ToInt32((double) (y + (Math.Sin(heading) * 10.0)))) };
            this.ScreenDc.DrawLines(new Pen(color), points);
            if (topString.Length > 0)
            {
                this.ScreenDc.DrawString(topString, this._fontText, new SolidBrush(color), new PointF(x - (topString.Length * 2.2f), (float) (y - 15)));
            }
            if (botString.Length > 0)
            {
                this.ScreenDc.DrawString(botString, this._fontText, new SolidBrush(color), new PointF((float) (x - (botString.Length * 2)), (float) (y + 6)));
            }
            SolidBrush brush = new SolidBrush(color);
            this.ScreenDc.DrawEllipse(new Pen(color), x - 3, y - 3, 6, 6);
            this.ScreenDc.FillEllipse(brush, x - 3, y - 3, 6, 6);
            brush.Dispose();
        }

        public void PrintCircle(Color color, int x, int y, string name)
        {
            SolidBrush brush = new SolidBrush(color);
            this.ScreenDc.DrawEllipse(new Pen(color), x - 4, y - 4, 8, 8);
            this.ScreenDc.FillEllipse(brush, x - 4, y - 4, 8, 8);
            this.ScreenDc.DrawString(name, this._fontText, new SolidBrush(color), new PointF((float) (x - (name.Length * 2)), (float) (y - 15)));
            brush.Dispose();
        }

        private void PrintPlayer()
        {
            this.PrintArrow(this._colorMe, base.Width / 2, base.Height / 2, (double) LazyLib.Wow.ObjectManager.MyPlayer.Facing, "", "");
        }

        public void Start()
        {
            base.Show();
        }

        public void StopMap()
        {
            this._offScreenBmp = null;
            this.ScreenDc = null;
        }
    }
}

