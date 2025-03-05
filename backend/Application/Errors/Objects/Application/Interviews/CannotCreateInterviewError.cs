namespace Application.Errors.Objects.Application.Interviews;

public class CannotCreateInterviewError : ApplicationError
{
    public CannotCreateInterviewError(string message, List<string>? path = null) : base(message, "CannotCreateInterviewServiceError", path ?? []) {}
}