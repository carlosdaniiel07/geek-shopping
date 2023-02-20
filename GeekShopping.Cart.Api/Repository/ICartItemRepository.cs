using GeekShopping.Cart.Api.Models.Entities;

namespace GeekShopping.Cart.Api.BaseRepository
{
    public interface ICartItemRepository : IBaseRepository<CartItem>
    {
        Task DeleteByCartIdAsync(Guid cartId);
    }
}
