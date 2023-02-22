using CouponEntity = GeekShopping.Coupon.Api.Models.Entities.Coupon;

namespace GeekShopping.Coupon.Api.Services
{
    public interface ICouponService
    {
        Task<CouponEntity> GetByCodeAsync(string code);
        Task<bool> CheckAsync(string code);
    }
}
