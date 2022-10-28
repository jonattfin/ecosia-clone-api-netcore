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

    public async Task<IEnumerable<Project>> Get() => await Task.FromResult(_projects);

    public async Task<Project?> GetById(Guid id) => await Task.FromResult(_projects.FirstOrDefault(p => p.Id == id));

    public async Task<bool> Delete(Guid id)
    {
        var project = await GetById(id);
        if (project is null)
        {
            return await Task.FromResult(false);
        }
        
        return await Task.FromResult(_projects.Remove(project));
    }

    public async Task Update(Guid id, Project project)
    {
        var existingProject = await GetById(id);
        if (existingProject is not null)
        {
            existingProject.Name = project.Name;
        }
    }

    public async Task Add(Project project)
    {
        _projects.Add(project);
    }
}