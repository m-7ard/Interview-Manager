using Application.Errors.Objects.Domains.JobApplications;
using Application.Handlers.JobApplications.Create;
using Application.Interfaces.DomainServices;
using Application.Interfaces.Persistence;
using Application.Interfaces.Repositories;
using Moq;

namespace Tests.UnitTests.Application.Handlers.JobApplications;

public class CreateJobApplicationUnitTest
{
    private readonly Mock<IJobApplicationDomainService> _mockJobApplicationDomainService;
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;
    private readonly CreateJobApplicationHandler _handler;
    private readonly CreateJobApplicationCommand _defaultRequest;

    public CreateJobApplicationUnitTest()
    {
        _mockUnitOfWork = new Mock<IUnitOfWork>();
        _mockUnitOfWork.Setup(uow => uow.JobApplicationRepository).Returns(new Mock<IJobApplicationRepository>().Object);

        _mockJobApplicationDomainService = new Mock<IJobApplicationDomainService>();

        _handler = new CreateJobApplicationHandler(
            jobApplicationDomainService: _mockJobApplicationDomainService.Object,
            unitOfWork: _mockUnitOfWork.Object
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
        Assert.IsType<CannotCreateJobApplicationDomainError>(result.GetError().First());
    }
}