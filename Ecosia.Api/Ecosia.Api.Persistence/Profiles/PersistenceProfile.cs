
using AutoMapper;
using Ecosia.Api.Domain.Features.Projects.Models;

namespace Ecosia.Api.Persistence.Profiles;

public class PersistenceProfile : Profile
{
    public PersistenceProfile()
    {
        CreateMap<Entities.ProjectEntity, Project>().ReverseMap();
        CreateMap<Entities.TagEntity, Tag>().ReverseMap();
    }
}