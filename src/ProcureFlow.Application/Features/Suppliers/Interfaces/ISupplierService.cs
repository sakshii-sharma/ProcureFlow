using ProcureFlow.Application.Features.Suppliers.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProcureFlow.Application.Features.Suppliers.Interfaces
{
    public interface ISupplierService
    {
        Task<SupplierDto> CreateAsync(CreateSupplierDto dto, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<SupplierDto>> GetAllAsync(CancellationToken cancellationToken = default);

        Task<SupplierDto> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task<SupplierDto> UpdateAsync(Guid id, UpdateSupplierDto dto, CancellationToken cancellationToken = default);

        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
