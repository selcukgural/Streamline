// using MediatR; // Removed
using Microsoft.EntityFrameworkCore;
using Streamline.Domain.Runtime; // Domain varlıklarımızı kullanmak için
using Streamline.Domain.Events; // Added for DomainEvent
// using System.Linq; // No longer needed here for events

namespace Streamline.Infrastructure.Persistence;

// Removed IMediator from constructor
public class StreamlineDbContext(DbContextOptions<StreamlineDbContext> options) : DbContext(options)
{
    // Removed IMediator field

    // Runtime Entities
    public DbSet<ProcessInstance> ProcessInstances { get; set; }
    public DbSet<Execution> Executions { get; set; }
    public DbSet<ActivityInstance> ActivityInstances { get; set; }
    public DbSet<Variable> Variables { get; set; }
    public DbSet<Job> Jobs { get; set; }
    public DbSet<Incident> Incidents { get; set; }
    public DbSet<EventSubscription> EventSubscriptions { get; set; }
    // TODO: Add DbSets for other domain entities (e.g., ProcessDefinition if managed here)

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Ignore DomainEvent base class
        modelBuilder.Ignore<DomainEvent>();

        // Burada Fluent API kullanarak varlıklar arasındaki ilişkileri,
        // kısıtlamaları, indeksleri vb. yapılandıracağız.
        // Örneğin:
        // modelBuilder.Entity<ProcessInstance>()
        //     .HasMany(p => p.Executions)
        //     .WithOne(e => e.ProcessInstance)
        //     .HasForeignKey(e => e.ProcessInstanceId);
        //
        // modelBuilder.Entity<Execution>()
        //     .HasMany(e => e.Variables) // Assuming Variables are local to Execution for now
        //     .WithOne() // No navigation property back to Execution from Variable? Needs clarification
        //     .HasForeignKey("ExecutionId"); // Shadow property or add to Variable entity

        // Diğer ilişkiler ve yapılandırmalar buraya eklenecek.
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(StreamlineDbContext).Assembly);
    }

    // Override SaveChangesAsync but remove event dispatching
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        // await DispatchDomainEventsAsync(); // Removed call
        // Add any other pre-save logic here if needed (e.g., Auditing)
        return await base.SaveChangesAsync(cancellationToken);
    }

    // Removed DispatchDomainEventsAsync method
}