using MediatR;
using Streamline.Domain.Runtime;

namespace Streamline.Application.Features.ProcessInstances.Queries;

/// <summary>
/// Query to retrieve a process instance by its ID.
/// </summary>
public record GetProcessInstanceByIdQuery(
    Guid Id
) : IRequest<ProcessInstance?>; // Returns nullable ProcessInstance