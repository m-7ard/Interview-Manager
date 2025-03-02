using Application.Interfaces.Repositories;
using Domain.DomainEvents.JobApplicationUpdates;
using Domain.Models;
using Domain.ValueObjects.JobApplication;
using Infrastructure.Mappers;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class JobApplicationRepository : IJobApplicationRepository
{
    private readonly MainDbContext _dbContext;

    private async Task PersistDomainEvents(JobApplication jobApplication)
    {
        foreach (var domainEvent in jobApplication.DomainEvents)
        {
            if (domainEvent is JobApplicationUpdateCreatedEvent createEvent)
            {
                var writeEntity = JobApplicationUpdateMapper.DomainToDbEntity(createEvent.Payload);
                _dbContext.Add(writeEntity);
            }
        }

        jobApplication.ClearDomainEvents();
    } 

    public JobApplicationRepository(MainDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task CreateAsync(JobApplication jobApplication)
    {
        var dbEntity = JobApplicationMapper.DomainToDbEntity(jobApplication);
        _dbContext.Add(dbEntity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(JobApplication jobApplication)
    {
        var writeEntity = JobApplicationMapper.DomainToDbEntity(jobApplication);
        var trackedEntity = await _dbContext.JobApplications.SingleAsync(d => d.Id == writeEntity.Id);
        _dbContext.Entry(trackedEntity).CurrentValues.SetValues(writeEntity);
        await PersistDomainEvents(jobApplication);
    }

    public async Task<JobApplication?> GetByIdAsync(JobApplicationId id)
    {
        var dbEntity = await _dbContext.JobApplications.SingleOrDefaultAsync(d => d.Id == id.Value);
        return dbEntity is null ? null : JobApplicationMapper.DbEntityToDomain(dbEntity);
    }
}