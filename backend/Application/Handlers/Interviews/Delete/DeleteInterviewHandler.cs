using Application.Errors.Objects.Application.Interviews;
using Application.Interfaces.DomainServices;
using Application.Interfaces.Persistence;
using Application.Utils;
using MediatR;

namespace Application.Handlers.Interviews.Delete;

public class DeleteInterviewHandler : IRequestHandler<DeleteInterviewCommand, OneOfHandlerResult<DeleteInterviewResult>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IInterviewDomainService _interviewDomainService;

    public DeleteInterviewHandler(IUnitOfWork unitOfWork, IInterviewDomainService interviewDomainService)
    {
        _unitOfWork = unitOfWork;
        _interviewDomainService = interviewDomainService;
    }

    public async Task<OneOfHandlerResult<DeleteInterviewResult>> Handle(DeleteInterviewCommand request, CancellationToken cancellationToken)
    {
        // Interview Exists
        var doesExist = await _interviewDomainService.TryGetById(request.Id);
        if (doesExist.IsT1) return new InterviewDoesNotExistError(message: doesExist.AsT1.Message).AsList();

        var interview = doesExist.AsT0;

        // Delete
        await _unitOfWork.InterviewRepository.DeleteAsync(interview);

        // Persist
        await _unitOfWork.SaveAsync();

        return new DeleteInterviewResult();
    }
}