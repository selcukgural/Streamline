using Microsoft.EntityFrameworkCore;
using Streamline.Infrastructure.Persistence;
using Streamline.Domain.Abstractions;
using Streamline.Engine.Services;
using Streamline.Infrastructure.Persistence.Repositories;
using Streamline.Infrastructure.Services;
using Streamline.Application;
using Streamline.Engine.Abstractions;
using Hangfire;
using Hangfire.Storage.SQLite;
using Streamline.Application.Features.Executions.Commands;
using Streamline.Application.Services;
using Streamline.Infrastructure.Scheduling;
using Streamline.Engine.Services.Handlers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add DbContext configuration
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<StreamlineDbContext>(options =>
    options.UseSqlite(connectionString)
    // .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking) // Optional: for read-only scenarios primarily
    );

// Register Unit of Work and Generic Repository
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));

// Register MediatR
builder.Services.AddMediatR(cfg => 
    cfg.RegisterServicesFromAssembly(typeof(ContinueExecutionCommand).Assembly));

// Register BPMN XML service
builder.Services.AddScoped<IBpmnXmlService, BpmnXmlService>();

// Register Engine Services
// Register IExecutionFlowManager and its implementation
builder.Services.AddScoped<IExecutionFlowManager, ExecutionFlowManager>();
// Keep existing registrations or update if necessary
// builder.Services.AddScoped<ExecutionFlowManager>(); // Removed direct registration if interface is used
builder.Services.AddScoped<IFlowNodeHandlerFactory, FlowNodeHandlerFactory>();

// Streamline.Engine assembly'sindeki tüm IFlowNodeEventHandler implementasyonlarını tara ve kaydet
// Update assembly source if needed due to ExecutionFlowManager move
var engineAssembly = typeof(ExecutionFlowManager).Assembly;
builder.Services.Scan(scan => scan
    .FromAssemblies(engineAssembly)
    .AddClasses(classes => classes.AssignableTo<IFlowNodeEventHandler>())
    .AsImplementedInterfaces()
    .WithScopedLifetime());

// Register Application Services
builder.Services.AddScoped<TimerJobTriggerService>();

// Register Infrastructure Services specific to Application needs
// ITimerJobScheduler now comes from Domain.Abstractions
builder.Services.AddScoped<ITimerJobScheduler, HangfireTimerJobScheduler>();

// --- Add Hangfire Services ---
builder.Services.AddHangfire(config => config
    .SetDataCompatibilityLevel(CompatibilityLevel.Version_180) // Use appropriate version
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UseSQLiteStorage("hangfire.db")); // Use SQLite storage

// Add the processing server as IHostedService
builder.Services.AddHangfireServer(options =>
{
    // Configure server options if needed (e.g., worker count)
    // options.WorkerCount = Environment.ProcessorCount * 2;
});
// -----------------------------

// --- Register Concrete Handlers for Factory ---
builder.Services.AddScoped<StartEventHandler>(); 
builder.Services.AddScoped<EndEventHandler>();
builder.Services.AddScoped<ParallelGatewayHandler>();
builder.Services.AddScoped<ExclusiveGatewayHandler>(); 
builder.Services.AddScoped<InclusiveGatewayHandler>();
builder.Services.AddScoped<EventBasedGatewayHandler>();
builder.Services.AddScoped<CSharpScriptTaskHandler>(); 
builder.Services.AddScoped<JavaScriptTaskHandler>();
builder.Services.AddScoped<TaskHandler>();
builder.Services.AddScoped<UserTaskHandler>();
builder.Services.AddScoped<ServiceTaskHandler>();
builder.Services.AddScoped<BusinessRuleTaskHandler>();
builder.Services.AddScoped<SendTaskHandler>();
builder.Services.AddScoped<ReceiveTaskHandler>();
builder.Services.AddScoped<ManualTaskHandler>();
builder.Services.AddScoped<IntermediateThrowEventHandler>();
builder.Services.AddScoped<IntermediateCatchEventHandler>();
builder.Services.AddScoped<BoundaryEventHandler>();
builder.Services.AddScoped<SubProcessHandler>();
// --------------------------------------------

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

// --- Add Hangfire Dashboard Middleware ---
// Add Hangfire Dashboard (accessible at /hangfire)
// TODO: Add authorization for the dashboard in production environments!
app.UseHangfireDashboard(); 
// ---------------------------------------

app.MapControllers();

app.Run();