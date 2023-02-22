using GeekShopping.Coupon.Api.Models.Context;
using Microsoft.EntityFrameworkCore;
using CouponEntity = GeekShopping.Coupon.Api.Models.Entities.Coupon;

namespace GeekShopping.Coupon.Api.Repository
{
    public class CouponRepository : ICouponRepository
    {
        private readonly DbSet<CouponEntity> _dbSet;

        public CouponRepository(MySqlContext context)
        {
            _dbSet = context.Set<CouponEntity>();
        }

        public async Task<CouponEntity> GetByCodeAsync(string code)
        {
            return await _dbSet
                .FirstOrDefaultAsync(coupon => coupon.Code == code);
        }
    }
}
