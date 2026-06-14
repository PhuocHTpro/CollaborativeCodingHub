using CollaborativeCodingServer.Core;
using System;

namespace CollaborativeCodingServer
{
    class Program
    {
        static void Main(string[] args)
        {
            ServerManager server = new ServerManager();

            server.Start(5000);

            Console.WriteLine("Press ENTER to exit...");
            Console.ReadLine();
        }
    }
}
