using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServicesProduct.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesProduct.DataAccess.Data.Configuration
{
    public class ProductConfiguration
    {
        public ProductConfiguration(EntityTypeBuilder<Product> entityBuilder)
        {

            entityBuilder.HasKey(x => x.ProductID);
            entityBuilder.Property(x => x.ProductName).IsRequired().HasMaxLength(100);
            entityBuilder.Property(x => x.SupplierID);
            entityBuilder.Property(x => x.CategoryID);
            entityBuilder.Property(x => x.QuantityPerUnit);
            entityBuilder.Property(x => x.UnitPrice);
            entityBuilder.Property(x => x.UnitsInStock);
            entityBuilder.Property(x => x.UnitsOnOrder);
            entityBuilder.Property(x => x.ReorderLevel);
            entityBuilder.Property(x => x.Discontinued);

            entityBuilder.HasData(new Product
            {
                ProductID = 1,
                ProductName = "Chai",
                SupplierID = 1,
                CategoryID = 1,
                QuantityPerUnit = "10 boxes x 20 bags",
                UnitPrice = 18,
                UnitsInStock = 39,
                UnitsOnOrder = 0,
                ReorderLevel = 10,
                Discontinued = false

            });
            entityBuilder.HasData(new Product
            {
                ProductID = 2,
                ProductName = "Chang",
                SupplierID = 1,
                CategoryID = 1,
                QuantityPerUnit = "24 - 12 oz bottles",
                UnitPrice = 19,
                UnitsInStock = 17,
                UnitsOnOrder = 40,
                ReorderLevel = 25,
                Discontinued = false
            });
            entityBuilder.HasData(new Product
            {

                ProductID = 3,
                ProductName = "Aniseed Syrup",
                SupplierID = 1,
                CategoryID = 2,
                QuantityPerUnit = "12 - 550 ml bottles",
                UnitPrice = 10,
                UnitsInStock = 13,
                UnitsOnOrder = 70,
                ReorderLevel = 25,
                Discontinued = false
            });
            entityBuilder.HasData(new Product
            {
                ProductID = 4,
                ProductName = "Chef Anton's Cajun Seasoning",
                SupplierID = 2,
                CategoryID = 2,
                QuantityPerUnit = "48 - 6 oz jars",
                UnitPrice = 22,
                UnitsInStock = 53,
                UnitsOnOrder = 0,
                ReorderLevel = 0,
                Discontinued = false
            });

            entityBuilder.HasData(new Product
            {
                ProductID = 5,
                ProductName = "Chef Anton's Gumbo Mix",
                SupplierID = 2,
                CategoryID = 2,
                QuantityPerUnit = "36 boxes",
                UnitPrice = 21,
                UnitsInStock = 0,
                UnitsOnOrder = 0,
                ReorderLevel = 0,
                Discontinued = true
            });
        }
    }
}
