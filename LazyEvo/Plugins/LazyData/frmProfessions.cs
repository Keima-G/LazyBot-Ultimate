namespace LazyEvo.Plugins.LazyData
{
    using LazyEvo.Plugins.ExtraLazy;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    public class frmProfessions : Form
    {
        private LazyEvo.Plugins.ExtraLazy.Professions _professions;
        private string _blueChat;
        private IContainer components;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Label label1;
        private Label label10;
        private Label label11;
        private Label label12;
        private Label label13;
        private Label label14;
        private Label label15;
        private Label label16;
        private GroupBox groupBox3;
        private Label label17;
        private Label label18;
        private Label label19;
        private Label label20;
        private Label label21;
        private Label label22;
        private Label label23;
        private Label label24;
        private GroupBox groupBox4;
        private Label label25;
        private Label label26;
        private Label label27;
        private Label label28;
        private Label label29;
        private Label label30;
        private Label label31;
        private Label label32;
        private GroupBox groupBox5;
        private Label label33;
        private Label label34;
        private Label label35;
        private Label label36;
        private Label label37;
        private Label label38;
        private Label label39;
        private Label label40;
        private GroupBox groupBox6;
        private Label label41;
        private Label label42;
        private Label label43;
        private Label label44;
        private Label label45;
        private Label label46;
        private Label label47;
        private Label label48;
        private Label label49;
        private Button button1;
        private Button button2;
        private Button button3;
        private TextBox txtBoxChat;

        public frmProfessions()
        {
            this.InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            base.Hide();
            Loader.stopUpdating();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.label49.Visible = true;
            Loader.getProfessions();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FrameViewer viewer1 = new FrameViewer();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
        }

        public void createDisplay()
        {
            if (base.InvokeRequired)
            {
                base.BeginInvoke(new CreateDisplay(this.createDisplay));
            }
            else
            {
                Profession profession;
                if (this._professions.Primary1 == null)
                {
                    this.label3.Text = "";
                    this.label5.Text = "";
                    this.label7.Text = "";
                    this.label9.Text = "";
                }
                else
                {
                    profession = this._professions.Primary1;
                    this.label3.Text = profession.Name;
                    this.label5.Text = profession.Rank.RankText;
                    this.label7.Text = profession.Level.ToString();
                    this.label9.Text = profession.Rank.MaxLevel.ToString();
                }
                if (this._professions.Primary2 == null)
                {
                    this.label13.Text = "";
                    this.label1.Text = "";
                    this.label14.Text = "";
                    this.label10.Text = "";
                }
                else
                {
                    profession = this._professions.Primary2;
                    this.label13.Text = profession.Name;
                    this.label1.Text = profession.Rank.RankText;
                    this.label14.Text = profession.Level.ToString();
                    this.label10.Text = profession.Rank.MaxLevel.ToString();
                }
                if (this._professions.Secondary1 == null)
                {
                    this.label21.Text = "";
                    this.label17.Text = "";
                    this.label22.Text = "";
                    this.label18.Text = "";
                }
                else
                {
                    profession = this._professions.Secondary1;
                    this.label21.Text = profession.Name;
                    this.label17.Text = profession.Rank.RankText;
                    this.label22.Text = profession.Level.ToString();
                    this.label18.Text = profession.Rank.MaxLevel.ToString();
                }
                if (this._professions.Secondary2 == null)
                {
                    this.label29.Text = "";
                    this.label25.Text = "";
                    this.label30.Text = "";
                    this.label26.Text = "";
                }
                else
                {
                    profession = this._professions.Secondary2;
                    this.label29.Text = profession.Name;
                    this.label25.Text = profession.Rank.RankText;
                    this.label30.Text = profession.Level.ToString();
                    this.label26.Text = profession.Rank.MaxLevel.ToString();
                }
                if (this._professions.Secondary3 == null)
                {
                    this.label37.Text = "";
                    this.label33.Text = "";
                    this.label38.Text = "";
                    this.label34.Text = "";
                }
                else
                {
                    profession = this._professions.Secondary3;
                    this.label37.Text = profession.Name;
                    this.label33.Text = profession.Rank.RankText;
                    this.label38.Text = profession.Level.ToString();
                    this.label34.Text = profession.Rank.MaxLevel.ToString();
                }
                if (this._professions.Secondary4 == null)
                {
                    this.label45.Text = "";
                    this.label41.Text = "";
                    this.label46.Text = "";
                    this.label42.Text = "";
                }
                else
                {
                    profession = this._professions.Secondary4;
                    this.label45.Text = profession.Name;
                    this.label41.Text = profession.Rank.RankText;
                    this.label46.Text = profession.Level.ToString();
                    this.label42.Text = profession.Rank.MaxLevel.ToString();
                }
                this.label49.Visible = false;
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
            this.label2 = new Label();
            this.label3 = new Label();
            this.label4 = new Label();
            this.label5 = new Label();
            this.label6 = new Label();
            this.label7 = new Label();
            this.label8 = new Label();
            this.label9 = new Label();
            this.groupBox1 = new GroupBox();
            this.groupBox2 = new GroupBox();
            this.label1 = new Label();
            this.label10 = new Label();
            this.label11 = new Label();
            this.label12 = new Label();
            this.label13 = new Label();
            this.label14 = new Label();
            this.label15 = new Label();
            this.label16 = new Label();
            this.groupBox3 = new GroupBox();
            this.label17 = new Label();
            this.label18 = new Label();
            this.label19 = new Label();
            this.label20 = new Label();
            this.label21 = new Label();
            this.label22 = new Label();
            this.label23 = new Label();
            this.label24 = new Label();
            this.groupBox4 = new GroupBox();
            this.label25 = new Label();
            this.label26 = new Label();
            this.label27 = new Label();
            this.label28 = new Label();
            this.label29 = new Label();
            this.label30 = new Label();
            this.label31 = new Label();
            this.label32 = new Label();
            this.groupBox5 = new GroupBox();
            this.label33 = new Label();
            this.label34 = new Label();
            this.label35 = new Label();
            this.label36 = new Label();
            this.label37 = new Label();
            this.label38 = new Label();
            this.label39 = new Label();
            this.label40 = new Label();
            this.groupBox6 = new GroupBox();
            this.label41 = new Label();
            this.label42 = new Label();
            this.label43 = new Label();
            this.label44 = new Label();
            this.label45 = new Label();
            this.label46 = new Label();
            this.label47 = new Label();
            this.label48 = new Label();
            this.label49 = new Label();
            this.button1 = new Button();
            this.button2 = new Button();
            this.button3 = new Button();
            this.txtBoxChat = new TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            base.SuspendLayout();
            this.label2.AutoSize = true;
            this.label2.Location = new Point(11, 0x10);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x27, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Name";
            this.label2.Click += new EventHandler(this.label2_Click);
            this.label3.AutoSize = true;
            this.label3.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.label3.Location = new Point(100, 0x10);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0, 13);
            this.label3.TabIndex = 2;
            this.label4.AutoSize = true;
            this.label4.Location = new Point(0x9f, 0x10);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x25, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Rank";
            this.label5.AutoSize = true;
            this.label5.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.label5.Location = new Point(260, 0x10);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0, 13);
            this.label5.TabIndex = 4;
            this.label6.AutoSize = true;
            this.label6.Location = new Point(11, 0x26);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x53, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Current Level";
            this.label7.AutoSize = true;
            this.label7.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.label7.Location = new Point(100, 0x26);
            this.label7.Name = "label7";
            this.label7.Size = new Size(0, 13);
            this.label7.TabIndex = 6;
            this.label8.AutoSize = true;
            this.label8.Location = new Point(0x9f, 0x26);
            this.label8.Name = "label8";
            this.label8.Size = new Size(0x5d, 13);
            this.label8.TabIndex = 7;
            this.label8.Text = "Maximum Level";
            this.label9.AutoSize = true;
            this.label9.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.label9.Location = new Point(260, 0x26);
            this.label9.Name = "label9";
            this.label9.Size = new Size(0, 13);
            this.label9.TabIndex = 8;
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.groupBox1.Location = new Point(12, 0x1f);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x143, 0x3e);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Primary Profession 1";
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Controls.Add(this.label16);
            this.groupBox2.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.groupBox2.Location = new Point(12, 0x63);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(0x143, 0x3e);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Primary Profession 2";
            this.label1.AutoSize = true;
            this.label1.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.label1.Location = new Point(260, 0x10);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0, 13);
            this.label1.TabIndex = 4;
            this.label10.AutoSize = true;
            this.label10.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.label10.Location = new Point(260, 0x26);
            this.label10.Name = "label10";
            this.label10.Size = new Size(0, 13);
            this.label10.TabIndex = 8;
            this.label11.AutoSize = true;
            this.label11.Location = new Point(11, 0x10);
            this.label11.Name = "label11";
            this.label11.Size = new Size(0x27, 13);
            this.label11.TabIndex = 1;
            this.label11.Text = "Name";
            this.label12.AutoSize = true;
            this.label12.Location = new Point(0x9f, 0x26);
            this.label12.Name = "label12";
            this.label12.Size = new Size(0x5d, 13);
            this.label12.TabIndex = 7;
            this.label12.Text = "Maximum Level";
            this.label13.AutoSize = true;
            this.label13.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.label13.Location = new Point(100, 0x10);
            this.label13.Name = "label13";
            this.label13.Size = new Size(0, 13);
            this.label13.TabIndex = 2;
            this.label14.AutoSize = true;
            this.label14.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.label14.Location = new Point(100, 0x26);
            this.label14.Name = "label14";
            this.label14.Size = new Size(0, 13);
            this.label14.TabIndex = 6;
            this.label15.AutoSize = true;
            this.label15.Location = new Point(0x9f, 0x10);
            this.label15.Name = "label15";
            this.label15.Size = new Size(0x25, 13);
            this.label15.TabIndex = 3;
            this.label15.Text = "Rank";
            this.label16.AutoSize = true;
            this.label16.Location = new Point(11, 0x26);
            this.label16.Name = "label16";
            this.label16.Size = new Size(0x53, 13);
            this.label16.TabIndex = 5;
            this.label16.Text = "Current Level";
            this.groupBox3.Controls.Add(this.label17);
            this.groupBox3.Controls.Add(this.label18);
            this.groupBox3.Controls.Add(this.label19);
            this.groupBox3.Controls.Add(this.label20);
            this.groupBox3.Controls.Add(this.label21);
            this.groupBox3.Controls.Add(this.label22);
            this.groupBox3.Controls.Add(this.label23);
            this.groupBox3.Controls.Add(this.label24);
            this.groupBox3.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.groupBox3.Location = new Point(12, 0xa7);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new Size(0x143, 0x3e);
            this.groupBox3.TabIndex = 11;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Secondary Profession 1";
            this.label17.AutoSize = true;
            this.label17.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.label17.Location = new Point(260, 0x10);
            this.label17.Name = "label17";
            this.label17.Size = new Size(0, 13);
            this.label17.TabIndex = 4;
            this.label18.AutoSize = true;
            this.label18.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.label18.Location = new Point(260, 0x26);
            this.label18.Name = "label18";
            this.label18.Size = new Size(0, 13);
            this.label18.TabIndex = 8;
            this.label19.AutoSize = true;
            this.label19.Location = new Point(11, 0x10);
            this.label19.Name = "label19";
            this.label19.Size = new Size(0x27, 13);
            this.label19.TabIndex = 1;
            this.label19.Text = "Name";
            this.label20.AutoSize = true;
            this.label20.Location = new Point(0x9f, 0x26);
            this.label20.Name = "label20";
            this.label20.Size = new Size(0x5d, 13);
            this.label20.TabIndex = 7;
            this.label20.Text = "Maximum Level";
            this.label21.AutoSize = true;
            this.label21.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.label21.Location = new Point(100, 0x10);
            this.label21.Name = "label21";
            this.label21.Size = new Size(0, 13);
            this.label21.TabIndex = 2;
            this.label22.AutoSize = true;
            this.label22.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.label22.Location = new Point(100, 0x26);
            this.label22.Name = "label22";
            this.label22.Size = new Size(0, 13);
            this.label22.TabIndex = 6;
            this.label23.AutoSize = true;
            this.label23.Location = new Point(0x9f, 0x10);
            this.label23.Name = "label23";
            this.label23.Size = new Size(0x25, 13);
            this.label23.TabIndex = 3;
            this.label23.Text = "Rank";
            this.label24.AutoSize = true;
            this.label24.Location = new Point(11, 0x26);
            this.label24.Name = "label24";
            this.label24.Size = new Size(0x53, 13);
            this.label24.TabIndex = 5;
            this.label24.Text = "Current Level";
            this.groupBox4.Controls.Add(this.label25);
            this.groupBox4.Controls.Add(this.label26);
            this.groupBox4.Controls.Add(this.label27);
            this.groupBox4.Controls.Add(this.label28);
            this.groupBox4.Controls.Add(this.label29);
            this.groupBox4.Controls.Add(this.label30);
            this.groupBox4.Controls.Add(this.label31);
            this.groupBox4.Controls.Add(this.label32);
            this.groupBox4.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.groupBox4.Location = new Point(12, 0xeb);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new Size(0x143, 0x3e);
            this.groupBox4.TabIndex = 11;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Secondary Profession 2";
            this.label25.AutoSize = true;
            this.label25.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.label25.Location = new Point(260, 0x10);
            this.label25.Name = "label25";
            this.label25.Size = new Size(0, 13);
            this.label25.TabIndex = 4;
            this.label26.AutoSize = true;
            this.label26.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.label26.Location = new Point(260, 0x26);
            this.label26.Name = "label26";
            this.label26.Size = new Size(0, 13);
            this.label26.TabIndex = 8;
            this.label27.AutoSize = true;
            this.label27.Location = new Point(11, 0x10);
            this.label27.Name = "label27";
            this.label27.Size = new Size(0x27, 13);
            this.label27.TabIndex = 1;
            this.label27.Text = "Name";
            this.label28.AutoSize = true;
            this.label28.Location = new Point(0x9f, 0x26);
            this.label28.Name = "label28";
            this.label28.Size = new Size(0x5d, 13);
            this.label28.TabIndex = 7;
            this.label28.Text = "Maximum Level";
            this.label29.AutoSize = true;
            this.label29.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.label29.Location = new Point(100, 0x10);
            this.label29.Name = "label29";
            this.label29.Size = new Size(0, 13);
            this.label29.TabIndex = 2;
            this.label30.AutoSize = true;
            this.label30.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.label30.Location = new Point(100, 0x26);
            this.label30.Name = "label30";
            this.label30.Size = new Size(0, 13);
            this.label30.TabIndex = 6;
            this.label31.AutoSize = true;
            this.label31.Location = new Point(0x9f, 0x10);
            this.label31.Name = "label31";
            this.label31.Size = new Size(0x25, 13);
            this.label31.TabIndex = 3;
            this.label31.Text = "Rank";
            this.label32.AutoSize = true;
            this.label32.Location = new Point(11, 0x26);
            this.label32.Name = "label32";
            this.label32.Size = new Size(0x53, 13);
            this.label32.TabIndex = 5;
            this.label32.Text = "Current Level";
            this.groupBox5.Controls.Add(this.label33);
            this.groupBox5.Controls.Add(this.label34);
            this.groupBox5.Controls.Add(this.label35);
            this.groupBox5.Controls.Add(this.label36);
            this.groupBox5.Controls.Add(this.label37);
            this.groupBox5.Controls.Add(this.label38);
            this.groupBox5.Controls.Add(this.label39);
            this.groupBox5.Controls.Add(this.label40);
            this.groupBox5.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.groupBox5.Location = new Point(12, 0x12f);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new Size(0x143, 0x3e);
            this.groupBox5.TabIndex = 11;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Secondary Profession 3";
            this.label33.AutoSize = true;
            this.label33.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.label33.Location = new Point(260, 0x10);
            this.label33.Name = "label33";
            this.label33.Size = new Size(0, 13);
            this.label33.TabIndex = 4;
            this.label34.AutoSize = true;
            this.label34.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.label34.Location = new Point(260, 0x26);
            this.label34.Name = "label34";
            this.label34.Size = new Size(0, 13);
            this.label34.TabIndex = 8;
            this.label35.AutoSize = true;
            this.label35.Location = new Point(11, 0x10);
            this.label35.Name = "label35";
            this.label35.Size = new Size(0x27, 13);
            this.label35.TabIndex = 1;
            this.label35.Text = "Name";
            this.label36.AutoSize = true;
            this.label36.Location = new Point(0x9f, 0x26);
            this.label36.Name = "label36";
            this.label36.Size = new Size(0x5d, 13);
            this.label36.TabIndex = 7;
            this.label36.Text = "Maximum Level";
            this.label37.AutoSize = true;
            this.label37.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.label37.Location = new Point(100, 0x10);
            this.label37.Name = "label37";
            this.label37.Size = new Size(0, 13);
            this.label37.TabIndex = 2;
            this.label38.AutoSize = true;
            this.label38.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.label38.Location = new Point(100, 0x26);
            this.label38.Name = "label38";
            this.label38.Size = new Size(0, 13);
            this.label38.TabIndex = 6;
            this.label39.AutoSize = true;
            this.label39.Location = new Point(0x9f, 0x10);
            this.label39.Name = "label39";
            this.label39.Size = new Size(0x25, 13);
            this.label39.TabIndex = 3;
            this.label39.Text = "Rank";
            this.label40.AutoSize = true;
            this.label40.Location = new Point(11, 0x26);
            this.label40.Name = "label40";
            this.label40.Size = new Size(0x53, 13);
            this.label40.TabIndex = 5;
            this.label40.Text = "Current Level";
            this.groupBox6.Controls.Add(this.label41);
            this.groupBox6.Controls.Add(this.label42);
            this.groupBox6.Controls.Add(this.label43);
            this.groupBox6.Controls.Add(this.label44);
            this.groupBox6.Controls.Add(this.label45);
            this.groupBox6.Controls.Add(this.label46);
            this.groupBox6.Controls.Add(this.label47);
            this.groupBox6.Controls.Add(this.label48);
            this.groupBox6.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.groupBox6.Location = new Point(12, 0x173);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new Size(0x143, 0x3e);
            this.groupBox6.TabIndex = 11;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Secondary Profession 4";
            this.label41.AutoSize = true;
            this.label41.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.label41.Location = new Point(260, 0x10);
            this.label41.Name = "label41";
            this.label41.Size = new Size(0, 13);
            this.label41.TabIndex = 4;
            this.label42.AutoSize = true;
            this.label42.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.label42.Location = new Point(260, 0x26);
            this.label42.Name = "label42";
            this.label42.Size = new Size(0, 13);
            this.label42.TabIndex = 8;
            this.label43.AutoSize = true;
            this.label43.Location = new Point(11, 0x10);
            this.label43.Name = "label43";
            this.label43.Size = new Size(0x27, 13);
            this.label43.TabIndex = 1;
            this.label43.Text = "Name";
            this.label44.AutoSize = true;
            this.label44.Location = new Point(0x9f, 0x26);
            this.label44.Name = "label44";
            this.label44.Size = new Size(0x5d, 13);
            this.label44.TabIndex = 7;
            this.label44.Text = "Maximum Level";
            this.label45.AutoSize = true;
            this.label45.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.label45.Location = new Point(100, 0x10);
            this.label45.Name = "label45";
            this.label45.Size = new Size(0, 13);
            this.label45.TabIndex = 2;
            this.label46.AutoSize = true;
            this.label46.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.label46.Location = new Point(100, 0x26);
            this.label46.Name = "label46";
            this.label46.Size = new Size(0, 13);
            this.label46.TabIndex = 6;
            this.label47.AutoSize = true;
            this.label47.Location = new Point(0x9f, 0x10);
            this.label47.Name = "label47";
            this.label47.Size = new Size(0x25, 13);
            this.label47.TabIndex = 3;
            this.label47.Text = "Rank";
            this.label48.AutoSize = true;
            this.label48.Location = new Point(11, 0x26);
            this.label48.Name = "label48";
            this.label48.Size = new Size(0x53, 13);
            this.label48.TabIndex = 5;
            this.label48.Text = "Current Level";
            this.label49.AutoSize = true;
            this.label49.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.label49.ForeColor = Color.Red;
            this.label49.Location = new Point(12, 9);
            this.label49.Name = "label49";
            this.label49.Size = new Size(0x71, 13);
            this.label49.TabIndex = 12;
            this.label49.Text = "Awaiting Results...";
            this.label49.Visible = false;
            this.label49.Click += new EventHandler(this.label49_Click);
            this.button1.Location = new Point(0x103, 0x223);
            this.button1.Name = "button1";
            this.button1.Size = new Size(0x4b, 0x17);
            this.button1.TabIndex = 13;
            this.button1.Text = "Close";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new EventHandler(this.button1_Click);
            this.button2.Location = new Point(0xad, 0x223);
            this.button2.Name = "button2";
            this.button2.Size = new Size(0x4b, 0x17);
            this.button2.TabIndex = 14;
            this.button2.Text = "Refresh";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new EventHandler(this.button2_Click);
            this.button3.Location = new Point(11, 0x223);
            this.button3.Name = "button3";
            this.button3.Size = new Size(0x4b, 0x17);
            this.button3.TabIndex = 15;
            this.button3.Text = "View Frames";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Visible = false;
            this.button3.Click += new EventHandler(this.button3_Click);
            this.txtBoxChat.Location = new Point(12, 0x1b7);
            this.txtBoxChat.Multiline = true;
            this.txtBoxChat.Name = "txtBoxChat";
            this.txtBoxChat.Size = new Size(0x143, 60);
            this.txtBoxChat.TabIndex = 0x10;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x15a, 0x246);
            base.ControlBox = false;
            base.Controls.Add(this.txtBoxChat);
            base.Controls.Add(this.button3);
            base.Controls.Add(this.button2);
            base.Controls.Add(this.button1);
            base.Controls.Add(this.label49);
            base.Controls.Add(this.groupBox6);
            base.Controls.Add(this.groupBox5);
            base.Controls.Add(this.groupBox4);
            base.Controls.Add(this.groupBox3);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.Name = "frmProfessions";
            this.Text = "Professions Demo";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void label1_Click_1(object sender, EventArgs e)
        {
        }

        private void label1_Click_2(object sender, EventArgs e)
        {
        }

        private void label2_Click(object sender, EventArgs e)
        {
        }

        private void label49_Click(object sender, EventArgs e)
        {
        }

        public void setBlueChat()
        {
            if (base.InvokeRequired)
            {
                base.BeginInvoke(new SetBlueChat(this.setBlueChat));
            }
            else
            {
                this.txtBoxChat.Text = this._blueChat;
            }
        }

        public string BlueChat
        {
            get => 
                this._blueChat;
            set => 
                this._blueChat = value;
        }

        public LazyEvo.Plugins.ExtraLazy.Professions Professions
        {
            get => 
                this._professions;
            set => 
                this._professions = value;
        }

        private delegate void CreateDisplay();

        private delegate void SetBlueChat();
    }
}

