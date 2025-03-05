using Application.Errors;
using Domain.Contracts.DomainServices.InterviewDomainService;
using Domain.Models;
using OneOf;

namespace Application.Interfaces.DomainServices;

public interface IInterviewDomainService
{
    public Task<OneOf<Interview, ApplicationError>> TryCreateInterview(JobApplication jobApplication, CreateInterviewServiceContract contract);
    public Task<OneOf<bool, ApplicationError>> TryUpdateInterview(Interview interview, UpdateInterviewServiceContract contract);
    public Task<OneOf<Interview, ApplicationError>> TryGetById(Guid id);
} 