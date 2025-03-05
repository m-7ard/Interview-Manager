using Application.Interfaces.Persistence;
using Application.Interfaces.Repositories;

namespace Infrastructure.Persistence;

public class UnitOfWork : IUnitOfWork
{
    public IJobApplicationRepository JobApplicationRepository { get; }
    public IInterviewRepository InterviewRepository { get; }
    private readonly MainDbContext _dbContext;

    public UnitOfWork(IJobApplicationRepository jobApplicationRepository, MainDbContext dbContext, IInterviewRepository interviewRepository)
    {
        JobApplicationRepository = jobApplicationRepository;
        _dbContext = dbContext;
        InterviewRepository = interviewRepository;
    }

    public async Task SaveAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}