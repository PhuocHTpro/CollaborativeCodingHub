using CollaborativeCodingClient.Models.Packets.Project;
using CollaborativeCodingClient.Network;
using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using CollaborativeCodingClient.Models.Packets.Replay;

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
            byte[] buffer = new byte[65536];
            var messageBuffer = new System.Text.StringBuilder();
            try
            {
                while (true)
                {
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);
                    if (bytesRead == 0)
                        break;
                    string chunk = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    messageBuffer.Append(chunk);
                    string raw = messageBuffer.ToString();

                    int start = 0;
                    while (start < raw.Length)
                    {
                        int begin = raw.IndexOf('{', start);
                        if (begin < 0) break;

                        int depth = 0;
                        int end = -1;
                        for (int i = begin; i < raw.Length; i++)
                        {
                            if (raw[i] == '{') depth++;
                            else if (raw[i] == '}')
                            {
                                depth--;
                                if (depth == 0) { end = i; break; }
                            }
                        }

                        if (end < 0) break; // Packet chưa đầy đủ, đợi thêm data

                        string jsonStr = raw.Substring(begin, end - begin + 1);
                        try
                        {
                            Packet packet = JsonHelper.Deserialize<Packet>(jsonStr);
                            HandlePacket(packet);
                        }
                        catch { /* bỏ qua packet lỗi */ }

                        start = end + 1;
                    }

                    // Giữ lại phần chưa được xử lý
                    if (start < raw.Length)
                        messageBuffer = new System.Text.StringBuilder(raw.Substring(start));
                    else
                        messageBuffer.Clear();
                }
            }
            catch
            {
            }
        }

        public string CurrentFileContent { get; set; } = string.Empty;

        public int CurrentFileID { get; set; }

        public bool IsConnected => client?.Connected ?? false;

        public event Action<Packet>? PacketReceived;

        private void HandleSyncFile(Packet packet)
        {
            SyncFileContentRequest sync = JsonHelper.Deserialize<SyncFileContentRequest>(packet.Data);
            CurrentFileID = sync.FileID;
            CurrentFileContent = sync.Content;
            PacketReceived?.Invoke(packet);
            Console.WriteLine();
            Console.WriteLine($"[SYNC FROM {sync.Username}]");
            Console.WriteLine(sync.Content);
        }

        private void HandleHistory(Packet packet)
        {
            List<HistoryInfoResponse> histories = JsonHelper.Deserialize<List<HistoryInfoResponse>>(packet.Data);
            Console.WriteLine();
            Console.WriteLine("===== FILE HISTORY =====");

            foreach (HistoryInfoResponse history in histories)
            {
                Console.WriteLine($"History ID: {history.HistoryID}");
                Console.WriteLine($"Version No: {history.VersionNo}");
                Console.WriteLine($"Edited Time: {history.EditedTime}");
                Console.WriteLine($"Editor ID: {history.EditedBy}");
                Console.WriteLine($"Summary: {history.ChangeSummary}");
                Console.WriteLine("-------------------------");
            }
        }

        private void HandleOpenHistory(Packet packet)
        {
            Console.WriteLine();

            Console.WriteLine("===== VERSION CONTENT =====");

            Console.WriteLine(packet.Data);
        }

        private void HandleTaskList(Packet packet)
        {
            Console.WriteLine();
            Console.WriteLine("===== TASK LIST =====");

            Console.WriteLine(packet.Data);
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

                case nameof(PacketType.LIST_HISTORY_SUCCESS):
                    HandleHistory(packet);
                    break;

                case nameof(PacketType.LIST_HISTORY_FAILED):
                    Console.WriteLine();
                    Console.WriteLine(packet.Data);
                    break;

                case nameof(PacketType.OPEN_FILE):
                    SyncFileContentRequest openResponse = JsonHelper.Deserialize<SyncFileContentRequest>(packet.Data);
                    CurrentFileID = openResponse.FileID;
                    CurrentFileContent = openResponse.Content;
                    Console.WriteLine();
                    Console.WriteLine("=====FILE CONTENT=====");
                    Console.WriteLine(openResponse.Content);
                    break;

                case nameof(PacketType.SYNC_FILE_CONTENT):
                    HandleSyncFile(packet);
                    break;

                case nameof(PacketType.COMPILE_SUCCESS):
                    Console.WriteLine();
                    Console.WriteLine("===== COMPILE SUCCESS =====");
                    Console.WriteLine(packet.Data);
                    break;

                case nameof(PacketType.COMPILE_FAILED):
                    Console.WriteLine();
                    Console.WriteLine("===== COMPILE FAILED =====");
                    Console.WriteLine(packet.Data);
                    break;

                case nameof(PacketType.OPEN_HISTORY_SUCCESS):
                    Console.WriteLine();
                    Console.WriteLine("===== HISTORY CONTENT =====");
                    Console.WriteLine(packet.Data);
                    break;

                case nameof(PacketType.OPEN_HISTORY_FAILED):
                    Console.WriteLine(packet.Data);
                    break;
                case nameof(PacketType.CREATE_TASK_SUCCESS):
                    Console.WriteLine("[TASK CREATED]");
                    break;

                case nameof(PacketType.CREATE_TASK_FAILED):
                    Console.WriteLine("[CREATE TASK FAILED]");
                    break;

                case nameof(PacketType.LIST_TASKS_SUCCESS):
                    HandleTaskList(packet);
                    break;

                case nameof(PacketType.LIST_TASKS_FAILED):
                    Console.WriteLine(packet.Data);
                    break;

                case nameof(PacketType.UPDATE_TASK_STATUS_SUCCESS):
                    Console.WriteLine("[TASK UPDATED]");
                    break;

                case nameof(PacketType.UPDATE_TASK_STATUS_FAILED):
                    Console.WriteLine("[UPDATE FAILED]");
                    break;

                default: 
                    Console.WriteLine(packet.Data);
                    break;
            }

            PacketReceived?.Invoke(packet);
        }
    }
}
