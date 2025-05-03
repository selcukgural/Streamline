using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Streamline.Domain.Runtime;

namespace Streamline.Infrastructure.Persistence.Configurations;

public class ProcessInstanceConfiguration : IEntityTypeConfiguration<ProcessInstance>
{
    public void Configure(EntityTypeBuilder<ProcessInstance> builder)
    {
        // Table Name (optional, EF Core uses DbSet name by default)
        builder.ToTable("ProcessInstances");

        // Primary Key
        builder.HasKey(p => p.Id);

        // Properties
        builder.Property(p => p.ProcessDefinitionId)
            .IsRequired()
            .HasMaxLength(255); // Example length

        builder.Property(p => p.BusinessKey)
            .HasMaxLength(255); // Example length

        builder.Property(p => p.State)
            .IsRequired()
            .HasConversion<string>() // Enum'ı string olarak sakla
            .HasMaxLength(50);

        builder.Property(p => p.StartTime).IsRequired();

        // Relationships

        // ProcessInstance -> Executions (One-to-Many)
        builder.HasMany(p => p.Executions)
            .WithOne(e => e.ProcessInstance)
            .HasForeignKey(e => e.ProcessInstanceId)
            .OnDelete(DeleteBehavior.Cascade); // Bir ProcessInstance silindiğinde ilişkili Execution'lar da silinsin

        // ProcessInstance -> ActivityInstances (One-to-Many)
        builder.HasMany(p => p.ActivityInstances)
            .WithOne(a => a.ProcessInstance)
            .HasForeignKey(a => a.ProcessInstanceId)
            .OnDelete(DeleteBehavior.Cascade);

        // ProcessInstance -> Jobs (One-to-Many)
        builder.HasMany(p => p.Jobs)
            .WithOne() // Job'dan ProcessInstance'a doğrudan navigation olmayabilir, Execution üzerinden gidilebilir.
            .HasForeignKey("ProcessInstanceId") // Shadow property veya Job'a eklenmeli (zaten ekliydi)
            .IsRequired(false) // Bir Job doğrudan bir ProcessInstance'a bağlı olmayabilir (örn. global timer)
            .OnDelete(DeleteBehavior.Cascade);

        // ProcessInstance -> Incidents (One-to-Many)
        builder.HasMany(p => p.Incidents)
            .WithOne() // Incident'dan ProcessInstance'a doğrudan navigation olmayabilir.
            .HasForeignKey(i => i.ProcessInstanceId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        // ProcessInstance -> EventSubscriptions (One-to-Many)
        builder.HasMany(p => p.EventSubscriptions)
            .WithOne() // EventSubscription'dan ProcessInstance'a doğrudan navigation olmayabilir.
            .HasForeignKey(es => es.ProcessInstanceId)
            .IsRequired(false) // Subscription process instance'a bağlı olmayabilir (örn. global signal)
            .OnDelete(DeleteBehavior.Cascade);

        // ProcessInstance -> Variables (One-to-Many) - Process level variables
        // Bu ilişkiyi Execution üzerinden değil de doğrudan ProcessInstance üzerinden kurmak istersek:
        builder.HasMany(p => p.Variables)
            .WithOne() // Variable'dan ProcessInstance'a navigation yoksa
            .HasForeignKey("ProcessInstanceId") // Variable'a ProcessInstanceId eklenmeli
            .IsRequired(false) // Variable Execution'a da ait olabilir
            .OnDelete(DeleteBehavior.Cascade);

        // Indexes (Example)
        builder.HasIndex(p => p.ProcessDefinitionId);
        builder.HasIndex(p => p.BusinessKey);
        builder.HasIndex(p => p.State);
        builder.HasIndex(p => p.StartTime);
        builder.HasIndex(p => p.EndTime);
    }
}