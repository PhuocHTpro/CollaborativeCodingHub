using CollaborativeCodingServer.Database;
using CollaborativeCodingServer.Models.Entities;
using CollaborativeCodingServer.Models.Packets.Room;
using Microsoft.Data.SqlClient;

namespace CollaborativeCodingServer.Repositories
{
    public class RoomRepository
    {
        public bool CreateRoom(Room room)
        {
            try
            {
                using SqlConnection conn = DbConnectionFactory.GetConnection();
                conn.Open();
                using SqlTransaction transaction = conn.BeginTransaction();

                string sql = @"INSERT INTO Rooms (RoomID, RoomName, OwnerID) VALUES (@RoomID, @RoomName, @OwnerID)";
                using SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Transaction = transaction;
                cmd.Parameters.AddWithValue("@RoomID", room.RoomId);
                cmd.Parameters.AddWithValue("@RoomName", room.RoomName);
                cmd.Parameters.AddWithValue("@OwnerID", room.OwnerID);

                bool created = cmd.ExecuteNonQuery() > 0;
                if (created)
                {
                    string memberSql = @"INSERT INTO RoomMembers (RoomID, UserID, Role) VALUES (@RoomID, @UserID, @Role)";
                    using SqlCommand memberCmd = new SqlCommand(memberSql, conn);
                    memberCmd.Transaction = transaction;
                    memberCmd.Parameters.AddWithValue("@RoomID", room.RoomId);
                    memberCmd.Parameters.AddWithValue("@UserID", room.OwnerID);
                    memberCmd.Parameters.AddWithValue("@Role", "Owner");
                    memberCmd.ExecuteNonQuery();
                }

                transaction.Commit();
                return created;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ROOM CREATE ERROR] {ex.Message}");
                return false;
            }
        }

        public Room? GetRoomById(string roomId)
        {
            using SqlConnection conn = DbConnectionFactory.GetConnection();
            conn.Open();

            string sql = @"SELECT RoomID, RoomName, OwnerID FROM Rooms WHERE RoomID = @RoomID";
            using SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@RoomID", roomId);

            using SqlDataReader reader = cmd.ExecuteReader();
            if (!reader.Read())
                return null;

            return new Room
            {
                RoomId = reader["RoomID"].ToString(),
                RoomName = reader["RoomName"].ToString(),
                OwnerID = Convert.ToInt32(reader["OwnerID"])
            };
        }

        public bool RoomExists(string roomId)
        {
            using SqlConnection conn = DbConnectionFactory.GetConnection();
            conn.Open();

            string sql = @"SELECT COUNT(1) FROM Rooms WHERE RoomID = @RoomID";
            using SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@RoomID", roomId);

            return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
        }

        public bool AddRoomMember(string roomId, int userId, string role = "Member")
        {
            try
            {
                using SqlConnection conn = DbConnectionFactory.GetConnection();
                conn.Open();

                string sql = @"
IF NOT EXISTS (SELECT 1 FROM RoomMembers WHERE RoomID = @RoomID AND UserID = @UserID)
BEGIN
    INSERT INTO RoomMembers (RoomID, UserID, Role) VALUES (@RoomID, @UserID, @Role)
END";
                using SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@RoomID", roomId);
                cmd.Parameters.AddWithValue("@UserID", userId);
                cmd.Parameters.AddWithValue("@Role", role);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ROOM MEMBER ERROR] {ex.Message}");
                return false;
            }
        }

        public List<RoomMemberResponse> GetRoomMembers(string roomId)
        {
            List<RoomMemberResponse> members = new();

            using SqlConnection conn = DbConnectionFactory.GetConnection();
            conn.Open();

            string sql = @"
SELECT u.UserID, u.Username, rm.Role, u.IsOnline
FROM RoomMembers rm
INNER JOIN Users u ON rm.UserID = u.UserID
WHERE rm.RoomID = @RoomID
ORDER BY CASE WHEN rm.Role = 'Owner' THEN 0 ELSE 1 END, u.Username";
            using SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@RoomID", roomId);

            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                members.Add(new RoomMemberResponse
                {
                    UserID = Convert.ToInt32(reader["UserID"]),
                    Username = reader["Username"].ToString() ?? "",
                    Role = reader["Role"].ToString() ?? "Member",
                    IsOnline = Convert.ToBoolean(reader["IsOnline"])
                });
            }

            return members;
        }
    }
}
