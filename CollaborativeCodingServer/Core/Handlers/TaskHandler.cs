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
            if (clientHandler.CurrentUser == null || request.ProjectID <= 0 || string.IsNullOrWhiteSpace(request.TaskName))
            {
                clientHandler.SendPacket(PacketType.CREATE_TASK_FAILED);
                return;
            }

            TaskItem task = new TaskItem
            {
                ProjectID = request.ProjectID,
                TaskName = request.TaskName,
                AssignedTo = request.AssignedTo,
                CreatedBy = clientHandler.CurrentUser.UserID,
                Status = "TODO"
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
            builder.AppendLine("TaskID | Task Name | Status | Assigned User");
            builder.AppendLine("------------------------------------------------------------");

            foreach (TaskItem task in tasks)
            {
                string assignedUser = task.AssignedTo.HasValue
                    ? $"{task.AssignedTo} - {task.AssignedUsername}"
                    : "Unassigned";
                builder.AppendLine($"{task.TaskID} | {task.TaskName} | {task.Status} | {assignedUser}");
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

        public void HandleDeleteTask(Packet packet)
        {
            DeleteTaskRequest request = JsonHelper.Deserialize<DeleteTaskRequest>(packet.Data);
            if (request.TaskID <= 0)
            {
                clientHandler.SendPacket(PacketType.DELETE_TASK_FAILED, "Invalid task ID.");
                return;
            }

            bool success = taskService.DeleteTask(request.TaskID);

            if (success)
            {
                clientHandler.SendPacket(PacketType.DELETE_TASK_SUCCESS);
            }
            else
            {
                clientHandler.SendPacket(PacketType.DELETE_TASK_FAILED, "Task not found.");
            }
        }
    }
}
