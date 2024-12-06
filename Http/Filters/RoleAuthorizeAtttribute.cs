using eticketing.Exceptions;
using eticketing.Models;
using Microsoft.AspNetCore.Mvc.Filters;

namespace eticketing.Http.Filters;

public class RoleAuthorizeAttribute(params string[] roles) : ActionFilterAttribute
{
    private readonly string[] _allowedRoles = roles;

    public override void OnResultExecuting(ResultExecutingContext context)
    {
        context.HttpContext.Items.TryGetValue("User", out var user);
        if (user == null)
        {
            throw new UnauthenticatedException("Please login");
        }
        var userRole = (user as UserAccessTokenData)?.Role.ToString();
        if (!_allowedRoles.Contains(userRole))
        {
            throw new ForbiddenException();
        }

        base.OnResultExecuting(context);
    }
}
