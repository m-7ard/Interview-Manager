using OneOf;

namespace Domain.ValueObjects.JobApplicationUpdate;

public class JobApplicationUpdateStatus : ValueObject
{
    public static JobApplicationUpdateStatus Sent => new("Sent");
    public static JobApplicationUpdateStatus Seen => new("Seen");
    public static JobApplicationUpdateStatus Rejected => new("Rejected");
    public static JobApplicationUpdateStatus Ghosted => new("Ghosted");
    public static JobApplicationUpdateStatus InterviewScheduled => new("InterviewScheduled");

    public static readonly List<JobApplicationUpdateStatus> ValidStatuses = [Sent, Seen, Rejected, Ghosted, InterviewScheduled];

    public string Value { get; }

    public JobApplicationUpdateStatus(string value)
    {   
        Value = value;
    }

    public static OneOf<JobApplicationUpdateStatus, string> CanCreate(string value)
    {
        var status = ValidStatuses.Find(status => status.Value == value);

        if (status is null)
        {
            return $"{value} is not a valid JobApplicationUpdateStatus";
        }

        return status;
    }

    public static JobApplicationUpdateStatus ExecuteCreate(string value)
    {
        var canCreateResult = CanCreate(value);

        if (canCreateResult.TryPickT1(out var error, out var status))
        {
            throw new Exception(error);
        }

        return status;
    }

    public static bool IsValid(string status)
    {
        return ValidStatuses.Exists(d => d.Value == status);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}