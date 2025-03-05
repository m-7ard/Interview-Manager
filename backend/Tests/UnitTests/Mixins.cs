using Domain.Contracts.Models.Interview;
using Domain.Contracts.Models.JobApplications;
using Domain.Models;
using Domain.ValueObjects.Interviews;

namespace Tests.UnitTests;

public class Mixins
{
    public static JobApplication CreateJobApplication(int seed)
    {
        return JobApplication.ExecuteCreate(new CreateJobApplicationDomainContract(id: Guid.NewGuid(), url: $"{seed}.com", resume: $"resume_{seed}", title: $"title_{seed}", company: $"company_{seed}", datePublished: DateTime.UtcNow, dateCreated: DateTime.UtcNow));
    }

    public static Interview CreateInterview(int seed, JobApplication jobApplication)
    {
        return Interview.ExecuteCreate(new CreateInterviewContract(id: Guid.NewGuid(), venue: $"venue_{seed}", status: InterviewStatus.Scheduled.Value, dateScheduled: DateTime.UtcNow, dateStarted: null, dateFinished: null, interviewer: $"interview_{seed}", jobApplicationId: jobApplication.Id));
    }
}