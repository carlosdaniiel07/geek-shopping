using GeekShopping.Order.Api.Models.Dto;
using GeekShopping.Order.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.Order.Api.Controllers
{
    [Route("api/v1/orders")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<ActionResult<AddOrderDto>> Post([FromBody] AddOrderDto addOrderDto)
        {
            var response = await _orderService.CreateAsync(addOrderDto);
            return response.Success ? Ok(response) : BadRequest(response);
        }
    }
}
