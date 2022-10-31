namespace Ecosia.Api.Features.Projects.Models;

public class ProjectResponse
{
    public Guid Id { get; init; }
    public string Name { get; init; }
}

public class ProjectsResponse
{
    public IEnumerable<ProjectResponse> Projects { get; init; }

    public int NumberOfPages { get; init; }

    public int PageIndex { get; init; }

    public int PageSize { get; init; }
}