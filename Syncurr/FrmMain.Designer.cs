namespace Syncurr
{
    partial class FrmMain
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.btnPin = new System.Windows.Forms.Button();
            this.lblPin = new System.Windows.Forms.Label();
            this.txtPin = new System.Windows.Forms.TextBox();
            this.btnTokens = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.grpProxySettings = new System.Windows.Forms.GroupBox();
            this.numProxyInternalPort = new System.Windows.Forms.NumericUpDown();
            this.numProxyPort = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtProxyPassword = new System.Windows.Forms.TextBox();
            this.txtProxyInternalUrl = new System.Windows.Forms.TextBox();
            this.txtProxyUsername = new System.Windows.Forms.TextBox();
            this.txtProxyUrl = new System.Windows.Forms.TextBox();
            this.grpProxyType = new System.Windows.Forms.GroupBox();
            this.rdoProxyTypeSocks5 = new System.Windows.Forms.RadioButton();
            this.rdoProxyTypeSocks4 = new System.Windows.Forms.RadioButton();
            this.rdoProxyTypeHttp = new System.Windows.Forms.RadioButton();
            this.chkProxy = new System.Windows.Forms.CheckBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.lstAlbums = new System.Windows.Forms.ListView();
            this.colAlbum = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colFolder = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ctxAlbum = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ctxAlbumRemove = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxAlbumProperties = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxAlbumSync = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.prgSync = new System.Windows.Forms.ProgressBar();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.ntfTray = new System.Windows.Forms.NotifyIcon(this.components);
            this.ctxAlbumOpenFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxAlbumOpenAlbum = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.grpProxySettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numProxyInternalPort)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numProxyPort)).BeginInit();
            this.grpProxyType.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.ctxAlbum.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnPin
            // 
            this.btnPin.Location = new System.Drawing.Point(6, 6);
            this.btnPin.Name = "btnPin";
            this.btnPin.Size = new System.Drawing.Size(75, 23);
            this.btnPin.TabIndex = 0;
            this.btnPin.Text = "Get Pin";
            this.btnPin.UseVisualStyleBackColor = true;
            this.btnPin.Click += new System.EventHandler(this.btnPin_Click);
            // 
            // lblPin
            // 
            this.lblPin.AutoSize = true;
            this.lblPin.Location = new System.Drawing.Point(6, 38);
            this.lblPin.Name = "lblPin";
            this.lblPin.Size = new System.Drawing.Size(25, 13);
            this.lblPin.TabIndex = 1;
            this.lblPin.Text = "Pin:";
            // 
            // txtPin
            // 
            this.txtPin.Location = new System.Drawing.Point(37, 35);
            this.txtPin.Name = "txtPin";
            this.txtPin.Size = new System.Drawing.Size(100, 20);
            this.txtPin.TabIndex = 2;
            // 
            // btnTokens
            // 
            this.btnTokens.Location = new System.Drawing.Point(143, 33);
            this.btnTokens.Name = "btnTokens";
            this.btnTokens.Size = new System.Drawing.Size(75, 23);
            this.btnTokens.TabIndex = 3;
            this.btnTokens.Text = "Get Tokens";
            this.btnTokens.UseVisualStyleBackColor = true;
            this.btnTokens.Click += new System.EventHandler(this.btnTokens_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Access Token:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 90);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Refresh Token:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 116);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Username:";
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(430, 259);
            this.tabControl1.TabIndex = 10;
            // 
            // tabPage1
            // 
            this.tabPage1.AutoScroll = true;
            this.tabPage1.Controls.Add(this.btnPin);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.lblPin);
            this.tabPage1.Controls.Add(this.txtPin);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.btnTokens);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.textBox3);
            this.tabPage1.Controls.Add(this.textBox1);
            this.tabPage1.Controls.Add(this.textBox2);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(422, 233);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Imgur Authorization";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.AutoScroll = true;
            this.tabPage2.Controls.Add(this.grpProxySettings);
            this.tabPage2.Controls.Add(this.grpProxyType);
            this.tabPage2.Controls.Add(this.chkProxy);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(422, 233);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Proxy";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // grpProxySettings
            // 
            this.grpProxySettings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpProxySettings.Controls.Add(this.numProxyInternalPort);
            this.grpProxySettings.Controls.Add(this.numProxyPort);
            this.grpProxySettings.Controls.Add(this.label7);
            this.grpProxySettings.Controls.Add(this.label5);
            this.grpProxySettings.Controls.Add(this.label9);
            this.grpProxySettings.Controls.Add(this.label6);
            this.grpProxySettings.Controls.Add(this.label8);
            this.grpProxySettings.Controls.Add(this.label4);
            this.grpProxySettings.Controls.Add(this.txtProxyPassword);
            this.grpProxySettings.Controls.Add(this.txtProxyInternalUrl);
            this.grpProxySettings.Controls.Add(this.txtProxyUsername);
            this.grpProxySettings.Controls.Add(this.txtProxyUrl);
            this.grpProxySettings.Location = new System.Drawing.Point(6, 102);
            this.grpProxySettings.Name = "grpProxySettings";
            this.grpProxySettings.Size = new System.Drawing.Size(410, 125);
            this.grpProxySettings.TabIndex = 3;
            this.grpProxySettings.TabStop = false;
            this.grpProxySettings.Text = "Proxy Settings";
            // 
            // numProxyInternalPort
            // 
            this.numProxyInternalPort.Location = new System.Drawing.Point(333, 45);
            this.numProxyInternalPort.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.numProxyInternalPort.Name = "numProxyInternalPort";
            this.numProxyInternalPort.Size = new System.Drawing.Size(64, 20);
            this.numProxyInternalPort.TabIndex = 6;
            this.numProxyInternalPort.ValueChanged += new System.EventHandler(this.numProxyInternalPort_ValueChanged);
            // 
            // numProxyPort
            // 
            this.numProxyPort.Location = new System.Drawing.Point(333, 19);
            this.numProxyPort.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.numProxyPort.Name = "numProxyPort";
            this.numProxyPort.Size = new System.Drawing.Size(64, 20);
            this.numProxyPort.TabIndex = 4;
            this.numProxyPort.ValueChanged += new System.EventHandler(this.numProxyPort_ValueChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(298, 48);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 13);
            this.label7.TabIndex = 2;
            this.label7.Text = "Port:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(298, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Port:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 100);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(56, 13);
            this.label9.TabIndex = 1;
            this.label9.Text = "Password:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 48);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(70, 13);
            this.label6.TabIndex = 1;
            this.label6.Text = "Internal URL:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 74);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(58, 13);
            this.label8.TabIndex = 1;
            this.label8.Text = "Username:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Proxy URL:";
            // 
            // txtProxyPassword
            // 
            this.txtProxyPassword.Location = new System.Drawing.Point(82, 97);
            this.txtProxyPassword.Name = "txtProxyPassword";
            this.txtProxyPassword.Size = new System.Drawing.Size(210, 20);
            this.txtProxyPassword.TabIndex = 8;
            this.txtProxyPassword.UseSystemPasswordChar = true;
            this.txtProxyPassword.TextChanged += new System.EventHandler(this.txtProxyPassword_TextChanged);
            // 
            // txtProxyInternalUrl
            // 
            this.txtProxyInternalUrl.Location = new System.Drawing.Point(82, 45);
            this.txtProxyInternalUrl.Name = "txtProxyInternalUrl";
            this.txtProxyInternalUrl.Size = new System.Drawing.Size(210, 20);
            this.txtProxyInternalUrl.TabIndex = 5;
            this.txtProxyInternalUrl.TextChanged += new System.EventHandler(this.txtProxyInternalUrl_TextChanged);
            // 
            // txtProxyUsername
            // 
            this.txtProxyUsername.Location = new System.Drawing.Point(82, 71);
            this.txtProxyUsername.Name = "txtProxyUsername";
            this.txtProxyUsername.Size = new System.Drawing.Size(210, 20);
            this.txtProxyUsername.TabIndex = 7;
            this.txtProxyUsername.TextChanged += new System.EventHandler(this.txtProxyUsername_TextChanged);
            // 
            // txtProxyUrl
            // 
            this.txtProxyUrl.Location = new System.Drawing.Point(82, 19);
            this.txtProxyUrl.Name = "txtProxyUrl";
            this.txtProxyUrl.Size = new System.Drawing.Size(210, 20);
            this.txtProxyUrl.TabIndex = 3;
            this.txtProxyUrl.TextChanged += new System.EventHandler(this.txtProxyUrl_TextChanged);
            // 
            // grpProxyType
            // 
            this.grpProxyType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpProxyType.Controls.Add(this.rdoProxyTypeSocks5);
            this.grpProxyType.Controls.Add(this.rdoProxyTypeSocks4);
            this.grpProxyType.Controls.Add(this.rdoProxyTypeHttp);
            this.grpProxyType.Location = new System.Drawing.Point(6, 29);
            this.grpProxyType.Name = "grpProxyType";
            this.grpProxyType.Size = new System.Drawing.Size(410, 67);
            this.grpProxyType.TabIndex = 2;
            this.grpProxyType.TabStop = false;
            this.grpProxyType.Text = "Proxy Type";
            // 
            // rdoProxyTypeSocks5
            // 
            this.rdoProxyTypeSocks5.AutoSize = true;
            this.rdoProxyTypeSocks5.Location = new System.Drawing.Point(82, 42);
            this.rdoProxyTypeSocks5.Name = "rdoProxyTypeSocks5";
            this.rdoProxyTypeSocks5.Size = new System.Drawing.Size(70, 17);
            this.rdoProxyTypeSocks5.TabIndex = 2;
            this.rdoProxyTypeSocks5.TabStop = true;
            this.rdoProxyTypeSocks5.Text = "SOCKS 5";
            this.rdoProxyTypeSocks5.UseVisualStyleBackColor = true;
            this.rdoProxyTypeSocks5.CheckedChanged += new System.EventHandler(this.rdoProxyType_CheckedChanged);
            // 
            // rdoProxyTypeSocks4
            // 
            this.rdoProxyTypeSocks4.AutoSize = true;
            this.rdoProxyTypeSocks4.Location = new System.Drawing.Point(6, 42);
            this.rdoProxyTypeSocks4.Name = "rdoProxyTypeSocks4";
            this.rdoProxyTypeSocks4.Size = new System.Drawing.Size(70, 17);
            this.rdoProxyTypeSocks4.TabIndex = 2;
            this.rdoProxyTypeSocks4.TabStop = true;
            this.rdoProxyTypeSocks4.Text = "SOCKS 4";
            this.rdoProxyTypeSocks4.UseVisualStyleBackColor = true;
            this.rdoProxyTypeSocks4.CheckedChanged += new System.EventHandler(this.rdoProxyType_CheckedChanged);
            // 
            // rdoProxyTypeHttp
            // 
            this.rdoProxyTypeHttp.AutoSize = true;
            this.rdoProxyTypeHttp.Location = new System.Drawing.Point(6, 19);
            this.rdoProxyTypeHttp.Name = "rdoProxyTypeHttp";
            this.rdoProxyTypeHttp.Size = new System.Drawing.Size(54, 17);
            this.rdoProxyTypeHttp.TabIndex = 2;
            this.rdoProxyTypeHttp.TabStop = true;
            this.rdoProxyTypeHttp.Text = "HTTP";
            this.rdoProxyTypeHttp.UseVisualStyleBackColor = true;
            this.rdoProxyTypeHttp.CheckedChanged += new System.EventHandler(this.rdoProxyType_CheckedChanged);
            // 
            // chkProxy
            // 
            this.chkProxy.AutoSize = true;
            this.chkProxy.Location = new System.Drawing.Point(6, 6);
            this.chkProxy.Name = "chkProxy";
            this.chkProxy.Size = new System.Drawing.Size(74, 17);
            this.chkProxy.TabIndex = 1;
            this.chkProxy.Text = "Use Proxy";
            this.chkProxy.UseVisualStyleBackColor = true;
            this.chkProxy.CheckedChanged += new System.EventHandler(this.chkProxy_CheckedChanged);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.lstAlbums);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(422, 233);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Albums";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // lstAlbums
            // 
            this.lstAlbums.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstAlbums.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colAlbum,
            this.colId,
            this.colFolder});
            this.lstAlbums.Location = new System.Drawing.Point(6, 6);
            this.lstAlbums.Name = "lstAlbums";
            this.lstAlbums.Size = new System.Drawing.Size(410, 221);
            this.lstAlbums.TabIndex = 0;
            this.lstAlbums.UseCompatibleStateImageBehavior = false;
            this.lstAlbums.View = System.Windows.Forms.View.Details;
            this.lstAlbums.DoubleClick += new System.EventHandler(this.lstAlbums_DoubleClick);
            this.lstAlbums.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lstAlbums_MouseClick);
            // 
            // colAlbum
            // 
            this.colAlbum.Text = "Album";
            this.colAlbum.Width = 114;
            // 
            // colId
            // 
            this.colId.Text = "ID";
            // 
            // colFolder
            // 
            this.colFolder.Text = "Folder";
            this.colFolder.Width = 225;
            // 
            // ctxAlbum
            // 
            this.ctxAlbum.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctxAlbumProperties,
            this.ctxAlbumSync,
            this.toolStripSeparator1,
            this.ctxAlbumOpenFolder,
            this.ctxAlbumOpenAlbum,
            this.toolStripSeparator2,
            this.ctxAlbumRemove});
            this.ctxAlbum.Name = "ctxAlbum";
            this.ctxAlbum.Size = new System.Drawing.Size(153, 148);
            // 
            // ctxAlbumRemove
            // 
            this.ctxAlbumRemove.Name = "ctxAlbumRemove";
            this.ctxAlbumRemove.Size = new System.Drawing.Size(152, 22);
            this.ctxAlbumRemove.Text = "Remove";
            this.ctxAlbumRemove.Click += new System.EventHandler(this.ctxAlbumRemove_Click);
            // 
            // ctxAlbumProperties
            // 
            this.ctxAlbumProperties.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctxAlbumProperties.Name = "ctxAlbumProperties";
            this.ctxAlbumProperties.Size = new System.Drawing.Size(152, 22);
            this.ctxAlbumProperties.Text = "Properties...";
            this.ctxAlbumProperties.Click += new System.EventHandler(this.ctxAlbumProperties_Click);
            // 
            // ctxAlbumSync
            // 
            this.ctxAlbumSync.Name = "ctxAlbumSync";
            this.ctxAlbumSync.Size = new System.Drawing.Size(152, 22);
            this.ctxAlbumSync.Text = "Sync Now";
            this.ctxAlbumSync.Click += new System.EventHandler(this.ctxAlbumSync_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
            // 
            // prgSync
            // 
            this.prgSync.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.prgSync.Location = new System.Drawing.Point(12, 277);
            this.prgSync.MarqueeAnimationSpeed = 0;
            this.prgSync.Name = "prgSync";
            this.prgSync.Size = new System.Drawing.Size(430, 23);
            this.prgSync.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.prgSync.TabIndex = 12;
            // 
            // textBox3
            // 
            this.textBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox3.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Syncurr.Properties.Settings.Default, "accountUsername", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBox3.Location = new System.Drawing.Point(93, 113);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(323, 20);
            this.textBox3.TabIndex = 8;
            this.textBox3.Text = global::Syncurr.Properties.Settings.Default.accountUsername;
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Syncurr.Properties.Settings.Default, "accessToken", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBox1.Location = new System.Drawing.Point(93, 61);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(323, 20);
            this.textBox1.TabIndex = 4;
            this.textBox1.Text = global::Syncurr.Properties.Settings.Default.accessToken;
            // 
            // textBox2
            // 
            this.textBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox2.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Syncurr.Properties.Settings.Default, "refreshToken", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBox2.Location = new System.Drawing.Point(93, 87);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(323, 20);
            this.textBox2.TabIndex = 5;
            this.textBox2.Text = global::Syncurr.Properties.Settings.Default.refreshToken;
            // 
            // ntfTray
            // 
            this.ntfTray.Icon = ((System.Drawing.Icon)(resources.GetObject("ntfTray.Icon")));
            this.ntfTray.Text = "Syncurr";
            this.ntfTray.Visible = true;
            this.ntfTray.DoubleClick += new System.EventHandler(this.ntfTray_DoubleClick);
            // 
            // ctxAlbumOpenFolder
            // 
            this.ctxAlbumOpenFolder.Name = "ctxAlbumOpenFolder";
            this.ctxAlbumOpenFolder.Size = new System.Drawing.Size(152, 22);
            this.ctxAlbumOpenFolder.Text = "Open Folder";
            this.ctxAlbumOpenFolder.Click += new System.EventHandler(this.ctxAlbumOpenFolder_Click);
            // 
            // ctxAlbumOpenAlbum
            // 
            this.ctxAlbumOpenAlbum.Name = "ctxAlbumOpenAlbum";
            this.ctxAlbumOpenAlbum.Size = new System.Drawing.Size(152, 22);
            this.ctxAlbumOpenAlbum.Text = "Open Album";
            this.ctxAlbumOpenAlbum.Click += new System.EventHandler(this.ctxAlbumOpenAlbum_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(149, 6);
            // 
            // FrmMain
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(454, 307);
            this.Controls.Add(this.prgSync);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(470, 345);
            this.Name = "FrmMain";
            this.Text = "Syncurr";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.FrmMain_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.FrmMain_DragEnter);
            this.Resize += new System.EventHandler(this.FrmMain_Resize);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.grpProxySettings.ResumeLayout(false);
            this.grpProxySettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numProxyInternalPort)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numProxyPort)).EndInit();
            this.grpProxyType.ResumeLayout(false);
            this.grpProxyType.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.ctxAlbum.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnPin;
        private System.Windows.Forms.Label lblPin;
        private System.Windows.Forms.TextBox txtPin;
        private System.Windows.Forms.Button btnTokens;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox grpProxySettings;
        private System.Windows.Forms.GroupBox grpProxyType;
        private System.Windows.Forms.RadioButton rdoProxyTypeSocks5;
        private System.Windows.Forms.RadioButton rdoProxyTypeSocks4;
        private System.Windows.Forms.RadioButton rdoProxyTypeHttp;
        private System.Windows.Forms.CheckBox chkProxy;
        private System.Windows.Forms.NumericUpDown numProxyInternalPort;
        private System.Windows.Forms.NumericUpDown numProxyPort;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtProxyPassword;
        private System.Windows.Forms.TextBox txtProxyInternalUrl;
        private System.Windows.Forms.TextBox txtProxyUsername;
        private System.Windows.Forms.TextBox txtProxyUrl;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.ListView lstAlbums;
        private System.Windows.Forms.ColumnHeader colAlbum;
        private System.Windows.Forms.ColumnHeader colId;
        private System.Windows.Forms.ColumnHeader colFolder;
        private System.Windows.Forms.ContextMenuStrip ctxAlbum;
        private System.Windows.Forms.ToolStripMenuItem ctxAlbumRemove;
        private System.Windows.Forms.ToolStripMenuItem ctxAlbumProperties;
        private System.Windows.Forms.ToolStripMenuItem ctxAlbumSync;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ProgressBar prgSync;
        private System.Windows.Forms.NotifyIcon ntfTray;
        private System.Windows.Forms.ToolStripMenuItem ctxAlbumOpenFolder;
        private System.Windows.Forms.ToolStripMenuItem ctxAlbumOpenAlbum;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    }
}

