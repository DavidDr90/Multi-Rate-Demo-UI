namespace App
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series6 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series7 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series8 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.PnlNav = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.xSymboleButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.fileTransferPicture = new System.Windows.Forms.PictureBox();
            this.highBandDrop = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.lowBandDrop = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cellularDrop = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.iperfCmdWiFi1 = new System.Windows.Forms.TextBox();
            this.iperfCmdWiFi2 = new System.Windows.Forms.TextBox();
            this.iperfCmdCellular = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.runIperButton = new System.Windows.Forms.Button();
            this.settingsPanel = new System.Windows.Forms.Panel();
            this.releaseCheckBox = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.profileName = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.numbersPanel = new System.Windows.Forms.Panel();
            this.profileButton = new System.Windows.Forms.Button();
            this.wifiButtom = new System.Windows.Forms.Button();
            this.btnStartDemo = new System.Windows.Forms.Button();
            this.label15 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.aggregateLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.cellularLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.lowBandLabel = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.highBandLabel = new System.Windows.Forms.Label();
            this.graphPanel = new System.Windows.Forms.Panel();
            this.mainChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.panel1 = new System.Windows.Forms.Panel();
            this.BtnSettings = new System.Windows.Forms.Button();
            this.BtnView2 = new System.Windows.Forms.Button();
            this.BtnView1 = new System.Windows.Forms.Button();
            this.startMonitorButton = new System.Windows.Forms.Button();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fileTransferPicture)).BeginInit();
            this.settingsPanel.SuspendLayout();
            this.numbersPanel.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.graphPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainChart)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // PnlNav
            // 
            this.PnlNav.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(126)))), ((int)(((byte)(249)))));
            this.PnlNav.Location = new System.Drawing.Point(0, 193);
            this.PnlNav.Name = "PnlNav";
            this.PnlNav.Size = new System.Drawing.Size(3, 100);
            this.PnlNav.TabIndex = 1;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.pictureBox1);
            this.panel6.Controls.Add(this.xSymboleButton);
            this.panel6.Controls.Add(this.label1);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel6.Location = new System.Drawing.Point(0, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(976, 144);
            this.panel6.TabIndex = 4;
            this.panel6.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.panel6.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBox1.Image = global::App.Properties.Resources.intel_logo;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(259, 144);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 13;
            this.pictureBox1.TabStop = false;
            // 
            // xSymboleButton
            // 
            this.xSymboleButton.FlatAppearance.BorderSize = 0;
            this.xSymboleButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xSymboleButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xSymboleButton.ForeColor = System.Drawing.Color.White;
            this.xSymboleButton.Location = new System.Drawing.Point(906, 52);
            this.xSymboleButton.Name = "xSymboleButton";
            this.xSymboleButton.Size = new System.Drawing.Size(40, 40);
            this.xSymboleButton.TabIndex = 12;
            this.xSymboleButton.Text = "X";
            this.xSymboleButton.UseVisualStyleBackColor = true;
            this.xSymboleButton.Click += new System.EventHandler(this.xSymboleButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(161)))), ((int)(((byte)(176)))));
            this.label1.Location = new System.Drawing.Point(379, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(304, 42);
            this.label1.TabIndex = 11;
            this.label1.Text = "Multi RAT Demo";
            // 
            // fileTransferPicture
            // 
            this.fileTransferPicture.Image = global::App.Properties.Resources.ezgif_com_gif_maker;
            this.fileTransferPicture.Location = new System.Drawing.Point(12, 785);
            this.fileTransferPicture.Name = "fileTransferPicture";
            this.fileTransferPicture.Size = new System.Drawing.Size(272, 60);
            this.fileTransferPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.fileTransferPicture.TabIndex = 14;
            this.fileTransferPicture.TabStop = false;
            this.fileTransferPicture.Visible = false;
            // 
            // highBandDrop
            // 
            this.highBandDrop.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.highBandDrop.FormattingEnabled = true;
            this.highBandDrop.Location = new System.Drawing.Point(312, 69);
            this.highBandDrop.Name = "highBandDrop";
            this.highBandDrop.Size = new System.Drawing.Size(381, 21);
            this.highBandDrop.TabIndex = 30;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(161)))), ((int)(((byte)(176)))));
            this.label5.Location = new System.Drawing.Point(120, 66);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(108, 24);
            this.label5.TabIndex = 33;
            this.label5.Text = "High Band";
            // 
            // lowBandDrop
            // 
            this.lowBandDrop.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.lowBandDrop.FormattingEnabled = true;
            this.lowBandDrop.Location = new System.Drawing.Point(312, 109);
            this.lowBandDrop.Name = "lowBandDrop";
            this.lowBandDrop.Size = new System.Drawing.Size(381, 21);
            this.lowBandDrop.TabIndex = 29;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(161)))), ((int)(((byte)(176)))));
            this.label6.Location = new System.Drawing.Point(120, 106);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(102, 24);
            this.label6.TabIndex = 32;
            this.label6.Text = "Low Band";
            // 
            // cellularDrop
            // 
            this.cellularDrop.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cellularDrop.FormattingEnabled = true;
            this.cellularDrop.Location = new System.Drawing.Point(312, 148);
            this.cellularDrop.Name = "cellularDrop";
            this.cellularDrop.Size = new System.Drawing.Size(381, 21);
            this.cellularDrop.TabIndex = 28;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(161)))), ((int)(((byte)(176)))));
            this.label7.Location = new System.Drawing.Point(120, 145);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(81, 24);
            this.label7.TabIndex = 31;
            this.label7.Text = "Cellular";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(161)))), ((int)(((byte)(176)))));
            this.label9.Location = new System.Drawing.Point(17, 248);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(168, 24);
            this.label9.TabIndex = 31;
            this.label9.Text = "WiFi 1 Cmd 7000";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(161)))), ((int)(((byte)(176)))));
            this.label10.Location = new System.Drawing.Point(17, 284);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(168, 24);
            this.label10.TabIndex = 31;
            this.label10.Text = "WiFi 2 Cmd 8000";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(161)))), ((int)(((byte)(176)))));
            this.label11.Location = new System.Drawing.Point(17, 321);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(130, 24);
            this.label11.TabIndex = 31;
            this.label11.Text = "Cellular Cmd";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(161)))), ((int)(((byte)(176)))));
            this.label8.Location = new System.Drawing.Point(242, 18);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(224, 29);
            this.label8.TabIndex = 34;
            this.label8.Text = "Choose Interfaces";
            // 
            // iperfCmdWiFi1
            // 
            this.iperfCmdWiFi1.Location = new System.Drawing.Point(187, 253);
            this.iperfCmdWiFi1.Name = "iperfCmdWiFi1";
            this.iperfCmdWiFi1.Size = new System.Drawing.Size(612, 20);
            this.iperfCmdWiFi1.TabIndex = 35;
            this.iperfCmdWiFi1.Text = "C:\\Users\\itl\\Desktop\\iperf-3.1.3-win64\\iperf3-download.exe -c 192.168.239.37 -t 9" +
    "999 -p 7000 -i1 -B 192.168.239.49";
            // 
            // iperfCmdWiFi2
            // 
            this.iperfCmdWiFi2.Location = new System.Drawing.Point(187, 289);
            this.iperfCmdWiFi2.Name = "iperfCmdWiFi2";
            this.iperfCmdWiFi2.Size = new System.Drawing.Size(612, 20);
            this.iperfCmdWiFi2.TabIndex = 35;
            this.iperfCmdWiFi2.Text = "C:\\Users\\itl\\Desktop\\iperf-3.1.3-win64\\iperf3-download.exe -c 192.168.239.37 -t 9" +
    "999 -p 8000 -i1 -B 192.168.239.47";
            // 
            // iperfCmdCellular
            // 
            this.iperfCmdCellular.Location = new System.Drawing.Point(187, 326);
            this.iperfCmdCellular.Name = "iperfCmdCellular";
            this.iperfCmdCellular.Size = new System.Drawing.Size(612, 20);
            this.iperfCmdCellular.TabIndex = 35;
            this.iperfCmdCellular.Text = "C:\\Users\\itl\\Desktop\\iperf-3.1.3-win64\\iperf3-download.exe -c 172.28.106.166 -b 1" +
    "000m -t 9999 -i 1 -B 10.155.118.159 ";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(161)))), ((int)(((byte)(176)))));
            this.label12.Location = new System.Drawing.Point(242, 202);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(206, 29);
            this.label12.TabIndex = 36;
            this.label12.Text = "IPerf Commands";
            // 
            // runIperButton
            // 
            this.runIperButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(51)))), ((int)(((byte)(50)))));
            this.runIperButton.FlatAppearance.BorderSize = 0;
            this.runIperButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.runIperButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.runIperButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(126)))), ((int)(((byte)(249)))));
            this.runIperButton.Location = new System.Drawing.Point(633, 414);
            this.runIperButton.Name = "runIperButton";
            this.runIperButton.Size = new System.Drawing.Size(186, 45);
            this.runIperButton.TabIndex = 37;
            this.runIperButton.Text = "Run IPerf";
            this.runIperButton.UseVisualStyleBackColor = false;
            this.runIperButton.Click += new System.EventHandler(this.runIperButton_Click);
            // 
            // settingsPanel
            // 
            this.settingsPanel.Controls.Add(this.releaseCheckBox);
            this.settingsPanel.Controls.Add(this.startMonitorButton);
            this.settingsPanel.Controls.Add(this.button1);
            this.settingsPanel.Controls.Add(this.fileTransferPicture);
            this.settingsPanel.Controls.Add(this.label12);
            this.settingsPanel.Controls.Add(this.profileName);
            this.settingsPanel.Controls.Add(this.iperfCmdCellular);
            this.settingsPanel.Controls.Add(this.iperfCmdWiFi2);
            this.settingsPanel.Controls.Add(this.iperfCmdWiFi1);
            this.settingsPanel.Controls.Add(this.label8);
            this.settingsPanel.Controls.Add(this.label13);
            this.settingsPanel.Controls.Add(this.label11);
            this.settingsPanel.Controls.Add(this.label10);
            this.settingsPanel.Controls.Add(this.label9);
            this.settingsPanel.Controls.Add(this.label7);
            this.settingsPanel.Controls.Add(this.cellularDrop);
            this.settingsPanel.Controls.Add(this.label6);
            this.settingsPanel.Controls.Add(this.lowBandDrop);
            this.settingsPanel.Controls.Add(this.label5);
            this.settingsPanel.Controls.Add(this.highBandDrop);
            this.settingsPanel.Location = new System.Drawing.Point(87, 144);
            this.settingsPanel.Name = "settingsPanel";
            this.settingsPanel.Size = new System.Drawing.Size(859, 477);
            this.settingsPanel.TabIndex = 15;
            this.settingsPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.settingsPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            // 
            // releaseCheckBox
            // 
            this.releaseCheckBox.AutoSize = true;
            this.releaseCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.releaseCheckBox.ForeColor = System.Drawing.Color.White;
            this.releaseCheckBox.Location = new System.Drawing.Point(33, 426);
            this.releaseCheckBox.Name = "releaseCheckBox";
            this.releaseCheckBox.Size = new System.Drawing.Size(252, 37);
            this.releaseCheckBox.TabIndex = 38;
            this.releaseCheckBox.Text = "Release/Renew";
            this.releaseCheckBox.UseVisualStyleBackColor = true;
            this.releaseCheckBox.CheckedChanged += new System.EventHandler(this.releaseCheckBox_CheckedChanged);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(51)))), ((int)(((byte)(50)))));
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(126)))), ((int)(((byte)(249)))));
            this.button1.Location = new System.Drawing.Point(613, 401);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(186, 45);
            this.button1.TabIndex = 37;
            this.button1.Text = "Run IPerf";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.runIperButton_Click);
            // 
            // profileName
            // 
            this.profileName.Location = new System.Drawing.Point(187, 391);
            this.profileName.Name = "profileName";
            this.profileName.Size = new System.Drawing.Size(107, 20);
            this.profileName.TabIndex = 35;
            this.profileName.Text = "DEMO_MRAT";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(161)))), ((int)(((byte)(176)))));
            this.label13.Location = new System.Drawing.Point(19, 391);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(130, 24);
            this.label13.TabIndex = 31;
            this.label13.Text = "Profile Name";
            // 
            // numbersPanel
            // 
            this.numbersPanel.Controls.Add(this.profileButton);
            this.numbersPanel.Controls.Add(this.wifiButtom);
            this.numbersPanel.Controls.Add(this.btnStartDemo);
            this.numbersPanel.Controls.Add(this.label15);
            this.numbersPanel.Controls.Add(this.label4);
            this.numbersPanel.Controls.Add(this.panel2);
            this.numbersPanel.Controls.Add(this.label3);
            this.numbersPanel.Controls.Add(this.panel5);
            this.numbersPanel.Controls.Add(this.label2);
            this.numbersPanel.Controls.Add(this.panel4);
            this.numbersPanel.Controls.Add(this.panel3);
            this.numbersPanel.Location = new System.Drawing.Point(87, 150);
            this.numbersPanel.Name = "numbersPanel";
            this.numbersPanel.Size = new System.Drawing.Size(889, 527);
            this.numbersPanel.TabIndex = 19;
            this.numbersPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.numbersPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            // 
            // profileButton
            // 
            this.profileButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(51)))), ((int)(((byte)(50)))));
            this.profileButton.FlatAppearance.BorderSize = 0;
            this.profileButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.profileButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.profileButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(126)))), ((int)(((byte)(249)))));
            this.profileButton.Location = new System.Drawing.Point(626, 392);
            this.profileButton.Name = "profileButton";
            this.profileButton.Size = new System.Drawing.Size(196, 45);
            this.profileButton.TabIndex = 16;
            this.profileButton.Text = "Disconnect Profile";
            this.profileButton.UseVisualStyleBackColor = false;
            this.profileButton.Click += new System.EventHandler(this.profileButton_Click);
            // 
            // wifiButtom
            // 
            this.wifiButtom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(51)))), ((int)(((byte)(50)))));
            this.wifiButtom.FlatAppearance.BorderSize = 0;
            this.wifiButtom.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.wifiButtom.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.wifiButtom.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(126)))), ((int)(((byte)(249)))));
            this.wifiButtom.Location = new System.Drawing.Point(626, 463);
            this.wifiButtom.Name = "wifiButtom";
            this.wifiButtom.Size = new System.Drawing.Size(196, 45);
            this.wifiButtom.TabIndex = 16;
            this.wifiButtom.Text = "Disconnect WiFi";
            this.wifiButtom.UseVisualStyleBackColor = false;
            this.wifiButtom.Click += new System.EventHandler(this.wifiButtom_Click);
            // 
            // btnStartDemo
            // 
            this.btnStartDemo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(51)))), ((int)(((byte)(50)))));
            this.btnStartDemo.FlatAppearance.BorderSize = 0;
            this.btnStartDemo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStartDemo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStartDemo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(126)))), ((int)(((byte)(249)))));
            this.btnStartDemo.Location = new System.Drawing.Point(42, 463);
            this.btnStartDemo.Name = "btnStartDemo";
            this.btnStartDemo.Size = new System.Drawing.Size(186, 45);
            this.btnStartDemo.TabIndex = 9;
            this.btnStartDemo.Text = "Start Demo!";
            this.btnStartDemo.UseVisualStyleBackColor = false;
            this.btnStartDemo.Click += new System.EventHandler(this.btnStartDemo_Click);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.Color.White;
            this.label15.Location = new System.Drawing.Point(390, 270);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(107, 24);
            this.label15.TabIndex = 14;
            this.label15.Text = "Aggregate";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(345, 53);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(186, 24);
            this.label4.TabIndex = 14;
            this.label4.Text = "WWAN Cellular 5G";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(42)))), ((int)(((byte)(64)))));
            this.panel2.Controls.Add(this.aggregateLabel);
            this.panel2.Location = new System.Drawing.Point(313, 297);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(258, 130);
            this.panel2.TabIndex = 15;
            // 
            // aggregateLabel
            // 
            this.aggregateLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.aggregateLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.aggregateLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(146)))), ((int)(((byte)(249)))));
            this.aggregateLabel.Location = new System.Drawing.Point(0, 0);
            this.aggregateLabel.Name = "aggregateLabel";
            this.aggregateLabel.Size = new System.Drawing.Size(258, 130);
            this.aggregateLabel.TabIndex = 0;
            this.aggregateLabel.Text = "---";
            this.aggregateLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(610, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(262, 24);
            this.label3.TabIndex = 12;
            this.label3.Text = "WiFi 6 - 2.4GHz Secondary";
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(42)))), ((int)(((byte)(64)))));
            this.panel5.Controls.Add(this.cellularLabel);
            this.panel5.Location = new System.Drawing.Point(313, 80);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(258, 130);
            this.panel5.TabIndex = 15;
            // 
            // cellularLabel
            // 
            this.cellularLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cellularLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cellularLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(146)))), ((int)(((byte)(249)))));
            this.cellularLabel.Location = new System.Drawing.Point(0, 0);
            this.cellularLabel.Name = "cellularLabel";
            this.cellularLabel.Size = new System.Drawing.Size(258, 130);
            this.cellularLabel.TabIndex = 0;
            this.cellularLabel.Text = "---";
            this.cellularLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(38, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(232, 24);
            this.label2.TabIndex = 8;
            this.label2.Text = "WiFi 6 - 5.2GHz Primary";
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(42)))), ((int)(((byte)(64)))));
            this.panel4.Controls.Add(this.lowBandLabel);
            this.panel4.Location = new System.Drawing.Point(607, 80);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(258, 130);
            this.panel4.TabIndex = 13;
            // 
            // lowBandLabel
            // 
            this.lowBandLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lowBandLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lowBandLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(146)))), ((int)(((byte)(249)))));
            this.lowBandLabel.Location = new System.Drawing.Point(0, 0);
            this.lowBandLabel.Name = "lowBandLabel";
            this.lowBandLabel.Size = new System.Drawing.Size(258, 130);
            this.lowBandLabel.TabIndex = 0;
            this.lowBandLabel.Text = "---";
            this.lowBandLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(42)))), ((int)(((byte)(64)))));
            this.panel3.Controls.Add(this.highBandLabel);
            this.panel3.Location = new System.Drawing.Point(17, 80);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(258, 130);
            this.panel3.TabIndex = 11;
            // 
            // highBandLabel
            // 
            this.highBandLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.highBandLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.highBandLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(146)))), ((int)(((byte)(249)))));
            this.highBandLabel.Location = new System.Drawing.Point(0, 0);
            this.highBandLabel.Name = "highBandLabel";
            this.highBandLabel.Size = new System.Drawing.Size(258, 130);
            this.highBandLabel.TabIndex = 0;
            this.highBandLabel.Text = "---";
            this.highBandLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // graphPanel
            // 
            this.graphPanel.Controls.Add(this.mainChart);
            this.graphPanel.Controls.Add(this.runIperButton);
            this.graphPanel.Location = new System.Drawing.Point(103, 162);
            this.graphPanel.Name = "graphPanel";
            this.graphPanel.Size = new System.Drawing.Size(856, 481);
            this.graphPanel.TabIndex = 20;
            // 
            // mainChart
            // 
            chartArea2.AxisX.Title = "Time (s)";
            chartArea2.AxisX.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea2.AxisY.Title = "Traffic (MB)";
            chartArea2.AxisY.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea2.Name = "ChartArea1";
            this.mainChart.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.mainChart.Legends.Add(legend2);
            this.mainChart.Location = new System.Drawing.Point(45, 41);
            this.mainChart.Name = "mainChart";
            series5.BackImageWrapMode = System.Windows.Forms.DataVisualization.Charting.ChartImageWrapMode.TileFlipX;
            series5.BorderColor = System.Drawing.Color.White;
            series5.BorderWidth = 3;
            series5.ChartArea = "ChartArea1";
            series5.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series5.Color = System.Drawing.Color.Blue;
            series5.Legend = "Legend1";
            series5.Name = "HighBand";
            series6.BorderWidth = 3;
            series6.ChartArea = "ChartArea1";
            series6.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series6.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            series6.Legend = "Legend1";
            series6.Name = "LowBand";
            series7.BorderWidth = 3;
            series7.ChartArea = "ChartArea1";
            series7.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series7.Color = System.Drawing.Color.Red;
            series7.Legend = "Legend1";
            series7.Name = "Cellular";
            series8.BorderWidth = 3;
            series8.ChartArea = "ChartArea1";
            series8.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series8.Color = System.Drawing.Color.Black;
            series8.Legend = "Legend1";
            series8.Name = "Sum";
            this.mainChart.Series.Add(series5);
            this.mainChart.Series.Add(series6);
            this.mainChart.Series.Add(series7);
            this.mainChart.Series.Add(series8);
            this.mainChart.Size = new System.Drawing.Size(774, 336);
            this.mainChart.TabIndex = 18;
            this.mainChart.Text = "chart1";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(30)))), ((int)(((byte)(54)))));
            this.panel1.Controls.Add(this.BtnSettings);
            this.panel1.Controls.Add(this.BtnView2);
            this.panel1.Controls.Add(this.BtnView1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 144);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(84, 549);
            this.panel1.TabIndex = 21;
            // 
            // BtnSettings
            // 
            this.BtnSettings.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.BtnSettings.FlatAppearance.BorderSize = 0;
            this.BtnSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnSettings.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSettings.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(126)))), ((int)(((byte)(249)))));
            this.BtnSettings.Location = new System.Drawing.Point(0, 504);
            this.BtnSettings.Name = "BtnSettings";
            this.BtnSettings.Size = new System.Drawing.Size(84, 45);
            this.BtnSettings.TabIndex = 3;
            this.BtnSettings.Text = "Settings";
            this.BtnSettings.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnSettings.UseVisualStyleBackColor = true;
            this.BtnSettings.Click += new System.EventHandler(this.BtnSettings_Click);
            this.BtnSettings.Leave += new System.EventHandler(this.BtnSettings_Leave);
            // 
            // BtnView2
            // 
            this.BtnView2.Dock = System.Windows.Forms.DockStyle.Top;
            this.BtnView2.FlatAppearance.BorderSize = 0;
            this.BtnView2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnView2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnView2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(126)))), ((int)(((byte)(249)))));
            this.BtnView2.Location = new System.Drawing.Point(0, 45);
            this.BtnView2.Name = "BtnView2";
            this.BtnView2.Size = new System.Drawing.Size(84, 45);
            this.BtnView2.TabIndex = 2;
            this.BtnView2.Text = "View 2";
            this.BtnView2.UseVisualStyleBackColor = true;
            this.BtnView2.Click += new System.EventHandler(this.BtnView2_Click);
            this.BtnView2.Leave += new System.EventHandler(this.BtnView2_Leave);
            // 
            // BtnView1
            // 
            this.BtnView1.Dock = System.Windows.Forms.DockStyle.Top;
            this.BtnView1.FlatAppearance.BorderSize = 0;
            this.BtnView1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnView1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnView1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(126)))), ((int)(((byte)(249)))));
            this.BtnView1.Location = new System.Drawing.Point(0, 0);
            this.BtnView1.Name = "BtnView1";
            this.BtnView1.Size = new System.Drawing.Size(84, 45);
            this.BtnView1.TabIndex = 1;
            this.BtnView1.Text = "View 1";
            this.BtnView1.UseVisualStyleBackColor = true;
            this.BtnView1.Click += new System.EventHandler(this.BtnView1_Click);
            this.BtnView1.Leave += new System.EventHandler(this.BtnView1_Leave);
            // 
            // startMonitorButton
            // 
            this.startMonitorButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(51)))), ((int)(((byte)(50)))));
            this.startMonitorButton.FlatAppearance.BorderSize = 0;
            this.startMonitorButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.startMonitorButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.startMonitorButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(126)))), ((int)(((byte)(249)))));
            this.startMonitorButton.Location = new System.Drawing.Point(373, 401);
            this.startMonitorButton.Name = "startMonitorButton";
            this.startMonitorButton.Size = new System.Drawing.Size(186, 45);
            this.startMonitorButton.TabIndex = 37;
            this.startMonitorButton.Text = "Start Monitoring";
            this.startMonitorButton.UseVisualStyleBackColor = false;
            this.startMonitorButton.Click += new System.EventHandler(this.startMonitorButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(51)))), ((int)(((byte)(73)))));
            this.ClientSize = new System.Drawing.Size(976, 693);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.PnlNav);
            this.Controls.Add(this.settingsPanel);
            this.Controls.Add(this.graphPanel);
            this.Controls.Add(this.numbersPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fileTransferPicture)).EndInit();
            this.settingsPanel.ResumeLayout(false);
            this.settingsPanel.PerformLayout();
            this.numbersPanel.ResumeLayout(false);
            this.numbersPanel.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.graphPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mainChart)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel PnlNav;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Button xSymboleButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox fileTransferPicture;
        private System.Windows.Forms.ComboBox highBandDrop;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox lowBandDrop;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cellularDrop;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox iperfCmdWiFi1;
        private System.Windows.Forms.TextBox iperfCmdWiFi2;
        private System.Windows.Forms.TextBox iperfCmdCellular;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button runIperButton;
        private System.Windows.Forms.Panel settingsPanel;
        private System.Windows.Forms.Panel numbersPanel;
        private System.Windows.Forms.Button wifiButtom;
        private System.Windows.Forms.Button btnStartDemo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label cellularLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label lowBandLabel;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label highBandLabel;
        private System.Windows.Forms.Panel graphPanel;
        private System.Windows.Forms.DataVisualization.Charting.Chart mainChart;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button BtnSettings;
        private System.Windows.Forms.Button BtnView2;
        private System.Windows.Forms.Button BtnView1;
        private System.Windows.Forms.TextBox profileName;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button profileButton;
        private System.Windows.Forms.CheckBox releaseCheckBox;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label aggregateLabel;
        private System.Windows.Forms.Button startMonitorButton;
    }
}

