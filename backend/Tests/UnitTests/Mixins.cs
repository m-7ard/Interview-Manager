using Domain.Contracts.Models.JobApplication;
using Domain.Models;

namespace Tests.UnitTests;

public class Mixins
{
    public static JobApplication CreateJobApplication(int seed)
    {
        return JobApplication.ExecuteCreate(new CreateJobApplicationDomainContract(id: Guid.NewGuid(), url: $"{seed}.com", resume: $"resume_{seed}", dateCreated: DateTime.UtcNow, title: $"title_{seed}", company: $"company_{seed}"));
    }
}