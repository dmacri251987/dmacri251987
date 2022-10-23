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


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Category>().HasData(new Category
            {
                CategoryID =1,
                CategoryName = "Beverages",
                Description = "Soft drinks, coffees, teas, beers, and ales",
                Picture = null

            });
            modelBuilder.Entity<Category>().HasData(new Category
            {
                CategoryID = 2,
                CategoryName = "Condiments",
                Description = "Sweet and savory sauces, relishes, spreads, and seasonings",
                Picture = null

            });
            modelBuilder.Entity<Category>().HasData(new Category
            {
                CategoryID = 3,
                CategoryName = "Confections",
                Description = "Desserts, candies, and sweet breads",
                Picture = null

            });
            modelBuilder.Entity<Category>().HasData(new Category
            {
                CategoryID = 4,
                CategoryName = "Dairy Products",
                Description = "Cheeses",
                Picture = null

            });
            modelBuilder.Entity<Category>().HasData(new Category
            {

                CategoryID = 5,
                CategoryName = "Grains/Cereals",
                Description = "Breads, crackers, pasta, and cereal",
                Picture = null

            });
            modelBuilder.Entity<Category>().HasData(new Category
            {

                CategoryID = 6,
                CategoryName = "Meat/Poultry",
                Description = "Prepared meats",
                Picture = null

            });
            modelBuilder.Entity<Category>().HasData(new Category
            {

                CategoryID = 7,
                CategoryName = "Produce",
                Description = "Dried fruit and bean curd",
                Picture = null

            });
            modelBuilder.Entity<Category>().HasData(new Category
            {
                CategoryID = 8,
                CategoryName = "Seafood",
                Description = "Seaweed and fish",
                Picture = null

            });
        }
    }
}
