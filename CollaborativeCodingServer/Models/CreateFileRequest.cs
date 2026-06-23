using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollaborativeCodingServer.Models
{
    public class CreateFileRequest
    {
        public int ProjectID { get; set; }

        public string FileName { get; set; }
    }
}
