using TaskManager.Models.Entities;

namespace TaskManager.Interfaces.Repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User?> GetByEmailAsync(string email);
        Task<User?> GetByIdWithCategoriesAsync(int id);
    }
}
