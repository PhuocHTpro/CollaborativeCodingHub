using CollaborativeCodingServer.Core;
using CollaborativeCodingServer.Models.Packets.Auth;
using CollaborativeCodingServer.Network;
using CollaborativeCodingServer.Services;

namespace CollaborativeCodingServer.Core.Handlers
{
    public class AuthHandler
    {
        private readonly ClientHandler clientHandler;
        private readonly AuthService authService = new AuthService();

        public AuthHandler(ClientHandler clientHandler)
        {
            this.clientHandler = clientHandler;
        }

        public void HandleLogin(Packet packet)
        {
            LoginRequest request = JsonHelper.Deserialize<LoginRequest>(packet.Data);
            bool success = authService.Login(request.Username, request.Password);
            if (success)
            {
                clientHandler.Username = request.Username;
                clientHandler.SendPacket(PacketType.LOGIN_SUCCESS);
            }
            else
            {
                clientHandler.SendPacket(PacketType.LOGIN_FAILED);
            }
        }

        public void HandleRegister(Packet packet)
        {
            RegisterRequest request = JsonHelper.Deserialize<RegisterRequest>(packet.Data);
            bool success = authService.Register(request.Username, request.Password);
            if (success)
            {
                clientHandler.SendPacket(PacketType.REGISTER_SUCCESS);
            }
            else
            {
                clientHandler.SendPacket(PacketType.REGISTER_FAILED);
            }
        }
    }
}
