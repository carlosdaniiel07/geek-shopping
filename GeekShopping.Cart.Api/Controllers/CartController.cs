using GeekShopping.Cart.Api.Models.Dto;
using GeekShopping.Cart.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.Cart.Api.Controllers
{
    [Route("api/v1/carts")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        private readonly IUserService _userService;

        public CartController(ICartService cartService, IUserService userService)
        {
            _cartService = cartService;
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<GetCartDto>> Get()
        {
            var cart = await _cartService.GetByUserIdAsync(_userService.GetLoggedUserId());

            if (cart == null)
                return NotFound();

            return Ok(cart);
        }

        [HttpDelete("clear")]
        public async Task<IActionResult> Clear()
        {
            await _cartService.ClearAsync(_userService.GetLoggedUserId());
            return Ok();
        }

        [HttpPost("items")]
        public async Task<IActionResult> AddItem([FromBody] AddCartItemDto addCartItemDto)
        {
            await _cartService.AddItemAsync(addCartItemDto);
            return Ok();
        }

        [HttpDelete("items/{id}")]
        public async Task<IActionResult> DeleteItem(Guid id)
        {
            await _cartService.RemoveItemAsync(id);
            return Ok();
        }

        [HttpPut("coupon")]
        public async Task<IActionResult> ApplyCoupon([FromBody] ApplyCouponDto applyCouponDto)
        {
            await _cartService.ApplyCouponAsync(_userService.GetLoggedUserId(), applyCouponDto.Coupon);
            return Ok();
        }

        [HttpDelete("coupon")]
        public async Task<IActionResult> DeleteCoupon()
        {
            await _cartService.RemoveCouponAsync(_userService.GetLoggedUserId());
            return Ok();
        }
    }
}
