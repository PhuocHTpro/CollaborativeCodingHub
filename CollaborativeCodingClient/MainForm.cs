using CollaborativeCodingClient.Models.Packets.Auth;
using CollaborativeCodingClient.Models.Packets.Project;
using CollaborativeCodingClient.Models.Packets.Room;
using CollaborativeCodingClient.Network;
using CollaborativeCodingClient.Models.Packets.Replay;
using CollaborativeCodingClient.Models.Packets.Task;
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
            SetStatus("🟡  Connecting to server...", System.Drawing.Color.FromArgb(200, 180, 60));
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
                    currentUsername = txtUsername.Text;
                    SetStatus($"🟢  Logged in as: {currentUsername}", System.Drawing.Color.FromArgb(60, 180, 80));
                    AppendLog($"✅ [LOGIN SUCCESS] Welcome, {currentUsername}!");
                    break;

                case nameof(PacketType.LOGIN_FAILED):
                    AppendLog("❌ [LOGIN FAILED] Invalid username or password.");
                    break;

                case nameof(PacketType.REGISTER_SUCCESS):
                    AppendLog("✅ [REGISTER SUCCESS] Account created. You can now login.");
                    break;

                case nameof(PacketType.REGISTER_FAILED):
                    AppendLog("❌ [REGISTER FAILED] Username already exists.");
                    break;

                case nameof(PacketType.CREATE_ROOM_SUCCESS):
                    AppendLog($"🏠 [ROOM CREATED] Room ID: {packet.Data}");
                    // Auto-fill Room ID vào txtProjectIDRoom để tiện tạo project
                    txtProjectIDRoom.Text = packet.Data;
                    SetStatus($"🟢  {currentUsername}  |  Room: {packet.Data}", System.Drawing.Color.FromArgb(60, 180, 80));
                    break;

                case nameof(PacketType.CREATE_ROOM_FAILED):
                    AppendLog("❌ [CREATE ROOM FAILED] Must be logged in to create a room.");
                    break;

                case nameof(PacketType.JOIN_ROOM_SUCCESS):
                    AppendLog($"🚪 [ROOM JOINED] Room ID: {packet.Data}");
                    // Auto-fill Room ID vào txtProjectIDRoom
                    txtProjectIDRoom.Text = packet.Data;
                    SetStatus($"🟢  {currentUsername}  |  Room: {packet.Data}", System.Drawing.Color.FromArgb(60, 180, 80));
                    break;

                case nameof(PacketType.JOIN_ROOM_FAILED):
                    AppendLog("❌ [JOIN ROOM FAILED] Room not found or invalid ID.");
                    break;

                case nameof(PacketType.CREATE_PROJECT_SUCCESS):
                    AppendLog($"📁 [PROJECT CREATED] Project ID: {packet.Data}");
                    // Auto-fill Project ID vào txtProjectId để tiện tạo file
                    txtProjectId.Text = packet.Data;
                    break;

                case nameof(PacketType.CREATE_PROJECT_FAILED):
                    AppendLog("❌ [PROJECT CREATE FAILED] Check room ID and login status.");
                    break;

                case nameof(PacketType.CREATE_FILE_SUCCESS):
                    AppendLog($"📄 [FILE CREATED] File ID: {packet.Data}");
                    // Auto-fill File ID vào txtOpenFileId để tiện mở file
                    txtOpenFileId.Text = packet.Data;
                    break;

                case nameof(PacketType.CREATE_FILE_FAILED):
                    AppendLog("❌ [FILE CREATE FAILED] Check project ID.");
                    break;

                case nameof(PacketType.UPDATE_FILE_SUCCESS):
                    AppendLog("💾 [FILE SAVED] Changes saved successfully.");
                    break;

                case nameof(PacketType.UPDATE_FILE_FAILED):
                    AppendLog("❌ [FILE SAVE FAILED] Could not save the file.");
                    break;

                case nameof(PacketType.UNLOCK_FILE_SUCCESS):
                    AppendLog($"🔓 [FILE UNLOCKED] File ID {packet.Data} is now available for others.");
                    lblCurrentFile.Text = "No file opened";
                    client.CurrentFileID = 0;
                    client.CurrentFileContent = string.Empty;
                    break;

                case nameof(PacketType.UNLOCK_FILE_FAILED):
                    AppendLog($"❌ [UNLOCK FAILED] {packet.Data}");
                    break;

                case nameof(PacketType.OPEN_FILE):
                    var openResponse = JsonHelper.Deserialize<SyncFileContentRequest>(packet.Data);
                    txtEditor.Text = openResponse.Content;
                    lblCurrentFile.Text = $"📄 File ID: {openResponse.FileID}  [LOCKED by you]";
                    client.CurrentFileID = openResponse.FileID;
                    client.CurrentFileContent = openResponse.Content;
                    // Tự động chuyển sang tab Editor
                    tabMain.SelectedTab = tabEditor;
                    AppendLog($"📂 [FILE OPENED] File ID: {openResponse.FileID} — editor tab activated.");
                    break;

                case nameof(PacketType.SYNC_FILE_CONTENT):
                    var syncResponse = JsonHelper.Deserialize<SyncFileContentRequest>(packet.Data);
                    if (syncResponse.FileID == client.CurrentFileID)
                    {
                        txtEditor.Text = syncResponse.Content;
                        client.CurrentFileContent = syncResponse.Content;
                        AppendLog($"🔄 [SYNCED] File {syncResponse.FileID} updated by {syncResponse.Username}.");
                    }
                    break;

                case nameof(PacketType.COMPILE_SUCCESS):
                    txtCompileResult.ForeColor = System.Drawing.Color.FromArgb(144, 238, 144);
                    txtCompileResult.Text = packet.Data;
                    AppendLog("⚙️ [COMPILE SUCCESS] See output below in Editor tab.");
                    tabMain.SelectedTab = tabEditor;
                    break;

                case nameof(PacketType.COMPILE_FAILED):
                    txtCompileResult.ForeColor = System.Drawing.Color.FromArgb(255, 120, 120);
                    txtCompileResult.Text = packet.Data;
                    AppendLog("⚙️ [COMPILE FAILED] See error output below.");
                    tabMain.SelectedTab = tabEditor;
                    break;

                case nameof(PacketType.FILE_NOT_FOUND):
                    AppendLog("❌ [FILE NOT FOUND] The requested file does not exist.");
                    break;

                case nameof(PacketType.FILE_LOCKED):
                    AppendLog($"🔒 [FILE LOCKED] This file is currently locked by: {packet.Data}");
                    break;

                case nameof(PacketType.ROOM_NOT_FOUND):
                    AppendLog("❌ [ROOM NOT FOUND] Check the room ID and try again.");
                    break;

                case nameof(PacketType.ACCESS_DENIED):
                    AppendLog($"⛔ [ACCESS DENIED] {packet.Data}");
                    break;

                default:
                    if (!string.IsNullOrWhiteSpace(packet.Data))
                        AppendLog(packet.Data);
                    break;
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                AppendLog("⚠️ Username and password are required.");
                return;
            }
            var request = new RegisterRequest
            {
                Username = txtUsername.Text.Trim(),
                Password = txtPassword.Text
            };
            SendPacket(PacketType.REGISTER, request);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                AppendLog("⚠️ Username and password are required.");
                return;
            }
            var request = new LoginRequest
            {
                Username = txtUsername.Text.Trim(),
                Password = txtPassword.Text
            };
            SendPacket(PacketType.LOGIN, request);
        }

        private void btnCreateRoom_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtRoomName.Text))
            {
                AppendLog("⚠️ Room name is required.");
                return;
            }
            var request = new CreateRoomRequest
            {
                RoomName = txtRoomName.Text.Trim()
            };
            SendPacket(PacketType.CREATE_ROOM, request);
        }

        private void btnJoinRoom_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtRoomID.Text))
            {
                AppendLog("⚠️ Room ID is required.");
                return;
            }
            var request = new JoinRoomRequest
            {
                RoomId = txtRoomID.Text.Trim().ToUpper()
            };
            SendPacket(PacketType.JOIN_ROOM, request);
        }

        private void btnCreateProject_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtProjectName.Text) || string.IsNullOrWhiteSpace(txtProjectIDRoom.Text))
            {
                AppendLog("⚠️ Project name and Room ID are required.");
                return;
            }
            var request = new CreateProjectRequest
            {
                ProjectName = txtProjectName.Text.Trim(),
                RoomID = txtProjectIDRoom.Text.Trim().ToUpper()
            };
            SendPacket(PacketType.CREATE_PROJECT, request);
        }

        private void btnCreateFile_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtProjectId.Text, out int projectId) || projectId <= 0)
            {
                AppendLog("⚠️ Project ID must be a valid number.");
                return;
            }
            if (string.IsNullOrWhiteSpace(txtFileName.Text))
            {
                AppendLog("⚠️ File name is required.");
                return;
            }
            var request = new CreateFileRequest
            {
                ProjectID = projectId,
                FileName = txtFileName.Text.Trim()
            };
            SendPacket(PacketType.CREATE_FILE, request);
        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtOpenFileId.Text, out int fileId) || fileId <= 0)
            {
                AppendLog("⚠️ File ID must be a valid number.");
                return;
            }
            var request = new OpenFileRequest
            {
                FileID = fileId
            };
            SendPacket(PacketType.OPEN_FILE, request);
        }

        private void btnUnlockFile_Click(object sender, EventArgs e)
        {
            if (client.CurrentFileID == 0)
            {
                AppendLog("⚠️ No file is currently opened/locked.");
                return;
            }

            // Gửi File ID hiện tại để unlock
            var packet = new Packet
            {
                Type = PacketType.UNLOCK_FILE.ToString(),
                Data = client.CurrentFileID.ToString()
            };
            client.Send(JsonHelper.Serialize(packet));
            AppendLog($"🔓 Requesting unlock for File ID: {client.CurrentFileID}...");
        }

        private void btnSaveFile_Click(object sender, EventArgs e)
        {
            if (client.CurrentFileID == 0)
            {
                AppendLog("⚠️ No file opened. Open a file first.");
                return;
            }

            var request = new UpdateFileContentRequest
            {
                FileID = client.CurrentFileID,
                Content = txtEditor.Text
            };
            client.CurrentFileContent = txtEditor.Text;
            SendPacket(PacketType.UPDATE_FILE_CONTENT, request);
        }

        private void btnCompile_Click(object sender, EventArgs e)
        {
            if (client.CurrentFileID == 0)
            {
                AppendLog("⚠️ No file opened. Open a file first.");
                return;
            }

            var request = new CompileRequest
            {
                FileID = client.CurrentFileID,
                Content = txtEditor.Text
            };
            SendPacket(PacketType.COMPILE, request);
            AppendLog("⚙️ Compiling...");
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
