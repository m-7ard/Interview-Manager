using Application.Interfaces.Persistence;
using Application.Interfaces.Repositories;

namespace Infrastructure.Persistence;

public class UnitOfWork : IUnitOfWork
{
    public IJobApplicationRepository JobApplicationRepository { get; }
    private readonly MainDbContext _dbContext;

    public UnitOfWork(IJobApplicationRepository jobApplicationRepository, MainDbContext dbContext)
    {
        JobApplicationRepository = jobApplicationRepository;
        _dbContext = dbContext;
    }

    public async Task SaveAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}