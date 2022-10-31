namespace Ecosia.Api.Models.Responses;

public class ProjectResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}

public class ProjectsResponse
{
    public IEnumerable<ProjectResponse> Projects { get; set; }

    public int NumberOfPages { get; set; }
}