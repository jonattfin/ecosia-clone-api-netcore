using Ecosia.Api.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Ecosia.Api.Contexts;

public class EcosiaDbContext : DbContext
{
    public DbSet<Project> Projects { get; set; }

    public EcosiaDbContext(DbContextOptions<EcosiaDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var projects = GenerateProjects();
        modelBuilder.Entity<Project>().HasData(projects);
    }

    private static IEnumerable<Project> GenerateProjects()
    {
        return Enumerable.Range(1, 20)
            .Select(element => new Project() { Id = Guid.NewGuid(), Name = $"Name {element}" });
    }
}