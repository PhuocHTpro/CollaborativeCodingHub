using CollaborativeCodingServer.Models.Packets.Replay;
using CollaborativeCodingServer.Network;
using CollaborativeCodingServer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text;
using CollaborativeCodingServer.Models.Entities;

namespace CollaborativeCodingServer.Core.Handlers
{
    public class ReplayHandler
    {
        private readonly ClientHandler clientHandler;
        private readonly ReplayService replayService = new ReplayService();

        public ReplayHandler(ClientHandler clientHandler)
        {
            this.clientHandler = clientHandler;
        }

        public void HandleListHistory(Packet packet)
        {
            ListHistoryRequest request = JsonHelper.Deserialize<ListHistoryRequest>(packet.Data);
            List<FileHistory> histories = replayService.GetHistory(request.FileID);

            if (histories.Count == 0)
            {
                clientHandler.SendPacket(PacketType.LIST_HISTORY_FAILED, "No history found.");
                return;
            }

            List<HistoryInfoResponse> response = new();
            foreach (FileHistory history in histories)
            {
                response.Add(new HistoryInfoResponse
                {
                    HistoryID = history.HistoryID,
                    VersionNo = history.VersionNo,
                    EditedTime = history.EditedTime,
                    EditedBy = history.EditedBy,
                    ChangeSummary = history.ChangeSummary
                });
            }

            clientHandler.SendPacket(PacketType.LIST_HISTORY_SUCCESS, JsonHelper.Serialize(response));
        }
        public void HandleOpenHistory(Packet packet)
        {
            OpenHistoryRequest request =
                JsonHelper.Deserialize<OpenHistoryRequest>(packet.Data);

            FileHistory? history =
                replayService.GetHistoryById(request.HistoryID);

            if (history == null)
            {
                clientHandler.SendPacket(
                    PacketType.OPEN_HISTORY_FAILED,
                    "History not found.");

                return;
            }

            clientHandler.SendPacket(PacketType.OPEN_HISTORY_SUCCESS, history.Content);
        }
    }
}

