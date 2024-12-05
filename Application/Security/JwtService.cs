using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using eticketing.Configuration;
using eticketing.Models;
using Microsoft.IdentityModel.Tokens;

namespace eticketing.Application.Security;

public class JwtService
{
    public string Create(UserAccessTokenData user)
    {
        var handler = new JwtSecurityTokenHandler();

        var privateKey = Encoding.UTF8.GetBytes(SecurityConfig.JwtPrivateKey);

        var credentials = new SigningCredentials(
            new SymmetricSecurityKey(privateKey),
            SecurityAlgorithms.HmacSha256
        );

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            SigningCredentials = credentials,
            Expires = DateTime.UtcNow.AddHours(1),
            Subject = GenerateClaims(user),
        };

        var token = handler.CreateToken(tokenDescriptor);
        return handler.WriteToken(token);
    }

    private static ClaimsIdentity GenerateClaims(UserAccessTokenData user)
    {
        var ci = new ClaimsIdentity();

        ci.AddClaim(new Claim("id", user.Id.ToString()));
        ci.AddClaim(new Claim("role", user.Role.ToString()));

        return ci;
    }
}
