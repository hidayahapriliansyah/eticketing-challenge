using System.IdentityModel.Tokens.Jwt;
using System.Text;
using eticketing.Application.Security;
using eticketing.Configuration;
using eticketing.Exceptions;
using eticketing.Helper;
using Microsoft.IdentityModel.Tokens;

namespace eticketing.Http.Middlewares;

public class AuthMiddleware : IMiddleware
{
    private readonly string _secretKey = SecurityConfig.JwtPrivateKey;

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        if (
            context.Request.Headers.TryGetValue("Authorization", out var tokenHeader)
            && tokenHeader.ToString().StartsWith("Bearer ")
        )
        {
            var token = tokenHeader.ToString()["Bearer ".Length..].Trim();
            try
            {
                var user = JwtService.VerifyUserAccessTokenData(token);
                context.Items["User"] = user;
            }
            catch (Exception ex)
            {
                throw new UnauthenticatedException();
                // throw un authenticated if jwt is expired berdasarkan message
                // Console.WriteLine($"Authentication failed: {ex.Message}");
            }
        }
        await next(context);
    }
}
