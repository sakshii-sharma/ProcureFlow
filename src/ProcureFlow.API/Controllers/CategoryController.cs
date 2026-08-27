using Microsoft.AspNetCore.Mvc;
using ProcureFlow.API.Common.Controllers;
using ProcureFlow.API.Common.Responses;
using ProcureFlow.Application.Features.Categories.DTOs;
using ProcureFlow.Application.Features.Categories.Interfaces;

namespace ProcureFlow.API.Controllers;

[Route("api/[controller]")]
public class CategoryController : BaseController
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    // GET: api/categories
    [HttpGet]
    [ProducesResponseType( typeof(ApiResponse<IReadOnlyList<CategoryDto>>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse<IReadOnlyList<CategoryDto>>>> GetAll(CancellationToken cancellationToken)
    {
        var categories = await _categoryService.GetAllAsync(cancellationToken);

        return OkResponse(categories, "Categories fetched successfully.");
    }

    // GET: api/categories/{id}
    [HttpGet("{id:guid}")]
    [ProducesResponseType( typeof(ApiResponse<CategoryDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApiResponse<CategoryDto>>> GetById( Guid id, CancellationToken cancellationToken)
    {
        var category = await _categoryService.GetByIdAsync( id, cancellationToken);

        return OkResponse(category, "Category fetched successfully.");
    }

    // POST: api/categories
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<CategoryDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<ApiResponse<CategoryDto>>> Create( [FromBody] CreateCategoryDto dto, CancellationToken cancellationToken)
    {
        var category = await _categoryService.CreateAsync( dto, cancellationToken);

        return CreatedResponse( category, "Category created successfully.");
    }

    // PUT: api/categories/{id}
    [HttpPut("{id:guid}")]
    [ProducesResponseType( typeof(ApiResponse<CategoryDto>),  StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<ApiResponse<CategoryDto>>> Update( Guid id, [FromBody] UpdateCategoryDto dto,  CancellationToken cancellationToken)
    {
        var category = await _categoryService.UpdateAsync( id, dto, cancellationToken);

        return OkResponse( category, "Category updated successfully.");
    }

    // DELETE: api/categories/{id}
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete( Guid id, CancellationToken cancellationToken)
    {
        await _categoryService.DeleteAsync( id, cancellationToken);

        return NoContent();
    }
}