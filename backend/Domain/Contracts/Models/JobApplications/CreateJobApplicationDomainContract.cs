namespace Domain.Contracts.Models.JobApplications;

public class CreateJobApplicationDomainContract
{
    public CreateJobApplicationDomainContract(Guid id, string url, string resume, string title, string company, DateTime datePublished, DateTime dateCreated)
    {
        Id = id;
        Url = url;
        Resume = resume;
        Title = title;
        Company = company;
        DatePublished = datePublished;
        DateCreated = dateCreated;
    }

    public Guid Id { get; set; }
    public string Url { get; set; }
    public string Resume { get; set; }
    public string Title { get; set; }
    public string Company { get; set; }
    public DateTime DatePublished { get; set; }
    public DateTime DateCreated { get; private set; }
}