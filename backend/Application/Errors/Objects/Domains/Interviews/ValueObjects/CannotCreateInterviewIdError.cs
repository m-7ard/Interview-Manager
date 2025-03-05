namespace Application.Errors.Objects.Domains.Interviews.ValueObjects;

public class CannotCreateInterviewIdError : ApplicationError
{
    public CannotCreateInterviewIdError(string message, List<string>? path = null) : base(message, "CannotCreateInterviewIdError", path ?? []) {}
}