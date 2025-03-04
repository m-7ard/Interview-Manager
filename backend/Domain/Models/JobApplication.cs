using Domain.Abstract;
using Domain.Contracts.Models.JobApplications;
using Domain.Contracts.Models.JobApplicationUpdate;
using Domain.Utils;
using Domain.ValueObjects.Common;
using Domain.ValueObjects.JobApplication;
using Domain.ValueObjects.JobApplicationUpdate;
using OneOf;

namespace Domain.Models;

public class JobApplication : AggregateRoot<JobApplicationId>
{
    public JobApplication(JobApplicationId id, string url, string resume, DateTime dateCreated, List<JobApplicationUpdate> updates, string title, string company, DateTime datePublished) : base(id)
    {
        Id = id;
        Url = url;
        Resume = resume;
        DateCreated = dateCreated;
        Updates = updates;

        Updates.Sort();
        Title = title;
        Company = company;
        DatePublished = datePublished;
    }

    public string Url { get; set; }
    public string Resume { get; set; }
    public DateTime DateCreated { get; set; }
    public List<JobApplicationUpdate> Updates { get; set; }
    public string Title { get; set; }
    public string Company { get; set; }
    public DateTime DatePublished { get; set; }

    private readonly HashSet<JobApplicationUpdateStatus> _allowedDuplicateUpdateStatus = [JobApplicationUpdateStatus.InterviewScheduled]; 

    public static OneOf<bool, string> CanCreate(CreateJobApplicationDomainContract contract)
    {
        var canCreateIdResult = JobApplicationId.CanCreate(contract.Id);
        if (canCreateIdResult.TryPickT1(out var error, out _))
        {
            return error;
        } 

        if (contract.DatePublished > DateTime.UtcNow)
        {
            return $"Date Published ({contract.DatePublished}) cannot be larger than current date.";
        }

        return true;
    }

    public static JobApplication ExecuteCreate(CreateJobApplicationDomainContract contract)
    {
        var canCreateResult = CanCreate(contract);
        if (canCreateResult.TryPickT1(out var error, out _))
        {
            throw new Exception(error);
        }

        var id = JobApplicationId.ExecuteCreate(contract.Id);

        return new JobApplication(
            id: id, 
            url: contract.Url, 
            resume: contract.Resume, 
            dateCreated: DateTime.UtcNow, 
            updates: [], 
            title: contract.Title, 
            company: contract.Company,
            datePublished: contract.DatePublished
        );
    }

    public static IResult<JobApplication> TryCreate(CreateJobApplicationDomainContract contract)
    {
        var canCreateResult = CanCreate(contract);
        if (canCreateResult.TryPickT1(out var error, out _))
        {
            return OneOfResult<JobApplication>.AsError(error);
        }

        var value = ExecuteCreate(contract);
        return OneOfResult<JobApplication>.AsSuccess(value);
    }

    public OneOfDomainResult<bool> CanAddUpdate(CreateJobApplicationUpdateContract contract)
    {
        var canCreateResult = JobApplicationUpdate.CanCreate(contract);
        if (canCreateResult.IsError())
        {
            return canCreateResult.GetError();
        }

        var status = JobApplicationUpdateStatus.ExecuteCreate(contract.Status);
        var duplicateUpdate = Updates.Find(update => _allowedDuplicateUpdateStatus.Contains(update.Status));
        if (duplicateUpdate is not null)
        {
            return $"Job Application cannot have duplicate updates of status \"{status}\"";
        }

        return true;
    }

    public JobApplicationUpdateId ExecuteAddUpdate(CreateJobApplicationUpdateContract contract)
    {
        var canAddResult = CanAddUpdate(contract);
        if (canAddResult.IsError())
        {
            throw new Exception(canAddResult.GetError());
        }

        var jobApplicationUpdate = JobApplicationUpdate.ExecuteCreate(contract);
        return jobApplicationUpdate.Id;
    }

    public JobApplicationUpdate? GetLatestUpdate()
    {
        return Updates.Last();
    }

    public OneOfDomainResult<bool> CanBeScheduledForInterview(InterviewInfo interviewInfo)
    {
        if (interviewInfo.DateScheduled < DatePublished)
        {
            return $"The Interview's Date Scheduled ({interviewInfo.DateScheduled}) cannot be less than the Job Applications Date Published ({DatePublished})";
        }

        return true;
    }
}