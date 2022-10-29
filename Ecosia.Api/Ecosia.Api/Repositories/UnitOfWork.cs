using Ecosia.Api.Models.Domain;

namespace Ecosia.Api.Repositories;

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