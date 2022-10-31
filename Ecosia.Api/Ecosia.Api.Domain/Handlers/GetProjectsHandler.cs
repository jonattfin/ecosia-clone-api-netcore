using Ecosia.Api.Domain.Models;
using Ecosia.Api.Domain.Repositories;
using MediatR;

namespace Ecosia.Api.Domain.Handlers;

public class GetProjectsHandler : BaseHandler<GetProjectsQuery, (IEnumerable<Project>, int)>
{
    public GetProjectsHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public override async Task<(IEnumerable<Project>, int)> Handle(GetProjectsQuery request, CancellationToken cancellationToken)
    {
        return await _unitOfWork.ProjectRepository.GetAsync(request.PageIndex, request.PageSize);
    }
}

public class GetProjectsQuery : IRequest<(IEnumerable<Project>, int)>
{
    public int PageIndex { get; }
    
    public int PageSize { get; }

    public GetProjectsQuery( int pageIndex, int pageSize)
    {
        PageSize = pageSize;
        PageIndex = pageIndex;
    }
}