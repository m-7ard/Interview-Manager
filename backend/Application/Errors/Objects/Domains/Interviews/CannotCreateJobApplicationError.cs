namespace Application.Errors.Objects.Domains.Interviews;

public class CannotCreateJobApplicationError : ApplicationError
{
    public CannotCreateJobApplicationError(string message, List<string>? path = null) : base(message, "CANNOT_CREATE_INTERVIEW", path ?? []) {}
}