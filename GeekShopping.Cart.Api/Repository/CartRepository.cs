using GeekShopping.Cart.Api.Models.Context;
using Microsoft.EntityFrameworkCore;
using CartEntity = GeekShopping.Cart.Api.Models.Entities.Cart;

namespace GeekShopping.Cart.Api.BaseRepository
{
    public class CartRepository : BaseRepository<CartEntity>, ICartRepository
    {
        public CartRepository(MySqlContext context) : base(context)
        {

        }

        public async Task<CartEntity> GetByUserIdAsync(Guid userId)
        {
            return await _dbSet
                .Include(cart => cart.Items)
                .FirstOrDefaultAsync(cart => cart.UserId == userId);
        }
    }
}
