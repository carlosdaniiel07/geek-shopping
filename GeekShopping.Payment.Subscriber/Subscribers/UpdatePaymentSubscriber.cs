using GeekShopping.Payment.Subscriber.Services;
using GeekShopping.Shared.Interfaces;
using GeekShopping.Shared.Models;

namespace GeekShopping.Payment.Subscriber.Subscribers
{
    public class UpdatePaymentSubscriber : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IMessageBus _messageBus;
        private readonly ILogger<UpdatePaymentSubscriber> _logger;

        public UpdatePaymentSubscriber(IServiceProvider serviceProvider, IMessageBus messageBus, ILogger<UpdatePaymentSubscriber> logger)
        {
            _serviceProvider = serviceProvider;
            _messageBus = messageBus;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("[UpdatePaymentSubscriber] listening queue");
            _messageBus.Subscribe<CheckoutChangedEvent>("update_payment", ProcessAsync);

            await Task.CompletedTask;
        }

        private async Task<bool> ProcessAsync(CheckoutChangedEvent checkoutChangedEvent)
        {
            try
            {
                using var scope = _serviceProvider.CreateScope();

                var paymentService = scope.ServiceProvider.GetRequiredService<IPaymentService>();

                await paymentService.UpdatePaymentAsync(checkoutChangedEvent);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
