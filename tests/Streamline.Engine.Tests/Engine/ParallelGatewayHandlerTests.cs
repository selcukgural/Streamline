using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Streamline.Application.Features.ProcessInstances.Commands;
using Streamline.Domain.Abstractions;
using Streamline.Domain.Runtime;
using Streamline.Engine.Abstractions;
using Streamline.Engine.Services;
using Streamline.Engine.Services.Handlers;
using Streamline.Infrastructure.Persistence;
using Streamline.Infrastructure.Persistence.Repositories;
using Streamline.Infrastructure.Services;
// For potential mocking if needed
// Added Activities
// Added Events
// Added Gateways
// For ExecutionFlowManager, FlowNodeHandlerFactory
// For specific handlers
// For BpmnXmlService
// For StartProcessInstanceCommand
// Added Xunit

// Added for NullLogger

namespace Streamline.Engine.Tests.Engine;

public class ParallelGatewayHandlerTests : IDisposable
{
    private readonly ServiceProvider _serviceProvider;
    private readonly StreamlineDbContext _dbContext;
    private const string TestProcessDefinitionId = "parallelForkTestProcess";

    // Basic BPMN with Start -> ParallelFork -> (TaskA -> EndA) / (TaskB -> EndB)
    private const string TestBpmnXml = @"<?xml version=""1.0"" encoding=""UTF-8""?>
<bpmn:definitions xmlns:bpmn=""http://www.omg.org/spec/BPMN/20100524/MODEL"" xmlns:bpmndi=""http://www.omg.org/spec/BPMN/20100524/DI"" xmlns:dc=""http://www.omg.org/spec/DD/20100524/DC"" xmlns:di=""http://www.omg.org/spec/DD/20100524/DI"" id=""Definitions_1"" targetNamespace=""http://bpmn.io/schema/bpmn"">
  <bpmn:process id=""Process_ParallelFork"" isExecutable=""true"">
    <bpmn:startEvent id=""StartEvent_1"">
      <bpmn:outgoing>Flow_Start_Fork</bpmn:outgoing>
    </bpmn:startEvent>
    <bpmn:parallelGateway id=""Gateway_Fork"">
      <bpmn:incoming>Flow_Start_Fork</bpmn:incoming>
      <bpmn:outgoing>Flow_Fork_TaskA</bpmn:outgoing>
      <bpmn:outgoing>Flow_Fork_TaskB</bpmn:outgoing>
    </bpmn:parallelGateway>
    <bpmn:sequenceFlow id=""Flow_Start_Fork"" sourceRef=""StartEvent_1"" targetRef=""Gateway_Fork"" />
    <bpmn:task id=""TaskA"" name=""Task A"">
      <bpmn:incoming>Flow_Fork_TaskA</bpmn:incoming>
      <bpmn:outgoing>Flow_TaskA_EndA</bpmn:outgoing>
    </bpmn:task>
    <bpmn:sequenceFlow id=""Flow_Fork_TaskA"" sourceRef=""Gateway_Fork"" targetRef=""TaskA"" />
    <bpmn:task id=""TaskB"" name=""Task B"">
      <bpmn:incoming>Flow_Fork_TaskB</bpmn:incoming>
      <bpmn:outgoing>Flow_TaskB_EndB</bpmn:outgoing>
    </bpmn:task>
    <bpmn:sequenceFlow id=""Flow_Fork_TaskB"" sourceRef=""Gateway_Fork"" targetRef=""TaskB"" />
    <bpmn:endEvent id=""EndA"">
      <bpmn:incoming>Flow_TaskA_EndA</bpmn:incoming>
    </bpmn:endEvent>
    <bpmn:sequenceFlow id=""Flow_TaskA_EndA"" sourceRef=""TaskA"" targetRef=""EndA"" />
    <bpmn:endEvent id=""EndB"">
      <bpmn:incoming>Flow_TaskB_EndB</bpmn:incoming>
    </bpmn:endEvent>
    <bpmn:sequenceFlow id=""Flow_TaskB_EndB"" sourceRef=""TaskB"" targetRef=""EndB"" />
  </bpmn:process>
  <bpmndi:BPMNDiagram id=""BPMNDiagram_1"">
    <bpmndi:BPMNPlane id=""BPMNPlane_1"" bpmnElement=""Process_ParallelFork"">
      </bpmndi:BPMNPlane>
  </bpmndi:BPMNDiagram>
</bpmn:definitions>";

    public ParallelGatewayHandlerTests()
    {
        var services = new ServiceCollection();

        // Configure In-Memory Database
        services.AddDbContext<StreamlineDbContext>(options =>
            options.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()), 
            ServiceLifetime.Singleton); 

        // Register Services 
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        // Use EfRepository<T> which exists in Infrastructure
        services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>)); 

        // Register XML Service
        services.AddSingleton<IBpmnXmlService, BpmnXmlService>();

        // Register Flow Node Handlers
        services.AddScoped<StartEventHandler>(); // Register concrete types for factory resolution
        services.AddScoped<EndEventHandler>();
        services.AddScoped<ParallelGatewayHandler>();
        services.AddScoped<ExclusiveGatewayHandler>(); 
        services.AddScoped<InclusiveGatewayHandler>();
        services.AddScoped<EventBasedGatewayHandler>();
        
        // Register BOTH Script Handlers
        services.AddScoped<CSharpScriptTaskHandler>(); 
        services.AddScoped<JavaScriptTaskHandler>();

        // Register other Task Handlers by concrete type
        services.AddScoped<TaskHandler>(); // Generic Task handler
        services.AddScoped<UserTaskHandler>();
        services.AddScoped<ServiceTaskHandler>();
        // Removed ScriptTaskHandler registration here
        services.AddScoped<BusinessRuleTaskHandler>();
        services.AddScoped<SendTaskHandler>();
        services.AddScoped<ReceiveTaskHandler>();
        services.AddScoped<ManualTaskHandler>();
        
        // Register Event Handlers by concrete type
        services.AddScoped<IntermediateThrowEventHandler>();
        services.AddScoped<IntermediateCatchEventHandler>();
        services.AddScoped<BoundaryEventHandler>();
        
        // Register Handler Factory - It resolves specific handlers above based on type requested
        services.AddScoped<IFlowNodeHandlerFactory, FlowNodeHandlerFactory>();

        // Register Execution Flow Manager
        services.AddScoped<IExecutionFlowManager, ExecutionFlowManager>();

        // Register Logging (Use Null Logger for tests)
        services.AddSingleton(typeof(ILogger<>), typeof(NullLogger<>));
        services.AddSingleton<ILoggerFactory, NullLoggerFactory>();

        // Register MediatR
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<StartProcessInstanceCommand>()
                                      .RegisterServicesFromAssemblyContaining<ExecutionFlowManager>()); 

        _serviceProvider = services.BuildServiceProvider();
        _dbContext = _serviceProvider.GetRequiredService<StreamlineDbContext>();
    }

    [Fact]
    public async Task Fork_Should_Create_Two_New_Executions_At_Tasks()
    {
        // Arrange
        var mediator = _serviceProvider.GetRequiredService<IMediator>();
        // Replace real BpmnXmlService with Mock in DI for better control
        // var bpmnXmlService = _serviceProvider.GetRequiredService<IBpmnXmlService>(); 
        var unitOfWork = _serviceProvider.GetRequiredService<IUnitOfWork>();

        // Test setup assumes StartProcessInstanceCommandHandler can get the definition.
        // To make this robust, we should ideally mock the definition retrieval mechanism.
        // For now, we rely on the command handler's current implementation.

        // Start the process instance
        var startCommand = new StartProcessInstanceCommand(TestProcessDefinitionId, "TestBusinessKey");
        // --- TEMPORARY WORKAROUND for GetProcessDefinitionXmlAsync --- 
        // Create the test file so the handler can find it
        var tempDir = Path.Combine(Path.GetTempPath(), "streamline_test_defs");
        Directory.CreateDirectory(tempDir);
        var tempFilePath = Path.Combine(tempDir, TestProcessDefinitionId + ".bpmn");
        await File.WriteAllTextAsync(tempFilePath, TestBpmnXml); // Write the XML to a temp file
        startCommand = new StartProcessInstanceCommand(tempFilePath, "TestBusinessKey"); // Use file path
        // --- END TEMPORARY WORKAROUND ---

        ProcessInstance initialProcessInstance = null;
        try
        {
            initialProcessInstance = await mediator.Send(startCommand);
        }
        finally
        {
            // Clean up the temporary file
            if (File.Exists(tempFilePath))
            {
                File.Delete(tempFilePath);
            }
            if (Directory.Exists(tempDir) && !Directory.EnumerateFileSystemEntries(tempDir).Any())
            {
                 try { Directory.Delete(tempDir); } catch { /* Ignore cleanup errors */ }
            }
        }
        
        // Assert initial state
        Assert.NotNull(initialProcessInstance);
        // Use DbContext to load related entities if needed
        await _dbContext.Entry(initialProcessInstance).Collection(i => i.Executions).LoadAsync();
        Assert.Single(initialProcessInstance.Executions);
        var initialExecution = initialProcessInstance.Executions.First();
        Assert.Equal("StartEvent_1", initialExecution.CurrentFlowNodeId); 
        Assert.True(initialExecution.IsActive);

        // Trigger continuation
        var executionFlowManager = _serviceProvider.GetRequiredService<IExecutionFlowManager>();
        await executionFlowManager.ContinueExecutionAsync(initialExecution.Id); 

        // Re-fetch data from DbContext
        var finalProcessInstance = await _dbContext.ProcessInstances
            .Include(p => p.Executions)
            .Include(p => p.ActivityInstances)
            .FirstOrDefaultAsync(p => p.Id == initialProcessInstance.Id);

        Assert.NotNull(finalProcessInstance);

        // Verify Executions
        var originalExecution = finalProcessInstance.Executions.FirstOrDefault(e => e.Id == initialExecution.Id);
        Assert.NotNull(originalExecution);
        Assert.False(originalExecution.IsActive, "Original execution should be terminated after fork.");

        var newExecutions = finalProcessInstance.Executions.Where(e => e.Id != initialExecution.Id).ToList();
        Assert.Equal(2, newExecutions.Count);
        Assert.All(newExecutions, e => Assert.True(e.IsActive));
        Assert.Contains(newExecutions, e => e.CurrentFlowNodeId == "TaskA");
        Assert.Contains(newExecutions, e => e.CurrentFlowNodeId == "TaskB");

        // Verify Activity Instances
        var taskAInstance = finalProcessInstance.ActivityInstances
            .FirstOrDefault(ai => ai.FlowNodeId == "TaskA" && ai.State == ActivityInstanceState.Active);
        var taskBInstance = finalProcessInstance.ActivityInstances
            .FirstOrDefault(ai => ai.FlowNodeId == "TaskB" && ai.State == ActivityInstanceState.Active);
        Assert.NotNull(taskAInstance);
        Assert.NotNull(taskBInstance);

        var taskAExecution = newExecutions.First(e => e.CurrentFlowNodeId == "TaskA");
        var taskBExecution = newExecutions.First(e => e.CurrentFlowNodeId == "TaskB");
        Assert.Equal(taskAExecution.Id, taskAInstance.ExecutionId);
        Assert.Equal(taskBExecution.Id, taskBInstance.ExecutionId);
    }

    public void Dispose()
    {
         var dbContext = _serviceProvider.GetService<StreamlineDbContext>();
         dbContext?.Database.EnsureDeleted();
        _serviceProvider?.Dispose();
        GC.SuppressFinalize(this);
    }
} 