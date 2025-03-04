namespace Api.Contracts.DTOs.JobApplications.Create;

public class CreateJobApplicationResponseDTO
{
    public CreateJobApplicationResponseDTO(string id)
    {
        Id = id;
    }

    public string Id { get; set; }
}