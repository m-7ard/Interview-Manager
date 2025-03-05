namespace Api.Contracts.DTOs.JobApplications.CreateInterview;

public class CreateInterviewRequestDTO
{
    public CreateInterviewRequestDTO(string venue, string status, DateTime dateScheduled, DateTime? dateStarted, DateTime? dateFinished, Guid jobApplicationId, string interviewer)
    {
        Venue = venue;
        Status = status;
        DateScheduled = dateScheduled;
        DateStarted = dateStarted;
        DateFinished = dateFinished;
        JobApplicationId = jobApplicationId;
        Interviewer = interviewer;
    }

    public string Venue { get; set; }
    public string Status { get; set; }
    public DateTime DateScheduled { get; set; }
    public DateTime? DateStarted { get; set; }
    public DateTime? DateFinished { get; set; }
    public Guid JobApplicationId { get; set; }
    public string Interviewer { get; set; }
}