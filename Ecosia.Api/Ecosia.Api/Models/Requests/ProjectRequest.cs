using System.ComponentModel.DataAnnotations;

namespace Ecosia.Api.Models.Requests;

public class AddProjectRequest
{
    [Required]
    [MinLength(5)]
    public string Name { get; set; }
}

public class UpdateProjectRequest
{
    [Required]
    [MinLength(5)]
    public string Name { get; set; }
}
