namespace Api.Contracts.DTOs.JobApplications.Create;

public class CreateJobApplicationRequestDTO
{
    public CreateJobApplicationRequestDTO(string url, string resume, string title, string company)
    {
        Url = url;
        Resume = resume;
        Title = title;
        Company = company;
    }

    public string Url { get; set; }
    public string Resume { get; set; }
    public string Title { get; set; }
    public string Company { get; set; }
}