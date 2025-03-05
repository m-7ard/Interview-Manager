namespace Application.Errors.Objects.Application.JobApplications;

public class CannotAddJobApplicationUpdateError : ApplicationError
{
    public CannotAddJobApplicationUpdateError(string message, List<string>? path = null) : base(message, "CANNOT_ADD_JOB_APPLICATION_UPDATE", path ?? []) {}
}