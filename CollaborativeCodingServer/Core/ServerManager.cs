using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace CollaborativeCodingServer.Core
{
    public class ServerManager
    {
        private TcpListener listener;

        public void Start(int port)
        {
            listener = new TcpListener(IPAddress.Any, port);

            listener.Start();

            Console.WriteLine($"[SERVER] Started at port {port}");

            Task.Run(() => AcceptClients());
        }

        private void AcceptClients()
        {
            while (true)
            {
                try
                {
                    TcpClient client = listener.AcceptTcpClient();

                    Console.WriteLine("[SERVER] New Client Connected");

                    ClientHandler handler = new ClientHandler(client);

                    Task.Run(() => handler.Start());
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
