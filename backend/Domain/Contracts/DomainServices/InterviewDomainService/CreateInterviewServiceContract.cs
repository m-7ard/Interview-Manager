using Domain.ValueObjects.JobApplication;

namespace Domain.Contracts.DomainServices.InterviewDomainService;

public class CreateInterviewServiceContract
{
    public CreateInterviewServiceContract(Guid id, string venue, string status, DateTime dateScheduled, DateTime? dateStarted, DateTime? dateFinished, string interviewer)
    {
        Id = id;
        Venue = venue;
        Status = status;
        DateScheduled = dateScheduled;
        DateStarted = dateStarted;
        DateFinished = dateFinished;
        Interviewer = interviewer;
    }

    public Guid Id { get; set; }
    public string Venue { get; set; }
    public string Status { get; set; }
    public DateTime DateScheduled { get; set; }
    public DateTime? DateStarted { get; set; }
    public DateTime? DateFinished { get; set; }
    public string Interviewer { get; set; }
}
