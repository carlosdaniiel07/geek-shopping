using AutoMapper;
using GeekShopping.Shared.Models;
using Stripe.Checkout;

namespace GeekShopping.Payment.Subscriber.Config
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMappings()
        {
            return new MapperConfiguration(config =>
            {
                config.CreateMap<OrderCreatedEvent.OrderItem, SessionLineItemOptions>()
                    .ForMember(dest => dest.Price, opt => opt.Ignore())
                    .ForMember(dest => dest.PriceData, opt => opt.MapFrom(src => new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = Convert.ToInt64(src.Price * 100),
                        Currency = "BRL".ToLower(),
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = src.Name,
                            Description = src.Description,
                            Metadata = new Dictionary<string, string>()
                            {
                                { "ProductId", src.ProductId.ToString() },
                            },
                        },
                    }))
                    .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity));
            });
        }
    }
}
