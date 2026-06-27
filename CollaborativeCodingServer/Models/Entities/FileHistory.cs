using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollaborativeCodingServer.Models.Entities
{
    public class FileHistory
    {
        public int HistoryID { get; set; }
        public int FileID { get; set; }
        public int VersionNo { get; set; }
        public string Content { get; set; }
        public int EditedBy { get; set; }
        public DateTime EditedTime { get; set; }
        public string ChangeSummary { get; set; }
    }
}
