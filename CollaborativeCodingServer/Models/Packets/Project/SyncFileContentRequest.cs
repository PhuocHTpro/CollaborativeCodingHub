using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollaborativeCodingServer.Models.Packets.Project
{
    public class SyncFileContentRequest
    {
        public int FileID { get; set; }

        public string Content { get; set; }

        public string Username { get; set; }
    }
}
