using Application.Utils;
using Domain.Contracts.DomainServices.JobApplicationDomainService;
using Domain.Models;
using Domain.Utils;

namespace Application.Interfaces.DomainServices;

public interface IJobApplicationService
{
    public Task<IApplicationResult<bool>> TryAddUpdate(JobApplication jobApplication, CreateJobApplicationUpdateServiceContract contract);
} 