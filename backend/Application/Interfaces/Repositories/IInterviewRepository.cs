using Domain.Models;
using Domain.ValueObjects.Interviews;
using Domain.ValueObjects.JobApplication;

namespace Application.Interfaces.Repositories;

public interface IInterviewRepository
{
    Task CreateAsync(Interview interview);
    Task<Interview?> GetByIdAsync(InterviewId id);
    Task UpdateAsync(Interview interview);
}