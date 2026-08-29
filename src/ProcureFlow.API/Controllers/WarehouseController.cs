using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProcureFlow.API.Common.Controllers;
using ProcureFlow.API.Common.Responses;
using ProcureFlow.Application.Features.Warehouses.DTOs;
using ProcureFlow.Application.Features.Warehouses.Interfaces;

namespace ProcureFlow.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarehouseController : BaseController
    {
        private readonly IWarehouseService _warehouseService;

        public WarehouseController(IWarehouseService warehouseService)
        {
            _warehouseService = warehouseService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<WarehouseDto>>), StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse<IReadOnlyList<WarehouseDto>>>> GetAll(CancellationToken cancellationToken = default)
        {
            var warehouses = await _warehouseService.GetAllAsync(cancellationToken);

            return OkResponse(warehouses, "Warehouses fetched successfully.");
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(ApiResponse<WarehouseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse<WarehouseDto>>> GetById(Guid id, CancellationToken cancellationToken = default)
        {
            var warehouse = await _warehouseService.GetByIdAsync(id, cancellationToken);

            return OkResponse(warehouse, "Warehouse fetched successfully.");
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<WarehouseDto>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponse<WarehouseDto>>> Create([FromBody] CreateWarehouseDto dto, CancellationToken cancellationToken = default)
        {
            var warehouse = await _warehouseService.CreateAsync(dto, cancellationToken);

            return CreatedResponse(warehouse, "Warehouse created successfully.");
        }

        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(ApiResponse<WarehouseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse<WarehouseDto>>> Update(Guid id, [FromBody] UpdateWarehouseDto dto, CancellationToken cancellationToken = default)
        {
            var warehouse = await _warehouseService.UpdateAsync(id, dto, cancellationToken);

            return OkResponse(warehouse, "Warehouse updated successfully.");
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken = default)
        {
            await _warehouseService.DeleteAsync(id, cancellationToken);

            return NoContent();
        }
    }
}
