using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CollaborativeCodingServer.Models;
using Microsoft.Data.SqlClient;

namespace CollaborativeCodingServer.Database
{
    public class FileRepository
    {
        public bool CreateFile(ProjectFile file)
        {
            using SqlConnection conn = DbConnectionFactory.GetConnection();

            conn.Open();

            string sql = @"INSERT INTO ProjectFiles(ProjectID, FileName, Content) VALUES (@ProjectID, @FileName, @Content)";

            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@ProjectID", file.ProjectID);

            cmd.Parameters.AddWithValue("@FileName", file.FileName);

            cmd.Parameters.AddWithValue("@Content", file.Content);

            return cmd.ExecuteNonQuery() > 0;
        }

        public List<ProjectFile> GetFilesByProject(int projectID)
        {
            List<ProjectFile> files = new List<ProjectFile>();

            using SqlConnection conn = DbConnectionFactory.GetConnection();

            conn.Open();

            string sql = @"SELECT FileID, ProjectID, FileName, Content FROM ProjectFiles WHERE ProjectID = @ProjectID";

            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@ProjectID", projectID);

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                files.Add(new ProjectFile
                {
                    FileID = Convert.ToInt32(reader["FileID"]),

                    ProjectID = Convert.ToInt32(reader["ProjectID"]),

                    FileName = reader["FileName"].ToString(),

                    Content = reader["Content"].ToString()
                });
            }

            return files;
        }

        public ProjectFile GetFileById(int fileID)
        {
            using SqlConnection conn = DbConnectionFactory.GetConnection();
            conn.Open();
            string sql = @"SELECT * FROM ProjectFiles WHERE FileID = @FileID";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@FileID", fileID);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new ProjectFile
                {
                    FileID = Convert.ToInt32(reader["FileID"]),
                    ProjectID = Convert.ToInt32(reader["ProjectID"]),
                    FileName = reader["FileName"].ToString(),
                    Content = reader["Content"].ToString()
                };
            }
            return null;
        }

        public bool UpdateFileContent(int fileID, string content)
        {
            using SqlConnection conn = DbConnectionFactory.GetConnection();
            conn.Open();
            string sql = @"UPDATE ProjectFiles SET Content = @Content WHERE FileID = @FileID";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Content", content);
            cmd.Parameters.AddWithValue("@FileID", fileID);
            return cmd.ExecuteNonQuery() > 0;
        }
    }
}