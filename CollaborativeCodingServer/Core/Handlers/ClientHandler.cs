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

        public ClientHandler(TcpClient client)
        {
            this.client = client;
            stream = client.GetStream();
            authHandler = new AuthHandler(this);
            roomHandler = new RoomHandler(this);
            projectHandler = new ProjectHandler(this);
        }

        public void Start()
        {
            byte[] buffer = new byte[4096];
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

                    string json = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    Packet packet = JsonHelper.Deserialize(json);
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
                        default:
                            Console.WriteLine("[SERVER] Unknown Packet");
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                ReleaseFileLocks();
                stream.Close();
                client.Close();
            }
        }

        private void ReleaseFileLocks()
        {
            var lockedFiles = FileLockManager.LockedFiles.Where(x => x.Value == Username).ToList();
            foreach (var item in lockedFiles)
            {
                FileLockManager.LockedFiles.Remove(item.Key);
                Console.WriteLine($"[LOCK RELEASED] File {item.Key}");
            }
        }

        public void Send(string message)
        {
            byte[] data = Encoding.UTF8.GetBytes(message);
            stream.Write(data, 0, data.Length);
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
    }
}
