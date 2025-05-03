using MediatR;

namespace Streamline.Domain.Events;

/// <summary>
/// Base record for domain events. All domain events should inherit from this.
/// Implements INotification for MediatR integration.
/// </summary>
public abstract record DomainEvent : INotification; 