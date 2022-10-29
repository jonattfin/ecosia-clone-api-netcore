using Ecosia.Api.Models.Domain;

namespace Ecosia.Api.Repositories;

public interface IRepository<T>
{
    Task<IEnumerable<T>> GetAsync();

    Task<T?> GetByIdAsync(Guid id);

    Task<bool> DeleteAsync(Guid id);

    Task UpdateAsync(Guid id, Project project);

    Task<Project> AddAsync(Project project);

    Task<bool> ExistsAsync(Guid id);

    Task SaveChangesAsync();
}