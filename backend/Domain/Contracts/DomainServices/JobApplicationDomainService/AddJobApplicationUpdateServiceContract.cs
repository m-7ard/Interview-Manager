namespace Domain.Contracts.DomainServices.JobApplicationDomainService;

public class AddJobApplicationUpdateServiceContract
{
    public AddJobApplicationUpdateServiceContract(Guid id, string status, DateTime dateOccured)
    {
        Id = id;
        Status = status;
        DateOccured = dateOccured;
    }

    public Guid Id { get; set; }
    public string Status { get; set; }
    public DateTime DateOccured { get; set; }
}