using Api.Contracts.DTOs.JobApplications.Create;
using FluentValidation;

namespace Api.Validators.JobApplications;

public class CreateJobApplicationValidator : AbstractValidator<CreateJobApplicationRequestDTO>
{
    public CreateJobApplicationValidator()
    {
        RuleFor(x => x).NotNull().WithMessage("Invalid data format.");
        RuleFor(x => x.Resume).NotEmpty();
        RuleFor(x => x.Title).NotEmpty();
        RuleFor(x => x.Url).NotEmpty();
    }
}