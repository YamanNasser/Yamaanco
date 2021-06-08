using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Yamaanco.Application.Enums;
using Yamaanco.Infrastructure.EF.Identity.Persistence.Models;

namespace Yamaanco.Infrastructure.EF.Identity.Persistence.Seeds
{
    public static class DefaultRoles
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Roles

            await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Business.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Basic.ToString()));
        }
    }
}