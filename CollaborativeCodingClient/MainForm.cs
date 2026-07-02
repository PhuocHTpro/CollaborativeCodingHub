using CollaborativeCodingClient.Models.Packets.Auth;
using CollaborativeCodingClient.Models.Packets.Project;
using CollaborativeCodingClient.Models.Packets.Replay;
using CollaborativeCodingClient.Models.Packets.Room;
using CollaborativeCodingClient.Models.Packets.Task;
using CollaborativeCodingClient.Network;
using System.Text;
using System.Windows.Forms;

namespace CollaborativeCodingClient
{
    public partial class MainForm : Form
    {
        private readonly ClientManager client;
        private string currentUsername = "";

        public MainForm()
        {
            InitializeComponent();
            client = new ClientManager();
            client.PacketReceived += OnPacketReceived;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            client.Connect("127.0.0.1", 5000);
            //client.Connect("10.102.84.5", 5000);
            SetStatus("Socket: connecting to 127.0.0.1:5000", System.Drawing.Color.FromArgb(220, 200, 90));
            AppendLog("Connecting to server at 127.0.0.1:5000...");
        }

        private void OnPacketReceived(Packet packet)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => OnPacketReceived(packet)));
                return;
            }

            switch (packet.Type)
            {
                case nameof(PacketType.LOGIN_SUCCESS):
                    currentUsername = txtUsername.Text.Trim();
                    SetStatus($"Socket: connected | User: {currentUsername}", System.Drawing.Color.FromArgb(120, 220, 140));
                    AppendLog($"LOGIN_SUCCESS {currentUsername}");
                    break;

                case nameof(PacketType.LOGIN_FAILED):
                    AppendLog("LOGIN_FAILED invalid username or password.");
                    break;

                case nameof(PacketType.REGISTER_SUCCESS):
                    AppendLog("REGISTER_SUCCESS account created.");
                    break;

                case nameof(PacketType.REGISTER_FAILED):
                    AppendLog("REGISTER_FAILED username already exists.");
                    break;

                case nameof(PacketType.CHAT):
                    AppendChat(packet.Data);
                    AppendLog("CHAT received.");
                    break;

                case nameof(PacketType.CREATE_ROOM_SUCCESS):
                    txtRoomID.Text = packet.Data;
                    txtProjectIDRoom.Text = packet.Data;
                    SetStatus($"Socket: connected | User: {currentUsername} | Room: {packet.Data}", System.Drawing.Color.FromArgb(120, 220, 140));
                    AppendLog($"CREATE_ROOM_SUCCESS RoomID={packet.Data}");
                    RequestRoomMembers();
                    break;

                case nameof(PacketType.CREATE_ROOM_FAILED):
                    AppendLog("CREATE_ROOM_FAILED login is required or room is invalid.");
                    break;

                case nameof(PacketType.JOIN_ROOM_SUCCESS):
                    txtProjectIDRoom.Text = packet.Data;
                    SetStatus($"Socket: connected | User: {currentUsername} | Room: {packet.Data}", System.Drawing.Color.FromArgb(120, 220, 140));
                    AppendLog($"JOIN_ROOM_SUCCESS RoomID={packet.Data}");
                    RequestRoomMembers();
                    break;

                case nameof(PacketType.JOIN_ROOM_FAILED):
                case nameof(PacketType.ROOM_NOT_FOUND):
                    AppendLog("JOIN_ROOM_FAILED room not found or invalid.");
                    break;

                case nameof(PacketType.LIST_ROOM_MEMBERS_SUCCESS):
                    RenderRoomMembers(packet.Data);
                    AppendLog("LIST_ROOM_MEMBERS_SUCCESS members updated.");
                    break;

                case nameof(PacketType.LIST_ROOM_MEMBERS_FAILED):
                    AppendLog($"LIST_ROOM_MEMBERS_FAILED {packet.Data}");
                    break;

                case nameof(PacketType.CREATE_PROJECT_SUCCESS):
                    txtProjectId.Text = packet.Data;
                    txtTaskProjectId.Text = packet.Data;
                    AppendLog($"CREATE_PROJECT_SUCCESS ProjectID={packet.Data}");
                    break;

                case nameof(PacketType.CREATE_PROJECT_FAILED):
                    AppendLog("CREATE_PROJECT_FAILED check login, room id, and joined room.");
                    break;

                case nameof(PacketType.LIST_PROJECTS):
                    txtProjectList.Text = string.IsNullOrWhiteSpace(packet.Data) ? "No projects returned." : packet.Data;
                    AppendLog("LIST_PROJECTS updated.");
                    break;

                case nameof(PacketType.CREATE_FILE_SUCCESS):
                    txtOpenFileId.Text = packet.Data;
                    txtHistoryFileId.Text = packet.Data;
                    AppendLog($"CREATE_FILE_SUCCESS FileID={packet.Data}");
                    break;

                case nameof(PacketType.CREATE_FILE_FAILED):
                    AppendLog("CREATE_FILE_FAILED check project id.");
                    break;

                case nameof(PacketType.LIST_FILES):
                    txtFileList.Text = string.IsNullOrWhiteSpace(packet.Data) ? "No files returned." : packet.Data;
                    AppendLog("LIST_FILES updated.");
                    break;

                case nameof(PacketType.DELETE_FILE_SUCCESS):
                    if (int.TryParse(packet.Data, out int deletedFileId) && client.CurrentFileID == deletedFileId)
                    {
                        client.CurrentFileID = 0;
                        client.CurrentFileContent = string.Empty;
                        lblCurrentFile.Text = "No file opened";
                    }
                    AppendLog($"DELETE_FILE_SUCCESS FileID={packet.Data}");
                    break;

                case nameof(PacketType.DELETE_FILE_FAILED):
                    AppendLog($"DELETE_FILE_FAILED {packet.Data}");
                    break;

                case nameof(PacketType.OPEN_FILE):
                    HandleOpenFile(packet);
                    break;

                case nameof(PacketType.FILE_NOT_FOUND):
                    AppendLog("FILE_NOT_FOUND requested file does not exist or is outside current room.");
                    break;

                case nameof(PacketType.FILE_LOCKED):
                    lblCurrentFile.Text = $"File locked by {packet.Data}";
                    AppendLog($"FILE_LOCKED owner={packet.Data}");
                    break;

                case nameof(PacketType.UPDATE_FILE_SUCCESS):
                    AppendLog("UPDATE_FILE_SUCCESS content saved.");
                    break;

                case nameof(PacketType.UPDATE_FILE_FAILED):
                    AppendLog($"UPDATE_FILE_FAILED {packet.Data}");
                    break;

                case nameof(PacketType.UNLOCK_FILE_SUCCESS):
                    txtEditor.ReadOnly = false;
                    lblCurrentFile.Text = "No file opened";
                    client.CurrentFileID = 0;
                    client.CurrentFileContent = string.Empty;
                    AppendLog($"UNLOCK_FILE_SUCCESS FileID={packet.Data}");
                    break;

                case nameof(PacketType.UNLOCK_FILE_FAILED):
                    AppendLog($"UNLOCK_FILE_FAILED {packet.Data}");
                    break;

                case nameof(PacketType.SYNC_FILE_CONTENT):
                    HandleSyncFile(packet);
                    break;

                case nameof(PacketType.COMPILE_SUCCESS):
                    txtCompileResult.ForeColor = System.Drawing.Color.FromArgb(190, 245, 190);
                    txtCompileResult.Text = packet.Data;
                    tabMain.SelectedTab = tabEditor;
                    AppendLog("COMPILE_SUCCESS output updated.");
                    break;

                case nameof(PacketType.COMPILE_FAILED):
                    txtCompileResult.ForeColor = System.Drawing.Color.FromArgb(255, 140, 140);
                    txtCompileResult.Text = packet.Data;
                    tabMain.SelectedTab = tabEditor;
                    AppendLog("COMPILE_FAILED output updated.");
                    break;

                case nameof(PacketType.LIST_HISTORY_SUCCESS):
                    RenderHistoryList(packet.Data);
                    tabMain.SelectedTab = tabReplay;
                    AppendLog("LIST_HISTORY_SUCCESS history updated.");
                    break;

                case nameof(PacketType.LIST_HISTORY_FAILED):
                    txtHistoryList.Text = packet.Data;
                    AppendLog($"LIST_HISTORY_FAILED {packet.Data}");
                    break;

                case nameof(PacketType.OPEN_HISTORY_SUCCESS):
                    txtHistoryPreview.Text = packet.Data;
                    tabMain.SelectedTab = tabReplay;
                    AppendLog("OPEN_HISTORY_SUCCESS preview updated.");
                    break;

                case nameof(PacketType.OPEN_HISTORY_FAILED):
                    txtHistoryPreview.Text = packet.Data;
                    AppendLog($"OPEN_HISTORY_FAILED {packet.Data}");
                    break;

                case nameof(PacketType.CREATE_TASK_SUCCESS):
                    AppendLog("CREATE_TASK_SUCCESS task created.");
                    RequestTaskList();
                    break;

                case nameof(PacketType.CREATE_TASK_FAILED):
                    AppendLog("CREATE_TASK_FAILED check project id, task name, and assigned user id.");
                    break;

                case nameof(PacketType.LIST_TASKS_SUCCESS):
                    txtTaskList.Text = packet.Data;
                    tabMain.SelectedTab = tabTasks;
                    AppendLog("LIST_TASKS_SUCCESS task list updated.");
                    break;

                case nameof(PacketType.LIST_TASKS_FAILED):
                    txtTaskList.Text = packet.Data;
                    AppendLog($"LIST_TASKS_FAILED {packet.Data}");
                    break;

                case nameof(PacketType.UPDATE_TASK_STATUS_SUCCESS):
                    AppendLog("UPDATE_TASK_STATUS_SUCCESS task updated.");
                    RequestTaskList();
                    break;

                case nameof(PacketType.UPDATE_TASK_STATUS_FAILED):
                    AppendLog("UPDATE_TASK_STATUS_FAILED.");
                    break;

                case nameof(PacketType.DELETE_TASK_SUCCESS):
                    AppendLog("DELETE_TASK_SUCCESS task deleted.");
                    RequestTaskList();
                    break;

                case nameof(PacketType.DELETE_TASK_FAILED):
                    AppendLog($"DELETE_TASK_FAILED {packet.Data}");
                    break;

                case nameof(PacketType.ACCESS_DENIED):
                    AppendLog($"ACCESS_DENIED {packet.Data}");
                    break;

                default:
                    if (!string.IsNullOrWhiteSpace(packet.Data))
                        AppendLog($"{packet.Type}: {packet.Data}");
                    else
                        AppendLog(packet.Type);
                    break;
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                AppendLog("Username and password are required.");
                return;
            }

            SendPacket(PacketType.REGISTER, new RegisterRequest
            {
                Username = txtUsername.Text.Trim(),
                Password = txtPassword.Text
            });
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                AppendLog("Username and password are required.");
                return;
            }

            SendPacket(PacketType.LOGIN, new LoginRequest
            {
                Username = txtUsername.Text.Trim(),
                Password = txtPassword.Text
            });
        }

        private void btnCreateRoom_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtRoomName.Text))
            {
                AppendLog("Room name is required.");
                return;
            }

            SendPacket(PacketType.CREATE_ROOM, new CreateRoomRequest
            {
                RoomName = txtRoomName.Text.Trim()
            });
        }

        private void btnJoinRoom_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtRoomID.Text))
            {
                AppendLog("Room ID is required.");
                return;
            }

            SendPacket(PacketType.JOIN_ROOM, new JoinRoomRequest
            {
                RoomId = txtRoomID.Text.Trim().ToUpper()
            });
        }

        private void btnRefreshMembers_Click(object sender, EventArgs e)
        {
            RequestRoomMembers();
        }

        private void btnCreateProject_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtProjectName.Text) || string.IsNullOrWhiteSpace(txtProjectIDRoom.Text))
            {
                AppendLog("Project name and Room ID are required.");
                return;
            }

            SendPacket(PacketType.CREATE_PROJECT, new CreateProjectRequest
            {
                ProjectName = txtProjectName.Text.Trim(),
                RoomID = txtProjectIDRoom.Text.Trim().ToUpper()
            });
        }

        private void btnListProjects_Click(object sender, EventArgs e)
        {
            SendRawPacket(PacketType.LIST_PROJECTS, "");
            AppendLog("Requesting project list...");
        }

        private void btnCreateFile_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtProjectId.Text, out int projectId) || projectId <= 0)
            {
                AppendLog("Project ID must be a valid number.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtFileName.Text))
            {
                AppendLog("File name is required.");
                return;
            }

            SendPacket(PacketType.CREATE_FILE, new CreateFileRequest
            {
                ProjectID = projectId,
                FileName = txtFileName.Text.Trim()
            });
        }

        private void btnListFiles_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtProjectId.Text, out int projectId) || projectId <= 0)
            {
                AppendLog("Project ID must be a valid number.");
                return;
            }

            SendPacket(PacketType.LIST_FILES, new ListFilesRequest
            {
                ProjectID = projectId
            });
            AppendLog("Requesting file list...");
        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtOpenFileId.Text, out int fileId) || fileId <= 0)
            {
                AppendLog("File ID must be a valid number.");
                return;
            }

            SendPacket(PacketType.OPEN_FILE, new OpenFileRequest
            {
                FileID = fileId
            });
        }

        private void btnUnlockFile_Click(object sender, EventArgs e)
        {
            if (client.CurrentFileID == 0)
            {
                AppendLog("No file is currently opened/locked.");
                return;
            }

            SendRawPacket(PacketType.UNLOCK_FILE, client.CurrentFileID.ToString());
            AppendLog($"Requesting unlock for FileID={client.CurrentFileID}...");
        }

        private void btnDeleteFile_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtOpenFileId.Text, out int fileId) || fileId <= 0)
            {
                AppendLog("File ID must be a valid number.");
                return;
            }

            SendPacket(PacketType.DELETE_FILE, new DeleteFileRequest
            {
                FileID = fileId
            });
            AppendLog($"Deleting FileID={fileId}...");
        }

        private void btnSaveFile_Click(object sender, EventArgs e)
        {
            if (txtEditor.ReadOnly)
            {
                MessageBox.Show("This file is locked by another user.", "Read Only", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (client.CurrentFileID == 0)
            {
                AppendLog("No file opened. Open a file first.");
                return;
            }

            client.CurrentFileContent = txtEditor.Text;
            SendPacket(PacketType.UPDATE_FILE_CONTENT, new UpdateFileContentRequest
            {
                FileID = client.CurrentFileID,
                Content = txtEditor.Text
            });
        }

        private void btnCompile_Click(object sender, EventArgs e)
        {
            if (client.CurrentFileID == 0)
            {
                AppendLog("No file opened. Open a file first.");
                return;
            }

            SendPacket(PacketType.COMPILE, new CompileRequest
            {
                FileID = client.CurrentFileID,
                Content = txtEditor.Text
            });
            AppendLog("Compiling current editor content...");
        }

        private void btnSendChat_Click(object sender, EventArgs e)
        {
            string message = txtChatInput.Text.Trim();
            if (string.IsNullOrWhiteSpace(message))
            {
                AppendLog("Chat message is empty.");
                return;
            }

            SendRawPacket(PacketType.CHAT, message);
            AppendChat($"[me] {currentUsername}: {message}");
            txtChatInput.Clear();
        }

        private void btnCreateTask_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtTaskProjectId.Text, out int projectId) || projectId <= 0)
            {
                AppendLog("Task Project ID must be a valid number.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtTaskName.Text))
            {
                AppendLog("Task name is required.");
                return;
            }

            int? assignedTo = null;
            if (!string.IsNullOrWhiteSpace(txtAssignedTo.Text))
            {
                if (!int.TryParse(txtAssignedTo.Text, out int assignedUserId))
                {
                    AppendLog("Assigned To must be empty or a valid user id.");
                    return;
                }
                assignedTo = assignedUserId;
            }

            SendPacket(PacketType.CREATE_TASK, new CreateTaskRequest
            {
                ProjectID = projectId,
                TaskName = txtTaskName.Text.Trim(),
                AssignedTo = assignedTo
            });
            AppendLog("Creating task...");
        }

        private void btnListTasks_Click(object sender, EventArgs e)
        {
            RequestTaskList();
        }

        private void btnUpdateTaskStatus_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtTaskId.Text, out int taskId) || taskId <= 0)
            {
                AppendLog("Task ID must be a valid number.");
                return;
            }

            SendPacket(PacketType.UPDATE_TASK_STATUS, new UpdateTaskStatusRequest
            {
                TaskID = taskId,
                Status = cmbTaskStatus.SelectedItem?.ToString() ?? "TODO"
            });
            AppendLog("Updating task status...");
        }

        private void btnDeleteTask_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtTaskId.Text, out int taskId) || taskId <= 0)
            {
                AppendLog("Task ID must be a valid number.");
                return;
            }

            SendPacket(PacketType.DELETE_TASK, new DeleteTaskRequest
            {
                TaskID = taskId
            });
            AppendLog($"Deleting TaskID={taskId}...");
        }

        private void btnListHistory_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtHistoryFileId.Text, out int fileId) || fileId <= 0)
            {
                AppendLog("History File ID must be a valid number.");
                return;
            }

            SendPacket(PacketType.LIST_HISTORY, new ListHistoryRequest
            {
                FileID = fileId
            });
            AppendLog("Requesting file history...");
        }

        private void btnOpenHistory_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtHistoryId.Text, out int historyId) || historyId <= 0)
            {
                AppendLog("History ID must be a valid number.");
                return;
            }

            SendPacket(PacketType.OPEN_HISTORY, new OpenHistoryRequest
            {
                HistoryID = historyId
            });
            AppendLog("Opening history version...");
        }

        private void HandleOpenFile(Packet packet)
        {
            SyncFileContentRequest openResponse = JsonHelper.Deserialize<SyncFileContentRequest>(packet.Data);
            txtEditor.Text = openResponse.Content;
            client.CurrentFileID = openResponse.FileID;
            client.CurrentFileContent = openResponse.Content;
            txtOpenFileId.Text = openResponse.FileID.ToString();
            txtHistoryFileId.Text = openResponse.FileID.ToString();

            // ===== Phân biệt ReadOnly hay Edit =====
            if (openResponse.ReadOnly)
            {
                txtEditor.ReadOnly = true;
                lblCurrentFile.Text = $"FileID: {openResponse.FileID} | Read Only";
                AppendLog("OPEN_FILE (Read Only)");
            }
            else
            {
                txtEditor.ReadOnly = false;
                lblCurrentFile.Text = $"FileID: {openResponse.FileID} | Editing";
                AppendLog("OPEN_FILE (Editable)");
            }
            tabMain.SelectedTab = tabEditor;
        }

        private void HandleSyncFile(Packet packet)
        {
            SyncFileContentRequest syncResponse = JsonHelper.Deserialize<SyncFileContentRequest>(packet.Data);
            if (syncResponse.FileID != client.CurrentFileID)
            {
                return;
            }

            txtEditor.Text = syncResponse.Content;
            client.CurrentFileContent = syncResponse.Content;
            AppendLog($"SYNC_FILE_CONTENT FileID={syncResponse.FileID} by {syncResponse.Username}");
        }

        private void RenderHistoryList(string json)
        {
            try
            {
                List<HistoryInfoResponse> histories = JsonHelper.Deserialize<List<HistoryInfoResponse>>(json);
                StringBuilder builder = new StringBuilder();
                foreach (HistoryInfoResponse history in histories)
                {
                    builder.AppendLine($"{history.HistoryID} | v{history.VersionNo} | editor {history.EditedBy} | {history.EditedTime:yyyy-MM-dd HH:mm:ss}");
                    builder.AppendLine($"    {history.ChangeSummary}");
                }

                txtHistoryList.Text = builder.Length == 0 ? "No history found." : builder.ToString();
            }
            catch
            {
                txtHistoryList.Text = json;
            }
        }

        private void RenderRoomMembers(string json)
        {
            lstMembers.Items.Clear();

            try
            {
                List<RoomMemberResponse> members = JsonHelper.Deserialize<List<RoomMemberResponse>>(json);
                foreach (RoomMemberResponse member in members)
                {
                    string status = member.IsOnline ? "Online" : "Offline";
                    lstMembers.Items.Add(new ListViewItem(new[] { member.Username, member.Role, status }));
                }
            }
            catch
            {
                AppendLog("Could not parse room members response.");
            }
        }

        private void RequestRoomMembers()
        {
            SendRawPacket(PacketType.LIST_ROOM_MEMBERS, "");
            AppendLog("Requesting room members...");
        }

        private void RequestTaskList()
        {
            if (!int.TryParse(txtTaskProjectId.Text, out int projectId) || projectId <= 0)
            {
                AppendLog("Task Project ID must be a valid number.");
                return;
            }

            SendPacket(PacketType.LIST_TASKS, new ListTaskRequest
            {
                ProjectID = projectId
            });
            AppendLog("Requesting task list...");
        }

        private void SetStatus(string message, System.Drawing.Color color)
        {
            lblStatus.Text = message;
            lblStatus.ForeColor = color;
        }

        private void AppendLog(string message)
        {
            txtLog.AppendText($"{DateTime.Now:HH:mm:ss}  {message}{Environment.NewLine}");
        }

        private void AppendChat(string message)
        {
            if (txtChatMessages.Text == "Team chat messages will appear here.")
            {
                txtChatMessages.Clear();
            }

            txtChatMessages.AppendText($"{DateTime.Now:HH:mm:ss}  {message}{Environment.NewLine}");
        }

        private void SendRawPacket(PacketType type, string data)
        {
            var packet = new Packet
            {
                Type = type.ToString(),
                Data = data
            };
            client.Send(JsonHelper.Serialize(packet));
        }

        private void SendPacket(PacketType type, object request)
        {
            var packet = new Packet
            {
                Type = type.ToString(),
                Data = JsonHelper.Serialize(request)
            };
            client.Send(JsonHelper.Serialize(packet));
        }
    }
}
