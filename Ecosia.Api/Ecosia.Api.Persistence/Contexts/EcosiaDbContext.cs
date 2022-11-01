using System.Collections.Immutable;
using Ecosia.Api.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ecosia.Api.Persistence.Contexts;

public class EcosiaDbContext : DbContext
{
    public DbSet<ProjectEntity> Projects { get; set; }

    public EcosiaDbContext(DbContextOptions<EcosiaDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var projects = GenerateProjects();
        var tags = GenerateTags(projects);
        
        modelBuilder.Entity<ProjectEntity>().HasData(projects);
        modelBuilder.Entity<TagEntity>().HasData(tags);
    }

    private static IEnumerable<TagEntity> GenerateTags(IEnumerable<ProjectEntity> projectEntities)
    {
        return projectEntities.Select(projectEntity => new TagEntity()
        {
            Id = Guid.NewGuid(),
            Title = "Tag Title 1",
            Subtitle = "Tag Subtitle 1",
            ProjectId = projectEntity.Id
        }).ToImmutableList();
    }

    private static IEnumerable<ProjectEntity> GenerateProjects()
    {
        var random = new Random();

        return Enumerable.Range(1, 20)
            .Select(element => new ProjectEntity()
            {
                Id = Guid.NewGuid(),
                Name = $"Name {element}",
                Description = $"Description {element}",
                Scope = $"Scope {element}",
                Title = $"Title {element}",
                ImageUrl = $"ImageUrl {element}",
                HectaresRestored = random.Next(100),
                TreesPlanted = random.Next(1000),
                YearSince = random.Next(2010, 2022),
            }).ToImmutableList();
    }
}