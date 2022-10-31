using Ecosia.Api.Domain.Features.Projects.Models;
using Ecosia.Api.Domain.Repositories;
using MediatR;

namespace Ecosia.Api.Domain.Features.Projects.Handlers;

public class GetProjectHandler : BaseHandler<GetProjectQuery, Project>
{
    public GetProjectHandler(IUnitOfWork unitOfWork): base(unitOfWork)
    {
    }

    public override async Task<Project> Handle(GetProjectQuery query, CancellationToken cancellationToken)
    {
        return await UnitOfWork.ProjectRepository.GetByIdAsync(query.ProjectId);
    }
}

public class GetProjectQuery : IRequest<Project>
{
    public Guid ProjectId { get; init; }
}