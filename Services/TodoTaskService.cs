using TaskManager.Interfaces.Repositories;
using TaskManager.Interfaces.Services;
using TaskManager.Models.Entities;
using TaskManager.Models.Enums;

namespace TaskManager.Services
{
    public class TodoTaskService : ITodoTaskService
    {
        private readonly ITodoTaskRepository _taskRepository;

        public TodoTaskService(ITodoTaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<TodoTask> CreateTaskAsync(TodoTask tarefa, int userId)
        {
            tarefa.UserId = userId;
            await _taskRepository.AddAsync(tarefa);
            return tarefa;
        }

        public async Task<IEnumerable<TodoTask>> GetUserTaskAsync(int userId)
        {
            return await _taskRepository.GetTasksByUserIdAsync(userId);
        }

        public async Task<TodoTask> GetTaskByIdAsync(int userId,  int taskId)
        {
            var task = await _taskRepository.GetByIdAsync(taskId);

            if(task == null)
            {
                return null;
            }

            if (task.UserId != userId)
            {
                throw new Exception("ACESSO NEGADO!");
            }

            return task;
        }

        public async Task<TodoTask> UpdateTaskStatusAsync(int userId, int taskId, StatusTask novoStatus)
        {

            var task = await GetTaskByIdAsync(userId, taskId);

            if(task == null)
            {
                return null;
            }

            task.Status = novoStatus;
            await _taskRepository.UpdateAsync(task);

            return task;
        }

        public async Task<TodoTask> UpdateTaskAsync(int userId, int taskId, TodoTask novaTarefa)
        {
            var task = await GetTaskByIdAsync(userId, taskId);

            if (task == null)
            {
                return null;
            }

            task.Title = novaTarefa.Title;
            task.Description = novaTarefa.Description;
            task.DueDate = novaTarefa.DueDate;
            task.Status = novaTarefa.Status;
            task.UpdatedAt = DateTime.Now;
            task.Priority = novaTarefa.Priority;

            await _taskRepository.UpdateAsync(task);
            return task;
        }

        public async Task<bool> DeleteTaskAsync(int userId, int taskId)
        {
            var task = await GetTaskByIdAsync(userId, taskId);

            if (task == null)
            {
                return false;
            }

            await _taskRepository.DeleteAsync(task.Id);

            return true;
        }

        public async Task UpdateTaskOrderAsync(int userId, Dictionary<int, int> newPositions)
        {
            var ids = newPositions.Keys.ToList();

            var tarefasDoBanco = await _taskRepository.GetTasksByIdsAsync(userId, ids);

            foreach(var tarefa in tarefasDoBanco)
            {
                tarefa.OrderIndex = newPositions[tarefa.Id];
            }

            await _taskRepository.UpdateRangeAsync(tarefasDoBanco);
        }
    }
}
