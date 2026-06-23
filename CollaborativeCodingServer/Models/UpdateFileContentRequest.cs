using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollaborativeCodingServer.Models
{
    public class UpdateFileContentRequest
    {
        public int FileID { get; set; }
        public string Content { get; set; }
    }
}
