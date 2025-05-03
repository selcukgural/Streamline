using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Streamline.Domain.Runtime;

namespace Streamline.Infrastructure.Persistence.Configurations;

public class IncidentConfiguration : IEntityTypeConfiguration<Incident>
{
    public void Configure(EntityTypeBuilder<Incident> builder)
    {
        builder.ToTable("Incidents");

        builder.HasKey(i => i.Id);

        builder.Property(i => i.IncidentType)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(i => i.ActivityId)
            .HasMaxLength(255);

        builder.Property(i => i.ProcessDefinitionId)
            .HasMaxLength(255);

        builder.Property(i => i.Message)
            .IsRequired();

        builder.Property(i => i.Details); // Potentially long text
            
        builder.Property(i => i.TenantId)
            .HasMaxLength(255);

        // Relationships
        // Incident -> Execution (Many-to-One)
        // Configured in ExecutionConfiguration

        // Incident -> ProcessInstance (Many-to-One)
        // Configured in ProcessInstanceConfiguration

        // Indexes
        builder.HasIndex(i => i.ExecutionId);
        builder.HasIndex(i => i.ProcessInstanceId);
        builder.HasIndex(i => i.ActivityId);
        builder.HasIndex(i => i.JobId);
        builder.HasIndex(i => i.IncidentType);
        builder.HasIndex(i => i.IncidentTimestamp);
        builder.HasIndex(i => i.TenantId);
    }
}