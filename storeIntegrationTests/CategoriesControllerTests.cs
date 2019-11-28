using AutoFixture;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Store;
using Store.Contracts.V1;
using Store.Contracts.V1.Responses;
using Store.Data;
using Store.Data.Migrations;
using Store.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using static Store.Contracts.V1.ApiRoutes;

namespace storeIntegrationTests
{
    public class CategoriesControllerTests : IntegrationTest
    {
        [Fact]
        public async Task GetAll_AsAnonymousUser_ReturnUnAuthorized()
        {
            var response = await TestClient.GetAsync(ApiRoutes.Category.GetAll);
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }
        [Fact]
        public async Task Add_UnAuthorized_CanNotAdd()
        {
            var response = await TestClient.PostAsync(ApiRoutes.Category.Add,null);
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }
        [Fact]
        public async Task Add_User_CanNotAdd()
        {
            await Login();
            var response = await TestClient.PostAsync(ApiRoutes.Category.Add, null);
            Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
        }
        [Fact]
        public async Task Add_Admin_CanAdd()
        {
            await LoginAsAdmin();
            var category = new ProductCategory { Id = 1, CategoryName = "kappa" };
            var response = await TestClient.PostAsJsonAsync(ApiRoutes.Category.Add,category);
            var response2 = await TestClient.GetAsync(ApiRoutes.Category.GetAll);
            var categories = await response2.Content.ReadAsAsync<IEnumerable<CategoryResponse>>();
            var final = await response.Content.ReadAsAsync<CategoryResponse>();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal("kappa",final.CategoryName);
            Assert.Equal("kappa",categories.ToArray().FirstOrDefault().CategoryName);
        }
        [Fact]
        public async Task GetAll_AsAuthorized_ReturnsCategories()
        {
            var fixture = new Fixture();
            await DbContext.ProductCategory.AddRangeAsync(fixture.CreateMany<ProductCategory>(4));
            await DbContext.SaveChangesAsync();
            await LoginAsAdmin();
            var response = await TestClient.GetAsync(ApiRoutes.Category.GetAll);
            var categories = await response.Content.ReadAsAsync<IEnumerable<CategoryResponse>>();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(4,categories.Count());
        }
    }
}
