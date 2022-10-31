namespace Ecosia.Api.Domain.Repositories;

public interface IRepository<T>
{
    Task<(IEnumerable<T>, int)> GetAsync(int pageNumber, int pageSize);

    Task<T?> GetByIdAsync(Guid id);

    Task<bool> DeleteAsync(Guid id);

    Task<T> UpdateAsync(T t);

    Task<T> AddAsync(T t);

    Task SaveChangesAsync();
}