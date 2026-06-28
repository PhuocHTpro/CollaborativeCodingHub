namespace CollaborativeCodingClient
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        // === TAB CONTROL ===
        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage tabAuth;
        private System.Windows.Forms.TabPage tabProject;
        private System.Windows.Forms.TabPage tabEditor;

        // === AUTH TAB ===
        private System.Windows.Forms.GroupBox grpLogin;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Button btnRegister;
        private System.Windows.Forms.Button btnLogin;

        private System.Windows.Forms.GroupBox grpRoom;
        private System.Windows.Forms.Label lblRoomName;
        private System.Windows.Forms.Label lblRoomID;
        private System.Windows.Forms.TextBox txtRoomName;
        private System.Windows.Forms.TextBox txtRoomID;
        private System.Windows.Forms.Button btnCreateRoom;
        private System.Windows.Forms.Button btnJoinRoom;

        // === PROJECT TAB ===
        private System.Windows.Forms.GroupBox grpProject;
        private System.Windows.Forms.Label lblProjectName;
        private System.Windows.Forms.Label lblProjectRoomID;
        private System.Windows.Forms.TextBox txtProjectName;
        private System.Windows.Forms.TextBox txtProjectIDRoom;
        private System.Windows.Forms.Button btnCreateProject;

        private System.Windows.Forms.GroupBox grpFile;
        private System.Windows.Forms.Label lblProjectId;
        private System.Windows.Forms.Label lblFileName;
        private System.Windows.Forms.Label lblOpenFileId;
        private System.Windows.Forms.TextBox txtProjectId;
        private System.Windows.Forms.TextBox txtFileName;
        private System.Windows.Forms.TextBox txtOpenFileId;
        private System.Windows.Forms.Button btnCreateFile;
        private System.Windows.Forms.Button btnOpenFile;
        private System.Windows.Forms.Button btnUnlockFile;

        // === EDITOR TAB ===
        private System.Windows.Forms.GroupBox grpEditor;
        private System.Windows.Forms.TextBox txtEditor;
        private System.Windows.Forms.Button btnSaveFile;
        private System.Windows.Forms.Button btnCompile;
        private System.Windows.Forms.GroupBox grpCompile;
        private System.Windows.Forms.TextBox txtCompileResult;

        // === SHARED ===
        private System.Windows.Forms.Panel pnlLog;
        private System.Windows.Forms.Label lblLogTitle;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.Panel pnlStatus;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblCurrentFile;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            // === INSTANTIATE ALL ===
            this.tabMain = new System.Windows.Forms.TabControl();
            this.tabAuth = new System.Windows.Forms.TabPage();
            this.tabProject = new System.Windows.Forms.TabPage();
            this.tabEditor = new System.Windows.Forms.TabPage();

            // Auth Tab
            this.grpLogin = new System.Windows.Forms.GroupBox();
            this.lblUsername = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.btnRegister = new System.Windows.Forms.Button();
            this.btnLogin = new System.Windows.Forms.Button();

            this.grpRoom = new System.Windows.Forms.GroupBox();
            this.lblRoomName = new System.Windows.Forms.Label();
            this.lblRoomID = new System.Windows.Forms.Label();
            this.txtRoomName = new System.Windows.Forms.TextBox();
            this.txtRoomID = new System.Windows.Forms.TextBox();
            this.btnCreateRoom = new System.Windows.Forms.Button();
            this.btnJoinRoom = new System.Windows.Forms.Button();

            // Project Tab
            this.grpProject = new System.Windows.Forms.GroupBox();
            this.lblProjectName = new System.Windows.Forms.Label();
            this.lblProjectRoomID = new System.Windows.Forms.Label();
            this.txtProjectName = new System.Windows.Forms.TextBox();
            this.txtProjectIDRoom = new System.Windows.Forms.TextBox();
            this.btnCreateProject = new System.Windows.Forms.Button();

            this.grpFile = new System.Windows.Forms.GroupBox();
            this.lblProjectId = new System.Windows.Forms.Label();
            this.lblFileName = new System.Windows.Forms.Label();
            this.lblOpenFileId = new System.Windows.Forms.Label();
            this.txtProjectId = new System.Windows.Forms.TextBox();
            this.txtFileName = new System.Windows.Forms.TextBox();
            this.txtOpenFileId = new System.Windows.Forms.TextBox();
            this.btnCreateFile = new System.Windows.Forms.Button();
            this.btnOpenFile = new System.Windows.Forms.Button();
            this.btnUnlockFile = new System.Windows.Forms.Button();

            // Editor Tab
            this.grpEditor = new System.Windows.Forms.GroupBox();
            this.txtEditor = new System.Windows.Forms.TextBox();
            this.btnSaveFile = new System.Windows.Forms.Button();
            this.btnCompile = new System.Windows.Forms.Button();
            this.grpCompile = new System.Windows.Forms.GroupBox();
            this.txtCompileResult = new System.Windows.Forms.TextBox();

            // Shared
            this.pnlLog = new System.Windows.Forms.Panel();
            this.lblLogTitle = new System.Windows.Forms.Label();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.pnlStatus = new System.Windows.Forms.Panel();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblCurrentFile = new System.Windows.Forms.Label();

            this.SuspendLayout();

            // ===========================
            // STATUS BAR (bottom)
            // ===========================
            this.pnlStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlStatus.Height = 28;
            this.pnlStatus.BackColor = System.Drawing.Color.FromArgb(45, 45, 48);
            this.pnlStatus.Padding = new System.Windows.Forms.Padding(8, 0, 8, 0);

            this.lblStatus.AutoSize = false;
            this.lblStatus.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblStatus.ForeColor = System.Drawing.Color.FromArgb(180, 180, 180);
            this.lblStatus.Text = "⬤  Connecting...";
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 8.5f);
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblStatus.Width = 220;

            this.lblCurrentFile.AutoSize = false;
            this.lblCurrentFile.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblCurrentFile.ForeColor = System.Drawing.Color.FromArgb(100, 180, 255);
            this.lblCurrentFile.Text = "No file opened";
            this.lblCurrentFile.Font = new System.Drawing.Font("Segoe UI", 8.5f);
            this.lblCurrentFile.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblCurrentFile.Width = 280;

            this.pnlStatus.Controls.Add(this.lblCurrentFile);
            this.pnlStatus.Controls.Add(this.lblStatus);

            // ===========================
            // LOG PANEL (right)
            // ===========================
            this.pnlLog.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlLog.Width = 300;
            this.pnlLog.BackColor = System.Drawing.Color.FromArgb(30, 30, 35);
            this.pnlLog.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);

            this.lblLogTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblLogTitle.Text = "  📋 Activity Log";
            this.lblLogTitle.Height = 30;
            this.lblLogTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5f);
            this.lblLogTitle.ForeColor = System.Drawing.Color.FromArgb(200, 200, 200);
            this.lblLogTitle.BackColor = System.Drawing.Color.FromArgb(40, 40, 45);
            this.lblLogTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            this.txtLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLog.Multiline = true;
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.BackColor = System.Drawing.Color.FromArgb(20, 20, 25);
            this.txtLog.ForeColor = System.Drawing.Color.FromArgb(180, 220, 180);
            this.txtLog.Font = new System.Drawing.Font("Consolas", 8.5f);
            this.txtLog.BorderStyle = System.Windows.Forms.BorderStyle.None;

            this.pnlLog.Controls.Add(this.txtLog);
            this.pnlLog.Controls.Add(this.lblLogTitle);

            // ===========================
            // TAB CONTROL (left/main)
            // ===========================
            this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMain.Font = new System.Drawing.Font("Segoe UI", 9.5f);
            this.tabMain.Padding = new System.Drawing.Point(12, 5);
            this.tabMain.Controls.Add(this.tabAuth);
            this.tabMain.Controls.Add(this.tabProject);
            this.tabMain.Controls.Add(this.tabEditor);

            // ===========================
            // TAB 1: AUTH & ROOM
            // ===========================
            this.tabAuth.Text = "🔐  Auth & Room";
            this.tabAuth.BackColor = System.Drawing.Color.FromArgb(248, 249, 252);
            this.tabAuth.Padding = new System.Windows.Forms.Padding(10);

            // GroupBox Login
            this.grpLogin.Text = "Authentication";
            this.grpLogin.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5f);
            this.grpLogin.Location = new System.Drawing.Point(14, 14);
            this.grpLogin.Size = new System.Drawing.Size(430, 140);
            this.grpLogin.ForeColor = System.Drawing.Color.FromArgb(50, 80, 160);

            this.lblUsername.Text = "Username:";
            this.lblUsername.Location = new System.Drawing.Point(14, 32);
            this.lblUsername.AutoSize = true;
            this.lblUsername.Font = new System.Drawing.Font("Segoe UI", 9f);

            this.txtUsername.Location = new System.Drawing.Point(14, 50);
            this.txtUsername.Size = new System.Drawing.Size(180, 26);
            this.txtUsername.Font = new System.Drawing.Font("Segoe UI", 9.5f);
            this.txtUsername.PlaceholderText = "Enter username";
            this.txtUsername.Name = "txtUsername";

            this.lblPassword.Text = "Password:";
            this.lblPassword.Location = new System.Drawing.Point(210, 32);
            this.lblPassword.AutoSize = true;
            this.lblPassword.Font = new System.Drawing.Font("Segoe UI", 9f);

            this.txtPassword.Location = new System.Drawing.Point(210, 50);
            this.txtPassword.Size = new System.Drawing.Size(180, 26);
            this.txtPassword.Font = new System.Drawing.Font("Segoe UI", 9.5f);
            this.txtPassword.UseSystemPasswordChar = true;
            this.txtPassword.PlaceholderText = "Enter password";
            this.txtPassword.Name = "txtPassword";

            this.btnRegister.Text = "📝 Register";
            this.btnRegister.Location = new System.Drawing.Point(14, 92);
            this.btnRegister.Size = new System.Drawing.Size(120, 34);
            this.btnRegister.Font = new System.Drawing.Font("Segoe UI", 9.5f);
            this.btnRegister.BackColor = System.Drawing.Color.FromArgb(108, 117, 125);
            this.btnRegister.ForeColor = System.Drawing.Color.White;
            this.btnRegister.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRegister.FlatAppearance.BorderSize = 0;
            this.btnRegister.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.Click += new System.EventHandler(this.btnRegister_Click);

            this.btnLogin.Text = "🔓 Login";
            this.btnLogin.Location = new System.Drawing.Point(144, 92);
            this.btnLogin.Size = new System.Drawing.Size(120, 34);
            this.btnLogin.Font = new System.Drawing.Font("Segoe UI", 9.5f);
            this.btnLogin.BackColor = System.Drawing.Color.FromArgb(0, 120, 215);
            this.btnLogin.ForeColor = System.Drawing.Color.White;
            this.btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogin.FlatAppearance.BorderSize = 0;
            this.btnLogin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);

            this.grpLogin.Controls.Add(this.lblUsername);
            this.grpLogin.Controls.Add(this.txtUsername);
            this.grpLogin.Controls.Add(this.lblPassword);
            this.grpLogin.Controls.Add(this.txtPassword);
            this.grpLogin.Controls.Add(this.btnRegister);
            this.grpLogin.Controls.Add(this.btnLogin);

            // GroupBox Room
            this.grpRoom.Text = "Room Management";
            this.grpRoom.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5f);
            this.grpRoom.Location = new System.Drawing.Point(14, 170);
            this.grpRoom.Size = new System.Drawing.Size(430, 160);
            this.grpRoom.ForeColor = System.Drawing.Color.FromArgb(50, 140, 80);

            this.lblRoomName.Text = "Room Name:";
            this.lblRoomName.Location = new System.Drawing.Point(14, 30);
            this.lblRoomName.AutoSize = true;
            this.lblRoomName.Font = new System.Drawing.Font("Segoe UI", 9f);

            this.txtRoomName.Location = new System.Drawing.Point(14, 48);
            this.txtRoomName.Size = new System.Drawing.Size(190, 26);
            this.txtRoomName.Font = new System.Drawing.Font("Segoe UI", 9.5f);
            this.txtRoomName.PlaceholderText = "e.g. Team Alpha";
            this.txtRoomName.Name = "txtRoomName";

            this.btnCreateRoom.Text = "➕ Create Room";
            this.btnCreateRoom.Location = new System.Drawing.Point(214, 46);
            this.btnCreateRoom.Size = new System.Drawing.Size(130, 32);
            this.btnCreateRoom.Font = new System.Drawing.Font("Segoe UI", 9f);
            this.btnCreateRoom.BackColor = System.Drawing.Color.FromArgb(40, 167, 69);
            this.btnCreateRoom.ForeColor = System.Drawing.Color.White;
            this.btnCreateRoom.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCreateRoom.FlatAppearance.BorderSize = 0;
            this.btnCreateRoom.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCreateRoom.Name = "btnCreateRoom";
            this.btnCreateRoom.Click += new System.EventHandler(this.btnCreateRoom_Click);

            this.lblRoomID.Text = "Room ID (to join):";
            this.lblRoomID.Location = new System.Drawing.Point(14, 92);
            this.lblRoomID.AutoSize = true;
            this.lblRoomID.Font = new System.Drawing.Font("Segoe UI", 9f);

            this.txtRoomID.Location = new System.Drawing.Point(14, 110);
            this.txtRoomID.Size = new System.Drawing.Size(190, 26);
            this.txtRoomID.Font = new System.Drawing.Font("Segoe UI", 9.5f);
            this.txtRoomID.PlaceholderText = "6-char Room ID";
            this.txtRoomID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtRoomID.Name = "txtRoomID";

            this.btnJoinRoom.Text = "🚪 Join Room";
            this.btnJoinRoom.Location = new System.Drawing.Point(214, 108);
            this.btnJoinRoom.Size = new System.Drawing.Size(130, 32);
            this.btnJoinRoom.Font = new System.Drawing.Font("Segoe UI", 9f);
            this.btnJoinRoom.BackColor = System.Drawing.Color.FromArgb(23, 162, 184);
            this.btnJoinRoom.ForeColor = System.Drawing.Color.White;
            this.btnJoinRoom.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnJoinRoom.FlatAppearance.BorderSize = 0;
            this.btnJoinRoom.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnJoinRoom.Name = "btnJoinRoom";
            this.btnJoinRoom.Click += new System.EventHandler(this.btnJoinRoom_Click);

            this.grpRoom.Controls.Add(this.lblRoomName);
            this.grpRoom.Controls.Add(this.txtRoomName);
            this.grpRoom.Controls.Add(this.btnCreateRoom);
            this.grpRoom.Controls.Add(this.lblRoomID);
            this.grpRoom.Controls.Add(this.txtRoomID);
            this.grpRoom.Controls.Add(this.btnJoinRoom);

            this.tabAuth.Controls.Add(this.grpLogin);
            this.tabAuth.Controls.Add(this.grpRoom);

            // ===========================
            // TAB 2: PROJECT & FILE
            // ===========================
            this.tabProject.Text = "📁  Project & Files";
            this.tabProject.BackColor = System.Drawing.Color.FromArgb(248, 249, 252);
            this.tabProject.Padding = new System.Windows.Forms.Padding(10);

            // GroupBox Project
            this.grpProject.Text = "Create Project";
            this.grpProject.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5f);
            this.grpProject.Location = new System.Drawing.Point(14, 14);
            this.grpProject.Size = new System.Drawing.Size(430, 120);
            this.grpProject.ForeColor = System.Drawing.Color.FromArgb(100, 60, 160);

            this.lblProjectName.Text = "Project Name:";
            this.lblProjectName.Location = new System.Drawing.Point(14, 30);
            this.lblProjectName.AutoSize = true;
            this.lblProjectName.Font = new System.Drawing.Font("Segoe UI", 9f);

            this.txtProjectName.Location = new System.Drawing.Point(14, 48);
            this.txtProjectName.Size = new System.Drawing.Size(195, 26);
            this.txtProjectName.Font = new System.Drawing.Font("Segoe UI", 9.5f);
            this.txtProjectName.PlaceholderText = "e.g. MyApp";
            this.txtProjectName.Name = "txtProjectName";

            this.lblProjectRoomID.Text = "Room ID:";
            this.lblProjectRoomID.Location = new System.Drawing.Point(220, 30);
            this.lblProjectRoomID.AutoSize = true;
            this.lblProjectRoomID.Font = new System.Drawing.Font("Segoe UI", 9f);

            this.txtProjectIDRoom.Location = new System.Drawing.Point(220, 48);
            this.txtProjectIDRoom.Size = new System.Drawing.Size(120, 26);
            this.txtProjectIDRoom.Font = new System.Drawing.Font("Segoe UI", 9.5f);
            this.txtProjectIDRoom.PlaceholderText = "Auto-filled";
            this.txtProjectIDRoom.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtProjectIDRoom.Name = "txtProjectIDRoom";

            this.btnCreateProject.Text = "➕ Create Project";
            this.btnCreateProject.Location = new System.Drawing.Point(14, 84);
            this.btnCreateProject.Size = new System.Drawing.Size(150, 24);
            this.btnCreateProject.Font = new System.Drawing.Font("Segoe UI", 9f);
            this.btnCreateProject.BackColor = System.Drawing.Color.FromArgb(111, 66, 193);
            this.btnCreateProject.ForeColor = System.Drawing.Color.White;
            this.btnCreateProject.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCreateProject.FlatAppearance.BorderSize = 0;
            this.btnCreateProject.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCreateProject.Name = "btnCreateProject";
            this.btnCreateProject.Click += new System.EventHandler(this.btnCreateProject_Click);

            this.grpProject.Controls.Add(this.lblProjectName);
            this.grpProject.Controls.Add(this.txtProjectName);
            this.grpProject.Controls.Add(this.lblProjectRoomID);
            this.grpProject.Controls.Add(this.txtProjectIDRoom);
            this.grpProject.Controls.Add(this.btnCreateProject);

            // GroupBox File
            this.grpFile.Text = "File Management";
            this.grpFile.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5f);
            this.grpFile.Location = new System.Drawing.Point(14, 150);
            this.grpFile.Size = new System.Drawing.Size(430, 200);
            this.grpFile.ForeColor = System.Drawing.Color.FromArgb(180, 100, 20);

            // Row 1: Create File
            this.lblProjectId.Text = "Project ID:";
            this.lblProjectId.Location = new System.Drawing.Point(14, 30);
            this.lblProjectId.AutoSize = true;
            this.lblProjectId.Font = new System.Drawing.Font("Segoe UI", 9f);

            this.txtProjectId.Location = new System.Drawing.Point(14, 48);
            this.txtProjectId.Size = new System.Drawing.Size(100, 26);
            this.txtProjectId.Font = new System.Drawing.Font("Segoe UI", 9.5f);
            this.txtProjectId.PlaceholderText = "Auto-filled";
            this.txtProjectId.Name = "txtProjectId";

            this.lblFileName.Text = "File Name:";
            this.lblFileName.Location = new System.Drawing.Point(126, 30);
            this.lblFileName.AutoSize = true;
            this.lblFileName.Font = new System.Drawing.Font("Segoe UI", 9f);

            this.txtFileName.Location = new System.Drawing.Point(126, 48);
            this.txtFileName.Size = new System.Drawing.Size(180, 26);
            this.txtFileName.Font = new System.Drawing.Font("Segoe UI", 9.5f);
            this.txtFileName.PlaceholderText = "e.g. main.py";
            this.txtFileName.Name = "txtFileName";

            this.btnCreateFile.Text = "➕ Create File";
            this.btnCreateFile.Location = new System.Drawing.Point(316, 46);
            this.btnCreateFile.Size = new System.Drawing.Size(100, 30);
            this.btnCreateFile.Font = new System.Drawing.Font("Segoe UI", 9f);
            this.btnCreateFile.BackColor = System.Drawing.Color.FromArgb(220, 130, 0);
            this.btnCreateFile.ForeColor = System.Drawing.Color.White;
            this.btnCreateFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCreateFile.FlatAppearance.BorderSize = 0;
            this.btnCreateFile.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCreateFile.Name = "btnCreateFile";
            this.btnCreateFile.Click += new System.EventHandler(this.btnCreateFile_Click);

            // Row 2: Open / Unlock File
            this.lblOpenFileId.Text = "File ID:";
            this.lblOpenFileId.Location = new System.Drawing.Point(14, 96);
            this.lblOpenFileId.AutoSize = true;
            this.lblOpenFileId.Font = new System.Drawing.Font("Segoe UI", 9f);

            this.txtOpenFileId.Location = new System.Drawing.Point(14, 114);
            this.txtOpenFileId.Size = new System.Drawing.Size(100, 26);
            this.txtOpenFileId.Font = new System.Drawing.Font("Segoe UI", 9.5f);
            this.txtOpenFileId.PlaceholderText = "Auto-filled";
            this.txtOpenFileId.Name = "txtOpenFileId";

            this.btnOpenFile.Text = "📂 Open File";
            this.btnOpenFile.Location = new System.Drawing.Point(126, 112);
            this.btnOpenFile.Size = new System.Drawing.Size(130, 32);
            this.btnOpenFile.Font = new System.Drawing.Font("Segoe UI", 9f);
            this.btnOpenFile.BackColor = System.Drawing.Color.FromArgb(0, 120, 215);
            this.btnOpenFile.ForeColor = System.Drawing.Color.White;
            this.btnOpenFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpenFile.FlatAppearance.BorderSize = 0;
            this.btnOpenFile.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Click += new System.EventHandler(this.btnOpenFile_Click);

            this.btnUnlockFile.Text = "🔓 Unlock File";
            this.btnUnlockFile.Location = new System.Drawing.Point(266, 112);
            this.btnUnlockFile.Size = new System.Drawing.Size(130, 32);
            this.btnUnlockFile.Font = new System.Drawing.Font("Segoe UI", 9f);
            this.btnUnlockFile.BackColor = System.Drawing.Color.FromArgb(200, 60, 60);
            this.btnUnlockFile.ForeColor = System.Drawing.Color.White;
            this.btnUnlockFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUnlockFile.FlatAppearance.BorderSize = 0;
            this.btnUnlockFile.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUnlockFile.Name = "btnUnlockFile";
            this.btnUnlockFile.Click += new System.EventHandler(this.btnUnlockFile_Click);

            // Tip label
            var lblTip = new System.Windows.Forms.Label();
            lblTip.Text = "ℹ️  Mở file sẽ khóa file đó. Dùng 'Unlock File' khi bạn không cần chỉnh sửa nữa để người khác có thể mở.";
            lblTip.Location = new System.Drawing.Point(14, 158);
            lblTip.Size = new System.Drawing.Size(400, 34);
            lblTip.Font = new System.Drawing.Font("Segoe UI", 8.5f, System.Drawing.FontStyle.Italic);
            lblTip.ForeColor = System.Drawing.Color.FromArgb(100, 100, 120);

            this.grpFile.Controls.Add(this.lblProjectId);
            this.grpFile.Controls.Add(this.txtProjectId);
            this.grpFile.Controls.Add(this.lblFileName);
            this.grpFile.Controls.Add(this.txtFileName);
            this.grpFile.Controls.Add(this.btnCreateFile);
            this.grpFile.Controls.Add(this.lblOpenFileId);
            this.grpFile.Controls.Add(this.txtOpenFileId);
            this.grpFile.Controls.Add(this.btnOpenFile);
            this.grpFile.Controls.Add(this.btnUnlockFile);
            this.grpFile.Controls.Add(lblTip);

            this.tabProject.Controls.Add(this.grpProject);
            this.tabProject.Controls.Add(this.grpFile);

            // ===========================
            // TAB 3: EDITOR & COMPILE
            // ===========================
            this.tabEditor.Text = "📝  Editor";
            this.tabEditor.BackColor = System.Drawing.Color.FromArgb(248, 249, 252);

            // Editor GroupBox
            this.grpEditor.Text = "Code Editor";
            this.grpEditor.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5f);
            this.grpEditor.Location = new System.Drawing.Point(8, 6);
            this.grpEditor.Size = new System.Drawing.Size(444, 300);
            this.grpEditor.ForeColor = System.Drawing.Color.FromArgb(20, 80, 140);

            this.txtEditor.Location = new System.Drawing.Point(8, 24);
            this.txtEditor.Size = new System.Drawing.Size(428, 228);
            this.txtEditor.Multiline = true;
            this.txtEditor.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtEditor.Font = new System.Drawing.Font("Consolas", 10.5f);
            this.txtEditor.BackColor = System.Drawing.Color.FromArgb(30, 30, 35);
            this.txtEditor.ForeColor = System.Drawing.Color.FromArgb(220, 220, 170);
            this.txtEditor.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtEditor.AcceptsTab = true;
            this.txtEditor.Name = "txtEditor";

            this.btnSaveFile.Text = "💾 Save File";
            this.btnSaveFile.Location = new System.Drawing.Point(8, 262);
            this.btnSaveFile.Size = new System.Drawing.Size(130, 32);
            this.btnSaveFile.Font = new System.Drawing.Font("Segoe UI", 9.5f);
            this.btnSaveFile.BackColor = System.Drawing.Color.FromArgb(40, 167, 69);
            this.btnSaveFile.ForeColor = System.Drawing.Color.White;
            this.btnSaveFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveFile.FlatAppearance.BorderSize = 0;
            this.btnSaveFile.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSaveFile.Name = "btnSaveFile";
            this.btnSaveFile.Click += new System.EventHandler(this.btnSaveFile_Click);

            this.btnCompile.Text = "⚙️ Compile & Run";
            this.btnCompile.Location = new System.Drawing.Point(148, 262);
            this.btnCompile.Size = new System.Drawing.Size(145, 32);
            this.btnCompile.Font = new System.Drawing.Font("Segoe UI", 9.5f);
            this.btnCompile.BackColor = System.Drawing.Color.FromArgb(220, 120, 0);
            this.btnCompile.ForeColor = System.Drawing.Color.White;
            this.btnCompile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCompile.FlatAppearance.BorderSize = 0;
            this.btnCompile.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCompile.Name = "btnCompile";
            this.btnCompile.Click += new System.EventHandler(this.btnCompile_Click);

            this.grpEditor.Controls.Add(this.txtEditor);
            this.grpEditor.Controls.Add(this.btnSaveFile);
            this.grpEditor.Controls.Add(this.btnCompile);

            // Compile Output GroupBox
            this.grpCompile.Text = "Compile Output";
            this.grpCompile.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5f);
            this.grpCompile.Location = new System.Drawing.Point(8, 312);
            this.grpCompile.Size = new System.Drawing.Size(444, 160);
            this.grpCompile.ForeColor = System.Drawing.Color.FromArgb(140, 60, 20);

            this.txtCompileResult.Location = new System.Drawing.Point(8, 22);
            this.txtCompileResult.Size = new System.Drawing.Size(428, 130);
            this.txtCompileResult.Multiline = true;
            this.txtCompileResult.ReadOnly = true;
            this.txtCompileResult.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtCompileResult.Font = new System.Drawing.Font("Consolas", 9f);
            this.txtCompileResult.BackColor = System.Drawing.Color.FromArgb(25, 25, 30);
            this.txtCompileResult.ForeColor = System.Drawing.Color.FromArgb(200, 240, 200);
            this.txtCompileResult.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCompileResult.Name = "txtCompileResult";

            this.grpCompile.Controls.Add(this.txtCompileResult);

            this.tabEditor.Controls.Add(this.grpEditor);
            this.tabEditor.Controls.Add(this.grpCompile);

            // ===========================
            // FORM SETUP
            // ===========================
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(820, 560);
            this.MinimumSize = new System.Drawing.Size(820, 560);
            this.Text = "CollaborativeCodingHub";
            this.Font = new System.Drawing.Font("Segoe UI", 9f);
            this.BackColor = System.Drawing.Color.FromArgb(240, 243, 250);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;

            this.Controls.Add(this.tabMain);
            this.Controls.Add(this.pnlLog);
            this.Controls.Add(this.pnlStatus);

            this.Load += new System.EventHandler(this.MainForm_Load);

            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
