using Ecosia.Api.Models.Requests;
using Ecosia.Api.Models.Responses;
using Ecosia.Api.Services;
using MediatR;

namespace Ecosia.Api.Handlers;

public class GetProjectsHandler : IRequestHandler<GetProjectsCommand, GetProjectsResponse>
{
    private readonly IProjectService _projectService;

    public GetProjectsHandler(IProjectService projectService)
    {
        _projectService = projectService;
    }
    
    public async Task<GetProjectsResponse> Handle(GetProjectsCommand command, CancellationToken cancellationToken)
    {
        var response = await _projectService.GetAsync(command.Request);
        return new GetProjectsResponse() { Projects = response };
    }
}


public class GetProjectsCommand : IRequest<GetProjectsResponse>
{
    public GetProjectsRequest Request { get; set; }
}

public class GetProjectsResponse
{
    public IEnumerable<ProjectResponse> Projects { get; set; }
}