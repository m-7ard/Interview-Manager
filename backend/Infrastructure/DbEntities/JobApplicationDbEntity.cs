namespace Infrastructure.DbEntities;

public class JobApplicationDbEntity
{
    public JobApplicationDbEntity(Guid id, string url, string resume, DateTime dateCreated, List<JobApplicationUpdateDbEntity> updates, string title, string company, DateTime datePublished)
    {
        Id = id;
        Url = url;
        Resume = resume;
        DateCreated = dateCreated;
        Updates = updates;
        Title = title;
        Company = company;
        DatePublished = datePublished;
    }

    public Guid Id { get; set; }
    public string Url { get; set; }
    public string Resume { get; set; }
    public DateTime DateCreated { get; set; }
    public List<JobApplicationUpdateDbEntity> Updates { get; set; } = [];
    public string Title { get; set; }
    public string Company { get; set; }
    public DateTime DatePublished { get; set; }
}