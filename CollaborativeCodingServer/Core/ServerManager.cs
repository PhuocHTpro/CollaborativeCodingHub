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

        public void Start(int port) // phương thức Start để khởi động server và lắng nghe kết nối từ client
        {
            listener = new TcpListener(IPAddress.Any, port);
            listener.Start();
            Console.WriteLine($"[SERVER] Started at port {port}");
            Task.Run(() => AcceptClients());
        }

        private void AcceptClients() // phương thức AcceptClients để chấp nhận kết nối từ client
        {
            while (true)
            {
                try
                {
                    TcpClient client = listener.AcceptTcpClient(); // chấp nhận kết nối từ client
                    Console.WriteLine("[SERVER] New Client Connected");
                    ClientHandler handler = new ClientHandler(client);
                    Task.Run(() => handler.Start()); // chạy phương thức Start của ClientHandler trong một luồng riêng để xử lý kết nối từ client
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        } 
    }
}
