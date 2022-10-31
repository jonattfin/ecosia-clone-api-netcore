namespace Ecosia.Api.Features.Projects.Models;

public record ProjectsResponse(IEnumerable<ProjectResponse> Projects, int NumberOfPages, int PageIndex, int PageSize);