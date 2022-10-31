using Ecosia.Api.Repositories;
using MediatR;

namespace Ecosia.Api.Handlers;

public class DeleteProjectHandler : BaseHandler<DeleteProjectCommand, bool>
{
    public DeleteProjectHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }
    
    public override async Task<bool> Handle(DeleteProjectCommand command, CancellationToken cancellationToken)
    {
        var result = await _unitOfWork.ProjectRepository.DeleteAsync(command.ProjectId);
        await _unitOfWork.SaveChangesAsync();
        
        return result;
    }
}


public class DeleteProjectCommand : IRequest<bool>
{
    public Guid ProjectId { get; set; }
}
