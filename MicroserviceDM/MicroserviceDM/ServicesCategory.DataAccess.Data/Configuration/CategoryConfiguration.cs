using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServicesCategory.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesCategory.DataAccess.Data.Configuration
{
    public class CategoryConfiguration
    {
        public CategoryConfiguration(EntityTypeBuilder<Category> entityBuilder)
        {

            entityBuilder.HasKey(x => x.CategoryID);
            entityBuilder.Property(x => x.CategoryName).IsRequired().HasMaxLength(100);
            entityBuilder.Property(x => x.Description).IsRequired().HasMaxLength(500);
            entityBuilder.Property(x => x.Picture).HasMaxLength(500);
            entityBuilder.HasData(new Category
            {
                CategoryID = 1,
                CategoryName = "Beverages",
                Description = "Soft drinks, coffees, teas, beers, and ales",
                Picture = new Byte[64]
            });
            entityBuilder.HasData(new Category
            {
                CategoryID = 2,
                CategoryName = "Condiments",
                Description = "Sweet and savory sauces, relishes, spreads, and seasonings",
                Picture = new Byte[64]

            });
            entityBuilder.HasData(new Category
            {
                CategoryID = 3,
                CategoryName = "Confections",
                Description = "Desserts, candies, and sweet breads",
                Picture = new Byte[64]

            });
            entityBuilder.HasData(new Category
            {
                CategoryID = 4,
                CategoryName = "Dairy Products",
                Description = "Cheeses",
                Picture = new Byte[64]

            });
            entityBuilder.HasData(new Category
            {

                CategoryID = 5,
                CategoryName = "Grains/Cereals",
                Description = "Breads, crackers, pasta, and cereal",
                Picture = new Byte[64]

            });
            entityBuilder.HasData(new Category
            {

                CategoryID = 6,
                CategoryName = "Meat/Poultry",
                Description = "Prepared meats",
                Picture = new Byte[64]

            });
            entityBuilder.HasData(new Category
            {

                CategoryID = 7,
                CategoryName = "Produce",
                Description = "Dried fruit and bean curd",
                Picture = new Byte[64]

            });
            entityBuilder.HasData(new Category
            {
                CategoryID = 8,
                CategoryName = "Seafood",
                Description = "Seaweed and fish",
                Picture = new Byte[64]

            });
        }
    }
}
