using CollaborativeCodingServer.Core;
using CollaborativeCodingServer.Models.Entities;
using CollaborativeCodingServer.Models.Packets.Auth;
using CollaborativeCodingServer.Network;
using CollaborativeCodingServer.Services;

namespace CollaborativeCodingServer.Core.Handlers
{
    public class AuthHandler
    {
        private readonly ClientHandler clientHandler; // khai báo biến clientHandler để xử lý các yêu cầu từ client
        private readonly AuthService authService = new AuthService(); // khai báo biến authService để xử lý các yêu cầu xác thực người dùng

        public AuthHandler(ClientHandler clientHandler)
        {
            this.clientHandler = clientHandler; // khởi tạo biến clientHandler
        }

        public void HandleLogin(Packet packet)
        {
            LoginRequest request = JsonHelper.Deserialize<LoginRequest>(packet.Data); // gán request bằng cách giải mã dữ liệu từ packet
            User user = authService.Login(request.Username, request.Password); // gán user bằng cách gọi phương thức Login của authService với username và password từ request
            if (user != null)
            {
                clientHandler.CurrentUser = user; 
                clientHandler.Username = request.Username;
                authService.SetOnlineStatus(user.UserID, true); // đặt trạng thái online của người dùng thành true
                clientHandler.SendPacket(PacketType.LOGIN_SUCCESS); // gửi packet LOGIN_SUCCESS đến client
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
