using Application.Errors;

public class CannotAddJobApplicationUpdateDomainError : ApplicationError
{
    public CannotAddJobApplicationUpdateDomainError(string message, List<string>? path = null) : base(message, "CANNOT_ADD_JOB_APPLICATION_UPDATE", path ?? []) {}
}