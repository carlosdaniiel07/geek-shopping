using Microsoft.EntityFrameworkCore;

namespace GeekShopping.Payment.Subscriber.Models.Context
{
    public class MySqlContext : DbContext
    {
        public DbSet<Entities.Payment> Payments { get; set; }

        public MySqlContext(DbContextOptions<MySqlContext> dbContextOptions) : base(dbContextOptions)
        {

        }
    }
}
