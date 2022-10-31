using Ecosia.Api.Domain.Features.Projects.Models;
using Ecosia.Api.Domain.Features.Shared.Handlers;
using Ecosia.Api.Domain.Repositories;
using MediatR;

namespace Ecosia.Api.Domain.Features.Projects.Handlers;

public class UpdateProjectRequestHandler : BaseRequestHandler<UpdateProjectCommand, Project>
{
    public UpdateProjectRequestHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public override async Task<Project> Handle(UpdateProjectCommand query, CancellationToken cancellationToken)
    {
        var project = await UnitOfWork.ProjectRepository.UpdateAsync(query.Project);
        await UnitOfWork.SaveChangesAsync();

        return project;
    }
}

public record UpdateProjectCommand(Project Project) : IRequest<Project>;