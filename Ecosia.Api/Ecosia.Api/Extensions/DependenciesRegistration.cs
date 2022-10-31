using System.Text;
using Ecosia.Api.Domain.Features.Projects.Models;
using Ecosia.Api.Domain.Repositories;
using Ecosia.Api.Persistence.Contexts;
using Ecosia.Api.Persistence.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Ecosia.Api.Extensions;

public static class DependenciesRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services,
        ConfigurationManager configurationManager)
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();

        services.AddMediatR(assemblies);
        services.AddAutoMapper(assemblies);

        services.AddDbContext<EcosiaDbContext>(options => options.UseInMemoryDatabase(databaseName: "EcosiaDbLocal"));

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IRepository<Project>, ProjectRepository>();

        services.AddAuthentication("Bearer").AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configurationManager["Authentication:Issuer"],
                    ValidAudience = configurationManager["Authentication:Audience"],
                    IssuerSigningKey =
                        new SymmetricSecurityKey(
                            Encoding.ASCII.GetBytes(configurationManager["Authentication:SecretForKey"]))
                };
            }
        );

        return services;
    }
}