namespace Application.Errors.Objects.Application.Interviews;

public class CannotUpdateInterviewError : ApplicationError
{
    public CannotUpdateInterviewError(string message, List<string>? path = null) : base(message, "CannotUpdateInterviewError", path ?? []) {}
}