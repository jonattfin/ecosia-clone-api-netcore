using Ecosia.Api.Models.Domain;
using Ecosia.Api.Repositories;
using MediatR;

namespace Ecosia.Api.Handlers;

public class CreateProjectHandler : IRequestHandler<CreateProjectCommand, Project>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateProjectHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<Project> Handle(CreateProjectCommand command, CancellationToken cancellationToken)
    {
        var project = await _unitOfWork.ProjectRepository.AddAsync(command.Project);
        await _unitOfWork.SaveChangesAsync();
        
        return project ;
    }
}

public class CreateProjectCommand : IRequest<Project>
{
    public Project Project { get; set; }
}