using Microsoft.AspNetCore.Mvc.Testing;
using Store;
using Store.Contracts.V1;
using Store.Contracts.V1.Responses;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace storeIntegrationTests
{
    public class UserControllerTests : IntegrationTest
    {
        [Fact]
        public async Task te4st1()
        {
            var response = await TestClient.GetAsync(ApiRoutes.Category.GetAll);
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }
    }
}
