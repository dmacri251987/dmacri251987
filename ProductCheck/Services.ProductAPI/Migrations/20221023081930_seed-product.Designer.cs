﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Services.ProductAPI.DbContexts;

#nullable disable

namespace Services.ProductAPI.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20221023081930_seed-product")]
    partial class seedproduct
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc.2.22472.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Services.ProductAPI.Models.Product", b =>
                {
                    b.Property<int>("ProductID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductID"));

                    b.Property<int?>("CategoryID")
                        .HasColumnType("int");

                    b.Property<bool>("Discontinued")
                        .HasColumnType("bit");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<string>("QuantityPerUnit")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<short?>("ReorderLevel")
                        .HasColumnType("smallint");

                    b.Property<int?>("SupplierID")
                        .HasColumnType("int");

                    b.Property<decimal?>("UnitPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<short?>("UnitsInStock")
                        .HasColumnType("smallint");

                    b.Property<short?>("UnitsOnOrder")
                        .HasColumnType("smallint");

                    b.HasKey("ProductID");

                    b.ToTable("DMProductsAPI");

                    b.HasData(
                        new
                        {
                            ProductID = 1,
                            CategoryID = 1,
                            Discontinued = false,
                            ProductName = "Chai",
                            QuantityPerUnit = "10 boxes x 20 bags",
                            ReorderLevel = (short)10,
                            SupplierID = 1,
                            UnitPrice = 18m,
                            UnitsInStock = (short)39,
                            UnitsOnOrder = (short)0
                        },
                        new
                        {
                            ProductID = 2,
                            CategoryID = 1,
                            Discontinued = false,
                            ProductName = "Chang",
                            QuantityPerUnit = "24 - 12 oz bottles",
                            ReorderLevel = (short)25,
                            SupplierID = 1,
                            UnitPrice = 19m,
                            UnitsInStock = (short)17,
                            UnitsOnOrder = (short)40
                        },
                        new
                        {
                            ProductID = 3,
                            CategoryID = 2,
                            Discontinued = false,
                            ProductName = "Aniseed Syrup",
                            QuantityPerUnit = "12 - 550 ml bottles",
                            ReorderLevel = (short)25,
                            SupplierID = 1,
                            UnitPrice = 10m,
                            UnitsInStock = (short)13,
                            UnitsOnOrder = (short)70
                        },
                        new
                        {
                            ProductID = 4,
                            CategoryID = 2,
                            Discontinued = false,
                            ProductName = "Chef Anton's Cajun Seasoning",
                            QuantityPerUnit = "48 - 6 oz jars",
                            ReorderLevel = (short)0,
                            SupplierID = 2,
                            UnitPrice = 22m,
                            UnitsInStock = (short)53,
                            UnitsOnOrder = (short)0
                        },
                        new
                        {
                            ProductID = 5,
                            CategoryID = 2,
                            Discontinued = true,
                            ProductName = "Chef Anton's Gumbo Mix",
                            QuantityPerUnit = "36 boxes",
                            ReorderLevel = (short)0,
                            SupplierID = 2,
                            UnitPrice = 21m,
                            UnitsInStock = (short)0,
                            UnitsOnOrder = (short)0
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
