using Domain.Contracts.JobApplication;
using Domain.Models;
using Infrastructure.DbEntities;

namespace Infrastructure.Mappers;

public static class JobApplicationMapper
{
    public static JobApplication DbEntityToDomain(JobApplicationDbEntity source)
    {
        var contract = new CreateJobApplicationContract(
            id: source.Id,
            url: source.Url,
            resume: source.Resume,
            dateCreated: source.DateCreated,
            title: source.Title,
            company: source.Company
        );

        return JobApplication.ExecuteCreate(contract);
    }

    public static JobApplicationDbEntity DomainToDbEntity(JobApplication source)
    {
        return new JobApplicationDbEntity(
            id: source.Id.Value,
            url: source.Url,
            resume: source.Resume,
            dateCreated: source.DateCreated,
            updates: source.Updates.Select(JobApplicationUpdateMapper.DomainToDbEntity).ToList(),
            title: source.Title,
            company: source.Company
        );
    }
}