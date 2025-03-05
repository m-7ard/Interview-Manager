using Application.Errors.Objects.Services.JobApplicationDomainService;
using Application.Handlers.JobApplications.Create;
using Application.Interfaces.DomainServices;
using Application.Interfaces.Persistence;
using Application.Interfaces.Repositories;
using Domain.Contracts.DomainServices.JobApplicationDomainService;
using Domain.Models;
using Moq;

namespace Tests.UnitTests.Application.Handlers.JobApplications;

public class CreateJobApplicationUnitTest
{
    private readonly Mock<IJobApplicationDomainService> _mockJobApplicationDomainService;
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;
    private readonly CreateJobApplicationHandler _handler;
    private readonly CreateJobApplicationCommand _defaultRequest;
    private readonly JobApplication _jobApplication;

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
            title: "title",
            company: "company",
            datePublished: DateTime.UtcNow
        );

        _jobApplication = Mixins.CreateJobApplication(1);
    }

    [Fact]
    public async Task CreateJobApplication_ValidData_Success()
    {
        // Arrage
        _mockJobApplicationDomainService.Setup(service => service.TryCreate(It.IsAny<CreateJobApplicationServiceContract>())).ReturnsAsync(_jobApplication);

        // Act
        var result = await _handler.Handle(_defaultRequest, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess());
    }

    [Fact]
    public async Task CreateJobApplication_CannotCreate_Failure()
    {
        // Arrage
        _mockJobApplicationDomainService.Setup(service => service.TryCreate(It.IsAny<CreateJobApplicationServiceContract>())).ReturnsAsync(MockValues.EmptyApplicationError);

        // Act
        var result = await _handler.Handle(_defaultRequest, CancellationToken.None);

        // Assert
        Assert.True(result.IsError());
        Assert.IsType<CannotCreateJobApplicationServiceError>(result.GetError().First());
    }
}