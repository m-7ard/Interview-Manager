using Application.Errors;
using Application.Errors.Objects.Domains.Interviews;
using Application.Interfaces.DomainServices;
using Application.Interfaces.Persistence;
using Domain.Contracts.DomainServices.InterviewDomainService;
using Domain.Contracts.Models.Interview;
using Domain.Models;
using OneOf;

namespace Application.DomainServices;

public class InterviewDomainService : IInterviewDomainService
{
    private readonly IUnitOfWork _unitOfWork;

    public InterviewDomainService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<OneOf<Interview, ApplicationError>> TryCreateInterview(CreateInterviewServiceContract contract)
    {
        var creationContract = new CreateInterviewContract(id: contract.Id, venue: contract.Venue, status: contract.Status, dateScheduled: contract.DateScheduled, dateStarted: contract.DateStarted, dateFinished: contract.DateFinished, interviewer: contract.Interviewer, jobApplicationId: contract.JobApplication.Id);
        var canCreate = Interview.CanCreate(creationContract);

        if (canCreate.IsT1) return new CannotCreateJobApplicationError(message: canCreate.AsT1);

        var interview = Interview.ExecuteCreate(creationContract);
        await _unitOfWork.InterviewRepository.CreateAsync(interview);

        return interview;
    }
}