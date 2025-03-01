using Application.Errors.Objects.Domains.JobApplicationUpdates;
using Application.Handlers.JobApplicationUpdates.Create;
using Application.Interfaces.Repositories;
using Domain.ValueObjects.JobApplicationUpdate;
using Moq;

namespace Tests.UnitTests.Application.Handlers.JobApplicationUpdates;

public class CreateJobApplicationUpdateUnitTest
{
    private readonly Mock<IJobApplicationRepository> _mockJobApplicationRepository;
    private readonly CreateJobApplicationUpdateHandler _handler;
    private readonly CreateJobApplicationUpdateCommand _defaultRequest;

    public CreateJobApplicationUpdateUnitTest()
    {
        _mockJobApplicationRepository = new Mock<IJobApplicationRepository>();
        _handler = new CreateJobApplicationUpdateHandler(
            jobApplicationRepository: _mockJobApplicationRepository.Object
        );
        _defaultRequest = new CreateJobApplicationUpdateCommand(
            id: Guid.NewGuid(),
            status: JobApplicationUpdateStatus.Sent.Value,
            dateOccured: DateTime.UtcNow
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
    public async Task CreateJobApplication_FutureDateOccured_Failure()
    {
        // Arrage
        _defaultRequest.DateOccured = DateTime.UtcNow.AddMinutes(1);

        // Act
        var result = await _handler.Handle(_defaultRequest, CancellationToken.None);

        // Assert
        Assert.True(result.IsError());
        Assert.IsType<CannotCreateJobApplicationUpdateError>(result.GetError().First());
    }
}