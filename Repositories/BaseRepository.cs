using Microsoft.EntityFrameworkCore;
using TaskManager.Data.Context;
using TaskManager.Interfaces.Repositories;
using TaskManager.Models.Entities;

namespace TaskManager.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly AppDbContext _appDbContext;
        protected readonly DbSet<T> _dbSet;

        public BaseRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            _dbSet = _appDbContext.Set<T>();
        }

        public async Task AddAsync(T obj)
        {
            await _dbSet.AddAsync(obj);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var ent = await _dbSet.FindAsync(id);

            if (ent == null)
            {
                return;
            }

            _dbSet.Remove(ent);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(T obj)
        {
            _dbSet.Update(obj);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task UpdateRangeAsync(IEnumerable<T> entities)
        {
            _dbSet.UpdateRange(entities);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
