using ProcureFlow.Application.Common.Models;
using ProcureFlow.Application.Features.Products.DTOs;
using ProcureFlow.Application.Features.Warehouses.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcureFlow.Application.Features.Warehouses.Interfaces
{
    public interface IWarehouseService
    {
        Task<IReadOnlyList<WarehouseDto>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<PaginatedResult<WarehouseDto>> GetAllPagedAsync(PaginationRequest request, CancellationToken cancellationToken = default);

        Task<WarehouseDto> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<WarehouseDto> CreateAsync(CreateWarehouseDto dto, CancellationToken cancellationToken = default);
        Task<WarehouseDto> UpdateAsync(Guid Id, UpdateWarehouseDto dto, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(Guid Id, CancellationToken cancellationToken = default);
    }
}
