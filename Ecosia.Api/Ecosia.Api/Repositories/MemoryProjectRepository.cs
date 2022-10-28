using Ecosia.Api.Models;
using Ecosia.Api.Models.Domain;

namespace Ecosia.Api.Repositories;

public class MemoryProjectRepository : IRepository<Project>
{
    private readonly IList<Project> _projects;

    public MemoryProjectRepository()
    {
        _projects = Enumerable.Range(1, 10).Select(_ =>
        {
            var id = Guid.NewGuid();

            return new Project()
            {
                Id = id,
                Name = $"name {id}"
            };
        }).ToList();
    }

    public async Task<IEnumerable<Project>> GetAsync() => await Task.FromResult(_projects);

    public async Task<Project?> GetByIdAsync(Guid id) => await Task.FromResult(_projects.FirstOrDefault(p => p.Id == id));

    public async Task<bool> DeleteAsync(Guid id)
    {
        var project = await GetByIdAsync(id);
        if (project is null)
        {
            return await Task.FromResult(false);
        }
        
        return await Task.FromResult(_projects.Remove(project));
    }

    public async Task UpdateAsync(Guid id, Project project)
    {
        var existingProject = await GetByIdAsync(id);
        if (existingProject is not null)
        {
            existingProject.Name = project.Name;
        }
    }

    public async Task AddAsync(Project project)
    {
        _projects.Add(project);
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        var project = _projects.FirstOrDefault(p => p.Id == id);
        return await Task.FromResult(project != null);
    }
}