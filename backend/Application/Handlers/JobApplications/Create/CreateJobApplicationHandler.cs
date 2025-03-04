using Application.Errors.Objects.Services.JobApplicationDomainService;
using Application.Interfaces.DomainServices;
using Application.Interfaces.Persistence;
using Application.Utils;
using Domain.Contracts.DomainServices.JobApplicationDomainService;
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
        // Try Create
        var tryCreate = await _jobApplicationDomainService.TryCreate(new CreateJobApplicationServiceContract(id: request.Id, url: request.Url, resume: request.Resume, datePublished: request.DatePublished, title: request.Title, company: request.Company));
        if (tryCreate.IsT1) return new CannotCreateJobApplicationServiceError(message: tryCreate.AsT1.Message).AsList();

        // Persist
        await _unitOfWork.SaveAsync();

        return new CreateJobApplicationResult();
    }
}