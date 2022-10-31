using AutoMapper;
using Ecosia.Api.Domain.Features.Projects.Models;
using Ecosia.Api.Domain.Repositories;
using Ecosia.Api.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Ecosia.Api.Persistence.Repositories;

public class ProjectRepository : IRepository<Project>
{
    private readonly EcosiaDbContext _context;
    private readonly IMapper _mapper;

    public ProjectRepository(EcosiaDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;

        // TODO
        _context.Database.EnsureCreated();
    }

    public async Task<(IEnumerable<Project>, int)> GetAsync(int pageNumber, int pageSize)
    {
        var projectsEntities = await _context.Projects.AsNoTracking()
            .OrderBy(p => p.Name)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var numberOfPages = await _context.Projects.AsNoTracking().CountAsync() / pageSize;

        return (_mapper.Map<IEnumerable<Project>>(projectsEntities), numberOfPages);
    }


    public async Task<Project?> GetByIdAsync(Guid id)
    {
        var projectEntity = await _context.Projects.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        return _mapper.Map<Project>(projectEntity);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var projectEntity = await _context.Projects.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        if (projectEntity is null)
        {
            return await Task.FromResult(false);
        }

        return await Task.FromResult(_context.Projects.Remove(projectEntity) != null);
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
        var projectEntity = _mapper.Map<Entities.ProjectEntity>(project);
        var newProjectEntity = await _context.Projects.AddAsync(projectEntity);
        
        return _mapper.Map<Project>(newProjectEntity.Entity);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}