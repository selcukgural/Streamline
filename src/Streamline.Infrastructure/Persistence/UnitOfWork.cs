// using Streamline.Application.Abstractions; // Removed
using MediatR; // Added for IMediator
// Added for ChangeTracker
using Streamline.Domain.Runtime;
// Added for DomainEvent
using Streamline.Domain.Abstractions; // Correct location for IUnitOfWork, IRepository
using Streamline.Infrastructure.Persistence.Repositories;
using System.Collections;

// Added for LINQ methods

namespace Streamline.Infrastructure.Persistence;

// Inject IMediator
public sealed class UnitOfWork(StreamlineDbContext context, IMediator mediator) : IUnitOfWork
{
    private readonly StreamlineDbContext _context = context ?? throw new ArgumentNullException(nameof(context));
    private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator)); // Assign IMediator
    private Hashtable? _repositories; // Lazy loading repository instances
    private bool _disposed;

    public IRepository<TEntity> GetRepository<TEntity>() where TEntity : EntityBase
    {
        _repositories ??= new Hashtable();

        var typeName = typeof(TEntity).Name;

        if (!_repositories.ContainsKey(typeName))
        {
            var repositoryType = typeof(EfRepository<>);
            var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _context);
                
            if (repositoryInstance != null)
            {
                _repositories.Add(typeName, repositoryInstance);
            }
            else
            {
                throw new InvalidOperationException($"Could not create repository for {typeName}");
            }
        }

        return (IRepository<TEntity>)_repositories[typeName]!;
    }

    // Updated SaveChangesAsync to dispatch domain events
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        // Get domain events
        var domainEntities = _context.ChangeTracker
            .Entries<EntityBase>()
            .Where(x => x.Entity.DomainEvents.Count != 0)
            .ToList(); // Materialize the list to avoid issues during iteration

        var domainEvents = domainEntities
            .SelectMany(x => x.Entity.DomainEvents)
            .ToList(); // Collect all events before clearing

        // Clear events before saving to prevent issues if SaveChanges fails 
        // or if handlers trigger more changes leading to infinite loops.
        domainEntities.ForEach(entity => entity.Entity.ClearDomainEvents());

        // Save changes to the database
        var result = await _context.SaveChangesAsync(cancellationToken);

        // Dispatch events only after successful save
        foreach (var domainEvent in domainEvents)
        {
            await _mediator.Publish(domainEvent, cancellationToken);
        }

        return result;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private void Dispose(bool disposing)
    {
        if (_disposed) return;
        if (disposing)
        {
            // Dispose managed state (managed objects).
            _context.Dispose();
            _repositories?.Clear(); // Clear the cache
        }

        // Free unmanaged resources (unmanaged objects) and override a finalizer below.
        // Set large fields to null.
        _disposed = true;
    }

    // Optional Finalizer ( uncomment if you have unmanaged resources )
    ~UnitOfWork()
    {
      Dispose(false);
    }
}