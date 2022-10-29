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
        modelBuilder.Entity<Project>().HasData(
            new Project { Id = Guid.NewGuid(), Name = "Michelotti" },
            new Project { Id = Guid.NewGuid(), Name = "Gates" },
            new Project { Id = Guid.NewGuid(), Name = "Nadella" });
    }
}