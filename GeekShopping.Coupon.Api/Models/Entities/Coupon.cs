using GeekShopping.Coupon.Api.Models.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeekShopping.Coupon.Api.Models.Entities
{
    [Table("coupon")]
    [Index(nameof(Code), IsUnique = true)]
    public class Coupon : BaseEntity
    {
        [Column("code")]
        [Required]
        [StringLength(6)]
        public string Code { get; set; }

        [Column("type")]
        public CouponType Type { get; set; }

        [Column("value")]
        [Precision(19, 2)]
        public decimal Value { get; set; }

        [Column("expires_at")]
        public DateTime? ExpiresAt { get; set; }
    }
}
