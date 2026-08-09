using ProcureFlow.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProcureFlow.Application.Common.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {

        Task<IReadOnlyList<TEntity>> GetAllAsync(Expression<Func<TEntity,bool>>? predicate = null, CancellationToken cancellationToken = default); // getAll with or without search parameter - predicate
        Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity,bool>> predicate, CancellationToken cancellationToken = default);
        Task<bool> AnyAsync(Expression<Func<TEntity,bool>> predicate, CancellationToken cancellationToken = default);
        Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
