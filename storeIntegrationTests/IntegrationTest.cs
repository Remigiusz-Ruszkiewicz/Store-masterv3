using Microsoft.AspNetCore.Mvc.Testing;
using Store;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Net.Http.Headers;
using Store.Contracts.V1;
using Store.Contracts.V1.Requests;
using System.Threading.Tasks;
using Store.Contracts.V1.Responses;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Store.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace storeIntegrationTests
{
    public abstract class IntegrationTest
    {
        protected readonly HttpClient TestClient;
        protected readonly DataContext DbContext;
        private readonly IServiceScope serviceScope;
        protected IntegrationTest()
        {
            var factory = new WebApplicationFactory<Startup>().WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.RemoveAll(typeof(DataContext));
                    services.AddDbContext<DataContext>(options =>
                    {
                        options.UseInMemoryDatabase("Store_Test");
                    });
                });
            });
            TestClient = factory.CreateClient();
            serviceScope = factory.Server.Host.Services.CreateScope();
            DbContext = serviceScope.ServiceProvider.GetRequiredService<DataContext>();
        }
        protected async Task Login()
        {
            TestClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await GetUserToken());
        }
        protected async Task LoginAsAdmin()
        {
            await SeedDatabase.AdminRole(serviceScope);
            TestClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await GetAdminToken());
        }

        private async Task<string> GetUserToken()
        {
           var response = await TestClient.PostAsJsonAsync(ApiRoutes.Users.Add, new NewUserRequest
           {
               Email = "Test144232@interia.pl",
               Password = "Remikaasd1322!"
           });
            var authSucces = await response.Content.ReadAsAsync<AuthSuccessResponse>();
            return authSucces.Token;
        }
        private async Task<string> GetAdminToken()
        {
            var response = await TestClient.PostAsJsonAsync(ApiRoutes.Users.Login, new LoginUserRequest
            {
                UserName = "admin@admin.pl",
                Password = "Admin1337!"
            });
            var authSucces = await response.Content.ReadAsAsync<AuthSuccessResponse>();
            return authSucces.Token;
        }
    }
}
