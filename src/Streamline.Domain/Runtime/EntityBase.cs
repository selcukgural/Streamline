using Streamline.Domain.Events; // DomainEvent için

namespace Streamline.Domain.Runtime;

/// <summary>
/// Base class for all runtime entities, providing a common identifier and domain event handling.
/// </summary>
public abstract class EntityBase
{
    /// <summary>
    /// Unique identifier for the entity.
    /// Using Guid for distributed systems compatibility.
    /// </summary>
    public Guid Id { get; protected set; } = Guid.NewGuid();

    // Consider adding CreatedDate, ModifiedDate etc. later if needed.

    // --- Domain Event Handling ---
    private readonly List<DomainEvent> _domainEvents = [];

    /// <summary>
    /// Gets the collection of domain events raised by this entity.
    /// </summary>
    [System.Text.Json.Serialization.JsonIgnore] // Avoid serializing domain events if entities are passed over API
    // [Newtonsoft.Json.JsonIgnore] // Removed as Newtonsoft.Json might not be used
    public IReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    /// <summary>
    /// Adds a domain event to the entity.
    /// </summary>
    /// <param name="domainEvent">The domain event to add.</param>
    protected void AddDomainEvent(DomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    /// <summary>
    /// Clears all domain events from the entity.
    /// Should be called after the events have been dispatched.
    /// </summary>
    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
    // ---------------------------
}