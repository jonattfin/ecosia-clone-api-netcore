using Ecosia.Api.Domain.Features.Shared.Handlers;
using Ecosia.Api.Domain.Repositories;
using MediatR;

namespace Ecosia.Api.Domain.Features.Projects.Handlers;

public class DeleteProjectRequestHandler : BaseRequestHandler<DeleteProjectCommand, bool>
{
    public DeleteProjectRequestHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }
    
    public override async Task<bool> Handle(DeleteProjectCommand query, CancellationToken cancellationToken)
    {
        var result = await UnitOfWork.ProjectRepository.DeleteAsync(query.ProjectId);
        await UnitOfWork.SaveChangesAsync();
        
        return result;
    }
}


public record DeleteProjectCommand(Guid ProjectId) : IRequest<bool>;