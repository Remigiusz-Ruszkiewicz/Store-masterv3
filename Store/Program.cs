using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Store.Data;
using Store.Helpers;
using Store.Services;

namespace Store
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();
            using (var servicescoped = host.Services.CreateScope())
            {
                var dbcontext = servicescoped.ServiceProvider.GetRequiredService<DataContext>();
                await dbcontext.Database.MigrateAsync();
                var userManager = servicescoped.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
                var user = new IdentityUser { UserName = "admin@admin.pl" };
                var password = "Admin1337!";
                if (await userManager.FindByNameAsync(user.UserName)==null)
                {
                    var result = await userManager.CreateAsync(user, password);
                }
                var roleManager = servicescoped.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var role = new IdentityRole { Name = "Admin" };
                if (await roleManager.FindByNameAsync(role.Name) == null)
                {
                    var result = await roleManager.CreateAsync(role);
                }
                await userManager.AddToRoleAsync(await userManager.FindByNameAsync(user.UserName), role.Name);
                var signinManager = servicescoped.ServiceProvider.GetRequiredService<SignInManager<IdentityUser>>();
                //await HttpContext.LoginAsync()
                //var options = servicescoped.ServiceProvider.GetRequiredService<IOptions<AppSettings>>();
                //    await HttpContext htp = HttpContextAccessor;
                ////var options = servicescoped.ServiceProvider.GetRequiredService(IHttpContextAccessor httpContextAccessor);
                //var userService = new UsersService(userManager,signinManager,options);
                //var loginresult = await userService.LoginAsync(user.UserName, password);
                //Console.WriteLine(loginresult.Token);


            }
            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
