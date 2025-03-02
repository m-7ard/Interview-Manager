using Domain.Models;
using Domain.ValueObjects.JobApplication;

namespace Application.Interfaces.Repositories;

public interface IJobApplicationRepository
{
    Task CreateAsync(JobApplication jobApplication);
    Task UpdateAsync(JobApplication jobApplication);
    Task<JobApplication?> GetByIdAsync(JobApplicationId id);
}