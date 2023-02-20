using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeekShopping.Cart.Api.Models.Entities
{
    [Table("cart")]
    public class Cart : BaseEntity
    {
        public IEnumerable<CartItem> Items { get; set; }

        [Column("user_id")]
        public Guid UserId { get; set; }

        [Column("coupon")]
        [StringLength(6)]
        public string Coupon { get; set; }
    }
}
