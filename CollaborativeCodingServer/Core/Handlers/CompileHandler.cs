using CollaborativeCodingServer.Models.Packets.Project;
using CollaborativeCodingServer.Network;
using CollaborativeCodingServer.Services;

namespace CollaborativeCodingServer.Core.Handlers
{
    public class CompileHandler
    {
        private readonly ClientHandler clientHandler; // khai báo biến clientHandler để xử lý các yêu cầu từ client
        private readonly CompileService compileService = new CompileService(); // khai báo biến compileService để xử lý các yêu cầu biên dịch mã nguồn

        public CompileHandler(ClientHandler clientHandler) // khai báo biến clientHandler để xử lý các yêu cầu từ client
        {
            this.clientHandler = clientHandler;
        }

        public void HandleCompile(Packet packet) // xử lý yêu cầu biên dịch mã nguồn từ client
        {
            CompileRequest request = JsonHelper.Deserialize<CompileRequest>(packet.Data); // giải mã dữ liệu từ packet thành đối tượng CompileRequest
            if (request == null || string.IsNullOrWhiteSpace(request.Content)) // kiểm tra xem dữ liệu có hợp lệ hay không
            {
                clientHandler.SendPacket(PacketType.COMPILE_FAILED, "No content provided for compilation.");
                return;
            }

            if (clientHandler.CurrentUser == null) // kiểm tra xem người dùng đã đăng nhập hay chưa
            {
                clientHandler.SendPacket(PacketType.ACCESS_DENIED, "Login required for compilation.");
                return;
            }

            CompileResult result = compileService.CompileCode(request.Content, request.FileID); // gọi phương thức CompileCode của compileService để biên dịch mã nguồn
            if (result.Success) // kiểm tra xem việc biên dịch có thành công hay không
            {
                clientHandler.SendPacket(PacketType.COMPILE_SUCCESS, result.Output);
            }
            else
            {
                clientHandler.SendPacket(PacketType.COMPILE_FAILED, result.Output);
            }
        }
    }
}
