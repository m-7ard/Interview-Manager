using Domain.Models;

namespace Application.Interfaces.Repositories;

public interface IJobApplicationRepository
{
    Task CreateAsync(JobApplication jobApplication);
    Task UpdateAsync(JobApplication jobApplication);
}