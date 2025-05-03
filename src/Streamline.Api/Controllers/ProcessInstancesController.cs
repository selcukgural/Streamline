using MediatR;
using Microsoft.AspNetCore.Mvc;
// using Streamline.Application.Abstractions; // No longer needed directly
using Streamline.Application.Features.ProcessInstances.Commands; // For Command
using Streamline.Application.Features.ProcessInstances.Queries; // For Query
using Streamline.Domain.Runtime;
using System.Net.Mime;

namespace Streamline.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
public class ProcessInstancesController(IMediator mediator, ILogger<ProcessInstancesController> logger)
    : ControllerBase
{
    /// <summary>
    /// Starts a new process instance based on a process definition ID.
    /// </summary>
    /// <param name="startRequest">Request containing the process definition ID and optional business key.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The newly created process instance.</returns>
    /// <response code="201">Returns the newly created process instance.</response>
    /// <response code="400">If the request is invalid or definition not found.</response>
    /// <response code="500">If an internal server error occurs.</response>
    [HttpPost]
    [ProducesResponseType(typeof(ProcessInstance), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> StartProcessInstance([FromBody] StartProcessInstanceRequest startRequest, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(startRequest.ProcessDefinitionId))
        {
            return BadRequest("ProcessDefinitionId cannot be empty.");
        }

        try
        {
            // Create and send the command via MediatR
            var command = new StartProcessInstanceCommand(startRequest.ProcessDefinitionId, startRequest.BusinessKey);
            var newInstance = await mediator.Send(command, cancellationToken);

            logger.LogInformation("Successfully initiated start for process instance {ProcessInstanceId}", newInstance.Id);
                
            // Return 201 Created with the location and the object
            return CreatedAtAction(nameof(GetProcessInstanceById), new { id = newInstance.Id }, newInstance);
        }
        catch (FileNotFoundException ex) // Catch specific exception for definition not found
        {
            logger.LogWarning(ex, "Process definition not found: {ProcessDefinitionId}", startRequest.ProcessDefinitionId);
            return BadRequest($"Process definition '{startRequest.ProcessDefinitionId}' not found.");
        }
        catch (InvalidOperationException ex) // Catch other known operational errors from the engine handler
        {
            logger.LogWarning(ex, "Invalid operation while starting process: {ProcessDefinitionId}", startRequest.ProcessDefinitionId);
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error starting process instance for definition {ProcessDefinitionId}", startRequest.ProcessDefinitionId);
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while starting the process instance.");
        }
    }

    /// <summary>
    /// Gets a process instance by its ID.
    /// </summary>
    /// <param name="id">The ID of the process instance.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The process instance.</returns>
    /// <response code="200">Returns the process instance.</response>
    /// <response code="404">If the process instance is not found.</response>
    /// <response code="500">If an internal server error occurs.</response>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ProcessInstance), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetProcessInstanceById(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            // Create and send the query via MediatR
            var query = new GetProcessInstanceByIdQuery(id);
            var instance = await mediator.Send(query, cancellationToken);

            if (instance == null)
            {
                return NotFound();
            }
                
            // TODO: Consider returning a DTO instead of the full domain entity.
            return Ok(instance);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error retrieving process instance {ProcessInstanceId}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving the process instance.");
        }
    }
        
    // TODO: Add endpoints for querying instances, terminating, suspending, etc.
}

/// <summary>
/// Request body for starting a process instance.
/// </summary>
public record StartProcessInstanceRequest(
    string ProcessDefinitionId,
    string? BusinessKey
);