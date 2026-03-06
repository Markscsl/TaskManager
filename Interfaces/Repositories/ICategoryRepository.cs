using TaskManager.Models.Entities;

namespace TaskManager.Interfaces.Repositories
{
    public interface ICategoryRepository : IBaseRepository<Category>
    {
        Task<IEnumerable<Category>> GetCategoriesByUserIdAsync(int userId);
    }
}
