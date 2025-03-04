using Application.Errors;
using Domain.Contracts.DomainServices.InterviewDomainService;
using Domain.Models;
using OneOf;

namespace Application.Interfaces.DomainServices;

public interface IInterviewDomainService
{
    public Task<OneOf<Interview, ApplicationError>> TryCreateInterview(CreateInterviewServiceContract contract);
} 