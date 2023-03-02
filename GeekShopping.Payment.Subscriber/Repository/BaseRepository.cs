using GeekShopping.Payment.Subscriber.Models.Context;
using GeekShopping.Payment.Subscriber.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace GeekShopping.Payment.Subscriber.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly MySqlContext _context;
        protected readonly DbSet<T> _dbSet;

        public BaseRepository(MySqlContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet
                .AsNoTracking()
                .OrderBy(x => x.CreatedAt)
                .ToListAsync();
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _dbSet
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task SaveAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
