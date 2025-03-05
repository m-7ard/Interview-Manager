using Application.Errors.Objects.Application.JobApplications;
using Application.Interfaces.DomainServices;
using Application.Interfaces.Persistence;
using Application.Utils;
using Domain.Contracts.DomainServices.JobApplicationDomainService;
using MediatR;

namespace Application.Handlers.JobApplicationUpdates.Add;

public class AddJobApplicationUpdateHandler : IRequestHandler<AddJobApplicationUpdateCommand, OneOfHandlerResult<AddJobApplicationUpdateResult>>
{
    private readonly IJobApplicationDomainService _jobApplicationDomainService;
    private readonly IUnitOfWork _unitOfWork;

    public AddJobApplicationUpdateHandler(IJobApplicationDomainService jobApplicationDomainService, IUnitOfWork unitOfWork)
    {
        _jobApplicationDomainService = jobApplicationDomainService;
        _unitOfWork = unitOfWork;
    }

    public async Task<OneOfHandlerResult<AddJobApplicationUpdateResult>> Handle(AddJobApplicationUpdateCommand request, CancellationToken cancellationToken)
    {
        // Exists
        var jobApplicationExists = await _jobApplicationDomainService.TryGetJobApplicationById(request.JobApplicationId);
        if (jobApplicationExists.IsT1) return new JobApplicationDoesNotExist(message: jobApplicationExists.AsT1.Message).AsList();

        var jobApplication = jobApplicationExists.AsT0;

        // Add
        var tryAdd = await _jobApplicationDomainService.TryAddUpdate(jobApplication, new AddJobApplicationUpdateServiceContract(id: request.JobApplicationUpdateId, status: request.Status, dateOccured: request.DateOccured));
        if (tryAdd.IsT1) return new CannotAddJobApplicationUpdateError(message: tryAdd.AsT1.Message).AsList();

        // Persist
        await _unitOfWork.JobApplicationRepository.UpdateAsync(jobApplication);
        await _unitOfWork.SaveAsync();

        return new AddJobApplicationUpdateResult();
    }
}