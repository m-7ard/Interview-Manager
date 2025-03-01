using OneOf;

namespace Domain.ValueObjects.Interviews;

public class InterviewId : ValueObject
{
    private InterviewId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static OneOf<bool, string> CanCreate(Guid value)
    {
        return true;
    }

    public static InterviewId ExecuteCreate(Guid value)
    {
        return new InterviewId(value);
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