using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Streamline.Domain.Runtime;

namespace Streamline.Infrastructure.Persistence.Configurations;

public class ExecutionConfiguration : IEntityTypeConfiguration<Execution>
{
    public void Configure(EntityTypeBuilder<Execution> builder)
    {
        builder.ToTable("Executions");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.CurrentFlowNodeId)
            .HasMaxLength(255);

        // Relationships

        // Execution -> ProcessInstance (Many-to-One)
        // This is configured in ProcessInstanceConfiguration (HasMany)

        // Execution -> ParentExecution (Self-referencing Many-to-One)
        builder.HasOne(e => e.ParentExecution)
            .WithMany() // No collection navigation property for child executions by default
            .HasForeignKey(e => e.ParentExecutionId)
            .IsRequired(false) // Root executions have no parent
            .OnDelete(DeleteBehavior.Restrict); // Don't automatically delete child executions if parent is deleted directly? Might depend on logic.

        // Execution -> Variables (One-to-Many)
        builder.HasMany(e => e.Variables)
            .WithOne() // Variable to Execution navigation is not defined here
            .HasForeignKey(v => v.ExecutionId)
            .IsRequired(false) // Variable might belong to ProcessInstance instead
            .OnDelete(DeleteBehavior.Cascade);

        // Execution -> Jobs (One-to-Many)
        builder.HasMany(e => e.Jobs)
            .WithOne() // Job to Execution navigation is not defined here
            .HasForeignKey(j => j.ExecutionId)
            .IsRequired(false) // Job might be process-level or global
            .OnDelete(DeleteBehavior.Cascade);

        // Execution -> Incidents (One-to-Many)
        builder.HasMany(e => e.Incidents)
            .WithOne() // Incident to Execution navigation is not defined here
            .HasForeignKey(i => i.ExecutionId)
            .IsRequired() // Incident should typically belong to an execution path
            .OnDelete(DeleteBehavior.Cascade);

        // Execution -> EventSubscriptions (One-to-Many)
        builder.HasMany(e => e.EventSubscriptions)
            .WithOne() // EventSubscription to Execution navigation is not defined here
            .HasForeignKey(es => es.ExecutionId)
            .IsRequired(false) // Subscription might be process-level
            .OnDelete(DeleteBehavior.Cascade);

        // Indexes
        builder.HasIndex(e => e.ProcessInstanceId);
        builder.HasIndex(e => e.ParentExecutionId);
        builder.HasIndex(e => e.CurrentFlowNodeId);
        builder.HasIndex(e => e.IsActive);
    }
}