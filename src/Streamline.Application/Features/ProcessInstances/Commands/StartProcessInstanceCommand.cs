using MediatR;
using Streamline.Domain.Runtime;

namespace Streamline.Application.Features.ProcessInstances.Commands;

/// <summary>
/// Command to start a new process instance.
/// </summary>
public record StartProcessInstanceCommand(
    string ProcessDefinitionId, 
    string? BusinessKey
    // Dictionary<string, object>? Variables = null // Add later if needed
) : IRequest<ProcessInstance>; // Returns the created ProcessInstance