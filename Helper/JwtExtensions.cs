using System.IdentityModel.Tokens.Jwt;
using eticketing.Exceptions;
using eticketing.Models;

namespace eticketing.Helper;

public static class JwtExtensions
{
    public static UserAccessTokenData ToUserAccessTokenData(this JwtSecurityToken jwtToken)
    {
        var idClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
        if (idClaim == null || !Guid.TryParse(idClaim, out var id))
        {
            throw new UnauthenticatedException("Invalid or missing 'id' claim");
        }

        var roleClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "role")?.Value;
        if (roleClaim == null || !Enum.TryParse(roleClaim, true, out Roles role))
        {
            throw new UnauthenticatedException("Invalid or missing 'role' claim");
        }

        var user = new UserAccessTokenData(Id: id, Role: role);

        return user;
    }
}
