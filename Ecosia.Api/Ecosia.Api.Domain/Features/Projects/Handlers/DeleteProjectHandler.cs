using Ecosia.Api.Domain.Repositories;
using MediatR;

namespace Ecosia.Api.Domain.Features.Projects.Handlers;

public class DeleteProjectHandler : BaseHandler<DeleteProjectCommand, bool>
{
    public DeleteProjectHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }
    
    public override async Task<bool> Handle(DeleteProjectCommand command, CancellationToken cancellationToken)
    {
        var result = await UnitOfWork.ProjectRepository.DeleteAsync(command.ProjectId);
        await UnitOfWork.SaveChangesAsync();
        
        return result;
    }
}


public class DeleteProjectCommand : IRequest<bool>
{
    public Guid ProjectId { get; init; }
}
