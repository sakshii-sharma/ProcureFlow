using ProcureFlow.Application.Features.Categories.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcureFlow.Application.Features.Categories.Interfaces
{
    public interface ICategoryService
    {
        // interface defines what the Category application service can do
        Task<IReadOnlyList<CategoryDto>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<CategoryDto> GetByIdAsync(Guid Id, CancellationToken cancellationToken = default);
        Task<CategoryDto> CreateAsync(CreateCategoryDto dto, CancellationToken cancellationToken = default); 
        Task<CategoryDto> UpdateAsync(Guid Id, UpdateCategoryDto dto, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(Guid Id, CancellationToken cancellationToken = default);
    }
}


/*
    WHY THESE METHODS :

    GetAllAsync
        → GET /api/categories

    GetByIdAsync
        → GET /api/categories/{id}

    CreateAsync
        → POST /api/categories

    UpdateAsync
        → PUT /api/categories/{id}

    DeleteAsync
        → DELETE /api/categories/{id}
 */