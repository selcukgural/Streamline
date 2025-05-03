using Hangfire;
using Streamline.Domain.Abstractions;
using Streamline.Application.Services; // For TimerJobTriggerService

namespace Streamline.Infrastructure.Scheduling; // Or other appropriate Infrastructure namespace

/// <summary>
/// Hangfire implementation for scheduling timer jobs.
/// </summary>
public class HangfireTimerJobScheduler(IBackgroundJobClient backgroundJobClient) : ITimerJobScheduler
{
    public Task<string> ScheduleTimerJobAsync(Guid eventSubscriptionId, DateTime dueTime)
    {
        // Schedule the job to call the TriggerTimerFiredNotificationAsync method 
        // of the TimerJobTriggerService when the dueTime is reached.
        // Hangfire's DI integration will resolve TimerJobTriggerService when the job runs.
        var jobId = backgroundJobClient.Schedule<TimerJobTriggerService>(
            service => service.TriggerTimerFiredNotificationAsync(eventSubscriptionId), 
            dueTime.ToUniversalTime()); // Ensure dueTime is UTC
        
        // Return the Hangfire Job ID
        return Task.FromResult(jobId);
    }

    public Task DeleteTimerJobAsync(string jobId)
    {
        // Delete the scheduled job using its ID
        backgroundJobClient.Delete(jobId);
        return Task.CompletedTask;
    }
} 