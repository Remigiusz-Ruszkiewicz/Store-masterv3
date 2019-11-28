using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Data
{
    public static class SeedDatabase
    {
        public static async Task AdminRole(IServiceScope serviceScope)
        {
            var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var user = new IdentityUser { UserName = "admin@admin.pl" };
            var password = "Admin1337!";
            if (await userManager.FindByNameAsync(user.UserName) == null)
            {
                var result = await userManager.CreateAsync(user, password);
            }
            var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var role = new IdentityRole { Name = "Admin" };
            if (await roleManager.FindByNameAsync(role.Name) == null)
            {
                var result = await roleManager.CreateAsync(role);
            }
            await userManager.AddToRoleAsync(await userManager.FindByNameAsync(user.UserName), role.Name);
        }
    }
}
