using System.ComponentModel.DataAnnotations.Schema;

namespace Ecosia.Api.Persistence.Entities;

[Table("tags")]
public class TagEntity
{
    public Guid Id { get; set; }

    public string Title { get; set; }
    
    public string Subtitle { get; set; }
    
    public ProjectEntity Project { get; set; }
    
    public Guid ProjectId { get; set; }
}