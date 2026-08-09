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
        Task<IEnumerable<CategoryDto>> GetAllAsync();
        Task<CategoryDto> GetByIdAsync(Guid Id);
        Task<Guid> CreateAsync(CategoryDto dto); 
        Task UpdateAsync(UpdateCategoryDto dto);
        Task DeleteAsync(Guid Id);
    }
}
