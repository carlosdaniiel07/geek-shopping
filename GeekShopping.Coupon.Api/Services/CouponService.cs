using GeekShopping.Coupon.Api.Repository;
using CouponEntity = GeekShopping.Coupon.Api.Models.Entities.Coupon;

namespace GeekShopping.Coupon.Api.Services
{
    public class CouponService : ICouponService
    {
        private readonly ICouponRepository _couponRepository;

        public CouponService(ICouponRepository couponRepository)
        {
            _couponRepository = couponRepository;
        }

        public async Task<CouponEntity> GetByCodeAsync(string code)
        {
            return await _couponRepository.GetByCodeAsync(code);
        }

        public async Task<CouponEntity> CheckAsync(string code)
        {
            var coupon = await _couponRepository.GetByCodeAsync(code);

            if (coupon == null)
                return null;

            var isExpired = coupon.ExpiresAt.HasValue && DateTime.UtcNow.Date > coupon.ExpiresAt.Value.Date;

            return isExpired ? null : coupon;
        }
    }
}
