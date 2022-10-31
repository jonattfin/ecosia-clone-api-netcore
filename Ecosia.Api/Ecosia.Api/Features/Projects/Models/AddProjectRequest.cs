using System.ComponentModel.DataAnnotations;

namespace Ecosia.Api.Features.Projects.Models;

public class AddProjectRequest
{
    [Required] 
    [MinLength(5)] 
    public string Name { get; set; }
    
    [Required] 
    public string Scope { get; set; }
    
    [Required]
    public string Description { get; set; }
    
    [Required]
    public string Title { get; set; }
    
    public int? TreesPlanted { get; set; }
    
    public int? HectaresRestored { get; set; }
    
    public int? YearSince { get; set; }
    
    public string ImageUrl { get; set; }
}