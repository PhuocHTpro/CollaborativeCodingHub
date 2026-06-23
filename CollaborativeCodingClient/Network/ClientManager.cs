using CollaborativeCodingClient.Models;
using CollaborativeCodingClient.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace CollaborativeCodingClient.Network
{
    public class ClientManager
    {
        private TcpClient client;
        private NetworkStream stream;
        public string CurrentFileName { get; set; }

        public void Connect(string ip, int port)
        {
            try
            {
                client = new TcpClient();
                client.Connect(ip, port);
                stream = client.GetStream();
                Console.WriteLine("[CLIENT] Connected");
                Task.Run(() => ReceiveLoop());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void Send(string message)
        {
            try
            {
                byte[] data = Encoding.UTF8.GetBytes(message);
                stream.Write(data, 0, data.Length);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void ReceiveLoop()
        {
            byte[] buffer = new byte[4096];
            try
            {
                while (true)
                {
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);
                    if (bytesRead == 0)
                        break;
                    string response = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    Packet packet = JsonHelper.Deserialize<Packet>( response);
                    HandlePacket(packet);
                }
            }
            catch
            {
            }
        }

        public string CurrentFileContent { get; private set; } = string.Empty;

        public int CurrentFileID { get; private set; }

        private void HandleSyncFile(Packet packet)
        {
            SyncFileContentRequest sync = JsonHelper.Deserialize<SyncFileContentRequest>(packet.Data);
            CurrentFileID = sync.FileID;
            CurrentFileContent = sync.Content;
            Console.WriteLine();
            Console.WriteLine($"[SYNC FROM {sync.Username}]");
            Console.WriteLine(sync.Content);
        }

        private void HandlePacket(Packet packet)
        {
            switch (packet.Type)
            {
                case nameof(PacketType.LOGIN_SUCCESS): 
                    Console.WriteLine("[LOGIN SUCCESS]");
                    break;

                case nameof(PacketType.LOGIN_FAILED): 
                    Console.WriteLine("[LOGIN FAILED]");
                    break;

                case nameof(PacketType.REGISTER_SUCCESS): 
                    Console.WriteLine("[REGISTER SUCCESS]");
                    break;

                case nameof(PacketType.CREATE_ROOM_SUCCESS):
                    Console.WriteLine("[ROOM CREATED]");
                    Console.WriteLine($"Room ID: {packet.Data}");
                    break;

                case nameof(PacketType.JOIN_ROOM_SUCCESS):
                    Console.WriteLine("[ROOM JOINED]");
                    Console.WriteLine(packet.Data);
                    break;

                case nameof(PacketType.CREATE_PROJECT_SUCCESS): 
                    Console.WriteLine("[PROJECT CREATED]");
                    break;

                case nameof(PacketType.CREATE_FILE_SUCCESS): 
                    Console.WriteLine("[FILE CREATED]");
                    break;

                case nameof(PacketType.UPDATE_FILE_SUCCESS): 
                    Console.WriteLine("[FILE SAVED]");
                    break;

                case nameof(PacketType.OPEN_FILE):
                    CurrentFileContent = packet.Data;
                    Console.WriteLine();
                    Console.WriteLine("=====FILE CONTENT=====");
                    Console.WriteLine(packet.Data);
                    break;

                case nameof(PacketType.SYNC_FILE_CONTENT):
                    HandleSyncFile(packet);
                    break;

                default: 
                    Console.WriteLine(packet.Data);
                    break;
            }
        }
    }
}
