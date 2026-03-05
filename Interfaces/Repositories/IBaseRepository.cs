using TaskManager.Models.Entities;

namespace TaskManager.Interfaces.Repositories
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task AddAsync(T entity);
        Task<T?> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task DeleteAsync(int id);
        Task UpdateAsync(T entity);
    }
}
