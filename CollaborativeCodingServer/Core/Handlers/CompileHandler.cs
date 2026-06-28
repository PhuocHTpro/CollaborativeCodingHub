using CollaborativeCodingServer.Models.Packets.Project;
using CollaborativeCodingServer.Network;
using CollaborativeCodingServer.Services;

namespace CollaborativeCodingServer.Core.Handlers
{
    public class CompileHandler
    {
        private readonly ClientHandler clientHandler;
        private readonly CompileService compileService = new CompileService();

        public CompileHandler(ClientHandler clientHandler)
        {
            this.clientHandler = clientHandler;
        }

        public void HandleCompile(Packet packet)
        {
            CompileRequest request = JsonHelper.Deserialize<CompileRequest>(packet.Data);
            if (request == null || string.IsNullOrWhiteSpace(request.Content))
            {
                clientHandler.SendPacket(PacketType.COMPILE_FAILED, "No content provided for compilation.");
                return;
            }

            if (clientHandler.CurrentUser == null)
            {
                clientHandler.SendPacket(PacketType.ACCESS_DENIED, "Login required for compilation.");
                return;
            }

            CompileResult result = compileService.CompileCode(request.Content, request.FileID);
            if (result.Success)
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
