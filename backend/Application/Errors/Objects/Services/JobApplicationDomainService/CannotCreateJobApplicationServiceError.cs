namespace Application.Errors.Objects.Services.JobApplicationDomainService;

public class CannotCreateJobApplicationServiceError : ApplicationError
{
    public CannotCreateJobApplicationServiceError(string message, List<string>? path = null) : base(message, "CannotCreateJobApplicationServiceError", path ?? []) {}
}