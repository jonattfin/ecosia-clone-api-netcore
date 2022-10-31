using AutoMapper;
using Ecosia.Api.Handlers;
using Ecosia.Api.Models.Domain;
using Ecosia.Api.Models.Requests;
using Ecosia.Api.Models.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ecosia.Api.Controllers;

[Route("api/projects")]
[ApiController]
public class ProjectsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public ProjectsController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> Get( int pageIndex = 0, int pageSize = 10)
    {
        var projects = await _mediator.Send(new GetProjectsQuery(pageIndex, pageSize));
        return Ok(_mapper.Map<IEnumerable<ProjectResponse>>(projects));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var project = await _mediator.Send(new GetProjectQuery(id));
        if (project is null)
        {
            return NotFound();
        }

        return Ok(_mapper.Map<ProjectResponse>(project));
    }

    [HttpPost]
    public async Task<IActionResult> Post(AddProjectRequest request)
    {
        var project = await _mediator.Send(new CreateProjectCommand
        {
            Project = _mapper.Map<Project>(request)
        });

        return CreatedAtAction(nameof(GetById), new { id = project.Id }, null);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Put(Guid id, UpdateProjectRequest request)
    {
        await _mediator.Send(new UpdateProjectCommand
        {
            Project = _mapper.Map<Project>(request)
        });

        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(Guid id)
    {
        var project = await _mediator.Send(new GetProjectQuery(id));
        if (project is null)
        {
            return NotFound();
        }

        await _mediator.Send(new DeleteProjectCommand() { Id = id });
        return Ok();
    }
}