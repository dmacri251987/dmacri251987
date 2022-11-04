using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServicesIdentity.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesIdentity.DataAccess.Data.Configuration
{
    public class UserRolConfiguration
    {
        public UserRolConfiguration(EntityTypeBuilder<UserRol> entityBuilder)
        {

            entityBuilder.HasKey(x => x.Id);
            entityBuilder.Property(x => x.IdClient).IsRequired();
            entityBuilder.Property(x => x.IdRol).IsRequired();        
            entityBuilder.HasData(new UserRol
            {
                Id = 1,
                IdClient = 1,
                IdRol = 1,
             
            });
            entityBuilder.HasData(new UserRol
            {
                Id = 2,
                IdClient = 2,
                IdRol = 2,
            });
        }
    }
}
