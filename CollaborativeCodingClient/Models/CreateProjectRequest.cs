using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollaborativeCodingClient.Models
{
    public class CreateProjectRequest
    {
        public string ProjectName { get; set; }

        public string RoomID { get; set; }
    }
}

