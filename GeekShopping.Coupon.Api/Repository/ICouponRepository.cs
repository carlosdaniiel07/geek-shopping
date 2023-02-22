using CouponEntity = GeekShopping.Coupon.Api.Models.Entities.Coupon;

namespace GeekShopping.Coupon.Api.Repository
{
    public interface ICouponRepository
    {
        Task<CouponEntity> GetByCodeAsync(string code);
    }
}
