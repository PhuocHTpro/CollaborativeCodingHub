using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollaborativeCodingClient.Network
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
        CREATE_FILE,
        LIST_PROJECTS,
        LIST_FILES,
        OPEN_FILE,
        UPDATE_FILE_CONTENT,
        DELETE_FILE,
        SYNC_FILE_CONTENT,
        INSERT,
        DELETE,
        ASSIGN_TASK,
        COMPILE,

        // RESPONSE
        LOGIN_SUCCESS,
        LOGIN_FAILED,

        REGISTER_SUCCESS,
        REGISTER_FAILED,

        CREATE_ROOM_SUCCESS,
        CREATE_ROOM_FAILED,

        JOIN_ROOM_SUCCESS,
        JOIN_ROOM_FAILED,

        CREATE_PROJECT_SUCCESS,
        CREATE_PROJECT_FAILED,

        CREATE_FILE_SUCCESS,
        CREATE_FILE_FAILED,

        UPDATE_FILE_SUCCESS,
        UPDATE_FILE_FAILED,

        FILE_NOT_FOUND,
        ROOM_NOT_FOUND
    }
}
