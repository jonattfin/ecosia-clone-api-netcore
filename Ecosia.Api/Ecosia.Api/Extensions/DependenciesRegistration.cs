using Ecosia.Api.Domain.Models;
using Ecosia.Api.Domain.Repositories;
using Ecosia.Api.Persistence.Contexts;
using Ecosia.Api.Persistence.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ecosia.Api.Extensions;

public static class DependenciesRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        
        services.AddMediatR(assemblies);
        services.AddAutoMapper(assemblies);

        services.AddDbContext<EcosiaDbContext>(options => options.UseInMemoryDatabase(databaseName: "EcosiaDbLocal"));

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IRepository<Project>, ProjectRepository>();

        return services;
    }
}