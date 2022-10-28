using Ecosia.Api.Models;
using Ecosia.Api.Models.Domain;
using Ecosia.Api.Models.Requests;
using Ecosia.Api.Models.Responses;
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
    public async Task<IActionResult> Get()
    {
        var projects = (await _projectService.Get()).Select(MapToResponse);
        return Ok(projects);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var project = await _projectService.GetById(id);
        if (project is null)
        {
            return NotFound();
        }

        return Ok(MapToResponse(project));
    }

    [HttpPost]
    public async Task<IActionResult> Post(ProjectRequest request)
    {
        var project = MapFromRequest(request);

        await _projectService.Add(project);
        return CreatedAtAction(nameof(GetById), new { id = project.Id }, null);
    }


    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Put(Guid id, ProjectRequest request)
    {
        var project = await _projectService.GetById(id);
        if (project is null)
        {
            return NotFound();
        }

        project.Name = request.Name;

        await _projectService.Update(id, project);
        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(Guid id)
    {
        var project = await _projectService.GetById(id);
        if (project is null)
        {
            return NotFound();
        }
        
        await _projectService.Delete(id);
        return Ok();
    }

    private static Project MapFromRequest(ProjectRequest request)
    {
        return new Project()
        {
            Id = Guid.NewGuid(),
            Name = request.Name
        };
    }

    private static ProjectResponse MapToResponse(Project project)
    {
        return new ProjectResponse()
        {
            Id = project.Id,
            Name = project.Name
        };
    }
}