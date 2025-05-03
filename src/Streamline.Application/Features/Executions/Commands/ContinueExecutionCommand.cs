using MediatR;

namespace Streamline.Application.Features.Executions.Commands;

/// <summary>
/// Command to signal that an execution should continue from its current flow node.
/// The handler will determine the next step based on the process definition.
/// </summary>
public record ContinueExecutionCommand(
    Guid ExecutionId
) : IRequest<Unit>; // Implement IRequest<Unit> for commands handled by IRequestHandler<..., Unit>