using CartEntity = GeekShopping.Cart.Api.Models.Entities.Cart;

namespace GeekShopping.Cart.Api.BaseRepository
{
    public interface ICartRepository : IBaseRepository<CartEntity>
    {
        Task<CartEntity> GetByUserIdAsync(Guid userId);
    }
}
