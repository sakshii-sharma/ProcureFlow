using ProcureFlow.Application.Common.Interfaces;
using ProcureFlow.Application.Features.Categories.Interfaces;
using ProcureFlow.Domain.Entities;
using ProcureFlow.Application.Features.Categories.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProcureFlow.Application.Common.Exceptions;
using ProcureFlow.Application.Common.Models;
using ProcureFlow.Application.Features.Products.DTOs;

namespace ProcureFlow.Application.Features.Categories.Services
{
    public class CategoryService : ICategoryService
    {
        public readonly IGenericRepository<Category> _repository;

        public CategoryService(IGenericRepository<Category> repository)
        {
            _repository = repository;
        }

        // Implementation for inherited Interface Members

        public async Task<IReadOnlyList<CategoryDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var categories = await _repository.GetAllAsync(c => !c.IsDeleted, cancellationToken);

            return categories.Select(MapToDto).ToList(); 
        }

        public async Task<PaginatedResult<CategoryDto>> GetAllPagedAsync(PaginationRequest request, CancellationToken cancellationToken = default)
        {
            var result = await _repository.GetPagedAsync(request.PageNumber, request.PageSize, p => !p.IsDeleted, cancellationToken);

            var items = result.Items.Select(MapToDto).ToList();

            return new PaginatedResult<CategoryDto>
            {
                Items = items,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                TotalCount = result.TotalCount
            };
        }

        public async Task<CategoryDto> GetByIdAsync(Guid Id, CancellationToken cancellationToken = default)
        {
            var category = await _repository.FirstOrDefaultAsync(c => c.Id == Id && !c.IsDeleted, cancellationToken);

            if (category == null)
            {
                throw new NotFoundException("CATEGORY_NOT_FOUND", "Category was not found.");
            }

            return MapToDto(category);
        }

        public async Task<CategoryDto> CreateAsync(CreateCategoryDto dto, CancellationToken cancellationToken = default)
        {
            string duplicateName = dto.Name;

            bool nameExists = await _repository.AnyAsync(c => c.Name == duplicateName && !c.IsDeleted, cancellationToken);

            if (nameExists)
            {
                throw new ConflictException( "CATEGORY_ALREADY_EXISTS", "A category with this name already exists." );
            }

            // dto to Category (entity)
            var category = new Category
            {
                Name = dto.Name.Trim(),
                Description = dto.Description?.Trim()
            };

            await _repository.AddAsync(category, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);

            return MapToDto(category);
        }


        public async Task<CategoryDto> UpdateAsync(Guid Id, UpdateCategoryDto dto, CancellationToken cancellationToken = default)
        {
            var category = await _repository.FirstOrDefaultAsync(c => c.Id == Id && !c.IsDeleted, cancellationToken);
            if (category == null)
            {
                throw new NotFoundException("CATEGORY_NOT_FOUND", "Category was not found.");
            }

            bool nameExists = await _repository.AnyAsync(c => (c.Id != Id && c.Name == dto.Name && !c.IsDeleted) , cancellationToken);
            if (nameExists)
            {
                throw new ConflictException("CATEGORY_ALREADY_EXISTS", "A category with this name already exists.");
            }

            category.Name = dto.Name.Trim();
            category.Description = dto.Description?.Trim();

            _repository.Update(category);

            await _repository.SaveChangesAsync(cancellationToken);

            return MapToDto(category);
        }


        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var category = await _repository.FirstOrDefaultAsync(c => c.Id == id && !c.IsDeleted, cancellationToken);

            if (category == null)
            {
                throw new NotFoundException("CATEGORY_NOT_FOUND", "Category was not found.");
            }

            category.IsDeleted = true;
            category.DeletedAt = DateTime.UtcNow;

            _repository.Update(category);

            await _repository.SaveChangesAsync(cancellationToken);

            return true;
        }


        // Method to Map Entity(Repository Layer) TO Dto(Service/Application Layer)

        public static CategoryDto MapToDto(Category category)
        {
            CategoryDto dto = new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,   
                Description = category.Description
            };

            return dto;
        }
    }
}
