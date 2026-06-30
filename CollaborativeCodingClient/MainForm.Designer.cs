namespace CollaborativeCodingClient
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage tabAuth;
        private System.Windows.Forms.TabPage tabWorkspace;
        private System.Windows.Forms.TabPage tabEditor;
        private System.Windows.Forms.TabPage tabChat;
        private System.Windows.Forms.TabPage tabTasks;
        private System.Windows.Forms.TabPage tabReplay;

        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtRoomName;
        private System.Windows.Forms.TextBox txtRoomID;
        private System.Windows.Forms.Button btnRegister;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Button btnCreateRoom;
        private System.Windows.Forms.Button btnJoinRoom;
        private System.Windows.Forms.Button btnRefreshMembers;
        private System.Windows.Forms.ListView lstMembers;

        private System.Windows.Forms.TextBox txtProjectName;
        private System.Windows.Forms.TextBox txtProjectIDRoom;
        private System.Windows.Forms.TextBox txtProjectId;
        private System.Windows.Forms.TextBox txtFileName;
        private System.Windows.Forms.TextBox txtOpenFileId;
        private System.Windows.Forms.Button btnCreateProject;
        private System.Windows.Forms.Button btnListProjects;
        private System.Windows.Forms.Button btnCreateFile;
        private System.Windows.Forms.Button btnListFiles;
        private System.Windows.Forms.Button btnOpenFile;
        private System.Windows.Forms.Button btnUnlockFile;
        private System.Windows.Forms.Button btnDeleteFile;
        private System.Windows.Forms.TextBox txtProjectList;
        private System.Windows.Forms.TextBox txtFileList;

        private System.Windows.Forms.TextBox txtEditor;
        private System.Windows.Forms.Button btnSaveFile;
        private System.Windows.Forms.Button btnCompile;
        private System.Windows.Forms.TextBox txtCompileResult;

        private System.Windows.Forms.TextBox txtChatMessages;
        private System.Windows.Forms.TextBox txtChatInput;
        private System.Windows.Forms.Button btnSendChat;

        private System.Windows.Forms.TextBox txtTaskProjectId;
        private System.Windows.Forms.TextBox txtTaskName;
        private System.Windows.Forms.TextBox txtAssignedTo;
        private System.Windows.Forms.TextBox txtTaskId;
        private System.Windows.Forms.ComboBox cmbTaskStatus;
        private System.Windows.Forms.Button btnCreateTask;
        private System.Windows.Forms.Button btnListTasks;
        private System.Windows.Forms.Button btnUpdateTaskStatus;
        private System.Windows.Forms.Button btnDeleteTask;
        private System.Windows.Forms.TextBox txtTaskList;

        private System.Windows.Forms.TextBox txtHistoryFileId;
        private System.Windows.Forms.TextBox txtHistoryId;
        private System.Windows.Forms.Button btnListHistory;
        private System.Windows.Forms.Button btnOpenHistory;
        private System.Windows.Forms.TextBox txtHistoryList;
        private System.Windows.Forms.TextBox txtHistoryPreview;

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
            this.tabMain = new System.Windows.Forms.TabControl();
            this.tabAuth = new System.Windows.Forms.TabPage();
            this.tabWorkspace = new System.Windows.Forms.TabPage();
            this.tabEditor = new System.Windows.Forms.TabPage();
            this.tabChat = new System.Windows.Forms.TabPage();
            this.tabTasks = new System.Windows.Forms.TabPage();
            this.tabReplay = new System.Windows.Forms.TabPage();

            this.txtUsername = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtRoomName = new System.Windows.Forms.TextBox();
            this.txtRoomID = new System.Windows.Forms.TextBox();
            this.btnRegister = new System.Windows.Forms.Button();
            this.btnLogin = new System.Windows.Forms.Button();
            this.btnCreateRoom = new System.Windows.Forms.Button();
            this.btnJoinRoom = new System.Windows.Forms.Button();
            this.btnRefreshMembers = new System.Windows.Forms.Button();
            this.lstMembers = new System.Windows.Forms.ListView();

            this.txtProjectName = new System.Windows.Forms.TextBox();
            this.txtProjectIDRoom = new System.Windows.Forms.TextBox();
            this.txtProjectId = new System.Windows.Forms.TextBox();
            this.txtFileName = new System.Windows.Forms.TextBox();
            this.txtOpenFileId = new System.Windows.Forms.TextBox();
            this.btnCreateProject = new System.Windows.Forms.Button();
            this.btnListProjects = new System.Windows.Forms.Button();
            this.btnCreateFile = new System.Windows.Forms.Button();
            this.btnListFiles = new System.Windows.Forms.Button();
            this.btnOpenFile = new System.Windows.Forms.Button();
            this.btnUnlockFile = new System.Windows.Forms.Button();
            this.btnDeleteFile = new System.Windows.Forms.Button();
            this.txtProjectList = new System.Windows.Forms.TextBox();
            this.txtFileList = new System.Windows.Forms.TextBox();

            this.txtEditor = new System.Windows.Forms.TextBox();
            this.btnSaveFile = new System.Windows.Forms.Button();
            this.btnCompile = new System.Windows.Forms.Button();
            this.txtCompileResult = new System.Windows.Forms.TextBox();

            this.txtChatMessages = new System.Windows.Forms.TextBox();
            this.txtChatInput = new System.Windows.Forms.TextBox();
            this.btnSendChat = new System.Windows.Forms.Button();

            this.txtTaskProjectId = new System.Windows.Forms.TextBox();
            this.txtTaskName = new System.Windows.Forms.TextBox();
            this.txtAssignedTo = new System.Windows.Forms.TextBox();
            this.txtTaskId = new System.Windows.Forms.TextBox();
            this.cmbTaskStatus = new System.Windows.Forms.ComboBox();
            this.btnCreateTask = new System.Windows.Forms.Button();
            this.btnListTasks = new System.Windows.Forms.Button();
            this.btnUpdateTaskStatus = new System.Windows.Forms.Button();
            this.btnDeleteTask = new System.Windows.Forms.Button();
            this.txtTaskList = new System.Windows.Forms.TextBox();

            this.txtHistoryFileId = new System.Windows.Forms.TextBox();
            this.txtHistoryId = new System.Windows.Forms.TextBox();
            this.btnListHistory = new System.Windows.Forms.Button();
            this.btnOpenHistory = new System.Windows.Forms.Button();
            this.txtHistoryList = new System.Windows.Forms.TextBox();
            this.txtHistoryPreview = new System.Windows.Forms.TextBox();

            this.pnlLog = new System.Windows.Forms.Panel();
            this.lblLogTitle = new System.Windows.Forms.Label();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.pnlStatus = new System.Windows.Forms.Panel();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblCurrentFile = new System.Windows.Forms.Label();

            this.SuspendLayout();

            this.ClientSize = new System.Drawing.Size(1120, 720);
            this.MinimumSize = new System.Drawing.Size(1000, 650);
            this.Text = "CollaborativeCodingHub";
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.BackColor = System.Drawing.Color.FromArgb(238, 242, 247);

            this.pnlStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlStatus.Height = 28;
            this.pnlStatus.BackColor = System.Drawing.Color.FromArgb(45, 45, 48);
            this.lblStatus.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblStatus.Width = 360;
            this.lblStatus.Text = "Socket: disconnected";
            this.lblStatus.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblStatus.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.lblCurrentFile.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblCurrentFile.Width = 420;
            this.lblCurrentFile.Text = "No file opened";
            this.lblCurrentFile.ForeColor = System.Drawing.Color.FromArgb(120, 190, 255);
            this.lblCurrentFile.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblCurrentFile.Padding = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.pnlStatus.Controls.Add(this.lblCurrentFile);
            this.pnlStatus.Controls.Add(this.lblStatus);

            this.pnlLog.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlLog.Width = 310;
            this.pnlLog.BackColor = System.Drawing.Color.FromArgb(30, 32, 36);
            this.lblLogTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblLogTitle.Height = 34;
            this.lblLogTitle.Text = "Activity Log";
            this.lblLogTitle.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblLogTitle.BackColor = System.Drawing.Color.FromArgb(43, 47, 54);
            this.lblLogTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblLogTitle.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.txtLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLog.Multiline = true;
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtLog.BackColor = System.Drawing.Color.FromArgb(24, 26, 30);
            this.txtLog.ForeColor = System.Drawing.Color.FromArgb(185, 220, 185);
            this.txtLog.Font = new System.Drawing.Font("Consolas", 8.5F);
            this.pnlLog.Controls.Add(this.txtLog);
            this.pnlLog.Controls.Add(this.lblLogTitle);

            this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMain.Padding = new System.Drawing.Point(14, 5);
            this.tabMain.Controls.Add(this.tabAuth);
            this.tabMain.Controls.Add(this.tabWorkspace);
            this.tabMain.Controls.Add(this.tabEditor);
            this.tabMain.Controls.Add(this.tabChat);
            this.tabMain.Controls.Add(this.tabTasks);
            this.tabMain.Controls.Add(this.tabReplay);

            ConfigureTab(this.tabAuth, "Auth & Room");
            ConfigureTab(this.tabWorkspace, "Workspace");
            ConfigureTab(this.tabEditor, "Editor");
            ConfigureTab(this.tabChat, "Team Chat");
            ConfigureTab(this.tabTasks, "Tasks");
            ConfigureTab(this.tabReplay, "Replay");

            BuildAuthTab();
            BuildWorkspaceTab();
            BuildEditorTab();
            BuildChatTab();
            BuildTasksTab();
            BuildReplayTab();

            this.Controls.Add(this.tabMain);
            this.Controls.Add(this.pnlLog);
            this.Controls.Add(this.pnlStatus);
            this.Load += new System.EventHandler(this.MainForm_Load);

            this.ResumeLayout(false);
        }

        private static void ConfigureTab(System.Windows.Forms.TabPage tab, string text)
        {
            tab.Text = text;
            tab.BackColor = System.Drawing.Color.FromArgb(248, 249, 252);
            tab.Padding = new System.Windows.Forms.Padding(10);
        }

        private static System.Windows.Forms.GroupBox Group(string title, int x, int y, int w, int h)
        {
            return new System.Windows.Forms.GroupBox
            {
                Text = title,
                Location = new System.Drawing.Point(x, y),
                Size = new System.Drawing.Size(w, h),
                ForeColor = System.Drawing.Color.FromArgb(45, 75, 120)
            };
        }

        private static System.Windows.Forms.Label Label(string text, int x, int y)
        {
            return new System.Windows.Forms.Label
            {
                Text = text,
                Location = new System.Drawing.Point(x, y),
                AutoSize = true,
                ForeColor = System.Drawing.Color.FromArgb(35, 38, 44)
            };
        }

        private static void StyleButton(System.Windows.Forms.Button button, System.Drawing.Color color)
        {
            button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.BackColor = color;
            button.ForeColor = System.Drawing.Color.White;
            button.Cursor = System.Windows.Forms.Cursors.Hand;
        }

        private static void StyleMultiline(System.Windows.Forms.TextBox textBox, bool code = false)
        {
            textBox.Multiline = true;
            textBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            textBox.Font = code
                ? new System.Drawing.Font("Consolas", 10F)
                : new System.Drawing.Font("Consolas", 9F);
            textBox.BackColor = code
                ? System.Drawing.Color.FromArgb(28, 30, 35)
                : System.Drawing.Color.White;
            textBox.ForeColor = code
                ? System.Drawing.Color.FromArgb(230, 230, 200)
                : System.Drawing.Color.FromArgb(30, 34, 40);
        }

        private void BuildAuthTab()
        {
            var grpLogin = Group("Authentication", 14, 14, 430, 150);
            grpLogin.Controls.Add(Label("Username", 14, 30));
            this.txtUsername.Location = new System.Drawing.Point(14, 50);
            this.txtUsername.Size = new System.Drawing.Size(180, 26);
            grpLogin.Controls.Add(this.txtUsername);
            grpLogin.Controls.Add(Label("Password", 210, 30));
            this.txtPassword.Location = new System.Drawing.Point(210, 50);
            this.txtPassword.Size = new System.Drawing.Size(180, 26);
            this.txtPassword.UseSystemPasswordChar = true;
            grpLogin.Controls.Add(this.txtPassword);
            this.btnRegister.Text = "Register";
            this.btnRegister.Location = new System.Drawing.Point(14, 96);
            this.btnRegister.Size = new System.Drawing.Size(120, 34);
            this.btnRegister.Click += new System.EventHandler(this.btnRegister_Click);
            StyleButton(this.btnRegister, System.Drawing.Color.FromArgb(108, 117, 125));
            grpLogin.Controls.Add(this.btnRegister);
            this.btnLogin.Text = "Login";
            this.btnLogin.Location = new System.Drawing.Point(144, 96);
            this.btnLogin.Size = new System.Drawing.Size(120, 34);
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            StyleButton(this.btnLogin, System.Drawing.Color.FromArgb(0, 120, 215));
            grpLogin.Controls.Add(this.btnLogin);

            var grpRoom = Group("Room Management", 14, 180, 430, 170);
            grpRoom.Controls.Add(Label("Room Name", 14, 30));
            this.txtRoomName.Location = new System.Drawing.Point(14, 50);
            this.txtRoomName.Size = new System.Drawing.Size(190, 26);
            grpRoom.Controls.Add(this.txtRoomName);
            this.btnCreateRoom.Text = "Create Room";
            this.btnCreateRoom.Location = new System.Drawing.Point(220, 48);
            this.btnCreateRoom.Size = new System.Drawing.Size(130, 31);
            this.btnCreateRoom.Click += new System.EventHandler(this.btnCreateRoom_Click);
            StyleButton(this.btnCreateRoom, System.Drawing.Color.FromArgb(40, 167, 69));
            grpRoom.Controls.Add(this.btnCreateRoom);
            grpRoom.Controls.Add(Label("Room ID", 14, 94));
            this.txtRoomID.Location = new System.Drawing.Point(14, 114);
            this.txtRoomID.Size = new System.Drawing.Size(190, 26);
            this.txtRoomID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            grpRoom.Controls.Add(this.txtRoomID);
            this.btnJoinRoom.Text = "Join Room";
            this.btnJoinRoom.Location = new System.Drawing.Point(220, 112);
            this.btnJoinRoom.Size = new System.Drawing.Size(130, 31);
            this.btnJoinRoom.Click += new System.EventHandler(this.btnJoinRoom_Click);
            StyleButton(this.btnJoinRoom, System.Drawing.Color.FromArgb(23, 162, 184));
            grpRoom.Controls.Add(this.btnJoinRoom);

            var grpMembers = Group("Room Members", 470, 14, 330, 336);
            this.lstMembers.Location = new System.Drawing.Point(12, 28);
            this.lstMembers.Size = new System.Drawing.Size(306, 248);
            this.lstMembers.View = System.Windows.Forms.View.Details;
            this.lstMembers.FullRowSelect = true;
            this.lstMembers.Columns.Add("User", 120);
            this.lstMembers.Columns.Add("Role", 80);
            this.lstMembers.Columns.Add("Status", 90);
            grpMembers.Controls.Add(this.lstMembers);
            this.btnRefreshMembers.Text = "Refresh Members";
            this.btnRefreshMembers.Location = new System.Drawing.Point(12, 286);
            this.btnRefreshMembers.Size = new System.Drawing.Size(150, 31);
            this.btnRefreshMembers.Click += new System.EventHandler(this.btnRefreshMembers_Click);
            StyleButton(this.btnRefreshMembers, System.Drawing.Color.FromArgb(0, 120, 215));
            grpMembers.Controls.Add(this.btnRefreshMembers);

            this.tabAuth.Controls.Add(grpLogin);
            this.tabAuth.Controls.Add(grpRoom);
            this.tabAuth.Controls.Add(grpMembers);
        }

        private void BuildWorkspaceTab()
        {
            var grpProject = Group("Project", 14, 14, 760, 130);
            grpProject.Controls.Add(Label("Project Name", 14, 30));
            this.txtProjectName.Location = new System.Drawing.Point(14, 50);
            this.txtProjectName.Size = new System.Drawing.Size(210, 26);
            grpProject.Controls.Add(this.txtProjectName);
            grpProject.Controls.Add(Label("Room ID", 240, 30));
            this.txtProjectIDRoom.Location = new System.Drawing.Point(240, 50);
            this.txtProjectIDRoom.Size = new System.Drawing.Size(120, 26);
            this.txtProjectIDRoom.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            grpProject.Controls.Add(this.txtProjectIDRoom);
            this.btnCreateProject.Text = "Create Project";
            this.btnCreateProject.Location = new System.Drawing.Point(380, 48);
            this.btnCreateProject.Size = new System.Drawing.Size(130, 31);
            this.btnCreateProject.Click += new System.EventHandler(this.btnCreateProject_Click);
            StyleButton(this.btnCreateProject, System.Drawing.Color.FromArgb(111, 66, 193));
            grpProject.Controls.Add(this.btnCreateProject);
            this.btnListProjects.Text = "List Projects";
            this.btnListProjects.Location = new System.Drawing.Point(520, 48);
            this.btnListProjects.Size = new System.Drawing.Size(120, 31);
            this.btnListProjects.Click += new System.EventHandler(this.btnListProjects_Click);
            StyleButton(this.btnListProjects, System.Drawing.Color.FromArgb(0, 120, 215));
            grpProject.Controls.Add(this.btnListProjects);

            var grpFile = Group("File Management", 14, 158, 760, 190);
            grpFile.Controls.Add(Label("Project ID", 14, 30));
            this.txtProjectId.Location = new System.Drawing.Point(14, 50);
            this.txtProjectId.Size = new System.Drawing.Size(90, 26);
            grpFile.Controls.Add(this.txtProjectId);
            grpFile.Controls.Add(Label("File Name", 120, 30));
            this.txtFileName.Location = new System.Drawing.Point(120, 50);
            this.txtFileName.Size = new System.Drawing.Size(170, 26);
            grpFile.Controls.Add(this.txtFileName);
            this.btnCreateFile.Text = "Create File";
            this.btnCreateFile.Location = new System.Drawing.Point(306, 48);
            this.btnCreateFile.Size = new System.Drawing.Size(110, 31);
            this.btnCreateFile.Click += new System.EventHandler(this.btnCreateFile_Click);
            StyleButton(this.btnCreateFile, System.Drawing.Color.FromArgb(220, 130, 0));
            grpFile.Controls.Add(this.btnCreateFile);
            this.btnListFiles.Text = "List Files";
            this.btnListFiles.Location = new System.Drawing.Point(426, 48);
            this.btnListFiles.Size = new System.Drawing.Size(100, 31);
            this.btnListFiles.Click += new System.EventHandler(this.btnListFiles_Click);
            StyleButton(this.btnListFiles, System.Drawing.Color.FromArgb(0, 120, 215));
            grpFile.Controls.Add(this.btnListFiles);
            grpFile.Controls.Add(Label("File ID", 14, 104));
            this.txtOpenFileId.Location = new System.Drawing.Point(14, 124);
            this.txtOpenFileId.Size = new System.Drawing.Size(90, 26);
            grpFile.Controls.Add(this.txtOpenFileId);
            this.btnOpenFile.Text = "Open File";
            this.btnOpenFile.Location = new System.Drawing.Point(120, 122);
            this.btnOpenFile.Size = new System.Drawing.Size(120, 31);
            this.btnOpenFile.Click += new System.EventHandler(this.btnOpenFile_Click);
            StyleButton(this.btnOpenFile, System.Drawing.Color.FromArgb(0, 120, 215));
            grpFile.Controls.Add(this.btnOpenFile);
            this.btnUnlockFile.Text = "Unlock File";
            this.btnUnlockFile.Location = new System.Drawing.Point(250, 122);
            this.btnUnlockFile.Size = new System.Drawing.Size(120, 31);
            this.btnUnlockFile.Click += new System.EventHandler(this.btnUnlockFile_Click);
            StyleButton(this.btnUnlockFile, System.Drawing.Color.FromArgb(190, 60, 60));
            grpFile.Controls.Add(this.btnUnlockFile);
            this.btnDeleteFile.Text = "Delete File";
            this.btnDeleteFile.Location = new System.Drawing.Point(380, 122);
            this.btnDeleteFile.Size = new System.Drawing.Size(120, 31);
            this.btnDeleteFile.Click += new System.EventHandler(this.btnDeleteFile_Click);
            StyleButton(this.btnDeleteFile, System.Drawing.Color.FromArgb(160, 60, 60));
            grpFile.Controls.Add(this.btnDeleteFile);

            var grpLists = Group("Workspace Lists", 14, 362, 760, 250);
            this.txtProjectList.Location = new System.Drawing.Point(12, 26);
            this.txtProjectList.Size = new System.Drawing.Size(360, 208);
            this.txtProjectList.ReadOnly = true;
            StyleMultiline(this.txtProjectList);
            this.txtProjectList.Text = "Projects will appear here.";
            grpLists.Controls.Add(this.txtProjectList);
            this.txtFileList.Location = new System.Drawing.Point(386, 26);
            this.txtFileList.Size = new System.Drawing.Size(360, 208);
            this.txtFileList.ReadOnly = true;
            StyleMultiline(this.txtFileList);
            this.txtFileList.Text = "Files will appear here.";
            grpLists.Controls.Add(this.txtFileList);

            this.tabWorkspace.Controls.Add(grpProject);
            this.tabWorkspace.Controls.Add(grpFile);
            this.tabWorkspace.Controls.Add(grpLists);
        }

        private void BuildEditorTab()
        {
            this.txtEditor.Location = new System.Drawing.Point(14, 14);
            this.txtEditor.Size = new System.Drawing.Size(760, 395);
            this.txtEditor.AcceptsTab = true;
            StyleMultiline(this.txtEditor, true);
            this.btnSaveFile.Text = "Save File";
            this.btnSaveFile.Location = new System.Drawing.Point(14, 420);
            this.btnSaveFile.Size = new System.Drawing.Size(120, 32);
            this.btnSaveFile.Click += new System.EventHandler(this.btnSaveFile_Click);
            StyleButton(this.btnSaveFile, System.Drawing.Color.FromArgb(40, 167, 69));
            this.btnCompile.Text = "Compile & Run";
            this.btnCompile.Location = new System.Drawing.Point(146, 420);
            this.btnCompile.Size = new System.Drawing.Size(135, 32);
            this.btnCompile.Click += new System.EventHandler(this.btnCompile_Click);
            StyleButton(this.btnCompile, System.Drawing.Color.FromArgb(220, 120, 0));
            this.txtCompileResult.Location = new System.Drawing.Point(14, 468);
            this.txtCompileResult.Size = new System.Drawing.Size(760, 144);
            this.txtCompileResult.ReadOnly = true;
            StyleMultiline(this.txtCompileResult, true);
            this.txtCompileResult.Text = "Compile output will appear here.";

            this.tabEditor.Controls.Add(this.txtEditor);
            this.tabEditor.Controls.Add(this.btnSaveFile);
            this.tabEditor.Controls.Add(this.btnCompile);
            this.tabEditor.Controls.Add(this.txtCompileResult);
        }

        private void BuildChatTab()
        {
            this.txtChatMessages.Location = new System.Drawing.Point(14, 14);
            this.txtChatMessages.Size = new System.Drawing.Size(760, 520);
            this.txtChatMessages.ReadOnly = true;
            StyleMultiline(this.txtChatMessages);
            this.txtChatMessages.Text = "Team chat messages will appear here.";
            this.txtChatInput.Location = new System.Drawing.Point(14, 550);
            this.txtChatInput.Size = new System.Drawing.Size(620, 26);
            this.btnSendChat.Text = "Send";
            this.btnSendChat.Location = new System.Drawing.Point(646, 548);
            this.btnSendChat.Size = new System.Drawing.Size(128, 31);
            this.btnSendChat.Click += new System.EventHandler(this.btnSendChat_Click);
            StyleButton(this.btnSendChat, System.Drawing.Color.FromArgb(0, 120, 215));

            this.tabChat.Controls.Add(this.txtChatMessages);
            this.tabChat.Controls.Add(this.txtChatInput);
            this.tabChat.Controls.Add(this.btnSendChat);
        }

        private void BuildTasksTab()
        {
            var grpForm = Group("Create / Update Task", 14, 14, 760, 155);
            grpForm.Controls.Add(Label("Project ID", 14, 30));
            this.txtTaskProjectId.Location = new System.Drawing.Point(14, 50);
            this.txtTaskProjectId.Size = new System.Drawing.Size(90, 26);
            grpForm.Controls.Add(this.txtTaskProjectId);
            grpForm.Controls.Add(Label("Task Name", 120, 30));
            this.txtTaskName.Location = new System.Drawing.Point(120, 50);
            this.txtTaskName.Size = new System.Drawing.Size(210, 26);
            grpForm.Controls.Add(this.txtTaskName);
            grpForm.Controls.Add(Label("Assigned UserID", 346, 30));
            this.txtAssignedTo.Location = new System.Drawing.Point(346, 50);
            this.txtAssignedTo.Size = new System.Drawing.Size(90, 26);
            grpForm.Controls.Add(this.txtAssignedTo);
            this.btnCreateTask.Text = "Create Task";
            this.btnCreateTask.Location = new System.Drawing.Point(456, 48);
            this.btnCreateTask.Size = new System.Drawing.Size(115, 31);
            this.btnCreateTask.Click += new System.EventHandler(this.btnCreateTask_Click);
            StyleButton(this.btnCreateTask, System.Drawing.Color.FromArgb(40, 167, 69));
            grpForm.Controls.Add(this.btnCreateTask);
            this.btnListTasks.Text = "List Tasks";
            this.btnListTasks.Location = new System.Drawing.Point(584, 48);
            this.btnListTasks.Size = new System.Drawing.Size(105, 31);
            this.btnListTasks.Click += new System.EventHandler(this.btnListTasks_Click);
            StyleButton(this.btnListTasks, System.Drawing.Color.FromArgb(0, 120, 215));
            grpForm.Controls.Add(this.btnListTasks);
            grpForm.Controls.Add(Label("Task ID", 14, 96));
            this.txtTaskId.Location = new System.Drawing.Point(14, 116);
            this.txtTaskId.Size = new System.Drawing.Size(90, 26);
            grpForm.Controls.Add(this.txtTaskId);
            grpForm.Controls.Add(Label("Status", 120, 96));
            this.cmbTaskStatus.Location = new System.Drawing.Point(120, 116);
            this.cmbTaskStatus.Size = new System.Drawing.Size(150, 26);
            this.cmbTaskStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTaskStatus.Items.AddRange(new object[] { "TODO", "IN_PROGRESS", "DONE" });
            this.cmbTaskStatus.SelectedIndex = 0;
            grpForm.Controls.Add(this.cmbTaskStatus);
            this.btnUpdateTaskStatus.Text = "Update Status";
            this.btnUpdateTaskStatus.Location = new System.Drawing.Point(286, 114);
            this.btnUpdateTaskStatus.Size = new System.Drawing.Size(130, 31);
            this.btnUpdateTaskStatus.Click += new System.EventHandler(this.btnUpdateTaskStatus_Click);
            StyleButton(this.btnUpdateTaskStatus, System.Drawing.Color.FromArgb(111, 66, 193));
            grpForm.Controls.Add(this.btnUpdateTaskStatus);
            this.btnDeleteTask.Text = "Delete Task";
            this.btnDeleteTask.Location = new System.Drawing.Point(430, 114);
            this.btnDeleteTask.Size = new System.Drawing.Size(120, 31);
            this.btnDeleteTask.Click += new System.EventHandler(this.btnDeleteTask_Click);
            StyleButton(this.btnDeleteTask, System.Drawing.Color.FromArgb(160, 60, 60));
            grpForm.Controls.Add(this.btnDeleteTask);

            this.txtTaskList.Location = new System.Drawing.Point(14, 184);
            this.txtTaskList.Size = new System.Drawing.Size(760, 428);
            this.txtTaskList.ReadOnly = true;
            StyleMultiline(this.txtTaskList);
            this.txtTaskList.Text = "Tasks will appear here.";

            this.tabTasks.Controls.Add(grpForm);
            this.tabTasks.Controls.Add(this.txtTaskList);
        }

        private void BuildReplayTab()
        {
            var grpActions = Group("Coding Replay / File History", 14, 14, 760, 95);
            grpActions.Controls.Add(Label("File ID", 14, 30));
            this.txtHistoryFileId.Location = new System.Drawing.Point(14, 50);
            this.txtHistoryFileId.Size = new System.Drawing.Size(90, 26);
            grpActions.Controls.Add(this.txtHistoryFileId);
            this.btnListHistory.Text = "List History";
            this.btnListHistory.Location = new System.Drawing.Point(120, 48);
            this.btnListHistory.Size = new System.Drawing.Size(120, 31);
            this.btnListHistory.Click += new System.EventHandler(this.btnListHistory_Click);
            StyleButton(this.btnListHistory, System.Drawing.Color.FromArgb(0, 120, 215));
            grpActions.Controls.Add(this.btnListHistory);
            grpActions.Controls.Add(Label("History ID", 270, 30));
            this.txtHistoryId.Location = new System.Drawing.Point(270, 50);
            this.txtHistoryId.Size = new System.Drawing.Size(90, 26);
            grpActions.Controls.Add(this.txtHistoryId);
            this.btnOpenHistory.Text = "Open Version";
            this.btnOpenHistory.Location = new System.Drawing.Point(376, 48);
            this.btnOpenHistory.Size = new System.Drawing.Size(130, 31);
            this.btnOpenHistory.Click += new System.EventHandler(this.btnOpenHistory_Click);
            StyleButton(this.btnOpenHistory, System.Drawing.Color.FromArgb(111, 66, 193));
            grpActions.Controls.Add(this.btnOpenHistory);

            this.txtHistoryList.Location = new System.Drawing.Point(14, 124);
            this.txtHistoryList.Size = new System.Drawing.Size(370, 488);
            this.txtHistoryList.ReadOnly = true;
            StyleMultiline(this.txtHistoryList);
            this.txtHistoryList.Text = "History list will appear here.";
            this.txtHistoryPreview.Location = new System.Drawing.Point(404, 124);
            this.txtHistoryPreview.Size = new System.Drawing.Size(370, 488);
            this.txtHistoryPreview.ReadOnly = true;
            StyleMultiline(this.txtHistoryPreview, true);
            this.txtHistoryPreview.Text = "Selected version content will appear here.";

            this.tabReplay.Controls.Add(grpActions);
            this.tabReplay.Controls.Add(this.txtHistoryList);
            this.tabReplay.Controls.Add(this.txtHistoryPreview);
        }
    }
}
