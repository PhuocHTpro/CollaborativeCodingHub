using CollaborativeCodingServer.Models.Entities;
using CollaborativeCodingServer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollaborativeCodingServer.Services
{
    public class ReplayService
    {
        private readonly FileHistoryRepository historyRepository;
        public ReplayService()
        {
            historyRepository = new FileHistoryRepository();
        }

        public bool SaveVersion(FileHistory history) {
            int latestVersion = historyRepository.GetLatestVersion(history.FileID);
            history.VersionNo = latestVersion + 1;

            return historyRepository.SaveHistory(history);
        }
    }
}
