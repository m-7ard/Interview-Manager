using Application.Errors.Objects.Domains.JobApplications;
using Application.Interfaces.DomainServices;
using Application.Interfaces.Persistence;
using Application.Interfaces.Repositories;
using Application.Utils;
using Domain.Contracts.Models.JobApplication;
using Domain.Models;
using MediatR;

namespace Application.Handlers.JobApplications.Create;

public class CreateJobApplicationHandler : IRequestHandler<CreateJobApplicationCommand, OneOfHandlerResult<CreateJobApplicationResult>>
{
    private readonly IJobApplicationDomainService _jobApplicationDomainService;
    private readonly IUnitOfWork _unitOfWork;

    public CreateJobApplicationHandler(IJobApplicationDomainService jobApplicationDomainService, IUnitOfWork unitOfWork)
    {
        _jobApplicationDomainService = jobApplicationDomainService;
        _unitOfWork = unitOfWork;
    }

    public async Task<OneOfHandlerResult<CreateJobApplicationResult>> Handle(CreateJobApplicationCommand request, CancellationToken cancellationToken)
    {
        // Can Create

        _jobApplicationDomainService.

        await _jobApplicationRepository.CreateAsync(tryCreateJobApplicationResult.GetValue());
        await _un

        return new CreateJobApplicationResult();
    }
}