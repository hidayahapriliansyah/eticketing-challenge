using eticketing.Application.Security;
using eticketing.Application.Services;
using eticketing.Exceptions;
using eticketing.Http.Requests;
using eticketing.Http.Responses;
using eticketing.Infrastructure.Database;
using eticketing.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eticketing.Http.Controllers;

[Route("api")]
[ApiController]
public class AuthController(
    JwtService jwtService,
    ETicketingDbContext dbContext,
    CustomerService customerService
) : ControllerBase
{
    private readonly JwtService _jwtService = jwtService;
    private readonly ETicketingDbContext _dbContext = dbContext;
    private readonly CustomerService _customerService = customerService;

    [HttpPost("admin/login")]
    [AllowAnonymous]
    public async Task<ActionResult<ApiResponse<LoginResponse>>> AdminLogin(
        [FromBody] LoginRequest request
    )
    {
        if (
            request == null
            || string.IsNullOrEmpty(request.Email)
            || string.IsNullOrEmpty(request.Password)
        )
        {
            throw new BadRequestException("payload data is not valid");
        }

        var admin =
            await _dbContext.Admin.FirstOrDefaultAsync(a => a.Email == request.Email)
            ?? throw new UnauthenticatedException("invalid credential");
        bool isValidPassword = PasswordHasher.ValidatePassword(request.Password, admin.Password);

        if (!isValidPassword)
        {
            throw new UnauthenticatedException("invalid credential");
        }

        var userAccessTokenData = new UserAccessTokenData(admin.Id, Roles.Admin);
        var token = _jwtService.Create(userAccessTokenData);

        var response = new ApiResponse<LoginResponse>
        {
            Success = true,
            Message = "success login",
            Data = new LoginResponse { Token = token, Role = "Admin" },
        };

        return Ok(response);
    }

    [HttpPost("customer/login")]
    [AllowAnonymous]
    public async Task<ActionResult<ApiResponse<LoginResponse>>> CustomerLogin(
        [FromBody] LoginRequest request
    )
    {
        if (
            request == null
            || string.IsNullOrEmpty(request.Email)
            || string.IsNullOrEmpty(request.Password)
        )
        {
            throw new BadRequestException("payload data is not valid");
        }

        var customer =
            await _dbContext.Customer.FirstOrDefaultAsync(a => a.Email == request.Email)
            ?? throw new UnauthenticatedException("invalid credential");
        bool isValidPassword = PasswordHasher.ValidatePassword(request.Password, customer.Password);

        if (!isValidPassword)
        {
            throw new UnauthenticatedException("invalid credential");
        }

        var userAccessTokenData = new UserAccessTokenData(customer.Id, Roles.Customer);
        var token = _jwtService.Create(userAccessTokenData);

        var response = new ApiResponse<LoginResponse>
        {
            Success = true,
            Message = "success login",
            Data = new LoginResponse { Token = token, Role = "Customer" },
        };

        return Ok(response);
    }

    [HttpPost("customers")]
    public async Task<ActionResult<ApiResponse<RegisterCustomerReponse>>> RegisterCustomer(
        [FromBody] RegisterCustomerRequest request
    )
    {
        var response = await _customerService.CreateCustomer(request);
        return CreatedAtAction(nameof(CustomerLogin), new { id = response.Data!.Id }, response);
    }
}
