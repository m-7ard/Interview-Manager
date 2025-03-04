using Api.Contracts.DTOs.JobApplications.Create;
using Api.Services;
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

    public ProductsController(ISender mediator, IValidator<CreateJobApplicationRequestDTO> createJobApplicationValidator)
    {
        _mediator = mediator;
        _createJobApplicationValidator = createJobApplicationValidator;
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

    [HttpPost("{id}/schedule-interview")]
    public async Task<IActionResult> ScheduleInterview(CreateJobApplicationRequestDTO request)
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
}