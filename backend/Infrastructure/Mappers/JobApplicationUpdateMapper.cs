using Domain.Contracts.JobApplicationUpdate;
using Domain.Models;
using Infrastructure.DbEntities;

namespace Infrastructure.Mappers;

public static class JobApplicationUpdateMapper
{
    public static JobApplicationUpdate DbEntityToDomain(JobApplicationUpdateDbEntity source)
    {
        var contract = new CreateJobApplicationUpdateContract(
            id: source.Id,
            status: source.Status,
            dateOccured: source.DateOccured
        );

        return JobApplicationUpdate.ExecuteCreate(contract);
    }

    public static JobApplicationUpdateDbEntity DomainToDbEntity(JobApplicationUpdate source)
    {
        return new JobApplicationUpdateDbEntity(
            id: source.Id.Value,
            status: source.Status.Value,
            dateOccured: source.DateOccured
        );
    }
}