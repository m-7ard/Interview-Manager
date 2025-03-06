using Application.Errors.Objects.Application.Interviews;
using Application.Handlers.Interviews.Delete;
using Application.Interfaces.DomainServices;
using Application.Interfaces.Persistence;
using Application.Interfaces.Repositories;
using Domain.Models;
using Moq;

namespace Tests.UnitTests.Application.Handlers.Interviews;

public class DeleteInterviewUnitTest
{
    private readonly Mock<IInterviewDomainService> _mockInterviewDomainService;
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;
    private readonly DeleteInterviewHandler _handler;
    private readonly DeleteInterviewCommand _defaultRequest;
    private readonly JobApplication _jobApplication;
    private readonly Interview _interview;

    public DeleteInterviewUnitTest()
    {
        _jobApplication = Mixins.CreateJobApplication(1);
        _interview = Mixins.CreateInterview(1, _jobApplication);

        _mockUnitOfWork = new Mock<IUnitOfWork>();
        _mockUnitOfWork.Setup(uow => uow.InterviewRepository).Returns(new Mock<IInterviewRepository>().Object);

        _mockInterviewDomainService = new Mock<IInterviewDomainService>();

        _handler = new DeleteInterviewHandler(
            unitOfWork: _mockUnitOfWork.Object,
            interviewDomainService: _mockInterviewDomainService.Object
        );
        
        _defaultRequest = new DeleteInterviewCommand(
            id: _interview.Id.Value,
            force: false
        );
    }

    [Fact]
    public async Task DeleteInterview_ValidData_Success()
    {
        // Arrage
        _mockInterviewDomainService.Setup(service => service.TryGetById(_interview.Id.Value)).ReturnsAsync(_interview);

        // Act
        var result = await _handler.Handle(_defaultRequest, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess());
    }

    [Fact]
    public async Task DeleteInterview_InterviewDoesNotExist_Failure()
    {
        // Arrage
        _mockInterviewDomainService.Setup(service => service.TryGetById(_interview.Id.Value)).ReturnsAsync(MockValues.EmptyApplicationError);

        // Act
        var result = await _handler.Handle(_defaultRequest, CancellationToken.None);

        // Assert
        Assert.True(result.IsError());
        Assert.IsType<InterviewDoesNotExistError>(result.GetError().First());
    }
}