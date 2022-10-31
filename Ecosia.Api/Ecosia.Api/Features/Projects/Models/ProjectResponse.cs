using Ecosia.Api.Domain.Features.Projects.Models;

namespace Ecosia.Api.Features.Projects.Models;

public class ProjectResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    
    public string Scope { get; set; }
    
    public string Description { get; set; }
    
    public string Title { get; set; }
    
    public int? TreesPlanted { get; set; }
    
    public int? HectaresRestored { get; set; }
    
    public int? YearSince { get; set; }

    public string ImageUrl { get; set; }

    public IEnumerable<Tag> Tags { get; set; }
};