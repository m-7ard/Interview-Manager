using Application.Errors;
using Application.Errors.Objects.Application.Interviews;
using Application.Errors.Objects.Application.JobApplications;
using Application.Errors.Objects.Domains.Interviews;
using Application.Errors.Objects.Domains.Interviews.ValueObjects;
using Application.Interfaces.DomainServices;
using Application.Interfaces.Persistence;
using Domain.Contracts.DomainServices.InterviewDomainService;
using Domain.Contracts.Models.Interview;
using Domain.Models;
using Domain.ValueObjects.Common;
using Domain.ValueObjects.Interviews;
using OneOf;

namespace Application.DomainServices;

public class InterviewDomainService : IInterviewDomainService
{
    private readonly IUnitOfWork _unitOfWork;

    public InterviewDomainService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<OneOf<Interview, ApplicationError>> TryCreateInterview(JobApplication jobApplication, CreateInterviewServiceContract contract)
    {
        // Can Schedule
        var canSchedule = jobApplication.CanBeScheduledForInterview(new InterviewInfo(dateScheduled: contract.DateScheduled, dateStarted: contract.DateStarted, dateFinished: contract.DateFinished));
        if (canSchedule.IsError()) return new CannotBeScheduledForInterviewError(message: canSchedule.GetError());
   
        // Can Create
        var creationContract = new CreateInterviewContract(id: contract.Id, venue: contract.Venue, status: contract.Status, dateScheduled: contract.DateScheduled, dateStarted: contract.DateStarted, dateFinished: contract.DateFinished, interviewer: contract.Interviewer, jobApplicationId: jobApplication.Id);
        var canCreate = Interview.CanCreate(creationContract);

        if (canCreate.IsT1) return new CannotCreateJobApplicationError(message: canCreate.AsT1);

        var interview = Interview.ExecuteCreate(creationContract);
        await _unitOfWork.InterviewRepository.CreateAsync(interview);

        return interview;
    }

    public async Task<OneOf<Interview, ApplicationError>> TryGetById(Guid id)
    {
        // Can create id
        var canCreatedInterviewId = InterviewId.CanCreate(id);
        if (canCreatedInterviewId.IsT1) return new CannotCreateInterviewIdError(message: canCreatedInterviewId.AsT1);
    
        var interviewId = InterviewId.ExecuteCreate(id);
        
        // Interview exists
        var interview = await _unitOfWork.InterviewRepository.GetByIdAsync(interviewId);
        if (interview is null) return new InterviewDoesNotExistError(message: $"Interview of Id \"{id}\" does not exist.");

        return interview;   
    }

    public async Task<OneOf<bool, ApplicationError>> TryUpdateInterview(Interview interview, UpdateInterviewServiceContract contract)
    {
        var updateContract = new UpdateInterviewDomainContract(id: interview.Id.Value, venue: contract.Venue, status: contract.Status, dateScheduled: contract.DateScheduled, dateStarted: contract.DateStarted, dateFinished: contract.DateFinished, interviewer: contract.Interviewer, jobApplicationId: interview.JobApplicationId);
        var canUpdate = interview.CanUpdate(updateContract);
        if (canUpdate.IsT1) return new CannotUpdateInterviewError(message: canUpdate.AsT1);

        interview.ExecuteUpdate(updateContract);
        await _unitOfWork.InterviewRepository.UpdateAsync(interview);     

        return true;
    }
}