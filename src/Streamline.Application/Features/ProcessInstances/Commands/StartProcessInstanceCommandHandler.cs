using MediatR;
using Microsoft.Extensions.Logging;
using Streamline.Application.Features.Executions.Commands;
using Streamline.Domain.Abstractions; // Added for ContinueExecutionCommand and IBpmnXmlService
using Streamline.Domain.Runtime;
using Streamline.Domain.Schema.Common;
using Streamline.Domain.Schema.Events;
using Streamline.Engine.Abstractions; // Removed erroneous comment

// Schema using from Domain

namespace Streamline.Application.Features.ProcessInstances.Commands;

public class StartProcessInstanceCommandHandler(
    IUnitOfWork unitOfWork,
    IBpmnXmlService bpmnXmlService,
    IMediator mediator, // Added Mediator
    ILogger<StartProcessInstanceCommandHandler> logger)
    : IRequestHandler<StartProcessInstanceCommand, ProcessInstance>
{
    // Replace later

    // Added Mediator
    // Added Mediator

    // TODO: Inject other dependencies like BPMN definition provider

    public async Task<ProcessInstance> Handle(StartProcessInstanceCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Handling StartProcessInstanceCommand for definition ID: {ProcessDefinitionId}", request.ProcessDefinitionId);

        // --- Step 1: BPMN Süreç Tanımını Yorumlama (Basit Hali) ---
        Definitions definitions;
        try
        {
            string xmlContent = await GetProcessDefinitionXmlAsync(request.ProcessDefinitionId, cancellationToken);
            definitions = bpmnXmlService.Import(xmlContent);
            logger.LogDebug("Successfully parsed process definition.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to load or parse process definition: {ProcessDefinitionId}", request.ProcessDefinitionId);
            throw new InvalidOperationException($"Failed to load or parse process definition '{request.ProcessDefinitionId}'.", ex);
        }

        var process = definitions.RootElement.OfType<Process>().FirstOrDefault();
        if (process == null)
        {
            throw new InvalidOperationException($"No process element found in definition '{request.ProcessDefinitionId}'.");
        }

        var startEvent = process.FlowElement?.OfType<StartEvent>().FirstOrDefault();
        if (startEvent == null)
        {
            throw new InvalidOperationException($"No suitable start event found in process '{(string.IsNullOrEmpty(process.Id) ? process.Name : process.Id)}'.");
        }
        logger.LogDebug("Found start event {StartEventId}", startEvent.Id);

        // --- Step 2 & 4: Yürütme ve Durum Yönetimi (Başlangıç) ---
        var processInstanceRepo = unitOfWork.GetRepository<ProcessInstance>();
        var activityInstanceRepo = unitOfWork.GetRepository<ActivityInstance>();
            
        var newInstance = new ProcessInstance(request.ProcessDefinitionId, request.BusinessKey)
        {
            ProcessDefinitionId = request.ProcessDefinitionId 
        };
        var initialExecution = newInstance.Executions.First();

        // --- Step 3: Akış Elemanı Davranışını Tetikleme (Başlangıç Olayı) ---
        initialExecution.EnterFlowNode(startEvent.Id);
        var startEventActivity = new ActivityInstance(newInstance.Id, startEvent.Id, initialExecution.Id, startEvent.Name ?? "Start Event")
        {
            ProcessInstance = newInstance, 
            FlowNodeId = startEvent.Id 
        };
        await activityInstanceRepo.AddAsync(startEventActivity, cancellationToken);
        await processInstanceRepo.AddAsync(newInstance, cancellationToken);
        startEventActivity.Complete();

        logger.LogInformation("Started process instance {ProcessInstanceId} and positioned execution {ExecutionId} at start event {StartEventId}", newInstance.Id, initialExecution.Id, startEvent.Id);

        await unitOfWork.SaveChangesAsync(cancellationToken);
        logger.LogDebug("Initial state saved for process instance {ProcessInstanceId}", newInstance.Id);

        // --- Trigger Continuation via MediatR ---
        logger.LogDebug("Sending ContinueExecutionCommand for execution {ExecutionId}", initialExecution.Id);
        await mediator.Send(new ContinueExecutionCommand(initialExecution.Id), cancellationToken);

        return newInstance;
    }
        
    private Task<string> GetProcessDefinitionXmlAsync(string processDefinitionIdOrPath, CancellationToken cancellationToken) {
        if (File.Exists(processDefinitionIdOrPath)) {
            return File.ReadAllTextAsync(processDefinitionIdOrPath, cancellationToken);
        }
        throw new FileNotFoundException("Process definition XML not found.", processDefinitionIdOrPath);
    }
}