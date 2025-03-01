using OneOf;

namespace Domain.ValueObjects.Interviews;

public class InterviewStatus : ValueObject
{
    public static InterviewStatus Scheduled => new("Scheduled");
    public static InterviewStatus Finished => new("Finished");
    public static InterviewStatus Cancelled => new("Cancelled");

    public static readonly List<InterviewStatus> ValidStatuses = [Scheduled, Finished, Cancelled];

    public string Value { get; }

    public InterviewStatus(string value)
    {   
        Value = value;
    }

    public static OneOf<InterviewStatus, string> CanCreate(string value)
    {
        var status = ValidStatuses.Find(status => status.Value == value);

        if (status is null)
        {
            return $"{value} is not a valid InterviewStatus";
        }

        return status;
    }

    public static InterviewStatus ExecuteCreate(string value)
    {
        var canCreateResult = CanCreate(value);

        if (canCreateResult.TryPickT1(out var error, out var status))
        {
            throw new Exception(error);
        }

        return status;
    }

    public static OneOf<InterviewStatus, string> TryCreate(string value)
    {
        var canCreateResult = CanCreate(value);
        if (canCreateResult.TryPickT1(out var error, out _))
        {
            return error;
        }

        return ExecuteCreate(value);
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