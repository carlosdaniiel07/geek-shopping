using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeekShopping.Cart.Api.Models.Entities
{
    [Table("cart_item")]
    public class CartItem : BaseEntity
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

        public Cart Cart { get; set; }

        [Column("cart_id")]
        public Guid CartId { get; set; }
    }
}
