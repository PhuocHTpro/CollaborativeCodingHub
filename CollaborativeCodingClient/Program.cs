using CollaborativeCodingClient.Network;
using CollaborativeCodingClient.Protocol;
using System;

namespace CollaborativeCodingClient
{
    class Program
    {
        static void Main(string[] args)
        {
            ClientManager client = new ClientManager();

            client.Connect("127.0.0.1", 5000);

            while (true)
            {
                string message = Console.ReadLine();

                if (message == "exit")
                    break;

                Packet packet = new Packet
                {
                    Type = PacketType.CHAT.ToString(),
                    Data = message
                };

                string json = JsonHelper.Serialize(packet);

                client.Send(json);
            }
        }
    }
}
