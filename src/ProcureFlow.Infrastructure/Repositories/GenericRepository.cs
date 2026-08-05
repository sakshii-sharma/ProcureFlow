using Microsoft.EntityFrameworkCore;
using ProcureFlow.Domain.Common;
using ProcureFlow.Infrastructure.Data;
using ProcureFlow.Infrastructure.Interfaces;

namespace ProcureFlow.Infrastructure.Repositories;

public class GenericRepository<TEntity> : IGenericRepository<TEntity>    where TEntity : BaseEntity
{
    protected readonly ProcureFlowDbContext _context;
    protected readonly DbSet<TEntity> _dbSet;

    public GenericRepository(ProcureFlowDbContext context)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
    }

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await _dbSet.AsNoTracking().ToListAsync();
    }

    public virtual async Task<TEntity?> GetByIdAsync(Guid id)
    {
        return await _dbSet.FindAsync(id);
    }

    public virtual async Task AddAsync(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public virtual void Update(TEntity entity)
    {
        _dbSet.Update(entity);
    }

    public virtual void Delete(TEntity entity)
    {
        _dbSet.Remove(entity);
    }

    public virtual async Task<bool> ExistsAsync(Guid id)
    {
        return await _dbSet.AnyAsync(x => x.Id == id);
    }
}