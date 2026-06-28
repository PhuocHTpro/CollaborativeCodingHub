using CollaborativeCodingServer.Models.Entities;
using CollaborativeCodingServer.Repositories;

namespace CollaborativeCodingServer.Services
{
    public class ReplayService
    {
        private readonly FileHistoryRepository historyRepository;

        public ReplayService()
        {
            historyRepository = new FileHistoryRepository();
        }

        public bool SaveVersion(FileHistory history)
        {
            int latestVersion = historyRepository.GetLatestVersion(history.FileID);
            history.VersionNo = latestVersion + 1;

            return historyRepository.SaveHistory(history);
        }

        public List<FileHistory> GetHistory(int fileID)
        {
            return historyRepository.GetHistoryByFile(fileID);
        }

        public FileHistory? GetHistoryById(int historyID)
        {
            return historyRepository.GetHistoryById(historyID);
        }
    }
}