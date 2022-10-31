using Ecosia.Api.Domain.Models;

namespace Ecosia.Api.Domain.Repositories;

public interface IUnitOfWork
{
    IRepository<Project> ProjectRepository { get; }
    Task SaveChangesAsync();
}

public class UnitOfWork : IUnitOfWork
{
    public IRepository<Project> ProjectRepository { get; }

    public UnitOfWork(IRepository<Project> projectRepository)
    {
        ProjectRepository = projectRepository;
    }

    public async Task SaveChangesAsync()
    {
        await ProjectRepository.SaveChangesAsync();
    }
}