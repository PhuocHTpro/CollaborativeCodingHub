using System.Text;
using CollaborativeCodingServer.Core;
using CollaborativeCodingServer.Models.Entities;
using CollaborativeCodingServer.Models.Packets.Project;
using CollaborativeCodingServer.Network;
using CollaborativeCodingServer.Repositories;
using CollaborativeCodingServer.Services;

namespace CollaborativeCodingServer.Core.Handlers
{
    public class ProjectHandler
    {
        private readonly ClientHandler clientHandler;
        private readonly ProjectService projectService = new ProjectService(); // khai báo biến projectService để xử lý các yêu cầu liên quan đến dự án
        private readonly FileService fileService = new FileService();
        private readonly RoomRepository roomRepository = new RoomRepository();

        public ProjectHandler(ClientHandler clientHandler)
        {
            this.clientHandler = clientHandler;
        }

        public void HandleCreateProject(Packet packet) // Xử lý yêu cầu tạo dự án
        {
            if (clientHandler.CurrentUser == null)
            {
                clientHandler.SendPacket(PacketType.CREATE_PROJECT_FAILED);
                return;
            }

            CreateProjectRequest request = JsonHelper.Deserialize<CreateProjectRequest>(packet.Data); // Tuần tự hóa dữ liệu từ gói tin thành đối tượng CreateProjectRequest
            if (string.IsNullOrWhiteSpace(request.ProjectName) || string.IsNullOrWhiteSpace(request.RoomID)) // Kiểm tra tên dự án và ID phòng có hợp lệ không
            {
                clientHandler.SendPacket(PacketType.CREATE_PROJECT_FAILED);
                return;
            }

            bool roomExists = roomRepository.RoomExists(request.RoomID); // Kiểm tra xem phòng có tồn tại không
            if (!roomExists) // Nếu phòng không tồn tại, gửi gói tin thất bại
            {
                clientHandler.SendPacket(PacketType.CREATE_PROJECT_FAILED);
                return;
            }
            if (clientHandler.CurrentRoom == null || clientHandler.CurrentRoom.RoomId != request.RoomID) // Kiểm tra xem người dùng có đang ở trong phòng đó không
            {
                clientHandler.SendPacket(PacketType.CREATE_PROJECT_FAILED);
                return;
            }

            int projectId = projectService.CreateProject(request.ProjectName, request.RoomID, clientHandler.CurrentUser.UserID); // Tạo dự án mới và nhận ID dự án
            if (projectId > 0) // Nếu tạo dự án thành công, gửi gói tin thành công với ID dự án
            {
                clientHandler.SendPacket(PacketType.CREATE_PROJECT_SUCCESS, projectId.ToString());
            }
            else
            {
                clientHandler.SendPacket(PacketType.CREATE_PROJECT_FAILED);
            }
        }

        public void HandleUnlockFile(Packet packet) // Xử lý yêu cầu mở khóa file
        {
            if (clientHandler.CurrentUser == null)
            {
                clientHandler.SendPacket(PacketType.UNLOCK_FILE_FAILED, "Login required.");
                return;
            }

            if (!int.TryParse(packet.Data, out int fileId) || fileId <= 0)
            {
                clientHandler.SendPacket(PacketType.UNLOCK_FILE_FAILED, "Invalid file ID.");
                return;
            }

            // Kiểm tra file có đang bị lock bởi người dùng này không
            if (FileLockManager.LockedFiles.ContainsKey(fileId) &&
                FileLockManager.LockedFiles[fileId] == clientHandler.Username)
            { // Nếu file đang bị lock bởi người dùng này, mở khóa file
                FileLockManager.LockedFiles.TryRemove(fileId, out _);
                if (clientHandler.CurrentFileId == fileId)
                {
                    clientHandler.CurrentFileId = null;
                }
                Console.WriteLine($"[UNLOCK] File {fileId} unlocked by {clientHandler.Username}");
                clientHandler.SendPacket(PacketType.UNLOCK_FILE_SUCCESS, fileId.ToString());
            }
            else if (!FileLockManager.LockedFiles.ContainsKey(fileId)) // Nếu file không bị lock, gửi gói tin thất bại với thông báo
            {
                clientHandler.SendPacket(PacketType.UNLOCK_FILE_FAILED, "File is not locked.");
            }
            else // Nếu file đang bị lock bởi người dùng khác, gửi gói tin thất bại với thông báo
            {
                string owner = FileLockManager.LockedFiles[fileId];
                clientHandler.SendPacket(PacketType.UNLOCK_FILE_FAILED, $"File is locked by {owner}, not you.");
            }
        }


        public void HandleCreateFile(Packet packet) // Xử lý yêu cầu tạo file
        {
            CreateFileRequest request = JsonHelper.Deserialize<CreateFileRequest>(packet.Data); // Tuần tự hóa dữ liệu từ gói tin thành đối tượng CreateFileRequest
            if (clientHandler.CurrentUser == null) // Kiểm tra xem người dùng đã đăng nhập chưa
            {
                clientHandler.SendPacket(PacketType.CREATE_FILE_FAILED);
                return;
            }
            if (request.ProjectID <= 0 || string.IsNullOrWhiteSpace(request.FileName)) // Kiểm tra xem ID dự án và tên file có hợp lệ không
            {
                clientHandler.SendPacket(PacketType.CREATE_FILE_FAILED);
                return;
            }

            if (!projectService.ProjectExists(request.ProjectID)) // Kiểm tra xem dự án có tồn tại không
            {
                clientHandler.SendPacket(PacketType.CREATE_FILE_FAILED);
                return;
            }
            int fileId = fileService.CreateFile(request.ProjectID, request.FileName, clientHandler.CurrentUser.UserID); // Tạo file mới và nhận ID file
            if (fileId > 0) // Nếu tạo file thành công, gửi gói tin thành công với ID file
            {
                clientHandler.SendPacket(PacketType.CREATE_FILE_SUCCESS, fileId.ToString());
            }
            else
            {
                clientHandler.SendPacket(PacketType.CREATE_FILE_FAILED);
            }
        }

        public void HandleListProjects()
        {
            List<ProjectInfo> projects = projectService.GetProjects(clientHandler.CurrentRoom.RoomId);
            StringBuilder builder = new StringBuilder();
            foreach (var project in projects)
            {
                builder.AppendLine($"{project.ProjectID} - {project.ProjectName}");
            }
            clientHandler.SendPacket(PacketType.LIST_PROJECTS, builder.ToString());
        }

        public void HandleListFiles(Packet packet)
        {
            ListFilesRequest request = JsonHelper.Deserialize<ListFilesRequest>(packet.Data);
            List<ProjectFile> files = fileService.GetFilesByProject(request.ProjectID);
            StringBuilder builder = new StringBuilder();
            foreach (var file in files)
            {
                builder.AppendLine($"{file.FileID} - {file.FileName}");
            }
            clientHandler.SendPacket(PacketType.LIST_FILES, builder.ToString());
        }

        public void HandleOpenFile(Packet packet)
        {
            OpenFileRequest request = JsonHelper.Deserialize<OpenFileRequest>(packet.Data);
            if (clientHandler.CurrentRoom == null)
            {
                clientHandler.SendPacket(PacketType.FILE_NOT_FOUND);
                return;
            }
            if (!projectService.CanAccessFile(request.FileID, clientHandler.CurrentRoom.RoomId))
            {
                clientHandler.SendPacket(PacketType.FILE_NOT_FOUND);
                return;
            }
            bool readOnly = false;
            if (!FileLockManager.LockedFiles.TryAdd(request.FileID, clientHandler.Username))
            {
                string owner = FileLockManager.LockedFiles[request.FileID];
                if (owner != clientHandler.Username)
                {
                    Console.WriteLine($"[READ ONLY] File locked by {owner}");
                    readOnly = true;
                }
            }
            ProjectFile file = fileService.GetFileById(request.FileID);
            if (file == null)
            {
                if (!readOnly)
                {
                    FileLockManager.LockedFiles.TryRemove(request.FileID, out _);
                }
                clientHandler.SendPacket(PacketType.FILE_NOT_FOUND);
                return;
            }
            SyncFileContentRequest openResponse = new SyncFileContentRequest
            {
                FileID = file.FileID,
                Content = file.Content,
                Username = clientHandler.Username,
                ReadOnly = readOnly
            };

            clientHandler.CurrentFileId = file.FileID;
            clientHandler.SendPacket(PacketType.OPEN_FILE, JsonHelper.Serialize(openResponse));
        }

        public void HandleDeleteFile(Packet packet)
        {
            DeleteFileRequest request = JsonHelper.Deserialize<DeleteFileRequest>(packet.Data);
            if (clientHandler.CurrentUser == null || clientHandler.CurrentRoom == null)
            {
                clientHandler.SendPacket(PacketType.DELETE_FILE_FAILED, "Login and room are required.");
                return;
            }

            if (request.FileID <= 0)
            {
                clientHandler.SendPacket(PacketType.DELETE_FILE_FAILED, "Invalid file ID.");
                return;
            }

            if (!projectService.CanAccessFile(request.FileID, clientHandler.CurrentRoom.RoomId))
            {
                clientHandler.SendPacket(PacketType.DELETE_FILE_FAILED, "File not found in current room.");
                return;
            }

            if (FileLockManager.LockedFiles.TryGetValue(request.FileID, out string? owner) &&
                owner != clientHandler.Username)
            {
                clientHandler.SendPacket(PacketType.DELETE_FILE_FAILED, $"File is locked by {owner}.");
                return;
            }

            bool success = fileService.DeleteFile(request.FileID);
            if (success)
            {
                FileLockManager.LockedFiles.TryRemove(request.FileID, out _);
                if (clientHandler.CurrentFileId == request.FileID)
                {
                    clientHandler.CurrentFileId = null;
                }
                clientHandler.SendPacket(PacketType.DELETE_FILE_SUCCESS, request.FileID.ToString());
            }
            else
            {
                clientHandler.SendPacket(PacketType.DELETE_FILE_FAILED, "Delete failed.");
            }
        }

        public void HandleUpdateFileContent(Packet packet)
        {
            UpdateFileContentRequest request = JsonHelper.Deserialize<UpdateFileContentRequest>(packet.Data);
            if (!FileLockManager.LockedFiles.ContainsKey(request.FileID))
            {
                clientHandler.SendPacket(PacketType.UPDATE_FILE_FAILED, "File is not locked.");
                return;
            }
            if (FileLockManager.LockedFiles[request.FileID] != clientHandler.Username)
            {
                clientHandler.SendPacket(PacketType.UPDATE_FILE_FAILED, "You do not own this file lock.");
                return;
            }
            if (clientHandler.CurrentRoom == null)
            {
                clientHandler.SendPacket(PacketType.UPDATE_FILE_FAILED);
                return;
            }
            if (!projectService.CanAccessFile(request.FileID, clientHandler.CurrentRoom.RoomId))
            {
                clientHandler.SendPacket(PacketType.UPDATE_FILE_FAILED, "You do not have access to this file.");
                return;
            }
            bool success = fileService.UpdateFileContent(request.FileID, request.Content, clientHandler.CurrentUser.UserID);
            if (success)
            {
                clientHandler.SendPacket(PacketType.UPDATE_FILE_SUCCESS);
                SyncFileContentRequest sync = new SyncFileContentRequest
                {
                    FileID = request.FileID,
                    Content = request.Content,
                    Username = clientHandler.Username
                };
                BroadcastFileUpdate(sync);
            }
            else
            {
                clientHandler.SendPacket(PacketType.UPDATE_FILE_FAILED);
            }
        }

        private void BroadcastFileUpdate(SyncFileContentRequest request)
        {
            if (clientHandler.CurrentRoom == null) return;

            Packet packet = new Packet
            {
                Type = PacketType.SYNC_FILE_CONTENT.ToString(),
                Data = JsonHelper.Serialize(request)
            };
            string json = JsonHelper.Serialize(packet);
            foreach (ClientHandler client in clientHandler.CurrentRoom.Clients)
            {
                if (client == clientHandler)
                    continue;
                if (client.CurrentFileId != request.FileID)
                    continue;
                client.Send(json);
            }
        }
    }
}
