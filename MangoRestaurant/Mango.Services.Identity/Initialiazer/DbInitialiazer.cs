using Mango.Services.Identity.DbContexts;
using Mango.Services.Identity.Models;
using Microsoft.AspNetCore.Identity;

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
           
        }
    }
}
