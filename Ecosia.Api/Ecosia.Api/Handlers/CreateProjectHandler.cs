using Ecosia.Api.Models.Requests;
using Ecosia.Api.Models.Responses;
using Ecosia.Api.Services;
using MediatR;

namespace Ecosia.Api.Handlers;

public class CreateProjectHandler : IRequestHandler<CreateProjectCommand, CreateProjectResponse>
{
    private readonly IProjectService _projectService;

    public CreateProjectHandler(IProjectService projectService)
    {
        _projectService = projectService;
    }
    
    public async Task<CreateProjectResponse> Handle(CreateProjectCommand command, CancellationToken cancellationToken)
    {
        var response = await _projectService.AddAsync(command.Request);
        return new CreateProjectResponse() { Project = response };
    }
}


public class CreateProjectCommand : IRequest<CreateProjectResponse>
{
    public AddProjectRequest Request { get; set; }
}

public class CreateProjectResponse
{
    public ProjectResponse Project { get; set; }
}

