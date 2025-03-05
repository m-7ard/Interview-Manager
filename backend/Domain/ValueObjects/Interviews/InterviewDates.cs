using Domain.Contracts.Models.Interview.ValueObjects;
using OneOf;

public class InterviewDates
{
    public DateTime DateScheduled { get; }
    public DateTime? DateStarted { get; }
    public DateTime? DateFinished { get; }

    private InterviewDates(DateTime dateScheduled, DateTime? dateStarted, DateTime? dateFinished)
    {
        DateScheduled = dateScheduled;
        DateStarted = dateStarted;
        DateFinished = dateFinished;
    }

    public static OneOf<bool, string> CanCreate(CreateInterviewDatesContract contract)
    {
        if (contract.DateStarted is not null && contract.DateStarted < contract.DateScheduled)
        {
            return $"Date started ({contract.DateStarted}) cannot be before the scheduled date ({contract.DateScheduled}).";
        }

        if (contract.DateFinished is not null && contract.DateStarted is not null && contract.DateFinished < contract.DateStarted)
        {
            return $"Date finished ({contract.DateFinished}) cannot be before the start date ({contract.DateStarted}).";
        }

        return true;
    }

    public static InterviewDates ExecuteCreate(CreateInterviewDatesContract contract)
    {
        var canCreateResult = CanCreate(contract);
        if (canCreateResult.TryPickT1(out var error, out var _))
        {
            throw new Exception(error);
        }

        return new InterviewDates(contract.DateScheduled, contract.DateStarted, contract.DateFinished);
    }

    public static OneOf<InterviewDates, string> TryCreate(CreateInterviewDatesContract contract)
    {
        var canCreateResult = CanCreate(contract);
        if (canCreateResult.TryPickT1(out var error, out var _))
        {
            throw new Exception(error);
        }
        
        return ExecuteCreate(contract);
    }
}
