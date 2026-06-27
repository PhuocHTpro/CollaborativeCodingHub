using CollaborativeCodingServer.Models.Entities;
using CollaborativeCodingServer.Repositories;

namespace CollaborativeCodingServer.Services
{
    public class ProjectService
    {
        private readonly ProjectRepository repository = new ProjectRepository();

        public bool CreateProject(string projectName, string roomID)
        {
            ProjectInfo project = new ProjectInfo
            {
                ProjectName = projectName,
                RoomID = roomID
            };

            return repository.CreateProject(project);
        }

        public List<ProjectInfo> GetProjects()
        {
            return repository.GetProjects();
        }
    }
}
