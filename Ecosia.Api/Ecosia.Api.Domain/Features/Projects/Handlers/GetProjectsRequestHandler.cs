using Ecosia.Api.Domain.Features.Projects.Models;
using Ecosia.Api.Domain.Repositories;
using MediatR;

namespace Ecosia.Api.Domain.Features.Projects.Handlers;

public class GetProjectsRequestHandler : BaseRequestHandler<GetProjectsQuery, (IEnumerable<Project>, int)>
{
    public GetProjectsRequestHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public override async Task<(IEnumerable<Project>, int)> Handle(GetProjectsQuery request, CancellationToken cancellationToken)
    {
        return await UnitOfWork.ProjectRepository.GetAsync(request.PageIndex, request.PageSize);
    }
}

public record GetProjectsQuery(int PageIndex, int PageSize) : IRequest<(IEnumerable<Project>, int)>;