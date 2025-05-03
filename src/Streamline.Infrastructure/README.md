# Streamline.Infrastructure

The `Streamline.Infrastructure` project provides the infrastructure layer for the Streamline BPMN workflow engine. It implements data persistence, repository patterns, background job scheduling, and integration services required by the application and engine layers.

## Key Responsibilities

- **Persistence:**  
  Implements Entity Framework Core (EF Core) for database access, schema management, and migrations.
- **Repositories:**  
  Provides generic and EF Core-specific repository implementations for domain entities.
- **Unit of Work:**  
  Manages transactional operations and domain event dispatching.
- **Background Jobs:**  
  Integrates with Hangfire for scheduling and executing background jobs (e.g., timer events).
- **BPMN XML Services:**  
  Handles import/export of BPMN 2.0 XML definitions.
- **Configuration:**  
  Contains EF Core entity configurations for all runtime entities.

## Main Components

### 1. Persistence

- **DbContext:**  
  `StreamlineDbContext` defines `DbSet` properties for all runtime entities (e.g., `ProcessInstance`, `Execution`, `ActivityInstance`, `Variable`, `Job`, `Incident`, `EventSubscription`).
- **Configurations:**  
  Fluent API configurations for each entity are located in the `Persistence/Configurations` folder, ensuring correct table mappings, relationships, and indexes.
- **Migrations:**  
  Database schema migrations are managed via EF Core and stored in the `Migrations` folder.

### 2. Repositories

- **Generic Repository:**  
  `EfRepository<TEntity>` provides CRUD and query operations for any entity.
- **EF Core Extensions:**  
  `IEfRepository<TEntity>` adds bulk update and delete operations using EF Core 9 features.
- **Unit of Work:**  
  `UnitOfWork` coordinates repository usage and ensures atomic operations, also dispatching domain events via MediatR after successful commits.

### 3. Scheduling

- **Hangfire Integration:**  
  `HangfireTimerJobScheduler` implements timer job scheduling using Hangfire, enabling delayed and recurring background tasks.

### 4. BPMN XML Services

- **BpmnXmlService:**  
  Handles serialization and deserialization of BPMN 2.0 XML files to and from C# domain models.

### 5. Design-Time Support

- **DesignTimeDbContextFactory:**  
  Enables EF Core tools to create the DbContext for migrations and schema updates.

## Dependencies

- `Microsoft.EntityFrameworkCore` (Core, Sqlite, InMemory, Tools, Design)
- `Hangfire.Storage.SQLite`
- `Microsoft.CodeAnalysis.Common`
- `MediatR` (for domain event dispatching)
- `Streamline.Application`, `Streamline.Domain`, `Streamline.Engine` (project references)

## Folder Structure

- `Persistence/`  
  - `StreamlineDbContext.cs`  
  - `Configurations/` (entity configurations)  
  - `Repositories/` (repository implementations)  
  - `UnitOfWork.cs`  
  - `DesignTimeDbContextFactory.cs`
- `Migrations/`  
  - EF Core migration files
- `Scheduling/`  
  - `HangfireTimerJobScheduler.cs`
- `Services/`  
  - `BpmnXmlService.cs`

## Usage

- Add `Streamline.Infrastructure` as a dependency to your application or API project.
- Configure the database connection string in your main project.
- Register the DbContext, repositories, and services in your DI container.
- Use the Unit of Work and repositories for data access in your application layer.

## Example: Registering Infrastructure in DI

```csharp
services.AddDbContext<StreamlineDbContext>(options =>
    options.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));

services.AddScoped<IUnitOfWork, UnitOfWork>();
services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
services.AddScoped<ITimerJobScheduler, HangfireTimerJobScheduler>();
services.AddScoped<IBpmnXmlService, BpmnXmlService>();
services.AddHangfire(config => config.UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UseSqlServerStorage(Configuration.GetConnectionString("HangfireConnection")));
```