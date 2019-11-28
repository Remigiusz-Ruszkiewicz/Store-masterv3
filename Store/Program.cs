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
                await SeedDatabase.AdminRole(servicescoped);
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
