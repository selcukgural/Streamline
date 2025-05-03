using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Streamline.Domain.Runtime;

namespace Streamline.Infrastructure.Persistence.Configurations;

public class VariableConfiguration : IEntityTypeConfiguration<Variable>
{
    public void Configure(EntityTypeBuilder<Variable> builder)
    {
        builder.ToTable("Variables");

        builder.HasKey(v => v.Id);

        builder.Property(v => v.Name)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(v => v.Type) // Renamed from DataType
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(v => v.Value); // Max length? Depends on expected data size

        // Relationships
        // Variable -> Execution (Many-to-One)
        // Configured in ExecutionConfiguration (HasMany)

        // Variable -> ProcessInstance (Many-to-One)
        // Configured in ProcessInstanceConfiguration (HasMany)

        // Composite Index for uniqueness (optional, depends on requirements)
        // Ensure a variable name is unique within its scope (either execution or process instance)
        builder.HasIndex(v => new { v.ProcessInstanceId, v.ExecutionId, v.Name }).IsUnique();

        // Other Indexes
        builder.HasIndex(v => v.ExecutionId);
        builder.HasIndex(v => v.ProcessInstanceId);
        builder.HasIndex(v => v.Name);
    }
}