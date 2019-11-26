using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.Contracts.V1;
using Store.Contracts.V1.Requests;
using Store.Contracts.V1.Responses;
using Store.Models;
using Store.Services;

namespace Store.Controllers.V1
{
    [Authorize]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoriesService categoriesService;
        public IMapper Mapper { get; }
        public CategoryController(ICategoriesService categoriesService, IMapper mapper)
        {
            this.categoriesService = categoriesService;
            Mapper = mapper;
        }


        [AllowAnonymous]
        [HttpGet(ApiRoutes.Category.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            var response = Mapper.Map<ICollection<ProductCategory>>(await categoriesService.GetAllAsync());
            return Ok(response);
        }

        [HttpPost(ApiRoutes.Category.Add)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Add([FromBody]CategoryRequest categoryRequest)
        {
            var newCategory = Mapper.Map<ProductCategory>(categoryRequest);
            var product = await categoriesService.AddAsync(newCategory);
            var response = Mapper.Map<CategoryResponse>(product);
            return Ok(response);
        }
    }

    

}