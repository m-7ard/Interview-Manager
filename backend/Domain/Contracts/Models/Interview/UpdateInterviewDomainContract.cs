using Domain.ValueObjects.JobApplication;

namespace Domain.Contracts.Models.Interview;

public class UpdateInterviewDomainContract
{
    public UpdateInterviewDomainContract(Guid id, string? venue, string status, DateTime dateScheduled, DateTime? dateStarted, DateTime? dateFinished, string interviewer, JobApplicationId jobApplicationId)
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
    public JobApplicationId JobApplicationId { get; set; }
}