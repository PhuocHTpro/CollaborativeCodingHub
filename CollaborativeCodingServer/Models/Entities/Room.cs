using CollaborativeCodingServer.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollaborativeCodingServer.Models.Entities
{
    public class Room
    {
        public string RoomId { get; set; }

        public string RoomName { get; set; }

        public string RoomCode { get; set; }

        public List<ClientHandler> Clients = new List<ClientHandler>();
    }
}
