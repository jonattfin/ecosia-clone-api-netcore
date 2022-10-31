using Ecosia.Api.Domain.Features.Projects.Models;
using Ecosia.Api.Domain.Repositories;
using MediatR;

namespace Ecosia.Api.Domain.Features.Projects.Handlers;

public class CreateProjectHandler : BaseHandler<CreateProjectCommand, Project>
{
    public CreateProjectHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public override async Task<Project> Handle(CreateProjectCommand command, CancellationToken cancellationToken)
    {
        var project = await UnitOfWork.ProjectRepository.AddAsync(command.Project);
        await UnitOfWork.SaveChangesAsync();

        return project;
    }
}

public class CreateProjectCommand : IRequest<Project>
{
    public Project Project { get; init; }
}