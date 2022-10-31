using Ecosia.Api.Domain.Features.Authentication.Handlers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ecosia.Api.Features.Authentication;

[Route("api/authentication")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly IMediator _mediator;

    public AuthenticationController(IConfiguration configuration, IMediator mediator)
    {
        _configuration = configuration;
        _mediator = mediator;
    }

    [HttpPost("authenticate")]
    public async Task<ActionResult<string>> Authenticate(AuthenticationRequest request)
    {
        var isAuthorized = await _mediator.Send(new GetCredentialsQuery(request.UserName, request.Password));

        if (!isAuthorized)
        {
            return Unauthorized();
        }

        var token = await _mediator.Send(new GetTokenCommand(
            _configuration["Authentication:SecretForKey"],
            _configuration["Authentication:Issuer"],
            _configuration["Authentication:Audience"]));

        return Ok(token);
    }

    public record AuthenticationRequest(string UserName, string Password);
}