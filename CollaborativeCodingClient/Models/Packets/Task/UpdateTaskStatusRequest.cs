using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollaborativeCodingClient.Models.Packets.Task
{
    public class UpdateTaskStatusRequest
    {
        public int TaskID { get; set; }
        public string Status { get; set; } = "";
    }
}
