using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CollaborativeCodingServer.Models;
using Microsoft.Data.SqlClient;

namespace CollaborativeCodingServer.Database
{
    public class ProjectRepository
    {
        public bool CreateProject(ProjectInfo project)
        {
            using SqlConnection conn = DbConnectionFactory.GetConnection();

            conn.Open();

            string sql = @"INSERT INTO Projects (ProjectName, RoomID) VALUES (@ProjectName, @RoomID)";

            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@ProjectName", project.ProjectName);

            cmd.Parameters.AddWithValue("@RoomID", project.RoomID);

            return cmd.ExecuteNonQuery() > 0;
        }

        public List<ProjectInfo> GetProjects()
        {
            List<ProjectInfo> projects = new List<ProjectInfo>();

            using SqlConnection conn = DbConnectionFactory.GetConnection();

            conn.Open();

            string sql = @"SELECT ProjectID, ProjectName, RoomID FROM Projects";

            SqlCommand cmd = new SqlCommand(sql, conn);

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
    }
}
