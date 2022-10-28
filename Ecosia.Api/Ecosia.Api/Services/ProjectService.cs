using Ecosia.Api.Models;
using Ecosia.Api.Models.Domain;
using Ecosia.Api.Repositories;

namespace Ecosia.Api.Services;

public interface IProjectService
{
    Task<IEnumerable<Project>> Get();
    Task<Project?> GetById(Guid id);
    Task<bool> Delete(Guid id);
    Task Update(Guid id, Project project);
    Task Add(Project project);
}

public class ProjectService : IProjectService
{
    private readonly IRepository<Project> _repository;

    public ProjectService(IRepository<Project> repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Project>> Get() => await _repository.Get();

    public async Task<Project?> GetById(Guid id) => await _repository.GetById(id);

    public async Task<bool> Delete(Guid id) => await _repository.Delete(id);

    public async Task Update(Guid id, Project project) => await _repository.Update(id, project);
    public async Task Add(Project project) => await _repository.Add(project);
}