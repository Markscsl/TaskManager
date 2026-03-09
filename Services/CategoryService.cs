using TaskManager.Interfaces.Repositories;
using TaskManager.Interfaces.Services;
using TaskManager.Models.Entities;

namespace TaskManager.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<Category> CreateCategoryAsync(Category category, int userId)
        {
            category.UserId = userId;
            await _categoryRepository.AddAsync(category);
            return category;
        }

        public async Task<Category> GetCategoryByIdAsync(int userId,  int categoryId)
        {
            var category = await _categoryRepository.GetByIdAsync(categoryId);

            if (category == null)
            {
                return null;
            }

            if (category.UserId != userId)
            {
                throw new Exception("ACESSO NEGADO!");
            }

            return category;
        }

        public async Task<IEnumerable<Category>> GetUserCategoriesAsync(int userId)
        {
            return await _categoryRepository.GetCategoriesByUserIdAsync(userId);
        }

        public async Task<Category> UpdateCategoryNameAsync(int userId, int categoryId, string name)
        {
            var cat = await GetCategoryByIdAsync(userId, categoryId);

            if (cat == null)
            {
                return null;
            }

            cat.Name = name;
            await _categoryRepository.UpdateAsync(cat);

            return cat;
        }

        public async Task<Category> UpdateCategoryColorAsync(int userId, int categoryId, string color)
        {
            var cat = await GetCategoryByIdAsync(userId, categoryId);

            if (cat == null)
            {
                return null;
            }

            cat.ColorHex = color;
            await _categoryRepository.UpdateAsync(cat);

            return cat;
        }

        public async Task<bool> DeleteCategoryAsync(int userId, int categoryId)
        {
            var cat = await GetCategoryByIdAsync(userId, categoryId);

            if (cat == null)
            {
                return false;
            }

            await _categoryRepository.DeleteAsync(cat.Id);

            return true;
        }

        public async Task<Category> UpdateCategoryAsync(int userId, int categoryId, Category category)
        {
            var cat = await GetCategoryByIdAsync(userId, categoryId);

            if (cat == null)
            {
                return null;
            }

            cat.Name = category.Name;
            cat.ColorHex = category.ColorHex; 
            cat.UpdatedAt = DateTime.Now;

            await _categoryRepository.UpdateAsync(cat);

            return cat;
        }
    }
}
