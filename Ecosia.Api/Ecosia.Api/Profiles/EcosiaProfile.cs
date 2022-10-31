using AutoMapper;
using Ecosia.Api.Models.Domain;
using Ecosia.Api.Models.Requests;
using Ecosia.Api.Models.Responses;

namespace Ecosia.Api.Profiles;

public class EcosiaProfile : Profile
{
    public EcosiaProfile()
    {
        CreateMap<AddProjectRequest, Project>();
        CreateMap<UpdateProjectRequest, Project>();
        CreateMap<Project, ProjectResponse>();
    }
}