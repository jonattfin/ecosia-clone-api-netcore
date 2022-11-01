using AutoMapper;
using Ecosia.Api.Domain.Features.Projects.Models;
using Ecosia.Api.Features.Projects.Models;

namespace Ecosia.Api.Features.Projects.Profiles;

public class ProjectProfile : Profile
{
    public ProjectProfile()
    {
        CreateMap<AddProjectRequest, Project>();
        CreateMap<UpdateProjectRequest, Project>().ReverseMap();
        CreateMap<Project, ProjectResponse>();
    }
}