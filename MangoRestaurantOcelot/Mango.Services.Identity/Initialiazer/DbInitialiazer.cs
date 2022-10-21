using Mango.Services.Identity.DbContexts;
using Mango.Services.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Mango.Services.Identity.Common;
using System.Security.Claims;
using IdentityModel;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Mango.Services.Identity.Initialiazer
{
    public class DbInitialiazer : IDbInitialiazer
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;


        public DbInitialiazer(ApplicationDbContext db, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
         
        }

        public void Initialize()
        {
            if (_roleManager.FindByIdAsync(SD.Admin).Result == null)
            {
                _roleManager.CreateAsync(new IdentityRole(SD.Admin)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Customer)).GetAwaiter().GetResult();

            }
            else
            {
                return;
            }

            ApplicationUser adminUser = new ApplicationUser()
            {
                UserName = "admin1@gmail.com",
                Email = "admin1@gmail.com",
                EmailConfirmed = true,
                PhoneNumber = "111111111111",
                FirstName = "Ben",
                LastName = "Admin"
            };

            //PasswordEncrypt();

            _userManager.CreateAsync(adminUser, "Admin123").GetAwaiter().GetResult();
            _userManager.AddToRoleAsync(adminUser, SD.Admin).GetAwaiter().GetResult();


            var temp1 = _userManager.AddClaimsAsync(adminUser, new Claim[]
             {
                new Claim(JwtClaimTypes.Name, adminUser.FirstName + " " + adminUser.LastName),
                new Claim(JwtClaimTypes.GivenName, adminUser.FirstName ),
                new Claim(JwtClaimTypes.FamilyName, adminUser.FirstName ),
                new Claim(JwtClaimTypes.Role, SD.Admin )
             }).Result;

            ApplicationUser customerUser = new ApplicationUser()
            {
                UserName = "cutomar1@gmail.com",
                Email = "cutomar1@gmail.com",
                EmailConfirmed = true,
                PhoneNumber = "111111111111",
                FirstName = "Ben",
                LastName = "Cust"
            };

            _userManager.CreateAsync(customerUser, "Admin123").GetAwaiter().GetResult();
            _userManager.AddToRoleAsync(customerUser, SD.Customer).GetAwaiter().GetResult();


            var temp2 = _userManager.AddClaimsAsync(customerUser, new Claim[]
             {
                new Claim(JwtClaimTypes.Name, customerUser.FirstName + " " + customerUser.LastName),
                new Claim(JwtClaimTypes.GivenName, customerUser.FirstName ),
                new Claim(JwtClaimTypes.FamilyName, customerUser.FirstName ),
                new Claim(JwtClaimTypes.Role, SD.Customer )
             }).Result;

        }

        private static void PasswordEncrypt()
        {
            string? password = "Admin123";

            byte[] salt;
            byte[] buffer2;
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, 0x10, 0x3e8))
            {
                salt = bytes.Salt;
                buffer2 = bytes.GetBytes(0x20);
            }
            byte[] dst = new byte[0x31];
            Buffer.BlockCopy(salt, 0, dst, 1, 0x10);
            Buffer.BlockCopy(buffer2, 0, dst, 0x11, 0x20);
            var resultado = Convert.ToBase64String(dst);
        }
    }
}
