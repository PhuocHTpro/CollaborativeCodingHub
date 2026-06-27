using CollaborativeCodingClient.Models.Packets.Auth;
using CollaborativeCodingClient.Models.Packets.Project;
using CollaborativeCodingClient.Models.Packets.Room;
using CollaborativeCodingClient.Network;
using System.Text;

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
                Console.WriteLine();
                Console.WriteLine("1. Register");
                Console.WriteLine("2. Login");
                Console.WriteLine("3. Chat");
                Console.WriteLine("4. Create Room");
                Console.WriteLine("5. Join Room");
                Console.WriteLine("6. Create Project");
                Console.WriteLine("7. Create File");
                Console.WriteLine("8. List Projects");
                Console.WriteLine("9. List Files");
                Console.WriteLine("10. Open File");
                Console.WriteLine("11. Save File");
                Console.WriteLine("0. Exit");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Register(client);
                        break;

                    case "2":
                        Login(client);
                        break;

                    case "3":
                        SendChat(client);
                        break;

                    case "4":
                        CreateRoom(client);
                        break;

                    case "5":
                        JoinRoom(client);
                        break;

                    case "6":
                        CreateProject(client);
                        break;

                    case "7":
                        CreateFile(client);
                        break;

                    case "8":
                        ListProjects(client);
                        break;

                    case "9":
                        ListFiles(client);
                        break;

                    case "10":
                        OpenFile(client);
                        break;

                    case "11":
                        SaveFile(client);
                        break;

                    case "0":
                        return;
                }
            }
        }

        private static void Register(ClientManager client)
        {
            Console.Write("Username: ");
            string username = Console.ReadLine();

            Console.Write("Password: ");
            string password = Console.ReadLine();

            RegisterRequest request = new RegisterRequest
            {
                Username = username,
                Password = password
            };

            Packet packet = new Packet
            {
                Type = PacketType.REGISTER.ToString(),
                Data = JsonHelper.Serialize(request)
            };

            string json = JsonHelper.Serialize(packet);

            client.Send(json);
        }

        private static void Login(ClientManager client)
        {
            Console.Write("Username: ");
            string username = Console.ReadLine();

            Console.Write("Password: ");
            string password = Console.ReadLine();

            LoginRequest request = new LoginRequest
            {
                Username = username,
                Password = password
            };

            Packet packet = new Packet
            {
                Type = PacketType.LOGIN.ToString(),
                Data = JsonHelper.Serialize(request)
            };

            string json = JsonHelper.Serialize(packet);

            client.Send(json);
        }

        private static void SendChat(ClientManager client)
        {
            Console.Write("Message: ");

            string message = Console.ReadLine();

            Packet packet = new Packet
            {
                Type = PacketType.CHAT.ToString(),
                Data = message
            };

            string json = JsonHelper.Serialize(packet);

            client.Send(json);
        }

        private static void CreateRoom(ClientManager client)
        {
            Console.Write("Room Name: ");

            string roomName = Console.ReadLine();

            CreateRoomRequest request = new CreateRoomRequest
            {
                RoomName = roomName
            };

            Packet packet = new Packet
            {
                Type = PacketType.CREATE_ROOM.ToString(),

                Data = JsonHelper.Serialize(request)
            };

            string json = JsonHelper.Serialize(packet);
            client.Send(json);
        }

        private static void JoinRoom(ClientManager client)
        {
            Console.Write("Room Id: ");

            string roomId = Console.ReadLine();

            JoinRoomRequest request = new JoinRoomRequest
            {
                RoomId = roomId
            };

            Packet packet = new Packet
            {
                Type = PacketType.JOIN_ROOM.ToString(),

                Data = JsonHelper.Serialize(request)
            };

            string json = JsonHelper.Serialize(packet);

            client.Send(json);
        }

        private static void CreateProject(ClientManager client)
        {
            Console.Write("Project Name: ");

            string projectName = Console.ReadLine();

            Console.Write("Room Id: ");

            string roomId = Console.ReadLine();

            CreateProjectRequest request = new CreateProjectRequest
            {
                ProjectName = projectName,
                RoomID = roomId
            };

            Packet packet = new Packet
            {
                Type = PacketType.CREATE_PROJECT.ToString(),

                Data = JsonHelper.Serialize(request)
            };

            string json = JsonHelper.Serialize(packet);

            client.Send(json);
        }

        private static void CreateFile(ClientManager client)
        {
            Console.Write("Project ID: ");

            int projectID = int.Parse(Console.ReadLine());

            Console.Write("File Name: ");

            string fileName = Console.ReadLine();

            CreateFileRequest request = new CreateFileRequest
            {
                ProjectID = projectID,
                FileName = fileName
            };

            Packet packet = new Packet
            {
                Type = PacketType.CREATE_FILE.ToString(),

                Data = JsonHelper.Serialize(request)
            };

            string json = JsonHelper.Serialize(packet);

            client.Send(json);
        }

        private static void ListProjects(ClientManager client)
        {
            Packet packet = new Packet
            {
                Type = PacketType.LIST_PROJECTS.ToString(),

                Data = ""
            };

            string json = JsonHelper.Serialize(packet);

            client.Send(json);
        }

        private static void ListFiles(ClientManager client)
        {
            Console.Write("Project ID: ");
            int projectID = int.Parse(Console.ReadLine());

            ListFilesRequest request = new ListFilesRequest
            {
                ProjectID = projectID
            };

            Packet packet = new Packet
            {
                Type = PacketType.LIST_FILES.ToString(),
                Data = JsonHelper.Serialize(request)
            };

            string json = JsonHelper.Serialize(packet);
            client.Send(json);
        }

        private static void OpenFile(ClientManager client)
        {
            Console.Write("File ID: ");
            int fileID = int.Parse(Console.ReadLine());
            OpenFileRequest request = new OpenFileRequest
            {
                FileID = fileID
            };
            Packet packet = new Packet
            {
                Type = PacketType.OPEN_FILE.ToString(),
                Data = JsonHelper.Serialize(request)
            };
            string json = JsonHelper.Serialize(packet);
            client.Send(json);
        }

        private static void SaveFile(ClientManager client)
        {
            Console.Write("File ID: ");
            int fileID = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter code (END để kết thúc):");
            StringBuilder builder = new StringBuilder();
            while (true)
            {
                string line = Console.ReadLine();
                if (line == "END")
                    break;

                builder.AppendLine(line);
            }
            UpdateFileContentRequest request = new UpdateFileContentRequest
            {
                FileID = fileID,
                Content = builder.ToString()
            };
            Packet packet = new Packet
            {
                Type = PacketType.UPDATE_FILE_CONTENT.ToString(),
                Data = JsonHelper.Serialize(request)
            };
            string json = JsonHelper.Serialize(packet);
            client.Send(json);
        }
    }
}