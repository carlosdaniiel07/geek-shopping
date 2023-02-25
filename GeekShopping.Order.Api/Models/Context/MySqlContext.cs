using GeekShopping.Order.Api.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace GeekShopping.Order.Api.Models.Context
{
    public class MySqlContext : DbContext
    {
        public DbSet<Entities.Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        public MySqlContext(DbContextOptions<MySqlContext> dbContextOptions) : base(dbContextOptions)
        {

        }
    }
}
