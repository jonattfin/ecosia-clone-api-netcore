using Ecosia.Api.Handlers;
using Ecosia.Api.Models.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ecosia.Api.Controllers;

[Route("api/projects")]
[ApiController]
public class ProjectsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProjectsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get(int pageSize = 10, int pageIndex = 0)
    {
        var request = new GetProjectsRequest(pageSize, pageIndex);
        var projects = await _mediator.Send(new GetProjectsCommand() { Request = request });
        return Ok(projects);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var request = new GetProjectRequest(id);
        var project = await _mediator.Send(new GetProjectCommand() { Request = request });
        if (project is null)
        {
            return NotFound();
        }

        return Ok(project);
    }

    [HttpPost]
    public async Task<IActionResult> Post(AddProjectRequest request)
    {
        var response = await _mediator.Send(new CreateProjectCommand { Request = request });
        return CreatedAtAction(nameof(GetById), new { id = response.Project.Id }, null);
    }

    // [HttpPut("{id:guid}")]
    // public async Task<IActionResult> Put(Guid id, UpdateProjectRequest request)
    // {
    //     var found = await _projectService.ExistsAsync(id);
    //     if (!found)
    //     {
    //         return NotFound();
    //     }
    //
    //     await _projectService.UpdateAsync(id, request);
    //     return Ok();
    // }

    [HttpDelete]
    public async Task<IActionResult> Delete(Guid id)
    {
        // var found = await _projectService.ExistsAsync(id);
        // if (!found)
        // {
        //     return NotFound();
        // }

        await _mediator.Send(new DeleteProjectCommand() { Id = id });
        return Ok();
    }
}