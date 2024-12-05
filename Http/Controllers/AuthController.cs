using eticketing.Application.Security;
using eticketing.Exceptions;
using eticketing.Http.Requests;
using eticketing.Infrastructure.Database;
using eticketing.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;

[Route("api")]
[ApiController]
public class AuthController(JwtService jwtService, ETicketingDbContext dbContext) : ControllerBase
{
    private readonly JwtService _jwtService = jwtService;
    private readonly ETicketingDbContext _dbContext = dbContext;

    [HttpPost("admin/login")]
    public async Task<IActionResult> AdminLogin([FromBody] LoginRequest request)
    {
        Console.WriteLine("Admin logi controller invoked ...");
        Console.WriteLine("request.email => ", request.Email);

        if (
            request == null
            || string.IsNullOrEmpty(request.Email)
            || string.IsNullOrEmpty(request.Password)
        )
        {
            throw new BadRequestException("payload data is not valid");
        }

        var admin = await _dbContext.Admin.FirstOrDefaultAsync(a => a.Email == request.Email);

        if (admin == null)
        {
            throw new UnauthenticatedException("invalid credential");
        }

        bool isValidPassword = PasswordHasher.ValidatePassword(request.Password, admin.Password);

        if (!isValidPassword)
        {
            throw new UnauthenticatedException("invalid credential");
        }

        var userAccessTokenData = new UserAccessTokenData(admin.Id, Roles.Admin);
        var token = _jwtService.Create(userAccessTokenData);

        return Ok(new { Token = token });
    }
}
