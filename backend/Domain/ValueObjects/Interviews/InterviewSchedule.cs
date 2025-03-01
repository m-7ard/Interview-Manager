using OneOf;

namespace Domain.ValueObjects.Interviews;

public class InterviewSchedule : ValueObject
{
    public InterviewStatus Status { get; }
    public InterviewDates Dates { get; }

    private InterviewSchedule(InterviewStatus status, InterviewDates dates)
    {
        Status = status;
        Dates = dates;
    }

    public static InterviewSchedule ExecuteCreate(InterviewStatus status, InterviewDates dates)
    {
        var validationResult = CanCreate(status, dates);

        if (validationResult.TryPickT1(out var error, out _))
        {
            throw new Exception(error);
        }

        return new InterviewSchedule(status, dates);
    }

    public static OneOf<bool, string> CanCreate(InterviewStatus status, InterviewDates dates)
    {
        if (status == InterviewStatus.Scheduled)
        {
            if (dates.DateStarted is not null || dates.DateFinished is not null)
            {
                return "Scheduled interviews cannot have a start or finish date.";
            }
        }

        if (status == InterviewStatus.Finished)
        {
            if (dates.DateStarted is null || dates.DateFinished is null)
            {
                return "Finished interviews must have both a start and finish date.";
            }
        }

        if (status == InterviewStatus.Cancelled)
        {
            if (dates.DateStarted is not null || dates.DateFinished is not null)
            {
                return "Cancelled interviews cannot have a start or finish date.";
            }
        }

        return true;
    }

    public static OneOf<InterviewSchedule, string> TryCreate(InterviewStatus status, InterviewDates dates)
    {
        var canCreateResult = CanCreate(status, dates);
        if (canCreateResult.TryPickT1(out var error, out _))
        {
            return error;
        }

        return ExecuteCreate(status, dates);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Status;
        yield return Dates;
    }
}