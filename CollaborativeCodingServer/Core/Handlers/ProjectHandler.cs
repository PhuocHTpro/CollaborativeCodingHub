using System.Text;
using CollaborativeCodingServer.Core;
using CollaborativeCodingServer.Models.Entities;
using CollaborativeCodingServer.Models.Packets.Project;
using CollaborativeCodingServer.Network;
using CollaborativeCodingServer.Services;

namespace CollaborativeCodingServer.Core.Handlers
{
    public class ProjectHandler
    {
        private readonly ClientHandler clientHandler;
        private readonly ProjectService projectService = new ProjectService();
        private readonly FileService fileService = new FileService();

        public ProjectHandler(ClientHandler clientHandler)
        {
            this.clientHandler = clientHandler;
        }

        public void HandleCreateProject(Packet packet)
        {
            CreateProjectRequest request = JsonHelper.Deserialize<CreateProjectRequest>(packet.Data);
            bool success = projectService.CreateProject(request.ProjectName, request.RoomID);
            if (success)
            {
                clientHandler.SendPacket(PacketType.CREATE_PROJECT_SUCCESS);
            }
            else
            {
                clientHandler.SendPacket(PacketType.CREATE_PROJECT_FAILED);
            }
        }

        public void HandleCreateFile(Packet packet)
        {
            CreateFileRequest request = JsonHelper.Deserialize<CreateFileRequest>(packet.Data);
            bool success = fileService.CreateFile(request.ProjectID, request.FileName);
            if (success)
            {
                clientHandler.SendPacket(PacketType.CREATE_FILE_SUCCESS);
            }
            else
            {
                clientHandler.SendPacket(PacketType.CREATE_FILE_FAILED);
            }
        }

        public void HandleListProjects()
        {
            List<ProjectInfo> projects = projectService.GetProjects();
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
            if (FileLockManager.LockedFiles.ContainsKey(request.FileID))
            {
                string owner = FileLockManager.LockedFiles[request.FileID];
                Console.WriteLine($"[LOCKED BY] {owner}");
            }
            else
            {
                FileLockManager.LockedFiles[request.FileID] = clientHandler.Username;
            }

            ProjectFile file = fileService.GetFileById(request.FileID);
            if (file == null)
            {
                clientHandler.SendPacket(PacketType.FILE_NOT_FOUND);
                return;
            }

            clientHandler.SendPacket(PacketType.OPEN_FILE, file.Content);
        }

        public void HandleUpdateFileContent(Packet packet)
        {
            UpdateFileContentRequest request = JsonHelper.Deserialize<UpdateFileContentRequest>(packet.Data);
            bool success = fileService.UpdateFileContent(request.FileID, request.Content);
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

                client.Send(json);
            }
        }
    }
}
