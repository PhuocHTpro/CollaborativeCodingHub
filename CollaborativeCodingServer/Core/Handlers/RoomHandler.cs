using System.Text;
using CollaborativeCodingServer.Core;
using CollaborativeCodingServer.Models.Entities;
using CollaborativeCodingServer.Models.Packets.Room;
using CollaborativeCodingServer.Network;
using CollaborativeCodingServer.Repositories;
using CollaborativeCodingServer.Services;

namespace CollaborativeCodingServer.Core.Handlers
{
    public class RoomHandler
    {
        private readonly ClientHandler clientHandler;
        private readonly RoomRepository roomRepository = new RoomRepository();

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
            if (clientHandler.CurrentUser == null)
            {
                clientHandler.SendPacket(PacketType.CREATE_ROOM_FAILED);
                return;
            }

            CreateRoomRequest request = JsonHelper.Deserialize<CreateRoomRequest>(packet.Data);
            if (string.IsNullOrWhiteSpace(request.RoomName))
            {
                clientHandler.SendPacket(PacketType.CREATE_ROOM_FAILED);
                return;
            }

            Room room = new Room
            {
                RoomId = Guid.NewGuid().ToString().Substring(0, 6).ToUpper(),
                RoomName = request.RoomName,
                OwnerID = clientHandler.CurrentUser.UserID
            };

            bool created = roomRepository.CreateRoom(room);
            if (!created)
            {
                clientHandler.SendPacket(PacketType.CREATE_ROOM_FAILED);
                return;
            }

            if (clientHandler.CurrentRoom != null)
            {
                clientHandler.CurrentRoom.Clients.Remove(clientHandler);
            }
            room.Clients.Add(clientHandler);
            clientHandler.CurrentRoom = room;
            RoomManager.Rooms.Add(room);
            Console.WriteLine($"[ROOM CREATED] {room.RoomName} ({room.RoomId})");
            clientHandler.SendPacket(PacketType.CREATE_ROOM_SUCCESS, room.RoomId);
        }

        public void HandleJoinRoom(Packet packet)
        {
            if (clientHandler.CurrentUser == null)
            {
                clientHandler.SendPacket(PacketType.JOIN_ROOM_FAILED);
                return;
            }

            JoinRoomRequest request = JsonHelper.Deserialize<JoinRoomRequest>(packet.Data);
            Room room = RoomManager.Rooms.FirstOrDefault(r => r.RoomId == request.RoomId);
            if (room == null)
            {
                room = roomRepository.GetRoomById(request.RoomId);
                if (room != null)
                {
                    if (!RoomManager.Rooms.Any(r => r.RoomId == room.RoomId))
                    {
                        RoomManager.Rooms.Add(room);
                    }
                }
            }

            if (room == null)
            {
                clientHandler.SendPacket(PacketType.ROOM_NOT_FOUND);
                return;
            }

            if (!room.Clients.Contains(clientHandler))
            {
                if (clientHandler.CurrentRoom != null && clientHandler.CurrentRoom != room)
                {
                    clientHandler.CurrentRoom.Clients.Remove(clientHandler);
                }
                room.Clients.Add(clientHandler);
            }

            clientHandler.CurrentRoom = room;
            Console.WriteLine($"[JOIN ROOM] {room.RoomName} ({room.RoomId})");
            clientHandler.SendPacket(PacketType.JOIN_ROOM_SUCCESS, room.RoomId);
        }

        private void BroadcastToRoom(string message)
        {
            Packet packet = new Packet
            {
                Type = PacketType.CHAT.ToString(),
                Data = message
            };
            string json = JsonHelper.Serialize(packet);
            foreach (ClientHandler client in clientHandler.CurrentRoom.Clients.ToList())
            {
                if (!client.Send(json))
                {
                    clientHandler.CurrentRoom.Clients.Remove(client);
                }
            }
        }
    }
}
