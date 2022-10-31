using Ecosia.Api.Domain.Features.Projects.Models;
using Ecosia.Api.Domain.Features.Shared.Handlers;
using Ecosia.Api.Domain.Repositories;
using MediatR;

namespace Ecosia.Api.Domain.Features.Projects.Handlers;

public class CreateProjectRequestHandler : BaseRequestHandler<CreateProjectCommand, Project>
{
    public CreateProjectRequestHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public override async Task<Project> Handle(CreateProjectCommand query, CancellationToken cancellationToken)
    {
        var project = await UnitOfWork.ProjectRepository.AddAsync(query.Project);
        await UnitOfWork.SaveChangesAsync();

        return project;
    }
}

public record CreateProjectCommand(Project Project) : IRequest<Project>;