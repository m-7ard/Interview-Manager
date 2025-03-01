namespace Application.Errors.Objects.Domains.JobApplications;

public class CannotCreateJobApplicationError : ApplicationError
{
    public CannotCreateJobApplicationError(string message, List<string> path) : base(message, GeneralApplicationErrorCodes.CANNOT_CREATE_JOB_APPLICATION, path)
    {
        Message = message;
        Path = path;
    }
}