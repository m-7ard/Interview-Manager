namespace Application.Errors.Objects.Application.Interviews;

public class InterviewDoesNotExistError : ApplicationError
{
    public InterviewDoesNotExistError(string message, List<string>? path = null) : base(message, "InterviewDoesNotExistError", path ?? []) {}
}