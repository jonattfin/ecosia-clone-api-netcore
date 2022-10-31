using Ecosia.Api.Domain.Models;
using Ecosia.Api.Domain.Repositories;
using Ecosia.Api.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Ecosia.Api.Persistence.Repositories;

public class ProjectRepository : IRepository<Project>
{
    private readonly EcosiaDbContext _context;

    public ProjectRepository(EcosiaDbContext context)
    {
        _context = context;

        // TODO
        _context.Database.EnsureCreated();
    }

    public async Task<(IEnumerable<Project>, int)> GetAsync(int pageIndex, int pageSize)
    {
        var projects = await _context.Projects.AsNoTracking()
            .OrderBy(p => p.Name)
            .Skip(pageIndex * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var numberOfPages = await _context.Projects.AsNoTracking().CountAsync() / pageSize;

        return (projects, numberOfPages);
    }


    public async Task<Project?> GetByIdAsync(Guid id) =>
        await _context.Projects.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);

    public async Task<bool> DeleteAsync(Guid id)
    {
        var project = await GetByIdAsync(id);
        if (project is null)
        {
            return await Task.FromResult(false);
        }

        return await Task.FromResult(_context.Projects.Remove(project) != null);
    }

    public async Task<Project> UpdateAsync(Project project)
    {
        var existingProject = await _context.Projects.FirstOrDefaultAsync(p => p.Id == p.Id);
        if (existingProject is not null)
        {
            existingProject.Name = project.Name;
        }

        return project;
    }

    public async Task<Project> AddAsync(Project project)
    {
        var projectEntity = await _context.Projects.AddAsync(project);
        return projectEntity.Entity;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}