using Microsoft.EntityFrameworkCore;
using TaskManager.Data.Context;
using TaskManager.Interfaces.Repositories;
using TaskManager.Models.Entities;

namespace TaskManager.Repositories
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext appDbContext) : base(appDbContext) { }

        public async Task<IEnumerable<Category>> GetCategoriesByUserIdAsync(int userId)
        {
            return await _appDbContext.Categories
                .Where(c => c.UserId == userId)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
