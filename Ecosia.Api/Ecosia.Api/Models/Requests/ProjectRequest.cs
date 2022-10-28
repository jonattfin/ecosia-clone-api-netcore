using System.ComponentModel.DataAnnotations;

namespace Ecosia.Api.Models.Requests;

public class ProjectRequest
{
    [Required]
    [MinLength(5)]
    public string Name { get; set; }
}