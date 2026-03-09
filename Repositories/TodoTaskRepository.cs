using Microsoft.EntityFrameworkCore;
using TaskManager.Data.Context;
using TaskManager.Interfaces.Repositories;
using TaskManager.Models.Entities;

namespace TaskManager.Repositories
{
    public class TodoTaskRepository : BaseRepository<TodoTask>, ITodoTaskRepository
    {
        public TodoTaskRepository(AppDbContext appDbContext) : base(appDbContext) { }

        public async Task<IEnumerable<TodoTask>> GetTasksByUserIdAsync(int userId)
        {
            return await _appDbContext.Tasks
                .Where(t => t.UserId == userId)
                .AsNoTracking()
                .Include(c => c.Category)
                .ToListAsync();
        }

        public async Task<IEnumerable<TodoTask>> GetTasksByIdsAsync(int userId, List<int> ids)
        {
            return await _appDbContext.Tasks
                .Where(t => t.UserId == userId && ids.Contains(t.Id))
                .ToListAsync();
        }
    }
}
