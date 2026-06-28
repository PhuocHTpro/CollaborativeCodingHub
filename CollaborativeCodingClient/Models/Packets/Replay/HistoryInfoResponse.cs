using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollaborativeCodingClient.Models.Packets.Replay
{
    public class HistoryInfoResponse
    {
        public int HistoryID { get; set; }

        public int VersionNo { get; set; }

        public DateTime EditedTime { get; set; }

        public int EditedBy { get; set; }

        public string ChangeSummary { get; set; } = "";
    }
}