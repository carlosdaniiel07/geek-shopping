using OrderEntity = GeekShopping.Order.Api.Models.Entities.Order;

namespace GeekShopping.Order.Api.Repository
{
    public interface IOrderRepository
    {
        Task SaveAsync(OrderEntity order);
    }
}
