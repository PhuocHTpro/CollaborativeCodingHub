using CollaborativeCodingServer.Models.Entities;
using CollaborativeCodingServer.Repositories;

namespace CollaborativeCodingServer.Services
{
    public class ProjectService
    {
        private readonly ProjectRepository repository = new ProjectRepository();

        public int CreateProject(string projectName, string roomID, int createdBy)
        {
            ProjectInfo project = new ProjectInfo
            {
                ProjectName = projectName,
                RoomID = roomID,
                CreatedBy = createdBy
            };

            return repository.CreateProject(project);
        }

        public List<ProjectInfo> GetProjects(string roomId)
        {
            return repository.GetProjects(roomId);
        }

        public bool CanAccessFile(int fileId, string roomId)
        {
            return repository.IsFileInRoom(fileId, roomId);
        }

        public bool ProjectExists(int projectID)
        {
            return repository.ProjectExists(projectID);
        }
    }
}
