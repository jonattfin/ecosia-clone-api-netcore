using Ecosia.Api.Domain.Models;
using Ecosia.Api.Domain.Repositories;
using MediatR;

namespace Ecosia.Api.Domain.Handlers;

public class UpdateProjectHandler : BaseHandler<UpdateProjectCommand, Project>
{
    public UpdateProjectHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public override async Task<Project> Handle(UpdateProjectCommand command, CancellationToken cancellationToken)
    {
        var project = await UnitOfWork.ProjectRepository.UpdateAsync(command.Project);
        await UnitOfWork.SaveChangesAsync();

        return project;
    }
}

public class UpdateProjectCommand : IRequest<Project>
{
    public Project Project { get; init; }
}