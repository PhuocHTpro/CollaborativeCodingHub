using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollaborativeCodingServer.Models.Entities
{
    public class ProjectFile
    {
        public int FileID { get; set; }

        public int ProjectID { get; set; }

        public string FileName { get; set; }

        public string Content { get; set; }

        public int CreatedBy { get; set; }

        public int LastModifiedBy { get; set; }
    }
}
