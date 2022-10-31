using Ecosia.Api.Models.Domain;

namespace Ecosia.Api.Repositories;

public interface IRepository<T>
{
    Task<IEnumerable<T>> GetAsync(int pageIndex, int pageSize);

    Task<T?> GetByIdAsync(Guid id);

    Task<bool> DeleteAsync(Guid id);

    Task<T> UpdateAsync(T t);

    Task<T> AddAsync(T t);

    Task SaveChangesAsync();
}