using Api.Contracts.DTOs.JobApplications.Create;
using Api.Contracts.DTOs.JobApplications.CreateInterview;
using Api.Services;
using Application.Handlers.Interviews.Create;
using Application.Handlers.JobApplications.Create;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[AllowAnonymous]
[Route("api/job-applications/")]
public class ProductsController : ControllerBase
{
    private readonly ISender _mediator;
    private readonly IValidator<CreateJobApplicationRequestDTO> _createJobApplicationValidator;
    private readonly IValidator<CreateInterviewRequestDTO> _updateJobApplicationValidator;

    public ProductsController(ISender mediator, IValidator<CreateJobApplicationRequestDTO> createJobApplicationValidator, IValidator<CreateInterviewRequestDTO> updateJobApplicationValidator)
    {
        _mediator = mediator;
        _createJobApplicationValidator = createJobApplicationValidator;
        _updateJobApplicationValidator = updateJobApplicationValidator;
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create(CreateJobApplicationRequestDTO request)
    {
        var validation = _createJobApplicationValidator.Validate(request);
        
        if (!validation.IsValid)
        {
            return BadRequest(ApiErrorService.FluentToApiErrors(validationFailures: validation.Errors, []));
        }

        var id = Guid.NewGuid();

        var command = new CreateJobApplicationCommand(
            id: id,
            url: request.Url,
            resume: request.Resume,
            title: request.Title,
            company: request.Company,
            datePublished: request.DatePublished
        );
        var result = await _mediator.Send(command);

        if (result.IsError())
        {
            return BadRequest(ApiErrorService.MapApplicationErrors(errors: result.GetError()));
        }
        
        return StatusCode(StatusCodes.Status201Created, new CreateJobApplicationResponseDTO(id: id.ToString()));
    }

    [HttpPost("{jobApplicationId}/create-interview")]
    public async Task<IActionResult> ScheduleInterview(Guid jobApplicationId, CreateInterviewRequestDTO request)
    {
        var validation = _updateJobApplicationValidator.Validate(request);
        
        if (!validation.IsValid)
        {
            return BadRequest(ApiErrorService.FluentToApiErrors(validationFailures: validation.Errors, []));
        }

        var id = Guid.NewGuid();

        var command = new CreateInterviewCommand(
            id: id,
            venue: request.Venue,
            status: request.Status,
            dateScheduled: request.DateScheduled,
            dateStarted: request.DateStarted,
            dateFinished: request.DateFinished,
            jobApplicationId: jobApplicationId,
            interviewer: request.Interviewer
        );
        var result = await _mediator.Send(command);

        if (result.IsError())
        {
            return BadRequest(ApiErrorService.MapApplicationErrors(errors: result.GetError()));
        }
        
        return StatusCode(StatusCodes.Status201Created, new CreateJobApplicationResponseDTO(id: id.ToString()));
    }
}