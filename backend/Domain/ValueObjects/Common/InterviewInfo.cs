namespace Domain.ValueObjects.Common;

public class InterviewInfo : ValueObject
{
    public InterviewInfo(DateTime dateScheduled, DateTime? dateStarted, DateTime? dateFinished)
    {
        DateScheduled = dateScheduled;
        DateStarted = dateStarted;
        DateFinished = dateFinished;
    }

    public DateTime DateScheduled { get; }
    public DateTime? DateStarted { get; }
    public DateTime? DateFinished { get; }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return DateScheduled;
        yield return DateStarted ?? DateTime.MinValue;;
        yield return DateFinished ?? DateTime.MinValue;;
    }
}