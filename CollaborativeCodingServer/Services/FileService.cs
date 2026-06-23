using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CollaborativeCodingServer.Database;
using CollaborativeCodingServer.Models;

namespace CollaborativeCodingServer.Services
{
    public class FileService
    {
        private readonly FileRepository repository = new FileRepository();

        public bool CreateFile(int projectID, string fileName)
        {
            ProjectFile file = new ProjectFile
            {
                ProjectID = projectID,
                FileName = fileName,
                Content = ""
            };

            return repository.CreateFile(file);
        }

        public List<ProjectFile> GetFilesByProject(int projectID)
        {
            return repository.GetFilesByProject(projectID);
        }

        public ProjectFile GetFileById(int fileID)
        {
            return repository.GetFileById(fileID);
        }

        public bool UpdateFileContent(int fileID, string content)
        {
            return repository.UpdateFileContent(fileID, content);
        }
    }
}
