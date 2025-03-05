using Domain.Contracts.Models.JobApplications;
using Domain.Models;
using Infrastructure.DbEntities;

namespace Infrastructure.Mappers;

public static class JobApplicationMapper
{
    public static JobApplication DbEntityToDomain(JobApplicationDbEntity source)
    {
        var contract = new CreateJobApplicationDomainContract(
            id: source.Id,
            url: source.Url,
            resume: source.Resume,
            title: source.Title,
            company: source.Company,
            datePublished: source.DatePublished,
            dateCreated: source.DateCreated
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
            company: source.Company,
            datePublished: source.DatePublished
        );
    }
}