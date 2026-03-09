using TaskManager.Models.Entities;

namespace TaskManager.Interfaces.Services
{
    public interface ICategoryService
    {
        Task<Category> CreateCategoryAsync(Category category, int userId);
        Task<Category> GetCategoryByIdAsync(int userId, int categoryId);
        Task<IEnumerable<Category>> GetUserCategoriesAsync(int userId);
        Task<bool> DeleteCategoryAsync(int userId, int categoryId);
        Task<Category> UpdateCategoryNameAsync(int userId, int catgoryId, string name);
        Task<Category> UpdateCategoryColorAsync(int userId, int categoryId, string color);
        Task<Category> UpdateCategoryAsync(int userId, int categoryId, Category category);
    }
}
