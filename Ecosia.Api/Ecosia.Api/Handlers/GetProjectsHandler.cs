using Ecosia.Api.Models.Domain;
using Ecosia.Api.Models.Requests;
using Ecosia.Api.Repositories;
using MediatR;

namespace Ecosia.Api.Handlers;

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