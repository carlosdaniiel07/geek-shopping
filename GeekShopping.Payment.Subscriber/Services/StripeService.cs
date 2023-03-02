using AutoMapper;
using GeekShopping.Payment.Subscriber.Models;
using GeekShopping.Shared.Models;
using Stripe;
using Stripe.Checkout;

namespace GeekShopping.Payment.Subscriber.Services
{
    public class StripeService : IPaymentGatewayService
    {
        private readonly IMapper _mapper;
        private readonly PaymentGatewayConfig _stripeConfig;

        public StripeService(IMapper mapper, IConfiguration configuration)
        {
            _mapper = mapper;
            _stripeConfig = configuration.GetSection("PaymentGateway").Get<PaymentGatewayConfig>();
            InitStripeConfig();
        }

        private void InitStripeConfig()
        {
            StripeConfiguration.ApiKey = _stripeConfig.ApiKey;
            StripeConfiguration.AppInfo = new AppInfo
            {
                Name = "GeekShopping.Payment.Subscriber",
            };
        }

        public async Task<CreateOrderResponse> CreateOrderAsync(OrderCreatedEvent orderCreatedEvent)
        {
            var sessionCreateOptions = new SessionCreateOptions
            {
                LineItems = _mapper.Map<IEnumerable<SessionLineItemOptions>>(orderCreatedEvent.Items).ToList(),
                Mode = "payment",
                SuccessUrl = "https://github.com/carlosdaniiel07/geek-shopping",
                CancelUrl = "https://github.com/carlosdaniiel07/geek-shopping",
            };
            var sessionService = new SessionService();
            var session = await sessionService.CreateAsync(sessionCreateOptions);

            return new CreateOrderResponse(session.Id, session.Url, "Stripe");
        }
    }
}
