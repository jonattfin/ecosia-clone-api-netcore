using Ecosia.Api.Models.Domain;
using Ecosia.Api.Models.Requests;
using Ecosia.Api.Repositories;
using MediatR;

namespace Ecosia.Api.Handlers;

public class GetProjectsHandler : IRequestHandler<GetProjectsQuery, IEnumerable<Project>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetProjectsHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<IEnumerable<Project>> Handle(GetProjectsQuery query, CancellationToken cancellationToken)
    {
        return await _unitOfWork.ProjectRepository.GetAsync(query.PageIndex, query.PageSize);
    }
}

public class GetProjectsQuery : IRequest<IEnumerable<Project>>
{
    public int PageIndex { get; }
    
    public int PageSize { get; }

    public GetProjectsQuery( int pageIndex, int pageSize)
    {
        PageSize = pageSize;
        PageIndex = pageIndex;
    }
}