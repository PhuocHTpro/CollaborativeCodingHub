using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollaborativeCodingClient.Models.Packets.Auth
{
    public class LoginRequest // tạo lớp LoginRequest để gửi thông tin đăng nhập từ client đến server
    {
        public string Username { get; set; }

        public string Password { get; set; }
    }
}
