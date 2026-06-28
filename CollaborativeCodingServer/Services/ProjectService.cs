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

        public List<ProjectInfo> GetProjects()
        {
            return repository.GetProjects();
        }
    }
}
