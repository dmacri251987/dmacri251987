using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServicesIdentity.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesIdentity.DataAccess.Data.Configuration
{
    public class UserConfiguration
    {
        public UserConfiguration(EntityTypeBuilder<User> entityBuilder)
        {

            entityBuilder.HasKey(x => x.Id);
            entityBuilder.Property(x => x.UserName).IsRequired().HasMaxLength(20);
            entityBuilder.Property(x => x.Password).IsRequired().HasMaxLength(500);
            entityBuilder.Property(x => x.Name).IsRequired().HasMaxLength(50);
            entityBuilder.Property(x => x.LastName).IsRequired().HasMaxLength(50);
            entityBuilder.Property(x => x.Email).HasMaxLength(100);
            entityBuilder.HasData(new User
            {
                Id = 1,
                UserName = "admin",
                Password = "1234",
                Name = "Admin",
                LastName = "Admin",
                Email = "admin@admin.com"
            });
            entityBuilder.HasData(new User
            {
                Id = 2,
                UserName = "pepe",
                Password = "1234",
                Name = "Pepe",
                LastName = "Pepe",
                Email = "pepe@admin.com"
            });
        }
    }
}
