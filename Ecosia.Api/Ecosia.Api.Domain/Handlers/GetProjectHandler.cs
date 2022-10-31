using Ecosia.Api.Domain.Models;
using Ecosia.Api.Domain.Repositories;
using MediatR;

namespace Ecosia.Api.Domain.Handlers;

public class GetProjectHandler : BaseHandler<GetProjectQuery, Project>
{
    public GetProjectHandler(IUnitOfWork unitOfWork): base(unitOfWork)
    {
    }

    public override async Task<Project> Handle(GetProjectQuery query, CancellationToken cancellationToken)
    {
        return await _unitOfWork.ProjectRepository.GetByIdAsync(query.ProjectId);
    }
}

public class GetProjectQuery : IRequest<Project>
{
    public Guid ProjectId { get; }

    public GetProjectQuery(Guid projectId)
    {
        ProjectId = projectId;
    }
}