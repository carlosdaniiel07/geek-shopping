using GeekShopping.Payment.Api.Models.Dto;
using GeekShopping.Shared.Exchanges;
using GeekShopping.Shared.Interfaces;
using GeekShopping.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace GeekShopping.Payment.Api.Controllers
{
    [Route("api/v1/payments")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IMessageBus _messageBus;

        public PaymentController(IMessageBus messageBus)
        {
            _messageBus = messageBus;
        }

        [HttpPost("notification")]
        public async Task<IActionResult> Notification()
        {
            var requestBody = await new StreamReader(Request.Body).ReadToEndAsync();
            var checkoutCompletedDto = JsonSerializer.Deserialize<CheckoutCompletedDto>(requestBody, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive= true,
            });

            if (checkoutCompletedDto.IsCompleted && checkoutCompletedDto.IsPaid)
            {
                var checkoutChangedEvent = new CheckoutChangedEvent(checkoutCompletedDto.Id, checkoutCompletedDto.PaymentIntent);
                await _messageBus.PublishAsync(ExchangesConfig.PaymentChange, checkoutChangedEvent);
            }

            return Ok();
        }
    }
}
