using Application.Errors.Objects;
using Application.Interfaces.Repositories;
using Application.Utils;
using Domain.Contracts.JobApplicationUpdate;
using Domain.Models;
using MediatR;

namespace Application.Handlers.JobApplicationUpdates.Create;

public class CreateJobApplicationUpdateHandler : IRequestHandler<CreateJobApplicationUpdateCommand, OneOfHandlerResult<CreateJobApplicationUpdateResult>>
{
    private readonly IJobApplicationRepository _jobApplicationRepository;

    public CreateJobApplicationUpdateHandler(IJobApplicationRepository jobApplicationRepository)
    {
        _jobApplicationRepository = jobApplicationRepository;
    }

    public async Task<OneOfHandlerResult<CreateJobApplicationUpdateResult>> Handle(CreateJobApplicationUpdateCommand request, CancellationToken cancellationToken)
    {
        

        var tryCreateJobApplicationUpdateResult = JobApplication. (contract);
        if (tryCreateJobApplicationUpdateResult.IsError())
        {
            return new CannotCreateJobApplicationError(message: tryCreateJobApplicationUpdateResult.GetError(), path: []);
        }

        await _jobApplicationRepository.CreateAsync(tryCreateJobApplicationUpdateResult.GetValue());
        return new CreateJobApplicationUpdateResult();
    }
}