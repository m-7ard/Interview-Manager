namespace Application.Errors.Objects.Services.JobApplicationDomainService;

public class JobApplicationDoesNotExistServiceError : ApplicationError
{
    public JobApplicationDoesNotExistServiceError(string message, List<string>? path = null) : base(message, "JobApplicationDoesNotExistServiceError", path ?? []) {}
}