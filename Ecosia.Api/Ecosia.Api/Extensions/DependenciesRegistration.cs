using System.Reflection;
using Ecosia.Api.Contexts;
using Ecosia.Api.Models.Domain;
using Ecosia.Api.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ecosia.Api.Extensions;

public static class DependenciesRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        services.AddDbContext<EcosiaDbContext>(options => options.UseInMemoryDatabase(databaseName: "EcosiaDbLocal"));

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IRepository<Project>, ProjectRepository>();

        return services;
    }
}