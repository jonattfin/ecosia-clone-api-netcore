using Ecosia.Api.Models.Requests;
using Ecosia.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Ecosia.Api.Controllers;

[Route("api/projects")]
[ApiController]
public class ProjectsController : ControllerBase
{
    private readonly IProjectService _projectService;

    public ProjectsController(IProjectService projectService)
    {
        _projectService = projectService;
    }

    [HttpGet]
    public async Task<IActionResult> Get(int pageSize = 10, int pageIndex = 0)
    {
        var request = new GetProjectsRequest(pageSize, pageIndex);
        var projects = await _projectService.GetAsync(request);
        return Ok(projects);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var request = new GetProjectRequest(id);
        var project = await _projectService.GetByIdAsync(request);
        if (project is null)
        {
            return NotFound();
        }

        return Ok(project);
    }

    [HttpPost]
    public async Task<IActionResult> Post(AddProjectRequest request)
    {
        var project = await _projectService.AddAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = project.Id }, null);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Put(Guid id, UpdateProjectRequest request)
    {
        var found = await _projectService.ExistsAsync(id);
        if (!found)
        {
            return NotFound();
        }

        await _projectService.UpdateAsync(id, request);
        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(Guid id)
    {
        var found = await _projectService.ExistsAsync(id);
        if (!found)
        {
            return NotFound();
        }

        await _projectService.DeleteAsync(id);
        return Ok();
    }
}