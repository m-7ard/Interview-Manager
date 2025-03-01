using OneOf;

namespace Domain.ValueObjects.JobApplication;

public class JobApplicationId : ValueObject
{
    private JobApplicationId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static OneOf<bool, string> CanCreate(Guid value)
    {
        return true;
    }

    public static JobApplicationId ExecuteCreate(Guid value)
    {
        return new JobApplicationId(value);
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