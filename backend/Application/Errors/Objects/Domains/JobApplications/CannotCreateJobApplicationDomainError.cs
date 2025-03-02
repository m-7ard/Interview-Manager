namespace Application.Errors.Objects.Domains.JobApplications;

public class CannotCreateJobApplicationDomainError : ApplicationError
{
    public CannotCreateJobApplicationDomainError(string message, List<string>? path = null) : base(message, GeneralApplicationErrorCodes.CANNOT_CREATE_JOB_APPLICATION, path ?? []) {}
}