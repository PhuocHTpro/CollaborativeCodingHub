using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CollaborativeCodingServer.Models;
using Microsoft.Data.SqlClient;

namespace CollaborativeCodingServer.Database
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

        public bool Login(string username, string password)
        {
            using SqlConnection conn = DbConnectionFactory.GetConnection();

            conn.Open();

            string sql = @"SELECT COUNT(*) FROM Users WHERE Username = @Username AND Password = @Password";

            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@Username", username);

            cmd.Parameters.AddWithValue("@Password", password);

            int count = (int)cmd.ExecuteScalar();

            return count > 0;
        }
    }
}
