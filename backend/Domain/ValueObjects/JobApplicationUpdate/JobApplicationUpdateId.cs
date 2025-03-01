using OneOf;

namespace Domain.ValueObjects.JobApplicationUpdate;

public class JobApplicationUpdateId : ValueObject
{
    private JobApplicationUpdateId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static OneOf<bool, string> CanCreate(Guid value)
    {
        return true;
    }

    public static JobApplicationUpdateId ExecuteCreate(Guid value)
    {
        return new JobApplicationUpdateId(value);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
    public override string ToString()
    {
        return Value.ToString();
    }
}