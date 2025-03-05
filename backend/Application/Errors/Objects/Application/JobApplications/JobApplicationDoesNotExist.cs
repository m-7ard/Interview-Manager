namespace Application.Errors.Objects.Application.JobApplications;

public class JobApplicationDoesNotExist : ApplicationError
{
    public JobApplicationDoesNotExist(string message, List<string>? path = null) : base(message, "JobApplicationDoesNotExist", path ?? []) {}
}