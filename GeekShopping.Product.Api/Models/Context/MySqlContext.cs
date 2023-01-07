using Microsoft.EntityFrameworkCore;
using ProductEntity = GeekShopping.Product.Api.Models.Entities.Product;

namespace GeekShopping.Product.Api.Models.Context
{
    public class MySqlContext : DbContext
    {
        public DbSet<ProductEntity> Products { get; set; }

        public MySqlContext(DbContextOptions<MySqlContext> dbContextOptions) : base(dbContextOptions)
        {

        }
    }
}
