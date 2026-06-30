using CollaborativeCodingServer.Models.Entities;
using CollaborativeCodingServer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollaborativeCodingServer.Services
{
    public class TaskService
    {
        private readonly TaskRepository repository = new();
        public bool CreateTask(TaskItem task)
        {
            return repository.CreateTask(task);
        }

        public List<TaskItem> GetTasks(int projectID)
        {
            return repository.GetTasksByProject(projectID);
        }

        public bool UpdateTaskStatus(int taskID, string status)
        {
            return repository.UpdateTaskStatus(taskID, status);
        }

        public bool DeleteTask(int taskID)
        {
            return repository.DeleteTask(taskID);
        }
    }
}
