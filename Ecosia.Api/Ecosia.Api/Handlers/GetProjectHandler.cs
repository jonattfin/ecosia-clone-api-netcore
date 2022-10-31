using Ecosia.Api.Models.Domain;
using Ecosia.Api.Models.Requests;
using Ecosia.Api.Repositories;
using MediatR;

namespace Ecosia.Api.Handlers;

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