using Application.Utils;
using MediatR;

namespace Application.Handlers.Interviews.Delete;

public class DeleteInterviewCommand : IRequest<OneOfHandlerResult<DeleteInterviewResult>>
{
    public DeleteInterviewCommand(Guid id, bool force)
    {
        Id = id;
        Force = force;
    }

    public Guid Id { get; set; }
    public bool Force { get; set; }
}