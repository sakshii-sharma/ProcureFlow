using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProcureFlow.Application.Common.Exceptions;
using ProcureFlow.Application.Common.Interfaces;
using ProcureFlow.Application.Features.Suppliers.DTOs;
using ProcureFlow.Application.Features.Suppliers.Interfaces;
using ProcureFlow.Domain.Entities;

namespace ProcureFlow.Application.Features.Suppliers.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly IGenericRepository<Supplier> _supplierRepository;

        public SupplierService(IGenericRepository<Supplier> supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }

        public async Task<IReadOnlyList<SupplierDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var suppliers = await _supplierRepository.GetAllAsync(s => !s.IsDeleted, cancellationToken);

            return suppliers.Select(MapToDto).ToList();
        }

        public async Task<SupplierDto> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var supplier = await _supplierRepository.FirstOrDefaultAsync(s => s.Id == id && !s.IsDeleted, cancellationToken);

            if (supplier is null)
                throw new NotFoundException("SUPPLIER_NOT_FOUND", "Supplier not found.");

            return MapToDto(supplier);
        }

        public async Task<SupplierDto> CreateAsync(CreateSupplierDto dto, CancellationToken cancellationToken = default)
        {
            var supplier = new Supplier
            {
                Name = dto.Name,
                Email = dto.Email,
                Phone = dto.Phone,
                Address = dto.Address
            };

            await _supplierRepository.AddAsync(supplier);
           // _supplierRepository.Add(supplier);
            await _supplierRepository.SaveChangesAsync(cancellationToken);

            return MapToDto(supplier);
        }

        public async Task<SupplierDto> UpdateAsync(Guid id, UpdateSupplierDto dto, CancellationToken cancellationToken = default)
        {
            var supplier = await _supplierRepository.FirstOrDefaultAsync(s => s.Id == id && !s.IsDeleted, cancellationToken);

            if (supplier == null)
                throw new NotFoundException("SUPPLIER_NOT_FOUND", "Supplier not found.");

            supplier.Name = dto.Name;
            supplier.Email = dto.Email;
            supplier.Phone = dto.Phone;
            supplier.Address = dto.Address;

             _supplierRepository.Update(supplier);
            await _supplierRepository.SaveChangesAsync(cancellationToken);

            return MapToDto(supplier);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var supplier = await _supplierRepository.FirstOrDefaultAsync(s => s.Id == id && !s.IsDeleted, cancellationToken);

            if (supplier == null)
                throw new NotFoundException("SUPPLIER_NOT_FOUND", "Supplier not found.");

            supplier.IsDeleted = true;
            supplier.DeletedAt = DateTime.UtcNow;

            _supplierRepository.Update(supplier);
            await _supplierRepository.SaveChangesAsync(cancellationToken);
        }

        private static SupplierDto MapToDto(Supplier supplier)
        {
            return new SupplierDto
            {
                Id = supplier.Id,
                Name = supplier.Name,
                Email = supplier.Email,
                Phone = supplier.Phone,
                Address = supplier.Address
            };
        }
    }
}
