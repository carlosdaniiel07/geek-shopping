using GeekShopping.Order.Api.Models.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeekShopping.Order.Api.Models.Entities
{
    [Table("order")]
    public class Order : BaseEntity
    {
        [Column("customer")]
        [MaxLength(120)]
        [Required]
        public string Customer { get; set; }

        [Column("document")]
        [MaxLength(20)]
        [Required]
        public string Document { get; set; }

        [Column("phone")]
        [MaxLength(20)]
        [Required]
        public string Phone { get; set; }

        [Column("address")]
        [MaxLength(300)]
        [Required]
        public string Address { get; set; }

        [Column("user_id")]
        public Guid UserId { get; set; }

        [Column("coupon")]
        [StringLength(6)]
        public string Coupon { get; set; }

        public IEnumerable<OrderItem> Items { get; set; }

        [Column("total")]
        [Precision(19, 2)]
        public decimal Total { get; set; }

        [Column("status")]
        public OrderStatus Status { get; set; }
    }
}
