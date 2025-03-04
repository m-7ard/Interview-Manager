namespace Api.Contracts.DTOs.JobApplications.Create;

public class CreateJobApplicationRequestDTO
{
    // TODO - rename this and then implement and write test
    public CreateJobApplicationRequestDTO(string url, string resume, string title, string company, DateTime datePublished)
    {
        Url = url;
        Resume = resume;
        Title = title;
        Company = company;
        DatePublished = datePublished;
    }

    public string Url { get; set; }
    public string Resume { get; set; }
    public string Title { get; set; }
    public string Company { get; set; }
    public DateTime DatePublished { get; set; }
}