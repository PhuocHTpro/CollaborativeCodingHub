using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollaborativeCodingServer.Models.Packets.Auth
{
    public class RegisterRequest
    {
        public string Username { get; set; }

        public string Password { get; set; }
    }
}
