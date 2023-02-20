using GeekShopping.Cart.Api.BaseRepository;
using GeekShopping.Cart.Api.Models.Context;
using GeekShopping.Cart.Api.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace GeekShopping.Cart.Api.Repository
{
    public class CartItemRepository : BaseRepository<CartItem>, ICartItemRepository
    {
        public CartItemRepository(MySqlContext context) : base(context)
        {

        }

        public async Task DeleteByCartIdAsync(Guid cartId)
        {
            var cartItems = await _dbSet.Where(cartItem => cartItem.CartId == cartId).ToListAsync();
            
            _dbSet.RemoveRange(cartItems);
            await _context.SaveChangesAsync();
        }
    }
}
