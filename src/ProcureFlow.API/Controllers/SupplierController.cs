using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProcureFlow.API.Common.Controllers;
using Microsoft.IdentityModel.Tokens;
using ProcureFlow.API.Common.Responses;
using ProcureFlow.Application.Features.Suppliers.DTOs;
using ProcureFlow.Application.Features.Suppliers.Interfaces;
using ProcureFlow.Application.Common.Models;
using ProcureFlow.Application.Features.Products.DTOs;

namespace ProcureFlow.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : BaseController
    {
        private readonly ISupplierService _supplierService;

        public SupplierController(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }


        // GET: api/supplier?pageNumber=1&pageSize=20
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<PaginatedResult<SupplierDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponse<PaginatedResult<SupplierDto>>>> GetAll([FromQuery] PaginationRequest request, CancellationToken cancellationToken)
        {
            var suppliers = await _supplierService.GetAllPagedAsync(request, cancellationToken);

            return OkResponse(suppliers, "Suppliers fetched successfully.");
        }


        /*
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<SupplierDto>>), StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse<IReadOnlyList<SupplierDto>>>> GetAll(CancellationToken cancellationToken = default)
        {
            var suppliers = await _supplierService.GetAllAsync(cancellationToken);

            return OkResponse(suppliers, "Suppliers fetched successfully.");
        }
        */

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(ApiResponse<SupplierDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse<SupplierDto>>> GetById(Guid id, CancellationToken cancellationToken = default)
        {
            var supplier = await _supplierService.GetByIdAsync(id, cancellationToken);

            return OkResponse(supplier, "Supplier fetched successfully.");
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<SupplierDto>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponse<SupplierDto>>> Create([FromBody] CreateSupplierDto dto, CancellationToken cancellationToken = default)
        {
            var supplier = await _supplierService.CreateAsync(dto, cancellationToken);

            return CreatedResponse(supplier, "Supplier created successfully.");
        }

        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(ApiResponse<SupplierDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse<SupplierDto>>> Update(Guid id, [FromBody] UpdateSupplierDto dto, CancellationToken cancellationToken = default)
        {
            var supplier = await _supplierService.UpdateAsync(id, dto, cancellationToken);

            return OkResponse(supplier, "Supplier updated successfully.");
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken = default)
        {
            await _supplierService.DeleteAsync(id, cancellationToken);

            return NoContent();
        }
    }
}