using Ecosia.Api.Models;
using Ecosia.Api.Models.Domain;

namespace Ecosia.Api.Repositories;

public interface IRepository<T>
{
    Task<IEnumerable<T>> Get();
    Task<T?> GetById(Guid id);
    Task<bool> Delete(Guid id);
    Task Update(Guid id, Project project);
    Task Add(Project project);
}