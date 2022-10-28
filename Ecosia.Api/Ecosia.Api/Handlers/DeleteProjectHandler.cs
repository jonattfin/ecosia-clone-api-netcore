using Ecosia.Api.Models.Responses;
using Ecosia.Api.Services;
using MediatR;

namespace Ecosia.Api.Handlers;

public class DeleteProjectHandler : IRequestHandler<DeleteProjectCommand, DeleteProjectResponse>
{
    private readonly IProjectService _projectService;

    public DeleteProjectHandler(IProjectService projectService)
    {
        _projectService = projectService;
    }
    
    public async Task<DeleteProjectResponse> Handle(DeleteProjectCommand command, CancellationToken cancellationToken)
    {
        var response = await _projectService.DeleteAsync(command.Id);
        return new DeleteProjectResponse() { Result = response };
    }
}


public class DeleteProjectCommand : IRequest<DeleteProjectResponse>
{
    public Guid Id { get; set; }
}

public class DeleteProjectResponse
{
    public bool Result { get; set; }
}