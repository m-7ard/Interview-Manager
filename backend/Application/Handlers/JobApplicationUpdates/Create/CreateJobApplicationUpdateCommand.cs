using Application.Utils;
using MediatR;

namespace Application.Handlers.JobApplicationUpdates.Create;

public class CreateJobApplicationUpdateCommand : IRequest<OneOfHandlerResult<CreateJobApplicationUpdateResult>>
{
    public CreateJobApplicationUpdateCommand(Guid id, string status, DateTime dateOccured)
    {
        Id = id;
        Status = status;
        DateOccured = dateOccured;
    }

    public Guid Id { get; set; }
    public string Status { get; set; }
    public DateTime DateOccured { get; set; }
}