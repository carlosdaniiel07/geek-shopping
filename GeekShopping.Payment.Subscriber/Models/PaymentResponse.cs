using System.Security.Permissions;

namespace GeekShopping.Payment.Subscriber.Models
{
    public class PaymentResponse
    {
        public decimal AmountReceived { get; set; }
        public decimal Fee { get; set; }
        public string Status { get; set; }

        public bool IsPaid => Status == "succeeded";
    }
}
