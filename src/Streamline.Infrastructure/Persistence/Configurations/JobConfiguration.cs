using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Streamline.Domain.Runtime;

namespace Streamline.Infrastructure.Persistence.Configurations;

public class JobConfiguration : IEntityTypeConfiguration<Job>
{
    public void Configure(EntityTypeBuilder<Job> builder)
    {
        builder.ToTable("Jobs");

        builder.HasKey(j => j.Id);

        builder.Property(j => j.JobHandlerType)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(j => j.JobHandlerConfiguration);

        builder.Property(j => j.TenantId)
            .HasMaxLength(255);

        builder.Property(j => j.LockOwner)
            .HasMaxLength(255);
            
        // Relationships
        // Job -> Execution (Many-to-One)
        // Configured in ExecutionConfiguration
            
        // Job -> ProcessInstance (Many-to-One)
        // Configured in ProcessInstanceConfiguration

        // Indexes
        builder.HasIndex(j => j.ExecutionId);
        builder.HasIndex(j => j.ProcessInstanceId);
        builder.HasIndex(j => j.JobHandlerType);
        builder.HasIndex(j => j.DueDate); 
        builder.HasIndex(j => j.Retries);
        builder.HasIndex(j => j.LockExpirationTime); // Important for job acquisition
        builder.HasIndex(j => new { j.LockOwner, j.LockExpirationTime }); // Composite index for locking
        builder.HasIndex(j => j.TenantId);
    }
}