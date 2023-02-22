using GeekShopping.Coupon.Api.Models.Enums;

namespace GeekShopping.Coupon.Api.Models.Dto
{
    public class GetCouponDto
    {
        public string Code { get; set; }
        public CouponType Type { get; set; }
        public decimal Value { get; set; }
        public DateTime? ExpiresAt { get; set; }
    }
}
