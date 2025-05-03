using Streamline.Domain.Runtime; // For EntityBase

namespace Streamline.Domain.Abstractions;

/// <summary>
/// Defines the Unit of Work pattern for managing transactions and repositories.
/// Resides in Domain as it defines how domain entities are persisted.
/// </summary>
public interface IUnitOfWork : IDisposable
{
    /// <summary>
    /// Gets a generic repository for the specified entity type.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <returns>An instance of the generic repository for the entity type.</returns>
    IRepository<TEntity> GetRepository<TEntity>() where TEntity : EntityBase;

    /// <summary>
    /// Saves all changes made in this unit of work to the underlying data store.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The number of state entries written to the data store.</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}