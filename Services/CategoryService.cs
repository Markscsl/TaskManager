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
    }
}
