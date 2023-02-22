using AutoMapper;
using GeekShopping.Coupon.Api.Models.Dto;
using CouponEntity = GeekShopping.Coupon.Api.Models.Entities.Coupon;

namespace GeekShopping.Coupon.Api.Config
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMappings()
        {
            return new MapperConfiguration(config =>
            {
                config.CreateMap<CouponEntity, GetCouponDto>();
            });
        }
    }
}
