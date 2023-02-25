using GeekShopping.Order.Api.Models.Context;
using Microsoft.EntityFrameworkCore;
using OrderEntity = GeekShopping.Order.Api.Models.Entities.Order;

namespace GeekShopping.Order.Api.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly MySqlContext _context;
        private readonly DbSet<OrderEntity> _dbSet;

        public OrderRepository(MySqlContext context)
        {
            _dbSet = context.Set<OrderEntity>();
            _context = context;
        }

        public async Task SaveAsync(OrderEntity order)
        {
            await _dbSet.AddAsync(order);
            await _context.SaveChangesAsync();
        }
    }
}
