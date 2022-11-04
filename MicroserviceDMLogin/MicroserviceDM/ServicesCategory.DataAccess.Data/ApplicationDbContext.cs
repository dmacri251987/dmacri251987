using Microsoft.EntityFrameworkCore;
using ServicesCategory.DataAccess.Data.Configuration;
using ServicesCategory.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesCategory.DataAccess.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Database schema
            builder.HasDefaultSchema("Category");

            ModelConfig(builder);
        }
        private void ModelConfig(ModelBuilder modelBuilder)
        {
            new CategoryConfiguration(modelBuilder.Entity<Category>());

        }
    }

}

