namespace Infrastructure.DbEntities;

public class InterviewDbEntity
{
    public InterviewDbEntity(Guid id, string? venue, string status, DateTime dateScheduled, DateTime? dateStarted, DateTime? dateFinished, string interviewer, Guid jobApplicationId)
    {
        Id = id;
        Venue = venue;
        Status = status;
        DateScheduled = dateScheduled;
        DateStarted = dateStarted;
        DateFinished = dateFinished;
        Interviewer = interviewer;
        JobApplicationId = jobApplicationId;
    }

    public Guid Id { get; set; }
    public string? Venue { get; set; }
    public string Status { get; set; }
    public DateTime DateScheduled { get; set; }
    public DateTime? DateStarted { get; set; }
    public DateTime? DateFinished { get; set; }
    public string Interviewer { get; set; }
    
    public Guid JobApplicationId { get; set; }
    public JobApplicationDbEntity JobApplication { get; private set; } = null!;
}