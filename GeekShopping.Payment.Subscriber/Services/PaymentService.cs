using GeekShopping.Payment.Subscriber.Models.Enums;
using GeekShopping.Payment.Subscriber.Repository;
using GeekShopping.Shared.Models;
using PaymentEntity = GeekShopping.Payment.Subscriber.Models.Entities.Payment;

namespace GeekShopping.Payment.Subscriber.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly ILogger<PaymentService> _logger;
        private readonly IPaymentGatewayService _paymentGatewayService;
        private readonly IPaymentRepository _paymentRepository;

        public PaymentService(ILogger<PaymentService> logger, IPaymentGatewayService paymentGatewayService, IPaymentRepository paymentRepository)
        {
            _logger = logger;
            _paymentGatewayService = paymentGatewayService;
            _paymentRepository = paymentRepository;
        }

        public async Task ProcessAsync(OrderCreatedEvent orderCreatedEvent)
        {
            _logger.LogInformation("[PaymentService] start processing order ID {orderId}", orderCreatedEvent.Id);

            var createOrderResponse = await _paymentGatewayService.CreateOrderAsync(orderCreatedEvent);
            var payment = new PaymentEntity
            {
                OrderId = orderCreatedEvent.Id,
                TotalValue = orderCreatedEvent.Total,
                Fee = 0,
                CheckoutUrl = createOrderResponse.CheckoutUrl,
                CheckoutExternalId = createOrderResponse.CheckoutId,
                PaymentExternalId = null,
                Status = PaymentStatus.Pending,
                ExternalProvider = createOrderResponse.Provider,
                CreatedAt = DateTime.UtcNow,
            };

            await _paymentRepository.SaveAsync(payment);

            _logger.LogInformation("[PaymentService] finished processing order ID {orderId}", orderCreatedEvent.Id);
        }

        public async Task UpdatePaymentAsync(CheckoutChangedEvent checkoutChangedEvent)
        {
            var paymentEntity = await _paymentRepository.GetByCheckoutExternalIdAsync(checkoutChangedEvent.CheckoutExternalId);

            if (paymentEntity == null)
                return;

            var order = await _paymentGatewayService.GetOrderByIdAsync(checkoutChangedEvent.CheckoutExternalId);

            if (!order.IsPaid)
                return;

            var payment = await _paymentGatewayService.GetPaymentByIdAsync(checkoutChangedEvent.PaymentExternalId);
            var isCompleted = payment.IsPaid && payment.AmountReceived == paymentEntity.TotalValue;

            if (!isCompleted)
                return;

            paymentEntity.PaymentExternalId = checkoutChangedEvent.PaymentExternalId;
            paymentEntity.Fee = payment.Fee;
            paymentEntity.Status = PaymentStatus.Completed;

            await _paymentRepository.UpdateAsync(paymentEntity);
        }
    }
}
