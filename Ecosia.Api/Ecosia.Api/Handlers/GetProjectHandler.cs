using Ecosia.Api.Models.Requests;
using Ecosia.Api.Models.Responses;
using Ecosia.Api.Services;
using MediatR;

namespace Ecosia.Api.Handlers;

public class GetProjectHandler : IRequestHandler<GetProjectCommand, GetProjectResponse>
{
    private readonly IProjectService _projectService;

    public GetProjectHandler(IProjectService projectService)
    {
        _projectService = projectService;
    }
    
    public async Task<GetProjectResponse> Handle(GetProjectCommand command, CancellationToken cancellationToken)
    {
        var response = await _projectService.GetByIdAsync(command.Request);
        return new GetProjectResponse() { Project = response };
    }
}


public class GetProjectCommand : IRequest<GetProjectResponse>
{
    public GetProjectRequest Request { get; set; }
}

public class GetProjectResponse
{
    public ProjectResponse Project { get; set; }
}