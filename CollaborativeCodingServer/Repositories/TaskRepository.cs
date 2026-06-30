using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CollaborativeCodingServer.Core;
using CollaborativeCodingServer.Database;
using CollaborativeCodingServer.Models.Entities;
using CollaborativeCodingServer.Models.Packets.Task;
using CollaborativeCodingServer.Network;
using CollaborativeCodingServer.Services;
using Microsoft.Data.SqlClient;

namespace CollaborativeCodingServer.Repositories
{
    public class TaskRepository
    {
        public bool CreateTask(TaskItem task)
        {
            using SqlConnection conn = DbConnectionFactory.GetConnection();

            conn.Open();

            string sql = @"INSERT INTO Tasks (ProjectID, TaskName, AssignedTo, CreatedBy, Status)
                        VALUES (@ProjectID, @TaskName, @AssignedTo, @CreatedBy, @Status)";

            using SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@ProjectID", task.ProjectID);
            cmd.Parameters.AddWithValue("@TaskName", task.TaskName);
            cmd.Parameters.AddWithValue("@AssignedTo",
                (object?)task.AssignedTo ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@CreatedBy", task.CreatedBy);
            cmd.Parameters.AddWithValue("@Status", task.Status);

            return cmd.ExecuteNonQuery() > 0;
        }

        public List<TaskItem> GetTasksByProject(int projectID)
        {
            List<TaskItem> tasks = new();

            using SqlConnection conn = DbConnectionFactory.GetConnection();

            conn.Open();

            string sql = @"
SELECT t.TaskID, t.ProjectID, t.TaskName, t.AssignedTo, u.Username AS AssignedUsername, t.Status
FROM Tasks t
LEFT JOIN Users u ON t.AssignedTo = u.UserID
WHERE t.ProjectID = @ProjectID
ORDER BY t.TaskID";

            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@ProjectID", projectID);

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                tasks.Add(new TaskItem
                {
                    TaskID = Convert.ToInt32(reader["TaskID"]),
                    ProjectID = Convert.ToInt32(reader["ProjectID"]),
                    TaskName = reader["TaskName"].ToString() ?? "",
                    AssignedTo = reader["AssignedTo"] == DBNull.Value? null: Convert.ToInt32(reader["AssignedTo"]),
                    AssignedUsername = reader["AssignedUsername"] == DBNull.Value ? "" : reader["AssignedUsername"].ToString() ?? "",
                    Status = reader["Status"].ToString() ?? "TODO"
                });
            }

            return tasks;

        }

        public bool UpdateTaskStatus(int taskID, string status)
        {
            using SqlConnection conn = DbConnectionFactory.GetConnection();
            conn.Open();
            string sql = @"UPDATE Tasks
                   SET Status = @Status
                   WHERE TaskID = @TaskID";

            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Status", status);
            cmd.Parameters.AddWithValue("@TaskID", taskID);
            return cmd.ExecuteNonQuery() > 0;
        }

        public bool DeleteTask(int taskID)
        {
            using SqlConnection conn = DbConnectionFactory.GetConnection();
            conn.Open();

            string sql = @"DELETE FROM Tasks WHERE TaskID = @TaskID";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@TaskID", taskID);

            return cmd.ExecuteNonQuery() > 0;
        }
    }
}
