using System.Net.Sockets;
using System.Text;
using CollaborativeCodingServer.Core.Handlers;
using CollaborativeCodingServer.Models.Entities;
using CollaborativeCodingServer.Network;

namespace CollaborativeCodingServer.Core
{
    public class ClientHandler
    {
        private readonly TcpClient client;
        private readonly NetworkStream stream;
        private readonly AuthHandler authHandler;
        private readonly RoomHandler roomHandler;
        private readonly ProjectHandler projectHandler;
        private readonly ReplayHandler replayHandler;
        private readonly TaskHandler taskHandler;
        private readonly CompileHandler compileHandler;
        public ClientHandler(TcpClient client)
        {
            this.client = client;
            stream = client.GetStream();
            authHandler = new AuthHandler(this);
            roomHandler = new RoomHandler(this);
            projectHandler = new ProjectHandler(this);
            replayHandler = new ReplayHandler(this);
            taskHandler = new TaskHandler(this);
            compileHandler = new CompileHandler(this);
        }

        public void Start()
        {
            byte[] buffer = new byte[65536];
            var messageBuffer = new System.Text.StringBuilder();
            try
            {
                while (true)
                {
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);
                    if (bytesRead == 0)
                    {
                        Console.WriteLine("[SERVER] Client Disconnected");
                        break;
                    }

                    string chunk = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    messageBuffer.Append(chunk);
                    string raw = messageBuffer.ToString();

                    // Xử lý nhiều packet trong một lần đọc (TCP framing)
                    int start = 0;
                    while (start < raw.Length)
                    {
                        int begin = raw.IndexOf('{', start);
                        if (begin < 0) break;

                        int depth = 0;
                        int end = -1;
                        for (int i = begin; i < raw.Length; i++)
                        {
                            if (raw[i] == '{') depth++;
                            else if (raw[i] == '}')
                            {
                                depth--;
                                if (depth == 0) { end = i; break; }
                            }
                        }

                        if (end < 0) break;

                        string jsonStr = raw.Substring(begin, end - begin + 1);
                        try
                        {
                            Packet packet = JsonHelper.Deserialize(jsonStr);
                            switch (packet.Type)
                            {
                                case "CHAT":
                                    roomHandler.HandleChat(packet);
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

                        start = end + 1;
                    }

                    if (start < raw.Length)
                        messageBuffer = new System.Text.StringBuilder(raw.Substring(start));
                    else
                        messageBuffer.Clear();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                ReleaseFileLocks();
                if (CurrentRoom != null)
                {
                    CurrentRoom.Clients.Remove(this);
                }
                stream.Close();
                client.Close();
            }
            if (CurrentRoom != null)
            {
                CurrentRoom.Clients.Remove(this);
                Console.WriteLine($"[ROOM] {Username} left room {CurrentRoom.RoomName}");
            }
        }


        private void ReleaseFileLocks()
        {
            if (string.IsNullOrEmpty(Username)) return;
            var lockedFiles = FileLockManager.LockedFiles.Where(x => x.Value == Username).ToList();
            foreach (var item in lockedFiles)
            {
                FileLockManager.LockedFiles.TryRemove(item.Key, out _);
                Console.WriteLine($"[LOCK RELEASED] File {item.Key}");
            }
        }

        public bool Send(string message)
        {
            try
            {
                byte[] data = Encoding.UTF8.GetBytes(message);
                stream.Write(data, 0, data.Length);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void SendPacket(PacketType type, string data = "")
        {
            Packet packet = new Packet
            {
                Type = type.ToString(),
                Data = data
            };
            string json = JsonHelper.Serialize(packet);
            Send(json);
        }


        public string Username { get; set; }
        public User CurrentUser { get; set; }
        public Room? CurrentRoom { get; set; }
        public int? CurrentProjectId { get; set; }
        public int? CurrentFileId { get; set; }
    }
}
