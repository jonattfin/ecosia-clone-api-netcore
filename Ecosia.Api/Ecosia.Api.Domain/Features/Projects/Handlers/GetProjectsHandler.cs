using Ecosia.Api.Domain.Features.Projects.Models;
using Ecosia.Api.Domain.Repositories;
using MediatR;

namespace Ecosia.Api.Domain.Features.Projects.Handlers;

public class GetProjectsHandler : BaseHandler<GetProjectsQuery, (IEnumerable<Project>, int)>
{
    public GetProjectsHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public override async Task<(IEnumerable<Project>, int)> Handle(GetProjectsQuery request, CancellationToken cancellationToken)
    {
        return await UnitOfWork.ProjectRepository.GetAsync(request.PageIndex, request.PageSize);
    }
}

public class GetProjectsQuery : IRequest<(IEnumerable<Project>, int)>
{
    public int PageIndex { get; init; }
    
    public int PageSize { get; init; }
}