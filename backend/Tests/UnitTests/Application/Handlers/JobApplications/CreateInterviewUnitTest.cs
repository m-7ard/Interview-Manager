using Application.Errors.Objects.Application.Interviews;
using Application.Errors.Objects.Application.JobApplications;
using Application.Errors.Objects.Services.JobApplicationDomainService;
using Application.Handlers.Interviews.Create;
using Application.Handlers.Interviews.Update;
using Application.Handlers.JobApplications.Create;
using Application.Interfaces.DomainServices;
using Application.Interfaces.Persistence;
using Application.Interfaces.Repositories;
using Domain.Contracts.DomainServices.InterviewDomainService;
using Domain.Contracts.DomainServices.JobApplicationDomainService;
using Domain.Models;
using Domain.ValueObjects.Interviews;
using Moq;

namespace Tests.UnitTests.Application.Handlers.JobApplications;

public class CreateInterviewUnitTest
{
    private readonly Mock<IJobApplicationDomainService> _mockJobApplicationDomainService;
    private readonly Mock<IInterviewDomainService> _mockInterviewDomainService;
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;
    private readonly CreateInterviewHandler _handler;
    private readonly CreateInterviewCommand _defaultRequest;
    private readonly JobApplication _jobApplication;
    private readonly Interview _interview;

    public CreateInterviewUnitTest()
    {
        _jobApplication = Mixins.CreateJobApplication(1);
        _interview = Mixins.CreateInterview(1, _jobApplication);

        _mockUnitOfWork = new Mock<IUnitOfWork>();
        _mockUnitOfWork.Setup(uow => uow.JobApplicationRepository).Returns(new Mock<IJobApplicationRepository>().Object);

        _mockJobApplicationDomainService = new Mock<IJobApplicationDomainService>();
        _mockInterviewDomainService = new Mock<IInterviewDomainService>();

        _handler = new CreateInterviewHandler(
            unitOfWork: _mockUnitOfWork.Object,
            interviewDomainService: _mockInterviewDomainService.Object,
            jobApplicationDomainService: _mockJobApplicationDomainService.Object
        );
        
        _defaultRequest = new CreateInterviewCommand(
            id: Guid.NewGuid(),
            venue: "venue",
            status: InterviewStatus.Scheduled.Value,
            dateScheduled: DateTime.UtcNow,
            dateStarted: DateTime.UtcNow,
            dateFinished: DateTime.UtcNow,
            jobApplicationId: _jobApplication.Id.Value,
            interviewer: "Interviewer"
        );

        _jobApplication = Mixins.CreateJobApplication(1);
    }

    [Fact]
    public async Task CreateJobApplication_ValidData_Success()
    {
        // Arrage
        _mockJobApplicationDomainService.Setup(service => service.TryGetJobApplicationById(It.IsAny<Guid>())).ReturnsAsync(_jobApplication);
        _mockInterviewDomainService.Setup(service => service.TryCreateInterview(_jobApplication, It.IsAny<CreateInterviewServiceContract>())).ReturnsAsync(_interview);

        // Act
        var result = await _handler.Handle(_defaultRequest, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess());
    }

    [Fact]
    public async Task CreateJobApplication_JobApplicationDoesNotExist_Failure()
    {
        // Arrage
        _mockJobApplicationDomainService.Setup(service => service.TryGetJobApplicationById(It.IsAny<Guid>())).ReturnsAsync(MockValues.EmptyApplicationError);

        // Act
        var result = await _handler.Handle(_defaultRequest, CancellationToken.None);

        // Assert
        Assert.True(result.IsError());
        Assert.IsType<JobApplicationDoesNotExist>(result.GetError().First());
    }

    [Fact]
    public async Task CreateJobApplication_CannotCreateInterview_Failure()
    {
        // Arrage
        _mockJobApplicationDomainService.Setup(service => service.TryGetJobApplicationById(It.IsAny<Guid>())).ReturnsAsync(_jobApplication);
        _mockInterviewDomainService.Setup(service => service.TryCreateInterview(_jobApplication, It.IsAny<CreateInterviewServiceContract>())).ReturnsAsync(MockValues.EmptyApplicationError);

        // Act
        var result = await _handler.Handle(_defaultRequest, CancellationToken.None);

        // Assert
        Assert.True(result.IsError());
        Assert.IsType<CannotCreateInterviewError>(result.GetError().First());
    }
}