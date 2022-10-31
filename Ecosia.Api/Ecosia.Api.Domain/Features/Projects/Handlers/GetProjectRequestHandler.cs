using Ecosia.Api.Domain.Features.Projects.Models;
using Ecosia.Api.Domain.Features.Shared.Handlers;
using Ecosia.Api.Domain.Repositories;
using MediatR;

namespace Ecosia.Api.Domain.Features.Projects.Handlers;

public class GetProjectRequestHandler : BaseRequestHandler<GetProjectQuery, Project>
{
    public GetProjectRequestHandler(IUnitOfWork unitOfWork): base(unitOfWork)
    {
    }

    public override async Task<Project> Handle(GetProjectQuery query, CancellationToken cancellationToken)
    {
        return await UnitOfWork.ProjectRepository.GetByIdAsync(query.ProjectId);
    }
}

public record GetProjectQuery(Guid ProjectId) : IRequest<Project>;