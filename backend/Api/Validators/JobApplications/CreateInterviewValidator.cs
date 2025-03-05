using Api.Contracts.DTOs.JobApplications.CreateInterview;
using FluentValidation;

namespace Api.Validators.JobApplications;

public class CreateInterviewValidator : AbstractValidator<CreateInterviewRequestDTO>
{
    public CreateInterviewValidator()
    {
        RuleFor(x => x).NotNull().WithMessage("Invalid data format.");
        RuleFor(x => x.DateStarted).NotEmpty();
        RuleFor(x => x.Interviewer).NotEmpty();
        RuleFor(x => x.JobApplicationId).NotEmpty();
        RuleFor(x => x.Status).NotEmpty();
        RuleFor(x => x.Venue).NotEmpty();
    }
}