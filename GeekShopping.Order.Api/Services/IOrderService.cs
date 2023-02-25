using GeekShopping.Order.Api.Models.Dto;

namespace GeekShopping.Order.Api.Services
{
    public interface IOrderService
    {
        Task<AddOrderResponseDto> CreateAsync(AddOrderDto addOrderDto);
    }
}
