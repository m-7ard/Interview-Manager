namespace Domain.Contracts.DomainServices.JobApplicationDomainService;

public class CreateJobApplicationServiceContract
{
    public CreateJobApplicationServiceContract(Guid id, string url, string resume, DateTime dateCreated, string title, string company)
    {
        Id = id;
        Url = url;
        Resume = resume;
        DateCreated = dateCreated;
        Title = title;
        Company = company;
    }

    public Guid Id { get; set; }
    public string Url { get; set; }
    public string Resume { get; set; }
    public DateTime DateCreated { get; set; }
    public string Title { get; set; }
    public string Company { get; set; }
}