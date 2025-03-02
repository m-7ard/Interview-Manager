using Application.Utils;
using MediatR;

namespace Application.Handlers.JobApplicationUpdates.Add;

public class AddJobApplicationUpdateCommand : IRequest<OneOfHandlerResult<AddJobApplicationUpdateResult>>
{
    public AddJobApplicationUpdateCommand(Guid jobApplicationId, Guid jobApplicationUpdateId, string status, DateTime dateOccured)
    {
        JobApplicationId = jobApplicationId;
        JobApplicationUpdateId = jobApplicationUpdateId;
        Status = status;
        DateOccured = dateOccured;
    }

    public Guid JobApplicationId { get; set; }
    public Guid JobApplicationUpdateId { get; set; }
    public string Status { get; set; }
    public DateTime DateOccured { get; set; }
}