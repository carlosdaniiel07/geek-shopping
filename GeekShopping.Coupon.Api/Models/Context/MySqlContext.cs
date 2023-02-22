using Microsoft.EntityFrameworkCore;

namespace GeekShopping.Coupon.Api.Models.Context
{
    public class MySqlContext : DbContext
    {
        public DbSet<Entities.Coupon> Coupons { get; set; }

        public MySqlContext(DbContextOptions<MySqlContext> dbContextOptions) : base(dbContextOptions)
        {

        }
    }
}
