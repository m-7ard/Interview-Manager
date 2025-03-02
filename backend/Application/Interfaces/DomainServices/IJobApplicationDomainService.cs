using Application.Errors;
using Domain.Contracts.DomainServices.JobApplicationDomainService;
using Domain.Models;
using OneOf;

namespace Application.Interfaces.DomainServices;

public interface IJobApplicationDomainService
{
    public Task<OneOf<bool, ApplicationError>> TryAddUpdate(JobApplication jobApplication, AddJobApplicationUpdateServiceContract contract);
    public Task<OneOf<JobApplication, ApplicationError>> TryGetJobApplicationById(Guid id);
    public Task<OneOf<JobApplication, ApplicationError>> TryCreate(CreateJobApplicationServiceContract contract);
} 