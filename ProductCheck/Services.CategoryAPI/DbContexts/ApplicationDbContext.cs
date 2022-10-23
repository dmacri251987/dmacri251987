using Microsoft.EntityFrameworkCore;
using Services.CategoryAPI.Models;

namespace Services.CategoryAPI.DbContexts
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

       public DbSet<Category> DMCategoriesAPI { get; set; }
    }
}
