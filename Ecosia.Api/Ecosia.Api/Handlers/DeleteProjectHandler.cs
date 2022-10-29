using Ecosia.Api.Repositories;
using MediatR;

namespace Ecosia.Api.Handlers;

public class DeleteProjectHandler : IRequestHandler<DeleteProjectCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteProjectHandler(IUnitOfWork unitOfWork) => unitOfWork = unitOfWork;
    
    
    public async Task<bool> Handle(DeleteProjectCommand command, CancellationToken cancellationToken)
    {
        var result = await _unitOfWork.ProjectRepository.DeleteAsync(command.Id);
        await _unitOfWork.SaveChangesAsync();
        
        return result;
    }
}


public class DeleteProjectCommand : IRequest<bool>
{
    public Guid Id { get; set; }
}
