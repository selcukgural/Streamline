namespace Streamline.Domain.Abstractions;

/// <summary>
/// Defines the contract for scheduling timer jobs related to event subscriptions.
/// This abstraction belongs to the Domain as scheduling timers is a core engine concept interaction.
/// </summary>
public interface ITimerJobScheduler
{
    /// <summary>
    /// Schedules a job to fire a notification for the specified event subscription at the given due time.
    /// </summary>
    /// <param name="eventSubscriptionId">The ID of the event subscription to trigger.</param>
    /// <param name="dueTime">The UTC time when the timer should fire.</param>
    /// <returns>The ID of the scheduled job (e.g., Hangfire Job ID).</returns>
    Task<string> ScheduleTimerJobAsync(Guid eventSubscriptionId, DateTime dueTime);

    /// <summary>
    /// Deletes a previously scheduled timer job.
    /// </summary>
    /// <param name="jobId">The ID of the job to delete.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task DeleteTimerJobAsync(string jobId); 
} 