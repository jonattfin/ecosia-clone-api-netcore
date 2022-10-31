using Ecosia.Api.Contexts;
using Ecosia.Api.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Ecosia.Api.Repositories;

public class ProjectRepository : IRepository<Project>
{
    private readonly EcosiaDbContext _context;

    public ProjectRepository(EcosiaDbContext context)
    {
        _context = context;

        // TODO
        _context.Database.EnsureCreated();
    }

    public async Task<IEnumerable<Project>> GetAsync(int pageIndex = 0, int pageSize = 10)
    {
        var projects = await _context.Projects.AsNoTracking()
            .OrderBy(p => p.Name)
            .Skip(pageIndex * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return projects;
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