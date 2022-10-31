
using AutoMapper;
using Ecosia.Api.Domain.Features.Projects.Models;

namespace Ecosia.Api.Persistence.Profiles;

public class PersistenceProfile : Profile
{
    public PersistenceProfile()
    {
        CreateMap<Entities.Project, Project>().ReverseMap();
    }
}