using Application.Errors.Objects.Application.JobApplications;
using Application.Handlers.JobApplicationUpdates.Add;
using Application.Interfaces.DomainServices;
using Application.Interfaces.Persistence;
using Application.Interfaces.Repositories;
using Domain.Contracts.DomainServices.JobApplicationDomainService;
using Domain.Models;
using Domain.ValueObjects.JobApplicationUpdate;
using Moq;

namespace Tests.UnitTests.Application.Handlers.JobApplicationUpdates;

public class AddJobApplicationUpdateUnitTest
{
    private readonly Mock<IJobApplicationDomainService> _mockJobApplicationDomainService;
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;
    private readonly AddJobApplicationUpdateHandler _handler;
    private readonly AddJobApplicationUpdateCommand _defaultRequest;
    private readonly JobApplication _mockJobApplication;

    public AddJobApplicationUpdateUnitTest()
    {
        _mockJobApplicationDomainService = new Mock<IJobApplicationDomainService>();
        _mockUnitOfWork = new Mock<IUnitOfWork>();

        _mockUnitOfWork.Setup(uow => uow.JobApplicationRepository).Returns(new Mock<IJobApplicationRepository>().Object);

        _handler = new AddJobApplicationUpdateHandler(
            jobApplicationDomainService: _mockJobApplicationDomainService.Object,
            unitOfWork: _mockUnitOfWork.Object
        );
        
        _defaultRequest = new AddJobApplicationUpdateCommand(
            jobApplicationId: Guid.NewGuid(),
            jobApplicationUpdateId: Guid.NewGuid(),
            status: JobApplicationUpdateStatus.Sent.Value,
            dateOccured: DateTime.UtcNow
        );

        _mockJobApplication = Mixins.CreateJobApplication(1);
    }

    [Fact]
    public async Task AddJobApplication_ValidData_Success()
    {
        // Arrage
        _mockJobApplicationDomainService.Setup(service => service.TryGetJobApplicationById(_defaultRequest.JobApplicationId)).ReturnsAsync(_mockJobApplication);
        _mockJobApplicationDomainService.Setup(service => service.TryAddUpdate(_mockJobApplication, It.IsAny<AddJobApplicationUpdateServiceContract>())).ReturnsAsync(true);

        // Act
        var result = await _handler.Handle(_defaultRequest, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess());
    }

    [Fact]
    public async Task AddJobApplication_JobApplicationDoesNotExist_Failure()
    {
        // Arrage
        _mockJobApplicationDomainService.Setup(service => service.TryGetJobApplicationById(_defaultRequest.JobApplicationId)).ReturnsAsync(MockValues.EmptyApplicationError);

        // Act
        var result = await _handler.Handle(_defaultRequest, CancellationToken.None);

        // Assert
        Assert.True(result.IsError());
        Assert.IsType<JobApplicationDoesNotExist>(result.GetError().First());
    }

    [Fact]
    public async Task AddJobApplication_CannotAddUpdate_Failure()
    {
        // Arrage
        _mockJobApplicationDomainService.Setup(service => service.TryGetJobApplicationById(_defaultRequest.JobApplicationId)).ReturnsAsync(_mockJobApplication);
        _mockJobApplicationDomainService.Setup(service => service.TryAddUpdate(_mockJobApplication, It.IsAny<AddJobApplicationUpdateServiceContract>())).ReturnsAsync(MockValues.EmptyApplicationError);

        // Act
        var result = await _handler.Handle(_defaultRequest, CancellationToken.None);

        // Assert
        Assert.True(result.IsError());
        Assert.IsType<CannotAddJobApplicationUpdateError>(result.GetError().First());
    }
}