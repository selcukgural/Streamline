using MediatR;
using Streamline.Domain.Abstractions;
using Streamline.Domain.Runtime;

namespace Streamline.Application.Features.ProcessInstances.Queries;

public class GetProcessInstanceByIdQueryHandler(IRepository<ProcessInstance> processInstanceRepository)
    : IRequestHandler<GetProcessInstanceByIdQuery, ProcessInstance?>
{
    // Alternatively, inject IUnitOfWork if you need other repositories or SaveChanges within the handler (less common for queries)

    public async Task<ProcessInstance?> Handle(GetProcessInstanceByIdQuery request, CancellationToken cancellationToken)
    {
        // TODO: Add logic to include related entities (Executions, Variables etc.) if needed using Include/ThenInclude
        // Example: return await _processInstanceRepository.GetByIdAsync(request.Id, cancellationToken, pi => pi.Executions, pi => pi.Variables);
        // This would require extending IRepository or using specification pattern.
            
        var instance = await processInstanceRepository.GetByIdAsync(request.Id, cancellationToken);
        return instance;
    }
}