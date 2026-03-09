using TaskManager.DTOs.TodoTask;
using TaskManager.Models.Entities;
using TaskManager.Models.Enums;

namespace TaskManager.Interfaces.Services
{
    public interface ITodoTaskService
    {
        Task<TodoTask> CreateTaskAsync(TodoTask tarefa, int userId);
        Task<IEnumerable<TodoTask>> GetUserTaskAsync(int userId);
        Task<TodoTask> GetTaskByIdAsync(int userId, int todoTaskId);
        Task<TodoTask> UpdateTaskStatusAsync(int userId, int todoTaskId, StatusTask novoStatus);
        Task<TodoTask> UpdateTaskAsync(int userId, int taskId, TodoTask novaTarefa);
        Task<bool> DeleteTaskAsync(int userId, int taskId);
        Task UpdateTaskOrderAsync(int userId, Dictionary<int, int> newPositions);
    }
}
