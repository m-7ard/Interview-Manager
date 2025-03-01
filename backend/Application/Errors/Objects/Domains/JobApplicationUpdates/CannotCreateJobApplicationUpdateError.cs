namespace Application.Errors.Objects.Domains.JobApplicationUpdates;

public class CannotCreateJobApplicationUpdateError : ApplicationError
{
    public CannotCreateJobApplicationUpdateError(string message, List<string> path) : base(message, GeneralApplicationErrorCodes.CANNOT_CREATE_JOB_APPLICATION, path)
    {
        Message = message;
        Path = path;
    }
}