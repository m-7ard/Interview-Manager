namespace Domain.Contracts.Models.Interview.ValueObjects;

public class CreateInterviewDatesContract
{
    public CreateInterviewDatesContract(DateTime dateScheduled, DateTime? dateStarted, DateTime? dateFinished)
    {
        DateScheduled = dateScheduled;
        DateStarted = dateStarted;
        DateFinished = dateFinished;
    }

    public DateTime DateScheduled { get; }
    public DateTime? DateStarted { get; }
    public DateTime? DateFinished { get; }
}