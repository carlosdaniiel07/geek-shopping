namespace GeekShopping.Shared.Models
{
    public class CheckoutChangedEvent : BaseEvent
    {
        public string CheckoutExternalId { get; }
        public string PaymentExternalId { get; }

        public CheckoutChangedEvent(string checkoutExternalId, string paymentExternalId)
        {
            CheckoutExternalId = checkoutExternalId;
            PaymentExternalId = paymentExternalId;
        }
    }
}
