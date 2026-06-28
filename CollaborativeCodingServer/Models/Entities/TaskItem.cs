using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollaborativeCodingServer.Models.Entities
{
    public class TaskItem
    {
        public int TaskID { get; set; }
        public int ProjectID { get; set; }
        public string TaskName { get; set; }
        public string Description { get; set; }
        public int? AssignedTo { get; set; }
        public int CreatedBy { get; set; }
        public string Status { get; set; }
        public string Priority { get; set; }
        public int Progress { get; set; }
        public DateTime? Deadline { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
