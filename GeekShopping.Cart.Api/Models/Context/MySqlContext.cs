using Microsoft.EntityFrameworkCore;

namespace GeekShopping.Cart.Api.Models.Context
{
    public class MySqlContext : DbContext
    {
        public DbSet<Entities.Cart> Carts { get; set; }
        public DbSet<Entities.CartItem> CartItems { get; set; }

        public MySqlContext(DbContextOptions<MySqlContext> dbContextOptions) : base(dbContextOptions)
        {

        }
    }
}
