using GeekShopping.Product.Api.Models.Context;
using Microsoft.EntityFrameworkCore;
using ProductEntity = GeekShopping.Product.Api.Models.Entities.Product;

namespace GeekShopping.Product.Api.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly MySqlContext _context;
        private readonly DbSet<ProductEntity> _dbSet;

        public ProductRepository(MySqlContext context)
        {
            _context = context;
            _dbSet = context.Set<ProductEntity>();
        }

        public async Task<IEnumerable<ProductEntity>> GetAllAsync()
        {
            return await _dbSet
                .AsNoTracking()
                .OrderBy(product => product.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<ProductEntity>> GetAllByIdAsync(IEnumerable<Guid> ids)
        {
            return await _dbSet
                .AsNoTracking()
                .Where(product => ids.Contains(product.Id))
                .ToListAsync();
        }

        public async Task<ProductEntity> GetByIdAsync(Guid id)
        {
            return await _dbSet
                .SingleOrDefaultAsync(product => product.Id == id);
        }

        public async Task SaveAsync(ProductEntity product)
        {
            await _dbSet.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ProductEntity product)
        {
            _dbSet.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(ProductEntity product)
        {
            _dbSet.Remove(product);
            await _context.SaveChangesAsync();
        }
    }
}
