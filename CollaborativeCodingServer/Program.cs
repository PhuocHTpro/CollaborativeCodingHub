using CollaborativeCodingServer.Core;
using CollaborativeCodingServer.Database;
using System;

namespace CollaborativeCodingServer
{
    class Program
    {
        static void Main(string[] args)
        {
            DatabaseTester.Test();

            // Test
            UserRepository repo = new UserRepository();
            Console.WriteLine(repo.UserExists("admin"));

            ServerManager server = new ServerManager();

            server.Start(5000);

            Console.WriteLine("Press ENTER to exit...");
            Console.ReadLine();
        }
    }
}
