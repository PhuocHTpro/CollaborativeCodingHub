using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CollaborativeCodingServer.Database;
using CollaborativeCodingServer.Models;

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
