using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Streamline.Domain.Runtime;

namespace Streamline.Infrastructure.Persistence.Configurations;

public class ActivityInstanceConfiguration : IEntityTypeConfiguration<ActivityInstance>
{
    public void Configure(EntityTypeBuilder<ActivityInstance> builder)
    {
        builder.ToTable("ActivityInstances");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.FlowNodeId)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(a => a.FlowNodeName)
            .HasMaxLength(255);
            
        builder.Property(a => a.State)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(50);
            
        builder.Property(a => a.AssigneeId)
            .HasMaxLength(255);

        builder.Property(a => a.CandidateUserIds); // Max length? Depends on storage
        builder.Property(a => a.CandidateGroupIds); // Max length? Depends on storage

        builder.Property(a => a.ErrorMessage);
        builder.Property(a => a.ErrorDetails); // Consider using a specific DB type for long text

        // Relationships
        // ActivityInstance -> ProcessInstance (Many-to-One)
        // Configured in ProcessInstanceConfiguration (HasMany)

        // Indexes
        builder.HasIndex(a => a.ProcessInstanceId);
        builder.HasIndex(a => a.FlowNodeId);
        builder.HasIndex(a => a.State);
        builder.HasIndex(a => a.StartTime);
        builder.HasIndex(a => a.EndTime);
        builder.HasIndex(a => a.AssigneeId);
        builder.HasIndex(a => a.CalledProcessInstanceId);
    }
}