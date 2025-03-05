using Application.Errors.Objects.Application.Interviews;
using Application.Errors.Objects.Application.JobApplications;
using Application.Interfaces.DomainServices;
using Application.Interfaces.Persistence;
using Application.Utils;
using Domain.Contracts.DomainServices.InterviewDomainService;
using MediatR;

namespace Application.Handlers.Interviews.Create;

public class CreateInterviewHandler : IRequestHandler<CreateInterviewCommand, OneOfHandlerResult<CreateInterviewResult>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IInterviewDomainService _interviewDomainService;
    private readonly IJobApplicationDomainService _jobApplicationDomainService;

    public CreateInterviewHandler(IUnitOfWork unitOfWork, IInterviewDomainService interviewDomainService, IJobApplicationDomainService jobApplicationDomainService)
    {
        _unitOfWork = unitOfWork;
        _interviewDomainService = interviewDomainService;
        _jobApplicationDomainService = jobApplicationDomainService;
    }

    public async Task<OneOfHandlerResult<CreateInterviewResult>> Handle(CreateInterviewCommand request, CancellationToken cancellationToken)
    {
        // Job Application Exists
        var doesExist = await _jobApplicationDomainService.TryGetJobApplicationById(request.JobApplicationId);
        if (doesExist.IsT1) return new JobApplicationDoesNotExist(message: doesExist.AsT1.Message).AsList();

        var jobApplication = doesExist.AsT0;

        // Try Create
        var tryCreate = await _interviewDomainService.TryCreateInterview(jobApplication: jobApplication, new CreateInterviewServiceContract(id: request.Id, venue: request.Venue, status: request.Status, dateScheduled: request.DateScheduled, dateStarted: request.DateScheduled, dateFinished: request.DateFinished, interviewer: request.Interviewer));
        if (tryCreate.IsT1) return new CannotCreateInterviewError(message: tryCreate.AsT1.Message).AsList();

        // Persist
        await _unitOfWork.SaveAsync();

        return new CreateInterviewResult();
    }
}