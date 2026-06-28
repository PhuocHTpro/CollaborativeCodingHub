using CollaborativeCodingServer.Database;
using CollaborativeCodingServer.Models.Entities;
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

                string sql = @"INSERT INTO Rooms (RoomID, RoomName, OwnerID) VALUES (@RoomID, @RoomName, @OwnerID)";
                using SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@RoomID", room.RoomId);
                cmd.Parameters.AddWithValue("@RoomName", room.RoomName);
                cmd.Parameters.AddWithValue("@OwnerID", room.OwnerID);

                return cmd.ExecuteNonQuery() > 0;
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
    }
}
