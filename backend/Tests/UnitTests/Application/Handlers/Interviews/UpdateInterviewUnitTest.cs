using Application.Errors.Objects.Application.Interviews;
using Application.Handlers.Interviews.Delete;
using Application.Handlers.Interviews.Update;
using Application.Interfaces.DomainServices;
using Application.Interfaces.Persistence;
using Application.Interfaces.Repositories;
using Domain.Contracts.DomainServices.InterviewDomainService;
using Domain.Models;
using Domain.ValueObjects.Interviews;
using Moq;

namespace Tests.UnitTests.Application.Handlers.Interviews;

public class UpdateInterviewUnitTest
{
    private readonly Mock<IInterviewDomainService> _mockInterviewDomainService;
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;
    private readonly UpdateInterviewHandler _handler;
    private readonly UpdateInterviewCommand _defaultRequest;
    private readonly JobApplication _jobApplication;
    private readonly Interview _interview;

    public UpdateInterviewUnitTest()
    {
        _jobApplication = Mixins.CreateJobApplication(1);
        _interview = Mixins.CreateInterview(1, _jobApplication);

        _mockUnitOfWork = new Mock<IUnitOfWork>();
        _mockUnitOfWork.Setup(uow => uow.JobApplicationRepository).Returns(new Mock<IJobApplicationRepository>().Object);

        _mockInterviewDomainService = new Mock<IInterviewDomainService>();

        _handler = new UpdateInterviewHandler(
            unitOfWork: _mockUnitOfWork.Object,
            interviewDomainService: _mockInterviewDomainService.Object
        );
        
        _defaultRequest = new UpdateInterviewCommand(
            id: _interview.Id.Value,
            venue: "venue",
            status: InterviewStatus.Scheduled.Value,
            dateScheduled: DateTime.UtcNow,
            dateStarted: DateTime.UtcNow,
            dateFinished: DateTime.UtcNow,
            interviewer: "Interviewer"
        );
    }

    [Fact]
    public async Task UpdateInterview_ValidData_Success()
    {
        // Arrage
        _mockInterviewDomainService.Setup(service => service.TryGetById(_interview.Id.Value)).ReturnsAsync(_interview);
        _mockInterviewDomainService.Setup(service => service.TryUpdateInterview(_interview, It.IsAny<UpdateInterviewServiceContract>())).ReturnsAsync(true);

        // Act
        var result = await _handler.Handle(_defaultRequest, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess());
    }

    [Fact]
    public async Task UpdateInterview_InterviewDoesNotExist_Failure()
    {
        // Arrage
        _mockInterviewDomainService.Setup(service => service.TryGetById(_interview.Id.Value)).ReturnsAsync(MockValues.EmptyApplicationError);

        // Act
        var result = await _handler.Handle(_defaultRequest, CancellationToken.None);

        // Assert
        Assert.True(result.IsError());
        Assert.IsType<InterviewDoesNotExistError>(result.GetError().First());
    }

    [Fact]
    public async Task UpdateInterview_CannotUpdateInterview_Failure()
    {
        // Arrage
        _mockInterviewDomainService.Setup(service => service.TryGetById(_interview.Id.Value)).ReturnsAsync(_interview);
        _mockInterviewDomainService.Setup(service => service.TryUpdateInterview(_interview, It.IsAny<UpdateInterviewServiceContract>())).ReturnsAsync(MockValues.EmptyApplicationError);

        // Act
        var result = await _handler.Handle(_defaultRequest, CancellationToken.None);

        // Assert
        Assert.True(result.IsError());
        Assert.IsType<CannotUpdateInterviewError>(result.GetError().First());
    }
}