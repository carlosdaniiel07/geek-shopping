using GeekShopping.Payment.Subscriber.Models;
using GeekShopping.Shared.Models;

namespace GeekShopping.Payment.Subscriber.Services
{
    public interface IPaymentGatewayService
    {
        Task<CreateOrderResponse> CreateOrderAsync(OrderCreatedEvent orderCreatedEvent);
    }
}
