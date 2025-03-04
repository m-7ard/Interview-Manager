using Application.Errors.Objects.Domains.Interviews;
using Application.Errors.Objects.Services.JobApplicationDomainService;
using Application.Interfaces.DomainServices;
using Application.Interfaces.Persistence;
using Application.Utils;
using Domain.Contracts.DomainServices.InterviewDomainService;
using Domain.Contracts.DomainServices.JobApplicationDomainService;
using Domain.ValueObjects.Common;
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
        if (doesExist.IsT1) return new JobApplicationDoesNotExistServiceError(message: doesExist.AsT1.Message).AsList();

        var jobApplication = doesExist.AsT0;

        // Can Schedule
        var canSchedule = jobApplication.CanBeScheduledForInterview(new InterviewInfo(dateScheduled: request.DateScheduled, dateStarted: request.DateStarted, dateFinished: request.DateFinished));
        if (canSchedule.IsError()) return new CannotBeScheduledForInterviewError(message: canSchedule.GetError()).AsList();

        // Try Create
        var tryCreate = await _interviewDomainService.TryCreateInterview(new CreateInterviewServiceContract(id: request.Id, venue: request.Venue, status: request.Status, dateScheduled: request.DateScheduled, dateStarted: request.DateScheduled, dateFinished: request.DateFinished, jobApplication: jobApplication, interviewer: request.Interviewer));
        if (tryCreate.IsT1) return new CannotCreateJobApplicationServiceError(message: tryCreate.AsT1.Message).AsList();

        // Persist
        await _unitOfWork.SaveAsync();

        return new CreateInterviewResult();
    }
}