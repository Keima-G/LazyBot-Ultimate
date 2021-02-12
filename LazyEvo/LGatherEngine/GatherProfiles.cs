namespace LazyEvo.LGatherEngine
{
    using DevComponents.DotNetBar;
    using DevComponents.DotNetBar.Controls;
    using LazyLib;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Net;
    using System.Reflection;
    using System.Text;
    using System.Threading;
    using System.Windows.Forms;
    using System.Xml;

    [Obfuscation(Feature="renaming", ApplyToMembers=true)]
    public class GatherProfiles : Office2007Form
    {
        public const string Url = "http://profiles.wow-lazybot.com/listflying.php?username={0}&password={1}";
        private EventHandler<EProfileDownloaded> ProfileDownload;
        private IContainer components;
        private DataGridViewX ProfileList;
        private ButtonX BtnRefresh;
        private ButtonX BtnDownload;
        private GroupPanel groupPanel1;
        private ButtonX BtnBrowse;
        private TextBoxX TBProfile;
        private LabelX labelX1;
        private TextBoxX TBZone;
        private LabelX labelX2;
        private TextBoxX TBComment;
        private LabelX labelX3;
        private LabelX labelX4;
        private LabelX labelX5;
        private TextBoxX TBName;
        private ButtonX BtnBrowseImage;
        private TextBoxX TBImage;
        private ButtonX BtnUpload;

        public event EventHandler<EProfileDownloaded> ProfileDownload
        {
            add
            {
                EventHandler<EProfileDownloaded> profileDownload = this.ProfileDownload;
                while (true)
                {
                    EventHandler<EProfileDownloaded> comparand = profileDownload;
                    EventHandler<EProfileDownloaded> handler3 = comparand + value;
                    profileDownload = Interlocked.CompareExchange<EventHandler<EProfileDownloaded>>(ref this.ProfileDownload, handler3, comparand);
                    if (ReferenceEquals(profileDownload, comparand))
                    {
                        return;
                    }
                }
            }
            remove
            {
                EventHandler<EProfileDownloaded> profileDownload = this.ProfileDownload;
                while (true)
                {
                    EventHandler<EProfileDownloaded> comparand = profileDownload;
                    EventHandler<EProfileDownloaded> handler3 = comparand - value;
                    profileDownload = Interlocked.CompareExchange<EventHandler<EProfileDownloaded>>(ref this.ProfileDownload, handler3, comparand);
                    if (ReferenceEquals(profileDownload, comparand))
                    {
                        return;
                    }
                }
            }
        }

        public GatherProfiles()
        {
            this.InitializeComponent();
        }

        private void BtnBrowseClick(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog {
                InitialDirectory = AppDomain.CurrentDomain.BaseDirectory,
                Filter = "Profile (*.xml)|*.xml"
            };
            if ((dialog.ShowDialog() == DialogResult.OK) && (Path.GetExtension(dialog.FileName) == ".xml"))
            {
                if (this.ValidateProfile(dialog.FileName))
                {
                    this.TBProfile.Text = dialog.FileName;
                }
                else
                {
                    MessageBox.Show("Invalid profile loaded");
                    this.TBProfile.Text = string.Empty;
                }
            }
        }

        private void BtnBrowseImageClick(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog {
                InitialDirectory = AppDomain.CurrentDomain.BaseDirectory,
                Filter = "Image (*.jpg)|*.jpg;(*.gif)|*.gif;(*.png)|*.png"
            };
            if ((dialog.ShowDialog() == DialogResult.OK) && ((Path.GetExtension(dialog.FileName) == ".jpg") || ((Path.GetExtension(dialog.FileName) == ".gif") || (Path.GetExtension(dialog.FileName) == ".png"))))
            {
                this.TBImage.Text = dialog.FileName;
            }
        }

        private void BtnDownloadClick(object sender, EventArgs e)
        {
            if (this.ProfileList.SelectedRows[0] == null)
            {
                MessageBox.Show("Selected a profile before downloading");
            }
            else
            {
                DataGridViewRow row = this.ProfileList.SelectedRows[0];
                OnlineProfile profile = new OnlineProfile(row.Cells[0].Value.ToString(), row.Cells[1].Value.ToString(), row.Cells[2].Value.ToString(), row.Cells[3].Value.ToString(), row.Cells[4].Value.ToString(), row.Cells[5].Value.ToString());
                WebClient client = new WebClient();
                string path = LazySettings.OurDirectory + @"\GatherProfiles";
                string str2 = path + @"\" + profile.Name + ".xml";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                if (!System.IO.File.Exists(str2) || (MessageBox.Show(profile.Name + " already exist - not saving. Overwrite?", "Overwrite", MessageBoxButtons.YesNo) != DialogResult.No))
                {
                    try
                    {
                        WebRequest.Create($"http://profiles.wow-lazybot.com/getflying.php?username={LazySettings.UserName}&password={LazySettings.Password}&id={profile.Id}").GetResponse();
                        client.DownloadFile($"http://profiles.wow-lazybot.com/GatherProfiles/{profile.Id}.xml", str2);
                        this.InvokeProfileDownload(new EProfileDownloaded(str2));
                        MessageBox.Show("Profile downloaded and loaded");
                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show("Download error !\n" + exception.Message);
                    }
                }
            }
        }

        private void BtnRefreshClick(object sender, EventArgs e)
        {
            this.RefreshList();
        }

        private void BtnUploadClick(object sender, EventArgs e)
        {
            if ((this.TBProfile.Text == string.Empty) || ((this.TBName.Text == string.Empty) || (this.TBZone.Text == string.Empty)))
            {
                MessageBox.Show("Profile, Name, Zone are required");
            }
            else
            {
                WebClient client = new WebClient();
                client.Headers.Add("Content-Type", "binary/octet-stream");
                byte[] bytes = client.UploadFile($"http://profiles.wow-lazybot.com/sendflying.php?username={LazySettings.UserName}&password={LazySettings.Password}&name={this.TBName.Text}&zone={this.TBZone.Text}&comment={this.TBComment.Text}", "POST", this.TBProfile.Text);
                string str2 = Encoding.UTF8.GetString(bytes, 0, bytes.Length);
                if (!str2.Contains("Key:"))
                {
                    MessageBox.Show("Could not upload: " + str2);
                }
                else
                {
                    this.TBProfile.Text = string.Empty;
                    this.TBImage.Text = string.Empty;
                    this.TBZone.Text = string.Empty;
                    this.TBComment.Text = string.Empty;
                    this.TBName.Text = string.Empty;
                    MessageBox.Show("Upload ok");
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

        private void InitializeComponent()
        {
            DataGridViewCellStyle style = new DataGridViewCellStyle();
            this.ProfileList = new DataGridViewX();
            this.BtnRefresh = new ButtonX();
            this.BtnDownload = new ButtonX();
            this.groupPanel1 = new GroupPanel();
            this.BtnUpload = new ButtonX();
            this.TBName = new TextBoxX();
            this.labelX4 = new LabelX();
            this.TBComment = new TextBoxX();
            this.labelX3 = new LabelX();
            this.TBZone = new TextBoxX();
            this.labelX2 = new LabelX();
            this.BtnBrowse = new ButtonX();
            this.TBProfile = new TextBoxX();
            this.labelX1 = new LabelX();
            this.TBImage = new TextBoxX();
            this.BtnBrowseImage = new ButtonX();
            this.labelX5 = new LabelX();
            ((ISupportInitialize) this.ProfileList).BeginInit();
            this.groupPanel1.SuspendLayout();
            base.SuspendLayout();
            this.ProfileList.AllowUserToAddRows = false;
            this.ProfileList.AllowUserToDeleteRows = false;
            this.ProfileList.AllowUserToResizeRows = false;
            this.ProfileList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.ProfileList.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            style.BackColor = SystemColors.Window;
            style.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            style.ForeColor = SystemColors.ControlText;
            style.SelectionBackColor = SystemColors.Highlight;
            style.SelectionForeColor = SystemColors.ControlText;
            style.WrapMode = DataGridViewTriState.False;
            this.ProfileList.DefaultCellStyle = style;
            this.ProfileList.GridColor = Color.FromArgb(0xd0, 0xd7, 0xe5);
            this.ProfileList.Location = new Point(5, 3);
            this.ProfileList.MultiSelect = false;
            this.ProfileList.Name = "ProfileList";
            this.ProfileList.ReadOnly = true;
            this.ProfileList.RowHeadersVisible = false;
            this.ProfileList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.ProfileList.ShowCellErrors = false;
            this.ProfileList.ShowCellToolTips = false;
            this.ProfileList.ShowEditingIcon = false;
            this.ProfileList.ShowRowErrors = false;
            this.ProfileList.Size = new Size(0x32b, 0xb5);
            this.ProfileList.TabIndex = 4;
            this.BtnRefresh.AccessibleRole = AccessibleRole.PushButton;
            this.BtnRefresh.ColorTable = eButtonColor.OrangeWithBackground;
            this.BtnRefresh.Location = new Point(0x68, 0xc1);
            this.BtnRefresh.Name = "BtnRefresh";
            this.BtnRefresh.Size = new Size(0x4b, 0x17);
            this.BtnRefresh.Style = eDotNetBarStyle.StyleManagerControlled;
            this.BtnRefresh.TabIndex = 5;
            this.BtnRefresh.Text = "Refresh";
            this.BtnRefresh.Click += new EventHandler(this.BtnRefreshClick);
            this.BtnDownload.AccessibleRole = AccessibleRole.PushButton;
            this.BtnDownload.ColorTable = eButtonColor.OrangeWithBackground;
            this.BtnDownload.Location = new Point(0xb9, 0xc1);
            this.BtnDownload.Name = "BtnDownload";
            this.BtnDownload.Size = new Size(0x4b, 0x17);
            this.BtnDownload.Style = eDotNetBarStyle.StyleManagerControlled;
            this.BtnDownload.TabIndex = 6;
            this.BtnDownload.Text = "Download";
            this.BtnDownload.Click += new EventHandler(this.BtnDownloadClick);
            this.groupPanel1.CanvasColor = SystemColors.Control;
            this.groupPanel1.ColorSchemeStyle = eDotNetBarStyle.Office2007;
            this.groupPanel1.Controls.Add(this.BtnUpload);
            this.groupPanel1.Controls.Add(this.TBName);
            this.groupPanel1.Controls.Add(this.labelX4);
            this.groupPanel1.Controls.Add(this.TBComment);
            this.groupPanel1.Controls.Add(this.labelX3);
            this.groupPanel1.Controls.Add(this.TBZone);
            this.groupPanel1.Controls.Add(this.labelX2);
            this.groupPanel1.Controls.Add(this.BtnBrowse);
            this.groupPanel1.Controls.Add(this.TBProfile);
            this.groupPanel1.Controls.Add(this.labelX1);
            this.groupPanel1.Location = new Point(0x67, 0xdb);
            this.groupPanel1.Name = "groupPanel1";
            this.groupPanel1.Size = new Size(0x25b, 110);
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
            this.groupPanel1.TabIndex = 7;
            this.groupPanel1.Text = "Add profile";
            this.BtnUpload.AccessibleRole = AccessibleRole.PushButton;
            this.BtnUpload.ColorTable = eButtonColor.OrangeWithBackground;
            this.BtnUpload.Location = new Point(0x20c, 60);
            this.BtnUpload.Name = "BtnUpload";
            this.BtnUpload.Size = new Size(0x48, 0x17);
            this.BtnUpload.Style = eDotNetBarStyle.StyleManagerControlled;
            this.BtnUpload.TabIndex = 0x12;
            this.BtnUpload.Text = "Upload";
            this.BtnUpload.Click += new EventHandler(this.BtnUploadClick);
            this.TBName.Border.Class = "TextBoxBorder";
            this.TBName.Border.CornerType = eCornerType.Square;
            this.TBName.Location = new Point(0x4e, 0x23);
            this.TBName.Name = "TBName";
            this.TBName.Size = new Size(0xa5, 20);
            this.TBName.TabIndex = 13;
            this.labelX4.BackColor = Color.Transparent;
            this.labelX4.BackgroundStyle.Class = "";
            this.labelX4.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX4.Location = new Point(3, 0x1f);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new Size(40, 0x1a);
            this.labelX4.TabIndex = 12;
            this.labelX4.Text = "Name:";
            this.TBComment.Border.Class = "TextBoxBorder";
            this.TBComment.Border.CornerType = eCornerType.Square;
            this.TBComment.Location = new Point(0x4e, 0x3f);
            this.TBComment.Name = "TBComment";
            this.TBComment.Size = new Size(0x18b, 20);
            this.TBComment.TabIndex = 11;
            this.labelX3.BackColor = Color.Transparent;
            this.labelX3.BackgroundStyle.Class = "";
            this.labelX3.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX3.Location = new Point(3, 60);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new Size(0x45, 0x17);
            this.labelX3.TabIndex = 10;
            this.labelX3.Text = "Comment:";
            this.TBZone.Border.Class = "TextBoxBorder";
            this.TBZone.Border.CornerType = eCornerType.Square;
            this.TBZone.Location = new Point(0x12e, 0x23);
            this.TBZone.Name = "TBZone";
            this.TBZone.Size = new Size(0xab, 20);
            this.TBZone.TabIndex = 9;
            this.labelX2.BackColor = Color.Transparent;
            this.labelX2.BackgroundStyle.Class = "";
            this.labelX2.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX2.Location = new Point(0xfc, 0x21);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new Size(0x30, 0x17);
            this.labelX2.TabIndex = 8;
            this.labelX2.Text = "Zone(s):";
            this.BtnBrowse.AccessibleRole = AccessibleRole.PushButton;
            this.BtnBrowse.ColorTable = eButtonColor.OrangeWithBackground;
            this.BtnBrowse.Location = new Point(0x1df, 4);
            this.BtnBrowse.Name = "BtnBrowse";
            this.BtnBrowse.Size = new Size(0x4b, 0x17);
            this.BtnBrowse.Style = eDotNetBarStyle.StyleManagerControlled;
            this.BtnBrowse.TabIndex = 7;
            this.BtnBrowse.Text = "Browse";
            this.BtnBrowse.Click += new EventHandler(this.BtnBrowseClick);
            this.TBProfile.Border.Class = "TextBoxBorder";
            this.TBProfile.Border.CornerType = eCornerType.Square;
            this.TBProfile.Enabled = false;
            this.TBProfile.Location = new Point(0x4e, 7);
            this.TBProfile.Name = "TBProfile";
            this.TBProfile.Size = new Size(0x18b, 20);
            this.TBProfile.TabIndex = 1;
            this.labelX1.BackColor = Color.Transparent;
            this.labelX1.BackgroundStyle.Class = "";
            this.labelX1.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX1.Location = new Point(3, 3);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new Size(40, 0x1a);
            this.labelX1.TabIndex = 0;
            this.labelX1.Text = "Profile:";
            this.TBImage.Border.Class = "TextBoxBorder";
            this.TBImage.Border.CornerType = eCornerType.Square;
            this.TBImage.Enabled = false;
            this.TBImage.Location = new Point(0x228, 0xc4);
            this.TBImage.Name = "TBImage";
            this.TBImage.Size = new Size(0x1c, 20);
            this.TBImage.TabIndex = 0x11;
            this.TBImage.Visible = false;
            this.BtnBrowseImage.AccessibleRole = AccessibleRole.PushButton;
            this.BtnBrowseImage.ColorTable = eButtonColor.OrangeWithBackground;
            this.BtnBrowseImage.Location = new Point(0x274, 0xc1);
            this.BtnBrowseImage.Name = "BtnBrowseImage";
            this.BtnBrowseImage.Size = new Size(0x4b, 0x17);
            this.BtnBrowseImage.Style = eDotNetBarStyle.StyleManagerControlled;
            this.BtnBrowseImage.TabIndex = 0x10;
            this.BtnBrowseImage.Text = "Browse";
            this.BtnBrowseImage.Visible = false;
            this.BtnBrowseImage.Click += new EventHandler(this.BtnBrowseImageClick);
            this.labelX5.BackColor = Color.Transparent;
            this.labelX5.BackgroundStyle.Class = "";
            this.labelX5.BackgroundStyle.CornerType = eCornerType.Square;
            this.labelX5.Location = new Point(0x24a, 190);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new Size(40, 0x1a);
            this.labelX5.TabIndex = 14;
            this.labelX5.Text = "Image:";
            this.labelX5.Visible = false;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.FromArgb(0xdf, 0xe9, 0xf5);
            base.ClientSize = new Size(0x335, 0x14e);
            base.Controls.Add(this.groupPanel1);
            base.Controls.Add(this.TBImage);
            base.Controls.Add(this.BtnDownload);
            base.Controls.Add(this.BtnBrowseImage);
            base.Controls.Add(this.BtnRefresh);
            base.Controls.Add(this.labelX5);
            base.Controls.Add(this.ProfileList);
            this.DoubleBuffered = true;
            this.MaximumSize = new Size(0x345, 0x174);
            base.Name = "GatherProfiles";
            this.Text = "Flying profiles";
            ((ISupportInitialize) this.ProfileList).EndInit();
            this.groupPanel1.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        private void InvokeProfileDownload(EProfileDownloaded e)
        {
            EventHandler<EProfileDownloaded> profileDownload = this.ProfileDownload;
            if (profileDownload != null)
            {
                profileDownload(null, e);
            }
        }

        private void RefreshList()
        {
            try
            {
                this.BtnRefresh.Enabled = false;
                this.Refresh();
                StreamReader reader = new StreamReader(WebRequest.Create($"http://profiles.wow-lazybot.com/listflying.php?username={LazySettings.UserName}&password={LazySettings.Password}").GetResponse().GetResponseStream(), Encoding.UTF8);
                XmlDocument document = new XmlDocument();
                document.LoadXml(reader.ReadToEnd().Replace("\r", ""));
                reader.Close();
                List<OnlineProfile> list2 = new List<OnlineProfile>();
                foreach (XmlNode node in document.GetElementsByTagName("Profile"))
                {
                    string id = "";
                    string creator = "";
                    string name = "";
                    string zone = "";
                    string comment = "";
                    string downloads = "";
                    foreach (XmlNode node2 in node.ChildNodes)
                    {
                        string str9 = node2.Name;
                        if (str9 != null)
                        {
                            if (str9 == "id")
                            {
                                id = node2.InnerText;
                                continue;
                            }
                            if (str9 == "creator")
                            {
                                creator = node2.InnerText;
                                continue;
                            }
                            if (str9 == "name")
                            {
                                name = node2.InnerText;
                                continue;
                            }
                            if (str9 == "zone")
                            {
                                zone = node2.InnerText;
                                continue;
                            }
                            if (str9 == "comment")
                            {
                                comment = node2.InnerText;
                                continue;
                            }
                            if (str9 == "downloads")
                            {
                                downloads = node2.InnerText;
                            }
                        }
                    }
                    list2.Add(new OnlineProfile(id, name, creator, zone, comment, downloads));
                }
                this.ProfileList.DataSource = list2;
                this.ProfileList.Columns[0].Visible = false;
                this.BtnRefresh.Enabled = true;
            }
            catch (Exception exception)
            {
                this.BtnRefresh.Enabled = false;
                MessageBox.Show("Could not refresh the list !\n" + exception);
            }
        }

        private bool ValidateProfile(string fileName)
        {
            XmlDocument document = new XmlDocument();
            try
            {
                document.Load(fileName);
            }
            catch (Exception exception)
            {
                LazyLib.Logging.Write("FileNotFoundError: " + exception, new object[0]);
                return false;
            }
            return ((document.GetElementsByTagName("Profile")[0] != null) ? (document.GetElementsByTagName("Waypoint")[0] != null) : false);
        }
    }
}

