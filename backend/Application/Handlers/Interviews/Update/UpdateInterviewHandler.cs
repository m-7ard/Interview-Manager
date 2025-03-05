using Application.Errors.Objects.Application.Interviews;
using Application.Errors.Objects.Application.JobApplications;
using Application.Interfaces.DomainServices;
using Application.Interfaces.Persistence;
using Application.Utils;
using Domain.Contracts.DomainServices.InterviewDomainService;
using MediatR;

namespace Application.Handlers.Interviews.Update;

public class UpdateInterviewHandler : IRequestHandler<UpdateInterviewCommand, OneOfHandlerResult<UpdateInterviewResult>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IInterviewDomainService _interviewDomainService;
    private readonly IJobApplicationDomainService _jobApplicationDomainService;

    public UpdateInterviewHandler(IUnitOfWork unitOfWork, IInterviewDomainService interviewDomainService, IJobApplicationDomainService jobApplicationDomainService)
    {
        _unitOfWork = unitOfWork;
        _interviewDomainService = interviewDomainService;
        _jobApplicationDomainService = jobApplicationDomainService;
    }

    public async Task<OneOfHandlerResult<UpdateInterviewResult>> Handle(UpdateInterviewCommand request, CancellationToken cancellationToken)
    {
        // Interview Exists
        var doesExist = await _interviewDomainService.TryGetById(request.Id);
        if (doesExist.IsT1) return new InterviewDoesNotExistError(message: doesExist.AsT1.Message).AsList();

        var interview = doesExist.AsT0;

        // Try Update
        var tryUpdate = await _interviewDomainService.TryUpdateInterview(interview, new UpdateInterviewServiceContract(id: request.Id, venue: request.Venue, status: request.Status, dateScheduled: request.DateScheduled, dateStarted: request.DateScheduled, dateFinished: request.DateFinished, interviewer: request.Interviewer));
        if (tryUpdate.IsT1) return new CannotUpdateInterviewError(message: tryUpdate.AsT1.Message).AsList();

        // Persist
        await _unitOfWork.SaveAsync();

        return new UpdateInterviewResult();
    }
}