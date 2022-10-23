using Microsoft.EntityFrameworkCore;
using Services.ProductAPI.Models;

namespace Services.ProductAPI.DbContexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Product> DMProductsAPI { get; set; }

    }
}
