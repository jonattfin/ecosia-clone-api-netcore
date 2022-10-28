using Ecosia.Api.Models;
using Ecosia.Api.Models.Domain;
using Ecosia.Api.Models.Requests;
using Ecosia.Api.Models.Responses;
using Ecosia.Api.Repositories;

namespace Ecosia.Api.Services;

public interface IProjectService
{
    Task<IEnumerable<ProjectResponse>> GetAsync(GetProjectsRequest request);
    
    Task<ProjectResponse?> GetByIdAsync(GetProjectRequest request);
    
    Task UpdateAsync(Guid id, UpdateProjectRequest request);
    
    Task<ProjectResponse> AddAsync(AddProjectRequest request);
    
    Task<bool> DeleteAsync(Guid id);

    Task<bool> ExistsAsync(Guid id);

}

public class ProjectService : IProjectService
{
    private readonly IRepository<Project> _repository;

    public ProjectService(IRepository<Project> repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<ProjectResponse>> GetAsync(GetProjectsRequest request)
    {
        var projects = await _repository.GetAsync();
        return projects.Select(MapToResponse);
    }

    public async Task<ProjectResponse?> GetByIdAsync(GetProjectRequest request)
    {
        var project = await _repository.GetByIdAsync(request.Id);
        return MapToResponse(project);
    }

    public async Task<bool> DeleteAsync(Guid id) => await _repository.DeleteAsync(id);
    public async Task<bool> ExistsAsync(Guid id) => await _repository.ExistsAsync(id);

    public async Task UpdateAsync(Guid id, UpdateProjectRequest request)
    {
        var project = MapFromUpdateRequest(request);
        await _repository.UpdateAsync(id, project);
    }
    public async Task<ProjectResponse?> AddAsync(AddProjectRequest request)
    {
        var project = MapFromAddRequest(request);
        await _repository.AddAsync(project);

        return MapToResponse(project);
    }


    private static Project MapFromUpdateRequest(UpdateProjectRequest request)
    {
        return new Project()
        {
            Id = Guid.NewGuid(),
            Name = request.Name
        };
    }
    
     private static Project MapFromAddRequest(AddProjectRequest request)
    {
        return new Project()
        {
            Id = Guid.NewGuid(),
            Name = request.Name
        };
    }

    private static ProjectResponse? MapToResponse(Project? project)
    {
        if (project is null)
        {
            return null;
        }
        
        return new ProjectResponse()
        {
            Id = project.Id,
            Name = project.Name
        };
    }
}