namespace Streamline.Domain.Runtime;

/// <summary>
/// Represents a variable associated with a process instance.
/// </summary>
public class Variable : EntityBase
{
    /// <summary>
    /// The name of the variable.
    /// </summary>
    public required string Name { get; init; }

    /// <summary>
    /// The data type of the variable (e.g., "string", "integer", "boolean", "json").
    /// </summary>
    public required string Type { get; init; }

    /// <summary>
    /// The value of the variable, potentially stored as a string or serialized object.
    /// </summary>
    public string? Value { get; set; } // Value can change

    /// <summary>
    /// Optional: ID of the Execution this variable is local to.
    /// Null if this is a process instance level variable.
    /// </summary>
    public Guid? ExecutionId { get; private set; }
    // No direct navigation property back to Execution needed by default

    /// <summary>
    /// Optional: ID of the Process Instance this variable belongs to.
    /// Might be redundant if ExecutionId is set, but useful for process-level variables.
    /// </summary>
    public Guid? ProcessInstanceId { get; private set; }
    // No direct navigation property back to ProcessInstance needed by default

    // Private constructor for EF Core
    private Variable()
    {
        // Name = null!; // Initialization no longer needed with init
        // Type = null!; 
    }

    // Public constructor
    public Variable(string name, string type, string? value, Guid? executionId = null, Guid? processInstanceId = null)
    {
        // Basic validation
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Variable name cannot be empty.", nameof(name));
        if (string.IsNullOrWhiteSpace(type)) throw new ArgumentException("Variable type cannot be empty.", nameof(type));
        if (executionId == null && processInstanceId == null)
        {
            throw new ArgumentException("A variable must belong to either an execution or a process instance.");
        }

        Name = name;
        Type = type;
        Value = value;
        ExecutionId = executionId;
        ProcessInstanceId = processInstanceId;
    }
}