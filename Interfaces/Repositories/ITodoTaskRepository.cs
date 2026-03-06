using TaskManager.Models.Entities;

namespace TaskManager.Interfaces.Repositories
{
    public interface ITodoTaskRepository : IBaseRepository<TodoTask>
    {
        Task<IEnumerable<TodoTask>> GetTasksByUserIdAsync(int userId);
    }
}
