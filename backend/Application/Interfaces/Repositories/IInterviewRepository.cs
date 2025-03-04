using Domain.Models;
using Domain.ValueObjects.JobApplication;

namespace Application.Interfaces.Repositories;

public interface IInterviewRepository
{
    Task CreateAsync(Interview interview);
}