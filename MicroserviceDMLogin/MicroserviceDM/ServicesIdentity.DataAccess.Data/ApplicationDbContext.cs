using Microsoft.EntityFrameworkCore;
using ServicesIdentity.DataAccess.Data.Configuration;
using ServicesIdentity.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesIdentity.DataAccess.Data
{
    public class ApplicationDbContext:DbContext
    {
        
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<User> User { get; set; }
        public DbSet<UserRol> UserRol { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Database schema
            builder.HasDefaultSchema("Identity");

            ModelConfig(builder);
        }
        private void ModelConfig(ModelBuilder modelBuilder)
        {
            new UserConfiguration(modelBuilder.Entity<User>());
            new UserRolConfiguration(modelBuilder.Entity<UserRol>());
        }
    }
}
