namespace Application.Errors.Objects.Domains.Interviews;

public class CannotBeScheduledForInterviewError : ApplicationError
{
    public CannotBeScheduledForInterviewError(string message, List<string>? path = null) : base(message, "CANNOT_SCHEDULE_INTERVIEW", path ?? []) {}
}