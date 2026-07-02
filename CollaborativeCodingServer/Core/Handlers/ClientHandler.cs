using System.Net.Sockets;
using System.Text;
using CollaborativeCodingServer.Core.Handlers;
using CollaborativeCodingServer.Models.Entities;
using CollaborativeCodingServer.Network;
using CollaborativeCodingServer.Services;

namespace CollaborativeCodingServer.Core
{
    public class ClientHandler
    {
        private readonly TcpClient client; // Khai báo biến client để lưu trữ thông tin kết nối từ client
        private readonly NetworkStream stream; // Khai báo biến stream để lưu trữ luồng dữ liệu từ client
        private readonly AuthHandler authHandler; // Khai báo biến authHandler để xử lý các yêu cầu xác thực từ client
        private readonly RoomHandler roomHandler; // Khai báo biến roomHandler để xử lý các yêu cầu liên quan đến phòng từ client
        private readonly ProjectHandler projectHandler;
        private readonly ReplayHandler replayHandler;
        private readonly TaskHandler taskHandler;
        private readonly CompileHandler compileHandler;
        public ClientHandler(TcpClient client)
        {
            this.client = client;
            stream = client.GetStream(); // Lấy luồng dữ liệu từ client
            authHandler = new AuthHandler(this); // Khởi tạo AuthHandler với ClientHandler hiện tại
            roomHandler = new RoomHandler(this); // Khởi tạo RoomHandler với ClientHandler hiện tại
            projectHandler = new ProjectHandler(this);
            replayHandler = new ReplayHandler(this);
            taskHandler = new TaskHandler(this);
            compileHandler = new CompileHandler(this);
        }

        public void Start()
        {
            byte[] buffer = new byte[65536]; 
            var messageBuffer = new System.Text.StringBuilder(); // Khởi tạo StringBuilder để lưu trữ dữ liệu nhận được từ client
            try
            {
                while (true) // Vòng lặp vô hạn để liên tục nhận dữ liệu từ client
                {
                    int bytesRead = stream.Read(buffer, 0, buffer.Length); // Đọc dữ liệu từ luồng và lưu vào buffer, trả về số byte đã đọc
                    if (bytesRead == 0)
                    {
                        Console.WriteLine("[SERVER] Client Disconnected");
                        break;
                    }

                    string chunk = Encoding.UTF8.GetString(buffer, 0, bytesRead); // Chuyển đổi dữ liệu từ byte[] sang string
                    messageBuffer.Append(chunk); // Thêm dữ liệu nhận được vào messageBuffer
                    string raw = messageBuffer.ToString(); 

                    // Xử lý nhiều packet trong một lần đọc (TCP framing)
                    int start = 0;
                    while (start < raw.Length) // Vòng lặp để xử lý từng packet trong dữ liệu nhận được
                    {
                        int begin = raw.IndexOf('{', start); // Tìm vị trí bắt đầu của packet (ký tự '{')
                        if (begin < 0) break; 

                        int depth = 0; // Biến depth để theo dõi số lượng dấu ngoặc nhọn '{' và '}' để xác định kết thúc của packet
                        int end = -1; // Biến end để lưu vị trí kết thúc của packet
                        for (int i = begin; i < raw.Length; i++) // Vòng lặp để tìm vị trí kết thúc của packet
                        {
                            if (raw[i] == '{') depth++;
                            else if (raw[i] == '}')
                            {
                                depth--;
                                if (depth == 0) { end = i; break; }
                            }
                        }

                        if (end < 0) break;
                        string jsonStr = raw.Substring(begin, end - begin + 1); // Lấy chuỗi JSON từ dữ liệu nhận được
                        try
                        {
                            Packet packet = JsonHelper.Deserialize(jsonStr); // Chuyển đổi dữ liệu từ JSON sang đối tượng Packet
                            switch (packet.Type)
                            {
                                case "CHAT":
                                    roomHandler.HandleChat(packet); // Gọi phương thức HandleChat của RoomHandler để xử lý yêu cầu chat từ client
                                    break;
                                case "LOGIN":
                                    authHandler.HandleLogin(packet);
                                    break;
                                case "REGISTER":
                                    authHandler.HandleRegister(packet);
                                    break;
                                case "CREATE_ROOM":
                                    roomHandler.HandleCreateRoom(packet);
                                    break;
                                case "JOIN_ROOM":
                                    roomHandler.HandleJoinRoom(packet);
                                    break;
                                case "LIST_ROOM_MEMBERS":
                                    roomHandler.HandleListRoomMembers();
                                    break;
                                case "CREATE_PROJECT":
                                    projectHandler.HandleCreateProject(packet);
                                    break;
                                case "CREATE_FILE":
                                    projectHandler.HandleCreateFile(packet);
                                    break;
                                case "LIST_PROJECTS":
                                    projectHandler.HandleListProjects();
                                    break;
                                case "LIST_FILES":
                                    projectHandler.HandleListFiles(packet);
                                    break;
                                case "OPEN_FILE":
                                    projectHandler.HandleOpenFile(packet);
                                    break;
                                case "UPDATE_FILE_CONTENT":
                                    projectHandler.HandleUpdateFileContent(packet);
                                    break;
                                case "DELETE_FILE":
                                    projectHandler.HandleDeleteFile(packet);
                                    break;
                                case "UNLOCK_FILE":
                                    projectHandler.HandleUnlockFile(packet);
                                    break;
                                case "LIST_HISTORY":
                                    replayHandler.HandleListHistory(packet);
                                    break;
                                case "OPEN_HISTORY":
                                    replayHandler.HandleOpenHistory(packet);
                                    break;
                                case "CREATE_TASK":
                                    taskHandler.HandleCreateTask(packet);
                                    break;
                                case "LIST_TASKS":
                                    taskHandler.HandleListTasks(packet);
                                    break;
                                case "UPDATE_TASK_STATUS":
                                    taskHandler.HandleUpdateTaskStatus(packet);
                                    break;
                                case "DELETE_TASK":
                                    taskHandler.HandleDeleteTask(packet);
                                    break;
                                case "COMPILE":
                                    compileHandler.HandleCompile(packet);
                                    break;
                                default:
                                    Console.WriteLine("[SERVER] Unknown Packet: " + packet.Type);
                                    break;
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("[PARSE ERROR] " + ex.Message);
                        }

                        start = end + 1; // Cập nhật vị trí bắt đầu cho lần đọc tiếp theo
                    }

                    if (start < raw.Length) // Nếu còn dữ liệu chưa xử lý, giữ lại phần dữ liệu đó trong messageBuffer
                        messageBuffer = new System.Text.StringBuilder(raw.Substring(start));
                    else
                        messageBuffer.Clear(); // Nếu tất cả dữ liệu đã được xử lý, xóa messageBuffer
                }
            }
            catch (Exception ex) // Bắt lỗi nếu có ngoại lệ xảy ra trong quá trình nhận dữ liệu từ client
            {
                Console.WriteLine(ex.Message);
            }
            finally // Đảm bảo rằng các tài nguyên được giải phóng khi kết thúc kết nối với client
            {
                ReleaseFileLocks(); // Giải phóng tất cả các khóa file mà client đang giữ
                CurrentFileId = null; // Giải phóng khóa file khi client ngắt kết nối
                if (CurrentUser != null) // Nếu người dùng hiện tại không null, cập nhật trạng thái online của người dùng khi client ngắt kết nối
                {
                    new AuthService().SetOnlineStatus(CurrentUser.UserID, false); // Cập nhật trạng thái online của người dùng khi client ngắt kết nối
                }
                if (CurrentRoom != null) // Nếu phòng hiện tại không null, xóa client khỏi danh sách clients của phòng khi client ngắt kết nối
                {
                    CurrentRoom.Clients.Remove(this); // Xóa client khỏi danh sách clients của phòng
                    Console.WriteLine($"[ROOM] {Username} left room {CurrentRoom.RoomName}"); // In ra thông báo khi client rời khỏi phòng
                }
                stream.Close(); // Đóng luồng dữ liệu từ client
                client.Close(); // Đóng kết nối với client
            }
        }


        private void ReleaseFileLocks() // Giải phóng tất cả các khóa file mà client đang giữ
        { 
            if (string.IsNullOrEmpty(Username)) return;
            var lockedFiles = FileLockManager.LockedFiles.Where(x => x.Value == Username).ToList();
            foreach (var item in lockedFiles)
            {
                FileLockManager.LockedFiles.TryRemove(item.Key, out _);
                Console.WriteLine($"[LOCK RELEASED] File {item.Key}");
            }
        }

        public bool Send(string message) // Gửi dữ liệu đến client
        {
            try
            {
                byte[] data = Encoding.UTF8.GetBytes(message); // Chuyển đổi dữ liệu từ string sang byte[]
                stream.Write(data, 0, data.Length); // Gửi dữ liệu đến client thông qua luồng dữ liệu
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void SendPacket(PacketType type, string data = "") // Gửi dữ liệu đến client dưới dạng Packet
        {
            Packet packet = new Packet // Tạo đối tượng Packet với kiểu dữ liệu và dữ liệu cần gửi
            {
                Type = type.ToString(),
                Data = data
            };
            string json = JsonHelper.Serialize(packet); // Chuyển đổi dữ liệu từ Packet sang JSON
            Send(json);
        }


        public string Username { get; set; } // Lưu trữ tên người dùng hiện tại của client
        public User CurrentUser { get; set; } // Lưu trữ thông tin người dùng hiện tại của client
        public Room? CurrentRoom { get; set; } // Lưu trữ phòng hiện tại của client
        public int? CurrentProjectId { get; set; } 
        public int? CurrentFileId { get; set; } 
    }
}
