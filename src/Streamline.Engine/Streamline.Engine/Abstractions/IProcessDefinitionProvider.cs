using Streamline.Domain.Schema.Events;

// For Definitions

namespace Streamline.Engine.Abstractions;

/// <summary>
/// Interface for retrieving parsed BPMN process definitions.
/// </summary>
public interface IProcessDefinitionProvider
{
    /// <summary>
    /// Gets the parsed BPMN definitions based on an identifier.
    /// </summary>
    /// <param name="definitionId">The identifier (e.g., key, ID, or potentially path for simple cases) of the definition.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The parsed Definitions object.</returns>
    /// <exception cref="DefinitionNotFoundException">Thrown if the definition cannot be found.</exception>
    /// <exception cref="DefinitionParseException">Thrown if the definition cannot be parsed.</exception>
    Task<Definitions> GetDefinitionByIdAsync(string definitionId, CancellationToken cancellationToken = default);
        
    // Potentially add methods for deploying definitions, querying versions etc. later
}

// --- Custom Exceptions (can be in a separate file/folder) ---
public class DefinitionNotFoundException : Exception
{
    public DefinitionNotFoundException(string definitionId) 
        : base($"Process definition '{definitionId}' not found.") { }
    public DefinitionNotFoundException(string definitionId, Exception inner) 
        : base($"Process definition '{definitionId}' not found.", inner) { }
}

public class DefinitionParseException(string definitionId, Exception inner)
    : Exception($"Failed to parse process definition '{definitionId}'.", inner);