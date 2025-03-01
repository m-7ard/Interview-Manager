namespace Infrastructure.DbEntities;

public class JobApplicationUpdateDbEntity
{
    public JobApplicationUpdateDbEntity(Guid id, string status, DateTime dateOccured)
    {
        Id = id;
        Status = status;
        DateOccured = dateOccured;
    }

    public Guid Id { get; set; }
    public string Status { get; set; }
    public DateTime DateOccured { get; set; }
}