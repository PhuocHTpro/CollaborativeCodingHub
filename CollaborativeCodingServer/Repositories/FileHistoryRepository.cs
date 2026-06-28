using CollaborativeCodingServer.Database;
using CollaborativeCodingServer.Models.Entities;
using Microsoft.Data.SqlClient;

namespace CollaborativeCodingServer.Repositories
{
    public class FileHistoryRepository
    {
        public bool SaveHistory(FileHistory history)
        {
            using SqlConnection conn = DbConnectionFactory.GetConnection();
            conn.Open();

            string sql = @"
                INSERT INTO FileHistory
                (
                    FileID,
                    VersionNo,
                    Content,
                    EditedBy,
                    ChangeSummary
                )
                VALUES
                (
                    @FileID,
                    @VersionNo,
                    @Content,
                    @EditedBy,
                    @ChangeSummary
                )";

            using SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@FileID", history.FileID);
            cmd.Parameters.AddWithValue("@VersionNo", history.VersionNo);
            cmd.Parameters.AddWithValue("@Content", history.Content);
            cmd.Parameters.AddWithValue("@EditedBy", history.EditedBy);
            cmd.Parameters.AddWithValue("@ChangeSummary", history.ChangeSummary ?? "");

            return cmd.ExecuteNonQuery() > 0;
        }

        public int GetLatestVersion(int fileID)
        {
            using SqlConnection conn = DbConnectionFactory.GetConnection();
            conn.Open();

            string sql = @"
                SELECT ISNULL(MAX(VersionNo),0)
                FROM FileHistory
                WHERE FileID = @FileID";

            using SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@FileID", fileID);

            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        public List<FileHistory> GetHistoryByFile(int fileID)
        {
            List<FileHistory> histories = new();

            using SqlConnection conn = DbConnectionFactory.GetConnection();
            conn.Open();

            string sql = @"
                SELECT *
                FROM FileHistory
                WHERE FileID = @FileID
                ORDER BY VersionNo DESC";

            using SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@FileID", fileID);

            using SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                histories.Add(new FileHistory
                {
                    HistoryID = Convert.ToInt32(reader["HistoryID"]),
                    FileID = Convert.ToInt32(reader["FileID"]),
                    VersionNo = Convert.ToInt32(reader["VersionNo"]),
                    Content = reader["Content"].ToString(),
                    EditedBy = Convert.ToInt32(reader["EditedBy"]),
                    EditedTime = Convert.ToDateTime(reader["EditedTime"]),
                    ChangeSummary = reader["ChangeSummary"].ToString()
                });
            }

            return histories;
        }

        public FileHistory? GetHistoryById(int historyID)
        {
            using SqlConnection conn = DbConnectionFactory.GetConnection();
            conn.Open();

            string sql = @"
                SELECT *
                FROM FileHistory
                WHERE HistoryID = @HistoryID";

            using SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@HistoryID", historyID);

            using SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                return new FileHistory
                {
                    HistoryID = Convert.ToInt32(reader["HistoryID"]),
                    FileID = Convert.ToInt32(reader["FileID"]),
                    VersionNo = Convert.ToInt32(reader["VersionNo"]),
                    Content = reader["Content"].ToString(),
                    EditedBy = Convert.ToInt32(reader["EditedBy"]),
                    EditedTime = Convert.ToDateTime(reader["EditedTime"]),
                    ChangeSummary = reader["ChangeSummary"].ToString()
                };
            }

            return null;
        }
    }
}