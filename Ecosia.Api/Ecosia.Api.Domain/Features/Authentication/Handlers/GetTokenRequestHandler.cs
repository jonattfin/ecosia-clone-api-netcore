using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Ecosia.Api.Domain.Features.Shared.Handlers;
using MediatR;
using Microsoft.IdentityModel.Tokens;

namespace Ecosia.Api.Domain.Features.Authentication.Handlers;

public class GetTokenRequestHandler : BaseRequestHandler<GetTokenCommand, string>
{
    public override async Task<string> Handle(GetTokenCommand command, CancellationToken cancellationToken)
    {
        var securityKey =
            new SymmetricSecurityKey(Encoding.ASCII.GetBytes(command.Secret));

        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var jwtSecurityToken = new JwtSecurityToken(
            command.Issuer,
            command.Audience,
            new List<Claim>(),
            DateTime.UtcNow,
            DateTime.UtcNow.AddHours(1),
            signingCredentials);

        return await Task.FromResult(new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken));
    }
}

public record GetTokenCommand(string Secret, string Issuer, string Audience) : IRequest<string>;