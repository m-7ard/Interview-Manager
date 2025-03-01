using Domain.Abstract;
using Domain.ValueObjects.JobApplicationUpdate;
using Domain.Utils;
using Domain.Contracts.Models.JobApplicationUpdate;

namespace Domain.Models;

public class JobApplicationUpdate : DomainEntity<JobApplicationUpdateId>
{
    public JobApplicationUpdate(JobApplicationUpdateId id, JobApplicationUpdateStatus status, DateTime dateOccured) : base(id)
    {
        Id = id;
        Status = status;
        DateOccured = dateOccured;
    }

    public JobApplicationUpdateStatus Status { get; set; }
    public DateTime DateOccured { get; set; }

    public static OneOfDomainResult<bool> CanCreate(CreateJobApplicationUpdateContract contract)
    {
        var canCreateIdResult = JobApplicationUpdateId.CanCreate(contract.Id);
        if (canCreateIdResult.TryPickT1(out var error, out _))
        {
            return error;
        } 

        var canCreateStatus = JobApplicationUpdateStatus.CanCreate(contract.Status);
        if (canCreateStatus.TryPickT1(out error, out _))
        {
            return error;
        } 
        
        if (contract.DateOccured > DateTime.UtcNow)
        {
            return $"Date Occured ({contract.DateOccured}) cannot be larger than current date.";
        }

        return true;
    }

    public static JobApplicationUpdate ExecuteCreate(CreateJobApplicationUpdateContract contract)
    {
        var canCreateResult = CanCreate(contract);
        if (canCreateResult.IsError())
        {
            throw new Exception(canCreateResult.GetError());
        }

        var id = JobApplicationUpdateId.ExecuteCreate(contract.Id);
        var status = JobApplicationUpdateStatus.ExecuteCreate(contract.Status);

        return new JobApplicationUpdate(id: id, status: status, dateOccured: contract.DateOccured);
    }
}