using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using eticketing.Configuration;
using eticketing.Helper;
using eticketing.Models;
using Microsoft.IdentityModel.Tokens;

namespace eticketing.Application.Security;

public class JwtService
{
    private static readonly string _secretKey = SecurityConfig.JwtPrivateKey;

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

    public static ClaimsIdentity GenerateClaims(UserAccessTokenData user)
    {
        var ci = new ClaimsIdentity();

        ci.AddClaim(new Claim("id", user.Id.ToString()));
        ci.AddClaim(new Claim("role", user.Role.ToString()));

        return ci;
    }

    public static UserAccessTokenData VerifyUserAccessTokenData(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_secretKey);

        tokenHandler.ValidateToken(
            token,
            new TokenValidationParameters
            {
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
            },
            out var validatedToken
        );

        var jwtToken = (JwtSecurityToken)validatedToken;

        var user = jwtToken.ToUserAccessTokenData();
        return user;
    }
}
