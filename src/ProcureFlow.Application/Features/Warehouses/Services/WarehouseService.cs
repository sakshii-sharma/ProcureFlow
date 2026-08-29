using ProcureFlow.Application.Common.Interfaces;
using ProcureFlow.Application.Features.Warehouses.Interfaces;
using ProcureFlow.Application.Features.Warehouses.DTOs;
using ProcureFlow.Application.Common.Exceptions;
using ProcureFlow.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProcureFlow.Application.Common.Models;
using ProcureFlow.Application.Features.Suppliers.DTOs;

namespace ProcureFlow.Application.Features.Warehouses.Services
{
    public class WarehouseService : IWarehouseService
    {
        public readonly IGenericRepository<Warehouse> _warehouseRepository;

        public WarehouseService(IGenericRepository<Warehouse> warehouseRepository)
        {
            _warehouseRepository = warehouseRepository;
        }

        // Implementation for inherited Interface Members
        public async Task<IReadOnlyList<WarehouseDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var warehouses = await _warehouseRepository.GetAllAsync(w => !w.IsDeleted , cancellationToken);

            return warehouses.Select(MapToDto).ToList();
        }

        public async Task<PaginatedResult<WarehouseDto>> GetAllPagedAsync(PaginationRequest request, CancellationToken cancellationToken = default)
        {
            var result = await _warehouseRepository.GetPagedAsync(request.PageNumber, request.PageSize, p => !p.IsDeleted, cancellationToken);

            var items = result.Items.Select(MapToDto).ToList();

            return new PaginatedResult<WarehouseDto>
            {
                Items = items,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                TotalCount = result.TotalCount
            };
        }

        public async Task<WarehouseDto> GetByIdAsync(Guid Id, CancellationToken cancellationToken = default)
        {
            var warehouse = await _warehouseRepository.FirstOrDefaultAsync(w => w.Id == Id && !w.IsDeleted, cancellationToken);

            if(warehouse is null)
            {
                throw new NotFoundException("WAREHOUSE_NOT_FOUND", "warehouse was not found.");
            }

            return MapToDto(warehouse);
        }

        public async Task<WarehouseDto> CreateAsync(CreateWarehouseDto dto, CancellationToken cancellationToken = default)
        {
            // dto to Category (entity)
            var warehouseEntity = new Warehouse
            {
                Name = dto.Name.Trim(),
                Location = dto.Location
            };


            await _warehouseRepository.AddAsync(warehouseEntity, cancellationToken);
            await _warehouseRepository.SaveChangesAsync(cancellationToken);

            return MapToDto(warehouseEntity);
        }

        public async Task<WarehouseDto> UpdateAsync(Guid id, UpdateWarehouseDto dto, CancellationToken cancellationToken)
        {
            var warehouse = await _warehouseRepository.GetByIdAsync(id, cancellationToken);

            if (warehouse is null)
            {
                throw new NotFoundException("WAREHOUSE_NOT_FOUND", "Warehouse not found.");
            }

            warehouse.Name = dto.Name;
            warehouse.Location = dto.Location;

            _warehouseRepository.Update(warehouse);
            await _warehouseRepository.SaveChangesAsync(cancellationToken);

            return MapToDto(warehouse);
        }

        public async Task<bool> DeleteAsync( Guid id,  CancellationToken cancellationToken)
        {
            var warehouse = await _warehouseRepository.GetByIdAsync(id, cancellationToken);

            if (warehouse is null)
            {
                throw new NotFoundException("WAREHOUSE_NOT_FOUND", "Warehouse not found.");
            }

            _warehouseRepository.Update(warehouse);
            await _warehouseRepository.SaveChangesAsync(cancellationToken);

            return true;
        }



        // Method to Map Entity(Repository Layer) TO Dto(Service/Application Layer)
        public static WarehouseDto MapToDto(Warehouse entity)
        {
            WarehouseDto dto = new ()
            {
                Id = entity.Id,
                Name = entity.Name,
                Location = entity.Location,
            };

            return dto;
        }

    }
    
}
