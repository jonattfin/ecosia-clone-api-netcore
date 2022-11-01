using AutoMapper;
using Ecosia.Api.Domain.Features.Projects.Handlers;
using Ecosia.Api.Domain.Features.Projects.Models;
using Ecosia.Api.Features.Projects.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ecosia.Api.Features.Projects;

[ApiController]
// [Authorize]
[Route("api/projects")]
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
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Get(int pageNumber = 1, int pageSize = 5)
    {
        var (projects, numberOfPages) =
            await _mediator.Send(new GetProjectsQuery(pageNumber, pageSize));

        var response = new ProjectsResponse(
            projects.Select(p => _mapper.Map<ProjectResponse>(p)),
            numberOfPages,
            pageNumber,
            pageSize);

        return Ok(response);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
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
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> Post(AddProjectRequest request)
    {
        var project = await _mediator.Send(new CreateProjectCommand(_mapper.Map<Project>(request)));

        await _mediator.Publish(new ProjectAddedNotification(project));

        return CreatedAtAction(nameof(GetById), new { id = project.Id }, null);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Put(Guid id, JsonPatchDocument<UpdateProjectRequest> request)
    {
        var project = await _mediator.Send(new GetProjectQuery(id));
        if (project is null)
        {
            return NotFound();
        }

        var updateProjectRequest = _mapper.Map<UpdateProjectRequest>(project);
        request.ApplyTo(updateProjectRequest);

        var updatedProject = _mapper.Map<Project>(updateProjectRequest);
        
        await _mediator.Send(new UpdateProjectCommand(updatedProject));

        return Ok();
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var project = await _mediator.Send(new GetProjectQuery(id));
        if (project is null)
        {
            return NotFound();
        }

        await _mediator.Send(new DeleteProjectCommand(id));
        return Ok();
    }
}





