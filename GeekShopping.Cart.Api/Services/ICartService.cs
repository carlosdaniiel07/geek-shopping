using GeekShopping.Cart.Api.Models.Dto;

namespace GeekShopping.Cart.Api.Services
{
    public interface ICartService
    {
        Task<GetCartDto> GetByUserIdAsync(Guid userId);
        Task AddItemAsync(AddCartItemDto addCartItemDto);
        Task RemoveItemAsync(Guid id);
        Task ClearAsync(Guid userId);
        Task ApplyCouponAsync(Guid userId, string coupon);
        Task RemoveCouponAsync(Guid userId);
    }
}
