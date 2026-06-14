using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using CollaborativeCodingServer.Protocol;

namespace CollaborativeCodingServer.Core
{
    public class ClientHandler
    {
        private readonly TcpClient client;
        private readonly NetworkStream stream;

        public ClientHandler(TcpClient client)
        {
            this.client = client;
            stream = client.GetStream();
        }

        public void Start()
        {
            byte[] buffer = new byte[4096];

            try
            {
                while (true)
                {
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);

                    if (bytesRead == 0)
                    {
                        Console.WriteLine("[SERVER] Client Disconnected");
                        break;
                    }

                    string json = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                    Packet packet = JsonHelper.Deserialize(json);

                    switch (packet.Type)
                    {
                        case "CHAT":
                            HandleChat(packet);
                            break;

                        case "LOGIN":
                            HandleLogin(packet);
                            break;

                        default:
                            Console.WriteLine("[SERVER] Unknown Packet");
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                stream.Close();
                client.Close();
            }
        }

        private void Send(string message)
        {
            byte[] data = Encoding.UTF8.GetBytes(message);

            stream.Write(data, 0, data.Length);
        }

        private void HandleChat(Packet packet)
        {
            Console.WriteLine($"[CHAT] {packet.Data}");

            Send("Chat packet received");
        }

        private void HandleLogin(Packet packet)
        {
            Console.WriteLine("[LOGIN REQUEST]");

            Send("Login packet received");
        }
    }
}
