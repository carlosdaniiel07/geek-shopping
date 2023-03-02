namespace GeekShopping.Payment.Subscriber.Models
{
    public class CreateOrderResponse
    {
        public string CheckoutId { get; private set; }
        public string CheckoutUrl { get; private set; }
        public string Provider { get; private set; }

        public CreateOrderResponse(string checkoutId, string checkoutUrl, string provider)
        {
            CheckoutId = checkoutId;
            CheckoutUrl = checkoutUrl;
            Provider = provider;
        }
    }
}
