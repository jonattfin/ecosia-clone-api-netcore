using Ecosia.Api.Models.Domain;
using Ecosia.Api.Repositories;
using MediatR;

namespace Ecosia.Api.Handlers;

public class UpdateProjectHandler : IRequestHandler<UpdateProjectCommand, Project>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateProjectHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<Project> Handle(UpdateProjectCommand command, CancellationToken cancellationToken)
    {
        var project = await _unitOfWork.ProjectRepository.UpdateAsync(command.Project);
        await _unitOfWork.SaveChangesAsync();
        
        return project ;
    }
}

public class UpdateProjectCommand : IRequest<Project>
{
    public Project Project { get; set; }
}