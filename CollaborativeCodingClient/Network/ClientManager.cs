using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace CollaborativeCodingClient.Network
{
    public class ClientManager
    {
        private TcpClient client;
        private NetworkStream stream;

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

                    Console.WriteLine();
                    Console.WriteLine(response);
                }
            }
            catch
            {
            }
        }
    }
}
