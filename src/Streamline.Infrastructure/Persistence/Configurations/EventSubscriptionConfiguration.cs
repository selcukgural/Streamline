using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Streamline.Domain.Runtime;

namespace Streamline.Infrastructure.Persistence.Configurations;

public class EventSubscriptionConfiguration : IEntityTypeConfiguration<EventSubscription>
{
    public void Configure(EntityTypeBuilder<EventSubscription> builder)
    {
        builder.ToTable("EventSubscriptions");

        builder.HasKey(es => es.Id);

        builder.Property(es => es.EventType)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(es => es.EventName)
            .HasMaxLength(255);

        builder.Property(es => es.ActivityId)
            .HasMaxLength(255);

        builder.Property(es => es.TenantId)
            .HasMaxLength(255);

        // Relationships
        // EventSubscription -> Execution (Many-to-One)
        // Configured in ExecutionConfiguration

        // EventSubscription -> ProcessInstance (Many-to-One)
        // Configured in ProcessInstanceConfiguration

        // Indexes
        builder.HasIndex(es => es.ExecutionId);
        builder.HasIndex(es => es.ProcessInstanceId);
        builder.HasIndex(es => es.ActivityId);
        builder.HasIndex(es => es.EventType);
        builder.HasIndex(es => es.EventName); 
        builder.HasIndex(es => es.TenantId);
        // Composite index for efficient event correlation
        builder.HasIndex(es => new { es.EventType, es.EventName, es.ExecutionId, es.TenantId });
    }
}