using Ecosia.Api.Domain.Features.Projects.Models;
using Ecosia.Api.Domain.Features.Shared.Handlers;
using Ecosia.Api.Domain.Repositories;
using MediatR;

namespace Ecosia.Api.Domain.Features.Projects.Handlers;

public class GetProjectsRequestHandler : BaseRequestHandler<GetProjectsQuery, (IEnumerable<Project>, int)>
{
    public GetProjectsRequestHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public override async Task<(IEnumerable<Project>, int)> Handle(GetProjectsQuery query, CancellationToken cancellationToken)
    {
        return await UnitOfWork.ProjectRepository.GetAsync(query.PageNumber, query.PageSize);
    }
}

public record GetProjectsQuery(int PageNumber, int PageSize) : IRequest<(IEnumerable<Project>, int)>;