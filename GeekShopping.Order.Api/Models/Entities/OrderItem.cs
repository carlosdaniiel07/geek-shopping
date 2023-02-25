using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeekShopping.Order.Api.Models.Entities
{
    [Table("order_item")]
    public class OrderItem : BaseEntity
    {
        [Column("name")]
        [Required]
        [StringLength(150)]
        public string Name { get; set; }

        [Column("price")]
        [Precision(19, 2)]
        public decimal Price { get; set; }

        [Column("description")]
        [StringLength(500)]
        public string Description { get; set; }

        [Column("quantity")]
        public int Quantity { get; set; }

        [Column("product_id")]
        public Guid ProductId { get; set; }

        [Column("total")]
        [Precision(19, 2)]
        public decimal Total { get; set; }

        public Order Order { get; set; }

        [Column("order_id")]
        public Guid OrderId { get; set; }
    }
}
