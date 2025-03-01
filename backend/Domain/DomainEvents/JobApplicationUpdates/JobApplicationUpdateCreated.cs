using Domain.Models;

namespace Domain.DomainEvents.JobApplicationUpdates;

public class JobApplicationUpdateCreatedEvent : DomainEvent
{
    public JobApplicationUpdateCreatedEvent(JobApplicationUpdate payload) : base()
    {
        Payload = payload;
    }

    public JobApplicationUpdate Payload { get; }
    public override string EventType => "JOB_APPLICATION_CREATED";
}