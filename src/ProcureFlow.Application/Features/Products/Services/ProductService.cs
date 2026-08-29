using ProcureFlow.Application.Common.Exceptions;
using ProcureFlow.Application.Common.Interfaces;
using ProcureFlow.Application.Features.Categories.DTOs;
using ProcureFlow.Application.Features.Categories.Interfaces;
using ProcureFlow.Application.Features.Products.DTOs;
using ProcureFlow.Application.Features.Products.Interfaces;
using ProcureFlow.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcureFlow.Application.Features.Products.Services
{
    public class ProductService : IProductService
    {
        public readonly IGenericRepository<Product> _productRepository;
        public readonly IGenericRepository<Category> _categoryRepository;

        public ProductService(IGenericRepository<Product> productRepository, IGenericRepository<Category> categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        // Implementation for inherited Interface Members
        public async Task<IReadOnlyList<ProductDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var products = await _productRepository.GetAllAsync(p => !p.IsDeleted, cancellationToken);

            return products.Select(MapToDto).ToList();
        }

        public async Task<ProductDto> GetByIdAsync(Guid Id, CancellationToken cancellationToken = default)
        {
            var product = await _productRepository.FirstOrDefaultAsync(p => p.Id == Id && !p.IsDeleted, cancellationToken);

            if (product is null)
            {
                throw new NotFoundException("PRODUCT_NOT_FOUND", "product was not found.");
            }

            return MapToDto(product);
        }

        public async Task<ProductDto> CreateAsync(CreateProductDto dto, CancellationToken cancellationToken = default)
        {
            // check Category Exists
            bool categoryExists = await _categoryRepository.AnyAsync(c => c.Id == dto.CategoryId, cancellationToken);

            if (!categoryExists)
            {
                throw new NotFoundException("CATEGORY_NOT_FOUND", "Invalid Category Id");
            }

            // check SKU uniquenesss
            bool skuExists = await _productRepository.AnyAsync(p => p.SKU == dto.SKU, cancellationToken);

            if (skuExists)
            {
                throw new ConflictException("SKU_Already_Present", "SKU already present");
            }

            // dto to Category (entity)
            var productentity = new Product
            {
                Name = dto.Name.Trim(),
                SKU = dto.SKU,
                CategoryId = dto.CategoryId,
                Description = dto.Description?.Trim()
            };


            await _productRepository.AddAsync(productentity, cancellationToken);
            await _productRepository.SaveChangesAsync(cancellationToken);
            
            return MapToDto(productentity);
        }


        public async Task<ProductDto> UpdateAsync(Guid Id, UpdateProductDto dto, CancellationToken cancellationToken = default)
        {
            
            var product = await _productRepository.FirstOrDefaultAsync(p => p.Id == Id && !p.IsDeleted, cancellationToken);
            if (product is null)
            {
                throw new NotFoundException("PRODUCT_NOT_FOUND", "Product was not found.");
            }
            

            bool skuExists = await _productRepository.AnyAsync(p => p.SKU == dto.SKU, cancellationToken);
            if (skuExists)
            {
                throw new ConflictException("SKU_Already_Present", "A Product with this SKU already exists.");
            }

            // check Category Exists
            bool categoryExists = await _categoryRepository.AnyAsync(c => c.Id == dto.CategoryId, cancellationToken);

            if (!categoryExists)
            {
                throw new NotFoundException("CATEGORY_NOT_FOUND", "Invalid Category Id");
            }

            product.Name = dto.Name.Trim();
            product.Description = dto.Description?.Trim();
            product.SKU = dto.SKU;
            product.CategoryId = dto.CategoryId;

            _productRepository.Update(product);

            await _productRepository.SaveChangesAsync(cancellationToken);

            return MapToDto(product);
        }


        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var product = await _productRepository.FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted, cancellationToken);

            if (product is null)
            {
                throw new NotFoundException("PRODUCT_NOT_FOUND", "product was not found.");
            }

            product.IsDeleted = true;
            product.DeletedAt = DateTime.UtcNow;

            _productRepository.Update(product);

            await _productRepository.SaveChangesAsync(cancellationToken);

            return true;
        }

        // Method to Map Entity(Repository Layer) TO Dto(Service/Application Layer)

        public static ProductDto MapToDto(Product product)
        {
            return new ProductDto          
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                SKU = product.SKU,
                CategoryId = product.CategoryId
            };

        }
    }
}
