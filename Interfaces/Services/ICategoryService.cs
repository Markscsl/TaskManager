using TaskManager.Models.Entities;

namespace TaskManager.Interfaces.Services
{
    public interface ICategoryService
    {
        Task<Category> CreateCategoryAsync(Category category, int userId);
        Task<Category> GetCategoryByIdAsync(int userId, int categoryId);
    }
}
