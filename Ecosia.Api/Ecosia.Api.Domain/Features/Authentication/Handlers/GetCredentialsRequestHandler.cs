using Ecosia.Api.Domain.Features.Shared.Handlers;
using MediatR;

namespace Ecosia.Api.Domain.Features.Authentication.Handlers;

public class GetCredentialsRequestHandler: BaseRequestHandler<GetCredentialsQuery, bool>
{
    public override async Task<bool> Handle(GetCredentialsQuery query, CancellationToken cancellationToken)
    {
        return await Task.FromResult(query.Username == "catalin" && query.Password == "parola");
    }
}

public record GetCredentialsQuery(string Username, string Password) : IRequest<bool>;