using AutoMapper;
using GeekShopping.Coupon.Api.Models.Dto;
using GeekShopping.Coupon.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.Coupon.Api.Controllers
{
    [Route("api/v1/coupons")]
    [ApiController]
    public class CouponController : ControllerBase
    {
        private readonly ICouponService _couponService;
        private readonly IMapper _mapper;

        public CouponController(ICouponService couponService, IMapper mapper)
        {
            _couponService = couponService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<GetCouponDto>> Get([FromQuery] string code)
        {
            var coupon = await _couponService.GetByCodeAsync(code);

            if (coupon == null)
                return NotFound();

            return Ok(_mapper.Map<GetCouponDto>(coupon));
        }

        [HttpGet("check")]
        public async Task<IActionResult> Check([FromQuery] string code)
        {
            var isValid = await _couponService.CheckAsync(code);
            return isValid ? Ok() : BadRequest();
        }
    }
}
