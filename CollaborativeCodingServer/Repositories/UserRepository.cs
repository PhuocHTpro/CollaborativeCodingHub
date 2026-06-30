using CollaborativeCodingServer.Database;
using CollaborativeCodingServer.Models.Entities;
using Microsoft.Data.SqlClient;

namespace CollaborativeCodingServer.Repositories
{
    public class UserRepository
    {
        public bool UserExists(string username)
        {
            using SqlConnection conn = DbConnectionFactory.GetConnection();

            conn.Open();

            string sql = @"SELECT COUNT(*) FROM Users WHERE Username = @Username";

            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@Username", username);

            int count = (int)cmd.ExecuteScalar();

            return count > 0;
        }

        public bool Register(User user)
        {
            using SqlConnection conn = DbConnectionFactory.GetConnection();

            conn.Open();

            string sql = @"INSERT INTO Users(Username, Password) VALUES(@Username, @Password)";

            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@Username", user.Username);

            cmd.Parameters.AddWithValue("@Password", user.Password);

            return cmd.ExecuteNonQuery() > 0;
        }

        public User Login(string username, string password)
        {
            using SqlConnection conn = DbConnectionFactory.GetConnection();

            conn.Open();

            string sql = @"SELECT UserID, Username, Password
                          FROM Users
                           WHERE Username = @Username AND Password = @Password";

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

        public void SetOnlineStatus(int userId, bool isOnline)
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
