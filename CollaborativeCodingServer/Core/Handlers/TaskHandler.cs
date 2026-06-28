using CollaborativeCodingServer.Models.Entities;
using CollaborativeCodingServer.Models.Packets.Task;
using CollaborativeCodingServer.Network;
using CollaborativeCodingServer.Services;
using System.Text;

namespace CollaborativeCodingServer.Core.Handlers
{
    public class TaskHandler
    {
        private readonly ClientHandler clientHandler;
        private readonly TaskService taskService = new();

        public TaskHandler(ClientHandler clientHandler)
        {
            this.clientHandler = clientHandler;
        }

        public void HandleCreateTask(Packet packet)
        {
            CreateTaskRequest request = JsonHelper.Deserialize<CreateTaskRequest>(packet.Data);

            TaskItem task = new TaskItem
            {
                ProjectID = request.ProjectID,
                TaskName = request.TaskName,
                AssignedTo = request.AssignedTo,
                Status = "Pending"
            };

            bool success = taskService.CreateTask(task);

            if (success)
            {
                clientHandler.SendPacket(PacketType.CREATE_TASK_SUCCESS);
            }
            else
            {
                clientHandler.SendPacket(PacketType.CREATE_TASK_FAILED);
            }
        }

        public void HandleListTasks(Packet packet)
        {
            ListTaskRequest request = JsonHelper.Deserialize<ListTaskRequest>(packet.Data);

            List<TaskItem> tasks = taskService.GetTasks(request.ProjectID);

            if (tasks.Count == 0)
            {
                clientHandler.SendPacket(PacketType.LIST_TASKS_FAILED, "No tasks found.");
                return;
            }

            StringBuilder builder = new StringBuilder();

            foreach (TaskItem task in tasks)
            {
                builder.AppendLine($"{task.TaskID} | {task.TaskName} | {task.Status}");
            }

            clientHandler.SendPacket(PacketType.LIST_TASKS_SUCCESS, builder.ToString());
        }

        public void HandleUpdateTaskStatus(Packet packet)
        {
            UpdateTaskStatusRequest request = JsonHelper.Deserialize<UpdateTaskStatusRequest>(packet.Data);

            bool success = taskService.UpdateTaskStatus(request.TaskID, request.Status);

            if (success)
            {
                clientHandler.SendPacket(PacketType.UPDATE_TASK_STATUS_SUCCESS);
            }
            else
            {
                clientHandler.SendPacket(PacketType.UPDATE_TASK_STATUS_FAILED);
            }
        }
    }
}