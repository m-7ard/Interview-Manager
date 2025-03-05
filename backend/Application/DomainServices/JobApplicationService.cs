using Application.Errors;
using Application.Errors.Objects.Application.JobApplications;
using Application.Errors.Objects.Domains.Interviews;
using Application.Errors.Objects.Domains.JobApplications.ValueObjects;
using Application.Interfaces.DomainServices;
using Application.Interfaces.Persistence;
using Domain.Contracts.DomainServices.JobApplicationDomainService;
using Domain.Contracts.Models.JobApplications;
using Domain.Contracts.Models.JobApplicationUpdate;
using Domain.Models;
using Domain.ValueObjects.JobApplication;
using OneOf;

namespace Application.DomainServices;

public class JobApplicationService : IJobApplicationDomainService
{
    private readonly IUnitOfWork _unitOfWork;

    public JobApplicationService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<OneOf<bool, ApplicationError>> TryAddUpdate(JobApplication jobApplication, AddJobApplicationUpdateServiceContract contract)
    {
        var canAdd = jobApplication.CanAddUpdate(new CreateJobApplicationUpdateContract(id: contract.Id, status: contract.Status, dateOccured: contract.DateOccured));
        if (canAdd.IsError()) return new CannotAddJobApplicationUpdateError(message: canAdd.GetError(), path: []);

        await _unitOfWork.JobApplicationRepository.UpdateAsync(jobApplication);
        return true;
    }

    public async Task<OneOf<JobApplication, ApplicationError>> TryGetJobApplicationById(Guid id)
    {
        var canCreateId = JobApplicationId.CanCreate(id);
        if (canCreateId.IsT1) return new CannotCreateJobApplicationIdError(message: canCreateId.AsT1, path: []);

        var idObj = JobApplicationId.ExecuteCreate(id);

        var jobApplication = await _unitOfWork.JobApplicationRepository.GetByIdAsync(idObj);
        if (jobApplication is null) return new JobApplicationDoesNotExist(message: $"Job Application of Id \"{id}\" does not exist");

        return jobApplication;
    }

    public async Task<OneOf<JobApplication, ApplicationError>> TryCreate(CreateJobApplicationServiceContract contract)
    {
        // Can Create
        var domainContract = new CreateJobApplicationDomainContract(
            id: contract.Id,
            url: contract.Url,
            resume: contract.Resume,
            datePublished: contract.DatePublished,
            title: contract.Title,
            company: contract.Company,
            dateCreated: contract.DatePublished
        );

        var tryCreateJobApplicationResult = JobApplication.TryCreate(domainContract);
        if (tryCreateJobApplicationResult.IsError()) return new CannotCreateJobApplicationError(message: tryCreateJobApplicationResult.GetError(), path: []);

        var jobApplication = tryCreateJobApplicationResult.GetValue();

        // Persist
        await _unitOfWork.JobApplicationRepository.CreateAsync(jobApplication);

        return jobApplication;
    }
}