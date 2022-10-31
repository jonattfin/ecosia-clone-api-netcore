using Ecosia.Api.Models.Domain;
using Ecosia.Api.Repositories;
using MediatR;

namespace Ecosia.Api.Handlers;

public class UpdateProjectHandler : BaseHandler<UpdateProjectCommand, Project>
{
    public UpdateProjectHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public override async Task<Project> Handle(UpdateProjectCommand command, CancellationToken cancellationToken)
    {
        var project = await _unitOfWork.ProjectRepository.UpdateAsync(command.Project);
        await _unitOfWork.SaveChangesAsync();

        return project;
    }
}

public class UpdateProjectCommand : IRequest<Project>
{
    public Project Project { get; set; }
}