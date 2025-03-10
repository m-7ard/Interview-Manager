using Application.Interfaces.Repositories;

namespace Application.Interfaces.Persistence;

public interface IUnitOfWork
{
    public IJobApplicationRepository JobApplicationRepository { get; }
    public IInterviewRepository InterviewRepository { get; }
    public Task SaveAsync();
}
