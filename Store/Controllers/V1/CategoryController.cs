using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Store.Contracts.V1;
using Store.Contracts.V1.Requests;
using Store.Contracts.V1.Responses;
using Store.Helpers;
using Store.Models;
using Store.Services;

namespace Store.Controllers.V1
{

    [Authorize]
    [ApiController]
    [Produces("application/json")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoriesService categoriesService;
        public IMapper Mapper { get; }
        public CategoryController(ICategoriesService categoriesService, IMapper mapper)
        {
            this.categoriesService = categoriesService;
            Mapper = mapper;
        }

        /// <summary>
        /// Pobiera Wszystkie Kategorie
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet(ApiRoutes.Category.GetAll)]
        public async Task<IActionResult> GetAll([FromQuery]PaginationRequest paginationRequest)
        {
            var paginationFilter = Mapper.Map<PaginationFilter>(paginationRequest);
            var categories = await categoriesService.GetAllAsync(paginationFilter);
            var response = PaginationHelper.CreateResponse<CategoryResponse>(paginationFilter,
                Mapper.Map<ICollection<CategoryResponse>>(categories),
                HttpContext, 
                ApiRoutes.Category.GetAll);
            return Ok(response);
        }

        //lol
        /// <summary>
        /// Dodaje Kategorie
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /v1/categories
        ///     {
        ///        "name": "nowa nazwa",
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Zwraca utworzoną kategorię</response>
        /// <response code="400">Błąd walidacji</response>
        /// <response code="401">Użytkownik Niezalogowany</response>
        /// <response code="403">Brak Dostępu</response>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpPost(ApiRoutes.Category.Add)]
        [ProducesResponseType(typeof(CategoryResponse),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Add([FromBody]CategoryRequest categoryRequest)
        {
            var newCategory = Mapper.Map<ProductCategory>(categoryRequest);
            var product = await categoriesService.AddAsync(newCategory);
            var response = Mapper.Map<CategoryResponse>(product);
            return Ok(response);
        }
    }

    

}