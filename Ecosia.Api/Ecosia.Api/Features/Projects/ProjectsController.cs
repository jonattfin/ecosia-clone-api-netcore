using AutoMapper;
using Ecosia.Api.Domain.Features.Projects.Handlers;
using Ecosia.Api.Domain.Features.Projects.Models;
using Ecosia.Api.Features.Projects.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ecosia.Api.Features.Projects;

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
    public async Task<IActionResult> Get(int pageIndex = 0, int pageSize = 5)
    {
        var (projects, numberOfPages) =
            await _mediator.Send(new GetProjectsQuery
            {
                PageIndex = pageIndex,
                PageSize = pageSize
            });

        var response = new ProjectsResponse()
        {
            Projects = projects.Select(p => _mapper.Map<ProjectResponse>(p)),
            NumberOfPages = numberOfPages,
            PageIndex = pageIndex,
            PageSize = pageSize
        };

        return Ok(response);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var project = await _mediator.Send(new GetProjectQuery { ProjectId = id });
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
        var project = await _mediator.Send(new GetProjectQuery {ProjectId = id});
        if (project is null)
        {
            return NotFound();
        }

        await _mediator.Send(new DeleteProjectCommand() { ProjectId = id });
        return Ok();
    }
}