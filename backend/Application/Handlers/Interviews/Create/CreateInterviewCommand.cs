using Application.Utils;
using MediatR;

namespace Application.Handlers.Interviews.Create;

public class CreateInterviewCommand : IRequest<OneOfHandlerResult<CreateInterviewResult>>
{
    public CreateInterviewCommand(Guid id, string venue, string status, DateTime dateScheduled, DateTime? dateStarted, DateTime? dateFinished, Guid jobApplicationId, string interviewer)
    {
        Id = id;
        Venue = venue;
        Status = status;
        DateScheduled = dateScheduled;
        DateStarted = dateStarted;
        DateFinished = dateFinished;
        JobApplicationId = jobApplicationId;
        Interviewer = interviewer;
    }

    public Guid Id { get; set; }
    public string Venue { get; set; }
    public string Status { get; set; }
    public DateTime DateScheduled { get; set; }
    public DateTime? DateStarted { get; set; }
    public DateTime? DateFinished { get; set; }
    public Guid JobApplicationId { get; set; }
    public string Interviewer { get; set; }
}