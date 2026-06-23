using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using CollaborativeCodingServer.Protocol;
using CollaborativeCodingServer.Services;
using CollaborativeCodingServer.Models;

namespace CollaborativeCodingServer.Core
{
    public class ClientHandler
    {
        private readonly TcpClient client; 
        private readonly NetworkStream stream;
        private readonly AuthService authService = new AuthService();
        private readonly ProjectService projectService = new ProjectService();
        private readonly FileService fileService = new FileService();

        public ClientHandler(TcpClient client)
        {
            this.client = client;
            stream = client.GetStream();
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
                            HandleChat(packet);
                            break;

                        case "LOGIN":
                            HandleLogin(packet);
                            break;

                        case "REGISTER":
                            HandleRegister(packet);
                            break;

                        case "CREATE_ROOM":
                            HandleCreateRoom(packet);
                            break;

                        case "JOIN_ROOM":
                            HandleJoinRoom(packet);
                            break;

                        case "CREATE_PROJECT":
                            HandleCreateProject(packet);
                            break;

                        case "CREATE_FILE":
                            HandleCreateFile(packet);
                            break;

                        case "LIST_PROJECTS":
                            HandleListProjects();
                            break;

                        case "LIST_FILES":
                            HandleListFiles(packet);
                            break;

                        case "OPEN_FILE":
                            HandleOpenFile(packet);
                            break;

                        case "UPDATE_FILE_CONTENT":
                            HandleUpdateFileContent(packet);
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

        private void Send(string message)
        {
            byte[] data = Encoding.UTF8.GetBytes(message);
            stream.Write(data, 0, data.Length);
        }

        private void SendPacket(PacketType type, string data = "")
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

        private void HandleChat(Packet packet)
        {
            if (CurrentRoom == null)
            {
                Send("YOU_ARE_NOT_IN_ROOM");
                return;
            }
            string message = $"[{CurrentRoom.RoomName}] {Username}: {packet.Data}";
            Console.WriteLine(message);
            BroadcastToRoom(message);
        }

        private void HandleLogin(Packet packet)
        {
            LoginRequest request = JsonHelper.Deserialize<LoginRequest>(packet.Data);
            bool success = authService.Login(request.Username, request.Password);
            if (success)
            {
                Username = request.Username;
                SendPacket(PacketType.LOGIN_SUCCESS);
            }
            else
            {
                SendPacket(PacketType.LOGIN_FAILED);
            }
        }

        private void HandleRegister(Packet packet)
        {
            RegisterRequest request = JsonHelper.Deserialize<RegisterRequest>(packet.Data);
            bool success = authService.Register(request.Username, request.Password);
            if (success)
            {
                SendPacket(PacketType.REGISTER_SUCCESS);
            }
            else
            {
                SendPacket(PacketType.REGISTER_FAILED);
            }
        }

        private void HandleCreateRoom(Packet packet)
        {
            CreateRoomRequest request = JsonHelper.Deserialize<CreateRoomRequest>(packet.Data);
            Room room = new Room
            {
                RoomId = Guid.NewGuid().ToString().Substring(0, 6).ToUpper(),
                RoomName = request.RoomName
            };
            room.Clients.Add(this);
            CurrentRoom = room;
            RoomManager.Rooms.Add(room);
            Console.WriteLine($"[ROOM CREATED] {room.RoomName}");
            SendPacket(PacketType.CREATE_ROOM_SUCCESS, room.RoomId);
        }

        public Room? CurrentRoom { get; set; }

        private void HandleJoinRoom(Packet packet)
        {
            JoinRoomRequest request = JsonHelper.Deserialize<JoinRoomRequest>(packet.Data);
            Room room = RoomManager.Rooms.FirstOrDefault(r => r.RoomId == request.RoomId);
            if (room == null)
            {
                SendPacket(PacketType.ROOM_NOT_FOUND);
                return;
            }
            room.Clients.Add(this);
            CurrentRoom = room;
            Console.WriteLine($"[JOIN ROOM] {room.RoomName}");
            SendPacket(PacketType.JOIN_ROOM_SUCCESS);
        }

        private void BroadcastToRoom(string message)
        {
            Packet packet = new Packet
            {
                Type = PacketType.CHAT.ToString(),
                Data = message
            };
            string json = JsonHelper.Serialize(packet);
            foreach (ClientHandler client in CurrentRoom.Clients)
            {
                client.Send(json);
            }
        }

        private void HandleCreateProject(Packet packet)
        {
            CreateProjectRequest request = JsonHelper.Deserialize<CreateProjectRequest>(packet.Data);
            bool success = projectService.CreateProject(request.ProjectName, request.RoomID);
            if (success)
            {
                SendPacket(PacketType.CREATE_PROJECT_SUCCESS);
            }
            else
            {
                SendPacket(PacketType.CREATE_PROJECT_FAILED);
            }
        }

        private void HandleCreateFile(Packet packet)
        {
            CreateFileRequest request = JsonHelper.Deserialize<CreateFileRequest>(packet.Data);
            bool success = fileService.CreateFile(request.ProjectID, request.FileName);
            if (success)
            {
                SendPacket(PacketType.CREATE_FILE_SUCCESS);
            }
            else
            {
                SendPacket(PacketType.CREATE_FILE_FAILED);
            }
        }

        private void HandleListProjects()
        {
            List<ProjectInfo> projects = projectService.GetProjects();
            StringBuilder builder = new StringBuilder();
            foreach (var project in projects)
            {
                builder.AppendLine($"{project.ProjectID} - {project.ProjectName}");
            }
            SendPacket(PacketType.LIST_PROJECTS, builder.ToString());
        }

        private void HandleListFiles(Packet packet)
        {
            ListFilesRequest request = JsonHelper.Deserialize<ListFilesRequest>(packet.Data);
            List<ProjectFile> files = fileService.GetFilesByProject(request.ProjectID);
            StringBuilder builder = new StringBuilder();
            foreach (var file in files)
            {
                builder.AppendLine($"{file.FileID} - {file.FileName}");
            }
            SendPacket(PacketType.LIST_FILES, builder.ToString());
        }

        private void HandleOpenFile(Packet packet)
        {
            OpenFileRequest request = JsonHelper.Deserialize<OpenFileRequest>(packet.Data);
            if (FileLockManager.LockedFiles.ContainsKey(request.FileID))
            {
                string owner = FileLockManager.LockedFiles[request.FileID];
                Console.WriteLine($"[LOCKED BY] {owner}");
            }
            else
            {
                FileLockManager.LockedFiles[request.FileID] = Username;
            }
            ProjectFile file = fileService.GetFileById(request.FileID);
            if (file == null)
            {
                SendPacket(PacketType.FILE_NOT_FOUND);
                return;
            }
            SendPacket(PacketType.OPEN_FILE, file.Content);
        }

        private void HandleUpdateFileContent(Packet packet)
        {
            UpdateFileContentRequest request = JsonHelper.Deserialize<UpdateFileContentRequest>(packet.Data);
            bool success = fileService.UpdateFileContent(request.FileID, request.Content);
            if (success)
            {
                SendPacket(PacketType.UPDATE_FILE_SUCCESS);
                SyncFileContentRequest sync = new SyncFileContentRequest
                {
                    FileID = request.FileID,
                    Content = request.Content,
                    Username = Username
                };
                BroadcastFileUpdate(sync);
            }
            else
            {
                SendPacket(PacketType.UPDATE_FILE_FAILED);
            }
        }

        private void BroadcastFileUpdate(SyncFileContentRequest request)
        {
            if (CurrentRoom == null) return;

            Packet packet = new Packet
            {
                Type = PacketType.SYNC_FILE_CONTENT.ToString(),
                Data = JsonHelper.Serialize(request)
            };
            string json = JsonHelper.Serialize(packet);
            foreach (ClientHandler client in CurrentRoom.Clients)
            {
                if (client == this)
                    continue;

                client.Send(json);
            }
        }
    }
}
