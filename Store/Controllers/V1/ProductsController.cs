using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.Contracts.V1;
using Store.Contracts.V1.Requests;
using Store.Contracts.V1.Responses;
using Store.Helpers;
using Store.Models;
using Store.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Controllers.V1
{
    [Authorize]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsService productsService;
        public IMapper Mapper { get; }

        public ProductsController(IProductsService productsService, IMapper mapper)
        {
            this.productsService = productsService;
            Mapper = mapper;
        }
        /// <summary>
        /// Pobiera Wszystkie Produkty
        /// </summary>
        [HttpGet(ApiRoutes.Products.GetAll)]
        public async Task<IActionResult> GetAll()
        {   
            var response = Mapper.Map<ICollection<ProductResponse>>(await productsService.GetAllAsync());
            return Ok(response);
        }
        /// <summary>
        /// Pobiera Produkty po Id
        /// </summary>
        [HttpGet(ApiRoutes.Products.Get)]
        public async Task<IActionResult> Get([FromRoute]Guid id)
        {
            var product = await productsService.GetAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            var response = Mapper.Map<ProductResponse>(product);
            return Ok(response);
        }
        /// <summary>
        /// Dodaje Produkty
        /// </summary>
        [HttpPost(ApiRoutes.Products.Add)]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Add([FromBody]ProductRequest productRequest)
        {
            //if (productRequest.Price<1 || productRequest.Price > 1000)
            //{
            //    return BadRequest();
            //}
            var newProduct = Mapper.Map<Product>(productRequest);
            var product = await productsService.AddAsync(newProduct);
            var response = Mapper.Map<ProductResponse>(product);
            return CreatedAtAction(nameof(Get), new { id = response.Id }, response);
        }
        /// <summary>
        /// Uaktualnia Produkty
        /// </summary>
        [HttpPut(ApiRoutes.Products.Update)]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody]ProductRequest productRequest)
        {
            var product = await productsService.GetAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            Mapper.Map(productRequest,product);
            var updatedProduct = await productsService.UpdateAsync(product);
            var response = Mapper.Map<ProductResponse>(updatedProduct);
            return Ok(response);
        }
        /// <summary>
        /// Usuwa Produkty
        /// </summary>
        [HttpDelete(ApiRoutes.Products.Delete)]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var deleted = await productsService.DeleteAsync(id);
            if (deleted)
                return NoContent();
            return NotFound();
        }
    }
}