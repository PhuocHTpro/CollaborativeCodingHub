using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollaborativeCodingServer.Models.Packets.Task
{
    public class CreateTaskRequest
    {
        public int ProjectID { get; set; }
        public string TaskName { get; set; } = "";
        public int? AssignedTo { get; set; }
    }
}
