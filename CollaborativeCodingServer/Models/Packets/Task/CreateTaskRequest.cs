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
        public string TaskName { get; set; }
        public string Description { get; set; }
        public int AssignedTo { get; set; }
        public string Priority { get; set; }
        public DateTime? Deadline { get; set; }
    }
}
