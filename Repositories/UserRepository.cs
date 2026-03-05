using Microsoft.EntityFrameworkCore;
using TaskManager.Data.Context;
using TaskManager.Interfaces.Repositories;
using TaskManager.Models.Entities;

namespace TaskManager.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext appDbContext) : base(appDbContext) { }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _appDbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User?> GetByIdWithCategoriesAsync(int id)
        {
            return await _appDbContext.Users
                .Include(u => u.Categories)
                .FirstOrDefaultAsync(u => u.Id == id);
        }
    }
}
