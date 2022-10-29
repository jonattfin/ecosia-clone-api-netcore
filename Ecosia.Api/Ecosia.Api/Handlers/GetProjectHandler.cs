using Ecosia.Api.Models.Domain;
using Ecosia.Api.Models.Requests;
using Ecosia.Api.Repositories;
using MediatR;

namespace Ecosia.Api.Handlers;

public class GetProjectHandler : IRequestHandler<GetProjectQuery, Project>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetProjectHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<Project> Handle(GetProjectQuery query, CancellationToken cancellationToken)
    {
        return await _unitOfWork.ProjectRepository.GetByIdAsync(query.Request.Id);
    }
}

public class GetProjectQuery : IRequest<Project>
{
    public GetProjectRequest Request { get; init; }
}