using MediatR;
using Microsoft.Extensions.Logging;
// using Streamline.Application.Abstractions; // Removed old using
using Streamline.Domain.Abstractions; // For IUnitOfWork, IExecutionFlowManager
// Removed unused using statements for schema etc.

namespace Streamline.Application.Features.Executions.Commands;

// Implement IRequestHandler<..., Unit> for commands that don't return a value
public class ContinueExecutionCommandHandler(
    IUnitOfWork unitOfWork,
    IExecutionFlowManager executionFlowManager, // Interface now from Domain
    ILogger<ContinueExecutionCommandHandler> logger)
    : IRequestHandler<ContinueExecutionCommand, Unit>
{
    // Interface now from Domain
    // Assign manager

    // Removed IBpmnXmlService and TODO comment

    // Removed IBpmnXmlService assignment

    // Return Task<Unit>
    public async Task<Unit> Handle(ContinueExecutionCommand request, CancellationToken cancellationToken)
    {
        logger.LogDebug("Handling ContinueExecutionCommand for Execution {ExecutionId}, delegating to ExecutionFlowManager.", request.ExecutionId);

        try
        {
            // Delegate the core logic to the flow manager
            await executionFlowManager.ContinueExecutionAsync(request.ExecutionId, cancellationToken);

            // Save changes made by the flow manager (and any other operations within this unit of work)
            await unitOfWork.SaveChangesAsync(cancellationToken);

            logger.LogDebug("State saved successfully after ExecutionFlowManager completed for Execution {ExecutionId}", request.ExecutionId);
        }
        catch (Exception ex)
        {
            // Log any exceptions bubbled up from the manager
            logger.LogError(ex, "Error occurred during ContinueExecution for Execution {ExecutionId}", request.ExecutionId);
            // Decide on error handling - rethrow, return failure, etc.
            // Re-throwing for now to let higher layers know
            throw; 
        }

        return Unit.Value;
    }
}