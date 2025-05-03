using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query; // Required for SetPropertyCalls
using Streamline.Domain.Abstractions;
using Streamline.Domain.Runtime;

namespace Streamline.Infrastructure.Persistence.Repositories;

/// <summary>
/// Extends the generic IRepository with EF Core specific bulk operations.
/// </summary>
/// <typeparam name="TEntity">The type of the entity. Must inherit from EntityBase.</typeparam>
public interface IEfRepository<TEntity> : IRepository<TEntity> where TEntity : EntityBase
{
    /// <summary>
    /// Executes a delete operation directly in the database based on the predicate.
    /// Does not track entities.
    /// </summary>
    /// <param name="predicate">The filter expression for entities to delete.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The number of rows affected.</returns>
    Task<int> ExecuteDeleteAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

    /// <summary>
    /// Executes an update operation directly in the database based on the predicate.
    /// Does not track entities.
    /// </summary>
    /// <param name="predicate">The filter expression for entities to update.</param>
    /// <param name="setPropertyCalls">An expression defining the property updates.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The number of rows affected.</returns>
    Task<int> ExecuteUpdateAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<SetPropertyCalls<TEntity>, SetPropertyCalls<TEntity>>> setPropertyCalls, CancellationToken cancellationToken = default);
} 