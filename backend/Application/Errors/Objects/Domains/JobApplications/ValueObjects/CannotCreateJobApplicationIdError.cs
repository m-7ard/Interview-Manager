namespace Application.Errors.Objects.Domains.JobApplications.ValueObjects;

public class CannotCreateJobApplicationIdError : ApplicationError
{
    public CannotCreateJobApplicationIdError(string message, List<string>? path = null) : base(message, "CannotCreateJobApplicationIdError", path ?? [])
    {
    }
}