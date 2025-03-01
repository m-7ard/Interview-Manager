namespace Domain.Contracts.Models.JobApplicationUpdate;

public class CreateJobApplicationUpdateContract
{
    public CreateJobApplicationUpdateContract(Guid id, string status, DateTime dateOccured)
    {
        Id = id;
        Status = status;
        DateOccured = dateOccured;
    }

    public Guid Id { get; set; }
    public string Status { get; set; }
    public DateTime DateOccured { get; set; }
}