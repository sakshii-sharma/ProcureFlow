using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProcureFlow.API.Common.Responses;
using ProcureFlow.Application.Features.Products.DTOs;
using ProcureFlow.Application.Features.Products.Interfaces;
using ProcureFlow.Domain.Entities;
using ProcureFlow.API.Common.Controllers;

namespace ProcureFlow.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : BaseController
    {
        public readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: api/products
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<ProductDto>>), StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse<IReadOnlyList<ProductDto>>>> GetAll(CancellationToken cancellationToken)
        {
            var products = await _productService.GetAllAsync(cancellationToken);

            return OkResponse(products, "Products fetched successfully");
        }

        // GET: api/products/{id}
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(ApiResponse<ProductDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse<ProductDto>>> GetById(Guid Id, CancellationToken cancellationToken)
        {
            var product = await _productService.GetByIdAsync(Id, cancellationToken);

            return OkResponse(product, "Product fetched successfully");
        }


        // POST: api/products
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<ProductDto>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<ApiResponse<ProductDto>>> Create([FromBody] CreateProductDto dto, CancellationToken cancellationToken)
        {
            var product = await _productService.CreateAsync(dto, cancellationToken);

            return CreatedResponse(product, "Product created successfully");
        }

        
        // PUT: api/products/{id}
        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(ApiResponse<ProductDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<ApiResponse<ProductDto>>> Update(Guid id, [FromBody] UpdateProductDto dto, CancellationToken cancellationToken)
        {
            var product = await _productService.UpdateAsync(id, dto, cancellationToken);

            return OkResponse(product, "Product updated successfully.");
        }

        // DELETE: api/products/{id}
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id,  CancellationToken cancellationToken)
        {
            await _productService.DeleteAsync(id, cancellationToken);

            return NoContent();
        }

    }
}
