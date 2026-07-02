using CollaborativeCodingServer.Database;
using CollaborativeCodingServer.Models.Entities;
using Microsoft.Data.SqlClient;

namespace CollaborativeCodingServer.Repositories
{
    public class UserRepository
    {
        public bool UserExists(string username) // phương thức UserExists để kiểm tra xem người dùng có tồn tại trong cơ sở dữ liệu hay không
        {
            using SqlConnection conn = DbConnectionFactory.GetConnection(); // dùng DbConnectionFactory để lấy kết nối
            conn.Open();
            string sql = @"SELECT COUNT(*) FROM Users WHERE Username = @Username";
            SqlCommand cmd = new SqlCommand(sql, conn); // tạo SqlCommand với câu lệnh SQL và kết nối
            cmd.Parameters.AddWithValue("@Username", username);
            int count = (int)cmd.ExecuteScalar();
            return count > 0;
        }

        public bool Register(User user) // phương thức Register để đăng ký người dùng mới vào cơ sở dữ liệu
        {
            using SqlConnection conn = DbConnectionFactory.GetConnection();
            conn.Open();
            string sql = @"INSERT INTO Users(Username, Password) VALUES(@Username, @Password)";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Username", user.Username);
            cmd.Parameters.AddWithValue("@Password", user.Password);
            return cmd.ExecuteNonQuery() > 0;
        }

        public User Login(string username, string password) // phương thức Login để đăng nhập người dùng bằng cách kiểm tra tên người dùng và mật khẩu trong cơ sở dữ liệu
        {
            using SqlConnection conn = DbConnectionFactory.GetConnection();
            conn.Open();
            string sql = @"SELECT UserID, Username, Password FROM Users WHERE Username = @Username AND Password = @Password";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Username", username);
            cmd.Parameters.AddWithValue("@Password", password);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new User
                {
                    UserID = Convert.ToInt32(reader["UserID"]),
                    Username = reader["Username"].ToString(),
                    Password = reader["Password"].ToString()
                };
            }
            return null;
        }

        public void SetOnlineStatus(int userId, bool isOnline) // phương thức SetOnlineStatus để cập nhật trạng thái online của người dùng trong cơ sở dữ liệu
        {
            using SqlConnection conn = DbConnectionFactory.GetConnection();
            conn.Open();

            string sql = @"UPDATE Users SET IsOnline = @IsOnline WHERE UserID = @UserID";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@IsOnline", isOnline);
            cmd.Parameters.AddWithValue("@UserID", userId);
            cmd.ExecuteNonQuery();
        }
    }
}
