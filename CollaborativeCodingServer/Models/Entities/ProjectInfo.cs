using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollaborativeCodingServer.Models.Entities
{
    public class ProjectInfo
    {
        public int ProjectID { get; set; }

        public string ProjectName { get; set; }

        public string RoomID { get; set; }

        public int CreatedBy { get; set; }
    }
}
