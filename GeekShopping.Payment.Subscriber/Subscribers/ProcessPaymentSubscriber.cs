using GeekShopping.Payment.Subscriber.Services;
using GeekShopping.Shared.Interfaces;
using GeekShopping.Shared.Models;

namespace GeekShopping.Payment.Subscriber.Subscribers
{
    public class ProcessPaymentSubscriber : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IMessageBus _messageBus;
        private readonly ILogger<ProcessPaymentSubscriber> _logger;

        public ProcessPaymentSubscriber(IServiceProvider serviceProvider, IMessageBus messageBus, ILogger<ProcessPaymentSubscriber> logger)
        {
            _serviceProvider = serviceProvider;
            _messageBus = messageBus;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _messageBus.Subscribe<OrderCreatedEvent>("process_payment", ProcessAsync);
            _logger.LogInformation("[ProcessPaymentSubscriber] listening queue");

            await Task.CompletedTask;
        }

        private async Task<bool> ProcessAsync(OrderCreatedEvent orderCreatedEvent)
        {
            try
            {
                using var scope = _serviceProvider.CreateScope();

                var paymentService = scope.ServiceProvider.GetRequiredService<IPaymentService>();

                await paymentService.ProcessAsync(orderCreatedEvent);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}