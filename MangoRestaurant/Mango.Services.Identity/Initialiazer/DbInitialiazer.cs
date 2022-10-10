using Mango.Services.Identity.DbContexts;
using Mango.Services.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Mango.Services.Identity.Common;

namespace Mango.Services.Identity.Initialiazer
{
    public class DbInitialiazer : IDbInitialiazer
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userMAnager;
        private readonly RoleManager<IdentityRole> _roleManager;


        public DbInitialiazer(ApplicationDbContext db, UserManager<ApplicationUser> userMAnager, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userMAnager = userMAnager;
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
                PhoneNumber = "111111111111"
            };
        }
    }
}
