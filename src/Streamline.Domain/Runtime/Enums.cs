namespace Streamline.Domain.Runtime;

public enum ProcessInstanceState
{
    Running,
    Suspended,
    Completed,
    Aborted, // Or Failed/Error
    Terminated
}

public enum ActivityInstanceState
{
    Active,     // Node is currently executing or waiting
    Completed,  // Node execution finished successfully
    Faulted,    // An error occurred during execution
    Terminated, // Execution was forcefully terminated (e.g., by terminating end event or boundary event)
    Cancelled   // Execution was cancelled (e.g., compensation)
}

// Add other enums as needed, e.g., VariableType