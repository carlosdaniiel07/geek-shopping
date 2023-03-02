using GeekShopping.Payment.Subscriber.Models.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeekShopping.Payment.Subscriber.Models.Entities
{
    [Table("payment")]
    [Index(nameof(CheckoutExternalId), IsUnique = true)]
    public class Payment : BaseEntity
    {
        [Column("order_id")]
        public Guid OrderId { get; set; }

        [Column("total_value")]
        [Precision(19, 2)]
        public decimal TotalValue { get; set; }

        [Column("fee")]
        [Precision(19, 2)]
        public decimal Fee { get; set; }

        [Column("checkout_url")]
        [Required]
        [MaxLength(300)]
        public string CheckoutUrl { get; set; }

        [Column("checkout_external_id")]
        [Required]
        [MaxLength(100)]
        public string CheckoutExternalId { get; set; }

        [Column("payment_external_id")]
        [MaxLength(100)]
        public string PaymentExternalId { get; set; }

        [Column("status")]
        public PaymentStatus Status { get; set; } = PaymentStatus.Pending;

        [Column("external_provider")]
        [MaxLength(50)]
        [Required]
        public string ExternalProvider { get; set; }
    }
}
