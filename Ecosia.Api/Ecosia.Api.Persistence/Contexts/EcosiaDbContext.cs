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
        modelBuilder.Entity<ProjectEntity>().HasData(projects);
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
                Tags = new List<TagEntity>()
                {
                    new()
                    {
                        Id = Guid.NewGuid(),
                        Title = "Tag Title 1",
                        Subtitle = "Tag Subtitle 1",
                    },
                    new()
                    {
                        Id = Guid.NewGuid(),
                        Title = "Tag Title 2",
                        Subtitle = "Tag Subtitle 2",
                    }
                }
            }).ToImmutableList();
    }
}