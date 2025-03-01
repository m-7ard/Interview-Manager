namespace Domain.Contracts.DomainServices.JobApplicationDomainService;

public class CreateJobApplicationUpdateServiceContract
{
    public CreateJobApplicationUpdateServiceContract(Guid id, string status, DateTime dateOccured)
    {
        Id = id;
        Status = status;
        DateOccured = dateOccured;
    }

    public Guid Id { get; set; }
    public string Status { get; set; }
    public DateTime DateOccured { get; set; }
}