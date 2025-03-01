
using Application.Interfaces.DomainServices;
using Application.Interfaces.Persistence;
using Application.Utils;
using Domain.Contracts.DomainServices.JobApplicationDomainService;
using Domain.Contracts.Models.JobApplicationUpdate;
using Domain.Models;
using Domain.Utils;

public class JobApplicationService : IJobApplicationService
{
    private readonly IUnitOfWork _unitOfWork;

    public JobApplicationService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<OneOfHandlerResult<bool>> TryAddUpdate(JobApplication jobApplication, CreateJobApplicationUpdateServiceContract contract)
    {
        var canAdd = jobApplication.CanAddUpdate(new CreateJobApplicationUpdateContract(id: contract.Id, status: contract.Status, dateOccured: contract.DateOccured));
        if (canAdd.IsError()) return new CannotAddJobApplicationUpdateDomainError(message: canAdd.GetError(), path: []).AsList();

        await _unitOfWork.JobApplicationRepository.UpdateAsync(jobApplication);
        return true;
    }
}