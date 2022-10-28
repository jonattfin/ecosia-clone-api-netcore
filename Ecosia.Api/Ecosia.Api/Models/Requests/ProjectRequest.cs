using System.ComponentModel.DataAnnotations;

namespace Ecosia.Api.Models.Requests;

public class GetProjectsRequest
{
    public int PageSize { get; }

    public int PageIndex { get; }

    public GetProjectsRequest(int pageSize, int pageIndex)
    {
        PageSize = pageSize;
        PageIndex = pageIndex;
    }
}


public class GetProjectRequest
{
    public Guid Id { get; }

    public GetProjectRequest(Guid id)
    {
        Id = id;
    }
}

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

public class DeleteProjectRequest
{
    
}