using AutoMapper;
using GeekShopping.Order.Api.Models.Dto;
using GeekShopping.Order.Api.Models.Enums;
using GeekShopping.Shared.Models;
using OrderEntity = GeekShopping.Order.Api.Models.Entities.Order;
using OrderItemEntity = GeekShopping.Order.Api.Models.Entities.OrderItem;

namespace GeekShopping.Order.Api.Config
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMappings()
        {
            return new MapperConfiguration(config =>
            {
                config.CreateMap<AddOrderDto, OrderEntity>()
                    .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.Items.Sum(orderItem => orderItem.Price * orderItem.Quantity)))
                    .ForMember(dest => dest.Status, opt => opt.MapFrom(_ => OrderStatus.WaitingPayment));
                config.CreateMap<AddOrderDto.OrderItem, OrderItemEntity>()
                    .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.Price * src.Quantity));
                config.CreateMap<OrderEntity, OrderCreatedEvent>();
                config.CreateMap<OrderItemEntity, OrderCreatedEvent.OrderItem>();
            });
        }
    }
}
