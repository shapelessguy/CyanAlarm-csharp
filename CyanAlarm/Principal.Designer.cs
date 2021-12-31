namespace CyanAlarm
{
    partial class Principal
    {
        /// <summary>
        /// Variabile di progettazione necessaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Pulire le risorse in uso.
        /// </summary>
        /// <param name="disposing">ha valore true se le risorse gestite devono essere eliminate, false in caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Codice generato da Progettazione Windows Form

        /// <summary>
        /// Metodo necessario per il supporto della finestra di progettazione. Non modificare
        /// il contenuto del metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Principal));
            this.WebcamPanel = new System.Windows.Forms.Panel();
            this.webcam1 = new CyanAlarm.Webcam();
            this.webcam2 = new CyanAlarm.Webcam();
            this.webcam3 = new CyanAlarm.Webcam();
            this.webcam4 = new CyanAlarm.Webcam();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.web_btn = new System.Windows.Forms.Button();
            this.server_btn = new System.Windows.Forms.Button();
            this.ServerPanel = new System.Windows.Forms.Panel();
            this.recServer = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.serverPic = new System.Windows.Forms.PictureBox();
            this.cam_all = new System.Windows.Forms.Button();
            this.cam4 = new System.Windows.Forms.Button();
            this.cam3 = new System.Windows.Forms.Button();
            this.cam2 = new System.Windows.Forms.Button();
            this.cam1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.server_resolution = new System.Windows.Forms.TrackBar();
            this.server_framerate = new System.Windows.Forms.TrackBar();
            this.client_btn = new System.Windows.Forms.Button();
            this.ClientPanel = new System.Windows.Forms.Panel();
            this.recClient = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.clientPic = new System.Windows.Forms.PictureBox();
            this.clientCam_all = new System.Windows.Forms.Button();
            this.clientCam4 = new System.Windows.Forms.Button();
            this.clientCam3 = new System.Windows.Forms.Button();
            this.clientCam2 = new System.Windows.Forms.Button();
            this.clientCam1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.client_resolution = new System.Windows.Forms.TrackBar();
            this.client_framerate = new System.Windows.Forms.TrackBar();
            this.onlineState = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.button5 = new System.Windows.Forms.Button();
            this.WebcamPanel.SuspendLayout();
            this.ServerPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.serverPic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.server_resolution)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.server_framerate)).BeginInit();
            this.ClientPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.clientPic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.client_resolution)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.client_framerate)).BeginInit();
            this.SuspendLayout();
            // 
            // WebcamPanel
            // 
            this.WebcamPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.WebcamPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.WebcamPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.WebcamPanel.Controls.Add(this.webcam1);
            this.WebcamPanel.Controls.Add(this.webcam2);
            this.WebcamPanel.Controls.Add(this.webcam3);
            this.WebcamPanel.Controls.Add(this.webcam4);
            this.WebcamPanel.Location = new System.Drawing.Point(138, 41);
            this.WebcamPanel.Name = "WebcamPanel";
            this.WebcamPanel.Size = new System.Drawing.Size(1025, 673);
            this.WebcamPanel.TabIndex = 0;
            // 
            // webcam1
            // 
            this.webcam1.Location = new System.Drawing.Point(19, 40);
            this.webcam1.Name = "webcam1";
            this.webcam1.Size = new System.Drawing.Size(495, 320);
            this.webcam1.TabIndex = 7;
            // 
            // webcam2
            // 
            this.webcam2.Location = new System.Drawing.Point(520, 40);
            this.webcam2.Name = "webcam2";
            this.webcam2.Size = new System.Drawing.Size(495, 320);
            this.webcam2.TabIndex = 8;
            // 
            // webcam3
            // 
            this.webcam3.Location = new System.Drawing.Point(19, 372);
            this.webcam3.Name = "webcam3";
            this.webcam3.Size = new System.Drawing.Size(495, 320);
            this.webcam3.TabIndex = 9;
            // 
            // webcam4
            // 
            this.webcam4.Location = new System.Drawing.Point(520, 372);
            this.webcam4.Name = "webcam4";
            this.webcam4.Size = new System.Drawing.Size(495, 320);
            this.webcam4.TabIndex = 10;
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Cambria", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.Location = new System.Drawing.Point(87, 21);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(45, 23);
            this.button3.TabIndex = 6;
            this.button3.Text = "stop";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Cambria", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(12, 21);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(51, 23);
            this.button2.TabIndex = 5;
            this.button2.Text = "start";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // listBox1
            // 
            this.listBox1.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 19;
            this.listBox1.Location = new System.Drawing.Point(12, 52);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(120, 555);
            this.listBox1.TabIndex = 0;
            // 
            // comboBox1
            // 
            this.comboBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBox1.Font = new System.Drawing.Font("Cambria", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(12, 689);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(120, 25);
            this.comboBox1.TabIndex = 7;
            this.comboBox1.TextChanged += new System.EventHandler(this.comboBox1_TextChanged);
            // 
            // web_btn
            // 
            this.web_btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.web_btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.web_btn.FlatAppearance.BorderSize = 0;
            this.web_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.web_btn.Font = new System.Drawing.Font("Cambria", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.web_btn.ForeColor = System.Drawing.Color.White;
            this.web_btn.Location = new System.Drawing.Point(1097, 12);
            this.web_btn.Name = "web_btn";
            this.web_btn.Size = new System.Drawing.Size(66, 29);
            this.web_btn.TabIndex = 8;
            this.web_btn.Text = "Webcam";
            this.web_btn.UseVisualStyleBackColor = false;
            this.web_btn.Click += new System.EventHandler(this.button1_Click);
            // 
            // server_btn
            // 
            this.server_btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.server_btn.BackColor = System.Drawing.Color.WhiteSmoke;
            this.server_btn.Font = new System.Drawing.Font("Cambria", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.server_btn.Location = new System.Drawing.Point(1039, 12);
            this.server_btn.Name = "server_btn";
            this.server_btn.Size = new System.Drawing.Size(57, 29);
            this.server_btn.TabIndex = 9;
            this.server_btn.Text = "Server";
            this.server_btn.UseVisualStyleBackColor = false;
            this.server_btn.Click += new System.EventHandler(this.button4_Click);
            // 
            // ServerPanel
            // 
            this.ServerPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ServerPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.ServerPanel.Controls.Add(this.recServer);
            this.ServerPanel.Controls.Add(this.label6);
            this.ServerPanel.Controls.Add(this.label4);
            this.ServerPanel.Controls.Add(this.serverPic);
            this.ServerPanel.Controls.Add(this.cam_all);
            this.ServerPanel.Controls.Add(this.cam4);
            this.ServerPanel.Controls.Add(this.cam3);
            this.ServerPanel.Controls.Add(this.cam2);
            this.ServerPanel.Controls.Add(this.cam1);
            this.ServerPanel.Controls.Add(this.label1);
            this.ServerPanel.Controls.Add(this.server_resolution);
            this.ServerPanel.Controls.Add(this.server_framerate);
            this.ServerPanel.Location = new System.Drawing.Point(138, 41);
            this.ServerPanel.Name = "ServerPanel";
            this.ServerPanel.Size = new System.Drawing.Size(1025, 673);
            this.ServerPanel.TabIndex = 11;
            // 
            // recServer
            // 
            this.recServer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.recServer.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.recServer.Location = new System.Drawing.Point(18, 24);
            this.recServer.Name = "recServer";
            this.recServer.Size = new System.Drawing.Size(24, 23);
            this.recServer.TabIndex = 15;
            this.recServer.Click += new System.EventHandler(this.recServer_Click);
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(644, 11);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(90, 19);
            this.label6.TabIndex = 14;
            this.label6.Text = "Framerate:";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(840, 11);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(93, 19);
            this.label4.TabIndex = 14;
            this.label4.Text = "Resolution:";
            // 
            // serverPic
            // 
            this.serverPic.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.serverPic.BackColor = System.Drawing.Color.Black;
            this.serverPic.Location = new System.Drawing.Point(36, 69);
            this.serverPic.Name = "serverPic";
            this.serverPic.Size = new System.Drawing.Size(950, 576);
            this.serverPic.TabIndex = 6;
            this.serverPic.TabStop = false;
            // 
            // cam_all
            // 
            this.cam_all.BackColor = System.Drawing.Color.LightSkyBlue;
            this.cam_all.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cam_all.Font = new System.Drawing.Font("Cambria", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cam_all.Location = new System.Drawing.Point(505, 18);
            this.cam_all.Name = "cam_all";
            this.cam_all.Size = new System.Drawing.Size(70, 35);
            this.cam_all.TabIndex = 5;
            this.cam_all.Text = "All";
            this.cam_all.UseVisualStyleBackColor = false;
            this.cam_all.Click += new System.EventHandler(this.cam_all_Click);
            // 
            // cam4
            // 
            this.cam4.BackColor = System.Drawing.Color.AliceBlue;
            this.cam4.Font = new System.Drawing.Font("Cambria", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cam4.Location = new System.Drawing.Point(429, 18);
            this.cam4.Name = "cam4";
            this.cam4.Size = new System.Drawing.Size(70, 35);
            this.cam4.TabIndex = 4;
            this.cam4.Text = "Cam 4";
            this.cam4.UseVisualStyleBackColor = false;
            this.cam4.Click += new System.EventHandler(this.cam4_Click);
            // 
            // cam3
            // 
            this.cam3.BackColor = System.Drawing.Color.AliceBlue;
            this.cam3.Font = new System.Drawing.Font("Cambria", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cam3.Location = new System.Drawing.Point(353, 18);
            this.cam3.Name = "cam3";
            this.cam3.Size = new System.Drawing.Size(70, 35);
            this.cam3.TabIndex = 3;
            this.cam3.Text = "Cam 3";
            this.cam3.UseVisualStyleBackColor = false;
            this.cam3.Click += new System.EventHandler(this.cam3_Click);
            // 
            // cam2
            // 
            this.cam2.BackColor = System.Drawing.Color.AliceBlue;
            this.cam2.Font = new System.Drawing.Font("Cambria", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cam2.Location = new System.Drawing.Point(277, 18);
            this.cam2.Name = "cam2";
            this.cam2.Size = new System.Drawing.Size(70, 35);
            this.cam2.TabIndex = 2;
            this.cam2.Text = "Cam 2";
            this.cam2.UseVisualStyleBackColor = false;
            this.cam2.Click += new System.EventHandler(this.cam2_Click);
            // 
            // cam1
            // 
            this.cam1.BackColor = System.Drawing.Color.AliceBlue;
            this.cam1.Font = new System.Drawing.Font("Cambria", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cam1.Location = new System.Drawing.Point(201, 18);
            this.cam1.Name = "cam1";
            this.cam1.Size = new System.Drawing.Size(70, 35);
            this.cam1.TabIndex = 1;
            this.cam1.Text = "Cam 1";
            this.cam1.UseVisualStyleBackColor = false;
            this.cam1.Click += new System.EventHandler(this.cam1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(70, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "Requested cam:";
            // 
            // server_resolution
            // 
            this.server_resolution.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.server_resolution.Location = new System.Drawing.Point(784, 31);
            this.server_resolution.Maximum = 9;
            this.server_resolution.Name = "server_resolution";
            this.server_resolution.Size = new System.Drawing.Size(202, 45);
            this.server_resolution.TabIndex = 13;
            this.server_resolution.Value = 9;
            // 
            // server_framerate
            // 
            this.server_framerate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.server_framerate.Location = new System.Drawing.Point(597, 31);
            this.server_framerate.Maximum = 9;
            this.server_framerate.Name = "server_framerate";
            this.server_framerate.Size = new System.Drawing.Size(185, 45);
            this.server_framerate.TabIndex = 13;
            this.server_framerate.Value = 9;
            // 
            // client_btn
            // 
            this.client_btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.client_btn.BackColor = System.Drawing.Color.WhiteSmoke;
            this.client_btn.Font = new System.Drawing.Font("Cambria", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.client_btn.Location = new System.Drawing.Point(982, 12);
            this.client_btn.Name = "client_btn";
            this.client_btn.Size = new System.Drawing.Size(57, 29);
            this.client_btn.TabIndex = 12;
            this.client_btn.Text = "Client";
            this.client_btn.UseVisualStyleBackColor = false;
            this.client_btn.Click += new System.EventHandler(this.client_btn_Click);
            // 
            // ClientPanel
            // 
            this.ClientPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ClientPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.ClientPanel.Controls.Add(this.recClient);
            this.ClientPanel.Controls.Add(this.button1);
            this.ClientPanel.Controls.Add(this.label5);
            this.ClientPanel.Controls.Add(this.label3);
            this.ClientPanel.Controls.Add(this.clientPic);
            this.ClientPanel.Controls.Add(this.clientCam_all);
            this.ClientPanel.Controls.Add(this.clientCam4);
            this.ClientPanel.Controls.Add(this.clientCam3);
            this.ClientPanel.Controls.Add(this.clientCam2);
            this.ClientPanel.Controls.Add(this.clientCam1);
            this.ClientPanel.Controls.Add(this.label2);
            this.ClientPanel.Controls.Add(this.client_resolution);
            this.ClientPanel.Controls.Add(this.client_framerate);
            this.ClientPanel.Location = new System.Drawing.Point(138, 41);
            this.ClientPanel.Name = "ClientPanel";
            this.ClientPanel.Size = new System.Drawing.Size(1025, 673);
            this.ClientPanel.TabIndex = 12;
            // 
            // recClient
            // 
            this.recClient.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.recClient.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.recClient.Location = new System.Drawing.Point(11, 26);
            this.recClient.Name = "recClient";
            this.recClient.Size = new System.Drawing.Size(24, 23);
            this.recClient.TabIndex = 14;
            this.recClient.Click += new System.EventHandler(this.recClient_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Beige;
            this.button1.Font = new System.Drawing.Font("Cambria", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(597, 20);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(70, 35);
            this.button1.TabIndex = 11;
            this.button1.Text = "Play";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(724, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(90, 19);
            this.label5.TabIndex = 10;
            this.label5.Text = "Framerate:";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(882, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 19);
            this.label3.TabIndex = 8;
            this.label3.Text = "Resolution:";
            // 
            // clientPic
            // 
            this.clientPic.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.clientPic.BackColor = System.Drawing.Color.Black;
            this.clientPic.Location = new System.Drawing.Point(36, 69);
            this.clientPic.Name = "clientPic";
            this.clientPic.Size = new System.Drawing.Size(950, 576);
            this.clientPic.TabIndex = 6;
            this.clientPic.TabStop = false;
            // 
            // clientCam_all
            // 
            this.clientCam_all.BackColor = System.Drawing.Color.LightSkyBlue;
            this.clientCam_all.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.clientCam_all.Font = new System.Drawing.Font("Cambria", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clientCam_all.Location = new System.Drawing.Point(487, 20);
            this.clientCam_all.Name = "clientCam_all";
            this.clientCam_all.Size = new System.Drawing.Size(70, 35);
            this.clientCam_all.TabIndex = 5;
            this.clientCam_all.Text = "All";
            this.clientCam_all.UseVisualStyleBackColor = false;
            this.clientCam_all.Click += new System.EventHandler(this.clientCam_all_Click);
            // 
            // clientCam4
            // 
            this.clientCam4.BackColor = System.Drawing.Color.AliceBlue;
            this.clientCam4.Font = new System.Drawing.Font("Cambria", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clientCam4.Location = new System.Drawing.Point(411, 20);
            this.clientCam4.Name = "clientCam4";
            this.clientCam4.Size = new System.Drawing.Size(70, 35);
            this.clientCam4.TabIndex = 4;
            this.clientCam4.Text = "Cam 4";
            this.clientCam4.UseVisualStyleBackColor = false;
            this.clientCam4.Click += new System.EventHandler(this.clientCam4_Click);
            // 
            // clientCam3
            // 
            this.clientCam3.BackColor = System.Drawing.Color.AliceBlue;
            this.clientCam3.Font = new System.Drawing.Font("Cambria", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clientCam3.Location = new System.Drawing.Point(335, 20);
            this.clientCam3.Name = "clientCam3";
            this.clientCam3.Size = new System.Drawing.Size(70, 35);
            this.clientCam3.TabIndex = 3;
            this.clientCam3.Text = "Cam 3";
            this.clientCam3.UseVisualStyleBackColor = false;
            this.clientCam3.Click += new System.EventHandler(this.clientCam3_Click);
            // 
            // clientCam2
            // 
            this.clientCam2.BackColor = System.Drawing.Color.AliceBlue;
            this.clientCam2.Font = new System.Drawing.Font("Cambria", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clientCam2.Location = new System.Drawing.Point(259, 20);
            this.clientCam2.Name = "clientCam2";
            this.clientCam2.Size = new System.Drawing.Size(70, 35);
            this.clientCam2.TabIndex = 2;
            this.clientCam2.Text = "Cam 2";
            this.clientCam2.UseVisualStyleBackColor = false;
            this.clientCam2.Click += new System.EventHandler(this.clientCam2_Click);
            // 
            // clientCam1
            // 
            this.clientCam1.BackColor = System.Drawing.Color.AliceBlue;
            this.clientCam1.Font = new System.Drawing.Font("Cambria", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clientCam1.Location = new System.Drawing.Point(183, 20);
            this.clientCam1.Name = "clientCam1";
            this.clientCam1.Size = new System.Drawing.Size(70, 35);
            this.clientCam1.TabIndex = 1;
            this.clientCam1.Text = "Cam 1";
            this.clientCam1.UseVisualStyleBackColor = false;
            this.clientCam1.Click += new System.EventHandler(this.clientCam1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(52, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(125, 19);
            this.label2.TabIndex = 0;
            this.label2.Text = "Requested cam:";
            // 
            // client_resolution
            // 
            this.client_resolution.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.client_resolution.Location = new System.Drawing.Point(838, 31);
            this.client_resolution.Maximum = 9;
            this.client_resolution.Name = "client_resolution";
            this.client_resolution.Size = new System.Drawing.Size(165, 45);
            this.client_resolution.TabIndex = 7;
            this.client_resolution.Value = 9;
            // 
            // client_framerate
            // 
            this.client_framerate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.client_framerate.Location = new System.Drawing.Point(694, 31);
            this.client_framerate.Maximum = 9;
            this.client_framerate.Name = "client_framerate";
            this.client_framerate.Size = new System.Drawing.Size(153, 45);
            this.client_framerate.TabIndex = 9;
            this.client_framerate.Value = 9;
            // 
            // onlineState
            // 
            this.onlineState.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.onlineState.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.onlineState.Location = new System.Drawing.Point(150, 10);
            this.onlineState.Name = "onlineState";
            this.onlineState.Size = new System.Drawing.Size(100, 25);
            this.onlineState.TabIndex = 13;
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(907, 10);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(31, 26);
            this.textBox1.TabIndex = 14;
            this.textBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            this.textBox1.Leave += new System.EventHandler(this.textBox1_Leave);
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(778, 14);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(123, 19);
            this.label7.TabIndex = 15;
            this.label7.Text = "Auto-rec (min):";
            // 
            // button5
            // 
            this.button5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button5.BackColor = System.Drawing.Color.WhiteSmoke;
            this.button5.Font = new System.Drawing.Font("Cambria", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button5.Location = new System.Drawing.Point(674, 7);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(87, 29);
            this.button5.TabIndex = 16;
            this.button5.Text = "Impostazioni";
            this.button5.UseVisualStyleBackColor = false;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // Principal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.ClientSize = new System.Drawing.Size(1175, 726);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.onlineState);
            this.Controls.Add(this.client_btn);
            this.Controls.Add(this.server_btn);
            this.Controls.Add(this.web_btn);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.WebcamPanel);
            this.Controls.Add(this.ClientPanel);
            this.Controls.Add(this.ServerPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.MinimumSize = new System.Drawing.Size(1191, 765);
            this.Name = "Principal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cyan Alarm";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.WebcamPanel.ResumeLayout(false);
            this.ServerPanel.ResumeLayout(false);
            this.ServerPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.serverPic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.server_resolution)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.server_framerate)).EndInit();
            this.ClientPanel.ResumeLayout(false);
            this.ClientPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.clientPic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.client_resolution)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.client_framerate)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel WebcamPanel;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private Webcam webcam1;
        private Webcam webcam2;
        private Webcam webcam3;
        private Webcam webcam4;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button web_btn;
        private System.Windows.Forms.Button server_btn;
        private System.Windows.Forms.Panel ServerPanel;
        private System.Windows.Forms.Button cam_all;
        private System.Windows.Forms.Button cam4;
        private System.Windows.Forms.Button cam3;
        private System.Windows.Forms.Button cam2;
        private System.Windows.Forms.Button cam1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox serverPic;
        private System.Windows.Forms.Button client_btn;
        private System.Windows.Forms.Panel ClientPanel;
        private System.Windows.Forms.PictureBox clientPic;
        private System.Windows.Forms.Button clientCam_all;
        private System.Windows.Forms.Button clientCam4;
        private System.Windows.Forms.Button clientCam3;
        private System.Windows.Forms.Button clientCam2;
        private System.Windows.Forms.Button clientCam1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TrackBar server_resolution;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TrackBar client_resolution;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TrackBar server_framerate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TrackBar client_framerate;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label onlineState;
        private System.Windows.Forms.Label recClient;
        private System.Windows.Forms.Label recServer;
        public System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button5;
    }
}

