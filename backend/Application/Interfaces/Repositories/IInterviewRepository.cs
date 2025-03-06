using Domain.Models;
using Domain.ValueObjects.Interviews;

namespace Application.Interfaces.Repositories;

public interface IInterviewRepository
{
    Task CreateAsync(Interview interview);
    Task<Interview?> GetByIdAsync(InterviewId id);
    Task UpdateAsync(Interview interview);
    Task DeleteAsync(Interview interview);
}