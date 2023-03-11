using System.Text.Json.Serialization;

namespace GeekShopping.Payment.Api.Models.Dto
{
    public class CheckoutCompletedDto
    {
        public string Id { get; set; }
        public string Object { get; set; }

        [JsonPropertyName("payment_intent")]
        public string PaymentIntent { get; set; }

        public string Status { get; set; }

        [JsonPropertyName("payment_status")]
        public string PaymentStatus { get; set; }

        public bool IsCompleted => Status == "complete";
        public bool IsPaid => PaymentStatus == "paid";
    }
}
