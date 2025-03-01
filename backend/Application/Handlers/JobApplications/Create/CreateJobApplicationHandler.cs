using Application.Errors.Objects;
using Application.Interfaces.Repositories;
using Application.Utils;
using Domain.Contracts.JobApplication;
using Domain.Models;
using MediatR;

namespace Application.Handlers.JobApplications.Create;

public class CreateJobApplicationHandler : IRequestHandler<CreateJobApplicationCommand, OneOfHandlerResult<CreateJobApplicationResult>>
{
    private readonly IJobApplicationRepository _jobApplicationRepository;

    public CreateJobApplicationHandler(IJobApplicationRepository jobApplicationRepository)
    {
        _jobApplicationRepository = jobApplicationRepository;
    }

    public async Task<OneOfHandlerResult<CreateJobApplicationResult>> Handle(CreateJobApplicationCommand request, CancellationToken cancellationToken)
    {
        var contract = new CreateJobApplicationContract(
            id: request.Id,
            url: request.Url,
            resume: request.Resume,
            dateCreated: request.DateCreated,
            title: request.Title,
            company: request.Company
        );

        var tryCreateJobApplicationResult = JobApplication.TryCreate(contract);
        if (tryCreateJobApplicationResult.IsError())
        {
            return new CannotCreateJobApplicationError(message: tryCreateJobApplicationResult.GetError(), path: []);
        }

        await _jobApplicationRepository.CreateAsync(tryCreateJobApplicationResult.GetValue());
        return new CreateJobApplicationResult();
    }
}