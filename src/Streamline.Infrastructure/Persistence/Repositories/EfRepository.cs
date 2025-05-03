using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Streamline.Domain.Runtime;
using System.Linq.Expressions;

namespace Streamline.Infrastructure.Persistence.Repositories;

/// <summary>
/// Generic repository implementation using Entity Framework Core.
/// Implements both IRepository and IEfRepository for EF Core specific operations.
/// </summary>
/// <typeparam name="TEntity">The entity type.</typeparam>
public class EfRepository<TEntity> : IEfRepository<TEntity> where TEntity : EntityBase
{
    protected readonly StreamlineDbContext _context;
    protected readonly DbSet<TEntity> _dbSet;

    public EfRepository(StreamlineDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _dbSet = _context.Set<TEntity>();
    }

    public virtual Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return _dbSet.FindAsync(new object[] { id }, cancellationToken).AsTask();
    }

    public virtual Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return _dbSet.FirstOrDefaultAsync(predicate, cancellationToken);
    }

    public virtual Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        return _dbSet.AddAsync(entity, cancellationToken).AsTask();
    }

    public virtual Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        _dbSet.Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
        return Task.CompletedTask;
    }

    public virtual Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        if (_context.Entry(entity).State == EntityState.Detached)
        {
            _dbSet.Attach(entity);
        }
        _dbSet.Remove(entity);
        return Task.CompletedTask;
    }

    public virtual async Task<bool> DeleteByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await GetByIdAsync(id, cancellationToken);
        if (entity == null) return false;
        await DeleteAsync(entity, cancellationToken);
        return true;
    }

    public virtual Task<List<TEntity>> ListAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return _dbSet.Where(predicate).ToListAsync(cancellationToken);
    }

    public virtual Task<List<TEntity>> ListAllAsync(CancellationToken cancellationToken = default)
    {
        return _dbSet.ToListAsync(cancellationToken);
    }

    public virtual Task<int> CountAsync(CancellationToken cancellationToken = default)
    {
        return _dbSet.CountAsync(cancellationToken);
    }

    public virtual Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return _dbSet.CountAsync(predicate, cancellationToken);
    }

    public virtual Task<bool> AnyAsync(CancellationToken cancellationToken = default)
    {
        return _dbSet.AnyAsync(cancellationToken);
    }

    public virtual Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return _dbSet.AnyAsync(predicate, cancellationToken);
    }

    public virtual Task<int> ExecuteDeleteAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return _dbSet.Where(predicate).ExecuteDeleteAsync(cancellationToken);
    }

    public virtual Task<int> ExecuteUpdateAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<SetPropertyCalls<TEntity>, SetPropertyCalls<TEntity>>> setPropertyCalls, CancellationToken cancellationToken = default)
    {
        return _dbSet.Where(predicate).ExecuteUpdateAsync(setPropertyCalls, cancellationToken);
    }
}