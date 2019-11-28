using Microsoft.AspNetCore.Mvc.Testing;
using Store;
using Store.Contracts.V1;
using Store.Contracts.V1.Responses;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace storeIntegrationTests
{
    public class ProductControllerTests
    {
        [Fact]
        public async Task Test1()
        {
            var factory = new WebApplicationFactory<Startup>();
            var client = factory.CreateClient();
            var response = await client.GetAsync(ApiRoutes.Category.GetAll);
            var categories = await response.Content.ReadAsAsync<IEnumerable<CategoryResponse>>();
        }
    }
}
