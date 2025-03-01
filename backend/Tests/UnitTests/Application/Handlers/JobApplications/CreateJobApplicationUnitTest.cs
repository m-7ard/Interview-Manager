using Application.Errors.Objects.Domains.JobApplications;
using Application.Handlers.JobApplications.Create;
using Application.Handlers.JobApplicationUpdates.Create;
using Application.Interfaces.Repositories;
using Moq;

namespace Tests.UnitTests.Application.Handlers.JobApplications;

public class CreateJobApplicationUnitTest
{
    private readonly Mock<IJobApplicationRepository> _mockJobApplicationRepository;
    private readonly CreateJobApplicationHandler _handler;
    private readonly CreateJobApplicationCommand _defaultRequest;

    public CreateJobApplicationUnitTest()
    {
        _mockJobApplicationRepository = new Mock<IJobApplicationRepository>();
        _handler = new CreateJobApplicationHandler(
            jobApplicationRepository: _mockJobApplicationRepository.Object
        );
        _defaultRequest = new CreateJobApplicationCommand(
            id: Guid.NewGuid(),
            url: "url",
            resume: "resume",
            dateCreated: DateTime.UtcNow,
            title: "title",
            company: "company" 
        );
    }

    [Fact]
    public async Task CreateJobApplication_ValidData_Success()
    {
        // Arrage

        // Act
        var result = await _handler.Handle(_defaultRequest, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess());
    }

    [Fact]
    public async Task CreateJobApplication_FutureDateCreated_Failure()
    {
        // Arrage
        _defaultRequest.DateCreated = DateTime.UtcNow.AddMinutes(1);

        // Act
        var result = await _handler.Handle(_defaultRequest, CancellationToken.None);

        // Assert
        Assert.True(result.IsError());
        Assert.IsType<CannotCreateJobApplicationError>(result.GetError().First());
    }
}