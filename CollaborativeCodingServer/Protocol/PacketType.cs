using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollaborativeCodingServer.Protocol
{
    public enum PacketType
    {
        CHAT,
        LOGIN,
        REGISTER,
        CREATE_ROOM,
        JOIN_ROOM,
        LEAVE_ROOM,
        CREATE_PROJECT,
        OPEN_FILE,
        INSERT,
        DELETE,
        ASSIGN_TASK,
        COMPILE
    }
}
