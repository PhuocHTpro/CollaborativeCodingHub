using System.Text;
using CollaborativeCodingServer.Core;
using CollaborativeCodingServer.Models.Entities;
using CollaborativeCodingServer.Models.Packets.Room;
using CollaborativeCodingServer.Network;
using CollaborativeCodingServer.Services;

namespace CollaborativeCodingServer.Core.Handlers
{
    public class RoomHandler
    {
        private readonly ClientHandler clientHandler;

        public RoomHandler(ClientHandler clientHandler)
        {
            this.clientHandler = clientHandler;
        }

        public void HandleChat(Packet packet)
        {
            if (clientHandler.CurrentRoom == null)
            {
                clientHandler.Send("YOU_ARE_NOT_IN_ROOM");
                return;
            }

            string message = $"[{clientHandler.CurrentRoom.RoomName}] {clientHandler.Username}: {packet.Data}";
            Console.WriteLine(message);
            BroadcastToRoom(message);
        }

        public void HandleCreateRoom(Packet packet)
        {
            CreateRoomRequest request = JsonHelper.Deserialize<CreateRoomRequest>(packet.Data);
            Room room = new Room
            {
                RoomId = Guid.NewGuid().ToString().Substring(0, 6).ToUpper(),
                RoomName = request.RoomName
            };

            room.Clients.Add(clientHandler);
            clientHandler.CurrentRoom = room;
            RoomManager.Rooms.Add(room);
            Console.WriteLine($"[ROOM CREATED] {room.RoomName}");
            clientHandler.SendPacket(PacketType.CREATE_ROOM_SUCCESS, room.RoomId);
        }

        public void HandleJoinRoom(Packet packet)
        {
            JoinRoomRequest request = JsonHelper.Deserialize<JoinRoomRequest>(packet.Data);
            Room room = RoomManager.Rooms.FirstOrDefault(r => r.RoomId == request.RoomId);
            if (room == null)
            {
                clientHandler.SendPacket(PacketType.ROOM_NOT_FOUND);
                return;
            }

            room.Clients.Add(clientHandler);
            clientHandler.CurrentRoom = room;
            Console.WriteLine($"[JOIN ROOM] {room.RoomName}");
            clientHandler.SendPacket(PacketType.JOIN_ROOM_SUCCESS);
        }

        private void BroadcastToRoom(string message)
        {
            Packet packet = new Packet
            {
                Type = PacketType.CHAT.ToString(),
                Data = message
            };
            string json = JsonHelper.Serialize(packet);
            foreach (ClientHandler client in clientHandler.CurrentRoom.Clients)
            {
                client.Send(json);
            }
        }
    }
}
