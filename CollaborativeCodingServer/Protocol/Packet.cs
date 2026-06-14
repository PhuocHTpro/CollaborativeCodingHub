using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollaborativeCodingServer.Protocol
{
    public class Packet
    {
        public string Type { get; set; }

        public string Data { get; set; }
    }
}
