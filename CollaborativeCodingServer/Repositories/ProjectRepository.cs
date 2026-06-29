using CollaborativeCodingServer.Database;
using CollaborativeCodingServer.Models.Entities;
using Microsoft.Data.SqlClient;

namespace CollaborativeCodingServer.Repositories
{
    public class ProjectRepository
    {
        public int CreateProject(ProjectInfo project)
        {
            try
            {
                using SqlConnection conn = DbConnectionFactory.GetConnection();
                conn.Open();

                string sql = @"INSERT INTO Projects (ProjectName, RoomID, CreatedBy) OUTPUT INSERTED.ProjectID VALUES (@ProjectName, @RoomID, @CreatedBy)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ProjectName", project.ProjectName);
                cmd.Parameters.AddWithValue("@RoomID", project.RoomID);
                cmd.Parameters.AddWithValue("@CreatedBy", project.CreatedBy);

                object result = cmd.ExecuteScalar();
                return result == null ? 0 : Convert.ToInt32(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[PROJECT CREATE ERROR] {ex.Message}");
                return 0;
            }
        }

        public List<ProjectInfo> GetProjects(string roomId)
        {
            List<ProjectInfo> projects = new();
            using SqlConnection conn = DbConnectionFactory.GetConnection();
            conn.Open();
            string sql = @"SELECT ProjectID, ProjectName, RoomID FROM Projects WHERE RoomID = @RoomID";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@RoomID", roomId);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                projects.Add(new ProjectInfo
                {
                    ProjectID = Convert.ToInt32(reader["ProjectID"]),
                    ProjectName = reader["ProjectName"].ToString(),
                    RoomID = reader["RoomID"].ToString()
                });
            }
            return projects;
        }

        public bool IsFileInRoom(int fileId, string roomId)
        {
            using SqlConnection conn = DbConnectionFactory.GetConnection();
            conn.Open();
            string sql = @"SELECT COUNT(*) FROM ProjectFiles pf INNER JOIN Projects p ON pf.ProjectID = p.ProjectID WHERE pf.FileID = @FileID AND p.RoomID = @RoomID";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@FileID", fileId);
            cmd.Parameters.AddWithValue("@RoomID", roomId);
            int count = (int)cmd.ExecuteScalar();
            return count > 0;
        }

        public bool ProjectExists(int projectId)
        {
            using SqlConnection conn = DbConnectionFactory.GetConnection();
            conn.Open();
            string sql = @"SELECT COUNT(*) FROM Projects WHERE ProjectID = @ProjectID";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@ProjectID", projectId);
            int count = (int)cmd.ExecuteScalar();
            return count > 0;
        }
    }
}
