using GeekShopping.Shared.Models;

namespace GeekShopping.Payment.Subscriber.Services
{
    public interface IPaymentService
    {
        Task ProcessAsync(OrderCreatedEvent orderCreatedEvent);
    }
}
