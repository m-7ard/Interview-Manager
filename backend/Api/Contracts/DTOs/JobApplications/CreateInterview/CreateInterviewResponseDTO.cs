namespace Api.Contracts.DTOs.JobApplications.CreateInterview;

public class CreateInterviewResponseDTO
{
    public CreateInterviewResponseDTO(string id)
    {
        Id = id;
    }

    public string Id { get; set; }
}