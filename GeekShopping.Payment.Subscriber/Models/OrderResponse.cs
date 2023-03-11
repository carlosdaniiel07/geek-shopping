namespace GeekShopping.Payment.Subscriber.Models
{
    public class OrderResponse
    {
        public string PaymentExternalId { get; set; }
        public string PaymentStatus { get; set; }

        public bool IsPaid => PaymentStatus == "paid";
    }
}
