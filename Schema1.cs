using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace AuthHandlerTest;

public class Schema1Options : AuthenticationSchemeOptions
{
}

public class Schema1 : AuthenticationHandler<Schema1Options>
{
    private readonly IActionContextAccessor _actionContextAccessor;

    public Schema1(IOptionsMonitor<Schema1Options> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock, IActionContextAccessor actionContextAccessor) : base(options, logger, encoder, clock)
    {
        _actionContextAccessor = actionContextAccessor;
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        ControllerContext? controllerContext = _actionContextAccessor.ActionContext as ControllerContext;

        var claims = new[]
        {
                new Claim(ClaimTypes.NameIdentifier, "aa"),
            };

        var claimsIdentity = new ClaimsIdentity(claims, "schema1");
        var ticket = new AuthenticationTicket(new ClaimsPrincipal(claimsIdentity), "schema1");
        return Task.FromResult(AuthenticateResult.Success(ticket));
    }
}