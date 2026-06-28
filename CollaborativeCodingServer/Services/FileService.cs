using CollaborativeCodingServer.Models.Entities;
using CollaborativeCodingServer.Repositories;

namespace CollaborativeCodingServer.Services
{
    public class FileService
    {
        private readonly FileRepository repository = new FileRepository();
        private readonly ReplayService replayService = new ReplayService();

        public int CreateFile(int projectID, string fileName, int createdBy)
        {
            ProjectFile file = new ProjectFile
            {
                ProjectID = projectID,
                FileName = fileName,
                Content = string.Empty,
                CreatedBy = createdBy,
                LastModifiedBy = createdBy
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

        public bool UpdateFileContent(int fileID, string content, int editedBy)
        {
            bool success = repository.UpdateFileContent(fileID, content);

            if (!success)
                return false;

            FileHistory history = new FileHistory
            {
                FileID = fileID,
                Content = content,
                EditedBy = editedBy,
                ChangeSummary = "Update File"
            };

            replayService.SaveVersion(history);
            return true;
        }
    }
}
