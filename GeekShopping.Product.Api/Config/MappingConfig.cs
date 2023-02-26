using AutoMapper;
using GeekShopping.Product.Api.Data.ValueObjects;
using GrpcProductModel = GeekShopping.Shared.Protos.Product;
using ProductEntity = GeekShopping.Product.Api.Models.Entities.Product;

namespace GeekShopping.Product.Api.Config
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMappings()
        {
            return new MapperConfiguration(config =>
            {
                config.CreateMap<ProductEntity, ProductVO>()
                    .ReverseMap();
                config.CreateMap<ProductEntity, GrpcProductModel>();
            });
        }
    }
}
